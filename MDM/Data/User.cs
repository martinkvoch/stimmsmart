using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using MDM.Controls;
using MDM.Properties;
using MDM.Windows;

namespace MDM.Data
{
    //TODO: ošetřit roli NotLogged ve wMain
    public enum UserRole { User, Admin, SuperAdmin, NotLogged };

    public struct TUser
    {
        public int ID;
        public string Login, Name, Password, Language;
        public UserRole Role;

        public TUser(int id, string login, string name, string password, string language, byte role)
        {
            ID = id;
            Login = login;
            Name = name;
            Password = password;
            Language = language;
            Role = (UserRole)role;

        }
    }

    public class User : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}",
             tname = "USER", panControl = "panUser",
             insFmt = "(LOGIN, NAME, PSW, ROLE, LANG) values ('{0}', '{1}', '{2}', {3}, '{4}')",
             updFmt = "NAME='{0}', ROLE={1}, LANG='{2}'", updWhereFmt = "ID = {0}",
             selFmt = "select ID, LOGIN [{0}], NAME [{1}], case ROLE when 1 then '{2}' else '{3}' end [{4}], LANG [{5}] from {6} where ROLE <> {7} and {8} DELETED order by 2";

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + tname);
            Database.ExecCmd(string.Format("create table " + tname + " (ID integer primary key, " +
                "LOGIN varchar(30) not null, " +
                "NAME varchar(100) not null, " +
                "PSW varchar(255) not null, " +
                "ROLE byte not null default 0 check(ROLE between {0} and {1}), LANG char(2), " +
                "DELETED boolean default FALSE)", (byte)Enum.GetValues(typeof(UserRole)).Cast<UserRole>().First(), (byte)Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Last()));
            using(User usr = new User())
                usr.Insert(string.Format(insFmt, UserRole.SuperAdmin, "Super Admin", Database.GetSAP(), Convert.ToByte(UserRole.SuperAdmin), new Settings().lang));
        }
        #endregion

        public User() : base(tname) { }

        #region SelectCmd(), SelectDeleted(), Count()
        public override string SelectCmd()
        {
            return string.Format(selFmt, Resources.UserHdrLogin, Resources.UserHdrName, Resources.UserRoleAdmin, Resources.UserRoleUser, Resources.UserHdrRole, Resources.UserHdrLang, tname, Convert.ToByte(UserRole.SuperAdmin), "not");
        }

        public string SelectDeleted()
        {
            return string.Format(selFmt, Resources.UserHdrLogin, Resources.UserHdrName, Resources.UserRoleAdmin, Resources.UserRoleUser, Resources.UserHdrRole, Resources.UserHdrLang, tname, Convert.ToByte(UserRole.SuperAdmin), string.Empty);
        }

        public static long Count(bool undel = false)
        {
            return Convert.ToInt64(Database.ExecScalar(string.Format("select count(*) from {0} where ROLE <> {1} and {2} DELETED", tname, Convert.ToByte(UserRole.SuperAdmin), undel ? string.Empty : "not")));
        }
        #endregion

        #region Get(), GetAll(), GetUser()
        public static TUser Get(int id)
        {
            TUser res = new TUser();

            using(User user = new User())
            {
                DataTable dt = user.Select("LOGIN, NAME, PSW, ROLE", "not DELETED and ID = " + id.ToString());

                if(dt.Rows.Count > 0) res = new TUser(id, dt.Rows[0]["LOGIN"].ToString(), dt.Rows[0]["NAME"].ToString(), dt.Rows[0]["PWD"].ToString(), dt.Rows[0]["LANG"].ToString(), Convert.ToByte(dt.Rows[0]["ROLE"]));
            }
            return res;
        }

        public static TUser[] GetAll()
        {
            TUser[] res = new TUser[0];

            using(User user = new User()) res = user.Select("ID, LOGIN, NAME, PSW, LANG, ROLE", "not DELETED").Rows.OfType<DataRow>().Select(r => new TUser(Convert.ToInt32(r[0]), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), Convert.ToByte(r[5]))).ToArray();
            return res;
        }

        public static TUser GetUser(DataRow row)
        {
            return new TUser(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[5].ToString(), Convert.ToByte(row[4]));
        }
        #endregion

        #region Add()
        public override void Add()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using(wUser usr = new wUser(true))
            {
                if(usr.ShowDialog() == DialogResult.OK)
                    using(User user = new User())
                        if(user.Insert(string.Format(insFmt, usr.LoginName, usr.UserName, usr.Password, usr.Role, usr.Language)) > 0)
                        {
                            string msg = string.Format(Resources.UserNewMsg, usr.LoginName);

                            Log.InfoToLog(methodName, msg);
                            //Log.InfoToLog(methodName, string.Format("insert into USER" + insFmt, usr.LoginName, usr.UserName, usr.Password, usr.Role, usr.Language));
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
            using(wUser usr = new wUser(false))
            {
                using(User user = new User())
                {
                    DataTable dt = user.Select("*", string.Format("ID={0}", id));

                    if(dt != null && dt.Rows.Count > 0)
                    {
                        usr.LoginName = dt.Rows[0]["LOGIN"].ToString();
                        usr.UserName = dt.Rows[0]["NAME"].ToString();
                        usr.EmptyPsw();
                        usr.Role = Convert.ToByte(dt.Rows[0]["ROLE"]);
                        usr.Language = dt.Rows[0]["LANG"].ToString();
                        if(usr.ShowDialog() == DialogResult.OK)
                        {
                            string where = string.Format(updWhereFmt, id);

                            if(user.Update(string.Format(updFmt, usr.UserName, usr.Role, usr.Language), where))
                            {
                                string msg = string.Format(Resources.UserEditMsg, usr.LoginName);

                                if(!string.IsNullOrEmpty(usr.PlainPassword)) user.Update(string.Format("PSW='{0}'", usr.Password), where);
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

        #region Purge(), Truncate()
        public static void Purge()
        {
            MDMTable.Purge(tname);
        }

        public static void Truncate()
        {
            using(User user = new User())
            {
                DataTable dt = user.Select("ID", string.Format("ROLE <> {0}", Convert.ToByte(UserRole.SuperAdmin)));

                user.Wipe(dt.Rows.OfType<DataRow>().Select(r => r[0]).ToArray());
            }
            //MDMTable.Truncate(tname);
        }
        #endregion
    }
}
