using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

using MDM.Data;
using MDM.DlgBox;
using MDM.Properties;
using MDM.Windows;
using System.Diagnostics;

namespace MDM
{
    static class Program
    {
        const string methodFmt = "{0}.{1}()", errorFmt = "{0}: {1}";
        public static string Language = "xx";
        public static TUser LoggedUser = new TUser();
        //private static int kbDelay = SystemInformation.KeyboardSpeed;

        #region IsDesignMode
        private static bool? isDesignMode;
        internal static bool IsDesignMode
        {
            get
            {
                if(!isDesignMode.HasValue) isDesignMode = (Process.GetCurrentProcess().ProcessName.ToLower().Contains("devenv"));
                return isDesignMode.Value;
            }
        }
        #endregion

        #region chErrors
        private static bool[] chErrors = new bool[0];
        public static bool[] ChErrors
        {
            get
            {
                if(chErrors.Count() == 0) chErrors = new bool[6];
                return chErrors;
            }
            set
            {
                chErrors = value;
            }
        }
        #endregion

        public static bool KeepRunning { get; set; }

        #region SetLanguage(), GetLangs()
        public static void SetLanguage()
        {
            CultureInfo culture = new CultureInfo(Language, true);

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static string[] GetLangs()
        {
            return new Settings().langs.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region Utility pro SplashScreen: ShowSplash(), GetVersion(), GetManAddr(), GetCopyright()
        internal static void ShowSplash(bool onInit = true)
        {
            using(wSplashScreen splash = new wSplashScreen(onInit)) splash.ShowDialog();
        }

        internal static string GetVersion()
        {
            Version ver = typeof(Program).Assembly.GetName().Version;

            //return ver.ToString();
            return string.Format("{0}.{1} (rev. {2})", ver.Major, ver.Minor, ver.Revision);
        }

        internal static string GetManAddr()
        {
            return new Settings().ManAddr.Replace("|", Environment.NewLine);
        }

        internal static string GetCopyright()
        {
            return ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false))
               .Copyright;
        }

        internal static string GetSerNum()
        {
            return Resources.serNum;
        }
        #endregion

        #region Utility pro Exit/Restart: AppExit(), Restart(), Shutdown()
        private static void appExit(object sender, EventArgs e)
        {
            if(!KeepRunning)
            {
                string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

                Log.InfoToLog(methodName, Resources.exitMsg);
                Database.Close();
            }
        }

        public static void Restart()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            Log.InfoToLog(methodName, Resources.restartMsg);
            appExit(null, null);
            Environment.Exit(0);
        }

        public static void Shutdown()
        {
            string methodName = string.Format(methodFmt, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            Log.InfoToLog(methodName, Resources.shutdownMsg);
            appExit(null, null);
            Environment.Exit(0);
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings settings = new Settings();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.CurrentCulture = new CultureInfo(settings.lang);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Language = settings.lang;
            SetLanguage();
            Database.Open();
            //Procedure.Init();
            //Log.Init();
            ShowSplash(true);
            Application.ApplicationExit += appExit;
            KeepRunning = false;
            while(true)
            {
                Application.Run(new wMain(Program.Language));
                if(!KeepRunning) break;
            } 
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.ErrorToLog("UnhandledException()", e.ExceptionObject.ToString());
            DialogBox.ShowError(e.ExceptionObject.ToString(), "Generic error");
        }
    }
}
