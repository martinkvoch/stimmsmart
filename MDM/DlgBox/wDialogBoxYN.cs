using System;
using System.Windows.Forms;

namespace MDM.DlgBox
{
    public partial class wDialogBoxYN : Form
    {
        public DialogResult Result = DialogResult.No;

        public string Message
        {
            get { return lbMessage.Text; }
            set { lbMessage.Text = value; }
        }

        public wDialogBoxYN()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Result = DialogResult.No;
            Close();
        }
    }
}
