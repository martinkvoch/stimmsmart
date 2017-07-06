using MDM.Classes;

namespace MDM.Controls
{
    public partial class mainPanel : MDMPanel
    {
        //private static int callCount = 0;
        public Channels Channels;

        public mainPanel()
        {
            //if(callCount++ < 1)
            //{
            InitializeComponent();
            if(Channels != null)
            {
                Channels.Dispose();
                Channels = null;
            }
            Channels = new Channels(this);
            Channels.On(Program.ChErrors);
            //}
        }
    }
}
