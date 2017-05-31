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
    public enum ProcResult { OK, Prematurely, Failed }

    public class Procedure : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", tname = "PROCEDURE",
             insFmt = "(NAME, LANG, PARENT) values ('{0}', '{1}', {2})", insFmt1 = "(NAME, LANG) values ('{0}', '{1}')",
             updFmt = "NAME='{0}', LANG={1}'", updWhereFmt = "ID = {0}",
             selFmt = "select ID, NAME [{0}], LANG [{1}] from {2} where {3} DELETED order by 2";
        private byte nop = new Settings().NOP;

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + tname);
            Database.ExecCmd(string.Format("create table " + tname + " (" +
                "ID integer primary key, " +
                "DATE date not null default CURRENT_DATE, " +
                "PAT_ID integer constraint PATIENT_FKEY references PATIENT(ID), " +
                "CYCLE byte not null default 1, " +
                "NUMBER byte not null default 1 check(NUMBER between 1 and {0}), " +
                "FINAL boolean not null default FALSE, " +
                "CHANNEL byte not null default 1, " +
                "RESULT byte not null default 0 check(RESULT between {1} and {2}), " +
                "DURATION smallint not null default 0, " +
                "PROGRESS text)", new Settings().NOP, (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().First(), (byte)Enum.GetValues(typeof(ProcResult)).Cast<ProcResult>().Last()));
            Database.ExecCmd(string.Format("create unique index {0}_UN on {0}(PAT_ID, CYCLE, NUMBER)", tname));
        }
        #endregion

        public Procedure() : base(tname) { }

        #region ProcNum()
        /// <summary>
        /// Vrací aktuální hodnoty čísla procedury a čísla cyklu pro daného pacienta.
        /// Pokud není pacient nalezen, vrací (proc, cycle) = (0, 0).
        /// </summary>
        /// <param name="patId">identifikační číslo pacienta</param>
        /// <param name="cycle">návratová hodnota čísla cyklu</param>
        /// <returns>Vrací číslo procedury.</returns>
        public static int ProcNum(int patId, ref int cycle)
        {
            int res = 0;

            cycle = 0;
            if(Patient.Exists(patId))
            {
                DataTable dt = Database.Select("select PAT_ID, CYCLE, max(NUMBER) PROC_NUM from PROCEDURE group by PAT_ID, CYCLE having not FINAL limit 1");

                if(dt != null && dt.Rows.Count > 0)
                {
                    res = Convert.ToInt32(dt.Rows[0]["PROC_NUM"]);
                    cycle = Convert.ToInt32(dt.Rows[0]["CYCLE"]);
                }
                else
                {
                    dt.Dispose();
                    dt = null;
                    dt = Database.Select("select PAT_ID, max(CYCLE) CYCLE, max(NUMBER) PROC_NUM from PROCEDURE group by PAT_ID having FINAL limit 1");
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        res = Convert.ToInt32(dt.Rows[0]["PROC_NUM"]);
                        cycle = Convert.ToInt32(dt.Rows[0]["CYCLE"]) + 1;
                    }
                }
            }
            return res;
        }
        #endregion
    }
}
