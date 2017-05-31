using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using MDM.DlgBox;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Data
{
    public class MDMTable : IDisposable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", whereFmt = " where {0}";
        public string TableName;

        /// <summary>
        /// Odkaz na hlavní okno pro zápis do deníku
        /// </summary>
        internal static wMain MainFrm;

        public void Dispose() { }

        public MDMTable(string tname) 
        {
            TableName = tname;
        }

        #region Select
        public virtual string SelectCmd()
        {
            return string.Format("select * from {0}", TableName);
        }

        public DataTable Select(string select, string where = null)
        {
            DataTable res = new DataTable();

            select = string.Format("select {0} from {1}", select, TableName);
            if(!string.IsNullOrEmpty(where)) select += string.Format(whereFmt, where);
            using(SQLiteConnection conn = Database.CreateConnection())
                using(SQLiteDataAdapter da = new SQLiteDataAdapter(select, conn)) da.Fill(res);
            return res;
        }
        #endregion

        #region Insert
        public virtual void Add() { }

        public virtual int Insert(string insert)
        {
            string cmd = string.Format("insert into {0}{1}", TableName, insert);

            if(Database.ExecCmd(cmd, false) == 1) return Convert.ToInt32(Database.ExecScalar("select max(ID) from " + TableName));
            return 0;
        }
        #endregion

        #region Update
        public virtual void Edit(object id) { }

        public virtual bool Update(string update, string where = null)
        {
            string cmd = string.Format("update {0} set {1}", TableName, update);

            if(!string.IsNullOrEmpty(where)) cmd += string.Format(whereFmt, where);
            return Database.ExecCmd(cmd) >= 0;
        }
        #endregion

        private const string wipeFmt = "delete from {0}", delFmt = "update {0} set DELETED = ", purgeFmt = "delete from {0} where DELETED";

        #region Delete
        /// <summary>
        /// Klausule WHERE daného příkazu SQL
        /// </summary>
        /// <param name="keys">seznam IDček</param>
        /// <returns>Vrací řetězec s formulací klauzule WHERE.</returns>
        public virtual string DeleteWhere(object[] keys)
        {
            return string.Format("ID in ({0})", string.Join(",", keys.Select(k => k.ToString())));
        }

        /// <summary>
        /// Dané záznamy z dané tabulky označí jako smazané.
        /// </summary>
        /// <param name="keys">seznam IDček, která mají být označena ke smazání</param>
        /// <returns>Vrací počet fakticky označených řádků.</returns>
        public virtual int Delete(object[] keys)
        {
            if(Database.TableHasDELETED(TableName))
            {
                string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                string cmd = string.Format(delFmt + '1' + " where not DELETED and {1}", TableName, DeleteWhere(keys));
                int res = Database.ExecCmd(cmd);

                if(res > 0) Log.WarningToLog(methodName, string.Format(Resources.delNRows, res, TableName));
                return res;
            }
            else return Wipe(keys);
        }

        /// <summary>
        /// Označí jeden řádek jako smazaný
        /// </summary>
        /// <param name="id">ID řádku, který má být označen jako smazaný</param>
        /// <returns>Vrací počet fakticky označených řádků.</returns>
        public virtual int Delete(object id)
        {
            return Delete(new object[] { id });
        }

        /// <summary>
        /// Označí všechny řádky v dané tabulce ke smazání
        /// </summary>
        /// <param name="tname">název tabulky, která má být označena</param>
        public static void DeleteAll(string tname)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(DialogBox.ShowYN(string.Format(Resources.delAllQ, tname), Resources.delAllQH) == DialogResult.Yes)
            {
                string msg = string.Format(Resources.delAllMsg, tname);

                Database.ExecCmd(string.Format(delFmt + '1', tname));
                Log.WarningToLog(methodName, msg);
                DialogBox.ShowWarn(msg, Resources.delAllQH);
            }
        }
        #endregion

        #region Undelete
        /// <summary>
        /// Dané záznamy z dané tabulky označené ke smazání označí jako řádné - nesmazané.
        /// </summary>
        /// <param name="keys">seznam IDček, která mají být odznačena</param>
        /// <returns>Vrací počet fakticky odznačených řádků.</returns>
        public virtual int Undelete(object[] keys)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string cmd = string.Format(delFmt + '0' + " where DELETED and {1}", TableName, DeleteWhere(keys));
            int res = Database.ExecCmd(cmd);

            if(res > 0) Log.InfoToLog(methodName, string.Format(Resources.delNRows, res, TableName));
            return res;
        }

        /// <summary>
        /// Odznačí jeden řádek tabulky, tj. zruší jeho označení ke smazání.
        /// </summary>
        /// <param name="id">ID řádku, který má být odznačen</param>
        /// <returns>Vrací počet fakticky odznačených řádků.</returns>
        public virtual int Undelete(object id)
        {
            return Undelete(new object[] { id });
        }
        #endregion

        //TODO: dodělat hlášky do Resources
        #region Wipe(), Truncate(), Purge()
        /// <summary>
        /// Fyzicky smaže dané záznamy z dané tabulky.
        /// </summary>
        /// <param name="keys">seznam IDček, která mají být smazána</param>
        /// <returns>Vrací počet fakticky smazaných řádků.</returns>
        public virtual int Wipe(object[] keys)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string cmd = string.Format(wipeFmt + " where {1}", TableName, DeleteWhere(keys));
            int res = Database.ExecCmd(cmd);

            if(res > 0) Log.WarningToLog(methodName, string.Format(Resources.delNRows, res, TableName));
            if(res > 50) Database.Compact();
            return res;
        }

        /// <summary>
        /// Fyzicky smaže jeden řádek
        /// </summary>
        /// <param name="id">ID řádku, který má být smazán</param>
        /// <returns>Vrací počet fakticky smazaných řádků.</returns>
        public virtual int Wipe(object id)
        {
            return Wipe(new object[] { id });
        }

        /// <summary>
        /// Fyzicky vymaže všechny řádky v dané tabulce
        /// </summary>
        /// <param name="tname">název tabulky, která má být vymazána</param>
        public static void Truncate(string tname)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(DialogBox.ShowYN(string.Format(Resources.delAllQ, tname), Resources.delAllQH) == DialogResult.Yes)
            {
                string msg = string.Format(Resources.delAllMsg, tname);

                Database.ExecCmd(string.Format(wipeFmt, tname));
                Database.Compact();
                Log.WarningToLog(methodName, msg);
                DialogBox.ShowWarn(msg, Resources.delAllQH);
            }
        }

        /// <summary>
        /// Fyzicky vymaže všechny řádky tabulky označené ke smazání
        /// </summary>
        /// <param name="tname">název tabulky, která má být očištěna</param>
        public static void Purge(string tname)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(DialogBox.ShowYN(string.Format(Resources.delAllQ, tname), Resources.delAllQH) == DialogResult.Yes)
            {
                string msg = string.Format(Resources.delAllMsg, tname);

                Database.ExecCmd(string.Format(purgeFmt, tname));
                Database.Compact();
                Log.WarningToLog(methodName, msg);
                DialogBox.ShowWarn(msg, Resources.delAllQH);
            }
        }
        #endregion
    }
}
