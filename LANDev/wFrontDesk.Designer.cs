namespace LANDev
{
    partial class wFrontDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wFrontDesk));
            this.cbClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.channel6 = new LANDev.Channel();
            this.channel5 = new LANDev.Channel();
            this.channel4 = new LANDev.Channel();
            this.channel3 = new LANDev.Channel();
            this.channel2 = new LANDev.Channel();
            this.channel1 = new LANDev.Channel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbOnOff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.led = new Bulb.LedBulb();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbClose
            // 
            this.cbClose.BackColor = System.Drawing.Color.Transparent;
            this.cbClose.BackgroundImage = global::LANDev.Properties.Resources.background;
            this.cbClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbClose.Location = new System.Drawing.Point(778, 0);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(26, 23);
            this.cbClose.TabIndex = 3;
            this.cbClose.Text = "X";
            this.cbClose.UseVisualStyleBackColor = false;
            this.cbClose.Click += new System.EventHandler(this.cbClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::LANDev.Properties.Resources.background;
            this.panel2.Controls.Add(this.channel6);
            this.panel2.Controls.Add(this.channel5);
            this.panel2.Controls.Add(this.channel4);
            this.panel2.Controls.Add(this.channel3);
            this.panel2.Controls.Add(this.channel2);
            this.panel2.Controls.Add(this.channel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(804, 296);
            this.panel2.TabIndex = 1;
            // 
            // channel6
            // 
            this.channel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel6.BackgroundImage")));
            this.channel6.Location = new System.Drawing.Point(666, 6);
            this.channel6.Name = "channel6";
            this.channel6.Number = ((byte)(6));
            this.channel6.Size = new System.Drawing.Size(132, 292);
            //this.channel6.Status = LANDev.ChannelStatus.Disabled;
            this.channel6.TabIndex = 0;
            // 
            // channel5
            // 
            this.channel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel5.BackgroundImage")));
            this.channel5.Location = new System.Drawing.Point(534, 6);
            this.channel5.Name = "channel5";
            this.channel5.Number = ((byte)(5));
            this.channel5.Size = new System.Drawing.Size(132, 292);
            //this.channel5.Status = LANDev.ChannelStatus.Disabled;
            this.channel5.TabIndex = 0;
            // 
            // channel4
            // 
            this.channel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel4.BackgroundImage")));
            this.channel4.Location = new System.Drawing.Point(402, 6);
            this.channel4.Name = "channel4";
            this.channel4.Number = ((byte)(4));
            this.channel4.Size = new System.Drawing.Size(132, 292);
            //this.channel4.Status = LANDev.ChannelStatus.Disabled;
            this.channel4.TabIndex = 0;
            // 
            // channel3
            // 
            this.channel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel3.BackgroundImage")));
            this.channel3.Location = new System.Drawing.Point(270, 6);
            this.channel3.Name = "channel3";
            this.channel3.Number = ((byte)(3));
            this.channel3.Size = new System.Drawing.Size(132, 292);
            //this.channel3.Status = LANDev.ChannelStatus.Disabled;
            this.channel3.TabIndex = 0;
            // 
            // channel2
            // 
            this.channel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel2.BackgroundImage")));
            this.channel2.Location = new System.Drawing.Point(138, 6);
            this.channel2.Name = "channel2";
            this.channel2.Number = ((byte)(2));
            this.channel2.Size = new System.Drawing.Size(132, 292);
            //this.channel2.Status = LANDev.ChannelStatus.Disabled;
            this.channel2.TabIndex = 0;
            // 
            // channel1
            // 
            this.channel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("channel1.BackgroundImage")));
            this.channel1.Location = new System.Drawing.Point(6, 6);
            this.channel1.Name = "channel1";
            this.channel1.Number = ((byte)(1));
            this.channel1.Size = new System.Drawing.Size(132, 292);
            //this.channel1.Status = LANDev.ChannelStatus.Disabled;
            this.channel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::LANDev.Properties.Resources.background;
            this.panel1.Controls.Add(this.cbOnOff);
            this.panel1.Controls.Add(this.cbClose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.led);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 80);
            this.panel1.TabIndex = 0;
            // 
            // cbOnOff
            // 
            this.cbOnOff.BackgroundImage = global::LANDev.Properties.Resources.switch_Off;
            this.cbOnOff.Location = new System.Drawing.Point(666, 16);
            this.cbOnOff.Name = "cbOnOff";
            this.cbOnOff.Size = new System.Drawing.Size(48, 48);
            this.cbOnOff.TabIndex = 4;
            this.cbOnOff.UseVisualStyleBackColor = true;
            this.cbOnOff.Click += new System.EventHandler(this.cbOnOff_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(67, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "POWER";
            // 
            // led
            // 
            this.led.BackColor = System.Drawing.Color.Transparent;
            this.led.Location = new System.Drawing.Point(155, 27);
            this.led.Name = "led";
            this.led.On = false;
            this.led.Size = new System.Drawing.Size(23, 23);
            this.led.TabIndex = 0;
            this.led.Text = "ledBulb1";
            // 
            // wFrontDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbClose;
            this.ClientSize = new System.Drawing.Size(804, 376);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wFrontDesk";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrontDesk";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Channel channel6;
        private Channel channel5;
        private Channel channel4;
        private Channel channel3;
        private Channel channel2;
        private Channel channel1;
        private System.Windows.Forms.Label label1;
        private Bulb.LedBulb led;
        private System.Windows.Forms.Button cbClose;
        private System.Windows.Forms.Button cbOnOff;
    }
}

