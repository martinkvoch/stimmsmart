using System;
using System.Windows.Forms;
using MDM.Properties;

namespace MDM.DlgBox
{
    public partial class wTimeoutDlgBox : Form
    {
        public DialogResult Result = DialogResult.OK;

        public wTimeoutDlgBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
    }
}
