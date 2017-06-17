using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDM.DlgBox
{
    public partial class wWaitBox : Form
    {
        public wWaitBox(string msg)
        {
            InitializeComponent();
            lbWaitMsg.Text = msg;
        }

        public static wWaitBox Show(string msg)
        {
            wWaitBox res = new wWaitBox(msg);

            res.Show();
            res.Refresh();
            return res;
        }
    }
}
