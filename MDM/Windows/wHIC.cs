using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDM.Data;
using MDM.Properties;

namespace MDM.Windows
{
    public partial class wHIC : Form
    {
        public string HICName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string HICode
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }

        private THIC hic = new THIC();
        public THIC HIC
        {
            get { return hic; }
            set
            {
                hic = value;
                HICName = hic.Name;
                HICode = hic.Code;
            }
        }

        public wHIC(bool newOne = true)
        {
            InitializeComponent();
            Text = newOne ? Resources.HICNewHdr : Resources.HICEditHdr;
            HICName = hic.Name;
            HICode = hic.Code;
        }
    }
}
