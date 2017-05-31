using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using word = System.UInt16;
using dword = System.UInt32;
using System.Threading;


using LANlib;

namespace LANDev
{
    public partial class Channel : UserControl
    {
        private const int TTICK = 10;
        private System.Threading.Timer worker;
        private bool resetSign = true;

        #region OnOff
        private Power onOff = Power.Off;
        public Power OnOff
        {
            get { return onOff; }
            set
            {
                if(onOff != value)
                {
                    onOff = value;
                    if(onOff == Power.On)
                    {
                        resetSign = false;
                        //DioBits = new Bits(Helper.DioLedGreen);
                        On();
                    }
                    else if(onOff == Power.Off)
                    {
                        resetSign = true;
                        DioBits = new Bits(Helper.DioLedOff);
                        Off();
                    }
                }
            }
        }
        #endregion

        #region TS
        private ulong ts = 0UL;
        public ulong TS
        {
            get { return (dword)(ts & 0x00000000FFFFFFFF); }
            internal set
            {
                ts = value;
                ModbusR.Input.TS = (dword)(ts & 0x00000000FFFFFFFF);
            }
        }
        #endregion

        #region Number
        private byte number = 1;
        [Category("Appearance")]
        [Description("Číslo kanálu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue((byte)1)]
        public byte Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                lbChNum.Text = string.Format("CH {0}", number);
            }
        }
        #endregion

        #region Mode
        private GenModes mode = GenModes.Quiet;
        /// <summary>
        /// Aktuální režim kanálu.
        /// </summary>
        [Category("Appearance")]
        [Description("Režim kanálu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(typeof(GenModes), "Quiet")]
        public GenModes Mode
        {
            get { return mode; }
            set
            {
                //if(mode != value)
                //{
                    mode = value;
                    switch(mode)
                    {
                        case GenModes.Quiet: quiet(); break;
                        case GenModes.NoSweep: noSweep(); break;
                        case GenModes.Sweep: sweep(); break;
                    }
                //}
            }
        }
        #endregion

        #region ModbusR
        private Modbus modbusR = new Modbus();
        /// <summary>
        /// Modbus registry
        /// </summary>
        public Modbus ModbusR
        {
            get { return modbusR ?? (modbusR = new Modbus()); }
        }
        #endregion

        #region DioBits
        private Bits dioBits = new Bits();
        public Bits DioBits
        {
            get { return dioBits; }
            set
            {
                dioBits = value;
                Helper.LEDBits(led, dioBits);
            }
        }
        #endregion

        public Channel()
        {
            InitializeComponent();
            mode = GenModes.Quiet;
            Off();
        }

        private static void workerTick(object status)
        {
            Channel ch = (Channel)status;

            ch.TS += TTICK;
        }

        private void writeDIO(QueryDG cmd, ResponseDG res)
        {
            Bits dioWr = new Bits(cmd.DioWR);

            //if(dioWr[DioReg.Reset]) reset();
            //else if(dioWr[DioReg.OnOff]) Off();
            //else if(DioBits[DioReg.OnOff]) On();
            DioBits = dioWr;
            //res.DioRD = dioWr.ByteValue;
        }

        internal ResponseDG processChannel(QueryDG cmd)
        {
            byte status = (byte)ErrStatus.OK;
            ResponseDG res = LAN.GetResponse(cmd, (ErrStatus)status, DioBits.ByteValue, ModbusR.Input);

            ModbusR.Input.TS = (dword)TS;
            if(cmd.Command == QueryCmd.CmdRd)
            {
                //res.DioRD = DioBits.ByteValue;
                //res.Status = (byte)ErrStatus.OK;
                ModbusR.Input.TsRdInpR = (dword)TS;
            }
            else if(cmd.Command == QueryCmd.CmdWr)
            {
                if(ModbusR.Verify(cmd.HoldingR))
                {
                    switch((GenModes)ModbusR.Input.Verified.Mode)
                    {
                        case GenModes.Quiet:
                            writeDIO(cmd, res);
                            quiet();
                            break;
                        case GenModes.Sweep: sweep(); break;
                        case GenModes.NoSweep: noSweep(); break;
                    }
                    ModbusR.Input.TsWrHoldR = (dword)TS;
                }
                else
                {
                    status = (byte)ErrStatus.NotVerified;
                    ModbusR.Input.Status[StatusBits.InvalidHold] = true;
                }
                //else res = new ResponseDG(cmd, ErrStatus.NotVerified);
            }
            else
            {
                status = (byte)ErrStatus.InvalidCmd;
            }
            //else res = new ResponseDG(cmd, ErrStatus.InvalidCmd, DioBits.ByteValue, ModbusR.Input);
            res.DioRD = DioBits.ByteValue;
            res.Status = status;
            res.InputR = ModbusR.Input;
            return res;
        }

        #region Režimy kanálu
        private void reset()
        {
            Off();
            On();
            resetSign = true;
        }

        public void Off()
        {
            //Bits db = new Bits(DioBits.ByteValue);

            ////db[DioReg.Reset] = false;
            ////db[DioReg.OnOff] = true;
            //DioBits = db;
            //Mode = GenModes.Quiet;
            modbusR = null;
            if(worker != null)
            {
                worker.Dispose();
                worker = null;
                ts = 0UL;
            }
        }

        public void On()
        {
            //Bits db = new Bits(DioBits.ByteValue);

            ////db[DioReg.Reset] = false;
            ////db[DioReg.OnOff] = false;
            //DioBits = db;
            //Mode = GenModes.Quiet;
            modbusR = new Modbus();
            worker = new System.Threading.Timer(workerTick, this, 0, TTICK);
        }

        private void quiet()
        {
            //DioBits = new Bits(Helper.DioLedWhite);
        }

        private void noSweep()
        {

        }

        private void sweep()
        {

        }
        #endregion
    }
}
