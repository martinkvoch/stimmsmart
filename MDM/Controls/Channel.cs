﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using word = System.UInt16;
using dword = System.UInt32;

using LANlib;
using MDM.DlgBox;
using MDM.Properties;
using MDM.Windows;
using MDM.Classes;

namespace MDM.Controls
{
    public enum ChannelStatus { Disabled, Inactive, Active, Ready, InProgress, SetCurrent, Paused, Restored, Error, Inaccessible }

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
        public const int NoSelPat = -1;
        private dword pck = 0U;
        private word procDuration = (word)new Settings().ProcDur, maxCurrent = 1000;
        private word elapsed = word.MaxValue;
        private string chNumTxt, remainTxt;
        private int current = 0;
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
        private LANQueue<ResponseDG> respQueue = new LANQueue<ResponseDG>();

        /// <summary>
        /// Začátek/konec response fronty daného kanálu
        /// </summary>
        public ResponseDG Response
        {
            get
            {
                ResponseDG res = null;

                if(respQueue.Count > 0) res = respQueue.Top();
                return res;
            }
            set
            {
                respQueue.Push(value);
            }
        }
        #endregion

        #region Status
        private ChannelStatus status = ChannelStatus.Disabled;
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
                    status = value;
                    switch(status)
                    {
                        case ChannelStatus.Disabled: reset(); break;
                        case ChannelStatus.Inactive: deactivate(); break;
                        case ChannelStatus.Active: activate(); break;
                        case ChannelStatus.SetCurrent: setCurrent(); break;
                        case ChannelStatus.Ready: ready(); break;
                        case ChannelStatus.InProgress: inProgress(); break;
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
        /// <summary>
        /// Aktuálně nastavená hodnota proudu. Pro všechny stavy s výjimkou InProgress a Restored by měla být nulová.
        /// </summary>
        public float Current
        {
            get { return tbCurrent.Value / 100F; }
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
                if(elapsed != value)
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
            get { return status == ChannelStatus.InProgress || status == ChannelStatus.Restored || status == ChannelStatus.Paused; }
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
            reset();
            pbProgress.Value = 0;
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

        /// <summary>
        /// Do fronty dotazů/požadavků na LAN vloží novou položku
        /// </summary>
        /// <param name="query">datová struktura dotazu (UDP paket)</param>
        private byte sendLANCmd(QueryDG query)
        {
            if(query.Command == QueryCmd.CmdWr && query.HoldingR == null)
            {
                ResponseDG resp;
                QueryDG q = new QueryDG(addr: Number, cmd: QueryCmd.CmdRd);

                resp = waitFor((byte)pck);
                if(resp != null && resp.InputR != null && resp.InputR.Holding != null) query.HoldingR = resp.InputR.Holding;
            }
            query.PacketNum = (byte)pck++;
            query.Address = Number;
            Channels.SendLANCmd(query);
            return query.PacketNum;
        }

        private ResponseDG waitFor(byte pckNum)
        {
            ResponseDG res;

            while(Response == null || Response.PacketNum != pckNum) Application.DoEvents();
            res = respQueue.Pull();
            return res;
        }

        private void processResponse(ResponseDG resp)
        {
            if(resp != null)
            {
                if(resp.Command == QueryCmd.CmdRd)
                {
                    //LEDBits = new Bits(resp.DioRD);
                }
            }
        }

        public bool IsConnected() // test konektivity
        {
            bool res = true;

            //Thread.Sleep(500);
            return res;
        }

        // kontroluje připojení pacienta na elektrody pomocí odporu < 10,5 kOhm
        // proud = 0,5 mA
        // (AIN2 - AIN1) < 52
        private void electrodesReady()
        {
            ResponseDG resp;
            //byte pckNum;
            //QueryDG q = new QueryDG(modbus: new ModbusHolding(dac: 32768, dout: 2));

            LANFunc.ChDAC(Number);
            LANFunc.ChDOUT(Number, 2);
            Thread.Sleep(100);
            LANFunc.ChAcf(Number, 32);
            Thread.Sleep(100);
            //pckNum = sendLANCmd(q);
            //Thread.Sleep(50);
            //resp = waitFor(pckNum);
            //q.HoldingR.AttenCoef = 32;
            //pckNum = sendLANCmd(q);
            //Thread.Sleep(50);
            //resp = waitFor(pckNum);
            do
            {
                resp = LANFunc.ChRd(Number);
                //q = new QueryDG(cmd: QueryCmd.CmdRd);
                //pckNum = sendLANCmd(q);
                //resp = waitFor(pckNum);
            } while((resp.InputR.AIN2 - resp.InputR.AIN1) >= 52);
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
            chWorker.CancelAsync();
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = false;
            timer.Stop();
            Patient = new SelectedPatient();
            lbPatName.Text = lbDiagnosis.Text = lbProcNum.Text = lbStatus.Text = string.Empty;
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            tbCurrent.Value = 0;
            tbCurrent.Maximum = maxCurrent;
            tbCurrent.TickFrequency = maxCurrent / 10;
            tbCurrent_ValueChanged(null, null);
            pbProgress.Value = 0;
            Elapsed = 0;
            ChannelEnabled = false;
            lbStatus.Text = Resources.chDisconected;
            lbStatus.ForeColor = SystemColors.InactiveCaptionText;
            lbStatus.BackColor = SystemColors.Window;

            ResponseDG resp = LANFunc.ChRst(Number);

            LEDBits = new Bits(resp.DioRD);
        }
        #endregion

        #region deactivate()
        /// <summary>
        /// Uvede kanál do stavu "neaktivní". V tomto stavu je třeba vybrat pacienta, poté kanál přechází do stavu "aktivní".
        /// </summary>
        private void deactivate()
        {
            if(!chWorker.IsBusy) chWorker.RunWorkerAsync();
            timer.Stop();
            pbProgress.Value = tbCurrent.Value = current = 0;
            if(IsConnected())
            {
                cbPatSelect.Enabled = true;
                cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = false;
                lbStatus.Text = Resources.chInactive;
                lbStatus.ForeColor = SystemColors.ActiveCaptionText;
                lbStatus.BackColor = SystemColors.Window;
            }

            ResponseDG resp = LANFunc.ChRst(Number);

            LEDBits = new Bits(resp.DioRD);
            //Bits lb = new Bits();
            //lb[DioReg.LedR] = true;
            //lb[DioReg.LedNBlink] = true;
            //LEDBits = lb;
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
            tbCurrent.Value = current = 0;
            lbStatus.Text = Resources.chActive;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
            Refresh();
            electrodesReady();
            Status = ChannelStatus.Ready;
            //Status = ChannelStatus.SetCurrent;
            //ledGreen();
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
        }
        #endregion

        #region inProgress()
        /// <summary>
        /// Uvede kanál do stavu "procedura probíhá". Z tohoto stavu může kanál přejít do stavu "pozastaven", "chyba" nebo "neaktivní".
        /// </summary>
        private void inProgress()
        {
            cbPatSelect.Enabled = false;
            cbStart.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = true;
            lbPatName.ForeColor = lbDiagnosis.ForeColor = lbProcNum.ForeColor = SystemColors.InactiveCaptionText;
            lbStatus.Text = Resources.chInProgress;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            timer.Start();
            ledGreen();
        }
        #endregion

        #region setCurrent()
        /// <summary>
        /// Uvede kanál do stavu "nastav proud". V tomto stavu je třeba nastavit hodnotu proudu. Poté kanál přechází do stavu "připraven".
        /// </summary>
        private void setCurrent()
        {
            cbPatSelect.Enabled = true;
            cbSetCurrent.Enabled = true;
            cbStart.Enabled = false;
            tbCurrent.Focus();
            lbCurrent.ForeColor = SystemColors.ActiveCaptionText;
            lbStatus.Text = Resources.chSetCurrent;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.OrangeRed;
        }
        #endregion

        #region paused()
        //TODO: pozastavení a obnovení také na mezerník
        /// <summary>
        /// Uvede kanál do stavu "pozastaven". Z tohoto stavu může kanál přejít do stavu "neaktivní" nebo "obnoven".
        /// </summary>
        private void paused()
        {
            cbPatSelect.Enabled = false;
            cbStart.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = true;
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
            cbPatSelect.Enabled = false;
            cbStart.Enabled = false;
            cbPause.Enabled = cbStop.Enabled = true;
            lbStatus.Text = Resources.chRestored;
            lbStatus.ForeColor = Color.White;
            lbStatus.BackColor = Color.Green;
            ledGreen();
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
            chWorker.CancelAsync();
            cbSetCurrent.Enabled = cbStart.Enabled = cbPause.Enabled = cbStop.Enabled = false;
            timer.Stop();
            Patient = new SelectedPatient();
            lbPatName.Text = lbDiagnosis.Text = lbProcNum.Text = lbStatus.Text = string.Empty;
            lbCurrent.ForeColor = SystemColors.InactiveCaptionText;
            tbCurrent.Value = 0;
            tbCurrent.Maximum = maxCurrent;
            tbCurrent.TickFrequency = maxCurrent / 10;
            tbCurrent_ValueChanged(null, null);
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
            if(Status == ChannelStatus.InProgress || Status == ChannelStatus.Restored) Elapsed++;
            if(elapsed > procDuration)
            {
                Elapsed = 0;
                Status = ChannelStatus.Inactive;
            }
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
                //byte pckNum;
                //QueryDG q = new QueryDG(cmd: QueryCmd.CmdRd);

                //pckNum = sendLANCmd(q);
                //resp = waitFor(pckNum);
                resp = LANFunc.ChRd(Number);
                processResponse(resp);
                Thread.Sleep(1000);
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
            if(tbCurrent.Value == 0) Status = ChannelStatus.Active;
            else
            {
                //sendLANCmd(new QueryDG(0, Number, QueryCmd.CmdWr, 0, new ModbusHolding()));
                //ResponseDG resp = Response;
                current = tbCurrent.Value;
                Status = ChannelStatus.Ready;
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
                tbCurrent.Value = 0;
                Status = ChannelStatus.Paused;
                timer.Stop();
            }
            else if(Status == ChannelStatus.Paused)
            {
                tbCurrent.Value = current;
                Status = ChannelStatus.Restored;
                timer.Start();
            }
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

        //TODO: na tlačítko STOP dát ukončení procedury i v přípravné fázi
        private void cbStop_Click(object sender, EventArgs e)
        {
            if(InOrder)
            {
                timer.Stop();
                if(elapsed < procDuration && DialogBox.ShowYN(Resources.procAbortQ, Resources.procAbortH) == DialogResult.Yes) Status = ChannelStatus.Inactive;
                else timer.Start();
            }
            else Status = ChannelStatus.Inactive;
        }

        private void tbCurrent_ValueChanged(object sender, EventArgs e)
        {
            lbCurrent.Text = ((float)tbCurrent.Value / 100F).ToString("F2");// + " mA";
        }
        #endregion
    }
}
