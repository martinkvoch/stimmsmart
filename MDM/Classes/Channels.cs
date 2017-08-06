﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using qword = System.UInt64;

using LANlib;
using MDM.Controls;
using MDM.Data;
using MDM.DlgBox;
using MDM.Properties;
using System.Drawing;

namespace MDM.Classes
{
    /// <summary>
    /// Třída pro manipulaci se všemi kanály
    /// </summary>
    public class Channels : Component
    {
        private static readonly byte noc = new Settings().NOC;
        internal static readonly Image[]
            ChannelsReadyImgs = new Image[] { Resources.cervena_ready, Resources.modra_ready, Resources.cerna_ready, Resources.zluta_ready, Resources.bila_ready, Resources.zelena_ready },
            ChannelsErrorImgs = new Image[] { Resources.cervena_error, Resources.modra_error, Resources.cerna_error, Resources.zluta_error, Resources.bila_error, Resources.zelena_error };
        private List<Channel> channels = new List<Channel>();
#if LAN
        private static qword pck = 0U;
        private static BackgroundWorker rWorker;
#endif

#region ChannelsOutOfOrder
        /// <summary>
        /// Indikuje, zda jsou všechny kanály mimo provoz
        /// </summary>
        public bool ChannelsOutOfOrder { get { return channels.All(ch => !ch.Enabled || !ch.InOrder); } }
#endregion

#region Konstruktor, destruktor a obsluha kanálů
        public Channels(MDMPanel parent)
        {
            int screenWidth = (int)SystemParameters.PrimaryScreenWidth;

#if LAN
            LAN.SlaveIP = new Settings().LanIP;
#endif
            for(byte ch = noc; ch > 0; ch--)
            {
                Channel chnl = new Channel(ch, this);

                chnl.Width = screenWidth / noc;
                chnl.Dock = DockStyle.Left;
                channels.Add(chnl);
                parent.Controls.Add(chnl);
            }
#if LAN
            rWorker = new BackgroundWorker();
            rWorker.WorkerSupportsCancellation = true;
            rWorker.DoWork += rWorker_DoWork;
            rWorker.RunWorkerAsync(this);
#endif
        }

        internal void DisposeMain()
        {
#if LAN
            rWorker.CancelAsync();
            while(rWorker.IsBusy) System.Windows.Forms.Application.DoEvents();
            rWorker.Dispose();
            rWorker = null;
#endif
            foreach(Channel ch in channels) ch.DisposeMain();
        }

#if LAN
        private static bool lanTimedOut = false;
        private static wTimeoutDlgBox dlg;// = new wTimeoutDlgBox();

        private static void rWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Channels channels = e.Argument as Channels;

            while(!rWorker.CancellationPending)
            {
                LAN.MasterCmd(new QueryDG(cmd: QueryCmd.CmdRd, modbus: new ModbusHolding()));
                //LANFunc.LanRd();
                if(!lanTimedOut)
                {
                    if(LAN.TimedOut)
                    {
                        dlg = new wTimeoutDlgBox();
                        dlg.Show();
                        dlg.Focus();
                        dlg.Refresh();
                        lanTimedOut = true;
                        channels.channels.ForEach(ch => ch.Invoke(new MethodInvoker(delegate { ch.Status = ChannelStatus.Disconnected; })));
                    }
                }
                else
                {
                    if(dlg.Result == DialogResult.OK && !dlg.IsDisposed)
                    {
                        dlg.Dispose();
                        lanTimedOut = false;
                    }
                    else if(!LAN.TimedOut)
                    {
                        wWaitBox box;
                        bool[] chErrors = new bool[noc];

                        if(!dlg.IsDisposed) dlg.Dispose();
                        box = wWaitBox.Show(Resources.testChannel);
                        chErrors = Autotest();
                        channels.channels.ForEach(ch => ch.Invoke(new MethodInvoker(delegate { ch.Status = ChannelStatus.Inactive; })));
                        box.Dispose();
                        lanTimedOut = false;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                Thread.Sleep(200);
            }
            if(!dlg.IsDisposed) dlg.Dispose();
            e.Cancel = true;
        }
#endif
#endregion

#region Autotest()
        private static void ledRed(byte chNum)
        {
            Bits led = new Bits();

            led[DioReg.LedR] = true;
            led[DioReg.LedNBlink] = true;
            LANFunc.ChDio(chNum, led.ByteValue);
        }

        public static bool[] Autotest()
        {
            bool[] chErrors = new bool[noc];
#if LAN
            const string methodFmt = "{0}.{1}()";
            ResponseDG resp;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string protocol = string.Empty;
            Bits led = new Bits();
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
            led = new Bits();
            LANFunc.Lan(0);
            // 0. postupné zapnutí všech kanálů
            for(byte b = 1; b <= noc; b++)
            {
                led[b - 1] = true;
                LANFunc.Lan(led.ByteValue);
                Thread.Sleep(300);
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
                        ledRed(b);
                        //led = new Bits();
                        //led[DioReg.LedR] = true;
                        //led[DioReg.LedNBlink] = true;
                        //LANFunc.ChDio(b, led.ByteValue);
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
                    ledRed(b);
                }
            }
            // 3. ověření, že na výstupu není nic připojeno
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
                }
                else if(!resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError32 + Environment.NewLine, b);
                    chErrors[b - 1] = true;
                    ledRed(b);
                }
            }
            // 4. test zpětné proudové vazby + měření napětí na zátěži
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
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
                        ledRed(b);
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
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
                }
            }
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
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
                    ledRed(b);
                }
            }
            // 7. vyhodnocení příznaku "Status D1 - příliš vysoká impedance" při proudu 10 mA
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
                }
            }
            for(byte b = 1; b <= noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++), b);
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
                    ledRed(b);
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
#endif
            return chErrors;
        }
#endregion

#region Metody
        /// <summary>
        /// Postupné zapnutí všech kanálů
        /// </summary>
        public void On(bool[] chErrors)
        {
            if(!Program.IsDesignMode)
            {
#if LAN
                if(ChannelsOutOfOrder)
                {
                    LANFunc.Lan(0); // prvním zápisem se vynuluje bit 14, 15 Statusu v Input registrech
                    Thread.Sleep(300);
                    channels.ForEach(ch => {
                        wWaitBox msg = wWaitBox.Show(string.Format(Resources.chOn, ch.Number));

                        try
                        {
                            msg.Show();
                            if(chErrors[ch.Number - 1]) ch.Status = ChannelStatus.Inaccessible;
                            else
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
#else
            channels.ForEach(ch => {
                wWaitBox msg = wWaitBox.Show(string.Format(Resources.chOn, ch.Number));

                if(!chErrors[ch.Number - 1]) ch.Status = ChannelStatus.Inactive;
                Thread.Sleep(200);
                msg.Dispose();
            });
#endif
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
#endregion
    }
}
