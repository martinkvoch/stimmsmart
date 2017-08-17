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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucOhmMeter.xaml
    /// </summary>
    public partial class ucOhmMeter : UserControl
    {
        private const double cOpacityOff = .1D, cOpacityOn = .6D;
        private byte[] cSegments = new byte[] { 28, 33, 38, 43, 48, 49, 50, 51, 52, 57, 62, 67, 72 };
        //private byte[] cSegments = new byte[] { 72, 67, 62, 57, 52, 51, 50, 49, 48, 43, 38, 33, 28 };
        private Storyboard sbFadeIn, sbFadeOut;

        #region On
        private bool on = false;
        public bool On
        {
            get { return on; }
            set
            {
                if(on != value)
                {
                    on = value;
                    if(on) Value = cSegments[0]; else resetMeter();
                }
            }
        }
        #endregion

        #region Value
        private byte _value = 0;
        public byte Value
        {
            get { return _value; }
            set
            {
                if(_value != value && On)
                {
                    if(value < cSegments[0]) _value = cSegments[0];
                    else if(value > cSegments[cSegments.Length - 1]) _value = cSegments[cSegments.Length - 1];
                         else _value = value;
                    segmentsOnOff(_value);
                }
            }
        }
        #endregion

        private void resetMeter()
        {
            foreach(Shape sp in canMain.Children)
            {
                sp.BeginStoryboard(sbFadeOut);
                sp.Opacity = cOpacityOff;
            }
        }

        private int getIndex(byte value)
        {
            int res = 0;

            if(value <= cSegments[0]) res = 0;
            else for(int i = 0; i < cSegments.Length - 2; i++) if(value > cSegments[i] && value <= cSegments[i + 1]) { res = i; break; }
            if(value > cSegments[cSegments.Length - 1]) res = cSegments.Length - 1;
            return res;
        }

        private void segmentsOnOff(byte value)
        {
            int idx = getIndex(value);

            resetMeter();
            for(int i = cSegments.Length - 1; i >= idx; i--)
            {
                Rectangle rect = canMain.FindName("Seg" + (i + 1).ToString()) as Rectangle;

                rect.BeginStoryboard(sbFadeIn);
                rect.Opacity = cOpacityOn;
            }
            //if(Seg14.Opacity == cOpacityOff)
            //{
            //    Seg14.BeginStoryboard(sbFadeIn);
            //    Seg14.Opacity = cOpacityOn;
            //}
            //for(int i = cSegments.Length - 1; i >= 0; i--)
            //{
            //    Rectangle rect = canMain.FindName("Seg" + (i+1).ToString()) as Rectangle;

            //    if(i <= idx)
            //    {
            //        if(rect.Opacity == cOpacityOff)
            //        {
            //            rect.BeginStoryboard(sbFadeIn);
            //            rect.Opacity = cOpacityOn;
            //        }
            //    }
            //    else
            //    {
            //        if(rect.Opacity == cOpacityOn)
            //        {
            //            rect.BeginStoryboard(sbFadeOut);
            //            rect.Opacity = cOpacityOff;
            //        }
            //    }
            //}
        }

        public ucOhmMeter()
        {
            InitializeComponent();
            sbFadeIn = FindResource("FadeIn") as Storyboard;
            sbFadeOut = FindResource("FadeOut") as Storyboard;
            resetMeter();
        }
    }
}
