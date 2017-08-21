namespace MDM.Windows
{
    partial class wPatient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wPatient));
            this.cbCancel = new System.Windows.Forms.Button();
            this.cbSave = new System.Windows.Forms.Button();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.lbID = new System.Windows.Forms.Label();
            this.txtMName = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.gbSex = new System.Windows.Forms.GroupBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMedRec = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDiagnosis = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbSomatotype = new System.Windows.Forms.GroupBox();
            this.rbHypotype = new System.Windows.Forms.RadioButton();
            this.rbNormotype = new System.Windows.Forms.RadioButton();
            this.rbHyperergic = new System.Windows.Forms.RadioButton();
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxHIC = new System.Windows.Forms.ComboBox();
            this.gbSex.SuspendLayout();
            this.gbSomatotype.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCancel
            // 
            resources.ApplyResources(this.cbCancel, "cbCancel");
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbCancel.Image = global::MDM.Properties.Resources.no;
            this.cbCancel.Name = "cbCancel";
            this.toolTip.SetToolTip(this.cbCancel, resources.GetString("cbCancel.ToolTip"));
            this.cbCancel.UseVisualStyleBackColor = true;
            // 
            // cbSave
            // 
            resources.ApplyResources(this.cbSave, "cbSave");
            this.cbSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbSave.Image = global::MDM.Properties.Resources.savedatabase;
            this.cbSave.Name = "cbSave";
            this.toolTip.SetToolTip(this.cbSave, resources.GetString("cbSave.ToolTip"));
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // txtFName
            // 
            resources.ApplyResources(this.txtFName, "txtFName");
            this.txtFName.Name = "txtFName";
            this.toolTip.SetToolTip(this.txtFName, resources.GetString("txtFName.ToolTip"));
            // 
            // lbID
            // 
            resources.ApplyResources(this.lbID, "lbID");
            this.lbID.BackColor = System.Drawing.SystemColors.Window;
            this.lbID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbID.Name = "lbID";
            this.toolTip.SetToolTip(this.lbID, resources.GetString("lbID.ToolTip"));
            // 
            // txtMName
            // 
            resources.ApplyResources(this.txtMName, "txtMName");
            this.txtMName.Name = "txtMName";
            this.toolTip.SetToolTip(this.txtMName, resources.GetString("txtMName.ToolTip"));
            // 
            // txtLName
            // 
            resources.ApplyResources(this.txtLName, "txtLName");
            this.txtLName.Name = "txtLName";
            this.toolTip.SetToolTip(this.txtLName, resources.GetString("txtLName.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // dtpBirthDate
            // 
            resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.toolTip.SetToolTip(this.dtpBirthDate, resources.GetString("dtpBirthDate.ToolTip"));
            // 
            // gbSex
            // 
            resources.ApplyResources(this.gbSex, "gbSex");
            this.gbSex.Controls.Add(this.rbFemale);
            this.gbSex.Controls.Add(this.rbMale);
            this.gbSex.Name = "gbSex";
            this.gbSex.TabStop = false;
            this.toolTip.SetToolTip(this.gbSex, resources.GetString("gbSex.ToolTip"));
            // 
            // rbFemale
            // 
            resources.ApplyResources(this.rbFemale, "rbFemale");
            this.rbFemale.Name = "rbFemale";
            this.toolTip.SetToolTip(this.rbFemale, resources.GetString("rbFemale.ToolTip"));
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            resources.ApplyResources(this.rbMale, "rbMale");
            this.rbMale.Checked = true;
            this.rbMale.Name = "rbMale";
            this.rbMale.TabStop = true;
            this.toolTip.SetToolTip(this.rbMale, resources.GetString("rbMale.ToolTip"));
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // txtAddr
            // 
            resources.ApplyResources(this.txtAddr, "txtAddr");
            this.txtAddr.Name = "txtAddr";
            this.toolTip.SetToolTip(this.txtAddr, resources.GetString("txtAddr.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            this.toolTip.SetToolTip(this.txtCity, resources.GetString("txtCity.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txtZip
            // 
            resources.ApplyResources(this.txtZip, "txtZip");
            this.txtZip.Name = "txtZip";
            this.toolTip.SetToolTip(this.txtZip, resources.GetString("txtZip.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Name = "txtCountry";
            this.toolTip.SetToolTip(this.txtCountry, resources.GetString("txtCountry.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // txtPhone
            // 
            resources.ApplyResources(this.txtPhone, "txtPhone");
            this.txtPhone.Name = "txtPhone";
            this.toolTip.SetToolTip(this.txtPhone, resources.GetString("txtPhone.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // txtMedRec
            // 
            resources.ApplyResources(this.txtMedRec, "txtMedRec");
            this.txtMedRec.Name = "txtMedRec";
            this.toolTip.SetToolTip(this.txtMedRec, resources.GetString("txtMedRec.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // txtDiagnosis
            // 
            resources.ApplyResources(this.txtDiagnosis, "txtDiagnosis");
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.toolTip.SetToolTip(this.txtDiagnosis, resources.GetString("txtDiagnosis.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // cbDiagnosis
            // 
            resources.ApplyResources(this.cbDiagnosis, "cbDiagnosis");
            this.cbDiagnosis.Name = "cbDiagnosis";
            this.toolTip.SetToolTip(this.cbDiagnosis, resources.GetString("cbDiagnosis.ToolTip"));
            this.cbDiagnosis.UseVisualStyleBackColor = true;
            this.cbDiagnosis.Click += new System.EventHandler(this.cbDiagnosis_Click);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // gbSomatotype
            // 
            resources.ApplyResources(this.gbSomatotype, "gbSomatotype");
            this.gbSomatotype.Controls.Add(this.rbHypotype);
            this.gbSomatotype.Controls.Add(this.rbNormotype);
            this.gbSomatotype.Controls.Add(this.rbHyperergic);
            this.gbSomatotype.Name = "gbSomatotype";
            this.gbSomatotype.TabStop = false;
            this.toolTip.SetToolTip(this.gbSomatotype, resources.GetString("gbSomatotype.ToolTip"));
            // 
            // rbHypotype
            // 
            resources.ApplyResources(this.rbHypotype, "rbHypotype");
            this.rbHypotype.Name = "rbHypotype";
            this.toolTip.SetToolTip(this.rbHypotype, resources.GetString("rbHypotype.ToolTip"));
            this.rbHypotype.UseVisualStyleBackColor = true;
            this.rbHypotype.Click += new System.EventHandler(this.somtype_Click);
            // 
            // rbNormotype
            // 
            resources.ApplyResources(this.rbNormotype, "rbNormotype");
            this.rbNormotype.Checked = true;
            this.rbNormotype.Name = "rbNormotype";
            this.rbNormotype.TabStop = true;
            this.toolTip.SetToolTip(this.rbNormotype, resources.GetString("rbNormotype.ToolTip"));
            this.rbNormotype.UseVisualStyleBackColor = true;
            this.rbNormotype.Click += new System.EventHandler(this.somtype_Click);
            // 
            // rbHyperergic
            // 
            resources.ApplyResources(this.rbHyperergic, "rbHyperergic");
            this.rbHyperergic.Name = "rbHyperergic";
            this.toolTip.SetToolTip(this.rbHyperergic, resources.GetString("rbHyperergic.ToolTip"));
            this.rbHyperergic.UseVisualStyleBackColor = true;
            this.rbHyperergic.Click += new System.EventHandler(this.somtype_Click);
            // 
            // txtNote
            // 
            resources.ApplyResources(this.txtNote, "txtNote");
            this.txtNote.HideSelection = false;
            this.txtNote.Name = "txtNote";
            this.txtNote.ShowSelectionMargin = true;
            this.toolTip.SetToolTip(this.txtNote, resources.GetString("txtNote.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // cbxHIC
            // 
            resources.ApplyResources(this.cbxHIC, "cbxHIC");
            this.cbxHIC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHIC.FormattingEnabled = true;
            this.cbxHIC.Name = "cbxHIC";
            this.toolTip.SetToolTip(this.cbxHIC, resources.GetString("cbxHIC.ToolTip"));
            this.cbxHIC.SelectedIndexChanged += new System.EventHandler(this.cbxHIC_SelectedIndexChanged);
            // 
            // wPatient
            // 
            this.AcceptButton = this.cbSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbCancel;
            this.ControlBox = false;
            this.Controls.Add(this.cbxHIC);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.cbDiagnosis);
            this.Controls.Add(this.gbSomatotype);
            this.Controls.Add(this.gbSex);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.txtMedRec);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.txtMName);
            this.Controls.Add(this.txtCountry);
            this.Controls.Add(this.txtZip);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "wPatient";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wPatient_FormClosing);
            this.gbSex.ResumeLayout(false);
            this.gbSex.PerformLayout();
            this.gbSomatotype.ResumeLayout(false);
            this.gbSomatotype.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbCancel;
        private System.Windows.Forms.Button cbSave;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.TextBox txtMName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.GroupBox gbSex;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMedRec;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cbDiagnosis;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbSomatotype;
        private System.Windows.Forms.RadioButton rbHypotype;
        private System.Windows.Forms.RadioButton rbNormotype;
        private System.Windows.Forms.RadioButton rbHyperergic;
        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxHIC;
    }
}