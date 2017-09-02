using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using word = System.UInt16;

using LANlib;
using MDM.Classes;
using MDM.Data;
using MDM.DlgBox;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Controls
{
    public enum ChannelStatus { Disabled, Inactive, Active, Ready, InProgress, SetCurrent, HighResistance, Paused, Restored/*, Error*/, Inaccessible, Disconnected }

#region struct SelectedPatient
    public struct SelectedPatient
    {
        public int ID, ProcID;
        public string Name, Diagnosis;
        public word ProcNum, CycleNum;
        public TProcSegment[] Segments;
        public int CurrSegment;

        public SelectedPatient(int id = Channel.NoSelection, int procID = Channel.NoSelection, string name = null, string dg = null, word procNum = 0, word cycleNum = 0, TProcSegment[] segments = null, int currSegment = 0)
        {
            ID = id;
            ProcID = procID;
            Name = name;
            Diagnosis = dg;
            ProcNum = procNum;
            CycleNum = cycleNum;
            Segments = segments ?? (id == Channel.NoSelection ? new TProcSegment[0] : Procedure.GetSegments(id, procNum));
            CurrSegment = currSegment;
        }
    }
#endregion

    /// <summary>
    /// Vizualizace jednoho kanálu přístroje MDM
    /// </summary>
    public partial class Channel : UserControl
    {
        internal const int NoSelection = -1;
        private word procDuration = (word)new Settings().ProcDur;
        private string chNumTxt, elapsedTxt;
        private double current = 0D;
        private BackgroundWorker chWorker = new BackgroundWorker();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

#region Vlastnosti
#region Number
        private byte chNum = 0;
        /// <summary>
        /// Číslo kanálu (1 - 6).
        /// </summary>
        public byte Number { get { return chNum; } }
#endregion

#region Channels
        public Channels Channels { get; set; }
#endregion

#region Status
        private ChannelStatus status = ChannelStatus.Disabled, oldStatus = ChannelStatus.Disabled;
        /// <summary>
        /// Aktuální stav kanálu.
        /// </summary>
        public ChannelStatus Status
        {
            get { return status; }
            set
            {
                if(status != value)
                {
                    oldStatus = status;
                    status = value;
                    switch(status)
                    {
                        case ChannelStatus.Disabled: reset(); break;
                        case ChannelStatus.Inactive: deactivate(); break;
                        case ChannelStatus.Active: activate(); break;
                        case ChannelStatus.Ready: preparedness(); break;
                        case ChannelStatus.InProgress: inProgress(); break;
                        case ChannelStatus.SetCurrent: setCurrent(); break;
                        case ChannelStatus.HighResistance: highResistance(); break;
                        case ChannelStatus.Paused: paused(); break;
                        case ChannelStatus.Restored: restored(); break;
                        //case ChannelStatus.Error: error(); break;
                        case ChannelStatus.Inaccessible: inaccessible(); break;
                        case ChannelStatus.Disconnected: disconnected(); break;
                    }
                }
            }
        }
#endregion

#region Patient
        private SelectedPatient patient = new SelectedPatient();
        /// <summary>
        /// Pacient vybraný k proceduře.
        /// </summary>
        public SelectedPatient Patient
        {
            get { return patient; }
            private set
            {
                if(patient.ID != value.ID)
                {
                    patient = value;
                    lbPatName.Text = patient.Name;
                    lbDiagnosis.Text = patient.Diagnosis;
                    lbProcNum.Text = patient.ProcNum == 0 ? string.Empty : patient.ProcNum.ToString();
                }
            }
        }

        private int procID
        {
            get { return Patient.ProcID; }
            set { patient.ProcID = value; }
            //set { patient = new SelectedPatient(Patient.ID, value, Patient.Name, Patient.Diagnosis, Patient.ProcNum, Patient.CycleNum, Patient.Segments, Patient.CurrSegment); }
        }
#endregion

#region Current
        private const double maxmA = 4D;
        private double _current = 0D, curstep = maxmA / 256D; // 0,015625 mA
        private volatile byte actCur = 0, toBeSet = 0;
        /// <summary>
        /// Aktuálně nastavená hodnota proudu.
        /// Nastavení probíhá po krocích ATCF = 1.
        /// </summary>
        public double Current
        {
            get { return _current; }
            set
            {
                if(_current != value)
                {
#if LAN
                    ResponseDG resp = LANFunc.ChRd(Number);

                    actCur = (byte)resp.InputR.Verified.AttenCoef;
#endif
                    toBeSet = (byte)(Math.Round(value / curstep, 0));
                    _current = value;
                }
            }
        }
        #endregion

#region Elapsed
        private word elapsed = word.MaxValue;
        /// <summary>
        /// Čas strávený procedurou nad vybraným pacientem.
        /// </summary>
        public word Elapsed
        {
            get { return elapsed; }
            set
            {
                word remain = (word)(procDuration - value);

                elapsed = value;
                ucMonitor.Elapsed = elapsed;
                ucMonitor.Remained = remain;
                //lbRemain.Text = string.Format("{0}:{1:D2}", remain / 60, remain % 60);
                //lbElapsed.Text = string.Format(elapsedTxt, elapsed / 60, elapsed % 60);
                if(elapsed != 0U) pbProgress.PerformStep();
                if(Status == ChannelStatus.InProgress || Status == ChannelStatus.SetCurrent || Status == ChannelStatus.Restored)
                {
                    if(ucMonitor.SegmentLeft == 0 && Patient.CurrSegment < Patient.Segments.Length)
                    {
                        patient.CurrSegment++;
                        ucMonitor.NextSegment();
                        ucMonitor.SegmentLeft = (word)(Patient.Segments[Patient.CurrSegment - 1].Duration * 60);
                        current = Current;
                        Current = .5;
                        while(actCur != toBeSet) Application.DoEvents();
#if LAN
                        LANFunc.ChMode(Number, 0, 0, 0, 0, 0);
#else
                        ucMonitor.Mode = 0;
                        ucMonitor.Sweep = ucMonitor.WS = ucMonitor.Status = 0;
#endif
                        Thread.Sleep(200);
#if LAN
                        ResponseDG resp = LANFunc.ChRd(Number);
                        if(resp.InputR.AIN2 > 0 && (resp.InputR.AIN2 - resp.InputR.AIN1) >= 52) Status = ChannelStatus.HighResistance; // příliš vysoká impedance - navlhčit elektrody
                        else
                        {
                            LANFunc.ChMode(Number, 2, Patient.Segments[Patient.CurrSegment - 1].WaveShape, Patient.Segments[Patient.CurrSegment - 1].TMax, Patient.Segments[Patient.CurrSegment - 1].TMin, Patient.Segments[Patient.CurrSegment - 1].TSweep);
                            Current = current;
                        }
#else
                        ucMonitor.Mode = 2;
                        ucMonitor.WS = Patient.Segments == null ? (word)0 : Patient.CurrSegment == 0 ? (word)0 : Patient.Segments[Patient.CurrSegment - 1].WaveShape;
                        ucMonitor.Sweep = Patient.Segments == null ? (word)0 : Patient.CurrSegment == 0 ? (word)0 : Patient.Segments[Patient.CurrSegment - 1].TSweep;
                        ucMonitor.Status = 0;
                        Current = current;
#endif
                    }
                    ucMonitor.SegmentLeft--;
                }
            }
        }

        ///// <summary>
        ///// Čas, který zbývá vykonat do ukončení procedury.
        ///// </summary>
        //public word Remained { get { return Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored ? (word)(procDuration - elapsed) : procDuration; } }
        #endregion

#region LEDBits
        private Bits ledBits = new Bits();
        private Bits LEDBits
        {
            get { return ledBits; }
            set
            {
                if(ledBits.Value != value.Value)
                {
                    ledBits = new Bits(value.Value);
                    Led.Color = Color.FromArgb(value[DioReg.LedR] ? 255 : 0, value[DioReg.LedG] ? 255 : 0, value[DioReg.LedB] ? 255 : 0);
                    Led.Blink(!value[DioReg.LedNBlink] ? 500 : 0);
                    Led.On = true;
                    Led.Refresh();
#if LAN
                    LANFunc.ChDio(Number, ledBits.ByteValue);
#endif
                }
            }
        }
#endregion

#region InOrder
        public bool InOrder
        {
            get { return status == ChannelStatus.InProgress || status == ChannelStatus.SetCurrent || status == ChannelStatus.Restored || status == ChannelStatus.Paused; }
        }
#endregion
#endregion

#region Konstruktor a destruktor
        /// <summary>
        /// Konstruktor kanálu s konkrétním číslem
        /// </summary>
        /// <param name="chnum">číslo přidělené kanálu</param>
        /// <param name="parent">odkaz na desku LAN</param>
        public Channel(byte chnum, Channels parent)
        {
            InitializeComponent();
            tbCurrent.Size = new Size(42, 226);
            tbCurrent.Enabled = false;
            chNum = chnum;
            Channels = parent;
            chNumTxt = groupBox1.Text;
            elapsedTxt = lbElapsed.Text;
            FontHeight = Width < 240 ? 8 : 10;
            Channel_FontChanged(null, null);
            pbProgress.Maximum = procDuration;
            chWorker.WorkerSupportsCancellation = true;
            chWorker.DoWork += chWorker_DoWork;
            timer.Interval = 1000;
            timer.Tick += timerTick;
            Led.Blink(0);
            groupBox1.Text = string.Format(chNumTxt, chNum);
            cbPatSelect.Text = string.Format("&{0}", chNum);
            pbProgress.Value = 0;
        }

        internal void DisposeMain()
        {
#if LAN
                LANFunc.ChRst(Number);
                LANFunc.LanChOnOff(Number, false);
#endif
            if(chWorker.IsBusy) chWorker.CancelAsync();
            //Thread.Sleep(1000);
            while(chWorker.IsBusy) Application.DoEvents();
            chWorker.Dispose();
            chWorker = null;
            timer.Stop();
            timer.Dispose();
            timer = null;
        }
        #endregion

#region Utility kanálu
        private void led(DioReg clr, bool blink)
        {
            Bits lb = new Bits();

            if(clr != DioReg.LedOff)
            {
                lb[clr] = true;
                lb[DioReg.LedNBlink] = !blink;
            }
            LEDBits = lb;
        }
        internal void LedRed(bool blink = false) { led(DioReg.LedR, blink); }
        internal void LedGreen(bool blink = false) { led(DioReg.LedG, blink); }
        internal void LedBlue(bool blink = false) { led(DioReg.LedB, blink); }
        internal void LedOff() { led(DioReg.LedOff, false); }

        private void currDecr(int dc)
        {
            int ac = actCur;

            while((ac - dc) < 0) dc--;
            actCur = (byte)(ac - dc);
        }

        private void currIncr(int ic)
        {
            int ac = actCur;

            while((ac + ic) > toBeSet) ic--;
            actCur = (byte)(ac + ic);
        }

#if LAN
        private bool bStatusD13 = false;

        private void processMBStatus(ResponseDG resp)
        {
            if(Status != ChannelStatus.Disabled && Status != ChannelStatus.Inactive/* && Status != ChannelStatus.Error*/ && Status != ChannelStatus.Inaccessible)
            {
                string msg = string.Empty;

                if(resp.InputR.Status[0]) msg = Resources.chStatusErr1 + Environment.NewLine;
                if(resp.InputR.Status[2]) msg += Resources.chStatusErr2 + Environment.NewLine;
                if(resp.InputR.Status[13] && !bStatusD13) msg += Resources.chStatusErr3 + Environment.NewLine;
                bStatusD13 = resp.InputR.Status[13];
                if(resp.InputR.Status[14])
                {
                    msg += Resources.chStatusErr4 + Environment.NewLine;
                    LANFunc.ChRst(Number);
                    Status = ChannelStatus.Inactive;
                }
                if(resp.InputR.Status[15])
                {
                    msg += Resources.chStatusErr5;
                    LANFunc.ChRst(Number);
                    Status = ChannelStatus.Inactive;
                }
                if(!string.IsNullOrEmpty(msg)) DialogBox.ShowError(msg, string.Format(Resources.chStatusErrHdr, Number));
            }
        }

        private void fillMonitor(ResponseDG resp)
        {
            ucMonitor.WS = resp.InputR.Verified.Waweform;
            ucMonitor.Sweep = resp.InputR.Verified.T3Sweep;
            ucMonitor.ATC = (byte)resp.InputR.Verified.AttenCoef;
            ucMonitor.DAC = resp.InputR.Verified.DAC;
            ucMonitor.DOUT = resp.InputR.Verified.DOUT.ByteValue;
            ucMonitor.Status = resp.InputR.Status.Value;
            ucMonitor.Mode = (byte)resp.InputR.Verified.Mode;
            if(Status == ChannelStatus.Active || Status == ChannelStatus.Ready || Status == ChannelStatus.HighResistance)
                ucMonitor.Ohms = resp.InputR.Verified.Mode == 0 ? (word)(resp.InputR.AIN2 - resp.InputR.AIN1) : word.MaxValue;
            else ucMonitor.Ohms = word.MaxValue;
        }

        private void processResponse(ResponseDG resp)
        {
            if(resp != null)
            {
                //chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
                fillMonitor(resp);
                processMBStatus(resp);
                if(Status == ChannelStatus.Active)
                {
                    if(actCur == toBeSet && resp.InputR.AIN2 > 0 && (resp.InputR.AIN2 - resp.InputR.AIN1) <= 48) Status = ChannelStatus.Ready;
                }
                else if(Status == ChannelStatus.Ready) // elektrody nejsou nasazeny ve stavu "připraven"
                {
                    if(actCur == toBeSet && resp.InputR.AIN2 > 0 && (resp.InputR.AIN2 - resp.InputR.AIN1) >= 52) Status = ChannelStatus.Active;
                }
                else if((Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored) && resp.InputR.Status[1])
                {
                    Status = ChannelStatus.HighResistance; // příliš vysoká impedance - navlhčit elektrody
                }
                else if(Status == ChannelStatus.HighResistance)
                {
                    if(actCur == (byte)(Math.Round(.5 / curstep, 0))) LANFunc.ChMode(Number, 0, 0, 0, 0, 0);
                    if(actCur == toBeSet && resp.InputR.AIN2 > 0 && (resp.InputR.AIN2 - resp.InputR.AIN1) <= 48)
                    {
                        LANFunc.ChMode(Number, 2, Patient.Segments[Patient.CurrSegment - 1].WaveShape, Patient.Segments[Patient.CurrSegment - 1].TMax, Patient.Segments[Patient.CurrSegment - 1].TMin, Patient.Segments[Patient.CurrSegment - 1].TSweep);
                        Current = current;
                        Status = oldStatus;
                    }
                }
                else if(oldStatus == ChannelStatus.Disconnected && Status != ChannelStatus.Disconnected)
                {
                    if(!chWorker.IsBusy) chWorker.RunWorkerAsync();
                }
                if(actCur != toBeSet)
                {
                    if(actCur < toBeSet)
                    {
                        if(oldStatus == ChannelStatus.HighResistance || oldStatus == ChannelStatus.Paused) currIncr(10);
                        else if(Status == ChannelStatus.Ready || Status == ChannelStatus.Active) currIncr(16);
                        else currIncr(6);
                    }
                    else
                    {
                        if(Status == ChannelStatus.Inactive || Status == ChannelStatus.Disabled || Status == ChannelStatus.Disconnected || Status == ChannelStatus.Inaccessible) currDecr(32);
                        else if(Status == ChannelStatus.Paused || Status == ChannelStatus.HighResistance) currDecr(16);
                        else currDecr(8);
                    }
                    LANFunc.ChAtCf(Number, actCur);
                    if(Status != ChannelStatus.SetCurrent) tbCurrent.Value = actCur;
                    //else _current = Math.Round(actCur * curstep, 2);
                }
            }
        }
#else
        private void fillMonitor()
        {
            ucMonitor.WS = Patient.Segments == null ? (word)0 : Patient.CurrSegment == 0 ? (word)0 : Patient.Segments[Patient.CurrSegment - 1].WaveShape;
            ucMonitor.Sweep = Patient.Segments == null ? (word)0 : Patient.CurrSegment == 0 ? (word)0 : Patient.Segments[Patient.CurrSegment - 1].TSweep;
            ucMonitor.ATC = actCur;
            ucMonitor.DAC = (word)(InOrder ? 0x8000 : 0);
            ucMonitor.DOUT = (byte)(InOrder ? 2 : 0);
            ucMonitor.Status = 0;
            ucMonitor.Mode = (byte)(InOrder ? 2 : 0);
        }

        private void processResponse()
        {
            fillMonitor();
            if(actCur != toBeSet)
            {
                if(actCur < toBeSet)
                {
                    if(oldStatus == ChannelStatus.HighResistance || oldStatus == ChannelStatus.Paused) currIncr(6);
                    else if(Status == ChannelStatus.Ready || Status == ChannelStatus.Active) currIncr(16);
                    else currIncr(2);
                }
                else
                {
                    if(Status == ChannelStatus.Inactive || Status == ChannelStatus.Disabled || Status == ChannelStatus.Disconnected || Status == ChannelStatus.Inaccessible) currDecr(32);
                    else if(Status == ChannelStatus.Paused || Status == ChannelStatus.HighResistance) currDecr(12);
                    else currDecr(4);
                }
                if(Status != ChannelStatus.SetCurrent) tbCurrent.Value = actCur;
                else _current = Math.Round(actCur * curstep, 2);
            }
            if(Status == ChannelStatus.Active) Status = ChannelStatus.Ready;
            else if(oldStatus == ChannelStatus.Disconnected && Status != ChannelStatus.Disconnected)
            {
                if(!chWorker.IsBusy) chWorker.RunWorkerAsync();
            }
        }
#endif
#endregion

#region Stavy kanálu
#region reset()
        /// <summary>
        /// Uvede kanál do stavu "nepřipojen/nepovolen". Z tohoto stavu do stavu "neaktivní" se kanál dostane fyzickým připojením přístroje MDM k počítači.
        /// </summary>
        private void reset()
        {
            if(!chWorker.IsBusy) chWorker.RunWorkerAsync();
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = tbCurrent.Enabled = cbPatSelect.Enabled = false;
            timer.Stop();
            Patient = new SelectedPatient();
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Bold);
            Current = current = 0D;
            pbProgress.Value = Elapsed = 0;
            Elapsed = 0;
            pbStatus.Image = Channels.ChannelsReadyImgs[Number - 1];
            lbStatus.Text = Resources.chDisconected;
            lbStatus.ForeColor = SystemColors.InactiveCaptionText;
            lbStatus.BackColor = SystemColors.Window;
            ucMonitor.On = false;
            ucMonitor.MonMode = Program.LoggedUser.Role == UserRole.SuperAdmin ? WpfUC.MonitorMode.Admin : WpfUC.MonitorMode.User;
#if LAN
            LANFunc.ChRst(Number);
            LANFunc.LanChOnOff(Number, false);
#endif
            LedOff();
        }
#endregion

#region deactivate()
        /// <summary>
        /// Uvede kanál do stavu "neaktivní". V tomto stavu je třeba vybrat pacienta, poté kanál přechází do stavu "aktivní".
        /// </summary>
        private void deactivate()
        {
            reset();
            lbStatus.Text = Resources.chInactive;
            lbStatus.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.BackColor = SystemColors.Window;
            cbPatSelect.Enabled = true;
        }
#endregion

#region activate()
        /// <summary>
        /// Uvede kanál do stavu "aktivní". V tomto stavu je třeba připojit elektrody a vložit je pacientovi na hlavu. Poté kanál přechází do stavu "připraven".
        /// </summary>
        private void activate()
        {
            cbPatSelect.Enabled = cbStop.Enabled = true;
            cbStart.Enabled = false;
            pbStatus.Image = Resources.program_stimsmart_vysoky_odpor;
            lbPatName.ForeColor = lbDiagnosis.ForeColor = lbProcNum.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.Text = Resources.chActive;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
#if LAN
            if(oldStatus == ChannelStatus.Inactive)
            {
                LANFunc.LanChOnOff(Number);
                LANFunc.ChRst(Number);
                LANFunc.ChDAC(Number);
                LANFunc.ChDOUT(Number, 2);
            }
#endif
            ucMonitor.Elapsed = 0;
            ucMonitor.Remained = procDuration;
            if(Patient.ID > NoSelection) ucMonitor.SegmentLeft = (word)(Patient.Segments[0].Duration * 60);
            ucMonitor.On = true;
            LedRed();
            Current = .5;
        }
        #endregion

#region ready()
        /// <summary>
        /// Uvede kanál do stavu "připraven". V tomto stavu se čeká na stisk tlačítka Začít, poté kanál přechází do stavu "léčení".
        /// V průběhu setrvání v tomto stavu se provádí měření odporu, pokud je odpor vysoký, přechází se do stavu "aktivní".
        /// </summary>
        private void preparedness()
        {
            if(Patient.ID > NoSelection)
            {
                cbPatSelect.Enabled = true;
                cbStart.Enabled = true;
                pbStatus.Image = Resources.program_stimsmart_kanal_pripraven_;
                lbStatus.Text = Resources.chReady;
                lbStatus.ForeColor = Color.White;
                lbStatus.BackColor = Color.Green;
                Elapsed = 0;
                LedGreen(true);
            }
            else Status = ChannelStatus.Inactive;
        }
#endregion

#region inProgress()
        /// <summary>
        /// Uvede kanál do stavu "léčení". Z tohoto stavu může kanál přejít do stavu "nastavení proudu", "příliš vysoký odpor", "pozastaven", "odpojený" nebo "neaktivní".
        /// </summary>
        private void inProgress()
        {
            cbPatSelect.Enabled = cbStart.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = cbSetCurrent.Enabled = true;
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Regular);
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            lbPatName.ForeColor = lbDiagnosis.ForeColor = lbProcNum.ForeColor = lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            pbStatus.Image = Resources.program_stimsmart_probiha_procedura;
            lbStatus.Text = Resources.chInProgress;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            tbCurrent.Enabled = false;
            if(oldStatus == ChannelStatus.Ready)
            {
                patient.CurrSegment = 1;
                ucMonitor.NextSegment();
                ucMonitor.SegmentLeft = (word)(Patient.Segments[0].Duration * 60);
#if LAN
                LANFunc.ChDAC(Number);
                LANFunc.ChDOUT(Number, 2);
                LANFunc.ChMode(Number, 2, Patient.Segments[0].WaveShape, Patient.Segments[0].TMax, Patient.Segments[0].TMin, Patient.Segments[0].TSweep);
#endif
                procID = PatProc.AddProcedure(Patient.ID, Program.LoggedUser.ID, Number);
            }
            timer.Start();
            LedGreen();
        }
#endregion

#region setCurrent()
        /// <summary>
        /// Uvede kanál do stavu "nastav proud". V tomto stavu je třeba nastavit hodnotu proudu. Poté kanál přechází do stavu "procedura probíhá" nebo "obnoven".
        /// </summary>
        private void setCurrent()
        {
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Bold);
            cbPause.Enabled = false;
            tbCurrent.Enabled = true;
            tbCurrent.Focus();
            lbCurrent.ForeColor = SystemColors.ActiveCaptionText;
            pbStatus.Image = Resources.program_stimsmart_nastavte_proud;
            lbStatus.Text = Resources.chSetCurrent;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
        }
#endregion

#region highResistance()
        /// <summary>
        /// Uvede kanál do stavu "příliš vysoký odpor". V tomto stavu je třeba navlhčit elektrody na hlavě pacienta, aby se odpor snížil.
        /// Poté kanál přechází do stavu "procedura probíhá" nebo "obnoven".
        /// </summary>
        private void highResistance()
        {
            paused();
            cbPause.Enabled = false;
            pbStatus.Image = Resources.program_stimsmart_vysoky_odpor;
            lbStatus.Text = Resources.chHighResistance;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
            current = Current;
            Current = .5;
            Sound.Beep();
        }
        #endregion

#region paused()
        //TODO: pozastavení a obnovení také na mezerník
        /// <summary>
        /// Uvede kanál do stavu "pozastaven". Z tohoto stavu může kanál přejít do stavu "neaktivní" nebo "obnoven".
        /// </summary>
        private void paused()
        {
            cbPatSelect.Enabled = cbStart.Enabled = cbSetCurrent.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = true;
            cbPause.Text = Resources.cbPauseResumeText;
            cbPause.Image = Resources.start;
            lbStatus.Text = Resources.chPaused;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
            LedGreen(true);
        }
#endregion

#region restored()
        /// <summary>
        /// Uvede kanál do stavu "obnoven". Z tohoto stavu může kanál přejít do stavu "pozastaven", "chyba" nebo "neaktivní".
        /// </summary>
        private void restored()
        {
            inProgress();
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            lbStatus.Text = Resources.chRestored;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
        }
#endregion

#region error() - odstraněno
        ///// <summary>
        ///// Uvede kanál do stavu "chyba". Z tohoto stavu může kanál přejít do stavu "obnoven" nebo "neaktivní".
        ///// </summary>
        //private void error()
        //{
        //    cbPatSelect.Enabled = false;
        //    lbStatus.Text = Resources.chError;
        //    lbStatus.ForeColor = Color.Yellow;
        //    lbStatus.BackColor = Color.Red;
        //    LedRed(true);
        //}
#endregion

#region inaccessible()
        /// <summary>
        /// Uvede kanál do stavu "nepřístupný". Z tohoto stavu se kanál nedostane do žádného jiného stavu.
        /// </summary>
        private void inaccessible()
        {
            if(chWorker.IsBusy) chWorker.CancelAsync();
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = tbCurrent.Enabled = cbPatSelect.Enabled = false;
            timer.Stop();
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            Current = 0D;
            pbProgress.Value = tbCurrent.Value = 0;
            Elapsed = 0;
            ucMonitor.On = false;
            pbStatus.Image = Channels.ChannelsErrorImgs[Number - 1];
            lbStatus.Text = Resources.chInaccessible;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Red;
            LEDBits = new Bits();
            if(procID > NoSelection)
            {
                PatProc.FinishProcedure(procID, Elapsed, ProcResult.Failed);
                Log.ErrorToLog(string.Format(Resources.chNum, Number), string.Format(Resources.chUserProcCompleted, Patient.Name, Patient.ProcNum, Patient.CycleNum, Patient.ProcNum == 1 ? "st" : Patient.ProcNum == 2 ? "nd" : Patient.ProcNum == 3 ? "rd" : "th", Elapsed / 60, Elapsed % 60), false);
                Patient = new SelectedPatient();
            }
            Patient = new SelectedPatient();
#if LAN
            LANFunc.ChRst(Number);
            LANFunc.LanChOnOff(Number, false);
#endif
        }
#endregion

#region Disconnected
        /// <summary>
        /// Uvede kanál do stavu "odpojený". Do tohoto stavu se kanál dostane po přerušení spojení s deskou LAN přístroje MDM.
        /// Po opětovném navázání spojení se kanál vrací do stavu "neaktivní".
        /// </summary>
        private void disconnected()
        {
            inaccessible();
        }
#endregion
#endregion

#region Obsluhy událostí
        /// <summary>
        /// Obsluha časovače kanálu.
        /// </summary>
        /// <param name="sender">odesílatel události</param>
        /// <param name="e">parametry události</param>
        private void timerTick(object sender, EventArgs e)
        {
            if(Status == ChannelStatus.InProgress || Status == ChannelStatus.SetCurrent || Status == ChannelStatus.Restored) Elapsed++;
            if(Elapsed >= procDuration)
            {
                if(procID > NoSelection) PatProc.FinishProcedure(procID, Elapsed, ProcResult.Finished);
                Log.InfoToLog(string.Format(Resources.chNum, Number), string.Format(Resources.chUserProcCompleted, Patient.Name, Patient.ProcNum, Patient.CycleNum, Patient.ProcNum == 1 ? "st" : Patient.ProcNum == 2 ? "nd" : Patient.ProcNum == 3 ? "rd" : "th", Elapsed / 60, Elapsed % 60));
                Status = ChannelStatus.Inactive;
                Sound.Beep();
            }
        }

        /// <summary>
        /// Periodická obsluha kanálu. Generuje periodicky čtecí požadavek a předá ho zpracující rutině.
        /// </summary>
        /// <param name="sender">odesílatel události</param>
        /// <param name="e">parametry události</param>
        private void chWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(chWorker.IsBusy && !chWorker.CancellationPending)
            {
                if(IsHandleCreated && !IsDisposed)
                {
                    try
                    {
#if LAN
                        ResponseDG resp = LANFunc.ChRd(Number);

                        if(IsHandleCreated && !IsDisposed) Invoke(new MethodInvoker(delegate { processResponse(resp); }));
#else
                        if(IsHandleCreated && !IsDisposed) Invoke(new MethodInvoker(delegate { processResponse(); }));
#endif
                    }
                    catch { }
                    Thread.Sleep(200);
                }
            }
            e.Cancel = true;
        }
        private void cbPatSelect_Click(object sender, EventArgs e)
        {
            using(wPatSelect frm = new wPatSelect())
            {
                Patient = new SelectedPatient();
                Status = ChannelStatus.Inactive;
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    if(!Channels.PatientAttached(frm.PatientID))
                    {
                        Patient = new SelectedPatient(frm.PatientID, NoSelection, frm.PatientName, frm.PatientDiagnosis, frm.PatientProcNum, frm.PatientCycleNum);
                        ucMonitor.Segments = Patient.Segments.Select(s => s.Duration).ToArray();
                        Status = ChannelStatus.Active;
                    }
                    else DialogBox.ShowWarn(Resources.patDuplMsg, Resources.patDuplMsgH);
                }
            }
        }

        private void cbSetCurrent_EnabledChanged(object sender, EventArgs e)
        {
            lbCurrent.ForeColor = cbSetCurrent.Enabled ? SystemColors.ActiveCaptionText : SystemColors.InactiveCaptionText;
            tbCurrent.Enabled = cbSetCurrent.Enabled;
        }


        private void cbSetCurrent_Click(object sender, EventArgs e)
        {
            if(Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored) Status = ChannelStatus.SetCurrent;
            else if(Status == ChannelStatus.SetCurrent)
            {
                //Current = Math.Round(tbCurrent.Value * (maxmA / tbCurrent.Maximum), 2);
                Current = Math.Round((double)tbCurrent.Value * curstep, 2);
                Status = oldStatus;
            }
        }

        private void cbStart_Click(object sender, EventArgs e)
        {
            Status = ChannelStatus.InProgress;
        }

        private void cbPause_Click(object sender, EventArgs e)
        {
            if(Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored)
            {
                current = Current;
                Current = 0D;
                Status = ChannelStatus.Paused;
                timer.Stop();
            }
            else if(Status == ChannelStatus.Paused)
            {
                Current = current;
                Status = ChannelStatus.Restored;
                timer.Start();
            }
        }

        private void cbStop_Click(object sender, EventArgs e)
        {
            if(InOrder || Status == ChannelStatus.HighResistance)
            {
                timer.Stop();
                if(Status == ChannelStatus.HighResistance && elapsed < procDuration && DialogBox.ShowYN(Resources.procAbortQ, Resources.procAbortH) == DialogResult.Yes)
                {
                    if(procID > NoSelection) PatProc.FinishProcedure(procID, Elapsed, ProcResult.Failed);
                    Log.ErrorToLog(string.Format(Resources.chNum, Number), string.Format(Resources.chUserProcCompleted, Patient.Name, Patient.ProcNum, Patient.CycleNum, Patient.ProcNum == 1 ? "st" : Patient.ProcNum == 2 ? "nd" : Patient.ProcNum == 3 ? "rd" : "th", Elapsed / 60, Elapsed % 60));
                    Status = ChannelStatus.Inactive;
                }
                else if(elapsed < procDuration && DialogBox.ShowYN(Resources.procAbortQ, Resources.procAbortH) == DialogResult.Yes)
                {
                    if(procID > NoSelection) PatProc.FinishProcedure(procID, Elapsed, ProcResult.Prematurely);
                    Log.WarningToLog(string.Format(Resources.chNum, Number), string.Format(Resources.chUserProcCompleted, Patient.Name, Patient.ProcNum, Patient.CycleNum, Patient.ProcNum == 1 ? "st" : Patient.ProcNum == 2 ? "nd" : Patient.ProcNum == 3 ? "rd" : "th", Elapsed / 60, Elapsed % 60));
                    Status = ChannelStatus.Inactive;
                }
                else timer.Start();
            }
            else Status = ChannelStatus.Inactive;
        }

        private void Channel_FontChanged(object sender, EventArgs e)
        {
            foreach(Control c in Controls) c.Font = Font;
        }

        private void tbCurrent_EnabledChanged(object sender, EventArgs e)
        {
            tbCurrent.TickColor = tbCurrent.TrackerColor = tbCurrent.TrackLineColor = tbCurrent.Enabled ? Color.Indigo : SystemColors.Control;
        }

        private void tbCurrent_MouseDown(object sender, MouseEventArgs e)
        {
            cbSetCurrent.PerformClick();
        }

        private void tbCurrent_ValueChanged(object sender, decimal value)
        {
            if(value > byte.MaxValue) value = byte.MaxValue;
            if(value < byte.MinValue) value = byte.MinValue;
            //_current = Math.Round((double)value * curstep, 2);
            //lbCurrent.Text = _current.ToString("F2");// + " mA";
            lbCurrent.Text = Math.Round((double)value * curstep, 2).ToString("F2");// + " mA";
            if(Status == ChannelStatus.SetCurrent) toBeSet = (byte)value;
        }
#endregion
    }
}
