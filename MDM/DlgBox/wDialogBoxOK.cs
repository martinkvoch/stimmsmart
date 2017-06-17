using System;
using System.Windows.Forms;
using MDM.Properties;

namespace MDM.DlgBox
{
    public partial class wDialogBoxOK : Form
    {
        public DialogResult Result = DialogResult.OK;

        public string Message
        {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value; }
        }

        public void SetIcon(MessageBoxIcon icon)
        {
            switch(icon)
            {
                case MessageBoxIcon.Error: imgClass.Image = Resources.error; break;
                case MessageBoxIcon.Warning: imgClass.Image = Resources.warning; break;
                default: imgClass.Image = Resources.info; break;
            }
        }

        public wDialogBoxOK()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close();
        }
    }
}
