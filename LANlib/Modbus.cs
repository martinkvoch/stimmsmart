using System;
using System.Linq;
using dword = System.UInt32;
using word = System.UInt16;

namespace LANlib
{
    public enum GenModes : word { Quiet, NoSweep, Sweep }
    public enum StatusBits { Limit10mA, HighImp, InvalidShape, InvalidHold = 13, SysWatchdog = 14, Restart = 15 }

    #region ModbusHolding
    /// <summary>
    /// Implementace Modbus holding registrů
    /// </summary>
    public class ModbusHolding
    {
        public word Mode;
        public word Waweform;
        public word T3Max, T3Min, T3Sweep;
        public word AttenCoef;
        public word DAC;
        public Bits DOUT;

        #region Length
        ///// <summary>
        ///// Délka Modbus holding registrů v bytech
        ///// </summary>
        //public static int Length { get { return new ModbusHolding().ToBytes().Length; } }
        #endregion

        public ModbusHolding(word mode = (word)GenModes.Quiet, word waweform = 0, word t3max = 0, word t3min = 0, word t3sweep = 0, word attencoef = 0, word dac = 0, word dout = 0)
        {
            Mode = mode;
            Waweform = waweform;
            T3Max = t3max;
            T3Min = t3min;
            T3Sweep = t3sweep;
            AttenCoef = attencoef;
            DAC = dac;
            DOUT = new Bits(dout);
        }

        #region ToWords()
        /// <summary>
        /// Konverze instance třídy na pole dvoubytových slov
        /// </summary>
        /// <returns>Vrací pole dvoubytových slov (word, ushort).</returns>
        public word[] ToWords()
        {
            word[] res = new word[8];

            res[0] = Mode;
            res[1] = Waweform;
            res[2] = T3Max;
            res[3] = T3Min;
            res[4] = T3Sweep;
            res[5] = AttenCoef;
            res[6] = DAC;
            res[7] = DOUT.Value;
            return res;
        }
        #endregion

        #region ToBytes()
        /// <summary>
        /// Konverze instance třídy na pole bytů
        /// </summary>
        /// <returns>Vrací pole bytů.</returns>
        public byte[] ToBytes()
        {
            return Helper.ToBytes(ToWords());
        }
        #endregion

        #region ToString()
        /// <summary>
        /// Konverze instance třídy na řetězec obsahující jednotlivé hodnoty po bytech
        /// </summary>
        /// <returns>Vrací řetězcovou hodnotu reprezentující datagram z Modbus holding registrů</returns>
        public override string ToString()
        {
            //word[] words = ToWords();
            string res = string.Empty;

            //foreach(word w in words) res += string.Format("{0} (0x{0:X4}) ", w, w);
            res += string.Format("Mode:                 {0}", Mode) + Environment.NewLine;
            res += string.Format("Shape:                {0}", Waweform) + Environment.NewLine;
            res += string.Format("T3 (Max, Min, Sweep): {0}, {1}, {2}", T3Max, T3Min, T3Sweep) + Environment.NewLine;
            res += string.Format("Atten. coef.:         {0}", AttenCoef) + Environment.NewLine;
            res += string.Format("DAC:                  {0}", DAC) + Environment.NewLine;
            res += string.Format("DOUT:                 {0}", DOUT) + Environment.NewLine;
            return res;
        }
        #endregion

        #region FromWords()
        /// <summary>
        /// Z pole dvoubytových slov zkonstruuje instanci třídy ModbusHolding
        /// </summary>
        /// <param name="words">pole dvoubytových slov (word, ushort)</param>
        /// <returns>Vrací instanci třídy ModbusHolding</returns>
        public static ModbusHolding FromWords(word[] words)
        {
            ModbusHolding res = new ModbusHolding(words[0], words[1], words[2], words[3], words[4], words[5], words[6], words[7]);

            return res;
        }
        #endregion

        #region FromBytes()
        /// <summary>
        /// Z pole bytů zkonstruuje instanci třídy ModbusHolding
        /// </summary>
        /// <param name="bytes">pole bytů</param>
        /// <returns>Vrací instanci třídy ModbusHolding</returns>
        public static ModbusHolding FromBytes(byte[] bytes)
        {
            return FromWords(Helper.ToWords(bytes));
        }
        #endregion
    }
    #endregion

    #region ModbusInput
    /// <summary>
    /// Implementace Modbus input registrů
    /// </summary>
    public class ModbusInput
    {
        public ModbusHolding Holding, Verified;
        public Bits Status;
        public word T3;
        public dword TS, TsWrHoldR, TsRdInpR;
        public word AIN1, AIN2;
        public word FWVersion { get { return 100; } }

        #region Length
        ///// <summary>
        ///// Délka Modbus input registrů v bytech
        ///// </summary>
        //public static int Length { get { return new ModbusInput().ToBytes().Length; } }
        #endregion

        public ModbusInput(ModbusHolding holding = null, ModbusHolding verified = null, word status = 0, word t3 = 0, dword ts = 0, dword tswrholdr = 0, dword tsrdinpr = 0, word ain1 = 0, word ain2 = 0)
        {
            Holding = holding ?? new ModbusHolding();
            Verified = verified ?? new ModbusHolding();
            Status = new Bits(status);
            T3 = t3;
            TS = ts;
            TsWrHoldR = tswrholdr;
            TsRdInpR = tsrdinpr;
            AIN1 = ain1;
            AIN2 = ain2;
        }

        #region ToWords()
        /// <summary>
        /// Konverze instance třídy na pole dvoubytových slov
        /// </summary>
        /// <returns>Vrací pole dvoubytových slov (word, ushort).</returns>
        public word[] ToWords()
        {
            word[] res = Holding.ToWords().Concat(Verified.ToWords()).Concat(new word[16]).ToArray();

            res[16] = Status.Value;
            res[17] = T3;
            res[18] = (word)(TS >> 16);
            res[19] = (word)(TS & 0x0000FFFF);
            res[20] = (word)(TsWrHoldR >> 16);
            res[21] = (word)(TsWrHoldR & 0x0000FFFF);
            res[22] = (word)(TsRdInpR >> 16);
            res[23] = (word)(TsRdInpR & 0x0000FFFF);
            res[24] = AIN1;
            res[25] = AIN2;
            res[26] = res[27] = res[28] = res[29] = res[30] = 0;
            res[31] = FWVersion;
            return res;
        }
        #endregion

        #region ToBytes()
        /// <summary>
        /// Konverze instance třídy na pole bytů
        /// </summary>
        /// <returns>Vrací pole bytů.</returns>
        public byte[] ToBytes()
        {
            return Helper.ToBytes(ToWords());
        }
        #endregion

        #region ToString()
        /// <summary>
        /// Konverze instance třídy na řetězec obsahující jednotlivé hodnoty po bytech
        /// </summary>
        /// <returns>Vrací řetězcovou hodnotu reprezentující datagram z Modbus input registrů</returns>
        public override string ToString()
        {
            //word[] words = ToWords();
            string res = string.Empty;

            //foreach(word w in words) res += string.Format("{0} (0x{0:X4}) ", w, w);
            res += "Holding regs.:" + Environment.NewLine;
            res += string.Format("{0}", Holding) + Environment.NewLine;
            res += "Verified holding regs.:" + Environment.NewLine;
            res += string.Format("{0}", Verified) + Environment.NewLine;
            res += string.Format("Status:                      {0}", Status) + Environment.NewLine;
            res += string.Format("T3:                          {0}", T3) + Environment.NewLine;
            res += string.Format("Ts, TsWrHoldR, TsRdInpR:     {0}, {1}, {2}", TS, TsWrHoldR, TsRdInpR) + Environment.NewLine;
            res += string.Format("Ain1, Ain2:                  {0}, {1}", AIN1, AIN2) + Environment.NewLine;
            res += string.Format("FWVersion:                   {0}", FWVersion) + Environment.NewLine;
            return res;
        }
        #endregion

        #region FromDWords()
        /// <summary>
        /// Z pole čtyřbytových slov zkonstruuje instanci třídy ModbusInput
        /// </summary>
        /// <param name="dwords">pole čtyřbytových slov (dword, uint)</param>
        /// <returns>Vrací instanci třídy Modbus input</returns>
        public static ModbusInput FromDWords(dword[] dwords)
        {
            return FromWords(Helper.ToWords(dwords));
        }
        #endregion

        #region FromWords()
        /// <summary>
        /// Z pole dvoubytových slov zkonstruuje instanci třídy ModbusInput
        /// </summary>
        /// <param name="words">pole dvoubytových slov (word, ushort)</param>
        /// <returns>Vrací instanci třídy ModbusInput</returns>
        public static ModbusInput FromWords(word[] words)
        {
            ModbusInput res = new ModbusInput(
                ModbusHolding.FromWords(words.Take(8).ToArray()),
                ModbusHolding.FromWords(words.Skip(8).Take(8).ToArray()),
                words[16], words[17],
                (dword)((words[18] << 16) + words[19]),
                (dword)((words[20] << 16) + words[21]),
                (dword)((words[22] << 16) + words[23]),
                words[24], words[25]);

            return res;
        }
        #endregion

        #region FromBytes()
        /// <summary>
        /// Z pole bytů zkonstruuje instanci třídy ModbusInput
        /// </summary>
        /// <param name="bytes">pole bytů</param>
        /// <returns>Vrací instanci třídy ModbusInput</returns>
        public static ModbusInput FromBytes(byte[] bytes)
        {
            return FromWords(Helper.ToWords(bytes));
        }
        #endregion
    }
    #endregion

    #region Modbus
    /// <summary>
    /// Realizace rozhraní protokolu Modbus RTU
    /// </summary>
    public class Modbus
    {
        private ModbusHolding holding;
        private ModbusInput input;

        #region Vlastnosti
        /// <summary>
        /// Sada holding registrů určených k zápisu do parametrů kanálu
        /// </summary>
        public ModbusHolding Holding { get { if(holding == null) holding = new ModbusHolding(); return holding; } }
        /// <summary>
        /// Sada input registrů určených ke čtení z kanálu
        /// </summary>
        public ModbusInput Input { get { if(input == null) input = new ModbusInput(); return input; } }
        #endregion

        #region Konstruktory
        public Modbus()
        {
            quietVerify(new ModbusHolding());
        }

        public Modbus(ModbusHolding holding)
        {
            quietVerify(holding);
        }
        #endregion

        #region Metody
        /// <summary>
        /// Proces verifikace hodnot holding registrů při přechodech mezi režimy kanálu
        /// </summary>
        /// <param name="cmd">vstupní holding registry, které mají být zapsány</param>
        /// <returns>Vrací logickou hodnotu true, pokud byla verifikace úspěšná.</returns>
        public bool Verify(ModbusHolding cmd)
        {
            bool res = false;

            Input.Holding = cmd;
            if(Holding.Mode == (word)GenModes.Quiet && cmd.Mode == (word)GenModes.Quiet) res = quietVerify(cmd);
            else if(Holding.Mode == (word)GenModes.Quiet && cmd.Mode == (word)GenModes.NoSweep) res = quiet2nosweepVerify(cmd);
            else if(Holding.Mode == (word)GenModes.NoSweep && cmd.Mode == (word)GenModes.NoSweep) res = nosweepVerify(cmd);
            else if(Holding.Mode == (word)GenModes.NoSweep && cmd.Mode == (word)GenModes.Quiet) res = nosweep2quietVerify(cmd);
            else if(Holding.Mode == (word)GenModes.Quiet && cmd.Mode == (word)GenModes.Sweep) res = quiet2sweepVerify(cmd);
            else if(Holding.Mode == (word)GenModes.Sweep && cmd.Mode == (word)GenModes.Sweep) res = sweepVerify(cmd);
            else if(Holding.Mode == (word)GenModes.Sweep && cmd.Mode == (word)GenModes.Quiet) res = sweep2quietVerify(cmd);
            holding = cmd;
            //Input.Verified = res ? cmd : new ModbusHolding();
            Input.Status[StatusBits.InvalidHold] = !res;
            return res;
        }

        private bool quietVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.Quiet;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                Input.Verified.Waweform = cmd.Waweform;
                Input.Verified.T3Max = cmd.T3Max;
                Input.Verified.T3Min = cmd.T3Min;
                Input.Verified.T3Sweep = cmd.T3Sweep;
                Input.Verified.DAC = cmd.DAC;
                res &= (cmd.AttenCoef >= 0 && cmd.AttenCoef <= 255);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DOUT.ByteValue >= 0 && cmd.DOUT.ByteValue <= 2);
                res &= (Holding.DOUT.ByteValue != 1 || cmd.DOUT.ByteValue != 2);
                res &= (Holding.DOUT.ByteValue != 2 || cmd.DOUT.ByteValue != 1);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool quiet2nosweepVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.NoSweep;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                res &= (cmd.Waweform >= 0U && cmd.Waweform <= 511U);
                if(res) Input.Verified.Waweform = cmd.Waweform;
                res &= (cmd.T3Max >= 800U && cmd.T3Max <= 1600U);
                if(res) Input.Verified.T3Max = cmd.T3Max;
                Input.Verified.T3Min = cmd.T3Min;
                Input.Verified.T3Sweep = cmd.T3Sweep;
                res &= (cmd.AttenCoef >= 0U && cmd.AttenCoef <= 255U);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DAC == 32768U && holding.DAC == 32768U);
                if(res) Input.Verified.DAC = cmd.DAC;
                res &= ((cmd.DOUT.ByteValue & 0x03) == 0U || (cmd.DOUT.ByteValue & 0x03) == 2U);
                res &= (cmd.DOUT.ByteValue == holding.DOUT.ByteValue);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool nosweepVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.NoSweep;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                res &= (cmd.Waweform == holding.Waweform);
                if(res) Input.Verified.Waweform = cmd.Waweform;
                res &= (cmd.T3Max >= 800U && cmd.T3Max <= 1600U);
                if(res) Input.Verified.T3Max = cmd.T3Max;
                Input.Verified.T3Min = cmd.T3Min;
                Input.Verified.T3Sweep = cmd.T3Sweep;
                res &= (cmd.AttenCoef >= 0U && cmd.AttenCoef <= 255U);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DAC == 32768U);
                if(res) Input.Verified.DAC = cmd.DAC;
                res &= ((cmd.DOUT.ByteValue & 0x03) == 0U || (cmd.DOUT.ByteValue & 0x03) == 2U);
                res &= (cmd.DOUT.ByteValue == holding.DOUT.ByteValue);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool nosweep2quietVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.Quiet;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                Input.Verified.Waweform = cmd.Waweform;
                Input.Verified.T3Max = cmd.T3Max;
                Input.Verified.T3Min = cmd.T3Min;
                Input.Verified.T3Sweep = cmd.T3Sweep;
                res &= (cmd.AttenCoef >= 0U && cmd.AttenCoef <= 255U);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DAC == 32768U);
                if(res) Input.Verified.DAC = cmd.DAC;
                res &= ((cmd.DOUT.ByteValue & 0x03) == 0U || (cmd.DOUT.ByteValue & 0x03) == 2U);
                res &= (cmd.DOUT.ByteValue == holding.DOUT.ByteValue);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool quiet2sweepVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.Sweep;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                res &= (cmd.Waweform >= 0U && cmd.Waweform <= 511U);
                if(res) Input.Verified.Waweform = cmd.Waweform;
                res &= (cmd.T3Max >= 800U && cmd.T3Max <= 1600U);
                if(res) Input.Verified.T3Max = cmd.T3Max;
                res &= (cmd.T3Min >= 800U && cmd.T3Min <= 1600U && cmd.T3Min < cmd.T3Max);
                if(res) Input.Verified.T3Min = cmd.T3Min;
                res &= (cmd.T3Sweep >= 10U && cmd.T3Sweep <= 1250U);
                if(res) Input.Verified.T3Sweep = cmd.T3Sweep;
                res &= (cmd.AttenCoef >= 0U && cmd.AttenCoef <= 255U);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DAC == 32768U && holding.DAC == 32768U);
                if(res) Input.Verified.DAC = cmd.DAC;
                res &= ((cmd.DOUT.ByteValue & 0x03) == 0U || (cmd.DOUT.ByteValue & 0x03) == 2U);
                res &= (cmd.DOUT.ByteValue == holding.DOUT.ByteValue);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool sweepVerify(ModbusHolding cmd)
        {
            bool res = cmd.Mode == (word)GenModes.Sweep;

            if(res)
            {
                Input.Verified.Mode = cmd.Mode;
                res &= (cmd.Waweform == holding.Waweform);
                if(res) Input.Verified.Waweform = cmd.Waweform;
                res &= (cmd.T3Max == holding.T3Max);
                if(res) Input.Verified.T3Max = cmd.T3Max;
                res &= (cmd.T3Min == holding.T3Min);
                if(res) Input.Verified.T3Min = cmd.T3Min;
                res &= (cmd.T3Sweep == holding.T3Sweep);
                if(res) Input.Verified.T3Sweep = cmd.T3Sweep;
                res &= (cmd.AttenCoef >= 0U && cmd.AttenCoef <= 255U);
                if(cmd.DOUT[1]) res &= (Math.Abs(holding.AttenCoef - cmd.AttenCoef) <= 32);
                if(res) Input.Verified.AttenCoef = cmd.AttenCoef;
                res &= (cmd.DAC == 32768U);
                if(res) Input.Verified.DAC = cmd.DAC;
                res &= ((cmd.DOUT.ByteValue & 0x03) == 0U || (cmd.DOUT.ByteValue & 0x03) == 2U);
                res &= (cmd.DOUT.ByteValue == holding.DOUT.ByteValue);
                if(res) Input.Verified.DOUT = cmd.DOUT;
            }
            return res;
        }

        private bool sweep2quietVerify(ModbusHolding cmd) { return nosweep2quietVerify(cmd); }
        #endregion
    }
    #endregion
}
