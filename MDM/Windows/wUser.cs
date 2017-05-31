using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MDM.Data;
using MDM.Properties;

namespace MDM.Windows
{
    public partial class wUser : Form
    {
        private bool? _pswMatch = null;
        private bool? pswMatch
        {
            get { return _pswMatch; }
            set
            {
                cbSave.Enabled = !value.HasValue || value.Value;
                if(lbPswMatch.Visible = value.HasValue)
                {
                    lbPswMatch.ForeColor = value.Value ? Color.Green : Color.Red;
                    lbPswMatch.Text = value.Value ? Resources.pswMatchMsg : Resources.pswNotMatchMsg;
                }
            }
        }

        public string LoginName
        {
            get { return txtLoginName.Text; }
            set { txtLoginName.Text = value; }
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }

        internal string PlainPassword
        {
            get { return textBox1.Text; }
        }

        public string Password
        {
            get { return EncryptionUtilities.CreatePasswordSalt(PlainPassword); }
            set { textBox1.Text = textBox2.Text = value; }
        }

        public string Language
        {
            get { return cbxLanguage.SelectedIndex == 0 ? new Settings().lang : cbxLanguage.SelectedItem.ToString().Substring(0, 2); }
            set { cbxLanguage.SelectedItem = cbxLanguage.Items.OfType<object>().First(i => i.ToString().StartsWith(value)); }
        }

        public byte Role
        {
            get { return Convert.ToByte(rbAdmin.Checked); }
            set { rbUser.Checked = (value == 0); rbAdmin.Checked = (value == 1); }
        }

        public void EmptyPsw() { Password = string.Empty; pswMatch = null; }

        public wUser(bool newOne = true)
        {
            const string langFmt = "{0} - {1}";
            wMain main = (Application.OpenForms[0] as wMain);

            InitializeComponent();

            cbxLanguage.Items.Add(string.Format(langFmt, new Settings().lang, Resources.curLang1));
            cbxLanguage.Items.AddRange(main.miLang.DropDownItems.OfType<ToolStripMenuItem>().Select(i => string.Format(langFmt, i.Tag, i.Text)).ToArray());
            cbxLanguage.SelectedIndex = 0;

            pswMatch = null;
            if(newOne)
            {
                Text = Resources.UserNewHdr;
                txtLoginName.Enabled = true;
            }
            else
            {
                Text = Resources.UserEditHdr;
                txtLoginName.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pswMatch = textBox1.Text.Equals(textBox2.Text, StringComparison.Ordinal);
        }

        private void cbEmptyPsw_Click(object sender, EventArgs e)
        {
            EmptyPsw();
        }
    }
}
