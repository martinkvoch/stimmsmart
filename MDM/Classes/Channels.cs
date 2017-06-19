using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using word = System.UInt16;
using dword = System.UInt32;

using MDM.Controls;
using MDM.Properties;
using LANlib;
using System;
using MDM.Data;
using MDM.DlgBox;
using System.Reflection;

namespace MDM.Classes
{
    /// <summary>
    /// Třída pro manipulaci se všemi kanály
    /// </summary>
    public class Channels
    {
        private static readonly byte noc = new Settings().NOC;
        private static dword pck = 0U;

        private List<Channel> channels = new List<Channel>();
        //private LANQueue<QueryDG> queue = new LANQueue<QueryDG>();
        //private BackgroundWorker qWorker = new BackgroundWorker();
        private static BackgroundWorker rWorker = new BackgroundWorker();

        #region bool Enabled
        //private bool enabled = true;
        /// <summary>
        /// Určuje, zda jsou kanály připojené/povolené
        /// </summary>
        //public bool Enabled { get; set; }
        //{
        //    get { return enabled; }
        //    set
        //    {
        //        if(enabled != value)
        //        {
        //            enabled = value;
        //            channels.ForEach(ch => { if(ch.Status != ChannelStatus.Inaccessible && ch.Status != ChannelStatus.Error) ch.ChannelEnabled = enabled; });
        //            //foreach(Channel ch in channels) ch.ChannelEnabled = enabled;
        //        }
        //    }
        //}
        #endregion

        #region ConnectedChannels
        /// <summary>
        /// Pole všech připojených kanálů
        /// </summary>
        public Channel[] ConnectedChannels { get { return channels.ToArray(); } }
        #endregion

        #region ChannelsOutOfOrder
        /// <summary>
        /// Indikuje, zda jsou všechny kanály mimo provoz
        /// </summary>
        public bool ChannelsOutOfOrder { get { return channels.All(ch => !ch.Enabled || !ch.InOrder); } }
        #endregion

        #region ChannelsInOrder
        /// <summary>
        /// Indikuje, zda je některý kanál v provozu
        /// </summary>
        public bool ChannelsInOrder { get { return channels.Any(ch => ch.InOrder); } }
        #endregion

        #region Konstruktor a obsluha kanálů
        public Channels(MDMPanel parent)
        {
            int screenWidth = (int)SystemParameters.PrimaryScreenWidth;

            LAN.SlaveIP = new Settings().LanIP;
            //qWorker.WorkerSupportsCancellation = true;
            rWorker.WorkerSupportsCancellation = true;
            //qWorker.DoWork += QWorker_DoWork;
            rWorker.DoWork += rWorker_DoWork;
            rWorker.RunWorkerAsync(this);
            for(byte ch = noc; ch > 0; ch--)
            {
                Channel chnl = new Channel(ch, this);

                chnl.Width = screenWidth / noc;
                chnl.Dock = DockStyle.Left;
                //chnl.Enabled = true;
                channels.Add(chnl);
                parent.Controls.Add(chnl);
                //chnl.Status = ChannelStatus.Disabled;
            }
            //Enabled = false;
        }

        private static void rWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!rWorker.CancellationPending)
            {
                LANFunc.LanRd();
                if(LAN.TimedOut) processTimedOut(e.Argument);
                else Thread.Sleep(1000);
            }
            e.Cancel = true;
        }

        private static void processTimedOut(object argument)
        {
            wTimeoutDlgBox dlg = new wTimeoutDlgBox();

            dlg.Show();
            do
            {
                System.Windows.Forms.Application.DoEvents();
            } while(LAN.TimedOut && dlg.DialogResult != DialogResult.Cancel);
            if(!dlg.IsDisposed) dlg.Dispose();
            if(!rWorker.IsBusy) rWorker.RunWorkerAsync();
            if(!LAN.TimedOut)
            {
                bool[] chErrors = new bool[noc];
                Channels channels = argument as Channels;
                wWaitBox box = wWaitBox.Show(Resources.testChannel);

                chErrors = Autotest();
                box.Dispose();
                for(byte b = 1; b <= noc; b++) channels.channels.ForEach(ch =>
                    ch.Invoke(new MethodInvoker(delegate { ch.Status = ChannelStatus.Inactive; }))
                    );
                channels.On(chErrors);
            }
        }

        ///// <summary>
        ///// Obsluhuje frontu požadavků na LAN
        ///// </summary>
        ///// <param name="sender">odesílatel události</param>
        ///// <param name="e">parametry události</param>
        //private void QWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while(true)
        //    {
        //        if(queue.Count > 0)
        //        {
        //            QueryDG cmd = queue.Pull();

        //            if(cmd != null)
        //            {
        //                ResponseDG resp = LAN.MasterCmd(cmd);

        //                if(resp != null)
        //                {
        //                    if(cmd.Address > 0) channels.First(ch => ch.Number == cmd.Address).Response = resp;
        //                    else LANFunc.Lan(resp.DioRD);
        //                }
        //            }
        //        }
        //        Thread.Sleep(1);
        //    }
        //}
        #endregion

        #region Autotest()
        public static bool[] Autotest()
        {
            const string methodFmt = "{0}.{1}()";
            ResponseDG resp;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string protocol = string.Empty;
            Bits led = new Bits();
            byte ledErr;
            bool[] chErrors = new bool[noc];
            Thread lanr = new Thread(() =>
            {
                while(true)
                {
                    LANFunc.LanRd();
                    for(byte b = 1; b <= noc; b++) LANFunc.ChRd(b);
                    Thread.Sleep(1000);
                }
            });

            LANFunc.LanRd();
            if(LAN.TimedOut)
            {
                protocol = Resources.testChError0;
                Log.ErrorToLog(methodName, Resources.testChErrors + ":" + Environment.NewLine + protocol);
                DialogBox.ShowError(protocol, Resources.testChErrors);
                for(byte b = 0; b < noc; b++) chErrors[b] = true;
                return chErrors;
            }

            lanr.IsBackground = true;
            lanr.SetApartmentState(ApartmentState.MTA);
            lanr.Start();
            Thread.Sleep(300);
            led[DioReg.LedR] = true;
            led[DioReg.LedNBlink] = true;
            ledErr = led.ByteValue;
            led = new Bits();
            LANFunc.Lan(0);
            // 0. postupné zapnutí všech kanálů
            for(byte b = 1; b <= noc; b++)
            {
                led[b - 1] = true;
                LANFunc.Lan(led.ByteValue);
                Thread.Sleep(100);
            }
            resp = LANFunc.LanRd();
            if(resp.DioRD != led.ByteValue)
            {
                Bits ledBits = new Bits(resp.DioRD);

                protocol += string.Format(Resources.testChError11 + Environment.NewLine, ledBits);
                for(byte b = 1; b <= noc; b++)
                    if(!ledBits[b - 1])
                    {
                        chErrors[b - 1] = true;
                        led = new Bits();
                        led[DioReg.LedR] = true;
                        led[DioReg.LedNBlink] = true;
                        LANFunc.ChDio(b, led.ByteValue);
                    }
            }
            // 1. test LED
            led = new Bits();
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            led[DioReg.LedR] = true;
            led[DioReg.LedNBlink] = true;
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError12 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            led = new Bits();
            led[DioReg.LedG] = true;
            led[DioReg.LedNBlink] = true;
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError13 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            led = new Bits();
            led[DioReg.LedB] = true;
            led[DioReg.LedNBlink] = true;
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError14 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 1; b <= noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            // 2. test přítomnosti napájecího napětí VN (+85V)
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN2 < 850)
                {
                    protocol += string.Format(Resources.testChError2 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            // 3. ověření, že na výstupu není nic připojeno
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.DAC = 32768;
                q.HoldingR.DOUT = new Bits(d1: true);
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(300);
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 16;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(300);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN1 > 10)
                {
                    protocol += string.Format(Resources.testChError31 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
                if(!resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError32 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            // 4. test zpětné proudové vazby + měření napětí na zátěži
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.DAC = 32768;
                q.HoldingR.DOUT = new Bits();
                q.HoldingR.AttenCoef = 0;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(400);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(Math.Abs(resp.InputR.AIN2 - resp.InputR.AIN1) > 2)
                {
                    protocol += string.Format(Resources.testChError4 + Environment.NewLine, b, resp.InputR.AIN2 - resp.InputR.AIN1, resp.InputR.Verified.AttenCoef);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            byte uk = 32, dif = 25, i = 0;
            byte[] aTolerance = { 2, 2, 3, 4, 5, 6, 7, 8 };
            do
            {
                for(byte b = 1; b <= noc; b++) LANFunc.ChAtCf(b, uk);
                Thread.Sleep(400);
                for(byte b = 1; b <= noc; b++)
                {
                    int res;

                    resp = LANFunc.ChRd(b);
                    res = resp.InputR.AIN2 - resp.InputR.AIN1 - dif;
                    if(Math.Abs(res) > aTolerance[i++])
                    {
                        protocol += string.Format(Resources.testChError4 + Environment.NewLine, b, res, resp.InputR.Verified.AttenCoef);
                        chErrors[b - 1] = true;
                        led = new Bits();
                        led[DioReg.LedR] = true;
                        led[DioReg.LedNBlink] = true;
                        LANFunc.ChDio(b, led.ByteValue);
                    }
                }
                if(uk == byte.MaxValue) uk = 0;
                if(uk > 0)
                {
                    if((uk + 32) > byte.MaxValue) uk = byte.MaxValue; else uk += 32;
                    dif += 25;
                }
            } while(uk == 0);
            // 5. test limitace výstupní proudové smyčky
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.DAC = 65535;
                q.HoldingR.DOUT = new Bits(d0: true);
                q.HoldingR.AttenCoef = 185;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.Status[0])
                {
                    protocol += string.Format(Resources.testChError5 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 200;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(!resp.InputR.Status[0])
                {
                    protocol += string.Format(Resources.testChError5 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            // 6. pokles hodnoty napětí zdroje při zatížení výstupu (do náhradní zátěže)
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN2 < 800)
                {
                    protocol += string.Format(Resources.testChError6 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            // 7. vyhodnocení příznaku "Status D1 - příliš vysoká impedance" při proudu 10 mA
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 220;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(200);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError7 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 240;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(200);
            for(byte b = 1; b <= noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(!resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError7 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    led = new Bits();
                    led[DioReg.LedR] = true;
                    led[DioReg.LedNBlink] = true;
                    LANFunc.ChDio(b, led.ByteValue);
                }
            }
            // vypnout kanály
            led = new Bits();
            for(byte b = 1; b <= noc; b++)
            {
                LANFunc.ChDio(b, led.ByteValue);
                LANFunc.ChRst(b);
            }
            Thread.Sleep(200);
            LANFunc.Lan(0);
            // pokud nastala chyba, zobrazíme ji
            if(protocol.Trim().Length > 0)
            {
                Log.ErrorToLog(methodName, Resources.testChErrors + ":" + Environment.NewLine + protocol);
                DialogBox.ShowError(protocol, Resources.testChErrors);
            }
            lanr.Abort();
            return chErrors;
        }
        #endregion

        #region Metody
        /// <summary>
        /// Postupné zapnutí všech kanálů
        /// </summary>
        public void On(bool[] chErrors)
        {
            if(ChannelsOutOfOrder)
            {
                Bits led = new Bits();

                //qWorker.RunWorkerAsync();
                //SendLANCmd(new QueryDG((byte)(pck++)));
                LANFunc.Lan(0); // prvním zápisem se vynuluje bit 14, 15 Statusu v Input registrech
                Thread.Sleep(100);
                channels.ForEach(ch => {
                    wWaitBox msg = wWaitBox.Show(string.Format(Resources.chOn, ch.Number));

                    try
                    {
                        msg.Show();
                        led[ch.Number - 1] = true;
                        if(chErrors[ch.Number - 1])
                        {
                            ch.Status = ChannelStatus.Inaccessible;
                            led[ch.Number - 1] = false;
                        }
                        else LANFunc.Lan(led.ByteValue);
                        //SendLANCmd(new QueryDG((byte)(pck++), ch.Number)); // prvním zápisem se vynuluje bit 14, 15 Statusu v Input registrech
                        if(!chErrors[ch.Number - 1])
                        {
                            LANFunc.ChRst(ch.Number); // prvním zápisem se vynuluje bit 14, 15 Statusu v Input registrech
                            ch.Status = ChannelStatus.Inactive;
                        }
                    }
                    finally
                    {
                        msg.Dispose();
                    }
                });
                //Enabled = true;
            }
        }

        /// <summary>
        /// Indikuje, zda je daný pacient již připojen k nějakému kanálu
        /// </summary>
        /// <param name="patId">identifikační číslo zkoumaného pacienta</param>
        /// <returns>Vrací logickou hodnotu true v případě, že pacient je již na některém kanále připojen.</returns>
        public bool PatientAttached(int patId)
        {
            return channels.Any(ch => ch.Patient.ID == patId);
        }

        ///// <summary>
        ///// Do fronty dotazů/požadavků na LAN vloží novou položku
        ///// </summary>
        ///// <param name="query">datová struktura dotazu (UDP paket)</param>
        //public void SendLANCmd(QueryDG query)
        //{
        //    queue.Push(query);
        //}
        #endregion
    }
}
