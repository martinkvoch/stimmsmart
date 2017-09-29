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
    public enum Somatotype { Hyperergic, Normotype, Hypotype };
    public enum Sex { Male, Female };

    #region struct TPatient
    public struct TPatient
    {
        public int ID;
        public string FirstName, MiddleName, LastName;
        public DateTime? BirthDate;
        public Sex Sex;
        public string Address, City, ZipCode, Country, Phone, MedRec, Note;
        public int? DgID;//, HICID;
        public Somatotype? SomType;

        public TPatient(int id, 
            string fname = null, string mname = null, string lname = null,
            DateTime? bdate = null, 
            Sex sex = Sex.Male, 
            string addr = null, string city = null, string zip = null, string country = null, string phone = null, string medrec = null, string note = null, 
            int? dgid = null, //int? hicid = null, 
            Somatotype? somtype = null)
        {
            ID = id;
            FirstName = fname;
            MiddleName = mname;
            LastName = lname;
            BirthDate = bdate;
            Sex = sex;
            Address = addr;
            City = city;
            ZipCode = zip;
            Country = country;
            Phone = phone;
            MedRec = medrec;
            Note = note;
            DgID = dgid;
            //HICID = hicid;
            SomType = somtype;
        }
    }
    #endregion

    public class Patient : MDMTable
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}", panControl = "panPatient", updWhereFmt = "ID = {0}",
             selFmt = "select PAT.ID, " +
                     "PAT.FIRST_NAME [{0}], PAT.MIDDLE_NAME [{1}], PAT.LAST_NAME [{2}], " +
                     "PAT.BIRTHDATE [{3}], " +
                     "case PAT.SEX when 1 then '{4}' else '{5}' end [{6}], " +
                     "PAT.ADDRESS [{7}], PAT.CITY [{8}], PAT.ZIPCODE [{9}], PAT.COUNTRY [{10}], PAT.PHONE [{11}], PAT.MEDICAL_RECORD [{12}], PAT.NOTE [{13}], " +
                     "DG.NAME [{14}], " +
                     //"HIC.NAME [{15}], " +
                     "case PAT.SOMATOTYPE when 0 then '{16}' when 1 then '{17}' else '{18}' end [{19}] " +
                 "from {20} PAT, {21} DG, {22} HIC " +
                 "where PAT.DG_ID = DG.ID and PAT.HIC_ID = HIC.ID and {23} PAT.DELETED order by 4";
        internal const string TName = "PATIENT";

        #region Init()
        public static void Init()
        {
            Database.ExecCmd("drop table if exists " + TName);
            Database.ExecCmd(string.Format("create table " + TName + " (ID integer primary key, " +
                "FIRST_NAME varchar(100) not null, " +
                "MIDDLE_NAME varchar(100), " +
                "LAST_NAME varchar(100) not null, " +
                "BIRTHDATE date not null, " +
                "SEX byte not null default 0 check(SEX between {0} and {1}), " +
                "ADDRESS varchar(255), " +
                "CITY varchar(100), " +
                "ZIPCODE varchar(10), " +
                "COUNTRY char(2), " +
                "PHONE varchar(50), " +
                "MEDICAL_RECORD varchar(50), " +
                "DG_ID integer constraint PATIENT_DIAGNOSIS_FKEY references {2}(ID), " +
                "HIC_ID integer constraint PATIENT_HIC_FKEY references {3}(ID), " +
                "SOMATOTYPE byte not null default 1 check(SOMATOTYPE between {4} and {5}), " +
                "NOTE text, " +
                "DELETED boolean default FALSE)",
                (byte)Enum.GetValues(typeof(Sex)).Cast<Sex>().First(), (byte)Enum.GetValues(typeof(Sex)).Cast<Sex>().Last(), Diagnosis.TName, HIC.TName,
                (byte)Enum.GetValues(typeof(Somatotype)).Cast<Somatotype>().First(), (byte)Enum.GetValues(typeof(Somatotype)).Cast<Somatotype>().Last()));
        }
        #endregion

        public Patient() : base(TName) { }

        #region SelectCmd(), SelectDeleted(), Count()
        public override string SelectCmd()
        {
            return string.Format(selFmt, Resources.PatHdrFName, Resources.PatHdrMName, Resources.PatHdrLName,
                Resources.PatHdrBirthDate,
                Resources.PatSexFemale, Resources.PatSexMale, Resources.PatHdrSex,
                Resources.PatHdrAddr, Resources.PatHdrCity, Resources.PatHdrZip, Resources.PatHdrCountry, Resources.PatHdrPhone, Resources.PatHdrMedRec, Resources.PatHdrNote,
                Resources.PatHdrDg, Resources.PatHdrHIC, Resources.PatSomTypHyperergic, Resources.PatSomTypNormotype, Resources.PatSomTypHypotype, Resources.PatHdrSomTyp, TName, Diagnosis.TName, HIC.TName, "not");
        }

        public string SelectDeleted()
        {
            return string.Format(selFmt, Resources.PatHdrFName, Resources.PatHdrMName, Resources.PatHdrLName,
                Resources.PatHdrBirthDate,
                Resources.PatSexFemale, Resources.PatSexMale, Resources.PatHdrSex,
                Resources.PatHdrAddr, Resources.PatHdrCity, Resources.PatHdrZip, Resources.PatHdrCountry, Resources.PatHdrPhone, Resources.PatHdrMedRec, Resources.PatHdrNote,
                Resources.PatHdrDg, Resources.PatHdrHIC, Resources.PatSomTypHyperergic, Resources.PatSomTypNormotype, Resources.PatSomTypHypotype, Resources.PatHdrSomTyp, TName, Diagnosis.TName, HIC.TName, string.Empty);
        }

        public static long Count(bool undel = false, int? id = null)
        {
            return Convert.ToInt64(Database.ExecScalar(string.Format("select count(*) from {0} where {1} DELETED {2}", TName, undel ? string.Empty : "not", id.HasValue ? "and ID = "+id.Value.ToString() : string.Empty)));
        }
        #endregion

        #region insertFmt(), updateFmt()
        private string insertFmt(string fname = null, string mname = null, string lname = null,
            DateTime? bdate = null, 
            Sex sex = Sex.Male, 
            string addr = null, string city = null, string zip = null, string country = null, string phone = null, string medrec = null, string note = null, 
            int? dgid = null, //int? hicid = null, 
            Somatotype? somtype = null)
        {
            const string delim = ", ";
            string ins = string.Empty, val = string.Empty;

            if(!string.IsNullOrEmpty(fname)) { ins += "FIRST_NAME" + delim; val += string.Format("'{0}'" + delim, fname); }
            if(!string.IsNullOrEmpty(mname)) { ins += "MIDDLE_NAME" + delim; val += string.Format("'{0}'" + delim, mname); }
            if(!string.IsNullOrEmpty(lname)) { ins += "LAST_NAME" + delim; val += string.Format("'{0}'" + delim, lname); }
            if(bdate.HasValue) { ins += "BIRTHDATE" + delim; val += string.Format("'{0}'" + delim, bdate.Value.ToString("yyyy-MM-dd")); }
            ins += "SEX" + delim; val += string.Format("{0}" + delim, (byte)sex);
            if(!string.IsNullOrEmpty(addr)) { ins += "ADDRESS" + delim; val += string.Format("'{0}'" + delim, addr); }
            if(!string.IsNullOrEmpty(city)) { ins += "CITY" + delim; val += string.Format("'{0}'" + delim, city); }
            if(!string.IsNullOrEmpty(zip)) { ins += "ZIPCODE" + delim; val += string.Format("'{0}'" + delim, zip); }
            if(!string.IsNullOrEmpty(country)) { ins += "COUNTRY" + delim; val += string.Format("'{0}'" + delim, country); }
            if(!string.IsNullOrEmpty(phone)) { ins += "PHONE" + delim; val += string.Format("'{0}'" + delim, phone); }
            if(!string.IsNullOrEmpty(medrec)) { ins += "MEDICAL_RECORD" + delim; val += string.Format("'{0}'" + delim, medrec); }
            if(!string.IsNullOrEmpty(note)) { ins += "NOTE" + delim; val += string.Format("'{0}'" + delim, note); }
            if(dgid.HasValue) { ins += "DG_ID" + delim; val += string.Format("{0}" + delim, dgid.Value); }
            //if(hicid.HasValue) { ins += "HIC_ID" + delim; val += string.Format("{0}" + delim, hicid.Value); }
            if(somtype.HasValue) { ins += "SOMATOTYPE" + delim; val += string.Format("{0}" + delim, (byte)somtype.Value); }

            if(ins.EndsWith(delim)) ins = ins.Substring(0, ins.Length - delim.Length);
            if(val.EndsWith(delim)) val = val.Substring(0, val.Length - delim.Length);
            return string.Format("({0}) values ({1})", ins, val);
        }
        
        private string updateFmt(string fname = null, string mname = null, string lname = null,
            DateTime? bdate = null,
            Sex sex = Sex.Male,
            string addr = null, string city = null, string zip = null, string country = null, string phone = null, string medrec = null, string note = null,
            int? dgid = null, //int? hicid = null,
            Somatotype? somtype = null)
        {
            const string delim = ", ";
            string res = string.Empty;

            if(!string.IsNullOrEmpty(fname)) { res += string.Format("FIRST_NAME='{0}'" + delim, fname); }
            if(!string.IsNullOrEmpty(mname)) { res += string.Format("MIDDLE_NAME='{0}'" + delim, mname); }
            if(!string.IsNullOrEmpty(lname)) { res += string.Format("LAST_NAME='{0}'" + delim, lname); }
            if(bdate.HasValue) { res += string.Format("BIRTHDATE='{0}'" + delim, bdate.Value.ToString("yyyy-MM-dd")); }
            res += string.Format("SEX={0}" + delim, (byte)sex);
            if(!string.IsNullOrEmpty(addr)) { res += string.Format("ADDRESS='{0}'" + delim, addr); }
            if(!string.IsNullOrEmpty(city)) { res += string.Format("CITY='{0}'" + delim, city); }
            if(!string.IsNullOrEmpty(zip)) { res += string.Format("ZIPCODE='{0}'" + delim, zip); }
            if(!string.IsNullOrEmpty(country)) { res += string.Format("COUNTRY='{0}'" + delim, country); }
            if(!string.IsNullOrEmpty(phone)) { res += string.Format("PHONE='{0}'" + delim, phone); }
            if(!string.IsNullOrEmpty(medrec)) { res += string.Format("MEDICAL_RECORD='{0}'" + delim, medrec); }
            if(!string.IsNullOrEmpty(note)) { res += string.Format("NOTE='{0}'" + delim, note); }
            if(dgid.HasValue) { res += string.Format("DG_ID={0}" + delim, dgid.Value); }
            //if(hicid.HasValue) { res += string.Format("HIC_ID={0}" + delim, hicid.Value); }
            if(somtype.HasValue) { res += string.Format("SOMATOTYPE={0}" + delim, (byte)somtype.Value); }

            if(res.EndsWith(delim)) res = res.Substring(0, res.Length - delim.Length);
            return res;
        }
        #endregion

        #region Get(), GetAll(), GetUser()
        //public static TUser Get(int id)
        //{
        //    TUser res = new TUser();

        //    using(User user = new User())
        //    {
        //        DataTable dt = user.Select("LOGIN, NAME, PSW, ROLE", "not DELETED and ID = " + id.ToString());

        //        if(dt.Rows.Count > 0) res = new TUser(id, dt.Rows[0]["LOGIN"].ToString(), dt.Rows[0]["NAME"].ToString(), dt.Rows[0]["PWD"].ToString(), dt.Rows[0]["LANG"].ToString(), Convert.ToByte(dt.Rows[0]["ROLE"]));
        //    }
        //    return res;
        //}

        //public static TUser[] GetAll()
        //{
        //    TUser[] res = new TUser[0];

        //    using(User user = new User()) res = user.Select("ID, LOGIN, NAME, PSW, LANG, ROLE", "not DELETED").Rows.OfType<DataRow>().Select(r => new TUser(Convert.ToInt32(r[0]), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), Convert.ToByte(r[5]))).ToArray();
        //    return res;
        //}

        //public static TUser GetUser(DataRow row)
        //{
        //    return new TUser(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[5].ToString(), Convert.ToByte(row[4]));
        //}
        #endregion

        #region Add()
        public override void Add()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using(wPatient frm = new wPatient(true))
            {
                if(frm.ShowDialog() == DialogResult.OK)
                    using(Patient pat = new Patient())
                        if(pat.Insert(string.Format(insertFmt(frm.FirstName, frm.MiddleName, frm.LastName, frm.BirthDay, frm.Sex,
                            frm.Address, frm.City, frm.ZIP, frm.Country, frm.Phone, frm.MedRec, frm.Note, frm.Diagnosis.ID, /*frm.HIC.Id,*/ frm.Somtype))) > 0)
                        {
                            string msg = string.Format(Resources.PatNewMsg, frm.PatientName);

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
            using(wPatient frm = new wPatient(false))
            {
                using(Patient pat = new Patient())
                {
                    DataTable dt = pat.Select("*", string.Format("ID={0}", id));

                    if(dt != null && dt.Rows.Count > 0)
                    {
                        frm.FirstName = dt.Rows[0]["FIRST_NAME"].ToString();
                        frm.MiddleName = dt.Rows[0]["MIDDLE_NAME"].ToString();
                        frm.LastName = dt.Rows[0]["LAST_NAME"].ToString();
                        frm.BirthDay = Convert.ToDateTime(dt.Rows[0]["BIRTHDATE"]);
                        frm.Sex = (Sex)Enum.Parse(typeof(Sex), dt.Rows[0]["SEX"].ToString());
                        frm.Address = dt.Rows[0]["ADDRESS"].ToString();
                        frm.City = dt.Rows[0]["CITY"].ToString();
                        frm.ZIP = dt.Rows[0]["ZIPCODE"].ToString();
                        frm.Country = dt.Rows[0]["COUNTRY"].ToString();
                        frm.Phone = dt.Rows[0]["PHONE"].ToString();
                        frm.MedRec = dt.Rows[0]["MEDICAL_RECORD"].ToString();
                        frm.Note = dt.Rows[0]["NOTE"].ToString();
                        frm.Diagnosis = Diagnosis.Get(Convert.ToInt32(dt.Rows[0]["DG_ID"]));
                        //frm.HIC = HIC.Get(Convert.ToInt32(dt.Rows[0]["HIC_ID"]));
                        frm.Somtype = (Somatotype)Enum.Parse(typeof(Somatotype), dt.Rows[0]["SOMATOTYPE"].ToString());
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            if(pat.Update(updateFmt(frm.FirstName, frm.MiddleName, frm.LastName, frm.BirthDay, frm.Sex, frm.Address, frm.City, frm.ZIP, frm.Country, frm.Phone, frm.MedRec, frm.Note, frm.Diagnosis.ID, /*frm.HIC.Id,*/ frm.Somtype), string.Format(updWhereFmt, id)))
                            {
                                string msg = string.Format(Resources.PatEditMsg, frm.PatientName);

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
            MDMTable.Purge(TName);
        }

        public static void Truncate()
        {
            MDMTable.Truncate(TName);
        }
        #endregion

        #region Age()
        //public static byte Age(int patientID)
        //{
        //    byte res = 0;

        //    using(Patient pat = new Patient())
        //    {
        //        object obj = Database.ExecScalar(string.Format("select BIRTHDATE from {0} where ID = {1}", TName, patientID));
        //        //object obj = Database.ExecScalar(string.Format("select CURRENT_DATE - BIRTHDATE from {0} where ID = {1}", TName, patientID));
        //        DateTime dt = Convert.ToDateTime(obj);

        //        res = Convert.ToByte(53);
        //    }
        //    //using(Patient pat = new Patient()) res = Convert.ToByte(Database.ExecScalar(string.Format("select CURRENT_DATE - BIRTHDATE from {0} where ID = {1}", TName, patientID)));
        //    return res;
        //}
        #endregion

        #region Exists()
        //public static bool Exists(int id)
        //{
        //    return Count(false, id) > 0L;
        //}
        #endregion
    }
}
