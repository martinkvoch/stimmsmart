using System.Windows.Forms;
using word = System.UInt16;

namespace MDM.Controls
{
    public struct TValues
    {
        internal word Status;
        internal byte AttenCoef;
        internal word DAC;
        internal byte DOUT;
        internal string ChStatus;

        public TValues(word status, byte attenCoef, word dac, byte dout, string chStatus)
        {
            Status = status;
            AttenCoef = attenCoef;
            DAC = dac;
            DOUT = dout;
            ChStatus = chStatus;
        }
    }

    public partial class ChannelMonitor : UserControl
    {
        const int maxChangeCount = 3;
        private TValues _values = new TValues();
        private int changeCount = 0;

        private TValues values
        {
            get { return _values; }
            set
            {
                _values = value;
                lbStatus.Text = _values.Status.ToString("X4");
                lbAtCf.Text = _values.AttenCoef.ToString("D3");
                lbDAC.Text = _values.DAC.ToString("X4");
                lbDOUT.Text = _values.DOUT.ToString("D1");
                lbChStatus.Text = _values.ChStatus.ToUpper().Substring(0, 2);
                if(changeCount++ > maxChangeCount)
                {
                    lbTick.Text = lbTick.Text.Equals(" ") ? "●" : " ";
                    changeCount = 0;
                }
            }
        }

        public ChannelMonitor()
        {
            InitializeComponent();
        }

        public void Record(word status, byte attenCoef, word dac, byte dout, string chStatus)
        {
            values = new TValues(status, attenCoef, dac, dout, chStatus);
        }
    }
}
