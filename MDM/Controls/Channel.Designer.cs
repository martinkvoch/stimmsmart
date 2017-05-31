namespace MDM.Controls
{
    partial class Channel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Channel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Led = new Bulb.LedBulb();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.lbRemain = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.tbCurrent = new System.Windows.Forms.TrackBar();
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
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCurrent)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Led);
            this.groupBox4.Controls.Add(this.lbElapsed);
            this.groupBox4.Controls.Add(this.lbRemain);
            this.groupBox4.Controls.Add(this.pbProgress);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // led
            // 
            resources.ApplyResources(this.Led, "led");
            this.Led.Color = System.Drawing.Color.WhiteSmoke;
            this.Led.Name = "led";
            this.Led.On = true;
            // 
            // lbElapsed
            // 
            resources.ApplyResources(this.lbElapsed, "lbElapsed");
            this.lbElapsed.BackColor = System.Drawing.Color.Olive;
            this.lbElapsed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbElapsed.Name = "lbElapsed";
            // 
            // lbRemain
            // 
            resources.ApplyResources(this.lbRemain, "lbRemain");
            this.lbRemain.Name = "lbRemain";
            // 
            // pbProgress
            // 
            resources.ApplyResources(this.pbProgress, "pbProgress");
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Step = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbCurrent);
            this.groupBox3.Controls.Add(this.tbCurrent);
            this.groupBox3.Controls.Add(this.cbStop);
            this.groupBox3.Controls.Add(this.cbPause);
            this.groupBox3.Controls.Add(this.cbStart);
            this.groupBox3.Controls.Add(this.cbSetCurrent);
            this.groupBox3.Controls.Add(this.lbStatus);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lbCurrent
            // 
            resources.ApplyResources(this.lbCurrent, "lbCurrent");
            this.lbCurrent.BackColor = System.Drawing.SystemColors.Window;
            this.lbCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbCurrent.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbCurrent.Name = "lbCurrent";
            // 
            // tbCurrent
            // 
            resources.ApplyResources(this.tbCurrent, "tbCurrent");
            this.tbCurrent.LargeChange = 10;
            this.tbCurrent.Maximum = 1000;
            this.tbCurrent.Name = "tbCurrent";
            this.tbCurrent.TickFrequency = 100;
            this.tbCurrent.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbCurrent.ValueChanged += new System.EventHandler(this.tbCurrent_ValueChanged);
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
            this.lbProcNum.BackColor = System.Drawing.SystemColors.Window;
            this.lbProcNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lbProcNum, "lbProcNum");
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
            this.Enter += new System.EventHandler(this.Channel_Enter);
            this.Leave += new System.EventHandler(this.Channel_Leave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCurrent)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
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
        private System.Windows.Forms.TrackBar tbCurrent;
        private System.Windows.Forms.Label lbRemain;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lbElapsed;
        private Bulb.LedBulb Led;
    }
}
