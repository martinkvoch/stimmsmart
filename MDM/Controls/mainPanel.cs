using MDM.Classes;
using System.Windows.Forms;

namespace MDM.Controls
{
    public partial class mainPanel : MDMPanel
    {
        public Channels Channels;

        public mainPanel()
        {
            InitializeComponent();
            if(Channels != null) DisposeMain();
            try
            {
                Application.UseWaitCursor = true;
                Channels = new Channels(this);
                Channels.On(Program.ChErrors);
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }

        internal void DisposeMain()
        {
            if(Channels != null) Channels.DisposeMain();
            Channels = null;
        }
    }
}
