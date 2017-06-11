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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbHypotype = new System.Windows.Forms.RadioButton();
            this.rbNormotype = new System.Windows.Forms.RadioButton();
            this.rbHyperergic = new System.Windows.Forms.RadioButton();
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxHIC = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbCancel.Image = global::MDM.Properties.Resources.no;
            resources.ApplyResources(this.cbCancel, "cbCancel");
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.UseVisualStyleBackColor = true;
            // 
            // cbSave
            // 
            this.cbSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbSave.Image = global::MDM.Properties.Resources.savedatabase;
            resources.ApplyResources(this.cbSave, "cbSave");
            this.cbSave.Name = "cbSave";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // txtFName
            // 
            resources.ApplyResources(this.txtFName, "txtFName");
            this.txtFName.Name = "txtFName";
            // 
            // lbID
            // 
            this.lbID.BackColor = System.Drawing.SystemColors.Window;
            this.lbID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lbID, "lbID");
            this.lbID.Name = "lbID";
            // 
            // txtMName
            // 
            resources.ApplyResources(this.txtMName, "txtMName");
            this.txtMName.Name = "txtMName";
            // 
            // txtLName
            // 
            resources.ApplyResources(this.txtLName, "txtLName");
            this.txtLName.Name = "txtLName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
            this.dtpBirthDate.Name = "dtpBirthDate";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFemale);
            this.groupBox1.Controls.Add(this.rbMale);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // rbFemale
            // 
            resources.ApplyResources(this.rbFemale, "rbFemale");
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            resources.ApplyResources(this.rbMale, "rbMale");
            this.rbMale.Checked = true;
            this.rbMale.Name = "rbMale";
            this.rbMale.TabStop = true;
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtAddr
            // 
            resources.ApplyResources(this.txtAddr, "txtAddr");
            this.txtAddr.Name = "txtAddr";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtZip
            // 
            resources.ApplyResources(this.txtZip, "txtZip");
            this.txtZip.Name = "txtZip";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Name = "txtCountry";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtPhone
            // 
            resources.ApplyResources(this.txtPhone, "txtPhone");
            this.txtPhone.Name = "txtPhone";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtMedRec
            // 
            resources.ApplyResources(this.txtMedRec, "txtMedRec");
            this.txtMedRec.Name = "txtMedRec";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtDiagnosis
            // 
            resources.ApplyResources(this.txtDiagnosis, "txtDiagnosis");
            this.txtDiagnosis.Name = "txtDiagnosis";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
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
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbHypotype);
            this.groupBox2.Controls.Add(this.rbNormotype);
            this.groupBox2.Controls.Add(this.rbHyperergic);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rbHypotype
            // 
            resources.ApplyResources(this.rbHypotype, "rbHypotype");
            this.rbHypotype.Name = "rbHypotype";
            this.rbHypotype.UseVisualStyleBackColor = true;
            this.rbHypotype.Click += new System.EventHandler(this.somtype_Click);
            // 
            // rbNormotype
            // 
            resources.ApplyResources(this.rbNormotype, "rbNormotype");
            this.rbNormotype.Checked = true;
            this.rbNormotype.Name = "rbNormotype";
            this.rbNormotype.TabStop = true;
            this.rbNormotype.UseVisualStyleBackColor = true;
            this.rbNormotype.Click += new System.EventHandler(this.somtype_Click);
            // 
            // rbHyperergic
            // 
            resources.ApplyResources(this.rbHyperergic, "rbHyperergic");
            this.rbHyperergic.Name = "rbHyperergic";
            this.rbHyperergic.UseVisualStyleBackColor = true;
            this.rbHyperergic.Click += new System.EventHandler(this.somtype_Click);
            // 
            // txtNote
            // 
            resources.ApplyResources(this.txtNote, "txtNote");
            this.txtNote.HideSelection = false;
            this.txtNote.Name = "txtNote";
            this.txtNote.ShowSelectionMargin = true;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // cbxHIC
            // 
            this.cbxHIC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHIC.FormattingEnabled = true;
            resources.ApplyResources(this.cbxHIC, "cbxHIC");
            this.cbxHIC.Name = "cbxHIC";
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
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
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
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wPatient_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbHypotype;
        private System.Windows.Forms.RadioButton rbNormotype;
        private System.Windows.Forms.RadioButton rbHyperergic;
        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxHIC;
    }
}