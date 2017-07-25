using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucDisp4D.xaml
    /// </summary>
    public partial class ucDisp4D : UserControl
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
                    on = D1000.On = D100.On = D10.On = D1.On = value;
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
                    _value %= 10000;
                    D1000.Value = (byte)(_value / 1000);
                    D100.Value = (byte)((_value / 100) % 10);
                    D10.Value = (byte)((_value / 10) % 10);
                    D1.Value = (byte)(_value % 10);
                }
            }
        }
        #endregion

        public ucDisp4D()
        {
            InitializeComponent();
            Value = 0;
            On = false;
        }
    }
}
