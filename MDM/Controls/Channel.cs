﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using word = System.UInt16;

using LANlib;
using MDM.Classes;
using MDM.DlgBox;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Controls
{
    public enum ChannelStatus { Disabled, Inactive, Active, Ready, InProgress, SetCurrent, HighResistance, Paused, Restored, Error, Inaccessible }

    #region struct SelectedPatient
    public struct SelectedPatient
    {
        public int ID;
        public string Name, Diagnosis;
        public word ProcNum, CycleNum;

        public SelectedPatient(int id = Channel.NoSelPat, string name = null, string dg = null, word procNum = 0, word cycleNum = 0)
        {
            ID = id;
            Name = name;
            Diagnosis = dg;
            ProcNum = procNum;
            CycleNum = cycleNum;
        }
    }
    #endregion

    /// <summary>
    /// Vizualizace jednoho kanálu přístroje MDM
    /// </summary>
    public partial class Channel : UserControl
    {
        private string[] aStatus = new string[] { "DI", "IN", "AC", "RD", "IP", "SC", "HR", "PA", "RE", "ER", "IA" };
        public const int NoSelPat = -1;
        private word procDuration = (word)new Settings().ProcDur;
        private word elapsed = word.MaxValue;
        private string chNumTxt, remainTxt;
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

        #region Response
        //private LANQueue<ResponseDG> respQueue = new LANQueue<ResponseDG>();

        ///// <summary>
        ///// Začátek/konec response fronty daného kanálu
        ///// </summary>
        //public ResponseDG Response
        //{
        //    get
        //    {
        //        ResponseDG res = null;

        //        if(respQueue.Count > 0) res = respQueue.Top();
        //        return res;
        //    }
        //    set
        //    {
        //        respQueue.Push(value);
        //    }
        //}
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
                        case ChannelStatus.Ready: ready(); break;
                        case ChannelStatus.InProgress: inProgress(); break;
                        case ChannelStatus.SetCurrent: setCurrent(); break;
                        case ChannelStatus.HighResistance: highResistance(); break;
                        case ChannelStatus.Paused: paused(); break;
                        case ChannelStatus.Restored: restored(); break;
                        case ChannelStatus.Error: error(); break;
                        case ChannelStatus.Inaccessible: inaccessible(); break;
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
                    lbProcNum.Text = patient.ProcNum.ToString();
                }
            }
        }
        #endregion

        #region Current
        const double maxmA = 4.0;
        double _current = 0D;
        /// <summary>
        /// Aktuálně nastavená hodnota proudu.
        /// Nastavení probíhá po krocích ATCF = 32.
        /// </summary>
        public double Current
        {
            //get { return tbCurrent.Value * .015625; }
            get { return Math.Round(tbCurrent.Value * (maxmA / tbCurrent.Maximum), 2); }
            set
            {
                tbCurrent.Value = (int)(Math.Round(value / (maxmA / tbCurrent.Maximum), 0));
                if(_current != value)
                {
                    ResponseDG resp = LANFunc.ChRd(Number);
                    int actCur = resp.InputR.Verified.AttenCoef, toBeSet = tbCurrent.Value;// (int)(Math.Round(value / (maxmA / tbCurrent.Maximum), 0));
                    //int actCur = tbCurrent.Value, toBeSet = (int)(Math.Round(value / (maxmA / tbCurrent.Maximum), 0));

                    _current = value;
                    if(toBeSet > byte.MaxValue) toBeSet = byte.MaxValue;
                    if(toBeSet < 0) toBeSet = 0;
                    if(actCur < toBeSet)
                        for(int i = actCur; i < toBeSet+32; i += 32)
                        {
                            int c = i;

                            if(c > toBeSet) c = toBeSet;
                            if(c != actCur)
                            {
                                LANFunc.ChAtCf(Number, (byte)c);
                                //DialogBox.ShowInfo(string.Format("^ LANFunc.ChAtCf({0}, {1})", Number, c), string.Format("Set Current to Channel {0}", Number));
                                //Thread.Sleep(200);
                            }
                        }
                    else if(actCur > toBeSet)
                        for(int i = actCur; i > toBeSet-32; i -= 32)
                        {
                            int c = i;

                            if(c < toBeSet) c = toBeSet;
                            if(c != actCur)
                            {
                                LANFunc.ChAtCf(Number, (byte)c);
                                //DialogBox.ShowInfo(string.Format("v LANFunc.ChAtCf({0}, {1})", Number, c), string.Format("Set Current to Channel {0}", Number));
                                //Thread.Sleep(200);
                            }
                        }
                    tbCurrent.Value = toBeSet;
                    //if(Status == ChannelStatus.SetCurrent)
                    //    DialogBox.ShowInfo(string.Format("actCur = {0}, toBeSet = {1} ({2})", actCur, toBeSet, tbCurrent.Value), string.Format("Set Current to Channel {0}", Number));
                }
            }
        }
        #endregion

        #region Elapsed, Remained
        /// <summary>
        /// Čas strávený procedurou nad vybraným pacientem.
        /// </summary>
        public word Elapsed
        {
            get { return elapsed; }
            set
            {
                //if(elapsed != value)
                {
                    word remain = (word)(procDuration - value);

                    elapsed = value;
                    lbElapsed.Text = string.Format("{0}:{1:D2}", elapsed / 60, elapsed % 60);
                    lbRemain.Text = string.Format(remainTxt, remain / 60, remain % 60);
                    if(elapsed != 0U) pbProgress.PerformStep();
                }
            }
        }

        /// <summary>
        /// Čas, který zbývá vykonat do ukončení procedury.
        /// </summary>
        public word Remained { get { return Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored ? (word)(procDuration - elapsed) : procDuration; } }
        #endregion

        #region ChannelEnabled
        private bool enabled = true;
        public bool ChannelEnabled
        {
            get { return enabled; }
            set
            {
                if(enabled != value)
                {
                    enabled = value;
                    cbPatSelect.Enabled = cbSetCurrent.Enabled = tbCurrent.Enabled =
                    cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = enabled;
                    if(enabled) Status = ChannelStatus.Inactive; else Status = ChannelStatus.Disabled;
                }
            }
        }
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
                    //byte pckNum;

                    ledBits = new Bits(value.Value);
                    Led.Color = Color.FromArgb(value[DioReg.LedR] ? 255 : 0, value[DioReg.LedG] ? 255 : 0, value[DioReg.LedB] ? 255 : 0);
                    Led.Blink(!value[DioReg.LedNBlink] ? 500 : 0);
                    Led.On = true;
                    Led.Refresh();
                    LANFunc.ChDio(Number, ledBits.ByteValue);
                    //pckNum = sendLANCmd(new QueryDG(addr: Number, led: ledBits.ByteValue));
                    //while(Response == null || Response.PacketNum != pckNum) Application.DoEvents();
                    //respQueue.Pull();
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

        #region Konstruktor
        /// <summary>
        /// Konstruktor kanálu s konkrétním číslem
        /// </summary>
        /// <param name="chnum">číslo přidělené kanálu</param>
        /// <param name="parent">odkaz na desku LAN</param>
        public Channel(byte chnum, Channels parent)
        {
            InitializeComponent();
            chNum = chnum;
            Channels = parent;
            chNumTxt = groupBox1.Text;
            remainTxt = lbRemain.Text;
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
            //reset();
            pbProgress.Value = 0;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                LANFunc.ChRst(Number);
                LANFunc.LanChOnOff(Number, false);
                components.Dispose();
                timer.Stop();
                timer.Dispose();
                if(chWorker.IsBusy) chWorker.CancelAsync();
                Thread.Sleep(1000);
                chWorker.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Utility kanálu
        private void led(DioReg clr, bool blink = false)
        {
            Bits lb = new Bits();

            lb[clr] = true;
            lb[DioReg.LedNBlink] = !blink;
            LEDBits = lb;
        }
        private void ledRed(bool blink = false) { led(DioReg.LedR, blink); }
        private void ledGreen(bool blink = false) { led(DioReg.LedG, blink); }
        private void ledBlue(bool blink = false) { led(DioReg.LedB, blink); }

        private void processMBStatus(ResponseDG resp)
        {
            if(Status != ChannelStatus.Disabled && Status != ChannelStatus.Inactive && Status != ChannelStatus.Error && Status != ChannelStatus.Inaccessible)
            {
                string msg = string.Empty;

                if(resp.InputR.Status[0]) msg = "Výstupní proud je z bezpečnostních důvodů omezen." + Environment.NewLine;
                //if(Status != ChannelStatus.Inactive && Status != ChannelStatus.Active && Status != ChannelStatus.Ready && !InOrder && resp.InputR.Status[1]) msg += "Příliš vysoká impedance zátěže." + Environment.NewLine;
                if(resp.InputR.Status[2]) msg += "DataFlash neobsahuje platná data." + Environment.NewLine;
                if(resp.InputR.Status[13]) msg += "Zápis do Holding registrů neobsahuje platná data." + Environment.NewLine;
                if(resp.InputR.Status[14])
                {
                    msg += "Vyvolán systémový watchdog." + Environment.NewLine;
                    LANFunc.ChRst(Number);
                    Status = ChannelStatus.Inactive;
                }
                if(resp.InputR.Status[15])
                {
                    msg += "Došlo k restartu desky.";
                    LANFunc.ChRst(Number);
                    Status = ChannelStatus.Inactive;
                }
                if(!string.IsNullOrEmpty(msg)) DialogBox.ShowError(msg, string.Format("Chybný stav kanálu {0}", Number));
            }
        }

        private void processResponse(ResponseDG resp)
        {
            if(resp != null)
            {
                chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
                if(resp.Command == QueryCmd.CmdRd)
                {
                    LEDBits = new Bits(resp.DioRD);
                    //Current = Math.Round(resp.InputR.Verified.AttenCoef * (maxmA / tbCurrent.Maximum), 2);
                }
                processMBStatus(resp);
                if(Status == ChannelStatus.Ready) // elektrody nejsou nasazeny ve stavu "připraven"
                {
                    ResponseDG res;

                    res = LANFunc.ChRd(Number);
                    if((resp.InputR.AIN2 - resp.InputR.AIN1) >= 48) Status = ChannelStatus.Active;
                }
                if((Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored) && resp.InputR.Status[1])
                    Status = ChannelStatus.HighResistance; // příliš vysoká impedance - navlhčit elektrody
            }
        }

        // kontroluje připojení pacienta na elektrody pomocí odporu < 10,5 kOhm
        // proud = 0,5 mA
        // (AIN2 - AIN1) < 52 (5,2 V)
        private void electrodesReady()
        {
            ResponseDG resp;

            resp = LANFunc.ChDAC(Number);
            resp = LANFunc.ChDOUT(Number, 2);
            Current = .5;
            resp = LANFunc.ChRd(Number);
            //DialogBox.ShowInfo(string.Format("Current = {0} mA, AtfC = {1}", Current, resp.InputR.Verified.AttenCoef), "Current");
            do
            {
                Application.DoEvents();
                resp = LANFunc.ChRd(Number);
            } while(Status == ChannelStatus.Active && (resp.InputR.AIN2 - resp.InputR.AIN1) >= 52);
        }

        // čeká na úpravu elektrod tak, aby odpor nebyl příliš vysoký
        // proud = 0,5 mA
        // Status(D1) = 1
        private void moistenElectrodes()
        {
            ResponseDG resp;

            Current = .5;
            resp = LANFunc.ChRd(Number);
            do
            {
                Application.DoEvents();
                resp = LANFunc.ChRd(Number);
                Thread.Sleep(100);
            } while(Status == ChannelStatus.HighResistance && resp.InputR.Status[1]);
        }
        #endregion

        #region Stavy kanálu
        //TODO: stavy kanálu zanést do Logu
        #region reset()
        /// <summary>
        /// Uvede kanál do stavu "nepřipojen/nepovolen". Z tohoto stavu do stavu "neaktivní" se kanál dostane fyzickým připojením přístroje MDM k počítači.
        /// </summary>
        private void reset()
        {
            ResponseDG resp = LANFunc.ChRd(Number);

            //chWorker.CancelAsync();
            chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
            if(!chWorker.IsBusy) chWorker.RunWorkerAsync();
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = tbCurrent.Enabled = false;
            timer.Stop();
            Patient = new SelectedPatient();
            lbPatName.Text = lbDiagnosis.Text = lbProcNum.Text = lbStatus.Text = string.Empty;
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Bold);
            Current = current = 0D;
            pbProgress.Value = Elapsed = 0;
            Elapsed = 0;
            ChannelEnabled = false;
            lbStatus.Text = Resources.chDisconected;
            lbStatus.ForeColor = SystemColors.InactiveCaptionText;
            lbStatus.BackColor = SystemColors.Window;
            Refresh();
            //resp = LANFunc.ChRst(Number);
            //LEDBits = new Bits(resp.DioRD);
        }
        #endregion

        #region deactivate()
        /// <summary>
        /// Uvede kanál do stavu "neaktivní". V tomto stavu je třeba vybrat pacienta, poté kanál přechází do stavu "aktivní".
        /// </summary>
        private void deactivate()
        {
            ResponseDG resp;

            reset();
            ledRed();
            resp = LANFunc.ChRd(Number);
            chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
            lbStatus.Text = Resources.chInactive;
            lbStatus.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.BackColor = SystemColors.Window;
            cbPatSelect.Enabled = true;
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Bold);
            Current = 0D;
            LANFunc.ChRst(Number);
            LANFunc.LanChOnOff(Number, false);
            LANFunc.LanChOnOff(Number);
            Refresh();
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
            lbPatName.ForeColor = lbDiagnosis.ForeColor = lbProcNum.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.Text = Resources.chActive;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
            Refresh();
            ledRed();
            electrodesReady();
            if(Status == ChannelStatus.Active) Status = ChannelStatus.Ready;
        }
        #endregion

        #region ready()
        /// <summary>
        /// Uvede kanál do stavu "připraven". V tomto stavu proběhne nastavení hodnoty proudu, poté kanál přechází do stavu "procedura probíhá" nebo  "aktivní".
        /// </summary>
        private void ready()
        {
            cbPatSelect.Enabled = true;
            cbSetCurrent.Enabled = false;
            cbStart.Enabled = true;
            lbStatus.Text = Resources.chReady;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            ledGreen(true);
            Refresh();
        }
        #endregion

        #region inProgress()
        /// <summary>
        /// Uvede kanál do stavu "procedura probíhá". Z tohoto stavu může kanál přejít do stavu "nastavení proudu", "příliš vysoký odpor", "pozastaven", "chyba" nebo "neaktivní".
        /// </summary>
        private void inProgress()
        {
            cbPatSelect.Enabled = cbStart.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = cbSetCurrent.Enabled = true;
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Regular);
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            lbPatName.ForeColor = lbDiagnosis.ForeColor = lbProcNum.ForeColor = lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            lbStatus.Text = Resources.chInProgress;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            tbCurrent.Enabled = false;
            timer.Start();
            ledGreen();
            Refresh();
        }
        #endregion

        #region setCurrent()
        /// <summary>
        /// Uvede kanál do stavu "nastav proud". V tomto stavu je třeba nastavit hodnotu proudu. Poté kanál přechází do stavu "procedura probíhá".
        /// </summary>
        private void setCurrent()
        {
            cbSetCurrent.Font = new Font(cbSetCurrent.Font, FontStyle.Bold);
            cbPause.Enabled = false;
            tbCurrent.Enabled = true;
            tbCurrent.Focus();
            lbCurrent.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.Text = Resources.chSetCurrent;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
        }
        #endregion

        #region highResistance()
        /// <summary>
        /// Uvede kanál do stavu "příliš vysoký odpor". V tomto stavu je třeba navlhčit elektrody na hlavě pacienta, aby se odpor snížil.
        /// Poté kanál přechází do stavu "procedura probíhá".
        /// </summary>
        private void highResistance()
        {
            paused();
            cbPause.Enabled = false;
            lbStatus.Text = Resources.chHighResistance;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
            current = Current;
            moistenElectrodes();
            Current = current;
            Status = oldStatus;
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
            ledGreen(true);
        }
        #endregion

        #region restored()
        /// <summary>
        /// Uvede kanál do stavu "obnoven". Z tohoto stavu může kanál přejít do stavu "pozastaven", "chyba" nebo "neaktivní".
        /// </summary>
        private void restored()
        {
            //cbPause.Enabled = cbStop.Enabled = cbSetCurrent.Enabled = true;
            //cbPatSelect.Enabled = cbStart.Enabled = false;
            inProgress();
            cbPause.Text = Resources.cbPauseText;
            cbPause.Image = Resources.pause;
            lbStatus.Text = Resources.chRestored;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            //ledGreen();
        }
        #endregion

        #region error()
        /// <summary>
        /// Uvede kanál do stavu "chyba". Z tohoto stavu může kanál přejít do stavu "obnoven" nebo "neaktivní".
        /// </summary>
        private void error()
        {
            cbPatSelect.Enabled = false;
            lbStatus.Text = Resources.chError;
            lbStatus.ForeColor = Color.Yellow;
            lbStatus.BackColor = Color.Red;
            ledRed(true);
        }
        #endregion

        #region inaccessible()
        /// <summary>
        /// Uvede kanál do stavu "nepřístupný". Z tohoto stavu se kanál nedostane do žádného jiného stavu.
        /// </summary>
        private void inaccessible()
        {
            ResponseDG resp = LANFunc.ChRd(Number);

            chWorker.CancelAsync();
            chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = false;
            timer.Stop();
            Patient = new SelectedPatient();
            lbPatName.Text = lbDiagnosis.Text = lbProcNum.Text = lbStatus.Text = string.Empty;
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            Current = 0D;
            pbProgress.Value = 0;
            Elapsed = 0;
            ChannelEnabled = false;
            lbStatus.Text = Resources.chInaccessible;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Red;
            LEDBits = new Bits();
            LANFunc.LanChOnOff(Number, false);
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
            if(elapsed > procDuration) Status = ChannelStatus.Inactive;
        }

        /// <summary>
        /// Periodická obsluha kanálu. Generuje periodicky čtecí požadavek a předá ho zpracující rutině.
        /// </summary>
        /// <param name="sender">odesílatel události</param>
        /// <param name="e">parametry události</param>
        private void chWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!chWorker.CancellationPending)
            {
                ResponseDG resp;

                resp = LANFunc.ChRd(Number);
                if(IsHandleCreated)
                {
                    Invoke(new MethodInvoker(delegate {
                        processResponse(resp);
                        //chMon.Record(resp.InputR.Status.Value, (byte)resp.InputR.Verified.AttenCoef, resp.InputR.Verified.DAC, resp.InputR.Verified.DOUT.ByteValue, aStatus[(int)Status]);
                    }));
                }
                Thread.Sleep(300);
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
                        word ppn = frm.PatientProcNum; // reference na PatientProcNum nastaví hodnotu PatientCycleNum

                        Patient = new SelectedPatient(frm.PatientID, frm.PatientName, frm.PatientDiagnosis, ppn, frm.PatientCycleNum);
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
                Current = Math.Round(tbCurrent.Value * (maxmA / tbCurrent.Maximum), 2);
                Status = oldStatus;
            }
            //else if(Status == ChannelStatus.SetCurrent) Status = ChannelStatus.InProgress;
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
                if(Status == ChannelStatus.HighResistance) Status = ChannelStatus.Inactive;
                else if(elapsed < procDuration && DialogBox.ShowYN(Resources.procAbortQ, Resources.procAbortH) == DialogResult.Yes) Status = ChannelStatus.Inactive;
                else timer.Start();
            }
            else Status = ChannelStatus.Inactive;
        }

        private void Channel_FontChanged(object sender, EventArgs e)
        {
            foreach(Control c in Controls) c.Font = Font;
        }

        //TODO: vyřešit označení vybraného kanálu
        private void Channel_Enter(object sender, EventArgs e)
        {
            //groupBox1.ForeColor = Color.Green;
        }

        private void Channel_Leave(object sender, EventArgs e)
        {
            //groupBox1.ForeColor = SystemColors.ControlText;
        }

        private void tbCurrent_ValueChanged(object sender, EventArgs e)
        {
            //if(Status == ChannelStatus.SetCurrent) Current = Math.Round(tbCurrent.Value * (maxmA / tbCurrent.Maximum), 2);
            //lbCurrent.Text = Current.ToString("F2");// + " mA";
            lbCurrent.Text = Math.Round(tbCurrent.Value * (maxmA / tbCurrent.Maximum), 2).ToString("F2");// + " mA";
        }
        #endregion
    }
}
