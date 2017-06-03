using System;
using System.Linq;
using word = System.UInt16;
using dword = System.UInt32;

using LANlib;

namespace MDMcom
{
    enum Commands { lan, ch, help, exit }

    static class Program
    {
        //static dword pck = 0U;
        //static byte lanDio = 0;
        static byte[] chDio = new byte[] { 0, 0, 0, 0, 0, 0 };
        static ModbusHolding[] holding = new LANlib.ModbusHolding[] { new ModbusHolding(), new ModbusHolding(), new ModbusHolding(), new ModbusHolding(), new ModbusHolding(), new ModbusHolding() };

        private static bool isNumber(string n)
        {
            bool res = false;
            word w;

            res = word.TryParse(n, out w);
            return res;
        }

        private static void help()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  lan <dio>");
            Console.WriteLine("  ch <num> {rd|<dio>}");
            //Console.WriteLine("  ch <num> {on|off|rst|rd|dac|dac0|<dio>}");
            Console.WriteLine("  ch <num> dac [<num>]   (default: 32768)");
            Console.WriteLine("  ch <num> dout <num>");
            Console.WriteLine("  ch <num> acf <num>");
            Console.WriteLine("  ch <num> mode <shape> <t3max> <t3min> <t3sweep> <acf>");
        }

        static void Main(string[] args)
        {
            string cmd = string.Empty;

            Console.WriteLine("MDMcom -- MDM command testing");
            while(!cmd.Equals(Commands.exit.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("COM> ");
                cmd = Console.ReadLine();
                if(cmd.Split(' ')[0].Equals(Commands.ch.ToString(), StringComparison.OrdinalIgnoreCase)) channel(cmd.Split(' ').Skip(1).ToArray());
                else if(cmd.Split(' ')[0].Equals(Commands.lan.ToString(), StringComparison.OrdinalIgnoreCase)) lan(cmd.Split(' ').Skip(1).ToArray());
                else if(cmd.Equals(Commands.help.ToString(), StringComparison.OrdinalIgnoreCase)) help();
                //else if(cmd.Equals(Commands.exit.ToString(), StringComparison.OrdinalIgnoreCase)) exit();
            }
        }

        static void lan(string[] cmd)
        {
            ResponseDG r = new ResponseDG();

            if(isNumber(cmd[0])) r = LANFunc.Lan((byte)(Convert.ToUInt32(cmd[0]) & 0xFF));
            Console.WriteLine(ansReport(r));
        }

        static void channel(string[] cmd)
        {
            ResponseDG r = new ResponseDG();
            byte chNum = (byte)(isNumber(cmd[0]) ? Convert.ToUInt32(cmd[0]) : 1);

            if(chNum >= 1 && chNum <= 6)
            {
                if(isNumber(cmd[1]))
                {
                    //bool reset = false, off = false;

                    //if(cmd.Length > 2 && isNumber(cmd[2])) reset = Convert.ToUInt16(cmd[2]) > 0;
                    //if(cmd.Length > 3 && isNumber(cmd[3])) off = Convert.ToUInt16(cmd[3]) > 0;
                    //r = LANFunc.ChDio(chNum, (byte)(Convert.ToUInt32(cmd[1]) & 0xFF), reset, off);
                    r = LANFunc.ChDio(chNum, Convert.ToByte(cmd[1]));
                }
                //else if(cmd[1].Equals("on", StringComparison.OrdinalIgnoreCase)) r = LANFunc.ChOn(chNum);
                //else if(cmd[1].Equals("off", StringComparison.OrdinalIgnoreCase)) r = LANFunc.ChOff(chNum);
                //else if(cmd[1].Equals("rst", StringComparison.OrdinalIgnoreCase)) r = LANFunc.ChRst(chNum);
                else if(cmd[1].Equals("rd", StringComparison.OrdinalIgnoreCase)) r = LANFunc.ChRd(chNum);
                //else if(cmd[1].Equals("dac0", StringComparison.OrdinalIgnoreCase)) r = LANFunc.ChDAC(chNum, 0);
                else if(cmd[1].Equals("dac", StringComparison.OrdinalIgnoreCase))
                {
                    word dac = cmd.Length > 2 && isNumber(cmd[2]) ? Convert.ToByte(cmd[2]) : (word)32768;

                    r = LANFunc.ChDAC(chNum, dac);
                }
                else if(cmd[1].Equals("dout", StringComparison.OrdinalIgnoreCase) && cmd.Length > 2 && isNumber(cmd[2])) r = LANFunc.ChDOUT(chNum, Convert.ToByte(cmd[2]));
                else if(cmd[1].Equals("acf", StringComparison.OrdinalIgnoreCase) && cmd.Length > 2 && isNumber(cmd[2])) r = LANFunc.ChAcf(chNum, Convert.ToByte(cmd[2]));
                else if(cmd[1].Equals("mode", StringComparison.OrdinalIgnoreCase) && cmd.Length > 2 && isNumber(cmd[2]))
                {
                    byte mode = Convert.ToByte(cmd[2]);
                    byte? acf = null;
                    word? shape = null, t3max = null, t3min = null, t3sweep = null;

                    if(cmd.Length > 3 && isNumber(cmd[3])) shape = Convert.ToUInt16(cmd[3]);
                    if(cmd.Length > 4 && isNumber(cmd[4])) t3max = Convert.ToUInt16(cmd[4]);
                    if(cmd.Length > 5 && isNumber(cmd[5])) t3min = Convert.ToUInt16(cmd[5]);
                    if(cmd.Length > 6 && isNumber(cmd[6])) t3sweep = Convert.ToUInt16(cmd[6]);
                    if(cmd.Length > 7 && isNumber(cmd[7])) acf = Convert.ToByte(cmd[7]);
                    r = LANFunc.ChMode(chNum, mode, shape, t3max, t3min, t3sweep, acf);
                }
            }
            else
            {
                r.Address = chNum;
                r.Status = (byte)ErrStatus.InvalidAddr;
            }
            Console.WriteLine(ansReport(r));
        }

        static string ansReport(ResponseDG r)
        {
            string res = string.Empty;

            res += string.Format("Packet No.: {0:D3}", r.PacketNum) + Environment.NewLine;
            res += string.Format("Address:    {0:D3}", r.Address) + Environment.NewLine;
            res += string.Format("Command:    {0}", r.Command) + Environment.NewLine;
            res += string.Format("Status:     {0} (0x{1:X2}, {1})", (ErrStatus)r.Status, r.Status) + Environment.NewLine;
            res += string.Format("LED Status: 0x{0:X2}", r.DioRD) + Environment.NewLine;
            res += "Modbus Input:" + Environment.NewLine;
            res += string.Format("{0}", r.InputR) + Environment.NewLine;
            return res;
        }

        //static void exit()
        //{
        //}
    }
}
