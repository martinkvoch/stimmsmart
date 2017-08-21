using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using MDM.Properties;

namespace MDM.Windows
{
    public partial class wConfig : Form
    {
        public wConfig()
        {
            int i;
            Settings settings = new Settings();
            string[] langs = settings.langs.Split(new char[] { '|' }), langNames = settings.langNames.Split(new char[] { '|' });

            InitializeComponent();
            nNOC.Value = settings.NOC;
            nNOP.Value = settings.NOP;
            nProcDur.Value = settings.ProcDur;
            nMinProcDur.Value = settings.CountedProcAfter;
            nMinTimeInt.Value = settings.MinProcInterval;
            cbxCurrLang.Items.AddRange(langs);
            i = cbxCurrLang.FindString(settings.lang);
            cbxCurrLang.Items.Clear();
            cbxCurrLang.Items.AddRange(langNames);
            cbxCurrLang.SelectedIndex = i;
            for(int j = 0; j < langs.Length; j++) lbxLanguages.Items.Add(langs[j] + " - " + langNames[j]);
            lbxLanguages.SelectedIndex = i;
        }

        private void cbOK_Click(object sender, EventArgs e)
        {
            //Settings settings = new Settings();

            Settings.Default.NOC = (byte)nNOC.Value;
            Settings.Default.NOP = (byte)nNOP.Value;
            Settings.Default.ProcDur = (short)nProcDur.Value;
            Settings.Default.CountedProcAfter = (byte)nMinProcDur.Value;
            Settings.Default.MinProcInterval = (byte)nMinTimeInt.Value;
            Settings.Default.langs = string.Join("|", lbxLanguages.Items.OfType<string>().Select(s => s.Substring(0, 2)));
            Settings.Default.lang = Settings.Default.langs.Split(new char[] { '|' })[cbxCurrLang.SelectedIndex];
            Settings.Default.Save();
            Settings.Default.Upgrade();
        }
    }
}
