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
                word res = 0;

                if(dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0) res = Convert.ToUInt16(dataGrid.SelectedRows[0].Cells[5].Value);
                //int id = PatientID;
                //word res = 0;

                //PatientCycleNum = 0;
                //if(id > 0)
                //{
                //    int cycle = 0;

                //    res = (word)(Procedure.ProcNum(id, ref cycle) + 1);
                //    if(cycle == 0) cycle++;
                //    PatientCycleNum = (word)cycle;
                //}
                return res;
            }
        }

        //public word PatientCycleNum { get; private set; }
        public word PatientCycleNum
        {
            get
            {
                word res = 0;

                if(dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0) res = Convert.ToUInt16(dataGrid.SelectedRows[0].Cells[4].Value);
                return res;
            }
        }
        #endregion

        public wPatSelect()
        {
            string cmd = string.Format("select P.ID [{0}], LAST_NAME || ', ' || FIRST_NAME || ifnull(' '||MIDDLE_NAME, '') [{1}], strftime('%Y', BIRTHDATE) [{2}], D.NAME [{3}], " +
                                          " ifnull(PR.CYCLE, 1) [{4}], ifnull(PR.NUMBER, 1) [{5}] " +
                                       "from PATIENT P left outer join PROCEDURE PR on P.ID = PR.PAT_ID, DIAGNOSIS D " +
                                       "where P.DG_ID = D.ID and not P.DELETED and not ifnull(PR.FINAL, 'FALSE')",
                Resources.patSelNumber, Resources.patSelName, Resources.patSelYrOfBirth, Resources.PatHdrDg, Resources.patSelCycle, Resources.patSelProcNum);

            InitializeComponent();
            using(SQLiteConnection conn = Database.CreateConnection())
            {
                using(SQLiteDataAdapter da = new SQLiteDataAdapter(cmd, conn)) da.Fill(dataSet);
                bindingSource.DataSource = dataSet.Tables[0].DefaultView;
                dataGrid.DataSource = bindingSource;
            }
            dataGrid.Columns[0].AutoSizeMode = dataGrid.Columns[2].AutoSizeMode = dataGrid.Columns[4].AutoSizeMode = dataGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGrid.Columns[0].Width = 40;
            dataGrid.Columns[4].Width = 50;
            dataGrid.Columns[2].Width = 60;
            dataGrid.Columns[5].Width = 70;
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
