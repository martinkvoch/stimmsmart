namespace MDM.Controls
{
    partial class ChannelMonitor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbChStatus = new System.Windows.Forms.Label();
            this.lbDOUT = new System.Windows.Forms.Label();
            this.lbDAC = new System.Windows.Forms.Label();
            this.lbAtCf = new System.Windows.Forms.Label();
            this.lbTick = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbChStatus, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDOUT, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDAC, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbAtCf, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTick, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Yellow;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbChStatus
            // 
            this.lbChStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbChStatus.Location = new System.Drawing.Point(188, 26);
            this.lbChStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbChStatus.Name = "lbChStatus";
            this.lbChStatus.Size = new System.Drawing.Size(43, 39);
            this.lbChStatus.TabIndex = 5;
            this.lbChStatus.Text = "DI";
            this.lbChStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDOUT
            // 
            this.lbDOUT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDOUT.Location = new System.Drawing.Point(161, 26);
            this.lbDOUT.Margin = new System.Windows.Forms.Padding(0);
            this.lbDOUT.Name = "lbDOUT";
            this.lbDOUT.Size = new System.Drawing.Size(27, 39);
            this.lbDOUT.TabIndex = 3;
            this.lbDOUT.Text = "0";
            this.lbDOUT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDAC
            // 
            this.lbDAC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDAC.Location = new System.Drawing.Point(101, 26);
            this.lbDAC.Margin = new System.Windows.Forms.Padding(0);
            this.lbDAC.Name = "lbDAC";
            this.lbDAC.Size = new System.Drawing.Size(60, 39);
            this.lbDAC.TabIndex = 2;
            this.lbDAC.Text = "0000";
            this.lbDAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAtCf
            // 
            this.lbAtCf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAtCf.Location = new System.Drawing.Point(60, 26);
            this.lbAtCf.Margin = new System.Windows.Forms.Padding(0);
            this.lbAtCf.Name = "lbAtCf";
            this.lbAtCf.Size = new System.Drawing.Size(41, 39);
            this.lbAtCf.TabIndex = 1;
            this.lbAtCf.Text = "000";
            this.lbAtCf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTick
            // 
            this.lbTick.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTick.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbTick.Location = new System.Drawing.Point(231, 0);
            this.lbTick.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lbTick.Name = "lbTick";
            this.tableLayoutPanel1.SetRowSpan(this.lbTick, 2);
            this.lbTick.Size = new System.Drawing.Size(43, 60);
            this.lbTick.TabIndex = 4;
            this.lbTick.Text = "●";
            this.lbTick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Location = new System.Drawing.Point(0, 26);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(60, 39);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "0000";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(63, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "ATC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(104, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "DAC";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(164, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "DT";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(191, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 26);
            this.label5.TabIndex = 11;
            this.label5.Text = "ChST";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChannelMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChannelMonitor";
            this.Size = new System.Drawing.Size(274, 65);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbDOUT;
        private System.Windows.Forms.Label lbDAC;
        private System.Windows.Forms.Label lbAtCf;
        private System.Windows.Forms.Label lbTick;
        private System.Windows.Forms.Label lbChStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
