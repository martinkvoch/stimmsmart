using System;
using System.Data;
using System.Linq;

using MDM.Properties;

namespace MDM.Data
{
    public enum ProcResult { Iniciated, Finished, Prematurely, Failed }

    public class PatProc : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", panControl = "panProcedure",
             insFmt = "(PAT_ID, USR_ID, CHANNEL) values ({0}, {1}, {2})",
             updFmt = "DURATION={0}, RESULT={1}", updWhereFmt = "ID = {0}",
             selFmt = "select p.LAST_NAME || ', ' || p.FIRST_NAME || ifnull(' '||p.MIDDLE_NAME, '') [{0}], strftime('%d.%m.%Y', r.DATE) || strftime(' %H:%M:%S', r.TIME) [{1}], " +
                         "u.NAME [{2}], substr(time(r.DURATION, 'unixepoch'), 4) [{3}], r.CHANNEL [{4}], " +
                         "case r.RESULT when 1 then '{5}' when 2 then '{6}' when 3 then '{7}' else '{8}' end [{9}] " +
                       "from {10} r, {11} p, {12} u where r.PAT_ID = p.id and r.USR_ID = u.ID order by 1,2";
        //private static byte nop = new Settings().NOP;
        internal const string TName = "PAT_PROC";

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + TName);
            Database.ExecCmd(string.Format("create table " + TName + " (" +
                "ID integer primary key, " +
                "DATE date not null default (date('now', 'localtime')), " +
                "TIME time not null default (time('now', 'localtime')), " +
                "PAT_ID integer constraint PATIENT_FKEY references {0}(ID), " +
                "USR_ID integer constraint USER_FKEY references {1}(ID), " +
                "CHANNEL byte not null default 1, " +
                "RESULT byte not null default 0 check(RESULT between {2} and {3}), " +
                "DURATION smallint not null default 0)",
                Patient.TName, User.TName,
                (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().First(), (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().Last()));
            //Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(PAT_ID, DATE)", TName));
        }
        #endregion

        public static void Alter()
        {
            Database.ExecCmd(string.Format("drop index {0}_UN", TName));
        }

        public PatProc() : base(TName) { }

        #region SelectCmd(), Count()
        public override string SelectCmd()
        {
            return string.Format(selFmt, Resources.ProcHdrPatient, Resources.ProcHdrDatum, Resources.ProcHdrOperator, Resources.ProcHdrDuration, Resources.ProcHdrChannel,
                Resources.ProcResultFinished, Resources.ProcResultPrematurely, Resources.ProcResultFailed, Resources.ProcResultInitiated, Resources.ProcHdrResult,
                TName, Patient.TName, User.TName);
        }

        public static long Count(int? id = null)
        {
            return Convert.ToInt64(Database.ExecScalar(string.Format("select count(*) from {0}{2}", TName, id.HasValue ? " where ID = " + id.Value.ToString() : string.Empty)));
        }
        #endregion

        public static int AddProcedure(int patID, int usrID, byte channel)
        {
            int res = -1;

            using(PatProc proc = new PatProc()) res = proc.Insert(string.Format(insFmt, patID, usrID, channel));
            return res;
        }

        public static void FinishProcedure(int procID, ushort duration, ProcResult result)
        {
            using(PatProc proc = new PatProc()) proc.Update(string.Format(updFmt, duration, (int)result), string.Format(updWhereFmt, procID));
        }
    }
}
