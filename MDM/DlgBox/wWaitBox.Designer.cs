namespace MDM.DlgBox
{
    partial class wWaitBox
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
            this.lbWaitMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbWaitMsg
            // 
            this.lbWaitMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWaitMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbWaitMsg.Location = new System.Drawing.Point(0, 0);
            this.lbWaitMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWaitMsg.Name = "lbWaitMsg";
            this.lbWaitMsg.Size = new System.Drawing.Size(342, 68);
            this.lbWaitMsg.TabIndex = 0;
            this.lbWaitMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wWaitBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(342, 68);
            this.Controls.Add(this.lbWaitMsg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.Color.Yellow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "wWaitBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wWaitBox";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbWaitMsg;
    }
}