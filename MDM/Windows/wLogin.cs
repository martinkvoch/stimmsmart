using System;
using System.Data;
using System.Windows.Forms;

using MDM.Data;

namespace MDM.Windows
{
    public partial class wLogin : Form
    {
        public TUser User { get { return Data.User.GetUser((cbxUserName.SelectedItem as DataRowView).Row); } }
        public byte Role
        {
            get
            {
                byte res = 0;

                return res;
            }
        }

        public wLogin()
        {
            InitializeComponent();
            txtPassword.Text = string.Empty;
            cbSignIn.Enabled = false;
            using(User user = new User()) cbxUserName.DataSource = user.Select("ID, LOGIN, NAME, PSW, ROLE, LANG", "not DELETED");
            cbxUserName.DisplayMember = "LOGIN";
            cbxUserName.SelectedIndex = cbxUserName.Items.Count > 1 ? 1 : 0;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string psw = txtPassword.Text.Trim(), hashpsw = ((DataRowView)cbxUserName.SelectedItem).Row["PSW"].ToString();

            cbSignIn.Enabled = !string.IsNullOrEmpty(psw) && 
                (
                    EncryptionUtilities.IsPasswordValid(psw, hashpsw) ||
                    EncryptionUtilities.IsPasswordValid(psw.Replace('y', 'z'), hashpsw) ||
                    EncryptionUtilities.IsPasswordValid(psw.Replace('z', 'y'), hashpsw)
                );
        }
    }
}
