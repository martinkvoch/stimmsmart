using System.Linq;

namespace LANlib
{
    public enum QueryCmd : byte { CmdWr, CmdRd }

    /// <summary>
    /// Implementace UDP paketu pro dotaz řídícího PC do MDM
    /// </summary>
    public class QueryDG
    {
        private const byte NOC = 6; // max. počet kanálů
        private ModbusHolding modbusR;
        private byte packetNum, address, dioWR;
        private QueryCmd command;
        public byte ProtocolNum { get { return 22; } }

        #region Datagram
        /// <summary>
        /// Bytová forma UDP paketu
        /// </summary>
        public byte[] Datagram
        {
            get
            {
                byte[] res = new byte[6];

                res[0] = ProtocolNum;
                res[1] = packetNum;
                res[2] = address;
                res[3] = dioWR;
                res[4] = (byte)command;
                res[5] = 0;
                res = res.Concat(modbusR.ToBytes()).ToArray();
                return res;
            }
        }
        #endregion

        #region Vlastnosti
        public byte PacketNum
        {
            get { return packetNum; }
            set { packetNum = value; }
        }

        public byte Address
        {
            get { return address; }
            set { address = value; }
        }

        public byte DioWR
        {
            get { return dioWR; }
            set { dioWR = value; }
        }

        public QueryCmd Command
        {
            get { return command; }
            set { command = value; }
        }

        public ModbusHolding HoldingR
        {
            get { return modbusR ?? (modbusR = new ModbusHolding()); }
            set { modbusR = value ?? new ModbusHolding(); }
        }
        #endregion

        public QueryDG(byte pckNum = 0, byte addr = 0, QueryCmd cmd = QueryCmd.CmdWr, byte led = 0, ModbusHolding modbus = null)
        {
            PacketNum = pckNum;
            Address = addr;// Math.Max((byte)0, Math.Min(addr, NOC));
            Command = cmd;
            //Bits dio = new Bits(led);
            //DioWR = dio.ByteValue;
            DioWR = led;
            modbusR = modbus ?? new ModbusHolding();
        }

        #region FromBytes()
        /// <summary>
        /// Zkonstruuje instanci třídy QueryDG ze zadaného pole bytů
        /// </summary>
        /// <param name="dgram">pole bytů</param>
        /// <returns>Vrací instanci třídy QueryDG</returns>
        public static QueryDG FromBytes(byte[] dgram)
        {
            QueryDG res = new QueryDG(dgram[1], dgram[2], (QueryCmd)dgram[4], (byte)(dgram[3] & Helper.BDioLedMask)/*, new Bits(dgram[3])[DioReg.Reset], new Bits(dgram[3])[DioReg.OnOff]*/, ModbusHolding.FromBytes(dgram.Skip(6).ToArray()));

            return res;
        }
        #endregion
    }
}
