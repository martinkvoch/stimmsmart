namespace MDM.Windows
{
    partial class wDiagnosis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wDiagnosis));
            this.tvDiagnosis = new System.Windows.Forms.TreeView();
            this.cbSelect = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvDiagnosis
            // 
            resources.ApplyResources(this.tvDiagnosis, "tvDiagnosis");
            this.tvDiagnosis.FullRowSelect = true;
            this.tvDiagnosis.Name = "tvDiagnosis";
            this.tvDiagnosis.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDiagnosis_AfterSelect);
            // 
            // cbSelect
            // 
            resources.ApplyResources(this.cbSelect, "cbSelect");
            this.cbSelect.BackColor = System.Drawing.Color.YellowGreen;
            this.cbSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbSelect.FlatAppearance.BorderSize = 0;
            this.cbSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbSelect.Image = global::MDM.Properties.Resources.yes;
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.UseVisualStyleBackColor = false;
            // 
            // cbCancel
            // 
            resources.ApplyResources(this.cbCancel, "cbCancel");
            this.cbCancel.BackColor = System.Drawing.Color.Salmon;
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbCancel.FlatAppearance.BorderSize = 0;
            this.cbCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbCancel.Image = global::MDM.Properties.Resources.no;
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.UseVisualStyleBackColor = false;
            // 
            // wDiagnosis
            // 
            this.AcceptButton = this.cbSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbCancel;
            this.ControlBox = false;
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbSelect);
            this.Controls.Add(this.tvDiagnosis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "wDiagnosis";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDiagnosis;
        private System.Windows.Forms.Button cbSelect;
        private System.Windows.Forms.Button cbCancel;
    }
}