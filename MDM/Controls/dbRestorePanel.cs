using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MDM.Data;
using MDM.Properties;

namespace MDM.Controls
{
    public partial class dbRestorePanel : MDMPanel
    {
        /// <summary>
        /// Počet dostupných záloh
        /// </summary>
        public int Count { get { return lbxBackups.Enabled ? lbxBackups.Items.Count : 0; } }

        public dbRestorePanel()
        {
            InitializeComponent();
        }

        private void updRestoreButtons()
        {
            cbDelete.Enabled = cbRestore.Enabled = (Count > 0);
        }

        #region Veřejné funkce
        public override void Fill(string cmd = null)
        {
            string[] files = Directory.Exists(Database.BackupDir) ? Directory.GetFiles(Database.BackupDir, Path.ChangeExtension("*", Database.EncExt)).ToArray() : new string[0];
            string[] backups = files.Select(f => Database.Fn2Date(f, true)).ToArray();

            lbxBackups.Items.Clear();
            if(backups.Length > 0)
            {
                int i = lbxBackups.SelectedIndex;

                lbxBackups.Items.AddRange(backups);
                lbxBackups.SelectedIndex = i < 0 ? Count - 1 : i == Count ? i - 1 : i;
            }
            else
            {
                lbxBackups.Items.Add(Resources.noBacksAvail);
                lbxBackups.Enabled = false;
            }
            updRestoreButtons();
        }

        public override void Open(ToolStripMenuItem panMenu = null)
        {
            base.Open(panMenu);
        }
        #endregion

        #region Zavření panelu
        private void miClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Tlačítka panelu
        private void cbRestore_Click(object sender, EventArgs e)
        {
            Database.Restore(lbxBackups.SelectedItem.ToString());
            Fill();
        }

        private void cbDelete_Click(object sender, EventArgs e)
        {
            string fn = Database.Date2Fn(lbxBackups.SelectedItem.ToString(), true);

            File.Delete(Database.Date2Fn(lbxBackups.SelectedItem.ToString(), true));
            Fill();
        }

        private void cbMakeNew_Click(object sender, EventArgs e)
        {
            Database.Backup();
            Fill();
        }
        #endregion
    }
}
