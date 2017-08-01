using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucSegments.xaml
    /// </summary>
    public partial class ucSegments : UserControl
    {
        //const int cSegNum = 6;
        const double cOpacityOff = .2D, cOpacityOn = .6D;

        #region On
        private bool on = true;
        private bool On
        {
            get { return on; }
            set
            {
                on = value;
                if(!on) Segments = new byte[] { 5, 5, 5, 5, 5, 5 };
            }
        }
        #endregion

        #region Segments
        private byte[] segments = new byte[0];
        public byte[] Segments
        {
            get { return segments; }
            set
            {
                int i = 0;

                segments = value;
                grMain.Children.Clear();
                foreach(int seg in segments)
                {
                    Border b = new Border() { Background = Brushes.Black, Margin = new Thickness(5, 0, 5, 0), Opacity = cOpacityOff };
                    int span = seg / 5;

                    grMain.Children.Add(b);
                    Grid.SetColumn(b, i);
                    Grid.SetColumnSpan(b, span);
                    i += span;
                }
                _curSegment = 0;
            }
        }
        #endregion

        #region curSegment
        private byte _curSegment = byte.MaxValue;
        private byte curSegment
        {
            get { return _curSegment; }
            set
            {
                if(_curSegment != value)
                {
                    _curSegment = value;
                    if(_curSegment == 0) foreach(UIElement b in grMain.Children) b.Opacity = cOpacityOff;
                    else if(_curSegment <= grMain.Children.Count) grMain.Children[_curSegment - 1].Opacity = cOpacityOn;
                }
            }
        }
        #endregion

        internal void NextSegment()
        {
            curSegment++;
        }

        public ucSegments()
        {
            InitializeComponent();
            On = false;
        }
    }
}
