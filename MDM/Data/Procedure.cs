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
             selFmt = "select p.LAST_NAME || ', ' || p.FIRST_NAME || ifnull(' '||p.MIDDLE_NAME, '') [{0}], strftime('%d.%m.%Y', r.DATE) || strftime(' %H:%M:%S', r.TIME) [{1}], " +
                         "u.NAME [{2}], substr(time(r.DURATION, 'unixepoch'), 4) [{3}], r.CHANNEL [{4}], " +
                         "case r.RESULT when 1 then '{5}' when 2 then '{6}' when 3 then '{7}' else '{8}' end [{9}] " +
                       "from PROCEDURE r, PATIENT p, USER u where r.PAT_ID = p.id and r.USR_ID = u.ID order by 1,2";
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

        #region SelectCmd(), Count()
        public override string SelectCmd()
        {
            return string.Format(selFmt, Resources.ProcHdrPatient, Resources.ProcHdrDatum, Resources.ProcHdrOperator, Resources.ProcHdrDuration, Resources.ProcHdrChannel,
                Resources.ProcResultFinished, Resources.ProcResultPrematurely, Resources.ProcResultFailed, Resources.ProcResultInitiated, Resources.ProcHdrResult);
        }

        public static long Count(int? id = null)
        {
            return Convert.ToInt64(Database.ExecScalar(string.Format("select count(*) from {0}{2}", tname, id.HasValue ? " where ID = " + id.Value.ToString() : string.Empty)));
        }
        #endregion

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
