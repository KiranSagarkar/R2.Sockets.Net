namespace R2.Sockets.Net
{
    partial class FormMCastReceiverN
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
            if (disposing && (components != null))
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
            this.lblLastPktTime = new System.Windows.Forms.Label();
            this.lblSourceIP = new System.Windows.Forms.Label();
            this.lblPktSize = new System.Windows.Forms.Label();
            this.lblInDataSize = new System.Windows.Forms.Label();
            this.lblInPktCount = new System.Windows.Forms.Label();
            this.txtMCastGroup = new System.Windows.Forms.TextBox();
            this.label4Mcast = new System.Windows.Forms.Label();
            this.chkUseIpFilter = new System.Windows.Forms.CheckBox();
            this.txtSourceIPMCast = new System.Windows.Forms.TextBox();
            this.label6MCast = new System.Windows.Forms.Label();
            this.chkActiveMCast = new System.Windows.Forms.CheckBox();
            this.txtLANIPMCast = new System.Windows.Forms.TextBox();
            this.label1MCast = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2MCast = new System.Windows.Forms.Label();
            this.checkBoxShow = new System.Windows.Forms.CheckBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lblLastPktTime
            // 
            this.lblLastPktTime.BackColor = System.Drawing.Color.White;
            this.lblLastPktTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLastPktTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastPktTime.Location = new System.Drawing.Point(336, 184);
            this.lblLastPktTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastPktTime.Name = "lblLastPktTime";
            this.lblLastPktTime.Size = new System.Drawing.Size(86, 19);
            this.lblLastPktTime.TabIndex = 35;
            this.lblLastPktTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSourceIP
            // 
            this.lblSourceIP.BackColor = System.Drawing.Color.LightYellow;
            this.lblSourceIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSourceIP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSourceIP.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSourceIP.Location = new System.Drawing.Point(239, 184);
            this.lblSourceIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSourceIP.Name = "lblSourceIP";
            this.lblSourceIP.Size = new System.Drawing.Size(94, 19);
            this.lblSourceIP.TabIndex = 34;
            this.lblSourceIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPktSize
            // 
            this.lblPktSize.BackColor = System.Drawing.Color.LightYellow;
            this.lblPktSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPktSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPktSize.Location = new System.Drawing.Point(179, 184);
            this.lblPktSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPktSize.Name = "lblPktSize";
            this.lblPktSize.Size = new System.Drawing.Size(59, 19);
            this.lblPktSize.TabIndex = 33;
            this.lblPktSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInDataSize
            // 
            this.lblInDataSize.BackColor = System.Drawing.Color.LightYellow;
            this.lblInDataSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInDataSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInDataSize.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblInDataSize.Location = new System.Drawing.Point(71, 184);
            this.lblInDataSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInDataSize.Name = "lblInDataSize";
            this.lblInDataSize.Size = new System.Drawing.Size(108, 19);
            this.lblInDataSize.TabIndex = 32;
            this.lblInDataSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInPktCount
            // 
            this.lblInPktCount.BackColor = System.Drawing.Color.LightYellow;
            this.lblInPktCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInPktCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInPktCount.Location = new System.Drawing.Point(11, 184);
            this.lblInPktCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInPktCount.Name = "lblInPktCount";
            this.lblInPktCount.Size = new System.Drawing.Size(59, 19);
            this.lblInPktCount.TabIndex = 31;
            this.lblInPktCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMCastGroup
            // 
            this.txtMCastGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCastGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMCastGroup.Location = new System.Drawing.Point(116, 11);
            this.txtMCastGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMCastGroup.MaxLength = 15;
            this.txtMCastGroup.Name = "txtMCastGroup";
            this.txtMCastGroup.Size = new System.Drawing.Size(114, 23);
            this.txtMCastGroup.TabIndex = 21;
            this.txtMCastGroup.Text = "233.3.3.3";
            // 
            // label4Mcast
            // 
            this.label4Mcast.AutoSize = true;
            this.label4Mcast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4Mcast.Location = new System.Drawing.Point(35, 13);
            this.label4Mcast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4Mcast.Name = "label4Mcast";
            this.label4Mcast.Size = new System.Drawing.Size(77, 15);
            this.label4Mcast.TabIndex = 30;
            this.label4Mcast.Text = "MCast Group";
            // 
            // chkUseIpFilter
            // 
            this.chkUseIpFilter.AutoSize = true;
            this.chkUseIpFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkUseIpFilter.Location = new System.Drawing.Point(247, 106);
            this.chkUseIpFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkUseIpFilter.Name = "chkUseIpFilter";
            this.chkUseIpFilter.Size = new System.Drawing.Size(107, 19);
            this.chkUseIpFilter.TabIndex = 27;
            this.chkUseIpFilter.Text = "Apply IP Filter ?";
            this.chkUseIpFilter.UseVisualStyleBackColor = true;
            // 
            // txtSourceIPMCast
            // 
            this.txtSourceIPMCast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourceIPMCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSourceIPMCast.Location = new System.Drawing.Point(116, 103);
            this.txtSourceIPMCast.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSourceIPMCast.MaxLength = 15;
            this.txtSourceIPMCast.Name = "txtSourceIPMCast";
            this.txtSourceIPMCast.Size = new System.Drawing.Size(114, 23);
            this.txtSourceIPMCast.TabIndex = 26;
            // 
            // label6MCast
            // 
            this.label6MCast.AutoSize = true;
            this.label6MCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6MCast.Location = new System.Drawing.Point(24, 107);
            this.label6MCast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6MCast.Name = "label6MCast";
            this.label6MCast.Size = new System.Drawing.Size(84, 15);
            this.label6MCast.TabIndex = 29;
            this.label6MCast.Text = "Valid Source IP";
            // 
            // chkActiveMCast
            // 
            this.chkActiveMCast.AutoSize = true;
            this.chkActiveMCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkActiveMCast.Location = new System.Drawing.Point(116, 135);
            this.chkActiveMCast.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkActiveMCast.Name = "chkActiveMCast";
            this.chkActiveMCast.Size = new System.Drawing.Size(57, 19);
            this.chkActiveMCast.TabIndex = 28;
            this.chkActiveMCast.Text = "Listen";
            this.chkActiveMCast.UseVisualStyleBackColor = true;
            this.chkActiveMCast.CheckedChanged += new System.EventHandler(this.chkActiveMCast_CheckedChanged);
            // 
            // txtLANIPMCast
            // 
            this.txtLANIPMCast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLANIPMCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLANIPMCast.Location = new System.Drawing.Point(116, 72);
            this.txtLANIPMCast.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLANIPMCast.MaxLength = 15;
            this.txtLANIPMCast.Name = "txtLANIPMCast";
            this.txtLANIPMCast.Size = new System.Drawing.Size(114, 23);
            this.txtLANIPMCast.TabIndex = 23;
            // 
            // label1MCast
            // 
            this.label1MCast.AutoSize = true;
            this.label1MCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1MCast.Location = new System.Drawing.Point(40, 75);
            this.label1MCast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1MCast.Name = "label1MCast";
            this.label1MCast.Size = new System.Drawing.Size(71, 15);
            this.label1MCast.TabIndex = 20;
            this.label1MCast.Text = "LAN Card IP";
            // 
            // textBoxPort
            // 
            this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxPort.Location = new System.Drawing.Point(116, 41);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPort.MaxLength = 5;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(58, 23);
            this.textBoxPort.TabIndex = 22;
            this.textBoxPort.Text = "34567";
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2MCast
            // 
            this.label2MCast.AutoSize = true;
            this.label2MCast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2MCast.Location = new System.Drawing.Point(83, 44);
            this.label2MCast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2MCast.Name = "label2MCast";
            this.label2MCast.Size = new System.Drawing.Size(29, 15);
            this.label2MCast.TabIndex = 25;
            this.label2MCast.Text = "Port";
            // 
            // checkBoxShow
            // 
            this.checkBoxShow.AutoSize = true;
            this.checkBoxShow.Location = new System.Drawing.Point(12, 164);
            this.checkBoxShow.Name = "checkBoxShow";
            this.checkBoxShow.Size = new System.Drawing.Size(53, 17);
            this.checkBoxShow.TabIndex = 36;
            this.checkBoxShow.Text = "Show";
            this.checkBoxShow.UseVisualStyleBackColor = true;
            this.checkBoxShow.CheckedChanged += new System.EventHandler(this.checkBoxShow_CheckedChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(347, 158);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 37;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // FormMCastReceiverN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 214);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.checkBoxShow);
            this.Controls.Add(this.lblLastPktTime);
            this.Controls.Add(this.lblSourceIP);
            this.Controls.Add(this.lblPktSize);
            this.Controls.Add(this.lblInDataSize);
            this.Controls.Add(this.lblInPktCount);
            this.Controls.Add(this.txtMCastGroup);
            this.Controls.Add(this.label4Mcast);
            this.Controls.Add(this.chkUseIpFilter);
            this.Controls.Add(this.txtSourceIPMCast);
            this.Controls.Add(this.label6MCast);
            this.Controls.Add(this.chkActiveMCast);
            this.Controls.Add(this.txtLANIPMCast);
            this.Controls.Add(this.label1MCast);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2MCast);
            this.MaximizeBox = false;
            this.Name = "FormMCastReceiverN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MCastReceiver Native";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMCastReceiver_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMCastReceiverN_FormClosed);
            this.Load += new System.EventHandler(this.FormMCastReceiver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLastPktTime;
        private System.Windows.Forms.Label lblSourceIP;
        private System.Windows.Forms.Label lblPktSize;
        private System.Windows.Forms.Label lblInDataSize;
        private System.Windows.Forms.Label lblInPktCount;
        private System.Windows.Forms.TextBox txtMCastGroup;
        private System.Windows.Forms.Label label4Mcast;
        private System.Windows.Forms.CheckBox chkUseIpFilter;
        private System.Windows.Forms.TextBox txtSourceIPMCast;
        private System.Windows.Forms.Label label6MCast;
        private System.Windows.Forms.CheckBox chkActiveMCast;
        private System.Windows.Forms.TextBox txtLANIPMCast;
        private System.Windows.Forms.Label label1MCast;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2MCast;
        private System.Windows.Forms.CheckBox checkBoxShow;
        private System.Windows.Forms.Button buttonReset;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}