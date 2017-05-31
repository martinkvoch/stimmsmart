using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

using MDM.Controls;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Data
{
    #region struct THIC
    public struct THIC
    {
        public int Id;
        public string Name, Code;

        public string HICName { get { return (string.IsNullOrEmpty(Code) ? string.Empty : Code + " - ") + Name; } }

        public static bool operator ==(THIC h1, THIC h2) { return h1.Id == h2.Id; }
        public static bool operator !=(THIC h1, THIC h2) { return h1.Id != h2.Id; }

        public override bool Equals(object obj)
        {
            return this == (THIC)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public THIC(int id = -1, string name = null, string code = null)
        {
            Id = id;
            Name = name;
            Code = code;
        }
    }
    #endregion

    public class HIC : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}",
             tname = "HIC", panControl = "panHIC",
             insFmt = "(NAME, CODE) values ('{0}', '{1}')", insFmt1 = "(NAME) values ('{0}')",
             updFmt = "NAME = '{0}', CODE = '{1}'", updFmt1 = "NAME = '{0}'", updWhereFmt = "ID = {0}",
             selFmt = "select ID, NAME [{0}], CODE [{1}] from {2} order by 2";

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + tname);
            Database.ExecCmd("create table " + tname + " (ID integer primary key, NAME varchar(255) not null, CODE varchar(10))");
            using(HIC hic = new HIC())
            {
                hic.Insert(string.Format(insFmt, "Zaměstnanecká pojišťovna Škoda (ZPŠ)", 209));
                hic.Insert(string.Format(insFmt, "Česká průmyslová zdravotní pojišťovna (ČPZP)", 205));
                hic.Insert(string.Format(insFmt, "Revírní bratrská pokladna, zdrav. pojišťovna (RBP)", 213));
                hic.Insert(string.Format(insFmt, "Vojenská zdravotní pojišťovna ČR (VoZP)", 201));
                hic.Insert(string.Format(insFmt, "Všeobecná zdravotní pojišťovna ČR (VZP)", 111));
                hic.Insert(string.Format(insFmt, "Zdravotní pojišťovna ministerstva vnitra ČR (ZPMV)", 211));
                hic.Insert(string.Format(insFmt, "Oborová zdravotní pojišťovna zam. bank, poj. a stav. (OZP)", 207));
            }
        }
        #endregion

        public HIC() : base(tname) { }

        public static THIC Get(int id)
        {
            THIC res = new THIC();

            using(HIC hic = new HIC())
            {
                DataTable dt = hic.Select("ID,NAME,CODE", "ID = " + id.ToString());

                if(dt.Rows.Count > 0) res = new THIC(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
            }
            return res;
        }

        #region SelectCmd(), Count()
        public override string SelectCmd()
        {
            return string.Format(selFmt, Resources.HICHdrName, Resources.HICHdrCode, tname);
        }

        public static int Count()
        {
            return Convert.ToInt32(Database.ExecScalar(string.Format("select count(*) from {0}", tname)));
        }
        #endregion

        #region Add()
        public override void Add()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using(wHIC frm = new wHIC(true))
            {
                if(frm.ShowDialog() == DialogResult.OK)
                    using(HIC hic = new HIC())
                        if(hic.Insert(string.Format(string.IsNullOrEmpty(frm.HICode) ? insFmt1 : insFmt, frm.HICName, frm.HICode)) > 0)
                        {
                            string msg = string.Format(Resources.HICNewMsg, frm.HICName);

                            Log.InfoToLog(methodName, msg);
                            if(MainFrm != null)
                            {
                                DBPanel pan = MainFrm.Controls[panControl] as DBPanel;

                                MainFrm.ShowInStatus(LogTyp.Information, msg);
                                if(pan != null) pan.Fill();
                            }
                        }
            }
        }
        #endregion

        #region Edit()
        public override void Edit(object id)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(id == null) return;
            using(wHIC frm = new wHIC(false))
            {
                using(HIC hic = new HIC())
                {
                    DataTable dt = hic.Select("*", string.Format("ID={0}", id));

                    if(dt != null && dt.Rows.Count > 0)
                    {
                        frm.HICName = dt.Rows[0]["NAME"].ToString();
                        frm.HICode = dt.Rows[0]["CODE"].ToString();
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            string where = string.Format(updWhereFmt, id);

                            if(hic.Update(string.Format(string.IsNullOrEmpty(frm.HICode) ? updFmt1 : updFmt, frm.HICName, frm.HICode), where))
                            {
                                string msg = string.Format(Resources.HICEditMsg, frm.HICName);

                                Log.InfoToLog(methodName, msg);
                                if(MainFrm != null)
                                {
                                    DBPanel pan = MainFrm.Controls[panControl] as DBPanel;

                                    MainFrm.ShowInStatus(LogTyp.Information, msg);
                                    if(pan != null) pan.Fill();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Truncate()
        public static void Truncate()
        {
            MDMTable.Truncate(tname);
        }
        #endregion
    }
}
