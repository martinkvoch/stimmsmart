using System.Windows.Controls;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucDisp3D.xaml
    /// </summary>
    public partial class ucDisp3D : UserControl
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
                    on = D100.On = D10.On = D1.On = value;
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
                    _value %= 1000;
                    D100.Value = (byte)(_value / 100);
                    D10.Value = (byte)((_value % 100) / 10);
                    D1.Value = (byte)(_value % 10);
                }
            }
        }
        #endregion

        public ucDisp3D()
        {
            InitializeComponent();
            Value = 0;
            On = false;
        }
    }
}
