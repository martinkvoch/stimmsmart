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
    /// Interaction logic for ucTimer.xaml
    /// </summary>
    public partial class ucTimer : UserControl
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
                    on = DH.On = JH.On = DS.On = JS.On = value;
                    if(!on) Value = 0;
                }
            }
        }
        #endregion

        #region Value
        private ushort _value = 0;
        public ushort Value
        {
            get { return _value; }
            set
            {
                if(_value != value && On)
                {
                    _value = (ushort)(value % 6000);
                    DH.Value = (byte)(_value / 600);
                    JH.Value = (byte)((_value / 60) % 10);
                    DS.Value = (byte)((_value % 60) / 10);
                    JS.Value = (byte)((_value % 60) % 10);
                }
            }
        }
        #endregion

        public ucTimer()
        {
            InitializeComponent();
            Value = 0;
            On = false;
        }
    }
}
