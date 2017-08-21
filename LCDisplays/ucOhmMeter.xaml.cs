using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using word = System.UInt16;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucOhmMeter.xaml
    /// </summary>
    public partial class ucOhmMeter : UserControl
    {
        private const double cOpacityOff = .1D, cOpacityOn = .6D;
        private static word[] cSegments = new word[] { 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62 };
        //private static word[] cSegments = new word[] { 28, 33, 38, 43, 48, 49, 50, 51, 52, 57, 62, 67, 72 };
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
                    if(on) Value = (word)(cSegments[cSegments.Length - 1] + 10); else resetMeter();
                }
            }
        }
        #endregion

        #region Value
        private word _value = (word)(cSegments[cSegments.Length - 1] + 10);
        public word Value
        {
            get { return _value; }
            set
            {
                if(_value != value && On)
                {
                    //if(value < cSegments[0]) _value = cSegments[0];
                    //else if(value > cSegments[cSegments.Length - 1]) _value = cSegments[cSegments.Length - 1];
                    //     else _value = value;
                    segmentsOnOff(_value = value);
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

        private int getIndex(word value)
        {
            int res = -1;

            //if(value <= cSegments[0]) res = 0;
            for(int i = 0; i < cSegments.Length - 1; i++) if(value >= cSegments[i] && value < cSegments[i + 1]) { res = i; break; }
            //if(value >= cSegments[cSegments.Length - 1]) res = cSegments.Length - 1;
            return res;
        }

        private void segmentsOnOff(word value)
        {
            int idx = getIndex(value);

            resetMeter();
            if(idx >= 0)
                for(int i = cSegments.Length + 1; i > idx; i--)
                {
                    Rectangle rect = canMain.FindName("Seg" + i.ToString()) as Rectangle;

                    rect.BeginStoryboard(sbFadeIn);
                    rect.Opacity = cOpacityOn;
                }
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
