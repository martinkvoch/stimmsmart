using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

using MDM.Controls;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Data
{
    public enum ProcResult { Iniciated, Finished, Prematurely, Failed }

    public class Procedure : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", tname = "PROCEDURE", panControl = "panProcedure",
             insFmt = "(PAT_ID, USR_ID, CHANNEL) values ({0}, {1}, {2})",
             updFmt = "DURATION={0}, RESULT={1}", updWhereFmt = "ID = {0}",
             selFmt = "select ID, NAME [{0}], LANG [{1}] from {2} where {3} DELETED order by 2";
        private byte nop = new Settings().NOP;

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + tname);
            Database.ExecCmd(string.Format("create table " + tname + " (" +
                "ID integer primary key, " +
                "DATE date not null default (date('now', 'localtime')), " +
                "TIME time not null default (time('now', 'localtime')), " +
                "PAT_ID integer constraint PATIENT_FKEY references PATIENT(ID), " +
                "USR_ID integer constraint USER_FKEY references USER(ID), " +
                "CHANNEL byte not null default 1, " +
                "RESULT byte not null default 0 check(RESULT between {0} and {1}), " +
                "DURATION smallint not null default 0)",
                (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().First(), (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().Last()));
            Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(PAT_ID, DATE)", tname));
            //Database.ExecCmd(string.Format("create table " + tname + " (" +
            //    "ID integer primary key, " +
            //    "DATE date not null default CURRENT_DATE, " +
            //    "PAT_ID integer constraint PATIENT_FKEY references PATIENT(ID), " +
            //    "CYCLE byte not null default 1, " +
            //    "NUMBER byte not null default 1 check(NUMBER between 1 and {0}), " +
            //    "FINAL boolean not null default FALSE, " +
            //    "CHANNEL byte not null default 1, " +
            //    "RESULT byte not null default 0 check(RESULT between {1} and {2}), " +
            //    "DURATION smallint not null default 0, " +
            //    "PROGRESS text)", new Settings().NOP, (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().First(), (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().Last()));
            //Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(PAT_ID, CYCLE, NUMBER)", tname));
        }
        #endregion

        public Procedure() : base(tname) { }

        public static int AddProcedure(int patID, int usrID, byte channel)
        {
            int res = -1;

            using(Procedure proc = new Procedure()) res = proc.Insert(string.Format(insFmt, patID, usrID, channel));
            return res;
        }

        public static void FinishProcedure(int procID, ushort duration, ProcResult result)
        {
            using(Procedure proc = new Procedure()) proc.Update(string.Format(updFmt, duration, (int)result), string.Format(updWhereFmt, procID));
        }
    }
}
