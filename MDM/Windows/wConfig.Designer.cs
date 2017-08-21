namespace MDM.Windows
{
    partial class wConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wConfig));
            this.nNOC = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nNOP = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nProcDur = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nMinProcDur = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nMinTimeInt = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxCurrLang = new System.Windows.Forms.ComboBox();
            this.cbRemoveLang = new System.Windows.Forms.Button();
            this.cbAddLang = new System.Windows.Forms.Button();
            this.lbxLanguages = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbCancel = new System.Windows.Forms.Button();
            this.cbOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nNOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nProcDur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMinProcDur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMinTimeInt)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nNOC
            // 
            resources.ApplyResources(this.nNOC, "nNOC");
            this.nNOC.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nNOC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nNOC.Name = "nNOC";
            this.nNOC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // nNOP
            // 
            resources.ApplyResources(this.nNOP, "nNOP");
            this.nNOP.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nNOP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nNOP.Name = "nNOP";
            this.nNOP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // nProcDur
            // 
            resources.ApplyResources(this.nProcDur, "nProcDur");
            this.nProcDur.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nProcDur.Minimum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nProcDur.Name = "nProcDur";
            this.nProcDur.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nMinProcDur
            // 
            resources.ApplyResources(this.nMinProcDur, "nMinProcDur");
            this.nMinProcDur.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nMinProcDur.Name = "nMinProcDur";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // nMinTimeInt
            // 
            resources.ApplyResources(this.nMinTimeInt, "nMinTimeInt");
            this.nMinTimeInt.Name = "nMinTimeInt";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxCurrLang);
            this.groupBox1.Controls.Add(this.cbRemoveLang);
            this.groupBox1.Controls.Add(this.cbAddLang);
            this.groupBox1.Controls.Add(this.lbxLanguages);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbxCurrLang
            // 
            this.cbxCurrLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCurrLang.FormattingEnabled = true;
            resources.ApplyResources(this.cbxCurrLang, "cbxCurrLang");
            this.cbxCurrLang.Name = "cbxCurrLang";
            // 
            // cbRemoveLang
            // 
            this.cbRemoveLang.Image = global::MDM.Properties.Resources.minus;
            resources.ApplyResources(this.cbRemoveLang, "cbRemoveLang");
            this.cbRemoveLang.Name = "cbRemoveLang";
            this.cbRemoveLang.UseVisualStyleBackColor = true;
            // 
            // cbAddLang
            // 
            this.cbAddLang.Image = global::MDM.Properties.Resources.plus;
            resources.ApplyResources(this.cbAddLang, "cbAddLang");
            this.cbAddLang.Name = "cbAddLang";
            this.cbAddLang.UseVisualStyleBackColor = true;
            // 
            // lbxLanguages
            // 
            this.lbxLanguages.FormattingEnabled = true;
            resources.ApplyResources(this.lbxLanguages, "lbxLanguages");
            this.lbxLanguages.Name = "lbxLanguages";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.cbCancel);
            this.panel1.Controls.Add(this.cbOK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbCancel.Image = global::MDM.Properties.Resources.no;
            resources.ApplyResources(this.cbCancel, "cbCancel");
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.UseVisualStyleBackColor = true;
            // 
            // cbOK
            // 
            this.cbOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbOK.Image = global::MDM.Properties.Resources.yes;
            resources.ApplyResources(this.cbOK, "cbOK");
            this.cbOK.Name = "cbOK";
            this.cbOK.UseVisualStyleBackColor = true;
            this.cbOK.Click += new System.EventHandler(this.cbOK_Click);
            // 
            // wConfig
            // 
            this.AcceptButton = this.cbOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbCancel;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nMinTimeInt);
            this.Controls.Add(this.nMinProcDur);
            this.Controls.Add(this.nProcDur);
            this.Controls.Add(this.nNOP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nNOC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "wConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.nNOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nProcDur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMinProcDur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMinTimeInt)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nNOC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nNOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nProcDur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nMinProcDur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nMinTimeInt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbxLanguages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxCurrLang;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cbCancel;
        private System.Windows.Forms.Button cbOK;
        private System.Windows.Forms.Button cbAddLang;
        private System.Windows.Forms.Button cbRemoveLang;
    }
}