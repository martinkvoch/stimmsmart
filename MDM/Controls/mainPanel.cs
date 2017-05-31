using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDM.Classes;

namespace MDM.Controls
{
    public partial class mainPanel : MDMPanel
    {
        public Channels Channels;

        public mainPanel()
        {
            InitializeComponent();
            Channels = new Channels(this);
            Channels.On(Program.ChErrors);
        }

        private void mainPanel_EnabledChanged(object sender, EventArgs e)
        {
            Channels.Enabled = Enabled;
            if(Enabled) Channels.Deactivation(); else Channels.Reset();
        }
    }
}
