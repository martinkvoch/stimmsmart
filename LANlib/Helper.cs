using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Bulb;
using word = System.UInt16;
using dword = System.UInt32;

namespace LANlib
{
    public enum DioReg : byte { LedR, LedG, LedB, LedNBlink }
    public enum LANReg : byte { G1 = 0x01, G2 = 0x02, G3 = 0x04, G4 = 0x08, G5 = 0x10, G6 = 0x20 }

    #region Bits
    public class Bits
    {
        private word value = 0;

        public word Value { get { return value; } }
        public byte ByteValue { get { return (byte)(value & 0x00FF); } }

        public bool this[int d]
        {
            get { return (value & (word)(0x0001 << d)) > 0; }
            set
            {
                word mask = (word)(0x0001 << d);

                this.value = value ? (word)(this.value | mask) : (word)(this.value & ~mask);
            }
        }

        public bool this[DioReg d]
        {
            get { return this[(int)d]; }
            set { this[(int)d] = value; }
        }

        public bool this[StatusBits s]
        {
            get { return this[(int)s]; }
            set { this[(int)s] = value; }
        }

        public Bits(word value) { this.value = value; }
        public Bits(byte value) { this.value = value; }
        public Bits() : this(0) { }
        public Bits(bool d0 = false, bool d1 = false, bool d2 = false, bool d3 = false, bool d4 = false, bool d5 = false, bool d6 = false, bool d7 = false) : this()
        {
            this[0] = d0;
            this[1] = d1;
            this[2] = d2;
            this[3] = d3;
            this[4] = d4;
            this[5] = d5;
            this[6] = d6;
            this[7] = d7;
        }

        public override string ToString()
        {
            string res = string.Empty;

            for(int i = 0; i < 16; i++) res += this[(DioReg)i] ? "1" : "0";
            res += string.Format(" (0x{0:X4}, {0})", value);
            return res;
        }
    }
    #endregion

    #region Helper
    public static class Helper
    {
        public const byte DioLedOff = 0x00,
                          DioLedRed = 0x01,
                          DioLedGreen = 0x02,
                          DioLedBlue = 0x04,
                          DioLedBlink = 0x08;
        //public const byte DioLedOff = 0x00,
        //                  DioLenBlue = 0x01,
        //                  DioLedGreen = 0x02,
        //                  DioLedLightBlue = 0x03,
        //                  DioLedRed = 0x04,
        //                  DioLedViolet = 0x05,
        //                  DioLedYellow = 0x06,
        //                  DioLedWhite = 0x07;

        public static byte[] ToBytes(word[] words)
        {
            byte[] res = new byte[words.Length << 1];

            for(int i = 0; i < words.Length; i++)
            {
                int j = i << 1;

                res[j] = (byte)(words[i] >> 8);
                res[j + 1] = (byte)(words[i] & 0x00FF);
            }
            return res;
        }

        public static word[] ToWords(byte[] bytes)
        {
            word[] res = new word[bytes.Length >> 1];

            for(int i = 0; i < bytes.Length; i += 2)
            {
                res[i >> 1] = (word)((bytes[i] << 8) + bytes[i + 1]);
            }
            return res;
        }

        public static word[] ToWords(dword[] dwords)
        {
            word[] res = new word[dwords.Length << 1];

            for(int i = 0; i < dwords.Length; i++)
            {
                int j = i << 1;

                res[j] = (word)(dwords[i] >> 16);
                res[j + 1] = (word)(dwords[i] & 0xFFFF);
            }
            return res;
        }

        public static byte BDioLedMask
        {
            get
            {
                Bits bits = new Bits();

                bits[DioReg.LedR] = true;
                bits[DioReg.LedG] = true;
                bits[DioReg.LedB] = true;
                bits[DioReg.LedNBlink] = true;
                return bits.ByteValue;
            }
        }

        public static void LEDBits(LedBulb led, Bits value)
        {
            //value = new Bits((byte)(value.ByteValue & BDioLedMask));
            led.Color = Color.FromArgb(value[DioReg.LedR] ? 255 : 0, value[DioReg.LedG] ? 255 : 0, value[DioReg.LedB] ? 255 : 0);
            led.Blink(!value[DioReg.LedNBlink] ? 500 : 0);
            led.On = true;
            //led.On = !value[DioReg.OnOff];
            led.Refresh();
        }
    }
    #endregion
}
