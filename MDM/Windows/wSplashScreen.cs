using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using MDM.Data;
using MDM.Properties;
using MDM.Classes;
using MDM.DlgBox;
using MDM.HASP;

namespace MDM
{
    public partial class wSplashScreen : Form
    {
        const short DELAY = 500;
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}";
        private bool onInit = true;

        public wSplashScreen(bool onInit = true)
        {
            Settings settings = new Settings();

            this.onInit = onInit;
            InitializeComponent();
            btnEsc.Visible = !onInit;
            Cursor = onInit ? Cursors.AppStarting : Cursors.Arrow;
            lbAppName.Text = Resources.AppName;
            lbVersion.Text = string.Format(lbVersion.Text, Program.GetVersion(), Environment.Is64BitProcess ? 64 : 32);
            lbManAddr.Text = Program.GetManAddr();
            lbProtBy.Text = settings.lang.Equals("ru", StringComparison.OrdinalIgnoreCase) ? settings.ProtByRU : settings.ProtBy;
            lbCopyright.Text = Program.GetCopyright();
            lbSerNum.Text = Program.GetSerNum();
            //Procedure.Init();
        }

        private void setProgress(string msg)
        {
            lbProgress.Text = msg;
            Refresh();
        }

        private void startup()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            Settings settings = new Settings();

            setProgress(Resources.startup);
            Log.InfoToLog(methodName, string.Format(Resources.startInitMsg, Resources.AppName) + ", " + string.Format(Resources.curLang, settings.lang) + ", " + string.Format(Resources.numberOfChannels, Math.Min(settings.NOC, Program.MOC)));
            Thread.Sleep(DELAY);
        }

        private void testDongle()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            setProgress(Resources.testDongle);
            Program.KeepRunning = MDMHASP.HaspCheck();
            Log.InfoToLog(methodName, Program.KeepRunning ? Resources.testDongleOK : Resources.testDongleOK);
            Thread.Sleep(DELAY);
        }

        private void testChannel()
        {
            setProgress(Resources.testChannel);
            Program.ChErrors = Channels.Autotest();
        }

        private void finishInit()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            setProgress(Resources.finish);
            Log.InfoToLog(methodName, Resources.finish);
            Thread.Sleep(DELAY);
        }

        private void wSplashScreen_Load(object sender, EventArgs e)
        {
            lbSerNum.Text = MDMHASP.HaspGet();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            //string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if(onInit)
            {
                Cursor = Cursors.AppStarting;
                startup();
                testDongle();
                if(Program.KeepRunning)
                {
                    testChannel();
                    finishInit();
                }
                else DialogBox.ShowErrorModal(Resources.testDongleKOmsg, Resources.testDongleKOhdr);
                Close();
            }
            else
            {
                Cursor = Cursors.Arrow;
                setProgress(Resources.pressEscButton);
            }
        }
    }
}
