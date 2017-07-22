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
    #region TDiagnosis
    public struct TDiagnosis
    {
        public int ID;
        public string Name, Lang;

        public static bool operator ==(TDiagnosis d1, TDiagnosis d2) { return d1.ID == d2.ID; }
        public static bool operator !=(TDiagnosis d1, TDiagnosis d2) { return d1.ID != d2.ID; }

        public override bool Equals(object obj)
        {
            return this == (TDiagnosis)obj;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public TDiagnosis(int id = -1, string name = null, string lang = null)
        {
            ID = id;
            Name = id == -1 ? Resources.dgNotSelected : name;
            Lang = lang ?? Program.Language;
        }
    }
    #endregion

    public class Diagnosis : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}",
             insFmt = "(NAME, LANG, PARENT) values ('{0}', '{1}', {2})", insFmt1 = "(NAME, LANG) values ('{0}', '{1}')",
             updFmt = "NAME='{0}', LANG={1}'", updWhereFmt = "ID = {0}",
             selFmt = "select ID, NAME [{0}], LANG [{1}] from {2} where {3} DELETED order by 2";
        internal const string TName = "DIAGNOSIS";

        #region Init()
        //private static string[] getLangs() 
        //{
        //    //bool haveDisposed = false;
        //    //wMain frm = null;
        //    //string[] res = new string[0];

        //    //if(Application.OpenForms != null && Application.OpenForms.Count > 0 && (Application.OpenForms[0] is wMain)) frm = (Application.OpenForms[0] as wMain);
        //    //else
        //    //{
        //    //    frm = new wMain(Program.Language);
        //    //    haveDisposed = true;
        //    //}
        //    //res = frm.miLang.DropDownItems.OfType<ToolStripMenuItem>().Select(i => i.Tag.ToString()).ToArray();
        //    //if(haveDisposed && frm != null) frm.Dispose();
        //    string[] res = new string[] { "cs", "en", "ru" };
        //    return res; 
        //}

        public static void Init()
        {
            string[] langs = Program.GetLangs().Select(l => l.Split('|')[0]).ToArray();

            Database.ExecCmd("drop table if exists " + TName);
            Database.ExecCmd("create table " + TName + " ("+
                "ID integer primary key, "+
                "NAME varchar(255) not null, "+
                "LANG char(2) not null, "+
                "PARENT integer constraint PARENT_ID_FKEY references "+ TName + "(ID), " +
                "DELETED boolean default FALSE)");
            using(Diagnosis dg = new Diagnosis())
            {
                ResourceManager rm = new ResourceManager("MDM.Properties.Resources", Assembly.GetExecutingAssembly());

                foreach(string lang in langs)
                {
                    int id;
                    CultureInfo culture = new CultureInfo(lang);

                    id = dg.Insert(string.Format(insFmt1, rm.GetString("dgEndocrinology", culture), lang));
                    dg.Insert(string.Format(insFmt, rm.GetString("dgDPNP", culture), lang, id));
                    id = dg.Insert(string.Format(insFmt1, rm.GetString("dgMicrocirculation", culture), lang));
                    dg.Insert(string.Format(insFmt, rm.GetString("dgEDis", culture), lang, id));
                    dg.Insert(string.Format(insFmt, rm.GetString("dgCPain", culture), lang, id));
                    dg.Insert(string.Format(insFmt, rm.GetString("dgCLesions", culture), lang, id));
                }
            }
        }
        #endregion

        public static TDiagnosis Get(int id)
        {
            TDiagnosis res = new TDiagnosis();

            using(Diagnosis dg = new Diagnosis())
            {
                DataTable dt = dg.Select("ID,NAME,LANG", "ID = " + id.ToString());

                if(dt.Rows.Count > 0) res = new TDiagnosis(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
            }
            return res;
        }

        public static TDiagnosis GetFirst()
        {
            TDiagnosis res = new TDiagnosis();

            using(Diagnosis dg = new Diagnosis())
            {
                DataRow dr;

                dr = dg.Select("ID,NAME,LANG", string.Format("PARENT is null and not DELETED and LANG = '{0}'", Program.Language)).Rows.OfType<DataRow>().FirstOrDefault();
                if(dr != null)
                {
                    dr = dg.Select("ID,NAME,LANG", string.Format("PARENT = {0} and not DELETED and LANG = '{1}'", Convert.ToInt32(dr[0]), Program.Language)).Rows.OfType<DataRow>().FirstOrDefault();
                    if(dr != null) res = new TDiagnosis(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString());
                }
            }
            return res;
        }

        public Diagnosis() : base(TName) { }
    }
}
