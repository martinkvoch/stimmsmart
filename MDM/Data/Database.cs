using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;

using MDM.DlgBox;
using MDM.Properties;

namespace MDM.Data
{
    public enum DbStatus { Created, Saved, Closed, Open, BackedUp, Restored, Compressed }

    public static class Database
    {
        const ushort autoBackupLimit = 20; // Po tomto počtu provedených DML příkazů se automaticky udělá Backup
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", dateFmt = "yyyyMMddHHmmss", pwd = "DJu9USeZ4zwcRp/cu0WKmA==";//#KsntsZ@

        private static readonly string connStr = new Settings().MDMConnectionString;
        public static DbStatus Status = DbStatus.Closed;

        #region BackupDir, EncExt
        /// <summary>
        /// Název adresáře, kde se uchovávají zálohy databáze
        /// </summary>
        public const string BackupDir = "backup";
        /// <summary>
        /// Přípona pro zaheslované databáze
        /// </summary>
        public const string EncExt = "enc";
        #endregion

        #region string dbFileName
        private static string _dbFileName;
        private static string dbFileName
        {
            get
            {
                if(string.IsNullOrEmpty(_dbFileName))
                {
                    string[] words = connStr.Split(new char[] { '=', ';' });
                    _dbFileName = words[1];
                }
                return _dbFileName;
            }
        }
        #endregion

        #region Privátní utility (zip, unzip, tableHasColumn, autoBackup)
        private static void zip(string file = null)
        {
            string zipFN = Path.ChangeExtension(file ?? dbFileName, EncExt);

            File.Delete(zipFN);
            using(ZipFile zip = new ZipFile(zipFN, Encoding.UTF8))
            {
                zip.Password = EncryptionUtilities.DecryptString(pwd);
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.AddFile(dbFileName, string.Empty);
                //zip.AddFile(Path.ChangeExtension(zipFN, Path.GetExtension(dbFileName)), string.Empty);
                zip.Save();
            }
        }

        private static void unzip(string file = null)
        {
            string fn = Path.ChangeExtension(file ?? dbFileName, Path.GetExtension(dbFileName)), path = Path.GetDirectoryName(fn);

            File.Delete(fn);
            using(ZipFile zip = new ZipFile(Path.ChangeExtension(fn, EncExt), Encoding.UTF8))
            {
                zip.Password = EncryptionUtilities.DecryptString(pwd);
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                //zip.StatusMessageTextWriter = Console.Out;
                zip.ExtractAll(string.IsNullOrEmpty(path) ? @".\" : path);
                //zip.ExtractAll(@".\");
            }
        }

        private static bool tableHasColumn(string tname, string cname)
        {
            bool res = false;

            using(SQLiteConnection conn = Database.CreateConnection())
            {
                using(DataTable dt = new DataTable())
                {
                    using(SQLiteDataAdapter da = new SQLiteDataAdapter("PRAGMA table_info(" + tname + ")", conn)) da.Fill(dt);
                    res = dt.Rows.OfType<DataRow>().FirstOrDefault(r => r["name"].ToString().Equals(cname, StringComparison.OrdinalIgnoreCase)) != null;
                }
            }
            return res;
        }

        private static ushort nCmds = 0;
        private static void autoBackup()
        {
            nCmds++;
            if(nCmds == autoBackupLimit)
            {
                Backup(true);
                nCmds = 0;
            }
        }
        #endregion

        #region Obecné utility (Fn2Date, Date2Fn, CreateConnection, Select, ExecCmd, ExecScalar, TableHasDELETED)
        internal static string GetSAP()
        {
            return EncryptionUtilities.CreatePasswordSalt(EncryptionUtilities.DecryptString(pwd));
        }

        /// <summary>
        /// Slouží ke konverzi jména souboru se zálohou na datum
        /// </summary>
        /// <param name="fn">jméno souboru se zálohou</param>
        /// <param name="lg">
        /// pokud je <c>true</c>, výsledné datum bude vyjádřeno jako "den v týdnu, den, měsíc, rok, hodina, minuta, vteřina",
        /// jinak jako "den, měsíc, rok, hodina, minuta, vteřina".
        /// </param>
        /// <returns>Vrací řetězec s datumem odpovídajícím jménu souboru se zálohou.</returns>
        public static string Fn2Date(string fn, bool lg = false)
        {
            DateTime dt = DateTime.ParseExact(Path.GetFileNameWithoutExtension(fn), dateFmt, null);

            return string.Format("{0} {1}", lg ? dt.ToLongDateString() : dt.ToShortDateString(), dt.ToLongTimeString());
        }

        /// <summary>
        /// Konvertuje řetězec ve formě datumu a času na jméno souboru se zálohou
        /// </summary>
        /// <param name="date">řetězec s datumem a časem</param>
        /// <returns>Vrací řetězec se jménem souboru s odpovídající zálohou (s relativní cestou a příponou).</returns>
        public static string Date2Fn(string date)
        {
            string fn = DateTime.Parse(date).ToString(dateFmt);

            return Path.Combine(BackupDir, fn.Substring(0, 4), fn.Substring(4, 2), Path.ChangeExtension(fn, Path.GetExtension(dbFileName)));
        }

        /// <summary>
        /// Vytvoří nové spojení na databázi
        /// </summary>
        /// <returns>Vrací instanci třídy SQLiteConnection.</returns>
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection res = new SQLiteConnection(connStr);

            res.Open();
            return res;
        }

        /// <summary>
        /// Provede zadaný select a vrátí výsledek v objektu DataTable.
        /// </summary>
        /// <param name="cmd">příkaz SELECT</param>
        /// <returns>Vrací instanci třídy DataTable</returns>
        public static DataTable Select(string cmd)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            DataTable res = new DataTable();


            try
            {
                DataSet dataSet = new DataSet();

                using(SQLiteConnection conn = CreateConnection())
                using(SQLiteDataAdapter da = new SQLiteDataAdapter(cmd, conn)) da.Fill(dataSet);
                if(dataSet.Tables.Count > 0) res = dataSet.Tables[0];
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(e.Message, string.Format(Resources.errExecCmd, e.ErrorCode, methodName));
            }
            return res;
        }

        /// <summary>
        /// Provede příkaz DDL/DML SQL.
        /// V případě neúspěchu se zobrazí chybová zpráva a ta se také zapíše do deníku.
        /// </summary>
        /// <param name="cmd">příkaz SQL, který má být proveden</param>
        /// <param name="wTran">pokud je true, provede se příkaz v rámci transakce</param>
        /// <returns>Vrací počet řádků, které byly příkazem ovlivněny.</returns>
        public static int ExecCmd(string cmd, bool wTran = true)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            int res = 0;

            try
            {
                using(SQLiteConnection conn = CreateConnection())
                if(wTran) using(SQLiteTransaction tran = conn.BeginTransaction())
                {
                    using(SQLiteCommand command = new SQLiteCommand(cmd, conn)) res = command.ExecuteNonQuery();
                    tran.Commit();
                }
                else using(SQLiteCommand command = new SQLiteCommand(cmd, conn)) res = command.ExecuteNonQuery();
                autoBackup();
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(e.Message, string.Format(Resources.errExecCmd, e.ErrorCode, methodName));
                res = -1;
            }
            return res;
        }

        /// <summary>
        /// Provede příkaz SQL, který vrací singulární (skalární) hodnotu.
        /// V případě neúspěchu se zobrazí chybová zpráva a ta se také zapíše do deníku.
        /// </summary>
        /// <param name="cmd">příkaz SQL, který má být proveden</param>
        /// <returns>Návratová hodnota v obecném tvaru.</returns>
        public static object ExecScalar(string cmd)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            object res = 0;

            try
            {
                using(SQLiteConnection conn = CreateConnection())
                    using(SQLiteCommand command = new SQLiteCommand(cmd, conn)) res = command.ExecuteScalar();
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(e.Message, string.Format(Resources.errExecCmd, e.ErrorCode, methodName));
            }
            return res;
        }

        /// <summary>
        /// Zjistí, zda tabulka obsahuje sloupec s názvem DELETED
        /// </summary>
        /// <param name="tname">název tabulky</param>
        /// <returns>Vrací true v případě, že tabulka sloupec obsahuje, jinak vrací false.</returns>
        public static bool TableHasDELETED(string tname)
        {
            return tableHasColumn(tname, "DELETED");
        }

        ///// <summary>
        ///// Zjistí, zda tabulka obsahuje sloupec s názvem ID
        ///// </summary>
        ///// <param name="tname">název tabulky</param>
        ///// <returns>Vrací true v případě, že tabulka sloupec obsahuje, jinak vrací false.</returns>
        //public static bool TableHasID(string tname)
        //{
        //    return tableHasColumn(tname, "ID");
        //}
        #endregion

        #region Init()
        internal static void Init(bool force = false)
        {
            DbStatus oldStatus = Status;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Status = DbStatus.Created;
                if(!force && !File.Exists(dbFileName)) force = true;
                if(force)
                {
                    if(File.Exists(dbFileName))
                    {
                        Backup(true);
                        File.Delete(dbFileName);
                    }
                    SQLiteConnection.CreateFile(dbFileName);
                    Log.Init();
                    User.Init();
                    Diagnosis.Init();
                    //HIC.Init();
                    Procedure.Init();
                    Patient.Init();
                    PatProc.Init();
                    Log.InfoToLog(methodName, Resources.newDataOK);
                    DialogBox.ShowInfo(Resources.newDataOK, Resources.newDataQH);
                }
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(Resources.newDataKO, Resources.newDataQH);
            }
            finally
            {
                Status = oldStatus;
            }
        }
        #endregion

        #region Save(), Backup(), Restore(), Compact()
        internal const string NulDrive = "nul";

        internal static string GetFlashDrive()
        {
            DriveInfo drive = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable).FirstOrDefault();
            string res = NulDrive;

            if(drive != null) res = drive.Name;
            return res;
        }

        private static void checkSpace(string fn)
        {
            DriveInfo drive = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable).FirstOrDefault();

            if(drive != null)
            {
                string baseDir = Path.Combine(drive.Name, BackupDir);
                string oldestDir = Directory.GetDirectories(Directory.GetDirectories(baseDir).OrderBy(d => d).FirstOrDefault()).OrderBy(d => d).FirstOrDefault();
                string oldestFile = Directory.GetFiles(oldestDir ?? baseDir, Path.ChangeExtension("*", EncExt)).OrderBy(f => f).FirstOrDefault();
                FileInfo fi = new FileInfo(fn);

                while(fi.Length > drive.AvailableFreeSpace && !string.IsNullOrEmpty(oldestFile))
                {
                    File.Delete(oldestFile);
                    if(Directory.GetFiles(oldestDir).Length == 0)
                    {
                        Directory.Delete(oldestDir);
                        oldestDir = Path.GetDirectoryName(oldestDir);
                        if(Directory.GetDirectories(oldestDir).Length == 0) Directory.Delete(oldestDir);
                    }
                    drive = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable).FirstOrDefault();
                    oldestDir = Directory.GetDirectories(Directory.GetDirectories(baseDir).OrderBy(d => d).FirstOrDefault()).OrderBy(d => d).FirstOrDefault();
                    oldestFile = Directory.GetFiles(oldestDir ?? baseDir, Path.ChangeExtension("*", EncExt)).OrderBy(f => f).FirstOrDefault();
                }
            }
        }

        internal static void Save()
        {
            DbStatus oldStatus = Status;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Status = DbStatus.Saved;
                if(File.Exists(dbFileName))
                {
                    zip();
                    Log.InfoToLog(methodName, "Databáze byla uložena.");
                }
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
            }
            finally
            {
                Status = oldStatus;
            }
        }

        internal static void Backup(bool silent = false)
        {
            DbStatus oldStatus = Status;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Status = DbStatus.BackedUp;
                if(File.Exists(dbFileName))
                {
                    string bckFN = Date2Fn(DateTime.Now.ToString("G")),
                           msg = string.Format(Resources.bckDataOK, Fn2Date(bckFN)),
                           usbFlash = GetFlashDrive();

                    if(!Directory.Exists(Path.GetDirectoryName(bckFN))) Directory.CreateDirectory(Path.GetDirectoryName(bckFN));
                    File.Copy(dbFileName, bckFN, true);
                    zip(bckFN);
                    File.Delete(bckFN);
                    if(usbFlash != NulDrive)
                    {
                        try
                        {
                            checkSpace(Path.ChangeExtension(bckFN, EncExt));
                            bckFN = Path.Combine(usbFlash, bckFN);
                            if(!Directory.Exists(Path.GetDirectoryName(bckFN))) Directory.CreateDirectory(Path.GetDirectoryName(bckFN));
                            File.Copy(dbFileName, bckFN, true);
                            zip(bckFN);
                            File.Delete(bckFN);
                        }
                        catch { }
                    }
                    Log.InfoToLog(methodName, msg);
                    if(!silent) DialogBox.ShowInfo(msg, Resources.bckDataH);
                }
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(Resources.bckDataKO, Resources.bckDataH);
            }
            finally
            {
                Status = oldStatus;
            }
        }

        internal static void Restore(string from)
        {
            DbStatus oldStatus = Status;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Status = DbStatus.Restored;
                if(DialogBox.ShowYN(Resources.restoreQ, Resources.restoreQH) == DialogResult.Yes)
                {
                    string bckFN = Path.Combine(BackupDir, from.Substring(0, 4), from.Substring(4, 2), from);// Date2Fn(from);
                    //DialogBox.ShowInfo(string.Format("restore from = '{0}', date = '{1}', bckFN = '{2}'", from, Fn2Date(from), bckFN), "Info");
                    string msg = string.Format(Resources.restoreOK, Fn2Date(from));
                    //string msg = string.Format(Resources.restoreOK, DateTime.Parse(from).ToString("G")), bckFN = Date2Fn(from);

                    Backup(true);
                    unzip(bckFN);
                    bckFN = Path.ChangeExtension(bckFN, Path.GetExtension(dbFileName));
                    File.Delete(dbFileName);
                    File.Move(bckFN, dbFileName);
                    Log.InfoToLog(methodName, msg);
                    DialogBox.ShowInfo(msg, Resources.restoreQH);
                }
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(Resources.bckDataKO, Resources.bckDataH);
            }
            finally
            {
                Status = oldStatus;
            }
        }

        internal static void Compact()
        {
            DbStatus oldStatus = Status;
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Status = DbStatus.Compressed;
                ExecCmd("vacuum", false);
                autoBackup();
                Log.InfoToLog(methodName, Resources.vacuumMsg);
            }
            finally
            {
                Status = oldStatus;
            }
        }

        internal static void DeleteBackup(string backup)
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                string yearName = backup.Substring(0, 4), monthName = backup.Substring(4, 2), bckFN = Path.Combine(BackupDir, yearName, monthName, backup), bckDir = Path.GetDirectoryName(bckFN);

                File.Delete(bckFN);
                if(Directory.GetFiles(bckDir).Length == 0) Directory.Delete(Path.GetDirectoryName(bckFN));
                //if(DialogBox.ShowYN(Resources.restoreQ, Resources.restoreQH) == DialogResult.Yes)
                //{
                    //string msg = string.Format(Resources.restoreOK, DateTime.Parse(backup).ToString("G")), bckFN = Date2Fn(backup);

                    //Log.InfoToLog(methodName, msg);
                    //DialogBox.ShowInfo(msg, Resources.restoreQH);
                //}
            }
            catch(SQLiteException e)
            {
                Log.ErrorToLog(methodName, string.Format(errorFmt, e.ErrorCode, e.Message));
                DialogBox.ShowError(Resources.bckDataKO, Resources.bckDataH);
            }
        }
        #endregion

        #region Open(), Close()
        internal static void Open()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                //if(!File.Exists(dbFileName))
                //{
                File.Delete(dbFileName);
                if(!File.Exists(Path.ChangeExtension(dbFileName, EncExt)))
                {
                    Init(true);
                    zip();
                }
                unzip();
                Status = DbStatus.Open;
                //}
                Log.InfoToLog(methodName, Resources.dbIsOpen);
            }
            catch(Exception e)
            {
                Log.ErrorToLog(methodName, e.Message);
                DialogBox.ShowError(e.Message, string.Format(Resources.errExecCmd, string.Empty, methodName));
                Status = DbStatus.Closed;
            }
        }

        internal static void Close()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(Status != DbStatus.Closed)
            {
                Backup(true);
                Log.InfoToLog(methodName, Resources.dbIsClosed);
                zip();
                File.Delete(dbFileName);
                Status = DbStatus.Closed;
            }
        }
        #endregion
    }
}
