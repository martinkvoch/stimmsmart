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
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbTick = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.lbChStatus, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbDOUT, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbDAC, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAtCf, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTick, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Yellow;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(248, 44);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbChStatus
            // 
            this.lbChStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbChStatus.Location = new System.Drawing.Point(169, 0);
            this.lbChStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbChStatus.Name = "lbChStatus";
            this.lbChStatus.Size = new System.Drawing.Size(39, 44);
            this.lbChStatus.TabIndex = 5;
            this.lbChStatus.Text = "DI";
            this.lbChStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDOUT
            // 
            this.lbDOUT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDOUT.Location = new System.Drawing.Point(145, 0);
            this.lbDOUT.Margin = new System.Windows.Forms.Padding(0);
            this.lbDOUT.Name = "lbDOUT";
            this.lbDOUT.Size = new System.Drawing.Size(24, 44);
            this.lbDOUT.TabIndex = 3;
            this.lbDOUT.Text = "0";
            this.lbDOUT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDAC
            // 
            this.lbDAC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDAC.Location = new System.Drawing.Point(91, 0);
            this.lbDAC.Margin = new System.Windows.Forms.Padding(0);
            this.lbDAC.Name = "lbDAC";
            this.lbDAC.Size = new System.Drawing.Size(54, 44);
            this.lbDAC.TabIndex = 2;
            this.lbDAC.Text = "0000";
            this.lbDAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAtCf
            // 
            this.lbAtCf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAtCf.Location = new System.Drawing.Point(54, 0);
            this.lbAtCf.Margin = new System.Windows.Forms.Padding(0);
            this.lbAtCf.Name = "lbAtCf";
            this.lbAtCf.Size = new System.Drawing.Size(37, 44);
            this.lbAtCf.TabIndex = 1;
            this.lbAtCf.Text = "000";
            this.lbAtCf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Location = new System.Drawing.Point(0, 0);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(54, 44);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "0000";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTick
            // 
            this.lbTick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbTick.Location = new System.Drawing.Point(208, 0);
            this.lbTick.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lbTick.Name = "lbTick";
            this.lbTick.Size = new System.Drawing.Size(40, 39);
            this.lbTick.TabIndex = 4;
            this.lbTick.Text = "●";
            this.lbTick.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChannelMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChannelMonitor";
            this.Size = new System.Drawing.Size(248, 44);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbDOUT;
        private System.Windows.Forms.Label lbDAC;
        private System.Windows.Forms.Label lbAtCf;
        private System.Windows.Forms.Label lbTick;
        private System.Windows.Forms.Label lbChStatus;
    }
}
