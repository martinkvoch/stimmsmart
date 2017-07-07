using MDM.Controls;
using MDM.Windows;
namespace MDM.Windows
{
    partial class wMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wMain));
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbKbdLang = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbClock = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tbAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbShutdown = new System.Windows.Forms.ToolStripButton();
            this.tbRestart = new System.Windows.Forms.ToolStripButton();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.tbLogin = new System.Windows.Forms.ToolStripButton();
            this.tbLogout = new System.Windows.Forms.ToolStripButton();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.miSwitchOff = new System.Windows.Forms.ToolStripMenuItem();
            this.miUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miUserList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.miLogIn = new System.Windows.Forms.ToolStripMenuItem();
            this.miLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.miDeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miWipeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.miPurgeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miTruncateUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.miPatientList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.miDeletePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndeletePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.miWipePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.miPurgePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.miTruncatePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.miHIC = new System.Windows.Forms.ToolStripMenuItem();
            this.miSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackupDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.miRestoreDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.miLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.miCompactDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.miTheLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearLogInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearLogWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearLogError = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearLogSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearLogAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miClosePanel = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.miLang = new System.Windows.Forms.ToolStripMenuItem();
            this.miLangCS = new System.Windows.Forms.ToolStripMenuItem();
            this.miLangEN = new System.Windows.Forms.ToolStripMenuItem();
            this.miLangRU = new System.Windows.Forms.ToolStripMenuItem();
            this.miDBRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreFromThisBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeNewBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.panPatient = new MDM.Controls.DBPanel();
            this.panHIC = new MDM.Controls.DBPanel();
            this.panUser = new MDM.Controls.DBPanel();
            this.panDBRestore = new MDM.Controls.dbRestorePanel();
            this.panLog = new MDM.Controls.DBPanel();
            this.panMain = new MDM.Controls.mainPanel();
            this.ssMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            resources.ApplyResources(this.ssMain, "ssMain");
            this.ssMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus,
            this.lbKbdLang,
            this.lbUser,
            this.lbClock});
            this.ssMain.Name = "ssMain";
            this.ssMain.SizingGrip = false;
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbStatus.Image = global::MDM.Properties.Resources.information;
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Spring = true;
            // 
            // lbKbdLang
            // 
            resources.ApplyResources(this.lbKbdLang, "lbKbdLang");
            this.lbKbdLang.Name = "lbKbdLang";
            // 
            // lbUser
            // 
            resources.ApplyResources(this.lbUser, "lbUser");
            this.lbUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbUser.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbUser.Image = global::MDM.Properties.Resources.user;
            this.lbUser.Name = "lbUser";
            // 
            // lbClock
            // 
            resources.ApplyResources(this.lbClock, "lbClock");
            this.lbClock.Image = global::MDM.Properties.Resources.clock;
            this.lbClock.Name = "lbClock";
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAbout,
            this.toolStripSeparator3,
            this.tbShutdown,
            this.tbRestart,
            this.tbExit,
            this.tbLogin,
            this.tbLogout});
            resources.ApplyResources(this.tsMain, "tsMain");
            this.tsMain.Name = "tsMain";
            // 
            // tbAbout
            // 
            this.tbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAbout.Image = global::MDM.Properties.Resources.about;
            resources.ApplyResources(this.tbAbout, "tbAbout");
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tbShutdown
            // 
            this.tbShutdown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbShutdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbShutdown.Image = global::MDM.Properties.Resources.switchoff;
            resources.ApplyResources(this.tbShutdown, "tbShutdown");
            this.tbShutdown.Name = "tbShutdown";
            this.tbShutdown.Click += new System.EventHandler(this.miSwitchOff_Click);
            // 
            // tbRestart
            // 
            this.tbRestart.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbRestart.Image = global::MDM.Properties.Resources.restart;
            resources.ApplyResources(this.tbRestart, "tbRestart");
            this.tbRestart.Name = "tbRestart";
            this.tbRestart.Click += new System.EventHandler(this.miRestart_Click);
            // 
            // tbExit
            // 
            this.tbExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbExit.Image = global::MDM.Properties.Resources.exit;
            resources.ApplyResources(this.tbExit, "tbExit");
            this.tbExit.Name = "tbExit";
            this.tbExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbLogin.Image = global::MDM.Properties.Resources.login;
            resources.ApplyResources(this.tbLogin, "tbLogin");
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Click += new System.EventHandler(this.miLogIn_Click);
            // 
            // tbLogout
            // 
            this.tbLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbLogout.Image = global::MDM.Properties.Resources.logout;
            resources.ApplyResources(this.tbLogout, "tbLogout");
            this.tbLogout.Name = "tbLogout";
            this.tbLogout.Click += new System.EventHandler(this.miLogOut_Click);
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout,
            this.toolStripMenuItem1,
            this.miExit,
            this.miRestart,
            this.miSwitchOff});
            this.miFile.Name = "miFile";
            resources.ApplyResources(this.miFile, "miFile");
            // 
            // miAbout
            // 
            this.miAbout.Image = global::MDM.Properties.Resources.about;
            resources.ApplyResources(this.miAbout, "miAbout");
            this.miAbout.Name = "miAbout";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // miExit
            // 
            this.miExit.Image = global::MDM.Properties.Resources.exit;
            resources.ApplyResources(this.miExit, "miExit");
            this.miExit.Name = "miExit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miRestart
            // 
            this.miRestart.Image = global::MDM.Properties.Resources.restart;
            resources.ApplyResources(this.miRestart, "miRestart");
            this.miRestart.Name = "miRestart";
            this.miRestart.Click += new System.EventHandler(this.miRestart_Click);
            // 
            // miSwitchOff
            // 
            this.miSwitchOff.Image = global::MDM.Properties.Resources.switchoff;
            resources.ApplyResources(this.miSwitchOff, "miSwitchOff");
            this.miSwitchOff.Name = "miSwitchOff";
            this.miSwitchOff.Click += new System.EventHandler(this.miSwitchOff_Click);
            // 
            // miUser
            // 
            this.miUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUserList,
            this.toolStripMenuItem5,
            this.miLogIn,
            this.miLogOut,
            this.toolStripMenuItem3,
            this.miNewUser,
            this.miEditUser,
            this.toolStripMenuItem8,
            this.miDeleteUser,
            this.miUndeleteUser,
            this.miWipeUser,
            this.toolStripMenuItem7,
            this.miPurgeUser,
            this.miTruncateUser});
            this.miUser.Name = "miUser";
            resources.ApplyResources(this.miUser, "miUser");
            // 
            // miUserList
            // 
            this.miUserList.Image = global::MDM.Properties.Resources.userlist;
            this.miUserList.Name = "miUserList";
            resources.ApplyResources(this.miUserList, "miUserList");
            this.miUserList.Click += new System.EventHandler(this.miUserList_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // miLogIn
            // 
            this.miLogIn.Image = global::MDM.Properties.Resources.login;
            this.miLogIn.Name = "miLogIn";
            resources.ApplyResources(this.miLogIn, "miLogIn");
            this.miLogIn.Click += new System.EventHandler(this.miLogIn_Click);
            // 
            // miLogOut
            // 
            this.miLogOut.Image = global::MDM.Properties.Resources.logout;
            this.miLogOut.Name = "miLogOut";
            resources.ApplyResources(this.miLogOut, "miLogOut");
            this.miLogOut.Click += new System.EventHandler(this.miLogOut_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // miNewUser
            // 
            this.miNewUser.Image = global::MDM.Properties.Resources.useradd;
            this.miNewUser.Name = "miNewUser";
            resources.ApplyResources(this.miNewUser, "miNewUser");
            this.miNewUser.Click += new System.EventHandler(this.miNewUser_Click);
            // 
            // miEditUser
            // 
            this.miEditUser.Image = global::MDM.Properties.Resources.useredit;
            this.miEditUser.Name = "miEditUser";
            resources.ApplyResources(this.miEditUser, "miEditUser");
            this.miEditUser.Click += new System.EventHandler(this.miEditUser_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // miDeleteUser
            // 
            this.miDeleteUser.Image = global::MDM.Properties.Resources.userdelete;
            this.miDeleteUser.Name = "miDeleteUser";
            resources.ApplyResources(this.miDeleteUser, "miDeleteUser");
            this.miDeleteUser.Click += new System.EventHandler(this.miDeleteUser_Click);
            // 
            // miUndeleteUser
            // 
            this.miUndeleteUser.Image = global::MDM.Properties.Resources.undeleteuser;
            this.miUndeleteUser.Name = "miUndeleteUser";
            resources.ApplyResources(this.miUndeleteUser, "miUndeleteUser");
            this.miUndeleteUser.Click += new System.EventHandler(this.miUndeleteUser_Click);
            // 
            // miWipeUser
            // 
            this.miWipeUser.Image = global::MDM.Properties.Resources.deluser;
            this.miWipeUser.Name = "miWipeUser";
            resources.ApplyResources(this.miWipeUser, "miWipeUser");
            this.miWipeUser.Click += new System.EventHandler(this.miWipeUser_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // miPurgeUser
            // 
            this.miPurgeUser.Image = global::MDM.Properties.Resources.clear;
            this.miPurgeUser.Name = "miPurgeUser";
            resources.ApplyResources(this.miPurgeUser, "miPurgeUser");
            this.miPurgeUser.Click += new System.EventHandler(this.miPurgeUser_Click);
            // 
            // miTruncateUser
            // 
            this.miTruncateUser.Image = global::MDM.Properties.Resources.erase;
            this.miTruncateUser.Name = "miTruncateUser";
            resources.ApplyResources(this.miTruncateUser, "miTruncateUser");
            this.miTruncateUser.Click += new System.EventHandler(this.miTruncateUser_Click);
            // 
            // miPatient
            // 
            this.miPatient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.miPatientList,
            this.toolStripMenuItem10,
            this.miNewPatient,
            this.miEditPatient,
            this.toolStripMenuItem11,
            this.miDeletePatient,
            this.miUndeletePatient,
            this.miWipePatient,
            this.toolStripMenuItem12,
            this.miPurgePatient,
            this.miTruncatePatient,
            this.toolStripMenuItem13,
            this.miHIC});
            this.miPatient.Name = "miPatient";
            resources.ApplyResources(this.miPatient, "miPatient");
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // miPatientList
            // 
            this.miPatientList.Image = global::MDM.Properties.Resources.userlist;
            this.miPatientList.Name = "miPatientList";
            resources.ApplyResources(this.miPatientList, "miPatientList");
            this.miPatientList.Click += new System.EventHandler(this.miPatientList_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // miNewPatient
            // 
            this.miNewPatient.Image = global::MDM.Properties.Resources.useradd;
            this.miNewPatient.Name = "miNewPatient";
            resources.ApplyResources(this.miNewPatient, "miNewPatient");
            this.miNewPatient.Click += new System.EventHandler(this.miNewPatient_Click);
            // 
            // miEditPatient
            // 
            this.miEditPatient.Image = global::MDM.Properties.Resources.useredit;
            this.miEditPatient.Name = "miEditPatient";
            resources.ApplyResources(this.miEditPatient, "miEditPatient");
            this.miEditPatient.Click += new System.EventHandler(this.miEditPatient_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
            // 
            // miDeletePatient
            // 
            this.miDeletePatient.Image = global::MDM.Properties.Resources.userdelete;
            this.miDeletePatient.Name = "miDeletePatient";
            resources.ApplyResources(this.miDeletePatient, "miDeletePatient");
            this.miDeletePatient.Click += new System.EventHandler(this.miDeletePatient_Click);
            // 
            // miUndeletePatient
            // 
            this.miUndeletePatient.Image = global::MDM.Properties.Resources.undeleteuser;
            this.miUndeletePatient.Name = "miUndeletePatient";
            resources.ApplyResources(this.miUndeletePatient, "miUndeletePatient");
            this.miUndeletePatient.Click += new System.EventHandler(this.miUndeletePatient_Click);
            // 
            // miWipePatient
            // 
            this.miWipePatient.Image = global::MDM.Properties.Resources.deluser;
            this.miWipePatient.Name = "miWipePatient";
            resources.ApplyResources(this.miWipePatient, "miWipePatient");
            this.miWipePatient.Click += new System.EventHandler(this.miWipePatient_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            resources.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
            // 
            // miPurgePatient
            // 
            this.miPurgePatient.Image = global::MDM.Properties.Resources.clear;
            this.miPurgePatient.Name = "miPurgePatient";
            resources.ApplyResources(this.miPurgePatient, "miPurgePatient");
            this.miPurgePatient.Click += new System.EventHandler(this.miPurgePatient_Click);
            // 
            // miTruncatePatient
            // 
            this.miTruncatePatient.Image = global::MDM.Properties.Resources.erase;
            this.miTruncatePatient.Name = "miTruncatePatient";
            resources.ApplyResources(this.miTruncatePatient, "miTruncatePatient");
            this.miTruncatePatient.Click += new System.EventHandler(this.miTruncatePatient_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
            // 
            // miHIC
            // 
            this.miHIC.Image = global::MDM.Properties.Resources.hic;
            resources.ApplyResources(this.miHIC, "miHIC");
            this.miHIC.Name = "miHIC";
            this.miHIC.Click += new System.EventHandler(this.miHIC_Click);
            // 
            // miSystem
            // 
            this.miSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBackupDatabase,
            this.miRestoreDatabase});
            this.miSystem.Name = "miSystem";
            resources.ApplyResources(this.miSystem, "miSystem");
            // 
            // miBackupDatabase
            // 
            this.miBackupDatabase.Image = global::MDM.Properties.Resources.dbbackup;
            resources.ApplyResources(this.miBackupDatabase, "miBackupDatabase");
            this.miBackupDatabase.Name = "miBackupDatabase";
            this.miBackupDatabase.Click += new System.EventHandler(this.miBackupDatabase_Click);
            // 
            // miRestoreDatabase
            // 
            this.miRestoreDatabase.Image = global::MDM.Properties.Resources.dbrestore;
            resources.ApplyResources(this.miRestoreDatabase, "miRestoreDatabase");
            this.miRestoreDatabase.Name = "miRestoreDatabase";
            this.miRestoreDatabase.Click += new System.EventHandler(this.miRestoreDatabase_Click);
            // 
            // miAdministration
            // 
            this.miAdministration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLog,
            this.toolStripMenuItem6,
            this.miNewDatabase,
            this.miCompactDatabase});
            this.miAdministration.Name = "miAdministration";
            resources.ApplyResources(this.miAdministration, "miAdministration");
            // 
            // miLog
            // 
            resources.ApplyResources(this.miLog, "miLog");
            this.miLog.Name = "miLog";
            this.miLog.Click += new System.EventHandler(this.miLog_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // miNewDatabase
            // 
            resources.ApplyResources(this.miNewDatabase, "miNewDatabase");
            this.miNewDatabase.Name = "miNewDatabase";
            this.miNewDatabase.Click += new System.EventHandler(this.miNewDatabase_Click);
            // 
            // miCompactDatabase
            // 
            this.miCompactDatabase.Image = global::MDM.Properties.Resources.dbcompact;
            resources.ApplyResources(this.miCompactDatabase, "miCompactDatabase");
            this.miCompactDatabase.Name = "miCompactDatabase";
            this.miCompactDatabase.Click += new System.EventHandler(this.miCompactDatabase_Click);
            // 
            // miTheLog
            // 
            this.miTheLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClear,
            this.toolStripMenuItem4,
            this.miClosePanel});
            this.miTheLog.Name = "miTheLog";
            resources.ApplyResources(this.miTheLog, "miTheLog");
            // 
            // miClear
            // 
            this.miClear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearLogInformation,
            this.miClearLogWarning,
            this.miClearLogError,
            this.miClearLogSelection,
            this.miClearLogAll});
            this.miClear.Image = global::MDM.Properties.Resources.clear;
            resources.ApplyResources(this.miClear, "miClear");
            this.miClear.Name = "miClear";
            // 
            // miClearLogInformation
            // 
            this.miClearLogInformation.Name = "miClearLogInformation";
            resources.ApplyResources(this.miClearLogInformation, "miClearLogInformation");
            this.miClearLogInformation.Click += new System.EventHandler(this.miClearLogInformation_Click);
            // 
            // miClearLogWarning
            // 
            this.miClearLogWarning.Name = "miClearLogWarning";
            resources.ApplyResources(this.miClearLogWarning, "miClearLogWarning");
            this.miClearLogWarning.Click += new System.EventHandler(this.miClearLogWarning_Click);
            // 
            // miClearLogError
            // 
            this.miClearLogError.Name = "miClearLogError";
            resources.ApplyResources(this.miClearLogError, "miClearLogError");
            this.miClearLogError.Click += new System.EventHandler(this.miClearLogError_Click);
            // 
            // miClearLogSelection
            // 
            this.miClearLogSelection.Name = "miClearLogSelection";
            resources.ApplyResources(this.miClearLogSelection, "miClearLogSelection");
            this.miClearLogSelection.Click += new System.EventHandler(this.miClearLogSelection_Click);
            // 
            // miClearLogAll
            // 
            this.miClearLogAll.Name = "miClearLogAll";
            resources.ApplyResources(this.miClearLogAll, "miClearLogAll");
            this.miClearLogAll.Click += new System.EventHandler(this.miClearLogAll_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // miClosePanel
            // 
            this.miClosePanel.Image = global::MDM.Properties.Resources.close16;
            resources.ApplyResources(this.miClosePanel, "miClosePanel");
            this.miClosePanel.Name = "miClosePanel";
            this.miClosePanel.Click += new System.EventHandler(this.miCloseLogPanel_Click);
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(12, 16);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miUser,
            this.miPatient,
            this.miSystem,
            this.miAdministration,
            this.miTheLog,
            this.miLang,
            this.miDBRestore});
            resources.ApplyResources(this.msMain, "msMain");
            this.msMain.Name = "msMain";
            // 
            // miLang
            // 
            this.miLang.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.miLang.AutoToolTip = true;
            this.miLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLangCS,
            this.miLangEN,
            this.miLangRU});
            this.miLang.Image = global::MDM.Properties.Resources.cs;
            resources.ApplyResources(this.miLang, "miLang");
            this.miLang.Name = "miLang";
            this.miLang.Tag = "cs";
            // 
            // miLangCS
            // 
            this.miLangCS.Image = global::MDM.Properties.Resources.cs;
            resources.ApplyResources(this.miLangCS, "miLangCS");
            this.miLangCS.Name = "miLangCS";
            this.miLangCS.Tag = "cs";
            this.miLangCS.Click += new System.EventHandler(this.miLangXX_Click);
            // 
            // miLangEN
            // 
            this.miLangEN.Image = global::MDM.Properties.Resources.en;
            resources.ApplyResources(this.miLangEN, "miLangEN");
            this.miLangEN.Name = "miLangEN";
            this.miLangEN.Tag = "en";
            this.miLangEN.Click += new System.EventHandler(this.miLangXX_Click);
            // 
            // miLangRU
            // 
            this.miLangRU.Image = global::MDM.Properties.Resources.ru;
            resources.ApplyResources(this.miLangRU, "miLangRU");
            this.miLangRU.Name = "miLangRU";
            this.miLangRU.Tag = "ru";
            this.miLangRU.Click += new System.EventHandler(this.miLangXX_Click);
            // 
            // miDBRestore
            // 
            this.miDBRestore.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreFromThisBackupToolStripMenuItem,
            this.makeNewBackupToolStripMenuItem,
            this.deleteBackupToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.miDBRestore.Name = "miDBRestore";
            resources.ApplyResources(this.miDBRestore, "miDBRestore");
            // 
            // restoreFromThisBackupToolStripMenuItem
            // 
            this.restoreFromThisBackupToolStripMenuItem.Image = global::MDM.Properties.Resources.backuprestore;
            resources.ApplyResources(this.restoreFromThisBackupToolStripMenuItem, "restoreFromThisBackupToolStripMenuItem");
            this.restoreFromThisBackupToolStripMenuItem.Name = "restoreFromThisBackupToolStripMenuItem";
            this.restoreFromThisBackupToolStripMenuItem.Click += new System.EventHandler(this.miRestoreBackup_Click);
            // 
            // makeNewBackupToolStripMenuItem
            // 
            this.makeNewBackupToolStripMenuItem.Image = global::MDM.Properties.Resources.backupmakenew;
            resources.ApplyResources(this.makeNewBackupToolStripMenuItem, "makeNewBackupToolStripMenuItem");
            this.makeNewBackupToolStripMenuItem.Name = "makeNewBackupToolStripMenuItem";
            this.makeNewBackupToolStripMenuItem.Click += new System.EventHandler(this.miMakeNewBackup_Click);
            // 
            // deleteBackupToolStripMenuItem
            // 
            this.deleteBackupToolStripMenuItem.Image = global::MDM.Properties.Resources.backupdelete;
            resources.ApplyResources(this.deleteBackupToolStripMenuItem, "deleteBackupToolStripMenuItem");
            this.deleteBackupToolStripMenuItem.Name = "deleteBackupToolStripMenuItem";
            this.deleteBackupToolStripMenuItem.Click += new System.EventHandler(this.miDeleteBackup_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::MDM.Properties.Resources.close16;
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.miCloseBackup_Click);
            // 
            // logoBox
            // 
            this.logoBox.Image = global::MDM.Properties.Resources.logo;
            resources.ApplyResources(this.logoBox, "logoBox");
            this.logoBox.Name = "logoBox";
            this.logoBox.TabStop = false;
            // 
            // panPatient
            // 
            this.panPatient.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panPatient, "panPatient");
            this.panPatient.Name = "panPatient";
            // 
            // panHIC
            // 
            this.panHIC.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panHIC, "panHIC");
            this.panHIC.Name = "panHIC";
            // 
            // panUser
            // 
            this.panUser.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panUser, "panUser");
            this.panUser.Name = "panUser";
            // 
            // panDBRestore
            // 
            this.panDBRestore.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panDBRestore, "panDBRestore");
            this.panDBRestore.Name = "panDBRestore";
            // 
            // panLog
            // 
            this.panLog.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panLog, "panLog");
            this.panLog.Name = "panLog";
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panMain, "panMain");
            this.panMain.Name = "panMain";
            // 
            // wMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.panPatient);
            this.Controls.Add(this.panHIC);
            this.Controls.Add(this.panUser);
            this.Controls.Add(this.panDBRestore);
            this.Controls.Add(this.panLog);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.msMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msMain;
            this.MinimizeBox = false;
            this.Name = "wMain";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wMain_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.wMain_Shown);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tbLogin;
        private System.Windows.Forms.ToolStripButton tbLogout;
        private System.Windows.Forms.ToolStripButton tbExit;
        private System.Windows.Forms.ToolStripButton tbRestart;
        private System.Windows.Forms.ToolStripButton tbShutdown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbAbout;
        private mainPanel panMain;
        private DBPanel panLog;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miRestart;
        private System.Windows.Forms.ToolStripMenuItem miSwitchOff;
        private System.Windows.Forms.ToolStripMenuItem miUser;
        private System.Windows.Forms.ToolStripMenuItem miLogIn;
        private System.Windows.Forms.ToolStripMenuItem miLogOut;
        private System.Windows.Forms.ToolStripMenuItem miPatient;
        private System.Windows.Forms.ToolStripMenuItem miSystem;
        private System.Windows.Forms.ToolStripMenuItem miAdministration;
        private System.Windows.Forms.ToolStripMenuItem miLog;
        internal System.Windows.Forms.ToolStripMenuItem miTheLog;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miClearLogInformation;
        private System.Windows.Forms.ToolStripMenuItem miClearLogWarning;
        private System.Windows.Forms.ToolStripMenuItem miClearLogError;
        private System.Windows.Forms.ToolStripMenuItem miClearLogAll;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem miLangCS;
        private System.Windows.Forms.ToolStripMenuItem miLangEN;
        private System.Windows.Forms.ToolStripMenuItem miLangRU;
        private System.Windows.Forms.ToolStripMenuItem miClearLogSelection;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miClosePanel;
        private System.Windows.Forms.ToolStripMenuItem miNewDatabase;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbClock;
        private System.Windows.Forms.ToolStripMenuItem miBackupDatabase;
        private System.Windows.Forms.ToolStripMenuItem miRestoreDatabase;
        private System.Windows.Forms.ToolStripStatusLabel lbKbdLang;
        private System.Windows.Forms.ToolStripStatusLabel lbUser;
        private dbRestorePanel panDBRestore;
        private System.Windows.Forms.ToolStripMenuItem miDBRestore;
        private System.Windows.Forms.ToolStripMenuItem restoreFromThisBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeNewBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private DBPanel panUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miNewUser;
        private System.Windows.Forms.ToolStripMenuItem miEditUser;
        private System.Windows.Forms.ToolStripMenuItem miDeleteUser;
        private System.Windows.Forms.ToolStripMenuItem miUserList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem miLang;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miCompactDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem miWipeUser;
        private System.Windows.Forms.ToolStripMenuItem miPurgeUser;
        private System.Windows.Forms.ToolStripMenuItem miTruncateUser;
        private System.Windows.Forms.ToolStripMenuItem miUndeleteUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private DBPanel panHIC;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem miHIC;
        private System.Windows.Forms.ToolStripMenuItem miPatientList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem miNewPatient;
        private System.Windows.Forms.ToolStripMenuItem miEditPatient;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem miDeletePatient;
        private System.Windows.Forms.ToolStripMenuItem miUndeletePatient;
        private System.Windows.Forms.ToolStripMenuItem miWipePatient;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem miPurgePatient;
        private System.Windows.Forms.ToolStripMenuItem miTruncatePatient;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private DBPanel panPatient;
        private System.Windows.Forms.PictureBox logoBox;
    }
}

