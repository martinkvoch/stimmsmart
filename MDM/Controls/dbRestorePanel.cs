using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using MDM.Data;

namespace MDM.Controls
{
    public partial class dbRestorePanel : MDMPanel
    {
        /// <summary>
        /// Počet dostupných záloh
        /// </summary>
        public int Count { get { return tvBackups.Enabled ? tvBackups.GetNodeCount(true) : 0; } }
        //public int Count { get { return lbxBackups.Enabled ? lbxBackups.Items.Count : 0; } }

        public dbRestorePanel()
        {
            InitializeComponent();
        }

        private void updRestoreButtons()
        {
            cbDelete.Enabled = cbRestore.Enabled = (Count > 0);
        }

        private static TreeNode createDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);

            foreach(DirectoryInfo directory in directoryInfo.GetDirectories()) directoryNode.Nodes.Add(createDirectoryNode(directory));
            foreach(FileInfo file in directoryInfo.GetFiles("*.enc")) directoryNode.Nodes.Add(new TreeNode(Database.Fn2Date(file.Name, true)) { Tag = file.Name });
            return directoryNode;
        }

        #region Veřejné funkce
        private TreeNode findNode(TreeNodeCollection tncoll, string strText)
        {
            foreach(TreeNode tnCurr in tncoll)
            {
                TreeNode tnFound;

                if(tnCurr.FullPath.Equals(strText, StringComparison.OrdinalIgnoreCase)) return tnCurr;
                tnFound = findNode(tnCurr.Nodes, strText);
                if(tnFound != null) return tnFound;
            }
            return null;
        }

        public override void Fill(string cmd = null)
        {
            //string[] files = Directory.Exists(Database.BackupDir) ? Directory.GetFiles(Database.BackupDir, Path.ChangeExtension("*", Database.EncExt)).ToArray() : new string[0];
            //string[] backups = files.Select(f => Database.Fn2Date(f, true)).ToArray();
            TreeNode node;
            string dt = Path.Combine(DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(Database.BackupDir);

            tvBackups.Nodes.Clear();
            tvBackups.Nodes.AddRange(createDirectoryNode(rootDirectoryInfo).Nodes.OfType<TreeNode>().ToArray());
            node = findNode(tvBackups.Nodes, dt);
            if(node != null)
            {
                if(node.Parent != null) node.Parent.Expand();
                node.Expand();
                if(node.Nodes.Count > 0)
                {
                    tvBackups.SelectedNode = node.Nodes[node.Nodes.Count - 1];
                    tvBackups.Focus();
                }
            }
            //lbxBackups.Items.Clear();
            //if(backups.Length > 0)
            //{
            //    int i = lbxBackups.SelectedIndex;

            //    lbxBackups.Items.AddRange(backups);
            //    lbxBackups.SelectedIndex = i < 0 ? Count - 1 : i == Count ? i - 1 : i;
            //}
            //else
            //{
            //    lbxBackups.Items.Add(Resources.noBacksAvail);
            //    lbxBackups.Enabled = false;
            //}
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
            TreeNode selNode = tvBackups.SelectedNode;

            //Database.Restore(lbxBackups.SelectedItem.ToString());
            if(tvBackups.SelectedNode != null && tvBackups.SelectedNode.Tag != null)
            {
                Database.Restore(selNode.Tag.ToString());
                Fill();
            }
        }

        private void cbDelete_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvBackups.SelectedNode;
            //string fn = Database.Date2Fn(lbxBackups.SelectedItem.ToString(), true);

            //File.Delete(Database.Date2Fn(lbxBackups.SelectedItem.ToString(), true));
            if(selNode != null && selNode.Tag != null)
            {
                Database.DeleteBackup(selNode.Tag.ToString());
                Fill();
            }
        }

        private void cbMakeNew_Click(object sender, EventArgs e)
        {
            Database.Backup();
            Fill();
        }
        #endregion
    }
}
