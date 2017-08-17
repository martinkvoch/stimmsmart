using System.Windows.Forms;

namespace MDM.DlgBox
{
    public partial class wWaitBox : Form
    {
        private wWaitBox(string msg)
        {
            InitializeComponent();
            lbWaitMsg.Text = msg;
        }

        public static wWaitBox Show(string fmt, params object[] args)
        {
            wWaitBox res = new wWaitBox(string.Format(fmt, args));

            res.Show();
            res.Refresh();
            return res;
        }
    }
}
