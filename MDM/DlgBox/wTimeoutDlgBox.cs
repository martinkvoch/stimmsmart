using LANlib;
using System;
using System.Windows.Forms;

namespace MDM.DlgBox
{
    public partial class wTimeoutDlgBox : Form
    {
        public DialogResult Result = DialogResult.Cancel;

        public wTimeoutDlgBox()
        {
            InitializeComponent();
            Sound.Beep();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close();
        }
    }
}
