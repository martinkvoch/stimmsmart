using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using MDM.Controls;
using MDM.Data;
using MDM.DlgBox;
using MDM.Properties;
using System.Globalization;

namespace MDM.Windows
{
    /// <summary>
    /// Hlavní okno aplikace. Obsahuje všechny komponenty aplikace. Je jazykově flexibilní, jazyk se nastavuje pomocí konfiguračního souboru.
    /// Každá komponenta aplikace je umístěna do panelu. Třída panelu je odvozena od bázové třídy MDMPanel.
    /// </summary>
    public partial class wMain : Form
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}";
        private MDMPanel currentPanel;
        private Timer timer;
        //private System.Threading.Timer timer;

        #region Language, setMILang()
        public string Language
        {
            get { return Program.Language; }
            set
            {
                if(!Program.Language.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    Program.Language = value;
                    setMILang();
                    Program.KeepRunning = true;
                    Close();
                }
            }
        }

        private void setMILang()
        {
            ToolStripMenuItem mi = miLang.DropDownItems.OfType<ToolStripMenuItem>().Where(m => m.Tag.ToString().Equals(Program.Language, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if(mi != null)
            {
                miLang.Image = mi.Image;
                miLang.Tag = mi.Tag;
                miLang.ToolTipText = mi.Text;
            }
        }
        #endregion

        #region Konstruktor
        private void constructMiLang()
        {
            string[] langs = Program.GetLangs();

            if(langs.Length > 0) miLang.DropDownItems.Clear();
            foreach(string l in langs)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem() {
                    Name = "miLang" + l.ToUpper(),
                    Image = (Bitmap)Resources.ResourceManager.GetObject(l),
                    ImageScaling = ToolStripItemImageScaling.None,
                    Text = new CultureInfo(l).NativeName,
                    Tag = l,
                };
                tsi.Click += miLangXX_Click;
                miLang.DropDownItems.Add(tsi);
            }
        }

        /// <summary>
        /// Konstruktor hlavního okna. Zde se provádějí úvodní nastavení.
        /// </summary>
        public wMain(string lang)
        {
            Settings settings = new Settings();
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            Program.SetLanguage();
            MDMTable.MainFrm = this;
            InitializeComponent();
            constructMiLang();
            Language = lang ?? settings.lang;
            setMILang();
            Text = Resources.AppName;
            using(Log log = new Log()) panLog.DBObject = log;
            SwitchToPanel();
            lbKbdLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName.ToUpper();
            if(!Program.KeepRunning)
            {
                signedOut();
                Log.InfoToLog(methodName, string.Format(Resources.startMsg, Text));
            }
            else
            {
                signInUser();
                Log.InfoToLog(methodName, string.Format(Resources.langChanged, miLang.ToolTipText));
            }
        }
        #endregion

        #region Operace s panely
        private void updAllButtons()
        {
            updUserButtons();
            updPatientButtons();
            updRestoreButtons();
            updLogButtons();
        }

        /// <summary>
        /// Přepíná zobrazení na daný panel
        /// </summary>
        /// <param name="pan">panel, který bude zobrazen</param>
        internal void SwitchToPanel(MDMPanel pan = null)
        {
            if(currentPanel != null && currentPanel != panMain)
            { // nejdřív musíme zavřít naposledy otevřený panel...
                currentPanel.Dock = DockStyle.None;
                currentPanel.Visible = false;
                if(currentPanel.PanelMenu != null) currentPanel.PanelMenu.Visible = false;
            }
            pan = pan ?? panMain;
            foreach(MDMPanel p in Controls.OfType<MDMPanel>()) { p.Dock = DockStyle.None; p.Visible = false; }
            pan.Dock = DockStyle.Fill;
            pan.Visible = true;
            pan.Focus();
            currentPanel = pan;
            updAllButtons();
        }
        #endregion

        #region Události hlavního okna
        private void Main_Load(object sender, EventArgs e)
        {
            logoBox.Location = new Point((Width - logoBox.Width) / 2, logoBox.Location.Y);
            if(timer == null)
            {
                timer = new Timer();
                timer.Tick += timer_Tick;
                timer.Interval = 500;
            }
            timer.Start();
            if(!Program.KeepRunning) miLogIn.PerformClick();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss");
            lbKbdLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName.ToUpper();
        }

        private void wMain_Shown(object sender, EventArgs e)
        {
            if(!Program.KeepRunning)
            {
                ShowInStatus(LogTyp.Information, string.Format(Resources.startMsg1, Text));
            }
            else Program.KeepRunning = false;
        }

        private void wMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Program.KeepRunning)
            {
                e.Cancel = (DialogBox.ShowYN(Resources.exitQ, Resources.exitQH) == DialogResult.No);
                if(!e.Cancel)
                {
                    miLogOut.PerformClick();
                    timer.Stop();
                    timer.Dispose();
                    timer = null;
                }
            }
        }
        #endregion

        #region StatusBar
        public void ShowInStatus(LogTyp typ, string msg)
        {
            switch(typ)
            {
                case LogTyp.Warning: lbStatus.ForeColor = Color.DarkGoldenrod; lbStatus.Image = Resources.warning; break;
                case LogTyp.Error: lbStatus.ForeColor = Color.DarkRed; lbStatus.Image = Resources.error; break;
                default: lbStatus.ForeColor = SystemColors.ControlText; lbStatus.Image = Resources.information; break;
            }
            lbStatus.Text = msg;
        }
        #endregion

        #region menu File
        private void miExit_Click(object sender, EventArgs e)
        {
            if(panMain != null && panMain.Channels != null && panMain.Channels.ChannelsOutOfOrder)
            {
                Program.KeepRunning = false;
                Close();
            }
            else DialogBox.ShowWarn(Resources.exitNo, Resources.exitQH);
        }

        private void miRestart_Click(object sender, EventArgs e)
        {
            if(panMain != null && panMain.Channels != null && panMain.Channels.ChannelsOutOfOrder)
            {
                if(DialogBox.ShowYN(Resources.restartQ, Resources.restartQH) == DialogResult.Yes)
                {
                    Program.KeepRunning = false;
                    Program.Restart();
                }
            }
            else DialogBox.ShowWarn(Resources.restartNo, Resources.restartQH);
        }

        private void miSwitchOff_Click(object sender, EventArgs e)
        {
            if(panMain != null && panMain.Channels != null && panMain.Channels.ChannelsOutOfOrder)
            {
                if(DialogBox.ShowYN(Resources.shutdownQ, Resources.shutdownQH) == DialogResult.Yes)
                {
                    Program.KeepRunning = false;
                    Program.Shutdown();
                }
            }
            else DialogBox.ShowWarn(Resources.shutdownNo, Resources.shutdownQH);
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            Program.ShowSplash(false);
        }
        #endregion

        #region menu User
        private void updUserButtons(bool undel = false)
        {
            miEditUser.Enabled = miDeleteUser.Enabled = miWipeUser.Enabled = (!undel && currentPanel == panUser && User.Count() > 0);
            miUndeleteUser.Enabled = User.Count(true) > 0;
        }

        private void applyRole(UserRole role)
        {
            miPatient.Visible = (role != UserRole.NotLogged);
            miUser.Visible = miSystem.Visible = toolStripMenuItem9.Visible = miHIC.Visible = (role != UserRole.NotLogged && role != UserRole.User);
            toolStripMenuItem11.Visible = toolStripMenuItem12.Visible = toolStripMenuItem13.Visible = miDeletePatient.Visible = miUndeletePatient.Visible = miWipePatient.Visible = miPurgePatient.Visible = miTruncatePatient.Visible = (role != UserRole.NotLogged && role != UserRole.User);
            miAdministration.Visible = (role == UserRole.SuperAdmin);
        }

        private void miUserList_Click(object sender, EventArgs e)
        {
            using(User usr = new User()) panUser.Open(usr, PanelLayout.Edit);
            updUserButtons();
        }

        private bool userLogged { get { return !string.IsNullOrEmpty(Program.LoggedUser.Login); } }

        private void signOutUser(string msg = null)
        {
            lbUser.Enabled = false;
            lbUser.Text = msg ?? Resources.signedNotMsg;
            miLogIn.Enabled = tbLogin.Enabled = true;
            miLogOut.Enabled = tbLogout.Enabled = false;
        }

        private void signedOut(string msg = null)
        {
            signOutUser(msg);
            Program.LoggedUser = new TUser();
            applyRole(UserRole.NotLogged);
            panMain.Enabled = false;
            SwitchToPanel();
        }

        private void signInUser()
        {
            if(userLogged)
            {
                lbUser.Enabled = true;
                lbUser.Text = Program.LoggedUser.Login;
                miLogIn.Enabled = tbLogin.Enabled = false;
                miLogOut.Enabled = tbLogout.Enabled = true;
                panMain.Enabled = true;
                applyRole(Program.LoggedUser.Role);
            }
            else signOutUser();
        }

        private void miLogIn_Click(object sender, EventArgs e)
        {
            using(wLogin frm = new wLogin())
            {
                const string login = "Login()";

                switch(frm.ShowDialog())
                {
                    case DialogResult.OK:
                        Program.LoggedUser = frm.User;
                        signInUser();
                        Log.InfoToLog(login, string.Format(Resources.signedInUser, lbUser.Text));
                        Language = Program.LoggedUser.Language;
                        break;
                    case DialogResult.Cancel:
                        if(userLogged) signedOut(Resources.signedOutMsg);
                        Log.ErrorToLog(login, string.Format(Resources.signedUnsuccessful, frm.User.Login));
                        break;
                }
            }
        }

        private void miLogOut_Click(object sender, EventArgs e)
        {
            TUser logusr = Program.LoggedUser;

            signedOut(Resources.signedOutMsg);
            Log.InfoToLog("Logout()", string.Format(Resources.signedOutUser, logusr.Login));
        }

        private void miNewUser_Click(object sender, EventArgs e)
        {
            using(User usr = new User()) usr.Add();
            updUserButtons();
        }

        private void miEditUser_Click(object sender, EventArgs e)
        {
            using(User usr = new User())
            {
                object id = panUser.CurrentID();

                if(id != null)
                {
                    usr.Edit(id);
                    updUserButtons();
                }
            }
        }

        private void miDeleteUser_Click(object sender, EventArgs e)
        {
            using(User usr = new User())
            {
                object id = panUser.CurrentID();

                if(id != null)
                {
                    usr.Delete(id);
                    updUserButtons();
                }
            }
        }

        private void miUndeleteUser_Click(object sender, EventArgs e)
        {
            using(User usr = new User()) panUser.Open(usr, PanelLayout.Undelete, usr.SelectDeleted());
            updUserButtons(true);
        }

        private void miWipeUser_Click(object sender, EventArgs e)
        {
            using(User usr = new User())
            {
                object id = panUser.CurrentID();

                if(id != null)
                {
                    usr.Wipe(id);
                    updUserButtons();
                }
            }
        }

        private void miPurgeUser_Click(object sender, EventArgs e)
        {
            User.Purge();
            updUserButtons();
        }

        private void miTruncateUser_Click(object sender, EventArgs e)
        {
            User.Truncate();
            updUserButtons();
        }
        #endregion

        #region menu Patient
        private void updPatientButtons(bool undel = false)
        {
            miEditPatient.Enabled = miDeletePatient.Enabled = miWipePatient.Enabled = (!undel && currentPanel == panPatient && Patient.Count() > 0);
            miUndeletePatient.Enabled = Patient.Count(true) > 0;
        }

        private void miPatientList_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient()) panPatient.Open(pat, Program.LoggedUser.Role == UserRole.User ? PanelLayout.WODelete : PanelLayout.Edit);
            updPatientButtons();
        }

        private void miNewPatient_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient()) pat.Add();
            updPatientButtons();
        }

        private void miEditPatient_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient())
            {
                object id = panPatient.CurrentID();

                if(id != null)
                {
                    pat.Edit(id);
                    updPatientButtons();
                }
            }
        }

        private void miDeletePatient_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient())
            {
                object id = panPatient.CurrentID();

                if(id != null)
                {
                    pat.Delete(id);
                    updPatientButtons();
                }
            }
        }

        private void miUndeletePatient_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient()) panPatient.Open(pat, PanelLayout.Undelete, pat.SelectDeleted());
            updPatientButtons(true);
        }

        private void miWipePatient_Click(object sender, EventArgs e)
        {
            using(Patient pat = new Patient())
            {
                object id = panPatient.CurrentID();

                if(id != null)
                {
                    pat.Wipe(id);
                    updPatientButtons();
                }
            }
        }

        private void miPurgePatient_Click(object sender, EventArgs e)
        {
            Patient.Purge();
            updPatientButtons();
        }

        private void miTruncatePatient_Click(object sender, EventArgs e)
        {
            Patient.Truncate();
            updPatientButtons();
        }

        private void miPatientProc_Click(object sender, EventArgs e)
        {
            using(Procedure proc = new Procedure()) panProcedure.Open(proc, miProcedure, PanelLayout.WFilter);
            updPatientButtons(true);
        }

        private void miHIC_Click(object sender, EventArgs e)
        {
            using(HIC hic = new HIC()) panHIC.Open(hic, PanelLayout.Edit);
        }
        #endregion

        #region menu System
        private void updRestoreButtons()
        {
            restoreFromThisBackupToolStripMenuItem.Enabled = deleteBackupToolStripMenuItem.Enabled = (panDBRestore.Count > 0);
        }

        private void miBackupDatabase_Click(object sender, EventArgs e)
        {
            Database.Backup();
            panDBRestore.Fill();
        }

        private void miRestoreDatabase_Click(object sender, EventArgs e)
        {
            panDBRestore.Open(miDBRestore);
            updRestoreButtons();
        }

        private void miRestoreBackup_Click(object sender, EventArgs e)
        {
            panDBRestore.cbRestore.PerformClick();
            updRestoreButtons();
        }

        private void miDeleteBackup_Click(object sender, EventArgs e)
        {
            panDBRestore.cbDelete.PerformClick();

            updRestoreButtons();
        }

        private void miMakeNewBackup_Click(object sender, EventArgs e)
        {
            panDBRestore.cbMakeNew.PerformClick();
            updRestoreButtons();
        }

        private void miCloseBackup_Click(object sender, EventArgs e)
        {
            if(currentPanel != null) (currentPanel as dbRestorePanel).Close();
        }

        private void miCompactDatabase_Click(object sender, EventArgs e)
        {
            Database.Compact();
        }
        #endregion

        #region menu Administration
        private void updLogButtons()
        {
            miClearLogInformation.Enabled = (Log.Count(LogTyp.Information) > 0);
            miClearLogWarning.Enabled = (Log.Count(LogTyp.Warning) > 0);
            miClearLogError.Enabled = (Log.Count(LogTyp.Error) > 0);
            miClearLogAll.Enabled = miClearLogSelection.Enabled = (Log.Count() > 0);
        }

        private void miLog_Click(object sender, EventArgs e)
        {
            using(Log log = new Log()) panLog.Open(log, miTheLog, PanelLayout.DeleteOnly);
            updLogButtons();
        }

        private void miClearLogInformation_Click(object sender, EventArgs e)
        {
            Log.WipeInfo();
            updLogButtons();
        }

        private void miClearLogWarning_Click(object sender, EventArgs e)
        {
            Log.WipeWarning();
            updLogButtons();
        }

        private void miClearLogError_Click(object sender, EventArgs e)
        {
            Log.WipeWarning();
            updLogButtons();
        }

        private void miClearLogSelection_Click(object sender, EventArgs e)
        {
            panLog.DeleteSelection();
            updLogButtons();
        }

        private void miClearLogAll_Click(object sender, EventArgs e)
        {
            Log.Truncate();
            updLogButtons();
        }

        private void miCloseLogPanel_Click(object sender, EventArgs e)
        {
            if(currentPanel != null) (currentPanel as DBPanel).Close();
        }

        private void miNewDatabase_Click(object sender, EventArgs e)
        {
            if(DialogBox.ShowYN(Resources.newDataQ, Resources.newDataQH) == DialogResult.Yes)
            {
                Database.Init(true);
                panDBRestore.Fill();
            }
        }
        #endregion

        #region Nastavení jazyka
        private void miLangXX_Click(object sender, EventArgs e)
        {
            Language = (sender as ToolStripMenuItem).Tag.ToString();
        }
        #endregion
    }
}
