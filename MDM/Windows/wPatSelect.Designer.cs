namespace MDM.Windows
{
    partial class wPatSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wPatSelect));
            this.cbSelect = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
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
            // txtFindName
            // 
            resources.ApplyResources(this.txtFindName, "txtFindName");
            this.txtFindName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtFindName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.TextChanged += new System.EventHandler(this.txtFindName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToOrderColumns = true;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGrid, "dataGrid");
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid.RowTemplate.Height = 40;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.DoubleClick += new System.EventHandler(this.dataGrid_DoubleClick);
            // 
            // wPatSelect
            // 
            this.AcceptButton = this.cbSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbCancel;
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFindName);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "wPatSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbSelect;
        private System.Windows.Forms.Button cbCancel;
        private System.Windows.Forms.TextBox txtFindName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}