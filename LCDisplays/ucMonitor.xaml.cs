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
    public enum MonitorMode { User, Admin }

    /// <summary>
    /// Interaction logic for ucMonitor.xaml
    /// </summary>
    public partial class ucMonitor : UserControl
    {
        #region MonMode
        private MonitorMode monMode = MonitorMode.User;
        public MonitorMode MonMode
        {
            get { return monMode; }
            set
            {
                if(monMode != value)
                {
                    monMode = value;
                    switch(monMode)
                    {
                        case MonitorMode.User:
                            grLAN.Visibility = grMode.Visibility = grSegments.Visibility = Visibility.Hidden;
                            grOhmMeter.Visibility = Visibility.Visible;
                            break;
                        case MonitorMode.Admin:
                            grOhmMeter.Visibility = Visibility.Hidden;
                            grLAN.Visibility = grMode.Visibility = grSegments.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        #endregion

        #region On
        private bool on = false;
        public bool On
        {
            get { return on; }
            set
            {
                //if(on != value)
                //{
                    //SetValue(OnProperty, value);
                    on = disWS.On = disSweep.On = disATC.On = disDAC.On = disDOUT.On = disStatus.On = timElapsed.On = timLeft.On = timSegmentLeft.On = disMode.On = disOhmMeter.On = value;
                    if(!on)
                    {
                        resetMonitor();
                        grMain.Background = FindResource("OffBackgroundBrush") as Brush;
                    }
                    else grMain.Background = FindResource("OnBackgroundBrush") as Brush;
                //}
            }
        }

        //public static readonly DependencyProperty OnProperty = DependencyProperty.Register("On", typeof(bool), typeof(ucMonitor), new PropertyMetadata(false));
        #endregion

        #region Displeje
        public word WS
        {
            get { return (word)disWS.Value; }
            set { disWS.Value = value; }
        }

        public word Sweep
        {
            get { return (word)disSweep.Value; }
            set { disSweep.Value = value; }
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

        public byte Mode
        {
            get { return disMode.Value; }
            set { disMode.Value = value; }
        }

        public word Ohms
        {
            get { return disOhmMeter.Value; }
            set { disOhmMeter.Value = value; /*lbOhms.Content = disOhmMeter.Value;*/ }
        }
        #endregion

        #region resetMonitor()
        private void resetMonitor()
        {
            WS = Sweep = DAC = Status = Elapsed = Remained = SegmentLeft = 0;
            ATC = DOUT = Mode = 0;
            Segments = new byte[] { 5, 5, 5, 5, 5, 5 };
            monMode = MonitorMode.Admin;
            MonMode = MonitorMode.User;
            Ohms = word.MaxValue;
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
