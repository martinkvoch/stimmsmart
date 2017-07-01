using System;
using System.Data;
using System.Linq;

using MDM.Controls;
using MDM.Properties;
using System.Windows.Forms;

namespace MDM.Data
{
    public enum LogTyp { Information, Warning, Error, All }

    public class Log : MDMTable
    {
        const string tname = "LOG", panControl = "panLog";

        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + tname);
            Database.ExecCmd(string.Format("create table " + tname + " (ID integer primary key, " +
                "DAT datetime not null default (datetime('now', 'localtime')), " +
                "TYP byte not null check(TYP between {0} and {1}), " +
                "SENDER varchar(30) not null, " +
                "MSG text)", (byte)Enum.GetValues(typeof(LogTyp)).Cast<LogTyp>().First(), (byte)Enum.GetValues(typeof(LogTyp)).Cast<LogTyp>().Last()));
        }

        public Log() : base(tname) { }

        public override string SelectCmd()
        {
            return string.Format("select ID, DAT [{0}], case TYP when 0 then '{1}' when 1 then '{2}' else '{3}' end [{4}], SENDER [{5}], MSG [{6}] from {7} order by 1", Resources.LogHdrDate, Resources.LogInfo, Resources.LogWarn, Resources.LogError, Resources.LogHdrType, Resources.LogHdrSender, Resources.LogHdrRecord, tname);
        }

        #region Zápis do deníku
        private void toLog(LogTyp typ, string sender, string msg = null)
        {
            if(Database.Status == DbStatus.Open)
            {
                string cmd = string.IsNullOrEmpty(msg) ? string.Format("(TYP, SENDER) values ({0}, '{1}')", (int)typ, sender) :
                    string.Format("(TYP, SENDER, MSG) values ({0}, '{1}', '{2}')", Convert.ToByte(typ), sender,
                    string.IsNullOrEmpty(msg) ? null : msg.Replace('\'', '"'));

                if(Insert(cmd) > 0 && MainFrm != null)
                {
                    DBPanel pan = MainFrm.Controls[panControl] as DBPanel;

                    if(MainFrm.IsHandleCreated) MainFrm.Invoke(new MethodInvoker(delegate {
                        MainFrm.ShowInStatus(typ, string.IsNullOrEmpty(msg) ? string.Empty : msg.Replace("\r\n", ", "));
                        if(pan != null) pan.Fill();
                    }));
                }
            }
        }

        /// <summary>
        /// Do deníku zapíše zprávu typu informace. Datum a čas zprávy budou dány aktuálním datumem a časem.
        /// </summary>
        /// <param name="sender">název komponenty, která zprávu zapsala</param>
        /// <param name="msg">zpráva zapsaná do deníku</param>
        public static void InfoToLog(string sender, string msg = null)
        {
            using(Log log = new Log()) log.toLog(LogTyp.Information, sender, msg);
        }

        /// <summary>
        /// Do deníku zapíše zprávu typu varování. Datum a čas zprávy budou dány aktuálním datumem a časem.
        /// </summary>
        /// <param name="sender">název komponenty, která zprávu zapsala</param>
        /// <param name="msg">zpráva zapsaná do deníku</param>
        public static void WarningToLog(string sender, string msg = null)
        {
            using(Log log = new Log()) log.toLog(LogTyp.Warning, sender, msg);
        }

        /// <summary>
        /// Do deníku zapíše zprávu typu chyba. Datum a čas zprávy budou dány aktuálním datumem a časem.
        /// </summary>
        /// <param name="sender">název komponenty, která zprávu zapsala</param>
        /// <param name="msg">zpráva zapsaná do deníku</param>
        public static void ErrorToLog(string sender, string msg = null)
        {
            using(Log log = new Log()) log.toLog(LogTyp.Error, sender, msg);
        }
        #endregion

        #region Mazání z deníku
        private static void wipeCateg(LogTyp typ)
        {
            using(Log log = new Log())
            {
                DataTable dt = log.Select("ID", string.Format("TYP = {0}", (byte)typ));

                log.Wipe(dt.Rows.OfType<DataRow>().Select(r => r[0]).ToArray());
                if(MainFrm != null) (MainFrm.Controls[panControl] as DBPanel).Fill();
            }
        }

        public static void WipeInfo()
        {
            wipeCateg(LogTyp.Information);
        }

        public static void WipeWarning()
        {
            wipeCateg(LogTyp.Warning);
        }

        public static void WipeError()
        {
            wipeCateg(LogTyp.Error);
        }

        public static void Truncate()
        {
            MDMTable.Truncate(tname);
        }
        #endregion

        public static long Count(LogTyp typ = LogTyp.All)
        {
            if(typ == LogTyp.All) return Convert.ToInt64(Database.ExecScalar("select count(*) from " + tname));
            else return Convert.ToInt64(Database.ExecScalar(string.Format("select count(*) from {0} where TYP = {1}", tname, Convert.ToByte(typ))));
        }
    }
}
