namespace MDM
{
    partial class wSplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wSplashScreen));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbAppName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbManAddr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbProtBy = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbSerNum = new System.Windows.Forms.Label();
            this.lbProgress = new System.Windows.Forms.Label();
            this.btnEsc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackgroundImage = global::MDM.Properties.Resources.logo;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lbAppName
            // 
            resources.ApplyResources(this.lbAppName, "lbAppName");
            this.lbAppName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbAppName.Name = "lbAppName";
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackgroundImage = global::MDM.Properties.Resources.Logo_ZAT;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Name = "label5";
            // 
            // lbManAddr
            // 
            resources.ApplyResources(this.lbManAddr, "lbManAddr");
            this.lbManAddr.Name = "lbManAddr";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lbProtBy
            // 
            resources.ApplyResources(this.lbProtBy, "lbProtBy");
            this.lbProtBy.Name = "lbProtBy";
            // 
            // lbCopyright
            // 
            resources.ApplyResources(this.lbCopyright, "lbCopyright");
            this.lbCopyright.Name = "lbCopyright";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lbSerNum
            // 
            resources.ApplyResources(this.lbSerNum, "lbSerNum");
            this.lbSerNum.Name = "lbSerNum";
            // 
            // lbProgress
            // 
            resources.ApplyResources(this.lbProgress, "lbProgress");
            this.lbProgress.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbProgress.Name = "lbProgress";
            // 
            // btnEsc
            // 
            resources.ApplyResources(this.btnEsc, "btnEsc");
            this.btnEsc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnEsc.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEsc.FlatAppearance.BorderSize = 0;
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.UseVisualStyleBackColor = false;
            // 
            // wSplashScreen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnEsc;
            this.Controls.Add(this.btnEsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbManAddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.lbSerNum);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.lbProtBy);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbAppName);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wSplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbAppName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbManAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbProtBy;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbSerNum;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Button btnEsc;
    }
}