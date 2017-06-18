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
        private static int callCount = 0;
        public Channels Channels;

        public mainPanel()
        {
            if(callCount++ < 1)
            {
                InitializeComponent();
                Channels = new Channels(this);
                Channels.On(Program.ChErrors);
            }
        }
    }
}
