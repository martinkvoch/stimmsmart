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
        private LANQueue<QueryDG> queue = new LANQueue<QueryDG>();
        private BackgroundWorker qWorker = new BackgroundWorker();

        #region bool Enabled
        private bool enabled = true;
        /// <summary>
        /// Určuje, zda jsou kanály připojené/povolené
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if(enabled != value)
                {
                    enabled = value;
                    channels.ForEach(ch => ch.ChannelEnabled = enabled);
                    //foreach(Channel ch in channels) ch.ChannelEnabled = enabled;
                }
            }
        }
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
            qWorker.DoWork += QWorker_DoWork;
            for(byte ch = noc; ch > 0; ch--)
            {
                Channel chnl = new Channel(ch);

                chnl.Channels = this;
                chnl.Width = screenWidth / noc;
                chnl.Dock = DockStyle.Left;
                chnl.Enabled = chnl.IsConnected();
                channels.Add(chnl);
                parent.Controls.Add(chnl);
            }
            Enabled = false;
        }

        /// <summary>
        /// Obsluhuje frontu požadavků na LAN
        /// </summary>
        /// <param name="sender">odesílatel události</param>
        /// <param name="e">parametry události</param>
        private void QWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                if(queue.Count > 0)
                {
                    QueryDG cmd = queue.Pull();

                    if(cmd != null)
                    {
                        ResponseDG resp = LAN.MasterCmd(cmd);

                        if(resp != null)
                        {
                            if(cmd.Address > 0) channels.First(ch => ch.Number == cmd.Address).Response = resp;
                            else LANFunc.Lan(resp.DioRD);
                        }
                    }
                }
                Thread.Sleep(1);
            } while(true);
        }
        #endregion

        #region Autotest()
        public static bool[] Autotest()
        {
            ResponseDG resp;
            string protocol = string.Empty;
            Bits led = new Bits();
            bool[] chErrors = new bool[noc];

            LANFunc.Lan(0);
            // 0. postupné zapnutí všech kanálů
            for(int i = 0; i < noc; i++)
            {
                led[i] = true;
                LANFunc.Lan(led.ByteValue);
                Thread.Sleep(100);
            }
            resp = LANFunc.ChRd(0);
            if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError12 + Environment.NewLine, led);
            // 1. test LED
            led = new Bits(d0: true);
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError12 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            led = new Bits(d1: true);
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError13 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            led = new Bits(d2: true);
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.DioRD != led.ByteValue) protocol += string.Format(Resources.testChError14 + Environment.NewLine, b);
            }
            led = new Bits();
            for(byte b = 0; b < noc; b++) LANFunc.ChDio(b, led.ByteValue);
            Thread.Sleep(1000);
            // 2. test přítomnosti napájecího napětí VN (+85V)
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN2 < 850)
                {
                    protocol += string.Format(Resources.testChError2 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            // 3. ověření, že na výstupu není nic připojeno
            for(byte b = 0; b < noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.DAC = 32768;
                q.HoldingR.DOUT = new Bits(d1: true);
                q.HoldingR.AttenCoef = 16;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(300);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN1 > 10)
                {
                    protocol += string.Format(Resources.testChError31 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
                if(resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError32 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            // 4. test zpětné proudové vazby + měření napětí na zátěži
            for(byte b = 0; b < noc; b++)
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
            Thread.Sleep(300);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(Math.Abs(resp.InputR.AIN2 - resp.InputR.AIN1) > 2)
                {
                    protocol += string.Format(Resources.testChError4 + Environment.NewLine, b, resp.InputR.AIN2 - resp.InputR.AIN1, resp.InputR.Holding.AttenCoef);
                    chErrors[b] = true;
                }
            }
            byte uk = 32;
            do
            {
                for(byte b = 0; b < noc; b++) LANFunc.ChAcf(b, uk);
                Thread.Sleep(300);
                for(byte b = 0; b < noc; b++)
                {
                    resp = LANFunc.ChRd(b);
                    if(Math.Abs(resp.InputR.AIN2 - resp.InputR.AIN1) > 2)
                    {
                        protocol += string.Format(Resources.testChError4 + Environment.NewLine, b, resp.InputR.AIN2 - resp.InputR.AIN1, resp.InputR.Holding.AttenCoef);
                        chErrors[b] = true;
                    }
                }
                if(uk == byte.MaxValue) uk = 0;
                if((uk + 32) > byte.MaxValue) uk = byte.MaxValue; else uk += 32;
            } while(uk == 0);
            // 5. test limitace výstupní proudové smyčky
            for(byte b = 0; b < noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.DAC = 65535;
                q.HoldingR.DOUT = new Bits(1);
                q.HoldingR.AttenCoef = 185;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.Status[0])
                {
                    protocol += string.Format(Resources.testChError5 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            for(byte b = 0; b < noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 200;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(!resp.InputR.Status[0])
                {
                    protocol += string.Format(Resources.testChError5 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            // 6. pokles hodnoty napětí zdroje při zatížení výstupu (do náhradní zátěže)
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.AIN2 < 800)
                {
                    protocol += string.Format(Resources.testChError6 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            // 7. vyhodnocení příznaku "Status D1 - příliš vysoká impedance" při proudu 10 mA
            for(byte b = 0; b < noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 220;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(!resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError7 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            for(byte b = 0; b < noc; b++)
            {
                QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), b);
                ResponseDG res = LANFunc.ChRd(b);

                q.DioWR = res.DioRD;
                q.HoldingR = res.InputR.Verified;
                q.HoldingR.AttenCoef = 240;
                res = LAN.MasterCmd(q);
            }
            Thread.Sleep(100);
            for(byte b = 0; b < noc; b++)
            {
                resp = LANFunc.ChRd(b);
                if(resp.InputR.Status[1])
                {
                    protocol += string.Format(Resources.testChError7 + Environment.NewLine, b);
                    chErrors[b] = true;
                }
            }
            LANFunc.Lan(0); // vypnout
            if(protocol.Trim().Length > 0)
            {
                Log.InfoToLog(Resources.testChErrors + ":" + Environment.NewLine + protocol);
                DialogBox.ShowError(protocol, Resources.testChErrors);
            }
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

                SendLANCmd(new QueryDG((byte)(pck++)));
                Thread.Sleep(100);
                channels.ForEach(ch => {
                    led[ch.Number - 1] = true;
                    SendLANCmd(new QueryDG((byte)(pck++), led: led.ByteValue));
                    if(chErrors[ch.Number]) ch.Status = ChannelStatus.Error;
                    Thread.Sleep(100);
                });
                Enabled = true;
                Reset();
                qWorker.RunWorkerAsync();
            }
        }

        public void Reset(byte chNum = 0)
        {
            if(chNum == 0) channels.ForEach(ch => ch.Status = ChannelStatus.Disabled);
            else if(chNum > 0 && chNum <= noc) channels.First(ch => ch.Number == chNum).Status = ChannelStatus.Disabled;
        }

        public void Deactivation(byte chNum = 0)
        {
            if(chNum == 0) channels.ForEach(ch => ch.Status = ChannelStatus.Inactive);
            else if(chNum > 0 && chNum <= noc) channels.First(ch => ch.Number == chNum).Status = ChannelStatus.Inactive;
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

        /// <summary>
        /// Do fronty dotazů/požadavků na LAN vloží novou položku
        /// </summary>
        /// <param name="query">datová struktura dotazu (UDP paket)</param>
        public void SendLANCmd(QueryDG query)
        {
            queue.Push(query);
        }
        #endregion
    }
}
