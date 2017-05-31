namespace MDM.Controls
{
    partial class dbRestorePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dbRestorePanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.miClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxBackups = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRestore = new System.Windows.Forms.Button();
            this.cbMakeNew = new System.Windows.Forms.Button();
            this.cbDelete = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.miClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            // 
            // miClose
            // 
            this.miClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.miClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.miClose.Image = global::MDM.Properties.Resources.close16;
            resources.ApplyResources(this.miClose, "miClose");
            this.miClose.Name = "miClose";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lbxBackups);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lbxBackups
            // 
            resources.ApplyResources(this.lbxBackups, "lbxBackups");
            this.lbxBackups.FormattingEnabled = true;
            this.lbxBackups.Name = "lbxBackups";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbRestore
            // 
            this.cbRestore.BackColor = System.Drawing.SystemColors.Control;
            this.cbRestore.Image = global::MDM.Properties.Resources.backuprestore;
            resources.ApplyResources(this.cbRestore, "cbRestore");
            this.cbRestore.Name = "cbRestore";
            this.cbRestore.UseVisualStyleBackColor = false;
            this.cbRestore.Click += new System.EventHandler(this.cbRestore_Click);
            // 
            // cbMakeNew
            // 
            this.cbMakeNew.BackColor = System.Drawing.SystemColors.Control;
            this.cbMakeNew.Image = global::MDM.Properties.Resources.backupmakenew;
            resources.ApplyResources(this.cbMakeNew, "cbMakeNew");
            this.cbMakeNew.Name = "cbMakeNew";
            this.cbMakeNew.UseVisualStyleBackColor = false;
            this.cbMakeNew.Click += new System.EventHandler(this.cbMakeNew_Click);
            // 
            // cbDelete
            // 
            this.cbDelete.BackColor = System.Drawing.SystemColors.Control;
            this.cbDelete.Image = global::MDM.Properties.Resources.backupdelete;
            resources.ApplyResources(this.cbDelete, "cbDelete");
            this.cbDelete.Name = "cbDelete";
            this.cbDelete.UseVisualStyleBackColor = false;
            this.cbDelete.Click += new System.EventHandler(this.cbDelete_Click);
            // 
            // dbRestorePanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.cbDelete);
            this.Controls.Add(this.cbMakeNew);
            this.Controls.Add(this.cbRestore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "dbRestorePanel";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton miClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxBackups;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button cbRestore;
        public System.Windows.Forms.Button cbMakeNew;
        public System.Windows.Forms.Button cbDelete;

    }
}
