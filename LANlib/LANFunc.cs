﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using word = System.UInt16;
using dword = System.UInt32;
using System.Threading;

namespace LANlib
{
    public static class LANFunc
    {
        static dword pck = 0U;

        /// <summary>
        /// Obsluha LAN převodníku.
        /// Zapínání a vypínání kanálů.
        /// </summary>
        /// <param name="dio">řídící bity LED diody na LAN převodníku</param>
        /// <returns>Návratový UDP paket z přístroje</returns>
        public static ResponseDG Lan(byte dio)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), led: dio);
            ResponseDG res = LAN.MasterCmd(q);

            Thread.Sleep(100);
            res = LanRd();
            return res;
        }

        public static void LanChOnOff(byte chnum, bool on = true)
        {
            byte dio;
            Bits chDio;
            ResponseDG res = LanRd();

            dio = res.DioRD;
            chDio = new Bits(dio);
            chDio[chnum - 1] = on;
            Lan(dio);
        }

        public static ResponseDG LanRd()
        {
            return ChRd(0);
        }

        /// <summary>
        /// Čtení MODBUS kanálu
        /// </summary>
        /// <param name="chnum">číslo kanálu</param>
        /// <returns></returns>
        public static ResponseDG ChRd(byte chnum)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), chnum);
            ResponseDG res = new ResponseDG();

            q.Command = QueryCmd.CmdRd;
            res = LAN.MasterCmd(q);
            return res;
        }

        //private static ResponseDG chOnOff(byte chnum, bool onoff = true)
        //{
        //    QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF));
        //    ResponseDG res = ChRd(chnum);
        //    Bits diowr = new Bits(res.DioRD);

        //    diowr[DioReg.LedBlink] = onoff;
        //    q.DioWR = diowr.ByteValue;
        //    res = LAN.MasterCmd(q);
        //    return res;
        //}

        //public static ResponseDG ChOn(byte chnum) { return chOnOff(chnum, true); }

        //public static ResponseDG ChOff(byte chnum) { return chOnOff(chnum, false); }

        public static ResponseDG ChRst(byte chnum)
        {
            ResponseDG res;
            QueryDG q = new QueryDG((byte)pck++, chnum);
            Bits diowr = new Bits();

            diowr[DioReg.LedR] = true;
            diowr[DioReg.LedNBlink] = true;
            q.DioWR = diowr.ByteValue;
            res = LAN.MasterCmd(q);
            Thread.Sleep(100);
            return res;
        }

        public static ResponseDG ChDio(byte chnum, byte dio)
        {
            QueryDG q = new QueryDG((byte)pck++, chnum, led: dio);
            ResponseDG res = ChRd(chnum);
            Bits diowr = new Bits(dio);

            q.HoldingR = res.InputR.Verified;
            q.DioWR = diowr.ByteValue;
            res = LAN.MasterCmd(q);
            Thread.Sleep(100);
            res = ChRd(chnum);
            return res;
        }

        public static ResponseDG ChDAC(byte chnum, word dac = 32768)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), chnum);
            ResponseDG res = ChRd(chnum);

            q.DioWR = res.DioRD;
            q.HoldingR = res.InputR.Verified;
            q.HoldingR.DAC = dac;
            res = LAN.MasterCmd(q);
            Thread.Sleep(100);
            res = ChRd(chnum);
            return res;
        }

        public static ResponseDG ChDOUT(byte chnum, byte dout = 0)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), chnum);
            ResponseDG res = ChRd(chnum);

            dout = (byte)(dout & 0x03);
            q.DioWR = res.DioRD;
            q.HoldingR = res.InputR.Verified;
            if(dout > 0) q.HoldingR.DAC = 32768;
            q.HoldingR.DOUT = new Bits(dout);
            res = LAN.MasterCmd(q);
            Thread.Sleep(100);
            res = ChRd(chnum);
            return res;
        }

        public static ResponseDG ChAtCf(byte chnum, byte acf = 0)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), chnum);
            ResponseDG res = ChRd(chnum);

            q.DioWR = res.DioRD;
            q.HoldingR = res.InputR.Verified;
            q.HoldingR.AttenCoef = acf;
            res = LAN.MasterCmd(q);
            Thread.Sleep(200);
            res = ChRd(chnum);
            return res;
        }

        public static ResponseDG ChMode(byte chnum, byte mode = 0, word? shape = null, word? t3max = null, word? t3min = null, word? t3sweep = null, byte? acf = null)
        {
            QueryDG q = new QueryDG((byte)(pck++ & 0x000000FF), chnum);
            ResponseDG res = ChRd(chnum);

            q.DioWR = res.DioRD;
            q.HoldingR = res.InputR.Verified;
            q.HoldingR.Mode = mode;
            if(shape.HasValue) q.HoldingR.Waweform = shape.Value;
            if(t3max.HasValue) q.HoldingR.T3Max = t3max.Value;
            if(t3min.HasValue) q.HoldingR.T3Min = t3min.Value;
            if(t3sweep.HasValue) q.HoldingR.T3Sweep = t3sweep.Value;
            if(acf.HasValue) q.HoldingR.AttenCoef = acf.Value;
            res = LAN.MasterCmd(q);
            Thread.Sleep(100);
            res = ChRd(chnum);
            return res;
        }
    }
}
