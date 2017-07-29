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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.ucMonitor = new WpfUC.ucMonitor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbRemain = new System.Windows.Forms.Label();
            this.Led = new Bulb.LedBulb();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbCurrent = new EConTech.Windows.MACUI.MACTrackBar();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.cbStop = new System.Windows.Forms.Button();
            this.cbPause = new System.Windows.Forms.Button();
            this.cbStart = new System.Windows.Forms.Button();
            this.cbSetCurrent = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPatSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbProcNum = new System.Windows.Forms.Label();
            this.lbDiagnosis = new System.Windows.Forms.Label();
            this.lbPatName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbStatus);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pbStatus
            // 
            resources.ApplyResources(this.pbStatus, "pbStatus");
            this.pbStatus.Image = global::MDM.Properties.Resources.program_stimsmart_ready;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.elementHost1);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Controls.Add(this.Led);
            this.groupBox4.Controls.Add(this.pbProgress);
            this.groupBox4.Controls.Add(this.lbElapsed);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // elementHost1
            // 
            resources.ApplyResources(this.elementHost1, "elementHost1");
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Child = this.ucMonitor;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.lbRemain);
            this.panel1.Name = "panel1";
            // 
            // lbRemain
            // 
            this.lbRemain.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lbRemain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lbRemain, "lbRemain");
            this.lbRemain.Name = "lbRemain";
            // 
            // Led
            // 
            resources.ApplyResources(this.Led, "Led");
            this.Led.Color = System.Drawing.Color.Black;
            this.Led.Name = "Led";
            this.Led.On = true;
            // 
            // pbProgress
            // 
            resources.ApplyResources(this.pbProgress, "pbProgress");
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Step = 1;
            // 
            // lbElapsed
            // 
            resources.ApplyResources(this.lbElapsed, "lbElapsed");
            this.lbElapsed.Name = "lbElapsed";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCurrent);
            this.groupBox3.Controls.Add(this.lbCurrent);
            this.groupBox3.Controls.Add(this.cbStop);
            this.groupBox3.Controls.Add(this.cbPause);
            this.groupBox3.Controls.Add(this.cbStart);
            this.groupBox3.Controls.Add(this.cbSetCurrent);
            this.groupBox3.Controls.Add(this.lbStatus);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tbCurrent
            // 
            resources.ApplyResources(this.tbCurrent, "tbCurrent");
            this.tbCurrent.BackColor = System.Drawing.Color.Transparent;
            this.tbCurrent.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tbCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.tbCurrent.IndentHeight = 6;
            this.tbCurrent.LargeChange = 1;
            this.tbCurrent.Maximum = 255;
            this.tbCurrent.Minimum = 0;
            this.tbCurrent.Name = "tbCurrent";
            this.tbCurrent.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbCurrent.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.tbCurrent.TickColor = System.Drawing.Color.Indigo;
            this.tbCurrent.TickFrequency = 16;
            this.tbCurrent.TickHeight = 4;
            this.tbCurrent.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbCurrent.TrackerColor = System.Drawing.Color.Indigo;
            this.tbCurrent.TrackerSize = new System.Drawing.Size(16, 16);
            this.tbCurrent.TrackLineColor = System.Drawing.Color.Indigo;
            this.tbCurrent.TrackLineHeight = 6;
            this.tbCurrent.Value = 0;
            this.tbCurrent.ValueChanged += new EConTech.Windows.MACUI.ValueChangedHandler(this.tbCurrent_ValueChanged);
            this.tbCurrent.EnabledChanged += new System.EventHandler(this.tbCurrent_EnabledChanged);
            // 
            // lbCurrent
            // 
            resources.ApplyResources(this.lbCurrent, "lbCurrent");
            this.lbCurrent.BackColor = System.Drawing.SystemColors.Window;
            this.lbCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbCurrent.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbCurrent.Name = "lbCurrent";
            // 
            // cbStop
            // 
            resources.ApplyResources(this.cbStop, "cbStop");
            this.cbStop.Image = global::MDM.Properties.Resources.stop;
            this.cbStop.Name = "cbStop";
            this.cbStop.UseVisualStyleBackColor = true;
            this.cbStop.Click += new System.EventHandler(this.cbStop_Click);
            // 
            // cbPause
            // 
            resources.ApplyResources(this.cbPause, "cbPause");
            this.cbPause.Image = global::MDM.Properties.Resources.pause;
            this.cbPause.Name = "cbPause";
            this.cbPause.UseVisualStyleBackColor = true;
            this.cbPause.Click += new System.EventHandler(this.cbPause_Click);
            // 
            // cbStart
            // 
            resources.ApplyResources(this.cbStart, "cbStart");
            this.cbStart.Image = global::MDM.Properties.Resources.start;
            this.cbStart.Name = "cbStart";
            this.cbStart.UseVisualStyleBackColor = true;
            this.cbStart.Click += new System.EventHandler(this.cbStart_Click);
            // 
            // cbSetCurrent
            // 
            resources.ApplyResources(this.cbSetCurrent, "cbSetCurrent");
            this.cbSetCurrent.Name = "cbSetCurrent";
            this.cbSetCurrent.UseVisualStyleBackColor = true;
            this.cbSetCurrent.EnabledChanged += new System.EventHandler(this.cbSetCurrent_EnabledChanged);
            this.cbSetCurrent.Click += new System.EventHandler(this.cbSetCurrent_Click);
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.BackColor = System.Drawing.SystemColors.Window;
            this.lbStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbStatus.Name = "lbStatus";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPatSelect);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
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
            this.cbPatSelect.Name = "cbPatSelect";
            this.cbPatSelect.UseVisualStyleBackColor = true;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lbProcNum
            // 
            resources.ApplyResources(this.lbProcNum, "lbProcNum");
            this.lbProcNum.BackColor = System.Drawing.SystemColors.Window;
            this.lbProcNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbProcNum.Name = "lbProcNum";
            // 
            // lbDiagnosis
            // 
            resources.ApplyResources(this.lbDiagnosis, "lbDiagnosis");
            this.lbDiagnosis.BackColor = System.Drawing.SystemColors.Window;
            this.lbDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbDiagnosis.Name = "lbDiagnosis";
            // 
            // lbPatName
            // 
            resources.ApplyResources(this.lbPatName, "lbPatName");
            this.lbPatName.BackColor = System.Drawing.SystemColors.Window;
            this.lbPatName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbPatName.Name = "lbPatName";
            // 
            // Channel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "Channel";
            this.FontChanged += new System.EventHandler(this.Channel_FontChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cbPatSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbProcNum;
        private System.Windows.Forms.Label lbDiagnosis;
        private System.Windows.Forms.Label lbPatName;
        private System.Windows.Forms.Button cbStart;
        private System.Windows.Forms.Button cbSetCurrent;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button cbStop;
        private System.Windows.Forms.Button cbPause;
        private System.Windows.Forms.Label lbCurrent;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbElapsed;
        private System.Windows.Forms.ProgressBar pbProgress;
        private Bulb.LedBulb Led;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbRemain;
        private System.Windows.Forms.PictureBox pbStatus;
        private EConTech.Windows.MACUI.MACTrackBar tbCurrent;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private WpfUC.ucMonitor ucMonitor;
    }
}
