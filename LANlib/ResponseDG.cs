using System.Linq;

namespace LANlib
{
    /// <summary>
    /// Implementace UDP paketu odeslaného z MDM jako odpověď
    /// </summary>
    public class ResponseDG
    {
        private byte protNum, packetNum, address, dioRD, status;
        private QueryCmd command;
        private ModbusInput modbusR;

        #region Datagram
        /// <summary>
        /// Bytová forma UDP paketu
        /// </summary>
        public byte[] Datagram
        {
            get
            {
                byte[] res = new byte[6];

                res[0] = protNum;
                res[1] = packetNum;
                res[2] = address;
                res[3] = dioRD;
                res[4] = (byte)command;
                res[5] = status;
                res = res.Concat(modbusR.ToBytes()).ToArray();
                return res;
            }
        }
        #endregion

        #region Vlastnosti
        public byte ProtNum
        {
            get { return protNum; }
            set { protNum = value; }
        }

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

        public byte DioRD
        {
            get { return dioRD; }
            set { dioRD = value; }
        }

        public QueryCmd Command
        {
            get { return command; }
            set { command = value; }
        }

        public byte Status
        {
            get { return status; }
            set { status = value; }
        }

        public ModbusInput InputR
        {
            get { return modbusR ?? (modbusR = new ModbusInput()); }
            set { modbusR = value ?? new ModbusInput(); }
        }
        #endregion

        public ResponseDG(byte protNum = 22, byte pckNum = 0, byte addr = 0, byte dio = 0, byte cmd = 0, byte status = 0, ModbusInput modbus = null)
        {
            ProtNum = protNum;
            PacketNum = pckNum;
            Address = addr;
            DioRD = dio;
            Command = (QueryCmd)cmd;
            Status = status;
            modbusR = modbus ?? new ModbusInput();
        }

        public ResponseDG(QueryDG cmd, ErrStatus status, byte dio = 0, ModbusInput input = null) : this(cmd.ProtocolNum, cmd.PacketNum, cmd.Address, dio, (byte)cmd.Command, (byte)status, input ?? new ModbusInput()) { }

        #region FromBytes()
        /// <summary>
        /// Zkonstruuje instanci třídy ResponseDG ze zadaného pole bytů
        /// </summary>
        /// <param name="dgram">pole bytů</param>
        /// <returns>Vrací instanci třídy ResponseDG</returns>
        public static ResponseDG FromBytes(byte[] dgram)
        {
            ResponseDG res = new ResponseDG(dgram[0], dgram[1], dgram[2], dgram[3], dgram[4], dgram[5], ModbusInput.FromBytes(dgram.Skip(6).ToArray()));

            return res;
        }
        #endregion
    }
}
