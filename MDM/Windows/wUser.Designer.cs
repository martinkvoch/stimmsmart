namespace MDM.Windows
{
    partial class wUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wUser));
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEmptyPsw = new System.Windows.Forms.Button();
            this.lbPswMatch = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbxLanguage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.cbSave = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLoginName
            // 
            resources.ApplyResources(this.txtLoginName, "txtLoginName");
            this.txtLoginName.Name = "txtLoginName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbEmptyPsw);
            this.groupBox1.Controls.Add(this.lbPswMatch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbEmptyPsw
            // 
            this.cbEmptyPsw.Image = global::MDM.Properties.Resources.pswempty;
            resources.ApplyResources(this.cbEmptyPsw, "cbEmptyPsw");
            this.cbEmptyPsw.Name = "cbEmptyPsw";
            this.cbEmptyPsw.UseVisualStyleBackColor = true;
            this.cbEmptyPsw.Click += new System.EventHandler(this.cbEmptyPsw_Click);
            // 
            // lbPswMatch
            // 
            resources.ApplyResources(this.lbPswMatch, "lbPswMatch");
            this.lbPswMatch.ForeColor = System.Drawing.Color.Green;
            this.lbPswMatch.Name = "lbPswMatch";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.UseSystemPasswordChar = true;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.UseSystemPasswordChar = true;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // cbxLanguage
            // 
            this.cbxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.cbxLanguage, "cbxLanguage");
            this.cbxLanguage.Name = "cbxLanguage";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAdmin);
            this.groupBox2.Controls.Add(this.rbUser);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rbAdmin
            // 
            resources.ApplyResources(this.rbAdmin, "rbAdmin");
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.TabStop = true;
            this.rbAdmin.UseVisualStyleBackColor = true;
            // 
            // rbUser
            // 
            resources.ApplyResources(this.rbUser, "rbUser");
            this.rbUser.Checked = true;
            this.rbUser.Name = "rbUser";
            this.rbUser.TabStop = true;
            this.rbUser.UseVisualStyleBackColor = true;
            // 
            // cbSave
            // 
            this.cbSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbSave.Image = global::MDM.Properties.Resources.savedatabase;
            resources.ApplyResources(this.cbSave, "cbSave");
            this.cbSave.Name = "cbSave";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbCancel.Image = global::MDM.Properties.Resources.no;
            resources.ApplyResources(this.cbCancel, "cbCancel");
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.UseVisualStyleBackColor = true;
            // 
            // wUser
            // 
            this.AcceptButton = this.cbSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbCancel;
            this.ControlBox = false;
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbxLanguage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtLoginName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "wUser";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbxLanguage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbAdmin;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.Button cbSave;
        private System.Windows.Forms.Button cbCancel;
        private System.Windows.Forms.Label lbPswMatch;
        private System.Windows.Forms.Button cbEmptyPsw;
    }
}