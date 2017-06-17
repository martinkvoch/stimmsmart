using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Windows.Forms;
using dword = System.UInt32;
using word = System.UInt16;

namespace LANlib
{
    public enum ErrStatus : byte { OK, InvalidAddr, InvalidCmd, RS485Timeout, InvalidResponse, CommonError, NotVerified, NoAction };

    public enum Power { Off, On };

    public static class LAN
    {
        private const word listenPort = 5000;
        private static IPEndPoint recvEP = new IPEndPoint(IPAddress.Any, listenPort);

        [DefaultValue(false)]
        public static bool TimedOut { get; private set; }

        #region MasterIP
        //private static string masterIP = "127.0.0.1";
        //[DefaultValue("127.0.0.1")]
        //public static string MasterIP
        //{
        //    get { return masterIP; }
        //    set { masterIP = value; }
        //}
        #endregion

        #region SlaveIP
        //private static string slaveIP = "192.168.0.99";
        private static string slaveIP;
        //[DefaultValue("127.0.0.1")]
        public static string SlaveIP
        {
            get
            {
                if(string.IsNullOrEmpty(slaveIP)) slaveIP = "10.10.18.3";
                return slaveIP;
            }
            set { slaveIP = value; }
        }
        #endregion

        #region UdpClient
        private static UdpClient _local;
        private static UdpClient local
        {
            get
            {
                if(_local == null) _local = new UdpClient(recvEP);
                return _local;
            }
            set
            {
                if(value == null && _local != null)
                {
                    _local.Close();
                    _local = null;
                }
            }
        }
        #endregion

        [DefaultValue(null)]
        public static Func<QueryDG, ResponseDG> CallBack { get; set; }

        #region MasterCmd
        /// <summary>
        /// Proces odeslání dotazového datagramu směrem na desku LAN a získání odpovědního datagramu z LAN.
        /// V případě chybné komunikace vrací chybový datagram.
        /// </summary>
        /// <param name="cmd">dotazový datagram</param>
        /// <returns>Vrací odpovědní datagram přijatý z LAN</returns>
        public static ResponseDG MasterCmd(QueryDG cmd)
        {
            ResponseDG res = GetResponse(cmd);

            try
            {
                using(UdpClient client = new UdpClient())
                {
                    byte[] buf = cmd.Datagram;
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(SlaveIP), listenPort);

                    client.Client.ReceiveTimeout = 5000;
                    client.Client.SendTimeout = 5000;
                    client.Send(buf, buf.Length, ep);
                    res = ResponseDG.FromBytes(client.Receive(ref ep));
                    TimedOut = false;
                }
            }
            catch(SocketException e)
            {
                ModbusInput input = new ModbusInput();

                TimedOut = e.SocketErrorCode == SocketError.TimedOut;
                input.Holding.Mode = (word)e.ErrorCode;
                input.Holding.Waweform = (word)e.NativeErrorCode;
                input.Holding.T3Max = (word)e.SocketErrorCode;
                res = GetResponse(cmd, ErrStatus.InvalidResponse, input: input);
            }
            catch(Exception e)
            {
                ModbusInput input = new ModbusInput();

                input.Holding.Mode = (word)(e.HResult >> 16);
                input.Holding.Waweform = (word)(e.HResult & 0x0000FFFF);
                res = GetResponse(cmd, ErrStatus.CommonError, input: input);
            }
            return res;
        }
        #endregion

        #region GetResponse
        public static ResponseDG GetResponse(QueryDG cmd, ErrStatus status = ErrStatus.NoAction, byte dio = 0, ModbusInput input = null) { return new ResponseDG(cmd, status, dio, input); }
        #endregion

        #region SlaveAns(), incomingDG(), processCmd()
        /// <summary>
        /// Zahájení smyčky komunikace mezi řídícím zařízením (PC) a generátorem.
        /// Komunikace probíhá v asynchronním režimu - podřízené zařízení neustále naslouchá a okamžitě zpracuje každý příchozí paket (dotaz).
        /// </summary>
        public static void SlaveAns()
        {
            //local = null;
            local.BeginReceive(new AsyncCallback(incomingDG), recvEP);
        }

        /// <summary>
        /// Metoda volaná v okamžiku přijetí dotazu od nadřízeného zařízení.
        /// </summary>
        /// <param name="ar">stav přijatého dotazu</param>
        private static void incomingDG(IAsyncResult ar)
        {
            QueryDG rcvQuery = new QueryDG();
            byte[] rcv = new QueryDG().Datagram, snd = new ResponseDG().Datagram;

            try
            {
                rcv = local.EndReceive(ar, ref recvEP);
                rcvQuery = QueryDG.FromBytes(rcv);
                snd = processCmd(rcvQuery).Datagram;

                local.Send(snd, snd.Length, recvEP);
                SlaveAns();
            }
            catch(SocketException e)
            {
                ModbusInput input = new ModbusInput();

                input.Holding.Mode = (word)e.ErrorCode;
                input.Holding.Waweform = (word)e.NativeErrorCode;
                input.Holding.T3Max = (word)e.SocketErrorCode;
                snd = GetResponse(rcvQuery, ErrStatus.InvalidResponse, input: input).Datagram;
                local.Send(snd, snd.Length, recvEP);
                SlaveAns();
            }
            catch(Exception e)
            {
                ModbusInput input = new ModbusInput();

                input.Holding.Mode = (word)(e.HResult >> 16);
                input.Holding.Waweform = (word)(e.HResult & 0x0000FFFF);
                snd = GetResponse(rcvQuery, ErrStatus.CommonError, input: input).Datagram;
                local.Send(snd, snd.Length, recvEP);
                SlaveAns();
            }
        }

        /// <summary>
        /// Vstupní bod zpracování přijatého dotazu a vytvoření odpovědi.
        /// </summary>
        /// <param name="cmd">dotaz od nadřízeného zařízení</param>
        /// <returns>Vrací odpověď formulovanou podřízeným zařízením.</returns>
        private static ResponseDG processCmd(QueryDG cmd)
        {
            ResponseDG res = GetResponse(cmd);

            if(CallBack != null) res = CallBack(cmd);
            return res;
        }
        #endregion
    }
}
