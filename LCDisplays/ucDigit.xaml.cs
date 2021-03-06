﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfUC
{
    /// <summary>
    /// Interaction logic for ucDigit.xaml
    /// </summary>
    public partial class ucDigit : UserControl
    {
        private double cOpacityOff = .1D, cOpacityOn = .7D;
        private Storyboard sbFadeIn, sbFadeOut;
        private Style styleOff, styleOn;

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
                sp.Style = styleOff;
                //sp.Opacity = cOpacityOff;
            }
            //Seg11.Visibility = Seg12.Visibility = Seg71.Visibility = Seg72.Visibility = Visibility.Hidden;
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
            Seg1.Style = Seg2.Style = Seg3.Style = Seg5.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void set1()
        {
            resetDigit();
            //Seg12.Visibility = Seg72.Visibility = Visibility.Visible;
            Seg3.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            //Seg12.BeginStoryboard(sbFadeIn);
            //Seg72.BeginStoryboard(sbFadeIn);
            Seg3.Style = Seg6.Style = styleOn;
            //Seg3.Opacity = Seg6.Opacity = /*Seg12.Opacity = Seg72.Opacity =*/ cOpacityOn;
        }

        private void set2()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg3.Style = Seg4.Style = Seg5.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void set3()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg3.Style = Seg4.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void set4()
        {
            resetDigit();
            //Seg11.Visibility = Seg12.Visibility = Seg72.Visibility = Visibility.Visible;
            //Seg11.BeginStoryboard(sbFadeIn);
            //Seg12.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            //Seg72.BeginStoryboard(sbFadeIn);
            ///*Seg11.Opacity = Seg12.Opacity =*/ Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = /*Seg72.Opacity =*/ cOpacityOn;
            Seg2.Style = Seg3.Style = Seg4.Style = Seg6.Style = styleOn;
        }

        private void set5()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg2.Style = Seg4.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
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
            Seg1.Style = Seg2.Style = Seg4.Style = Seg5.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void set7()
        {
            resetDigit();
            //Seg72.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            //Seg72.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg3.Style = Seg6.Style = styleOn;
            //Seg1.Opacity = Seg3.Opacity = Seg6.Opacity = /*Seg72.Opacity =*/ cOpacityOn;
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
            Seg1.Style = Seg2.Style = Seg3.Style = Seg4.Style = Seg5.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
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
            Seg1.Style = Seg2.Style = Seg3.Style = Seg4.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void setA()
        {
            resetDigit();
            //Seg71.Visibility = Seg72.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            //Seg71.BeginStoryboard(sbFadeIn);
            //Seg72.BeginStoryboard(sbFadeIn);
            //Seg1.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = /*Seg71.Opacity = Seg72.Opacity =*/ cOpacityOn;
            Seg1.Style = Seg2.Style = Seg3.Style = Seg4.Style = Seg5.Style = Seg6.Style = styleOn;
        }

        private void setB()
        {
            resetDigit();
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            //Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
            Seg2.Style = Seg4.Style = Seg5.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            //Seg1.BeginStoryboard(sbFadeIn);
            //Seg11.BeginStoryboard(sbFadeIn);
            //Seg2.BeginStoryboard(sbFadeIn);
            //Seg3.BeginStoryboard(sbFadeIn);
            //Seg4.BeginStoryboard(sbFadeIn);
            //Seg5.BeginStoryboard(sbFadeIn);
            //Seg6.BeginStoryboard(sbFadeIn);
            //Seg7.BeginStoryboard(sbFadeIn);
            //Seg71.BeginStoryboard(sbFadeIn);
            //Seg1.Opacity = Seg11.Opacity = Seg2.Opacity = Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = Seg71.Opacity = cOpacityOn;
        }

        private void setC()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg2.Style = Seg5.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg5.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void setD()
        {
            resetDigit();
            Seg3.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg6.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            //Seg3.Opacity = Seg4.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = cOpacityOn;
            Seg3.Style = Seg4.Style = Seg5.Style = Seg6.Style = Seg7.Style = styleOn;
            //Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            //Seg1.BeginStoryboard(sbFadeIn);
            //Seg11.BeginStoryboard(sbFadeIn);
            //Seg2.BeginStoryboard(sbFadeIn);
            //Seg3.BeginStoryboard(sbFadeIn);
            //Seg5.BeginStoryboard(sbFadeIn);
            //Seg6.BeginStoryboard(sbFadeIn);
            //Seg7.BeginStoryboard(sbFadeIn);
            //Seg71.BeginStoryboard(sbFadeIn);
            //Seg1.Opacity = Seg11.Opacity = Seg2.Opacity = Seg3.Opacity = Seg5.Opacity = Seg6.Opacity = Seg7.Opacity = Seg71.Opacity = cOpacityOn;
        }

        private void setE()
        {
            resetDigit();
            Seg1.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            Seg7.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg2.Style = Seg4.Style = Seg5.Style = Seg7.Style = styleOn;
            //Seg1.Opacity = Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = Seg7.Opacity = cOpacityOn;
        }

        private void setF()
        {
            resetDigit();
            //Seg11.Visibility = Seg71.Visibility = Visibility.Visible;
            Seg1.BeginStoryboard(sbFadeIn);
            //Seg11.BeginStoryboard(sbFadeIn);
            Seg2.BeginStoryboard(sbFadeIn);
            Seg4.BeginStoryboard(sbFadeIn);
            Seg5.BeginStoryboard(sbFadeIn);
            //Seg71.BeginStoryboard(sbFadeIn);
            Seg1.Style = Seg2.Style = Seg4.Style = Seg5.Style = styleOn;
            //Seg1.Opacity = /*Seg11.Opacity =*/ Seg2.Opacity = Seg4.Opacity = Seg5.Opacity = /*Seg71.Opacity =*/ cOpacityOn;
        }
        #endregion

        public ucDigit()
        {
            InitializeComponent();
            sbFadeIn = FindResource("FadeIn") as Storyboard;
            sbFadeOut = FindResource("FadeOut") as Storyboard;
            cOpacityOff = (double)FindResource("OpacityOff");
            cOpacityOn = (double)FindResource("OpacityOn");
            styleOff = FindResource("SegmentStyleOff") as Style;
            styleOn = FindResource("SegmentStyleOn") as Style;
            resetDigit();
            On = false;
            Value = 0;
        }
    }
}
