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
using word = System.UInt16;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucMonitor.xaml
    /// </summary>
    public partial class ucMonitor : UserControl
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
                    //SetValue(OnProperty, value);
                    on = disAIN1.On = disAIN2.On = disATC.On = disDAC.On = disDOUT.On = disStatus.On = timElapsed.On = timLeft.On = timSegmentLeft.On = value;
                    if(!on)
                    {
                        resetMonitor();
                        grMain.Background = FindResource("OffBackgroundBrush") as Brush;
                    }
                    else grMain.Background = FindResource("OnBackgroundBrush") as Brush;
                }
            }
        }

        //public static readonly DependencyProperty OnProperty = DependencyProperty.Register("On", typeof(bool), typeof(ucMonitor), new PropertyMetadata(false));
        #endregion

        #region Displeje
        public word AIN1
        {
            get { return (word)disAIN1.Value; }
            set { disAIN1.Value = value; }
        }

        public word AIN2
        {
            get { return (word)disAIN2.Value; }
            set { disAIN2.Value = value; }
        }

        public byte ATC
        {
            get { return (byte)disATC.Value; }
            set { disATC.Value = value; }
        }

        public word DAC
        {
            get { return (word)disDAC.Value; }
            set { disDAC.Value = value; }
        }

        public byte DOUT
        {
            get { return (byte)disDOUT.Value; }
            set { disDOUT.Value = value; }
        }

        public word Status
        {
            get { return (word)disStatus.Value; }
            set { disStatus.Value = value; }
        }

        public word Elapsed
        {
            get { return timElapsed.Value; }
            set { timElapsed.Value = value; }
        }

        public word Remained
        {
            get { return timLeft.Value; }
            set { timLeft.Value = value; }
        }

        public word SegmentLeft
        {
            get { return timSegmentLeft.Value; }
            set { timSegmentLeft.Value = value; }
        }

        public byte[] Segments
        {
            get { return segProcSegments.Segments; }
            set { segProcSegments.Segments = value; }
        }
        #endregion

        #region resetMonitor()
        private void resetMonitor()
        {
            AIN1 = AIN2 = DAC = Status = Elapsed = Remained = SegmentLeft = 0;
            ATC = DOUT = 0;
        }
        #endregion

        public void NextSegment()
        {
            segProcSegments.NextSegment();
        }

        public ucMonitor()
        {
            InitializeComponent();
            On = false;
            resetMonitor();
        }
    }
}
