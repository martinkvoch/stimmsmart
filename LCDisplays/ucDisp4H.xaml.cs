using System.Windows.Controls;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucDisp4H.xaml
    /// </summary>
    public partial class ucDisp4H : UserControl
    {
        #region On
        private bool on = false;
        public bool On
        {
            get { return on; }
            set
            {
                if(on != value)
                {
                    on = H1000.On = H100.On = H10.On = H1.On = value;
                    if(!on) Value = 0;
                }
            }
        }
        #endregion

        #region Value
        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                if(_value != value && On)
                {
                    _value = value;
                    if(value < 0) _value = -value;
                    _value %= 0x10000;
                    H1000.Value = (byte)(_value >> 12);
                    H100.Value = (byte)((_value >> 8) % 16);
                    H10.Value = (byte)((_value >> 4) % 16);
                    H1.Value = (byte)(_value % 16);
                }
            }
        }
        #endregion

        public ucDisp4H()
        {
            InitializeComponent();
            Value = 0;
            On = false;
        }
    }
}
