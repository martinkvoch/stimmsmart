using MDM.Classes;

namespace MDM.Controls
{
    public partial class mainPanel : MDMPanel
    {
        public Channels Channels;

        public mainPanel()
        {
            InitializeComponent();
            if(Channels != null) DisposeMain();
            Channels = new Channels(this);
            Channels.On(Program.ChErrors);
        }

        internal void DisposeMain()
        {
            Channels.DisposeMain();
            Channels = null;
        }
    }
}
