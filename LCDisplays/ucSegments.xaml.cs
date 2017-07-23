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
    /// Interaction logic for ucSegments.xaml
    /// </summary>
    public partial class ucSegments : UserControl
    {
        const double cOpacityOff = .2D, cOpacityOn = .6D;

        #region On
        private bool on = false;
        private bool On
        {
            get { return on; }
            set
            {
                if(on != value)
                {
                    on = value;
                    Visibility = on ? Visibility.Visible : Visibility.Hidden;
                    if(!on)
                    {
                        curSegment = 0;
                        Segments = new byte[0];
                    }
                }
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
                if(segments != value)
                {
                    int i = 0;

                    segments = value;
                    //grMain.ColumnDefinitions.Clear();
                    grMain.Children.Clear();
                    foreach(int seg in segments)
                    {
                        Border b = new Border() { Background=Brushes.Black, Margin=new Thickness(5, 0, 5, 0), Opacity=cOpacityOff };
                        int span = seg / 5;

                        //grMain.ColumnDefinitions.Add(new ColumnDefinition());
                        grMain.Children.Add(b);
                        Grid.SetColumn(b, i);
                        Grid.SetColumnSpan(b, span);
                        i += span;
                    }
                    curSegment = 0;
                    On = segments.Length > 0;
                }
            }
        }
        #endregion

        #region curSegment
        private byte _curSegment = 0;
        private byte curSegment
        {
            get { return _curSegment; }
            set
            {
                if(_curSegment != value)
                {
                    _curSegment = value;
                    if(_curSegment == 0) foreach(UIElement b in grMain.Children) b.Opacity = cOpacityOff;
                    else
                    {
                        if(!on) On = true;
                        grMain.Children[_curSegment - 1].Opacity = cOpacityOn;
                    }
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
            Visibility = Visibility.Hidden;
            curSegment = 0;
        }
    }
}
