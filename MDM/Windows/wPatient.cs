using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using MDM.Data;
using MDM.Properties;
using MDM.DlgBox;

namespace MDM.Windows
{
    public partial class wPatient : Form
    {
        #region Sloupce
        public string FirstName
        {
            get { return txtFName.Text; }
            set { txtFName.Text = value; }
        }

        public string MiddleName
        {
            get { return txtMName.Text; }
            set { txtMName.Text = value; }
        }

        public string LastName
        {
            get { return txtLName.Text; }
            set { txtLName.Text = value; }
        }

        public string PatientName
        {
            get { return string.Format("{0} {1} {2}", (FirstName ?? string.Empty), (MiddleName ?? string.Empty), (LastName ?? string.Empty)).Trim(); }
        }

        public DateTime BirthDay
        {
            get { return dtpBirthDate.Value; }
            set { dtpBirthDate.Value = value; }
        }

        public Sex Sex
        {
            get { return (Sex)Enum.Parse(typeof(Sex), Convert.ToByte(rbFemale.Checked).ToString()); }
            set { if(value == Sex.Male) rbMale.Checked = true; else rbFemale.Checked = true; }
        }

        public string Address
        {
            get { return txtAddr.Text; }
            set { txtAddr.Text = value; }
        }

        public string City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }

        public string ZIP
        {
            get { return txtZip.Text; }
            set { txtZip.Text = value; }
        }

        public string Country
        {
            get { return txtCountry.Text; }
            set { txtCountry.Text = value; }
        }

        public string Phone
        {
            get { return txtPhone.Text; }
            set { txtPhone.Text = value; }
        }

        public string MedRec
        {
            get { return txtMedRec.Text; }
            set { txtMedRec.Text = value; }
        }

        public string Note
        {
            get { return txtNote.Text; }
            set { txtNote.Text = value; }
        }

        #region Diagnosis
        private TDiagnosis currDg = new TDiagnosis();
        public TDiagnosis Diagnosis
        {
            get
            {
                if(currDg == null || currDg.ID == 0) currDg = Data.Diagnosis.GetFirst();
                return currDg;
            }
            set { currDg = value; DgName = currDg.Name; }
        }

        public string DgName
        {
            get { return txtDiagnosis.Text; }
            set { txtDiagnosis.Text = value; }
        }
        #endregion

        #region HIC
        private THIC hic = new THIC();
        public THIC HIC
        {
            get { return hic; }
            set
            {
                if(hic != value)
                {
                    hic = value;
                    cbxHIC.SelectedItem = cbxHIC.Items.OfType<DataRowView>().FirstOrDefault(r => Convert.ToInt32(r.Row[0]) == hic.Id);
                }
            }
        }

        public string HICName
        {
            get { return cbxHIC.Text; }
        }
        #endregion

        #region Somtype
        private Somatotype somtype = Somatotype.Normotype;
        public Somatotype Somtype
        {
            get { return somtype; }
            set
            {
                somtype = value;
                rbHyperergic.Checked = (somtype == Somatotype.Hyperergic);
                rbNormotype.Checked = (somtype == Somatotype.Normotype);
                rbHypotype.Checked = (somtype == Somatotype.Hypotype);
            }
        }
        #endregion
        #endregion

        public wPatient(bool newOne = true)
        {
            InitializeComponent();

            //using(HIC hic = new HIC()) cbxHIC.DataSource = hic.Select("ID, NAME, CODE, trim(CODE)||' - '||trim(NAME) DISPNAME");
            //cbxHIC.DisplayMember = "DISPNAME";
            if(newOne)
            {
                Text = Resources.PatNewHdr;
                //cbxHIC.SelectedIndex = 0;
                Somtype = Somatotype.Normotype;
                DgName = Diagnosis.Name;
            }
            else
            {
                Text = Resources.PatEditHdr;
                txtFName.Enabled = txtMName.Enabled = txtLName.Enabled = gbSex.Enabled = txtDiagnosis.Enabled =
                    cbDiagnosis.Enabled = gbSomatotype.Enabled = dtpBirthDate.Enabled = false;
            }
        }

        private void cbxHIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow row = (cbxHIC.SelectedItem as DataRowView).Row;

            HIC = new THIC(Convert.ToInt32(row["ID"]), row["NAME"].ToString(), row["CODE"].ToString());
        }

        private void cbDiagnosis_Click(object sender, EventArgs e)
        {
            using(wDiagnosis frm = new wDiagnosis())
            {
                frm.Diagnosis = Diagnosis;
                if(frm.ShowDialog() == DialogResult.OK) Diagnosis = frm.Diagnosis;
            }
        }

        private void somtype_Click(object sender, EventArgs e)
        {
            somtype = sender == rbHyperergic ? Somatotype.Hyperergic : sender == rbHypotype ? Somatotype.Hypotype : Somatotype.Normotype;
        }

        private void wPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult == DialogResult.OK)
            {
                int age = DateTime.Now.Year - BirthDay.Year;

                if(string.IsNullOrEmpty(FirstName))
                {
                    txtFName.Focus();
                    DialogBox.ShowError(Resources.patAddErrFName, Resources.patAddErrHdr).Focus();
                }
                if(string.IsNullOrEmpty(LastName))
                {
                    txtLName.Focus();
                    DialogBox.ShowError(Resources.patAddErrLName, Resources.patAddErrHdr).Focus();
                }
                if(age < 18 || age > 99)
                {
                    dtpBirthDate.Focus();
                    DialogBox.ShowError(Resources.patAddErrBirthDate, Resources.patAddErrHdr).Focus();
                }
                e.Cancel = string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || age < 18 || age > 99;
            }
        }
    }
}
