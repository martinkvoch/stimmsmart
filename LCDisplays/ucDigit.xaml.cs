using System.Windows.Controls;
using System.Collections;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucDigit.xaml
    /// </summary>
    public partial class ucDigit : UserControl
    {
        private const double opacityOff = .1, opacityOn = .6;
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
                    if(on) set0(); else resetDigit();
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
                    _value = (byte)(value % 16);
                    switch(_value)
                    {
                        case 0: set0(); break;
                        case 1: set1(); break;
                        case 2: set2(); break;
                        case 3: set3(); break;
                        case 4: set4(); break;
                        case 5: set5(); break;
                        case 6: set6(); break;
                        case 7: set7(); break;
                        case 8: set8(); break;
                        case 9: set9(); break;
                        case 10: setA(); break;
                        case 11: setB(); break;
                        case 12: setC(); break;
                        case 13: setD(); break;
                        case 14: setE(); break;
                        case 15: setF(); break;
                    }
                }
            }
        }
        #endregion

        #region Nastavení číslic
        private void resetDigit()
        {
            foreach(Shape sp in canMain.Children)
            {
                sp.BeginStoryboard(sbFadeOut);
                sp.Opacity = opacityOff;
            }
            Seg11.Visibility = Seg12.Visibility = Seg71.Visibility = Seg72.Visibility = Visibility.Hidden;
        }

        private void set0()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set1()
        {
            resetDigit();
            Seg12.Visibility = Seg72.Visibility = Visibility.Visible;
            Seg3.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg12.BeginStoryboard(sbFadeIn);
            Seg72.BeginStoryboard(sbFadeIn);
            Seg3.Opacity = Seg6.Opacity = Seg12.Opacity = Seg72.Opacity = opacityOn;
        }

        private void set2()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set3()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set4()
        {
            resetDigit();
            Seg11.Visibility = Seg12.Visibility = Seg72.Visibility = Visibility.Visible;
            Seg11.BeginStoryboard(sbFadeIn);
            Seg12.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg72.BeginStoryboard(sbFadeIn);
            Seg11.Opacity = Seg12.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = Seg72.Opacity = opacityOn;
        }

        private void set5()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set6()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set7()
        {
            resetDigit();
            Seg72.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg72.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg3.Opacity = Seg6.Opacity = Seg72.Opacity = opacityOn;
        }

        private void set8()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void set9()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = opacityOn;
        }

        private void setA()
        {
            resetDigit();
            Seg71.Visibility = Seg72.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg71.BeginStoryboard(sbFadeIn);
            Seg72.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg71.Opacity = Seg72.Opacity = opacityOn;
        }

        private void setB()
        {
            resetDigit();
            Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg11.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg71.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg11.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = Seg71.Opacity = opacityOn;
        }

        private void setC()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg5.Opacity = Seg7.Opacity = opacityOn;
        }

        private void setD()
        {
            resetDigit();
            Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg11.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg71.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg11.Opacity = Seg2.Opacity = Seg3.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = Seg71.Opacity = opacityOn;
        }

        private void setE()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg7.Opacity = opacityOn;
        }

        private void setF()
        {
            resetDigit();
            Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg11.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg71.BeginStoryboard(sbFadeIn);
            Seg1.Opacity = Seg11.Opacity = Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg71.Opacity = opacityOn;
        }
        #endregion

        public ucDigit()
        {
            InitializeComponent();
            sbFadeIn = FindResource("FadeIn") as Storyboard;
            sbFadeOut = FindResource("FadeOut") as Storyboard;
            resetDigit();
            On = false;
            Value = 0;
        }
    }
}
