using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LANDev.Properties;
using LANlib;

namespace LANDev
{
    using word = UInt16;
    using dword = UInt32;

    public partial class wFrontDesk : Form
    {
        /// <summary>
        /// Počet kanálů v MDM
        /// </summary>
        public const byte NOC = 6;

        #region OnOff
        private Power onOff = Power.Off;

        private void _onOff()
        {
            cbOnOff.BackgroundImage = onOff == Power.On ? Resources.switch_On : Resources.switch_Off;
            led.On = onOff == Power.On;
            LAN.CallBack = processCmd;
            if(onOff == Power.On)
            {
                setChMode(GenModes.Quiet);
                LAN.SlaveAns();
            }
            else
            {
                Bits dio = new Bits(Helper.DioLedOff);

                //dio[DioReg.OnOff] = true;
                //setChDio(dio.ByteValue);
                setChOff();
            }
            //else LAN.MasterCmd("Off");
            Refresh();
        }

        [Category("Appearance")]
        [Description("Indikace zapnutí přístroje.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Power OnOff
        {
            get { return onOff; }
            set
            {
                if(onOff != value)
                {
                    onOff = value;
                    _onOff();
                }
            }
        }
        #endregion

        #region Channels
        public Channel[] Channels
        {
            get { return panel2.Controls.OfType<Control>().Where(c => c is Channel).Cast<Channel>().ToArray(); }
        }

        private void setChMode(GenModes mode)
        {
            foreach(Channel ch in Channels) ch.Mode = mode;
        }

        private void setChOff()
        {
            foreach(Channel ch in Channels) ch.OnOff = Power.Off;
        }

        private Channel getChannel(byte n)
        {
            return Channels.FirstOrDefault(c => c.Number == n);
        }
        #endregion

        #region GenRBits
        private Bits genRBits = new Bits();
        private Bits GenRBits
        {
            get { return genRBits; }
            set
            {
                if(genRBits != value)
                {
                    //ledBits = new Bits((byte)(value.Value & Helper.BDioLedMask));
                    //Helper.GenRBits(led, ledBits);
                    genRBits = new Bits(value.ByteValue);
                    Helper.LEDBits(led, new Bits(onOff == Power.Off ? Helper.DioLedOff : Helper.DioLedGreen));
                    foreach(Channel ch in Channels) ch.OnOff = genRBits[ch.Number] ? Power.On : Power.Off;
                }
            }
        }
        #endregion

        public wFrontDesk()
        {
            InitializeComponent();
            onOff = Power.Off;
            _onOff();
        }

        private ResponseDG processCmd(QueryDG cmd)
        {
            ResponseDG res = LAN.GetResponse(cmd);

            if((byte)cmd.Command != (byte)QueryCmd.CmdWr || (byte)cmd.Command != (byte)QueryCmd.CmdRd)
            {
                if(cmd.Address == 0) res = processLAN(cmd);
                else if(cmd.Address > 0 && cmd.Address <= NOC)
                {
                    Channel ch = getChannel(cmd.Address);

                    if(ch != null) res = ch.processChannel(cmd);
                    else res.Status = (byte)ErrStatus.InvalidAddr;
                }
                else res.Status = (byte)ErrStatus.InvalidAddr;
            }
            else res.Status = (byte)ErrStatus.InvalidCmd;
            return res;
            //if(cmd.Equals("Off", StringComparison.OrdinalIgnoreCase)) Invoke(new MethodInvoker(delegate { onOff = Power.Off; _onOff(); }));
            //else if(cmd.Equals("On", StringComparison.OrdinalIgnoreCase)) Invoke(new MethodInvoker(delegate { onOff = Power.On; _onOff(); }));
            //else if(cmd.Equals("error", StringComparison.OrdinalIgnoreCase)) Invoke(new MethodInvoker(delegate { setStatus(ChannelStatus.Error); }));
            //return cmd;
        }

        private ResponseDG processLAN(QueryDG cmd)
        {
            ResponseDG res = LAN.GetResponse(cmd);

            if(cmd.Command == QueryCmd.CmdWr) GenRBits = new Bits(cmd.DioWR);
            res.DioRD = GenRBits.ByteValue;
            res.Status = (byte)ErrStatus.OK;
            return res;
        }

        private void cbClose_Click(object sender, EventArgs e)
        {
            if(onOff == Power.On)
            {
                OnOff = Power.Off;
                Thread.Sleep(1000);
            }
            Close();
        }

        private void cbOnOff_Click(object sender, EventArgs e)
        {
            if(onOff == Power.On) OnOff = Power.Off; else OnOff = Power.On;
        }
    }
}
