using System;
using System.Data;
using System.Linq;
using word = System.UInt16;

using MDM.Properties;

namespace MDM.Data
{
    #region struct TProcSegment
    public struct TProcSegment
    {
        public int ProcID;
        public byte Order;
        public byte Duration;
        public word WaveShape;
        public word TMin;
        public word TMax;
        public word TSweep;

        public TProcSegment(int procID, byte order, byte duration, word waveShape, word tMin, word tMax, word tSweep)
        {
            ProcID = procID;
            Order = order;
            Duration = duration;
            WaveShape = waveShape;
            TMin = tMin;
            TMax = tMax;
            TSweep = tSweep;
        }
    }
    #endregion

    #region ProcSegment
    public class ProcSegment : MDMTable
    {
        private const string insFmt = "(PROC_ID, PROC_NUM, SEG_ORDER, DURATION, WAVE_SHAPE, T_MIN, T_MAX, T_SWEEP) values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})";
        internal const string TName = "PROC_SEGMENT";

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + TName);
            Database.ExecCmd(string.Format("create table " + TName + " (" +
                "ID integer primary key, " +
                "PROC_ID integer constraint PROCEDURE_FKEY references {0}(ID), " +
                "PROC_NUM byte not null default 1, " +
                "SEG_ORDER byte not null default 0, " +
                "DURATION byte not null, " +
                "WAVE_SHAPE unsigned smallint not null default 0, " +
                "T_MIN unsigned smallint not null default 0, " +
                "T_MAX unsigned smallint not null default 0, " +
                "T_SWEEP unsigned smallint not null default 0)", Procedure.TName));
            //Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(PROC_ID, PROC_NUM, SEG_ORDER)", TName));
        }
        #endregion

        internal void Insert(int procID, byte procNum, byte order, byte duration, word waveShape,/* word tMin, word tMax,*/ word tSweep)
        {
            Insert(string.Format(insFmt, procID, procNum, order, duration, waveShape, 887, 1144, tSweep));
        }

        public ProcSegment() : base(TName) { }
    }
    #endregion

    #region Procedure
    public class Procedure: MDMTable
    {
        private const string insFmt = "(SEX, SOMATOTYPE, AGE_FROM, AGE_TO) values ({0}, {1}, {2}, {3})", selProcFmt =
        "select s.PROC_ID, s.SEG_ORDER, s.DURATION, s.WAVE_SHAPE, s.T_MIN, s.T_MAX, s.T_SWEEP from {0} p, {1} s " +
        "where p.ID = s.PROC_ID " +
          "and SEX = (select SEX from {2} where ID = {3}) " +
          "and (select CURRENT_DATE - BIRTHDATE from {2} where ID = {3}) between AGE_FROM and AGE_TO " +
          "and SOMATOTYPE = (select SOMATOTYPE from {2} where ID = {3}) " +
          "and s.PROC_NUM = {4} " +
        "order by s.SEG_ORDER";

        internal const string TName = "PROCEDURE";

        public static TProcSegment[] GetSegments(int patientID, int procNum)
        {
            DataTable dt = Database.Select(string.Format(selProcFmt, TName, ProcSegment.TName, Patient.TName, patientID, procNum));
            TProcSegment[] res = new TProcSegment[0];

            if(dt != null && dt.Rows.Count > 0) res = dt.Rows.OfType<DataRow>()
               .Select(r => new TProcSegment(Convert.ToInt32(r["PROC_ID"]), Convert.ToByte(r["SEG_ORDER"]), Convert.ToByte(r["DURATION"]), Convert.ToUInt16(r["WAVE_SHAPE"]), Convert.ToUInt16(r["T_MIN"]), Convert.ToUInt16(r["T_MAX"]), Convert.ToUInt16(r["T_SWEEP"]))).ToArray();
            return res;
        }

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + ProcSegment.TName);
            Database.ExecCmd("drop table if exists " + TName);
            Database.ExecCmd(string.Format("create table " + TName + " (" +
                "ID integer primary key, " +
                "SEX byte not null default 0 check(SEX between {0} and {1}), " +
                "SOMATOTYPE byte not null default 0 check(SOMATOTYPE between {2} and {3}), " +
                "AGE_FROM byte not null, " +
                "AGE_TO byte not null)",
                (byte)Enum.GetValues(typeof(Sex)).Cast<Sex>().First(), (byte)Enum.GetValues(typeof(Sex)).Cast<Sex>().Last(),
                (byte)Enum.GetValues(typeof(Somatotype)).Cast<Somatotype>().First(), (byte)Enum.GetValues(typeof(Somatotype)).Cast<Somatotype>().Last()));
            //Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(SEX, SOMATOTYPE, AGE_FROM)", TName));
            ProcSegment.Init();
            using(Procedure proc = new Procedure()) using(ProcSegment seg = new ProcSegment())
            {
                int id;

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Normotype, 18, 55));
                seg.Insert(id, 1, 0, 15, 109, 832);
                seg.Insert(id, 1, 1, 15, 10, 832);
                seg.Insert(id, 2, 0, 15, 10, 832);
                seg.Insert(id, 2, 1, 15, 76, 832);
                seg.Insert(id, 3, 0, 15, 109, 832);
                seg.Insert(id, 3, 1, 15, 10, 832);
                seg.Insert(id, 4, 0, 15, 10, 832);
                seg.Insert(id, 4, 1, 15, 76, 832);
                seg.Insert(id, 5, 0, 15, 109, 832);
                seg.Insert(id, 5, 1, 15, 10, 832);
                seg.Insert(id, 6, 0, 15, 10, 832);
                seg.Insert(id, 6, 1, 15, 76, 832);
                seg.Insert(id, 7, 0, 15, 109, 832);
                seg.Insert(id, 7, 1, 15, 10, 832);
                seg.Insert(id, 8, 0, 15, 10, 832);
                seg.Insert(id, 8, 1, 15, 76, 832);
                seg.Insert(id, 9, 0, 5, 174, 1245);
                seg.Insert(id, 9, 1, 10, 76, 533);
                seg.Insert(id, 9, 2, 15, 10, 832);
                seg.Insert(id, 10, 0, 5, 174, 1245);
                seg.Insert(id, 10, 1, 10, 76, 533);
                seg.Insert(id, 10, 2, 15, 10, 817);
                seg.Insert(id, 11, 0, 5, 174, 1245);
                seg.Insert(id, 11, 1, 10, 76, 533);
                seg.Insert(id, 11, 2, 15, 10, 832);
                seg.Insert(id, 12, 0, 5, 174, 1245);
                seg.Insert(id, 12, 1, 10, 76, 533);
                seg.Insert(id, 12, 2, 15, 10, 817);
                seg.Insert(id, 13, 0, 5, 174, 1245);
                seg.Insert(id, 13, 1, 10, 76, 533);
                seg.Insert(id, 13, 2, 15, 10, 817);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Hypotype, 18, 55));
                seg.Insert(id, 1, 0, 10, 109, 510);
                seg.Insert(id, 1, 1, 20, 10, 510);
                seg.Insert(id, 2, 0, 10, 109, 510);
                seg.Insert(id, 2, 1, 20, 10, 510);
                seg.Insert(id, 3, 0, 10, 109, 510);
                seg.Insert(id, 3, 1, 20, 10, 510);
                seg.Insert(id, 4, 0, 10, 109, 510);
                seg.Insert(id, 4, 1, 20, 10, 510);
                seg.Insert(id, 5, 0, 10, 109, 510);
                seg.Insert(id, 5, 1, 20, 10, 510);
                seg.Insert(id, 6, 0, 10, 109, 510);
                seg.Insert(id, 6, 1, 20, 10, 510);
                seg.Insert(id, 7, 0, 15, 174, 778);
                seg.Insert(id, 7, 1, 15, 76, 778);
                seg.Insert(id, 8, 0, 15, 174, 778);
                seg.Insert(id, 8, 1, 15, 76, 811);
                seg.Insert(id, 9, 0, 15, 174, 778);
                seg.Insert(id, 9, 1, 15, 76, 778);
                seg.Insert(id, 10, 0, 15, 174, 817);
                seg.Insert(id, 10, 1, 15, 76, 817);
                seg.Insert(id, 11, 0, 10, 109, 533);
                seg.Insert(id, 11, 1, 20, 10, 533);
                seg.Insert(id, 12, 0, 10, 109, 533);
                seg.Insert(id, 12, 1, 20, 10, 533);
                seg.Insert(id, 13, 0, 10, 109, 533);
                seg.Insert(id, 13, 1, 20, 10, 533);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Hyperergic, 18, 55));
                seg.Insert(id, 1, 0, 15, 109, 795);
                seg.Insert(id, 1, 1, 15, 10, 795);
                seg.Insert(id, 2, 0, 15, 10, 795);
                seg.Insert(id, 2, 1, 15, 76, 795);
                seg.Insert(id, 3, 0, 15, 109, 795);
                seg.Insert(id, 3, 1, 15, 10, 795);
                seg.Insert(id, 4, 0, 15, 10, 795);
                seg.Insert(id, 4, 1, 15, 76, 795);
                seg.Insert(id, 5, 0, 15, 109, 795);
                seg.Insert(id, 5, 1, 15, 10, 795);
                seg.Insert(id, 6, 0, 15, 10, 795);
                seg.Insert(id, 6, 1, 15, 76, 795);
                seg.Insert(id, 7, 0, 15, 174, 817);
                seg.Insert(id, 7, 1, 15, 76, 817);
                seg.Insert(id, 8, 0, 15, 174, 817);
                seg.Insert(id, 8, 1, 15, 76, 817);
                seg.Insert(id, 9, 0, 15, 174, 795);
                seg.Insert(id, 9, 1, 15, 76, 795);
                seg.Insert(id, 10, 0, 15, 174, 817);
                seg.Insert(id, 10, 1, 15, 76, 817);
                seg.Insert(id, 11, 0, 15, 109, 838);
                seg.Insert(id, 11, 1, 15, 10, 838);
                seg.Insert(id, 12, 0, 15, 174, 817);
                seg.Insert(id, 12, 1, 15, 76, 817);
                seg.Insert(id, 13, 0, 15, 174, 817);
                seg.Insert(id, 13, 1, 15, 76, 817);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Normotype, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 20, 109, 533);
                seg.Insert(id, 1, 1, 10, 10, 533);
                seg.Insert(id, 2, 0, 15, 76, 832);
                seg.Insert(id, 2, 1, 15, 10, 832);
                seg.Insert(id, 3, 0, 20, 109, 533);
                seg.Insert(id, 3, 1, 10, 10, 533);
                seg.Insert(id, 4, 0, 15, 76, 832);
                seg.Insert(id, 4, 1, 15, 10, 832);
                seg.Insert(id, 5, 0, 20, 109, 550);
                seg.Insert(id, 5, 1, 10, 10, 533);
                seg.Insert(id, 6, 0, 15, 76, 832);
                seg.Insert(id, 6, 1, 15, 10, 832);
                seg.Insert(id, 7, 0, 20, 109, 550);
                seg.Insert(id, 7, 1, 10, 10, 533);
                seg.Insert(id, 8, 0, 15, 76, 832);
                seg.Insert(id, 8, 1, 15, 10, 832);
                seg.Insert(id, 9, 0, 5, 273, 1287);
                seg.Insert(id, 9, 1, 15, 76, 832);
                seg.Insert(id, 9, 2, 10, 10, 533);
                seg.Insert(id, 10, 0, 5, 273, 1287);
                seg.Insert(id, 10, 1, 15, 76, 832);
                seg.Insert(id, 10, 2, 10, 10, 533);
                seg.Insert(id, 11, 0, 5, 273, 1271);
                seg.Insert(id, 11, 1, 15, 76, 832);
                seg.Insert(id, 11, 2, 10, 10, 533);
                seg.Insert(id, 12, 0, 5, 273, 1287);
                seg.Insert(id, 12, 1, 15, 76, 832);
                seg.Insert(id, 12, 2, 10, 10, 533);
                seg.Insert(id, 13, 0, 5, 273, 1287);
                seg.Insert(id, 13, 1, 15, 76, 832);
                seg.Insert(id, 13, 2, 10, 10, 533);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Hypotype, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 10, 76, 533);
                seg.Insert(id, 1, 1, 20, 10, 533);
                seg.Insert(id, 2, 0, 20, 109, 533);
                seg.Insert(id, 2, 1, 10, 273, 533);
                seg.Insert(id, 3, 0, 10, 76, 533);
                seg.Insert(id, 3, 1, 20, 10, 533);
                seg.Insert(id, 4, 0, 20, 109, 533);
                seg.Insert(id, 4, 1, 10, 273, 533);
                seg.Insert(id, 5, 0, 10, 76, 533);
                seg.Insert(id, 5, 1, 20, 10, 533);
                seg.Insert(id, 6, 0, 20, 109, 533);
                seg.Insert(id, 6, 1, 10, 273, 533);
                seg.Insert(id, 7, 0, 20, 109, 533);
                seg.Insert(id, 7, 1, 10, 273, 533);
                seg.Insert(id, 8, 0, 10, 76, 533);
                seg.Insert(id, 8, 1, 20, 10, 533);
                seg.Insert(id, 9, 0, 20, 109, 533);
                seg.Insert(id, 9, 1, 10, 273, 533);
                seg.Insert(id, 10, 0, 10, 76, 533);
                seg.Insert(id, 10, 1, 20, 10, 533);
                seg.Insert(id, 11, 0, 20, 109, 533);
                seg.Insert(id, 11, 1, 10, 273, 533);
                seg.Insert(id, 12, 0, 10, 76, 533);
                seg.Insert(id, 12, 1, 20, 10, 533);
                seg.Insert(id, 13, 0, 20, 109, 533);
                seg.Insert(id, 13, 1, 10, 273, 533);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Female, (byte)Somatotype.Hyperergic, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 20, 109, 817);
                seg.Insert(id, 1, 1, 10, 10, 817);
                seg.Insert(id, 2, 0, 10, 273, 817);
                seg.Insert(id, 2, 1, 20, 76, 817);
                seg.Insert(id, 3, 0, 20, 109, 817);
                seg.Insert(id, 3, 1, 10, 10, 817);
                seg.Insert(id, 4, 0, 10, 273, 817);
                seg.Insert(id, 4, 1, 20, 76, 817);
                seg.Insert(id, 5, 0, 20, 109, 817);
                seg.Insert(id, 5, 1, 10, 10, 817);
                seg.Insert(id, 6, 0, 10, 273, 817);
                seg.Insert(id, 6, 1, 20, 76, 817);
                seg.Insert(id, 7, 0, 20, 109, 817);
                seg.Insert(id, 7, 1, 10, 10, 817);
                seg.Insert(id, 8, 0, 10, 109, 817);
                seg.Insert(id, 8, 1, 20, 10, 817);
                seg.Insert(id, 9, 0, 20, 109, 817);
                seg.Insert(id, 9, 1, 10, 10, 817);
                seg.Insert(id, 10, 0, 10, 109, 817);
                seg.Insert(id, 10, 1, 20, 10, 817);
                seg.Insert(id, 11, 0, 20, 273, 817);
                seg.Insert(id, 11, 1, 10, 76, 817);
                seg.Insert(id, 12, 0, 10, 273, 817);
                seg.Insert(id, 12, 1, 20, 76, 817);
                seg.Insert(id, 13, 0, 20, 109, 817);
                seg.Insert(id, 13, 1, 10, 10, 817);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Normotype, 18, 55));
                seg.Insert(id, 1, 0, 15, 174, 778);
                seg.Insert(id, 1, 1, 15, 273, 778);
                seg.Insert(id, 2, 0, 15, 174, 778);
                seg.Insert(id, 2, 1, 15, 273, 778);
                seg.Insert(id, 3, 0, 15, 174, 778);
                seg.Insert(id, 3, 1, 15, 273, 778);
                seg.Insert(id, 4, 0, 15, 109, 778);
                seg.Insert(id, 4, 1, 15, 10, 778);
                seg.Insert(id, 5, 0, 15, 109, 778);
                seg.Insert(id, 5, 1, 15, 10, 778);
                seg.Insert(id, 6, 0, 15, 109, 778);
                seg.Insert(id, 6, 1, 15, 10, 778);
                seg.Insert(id, 7, 0, 15, 109, 778);
                seg.Insert(id, 7, 1, 15, 10, 778);
                seg.Insert(id, 8, 0, 15, 174, 778);
                seg.Insert(id, 8, 1, 15, 273, 778);
                seg.Insert(id, 9, 0, 15, 174, 778);
                seg.Insert(id, 9, 1, 15, 273, 778);
                seg.Insert(id, 10, 0, 15, 174, 778);
                seg.Insert(id, 10, 1, 15, 273, 778);
                seg.Insert(id, 11, 0, 15, 109, 778);
                seg.Insert(id, 11, 1, 15, 10, 778);
                seg.Insert(id, 12, 0, 15, 109, 778);
                seg.Insert(id, 12, 1, 15, 10, 778);
                seg.Insert(id, 13, 0, 15, 109, 778);
                seg.Insert(id, 13, 1, 15, 10, 778);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Hypotype, 18, 55));
                seg.Insert(id, 1, 0, 15, 109, 800);
                seg.Insert(id, 1, 1, 15, 10, 800);
                seg.Insert(id, 2, 0, 15, 109, 800);
                seg.Insert(id, 2, 1, 15, 10, 800);
                seg.Insert(id, 3, 0, 15, 109, 800);
                seg.Insert(id, 3, 1, 15, 10, 800);
                seg.Insert(id, 4, 0, 15, 273, 800);
                seg.Insert(id, 4, 1, 15, 76, 800);
                seg.Insert(id, 5, 0, 15, 273, 800);
                seg.Insert(id, 5, 1, 15, 76, 800);
                seg.Insert(id, 6, 0, 15, 273, 800);
                seg.Insert(id, 6, 1, 15, 76, 800);
                seg.Insert(id, 7, 0, 15, 273, 800);
                seg.Insert(id, 7, 1, 15, 76, 800);
                seg.Insert(id, 8, 0, 15, 109, 800);
                seg.Insert(id, 8, 1, 15, 10, 800);
                seg.Insert(id, 9, 0, 15, 109, 800);
                seg.Insert(id, 9, 1, 15, 10, 800);
                seg.Insert(id, 10, 0, 15, 109, 800);
                seg.Insert(id, 10, 1, 15, 10, 800);
                seg.Insert(id, 11, 0, 15, 273, 800);
                seg.Insert(id, 11, 1, 15, 76, 800);
                seg.Insert(id, 12, 0, 15, 273, 800);
                seg.Insert(id, 12, 1, 15, 76, 800);
                seg.Insert(id, 13, 0, 15, 273, 800);
                seg.Insert(id, 13, 1, 15, 76, 800);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Hyperergic, 18, 55));
                seg.Insert(id, 1, 0, 10, 273, 510);
                seg.Insert(id, 1, 1, 10, 174, 510);
                seg.Insert(id, 1, 2, 10, 109, 510);
                seg.Insert(id, 2, 0, 10, 273, 510);
                seg.Insert(id, 2, 1, 10, 174, 510);
                seg.Insert(id, 2, 2, 10, 109, 510);
                seg.Insert(id, 3, 0, 10, 273, 510);
                seg.Insert(id, 3, 1, 10, 174, 510);
                seg.Insert(id, 3, 2, 10, 109, 510);
                seg.Insert(id, 4, 0, 10, 273, 510);
                seg.Insert(id, 4, 1, 10, 174, 510);
                seg.Insert(id, 4, 2, 10, 109, 510);
                seg.Insert(id, 5, 0, 10, 273, 510);
                seg.Insert(id, 5, 1, 10, 174, 510);
                seg.Insert(id, 5, 2, 10, 109, 510);
                seg.Insert(id, 6, 0, 10, 273, 510);
                seg.Insert(id, 6, 1, 10, 174, 510);
                seg.Insert(id, 6, 2, 10, 109, 510);
                seg.Insert(id, 7, 0, 10, 109, 510);
                seg.Insert(id, 7, 1, 10, 10, 510);
                seg.Insert(id, 7, 2, 10, 76, 510);
                seg.Insert(id, 8, 0, 10, 109, 510);
                seg.Insert(id, 8, 1, 10, 10, 510);
                seg.Insert(id, 8, 2, 10, 76, 510);
                seg.Insert(id, 9, 0, 10, 109, 510);
                seg.Insert(id, 9, 1, 10, 10, 510);
                seg.Insert(id, 9, 2, 10, 76, 510);
                seg.Insert(id, 10, 0, 10, 273, 510);
                seg.Insert(id, 10, 1, 10, 174, 510);
                seg.Insert(id, 10, 2, 10, 109, 510);
                seg.Insert(id, 11, 0, 10, 273, 510);
                seg.Insert(id, 11, 1, 10, 174, 510);
                seg.Insert(id, 11, 2, 10, 109, 510);
                seg.Insert(id, 12, 0, 10, 109, 510);
                seg.Insert(id, 12, 1, 10, 10, 510);
                seg.Insert(id, 12, 2, 10, 76, 510);
                seg.Insert(id, 13, 0, 10, 109, 510);
                seg.Insert(id, 13, 1, 10, 10, 510);
                seg.Insert(id, 13, 2, 10, 76, 510);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Normotype, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 15, 109, 778);
                seg.Insert(id, 1, 1, 15, 10, 778);
                seg.Insert(id, 2, 0, 15, 109, 778);
                seg.Insert(id, 2, 1, 15, 10, 778);
                seg.Insert(id, 3, 0, 15, 109, 778);
                seg.Insert(id, 3, 1, 15, 10, 778);
                seg.Insert(id, 4, 0, 15, 109, 778);
                seg.Insert(id, 4, 1, 15, 10, 778);
                seg.Insert(id, 5, 0, 15, 109, 778);
                seg.Insert(id, 5, 1, 15, 10, 778);
                seg.Insert(id, 6, 0, 15, 109, 778);
                seg.Insert(id, 6, 1, 15, 10, 778);
                seg.Insert(id, 7, 0, 15, 174, 778);
                seg.Insert(id, 7, 1, 15, 76, 778);
                seg.Insert(id, 8, 0, 15, 109, 778);
                seg.Insert(id, 8, 1, 15, 10, 778);
                seg.Insert(id, 9, 0, 15, 174, 778);
                seg.Insert(id, 9, 1, 15, 76, 778);
                seg.Insert(id, 10, 0, 15, 109, 778);
                seg.Insert(id, 10, 1, 15, 10, 778);
                seg.Insert(id, 11, 0, 15, 174, 778);
                seg.Insert(id, 11, 1, 15, 76, 778);
                seg.Insert(id, 12, 0, 15, 109, 778);
                seg.Insert(id, 12, 1, 15, 10, 778);
                seg.Insert(id, 13, 0, 15, 174, 778);
                seg.Insert(id, 13, 1, 15, 76, 778);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Hypotype, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 15, 109, 817);
                seg.Insert(id, 1, 1, 15, 10, 817);
                seg.Insert(id, 2, 0, 15, 273, 817);
                seg.Insert(id, 2, 1, 15, 174, 817);
                seg.Insert(id, 3, 0, 15, 109, 817);
                seg.Insert(id, 3, 1, 15, 10, 817);
                seg.Insert(id, 4, 0, 15, 273, 817);
                seg.Insert(id, 4, 1, 15, 174, 817);
                seg.Insert(id, 5, 0, 15, 109, 817);
                seg.Insert(id, 5, 1, 15, 10, 817);
                seg.Insert(id, 6, 0, 15, 273, 817);
                seg.Insert(id, 6, 1, 15, 174, 817);
                seg.Insert(id, 7, 0, 20, 109, 533);
                seg.Insert(id, 7, 1, 10, 10, 533);
                seg.Insert(id, 8, 0, 20, 109, 533);
                seg.Insert(id, 8, 1, 10, 10, 533);
                seg.Insert(id, 9, 0, 20, 109, 533);
                seg.Insert(id, 9, 1, 10, 10, 533);
                seg.Insert(id, 10, 0, 20, 109, 495);
                seg.Insert(id, 10, 1, 10, 10, 495);
                seg.Insert(id, 11, 0, 20, 109, 495);
                seg.Insert(id, 11, 1, 10, 10, 495);
                seg.Insert(id, 12, 0, 20, 109, 495);
                seg.Insert(id, 12, 1, 10, 10, 495);
                seg.Insert(id, 13, 0, 20, 109, 495);
                seg.Insert(id, 13, 1, 15, 10, 495);

                id = proc.Insert(string.Format(insFmt, (byte)Sex.Male, (byte)Somatotype.Hyperergic, 56, byte.MaxValue));
                seg.Insert(id, 1, 0, 10, 174, 778);
                seg.Insert(id, 1, 1, 10, 273, 1076);
                seg.Insert(id, 1, 2, 10, 76, 500);
                seg.Insert(id, 2, 0, 10, 174, 752);
                seg.Insert(id, 2, 1, 10, 273, 1076);
                seg.Insert(id, 2, 2, 10, 76, 487);
                seg.Insert(id, 3, 0, 10, 174, 778);
                seg.Insert(id, 3, 1, 10, 273, 1092);
                seg.Insert(id, 3, 2, 10, 76, 495);
                seg.Insert(id, 4, 0, 10, 174, 778);
                seg.Insert(id, 4, 1, 10, 273, 1092);
                seg.Insert(id, 4, 2, 10, 76, 487);
                seg.Insert(id, 5, 0, 10, 174, 778);
                seg.Insert(id, 5, 1, 10, 273, 1092);
                seg.Insert(id, 5, 2, 10, 76, 495);
                seg.Insert(id, 6, 0, 10, 174, 778);
                seg.Insert(id, 6, 1, 10, 273, 1107);
                seg.Insert(id, 6, 2, 10, 76, 503);
                seg.Insert(id, 7, 0, 15, 109, 778);
                seg.Insert(id, 7, 1, 15, 10, 778);
                seg.Insert(id, 8, 0, 15, 109, 778);
                seg.Insert(id, 8, 1, 15, 10, 778);
                seg.Insert(id, 9, 0, 15, 109, 778);
                seg.Insert(id, 9, 1, 15, 10, 778);
                seg.Insert(id, 10, 0, 10, 174, 778);
                seg.Insert(id, 10, 1, 10, 273, 1107);
                seg.Insert(id, 10, 2, 10, 76, 500);
                seg.Insert(id, 11, 0, 10, 174, 778);
                seg.Insert(id, 11, 1, 10, 273, 1080);
                seg.Insert(id, 11, 2, 10, 76, 500);
                seg.Insert(id, 12, 0, 15, 109, 778);
                seg.Insert(id, 12, 1, 15, 10, 778);
                seg.Insert(id, 13, 0, 15, 109, 778);
                seg.Insert(id, 13, 1, 15, 10, 778);
            }
        }
        #endregion

        public Procedure() : base(TName) { }
    }
    #endregion
}
