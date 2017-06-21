using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

using MDM.Data;
using MDM.Properties;


namespace MDM.Windows
{
    using word = UInt16;
    using dword = UInt32;

    public partial class wPatSelect : Form
    {
        private DataSet dataSet = new DataSet();
        private BindingSource bindingSource = new BindingSource();

        #region Vlastnosti
        public int PatientID
        {
            get
            {
                int res = -1;

                if(dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0) res = Convert.ToInt32(dataGrid.SelectedRows[0].Cells[0].Value);
                return res;
            }
        }

        public string PatientName
        {
            get
            {
                string res = string.Empty;

                if(dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0) res = dataGrid.SelectedRows[0].Cells[1].Value.ToString();
                return res;
            }
        }

        public string PatientDiagnosis
        {
            get
            {
                string res = string.Empty;

                if(dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0) res = dataGrid.SelectedRows[0].Cells[3].Value.ToString();
                return res;
            }
        }

        public word PatientProcNum
        {
            get
            {
                int id = PatientID;
                word res = 0;

                PatientCycleNum = 0;
                if(id > 0)
                {
                    int cycle = 0;

                    res = (word)(Procedure.ProcNum(id, ref cycle) + 1);
                    if(cycle == 0) cycle++;
                    PatientCycleNum = (word)cycle;
                }
                return res;
            }
        }

        public word PatientCycleNum { get; private set; }
        #endregion

        public wPatSelect()
        {
            //string cmd = string.Format("select P.ID [{0}], LAST_NAME || ', ' || FIRST_NAME || ifnull(' '||MIDDLE_NAME, '') [{1}], strftime('%Y', BIRTHDATE) [{2}], D.NAME [{3}] " +
            //                           "from PATIENT P, DIAGNOSIS D, PROCEDURE R " +
            //                           "where P.DG_ID = D.ID and R.PAT_ID = P.ID and not P.DELETED",
            string cmd = string.Format("select P.ID [{0}], LAST_NAME || ', ' || FIRST_NAME || ifnull(' '||MIDDLE_NAME, '') [{1}], strftime('%Y', BIRTHDATE) [{2}], D.NAME [{3}] " +
                                       "from PATIENT P, DIAGNOSIS D " +
                                       "where P.DG_ID = D.ID and not P.DELETED",
                Resources.patSelNumber, Resources.patSelName, Resources.patSelYrOfBirth, Resources.PatHdrDg);

            InitializeComponent();
            using(SQLiteConnection conn = Database.CreateConnection())
            {
                using(SQLiteDataAdapter da = new SQLiteDataAdapter(cmd, conn)) da.Fill(dataSet);
                bindingSource.DataSource = dataSet.Tables[0].DefaultView;
                dataGrid.DataSource = bindingSource;
            }
        }

        private void txtFindName_TextChanged(object sender, EventArgs e)
        {
            bindingSource.Filter = string.IsNullOrEmpty(txtFindName.Text) ? string.Empty : string.Format("[{0}] like '{1}%'", Resources.patSelName, txtFindName.Text);
        }

        private void dataGrid_DoubleClick(object sender, EventArgs e)
        {
            cbSelect.PerformClick();
        }
    }
}
