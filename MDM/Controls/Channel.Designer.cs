namespace MDM.Controls
{
    partial class Channel
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Channel));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbCurrent = new MDM.Controls.MDMTrackBar();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.cbCurrPlus = new System.Windows.Forms.Button();
            this.cbCurrMinus = new System.Windows.Forms.Button();
            this.cbStop = new System.Windows.Forms.Button();
            this.cbPause = new System.Windows.Forms.Button();
            this.cbStart = new System.Windows.Forms.Button();
            this.cbSetCurrent = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPatSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbProcNum = new System.Windows.Forms.Label();
            this.lbDiagnosis = new System.Windows.Forms.Label();
            this.lbPatName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.Led = new Bulb.LedBulb();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.ucMonitor = new WpfUC.ucMonitor();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCurrent);
            this.groupBox3.Controls.Add(this.lbCurrent);
            this.groupBox3.Controls.Add(this.cbCurrPlus);
            this.groupBox3.Controls.Add(this.cbCurrMinus);
            this.groupBox3.Controls.Add(this.cbStop);
            this.groupBox3.Controls.Add(this.cbPause);
            this.groupBox3.Controls.Add(this.cbStart);
            this.groupBox3.Controls.Add(this.cbSetCurrent);
            this.groupBox3.Controls.Add(this.lbStatus);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tbCurrent
            // 
            resources.ApplyResources(this.tbCurrent, "tbCurrent");
            this.tbCurrent.BackColor = System.Drawing.Color.Gainsboro;
            this.tbCurrent.ForeColor = System.Drawing.Color.RoyalBlue;
            this.tbCurrent.Name = "tbCurrent";
            this.tbCurrent.ThumbColor = System.Drawing.Color.DarkMagenta;
            this.tbCurrent.ValueChanged += new System.EventHandler(this.tbCurrent_ValueChanged);
            // 
            // lbCurrent
            // 
            resources.ApplyResources(this.lbCurrent, "lbCurrent");
            this.lbCurrent.BackColor = System.Drawing.Color.DarkMagenta;
            this.lbCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbCurrent.ForeColor = System.Drawing.Color.White;
            this.lbCurrent.Name = "lbCurrent";
            // 
            // cbCurrPlus
            // 
            this.cbCurrPlus.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.cbCurrPlus, "cbCurrPlus");
            this.cbCurrPlus.FlatAppearance.BorderSize = 0;
            this.cbCurrPlus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbCurrPlus.ForeColor = System.Drawing.Color.White;
            this.cbCurrPlus.Image = global::MDM.Properties.Resources.stop_red1;
            this.cbCurrPlus.Name = "cbCurrPlus";
            this.cbCurrPlus.UseVisualStyleBackColor = false;
            this.cbCurrPlus.Click += new System.EventHandler(this.cbCurrPlus_Click);
            // 
            // cbCurrMinus
            // 
            this.cbCurrMinus.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.cbCurrMinus, "cbCurrMinus");
            this.cbCurrMinus.FlatAppearance.BorderSize = 0;
            this.cbCurrMinus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbCurrMinus.ForeColor = System.Drawing.Color.White;
            this.cbCurrMinus.Image = global::MDM.Properties.Resources.erase1;
            this.cbCurrMinus.Name = "cbCurrMinus";
            this.cbCurrMinus.UseVisualStyleBackColor = false;
            this.cbCurrMinus.Click += new System.EventHandler(this.cbCurrMinus_Click);
            // 
            // cbStop
            // 
            this.cbStop.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.cbStop, "cbStop");
            this.cbStop.FlatAppearance.BorderSize = 0;
            this.cbStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbStop.ForeColor = System.Drawing.Color.White;
            this.cbStop.Image = global::MDM.Properties.Resources.stop;
            this.cbStop.Name = "cbStop";
            this.cbStop.UseVisualStyleBackColor = false;
            this.cbStop.Click += new System.EventHandler(this.cbStop_Click);
            // 
            // cbPause
            // 
            this.cbPause.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.cbPause, "cbPause");
            this.cbPause.FlatAppearance.BorderSize = 0;
            this.cbPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbPause.ForeColor = System.Drawing.Color.White;
            this.cbPause.Image = global::MDM.Properties.Resources.pause;
            this.cbPause.Name = "cbPause";
            this.cbPause.UseVisualStyleBackColor = false;
            this.cbPause.Click += new System.EventHandler(this.cbPause_Click);
            // 
            // cbStart
            // 
            resources.ApplyResources(this.cbStart, "cbStart");
            this.cbStart.BackColor = System.Drawing.Color.RoyalBlue;
            this.cbStart.FlatAppearance.BorderSize = 0;
            this.cbStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbStart.ForeColor = System.Drawing.Color.White;
            this.cbStart.Image = global::MDM.Properties.Resources.start;
            this.cbStart.Name = "cbStart";
            this.cbStart.UseVisualStyleBackColor = false;
            this.cbStart.Click += new System.EventHandler(this.cbStart_Click);
            // 
            // cbSetCurrent
            // 
            resources.ApplyResources(this.cbSetCurrent, "cbSetCurrent");
            this.cbSetCurrent.BackColor = System.Drawing.Color.RoyalBlue;
            this.cbSetCurrent.FlatAppearance.BorderSize = 0;
            this.cbSetCurrent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbSetCurrent.ForeColor = System.Drawing.Color.White;
            this.cbSetCurrent.Image = global::MDM.Properties.Resources.stop_red;
            this.cbSetCurrent.Name = "cbSetCurrent";
            this.cbSetCurrent.UseVisualStyleBackColor = false;
            this.cbSetCurrent.EnabledChanged += new System.EventHandler(this.cbSetCurrent_EnabledChanged);
            this.cbSetCurrent.Click += new System.EventHandler(this.cbSetCurrent_Click);
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.BackColor = System.Drawing.Color.Goldenrod;
            this.lbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbStatus.Name = "lbStatus";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPatSelect);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lbProcNum);
            this.groupBox2.Controls.Add(this.lbDiagnosis);
            this.groupBox2.Controls.Add(this.lbPatName);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cbPatSelect
            // 
            resources.ApplyResources(this.cbPatSelect, "cbPatSelect");
            this.cbPatSelect.BackColor = System.Drawing.Color.RoyalBlue;
            this.cbPatSelect.FlatAppearance.BorderSize = 0;
            this.cbPatSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.cbPatSelect.ForeColor = System.Drawing.Color.White;
            this.cbPatSelect.Name = "cbPatSelect";
            this.cbPatSelect.UseVisualStyleBackColor = false;
            this.cbPatSelect.Click += new System.EventHandler(this.cbPatSelect_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lbProcNum
            // 
            resources.ApplyResources(this.lbProcNum, "lbProcNum");
            this.lbProcNum.BackColor = System.Drawing.Color.DarkMagenta;
            this.lbProcNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbProcNum.ForeColor = System.Drawing.Color.White;
            this.lbProcNum.Name = "lbProcNum";
            // 
            // lbDiagnosis
            // 
            resources.ApplyResources(this.lbDiagnosis, "lbDiagnosis");
            this.lbDiagnosis.BackColor = System.Drawing.Color.DarkMagenta;
            this.lbDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbDiagnosis.ForeColor = System.Drawing.Color.White;
            this.lbDiagnosis.Name = "lbDiagnosis";
            // 
            // lbPatName
            // 
            resources.ApplyResources(this.lbPatName, "lbPatName");
            this.lbPatName.BackColor = System.Drawing.Color.DarkMagenta;
            this.lbPatName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbPatName.ForeColor = System.Drawing.Color.White;
            this.lbPatName.Name = "lbPatName";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.Led);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Name = "panel1";
            // 
            // pbProgress
            // 
            resources.ApplyResources(this.pbProgress, "pbProgress");
            this.pbProgress.ForeColor = System.Drawing.Color.SteelBlue;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Step = 1;
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Led
            // 
            resources.ApplyResources(this.Led, "Led");
            this.Led.Name = "Led";
            this.Led.On = true;
            // 
            // pbStatus
            // 
            resources.ApplyResources(this.pbStatus, "pbStatus");
            this.pbStatus.Image = global::MDM.Properties.Resources.stimsmart_kanal_pripraven;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.TabStop = false;
            // 
            // elementHost1
            // 
            resources.ApplyResources(this.elementHost1, "elementHost1");
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Child = this.ucMonitor;
            // 
            // Channel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Channel";
            this.SizeChanged += new System.EventHandler(this.Channel_SizeChanged);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbCurrent;
        private System.Windows.Forms.Button cbStop;
        private System.Windows.Forms.Button cbPause;
        private System.Windows.Forms.Button cbStart;
        private System.Windows.Forms.Button cbSetCurrent;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cbPatSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbProcNum;
        private System.Windows.Forms.Label lbDiagnosis;
        private System.Windows.Forms.Label lbPatName;
        private System.Windows.Forms.Panel panel3;
        private Bulb.LedBulb Led;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Integration.ElementHost elementHost1;
        internal WpfUC.ucMonitor ucMonitor;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cbCurrPlus;
        private System.Windows.Forms.Button cbCurrMinus;
        private MDMTrackBar tbCurrent;
    }
}
