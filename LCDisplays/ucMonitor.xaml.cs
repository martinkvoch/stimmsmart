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
        public int AIN1
        {
            get { return disAIN1.Value; }
            set { disAIN1.Value = value; }
        }

        public int AIN2
        {
            get { return disAIN2.Value; }
            set { disAIN2.Value = value; }
        }

        public int ATC
        {
            get { return disATC.Value; }
            set { disATC.Value = value; }
        }

        public int DAC
        {
            get { return disDAC.Value; }
            set { disDAC.Value = value; }
        }

        public int DOUT
        {
            get { return disDOUT.Value; }
            set { disDOUT.Value = value; }
        }

        public int Status
        {
            get { return disStatus.Value; }
            set { disStatus.Value = value; }
        }

        public ushort Elapsed
        {
            get { return timElapsed.Value; }
            set { timElapsed.Value = value; }
        }

        public ushort Remained
        {
            get { return timLeft.Value; }
            set { timLeft.Value = value; }
        }

        public ushort SegmentLeft
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
            //disAIN1.Value = disAIN2.Value = disATC.Value = disDAC.Value = disDOUT.Value = disStatus.Value = timElapsed.Value = timLeft.Value = timSegmentLeft.Value = 0;
            AIN1 = AIN2 = ATC = DAC = DOUT = Status = Elapsed = Remained = SegmentLeft = 0;
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
