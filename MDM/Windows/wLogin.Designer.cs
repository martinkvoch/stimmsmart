namespace MDM.Windows
{
    partial class wLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wLogin));
            this.cbSignIn = new System.Windows.Forms.Button();
            this.cbExitApp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxUserName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbSignIn
            // 
            this.cbSignIn.BackColor = System.Drawing.Color.YellowGreen;
            this.cbSignIn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbSignIn.FlatAppearance.BorderSize = 0;
            this.cbSignIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.cbSignIn, "cbSignIn");
            this.cbSignIn.Image = global::MDM.Properties.Resources.yes;
            this.cbSignIn.Name = "cbSignIn";
            this.cbSignIn.UseVisualStyleBackColor = false;
            // 
            // cbExitApp
            // 
            this.cbExitApp.BackColor = System.Drawing.Color.Salmon;
            this.cbExitApp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbExitApp.FlatAppearance.BorderSize = 0;
            this.cbExitApp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.cbExitApp, "cbExitApp");
            this.cbExitApp.Image = global::MDM.Properties.Resources.no;
            this.cbExitApp.Name = "cbExitApp";
            this.cbExitApp.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbxUserName
            // 
            this.cbxUserName.BackColor = System.Drawing.Color.Silver;
            this.cbxUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbxUserName, "cbxUserName");
            this.cbxUserName.FormattingEnabled = true;
            this.cbxUserName.Name = "cbxUserName";
            this.cbxUserName.SelectedIndexChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // wLogin
            // 
            this.AcceptButton = this.cbSignIn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbExitApp;
            this.ControlBox = false;
            this.Controls.Add(this.cbxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbExitApp);
            this.Controls.Add(this.cbSignIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "wLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbSignIn;
        private System.Windows.Forms.Button cbExitApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxUserName;
    }
}