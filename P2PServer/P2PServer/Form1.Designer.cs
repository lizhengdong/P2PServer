namespace P2PServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MoniterIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MoniterPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.P2Pip = new System.Windows.Forms.TextBox();
            this.P2Pport = new System.Windows.Forms.TextBox();
            this.ShowMoniter = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.KZyuyinS = new System.Windows.Forms.ToolStripStatusLabel();
            this.KZyuyinM = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MonitorIPChange = new System.Windows.Forms.GroupBox();
            this.MonitorStateLabel = new System.Windows.Forms.Label();
            this.StopIPChangeMonitorButton = new System.Windows.Forms.Button();
            this.StartIPChangeMonitorButton = new System.Windows.Forms.Button();
            this.ChangeMailInfoButton = new System.Windows.Forms.Button();
            this.SaveMailInfoButton = new System.Windows.Forms.Button();
            this.ReceiveMailBoxAddTextBox = new System.Windows.Forms.TextBox();
            this.SendMailBoxPasswordTextBox = new System.Windows.Forms.TextBox();
            this.SendMailBoxAddTextBox = new System.Windows.Forms.TextBox();
            this.ReceiveMailBoxAddLabel = new System.Windows.Forms.Label();
            this.SendMailBoxPasswordLabel = new System.Windows.Forms.Label();
            this.SendMailBoxAddLabel = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.MonitorIPChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "控制端信息";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "控制端IP";
            // 
            // MoniterIP
            // 
            this.MoniterIP.Location = new System.Drawing.Point(95, 109);
            this.MoniterIP.Name = "MoniterIP";
            this.MoniterIP.Size = new System.Drawing.Size(180, 21);
            this.MoniterIP.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "控制端端口";
            // 
            // MoniterPort
            // 
            this.MoniterPort.Location = new System.Drawing.Point(390, 109);
            this.MoniterPort.Name = "MoniterPort";
            this.MoniterPort.Size = new System.Drawing.Size(190, 21);
            this.MoniterPort.TabIndex = 4;
            this.MoniterPort.TextChanged += new System.EventHandler(this.MoniterPort_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "第三方服务器信息";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "服务器IP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(307, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "服务器主端口";
            // 
            // P2Pip
            // 
            this.P2Pip.Location = new System.Drawing.Point(95, 37);
            this.P2Pip.Name = "P2Pip";
            this.P2Pip.Size = new System.Drawing.Size(180, 21);
            this.P2Pip.TabIndex = 13;
            // 
            // P2Pport
            // 
            this.P2Pport.Location = new System.Drawing.Point(390, 37);
            this.P2Pport.Name = "P2Pport";
            this.P2Pport.Size = new System.Drawing.Size(190, 21);
            this.P2Pport.TabIndex = 14;
            // 
            // ShowMoniter
            // 
            this.ShowMoniter.Location = new System.Drawing.Point(95, 74);
            this.ShowMoniter.Name = "ShowMoniter";
            this.ShowMoniter.Size = new System.Drawing.Size(180, 21);
            this.ShowMoniter.TabIndex = 15;
            this.ShowMoniter.TextChanged += new System.EventHandler(this.ShowMoniter_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.KZyuyinS,
            this.KZyuyinM});
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(629, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel1.Text = "手机状态";
            // 
            // KZyuyinS
            // 
            this.KZyuyinS.Name = "KZyuyinS";
            this.KZyuyinS.Size = new System.Drawing.Size(0, 17);
            // 
            // KZyuyinM
            // 
            this.KZyuyinM.Name = "KZyuyinM";
            this.KZyuyinM.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MonitorIPChange
            // 
            this.MonitorIPChange.Controls.Add(this.MonitorStateLabel);
            this.MonitorIPChange.Controls.Add(this.StopIPChangeMonitorButton);
            this.MonitorIPChange.Controls.Add(this.StartIPChangeMonitorButton);
            this.MonitorIPChange.Controls.Add(this.ChangeMailInfoButton);
            this.MonitorIPChange.Controls.Add(this.SaveMailInfoButton);
            this.MonitorIPChange.Controls.Add(this.ReceiveMailBoxAddTextBox);
            this.MonitorIPChange.Controls.Add(this.SendMailBoxPasswordTextBox);
            this.MonitorIPChange.Controls.Add(this.SendMailBoxAddTextBox);
            this.MonitorIPChange.Controls.Add(this.ReceiveMailBoxAddLabel);
            this.MonitorIPChange.Controls.Add(this.SendMailBoxPasswordLabel);
            this.MonitorIPChange.Controls.Add(this.SendMailBoxAddLabel);
            this.MonitorIPChange.Location = new System.Drawing.Point(26, 176);
            this.MonitorIPChange.Name = "MonitorIPChange";
            this.MonitorIPChange.Size = new System.Drawing.Size(554, 227);
            this.MonitorIPChange.TabIndex = 32;
            this.MonitorIPChange.TabStop = false;
            this.MonitorIPChange.Text = "监视第三方IP变化";
            this.MonitorIPChange.Visible = false;
            // 
            // MonitorStateLabel
            // 
            this.MonitorStateLabel.AutoSize = true;
            this.MonitorStateLabel.Location = new System.Drawing.Point(12, 209);
            this.MonitorStateLabel.Name = "MonitorStateLabel";
            this.MonitorStateLabel.Size = new System.Drawing.Size(137, 12);
            this.MonitorStateLabel.TabIndex = 10;
            this.MonitorStateLabel.Text = "是否能成功连接到发件箱";
            // 
            // StopIPChangeMonitorButton
            // 
            this.StopIPChangeMonitorButton.Location = new System.Drawing.Point(239, 174);
            this.StopIPChangeMonitorButton.Name = "StopIPChangeMonitorButton";
            this.StopIPChangeMonitorButton.Size = new System.Drawing.Size(166, 23);
            this.StopIPChangeMonitorButton.TabIndex = 9;
            this.StopIPChangeMonitorButton.Text = "停止IP地址变化监视";
            this.StopIPChangeMonitorButton.UseVisualStyleBackColor = true;
            this.StopIPChangeMonitorButton.Click += new System.EventHandler(this.StopIPChangeMonitorButton_Click);
            // 
            // StartIPChangeMonitorButton
            // 
            this.StartIPChangeMonitorButton.Location = new System.Drawing.Point(16, 174);
            this.StartIPChangeMonitorButton.Name = "StartIPChangeMonitorButton";
            this.StartIPChangeMonitorButton.Size = new System.Drawing.Size(174, 23);
            this.StartIPChangeMonitorButton.TabIndex = 8;
            this.StartIPChangeMonitorButton.Text = "启动IP地址变化监视";
            this.StartIPChangeMonitorButton.UseVisualStyleBackColor = true;
            this.StartIPChangeMonitorButton.Click += new System.EventHandler(this.StartIPChangeMonitorButton_Click);
            // 
            // ChangeMailInfoButton
            // 
            this.ChangeMailInfoButton.Location = new System.Drawing.Point(330, 132);
            this.ChangeMailInfoButton.Name = "ChangeMailInfoButton";
            this.ChangeMailInfoButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeMailInfoButton.TabIndex = 7;
            this.ChangeMailInfoButton.Text = "修改";
            this.ChangeMailInfoButton.UseVisualStyleBackColor = true;
            this.ChangeMailInfoButton.Click += new System.EventHandler(this.ChangeMailInfoButton_Click);
            // 
            // SaveMailInfoButton
            // 
            this.SaveMailInfoButton.Location = new System.Drawing.Point(219, 131);
            this.SaveMailInfoButton.Name = "SaveMailInfoButton";
            this.SaveMailInfoButton.Size = new System.Drawing.Size(75, 23);
            this.SaveMailInfoButton.TabIndex = 6;
            this.SaveMailInfoButton.Text = "保存";
            this.SaveMailInfoButton.UseVisualStyleBackColor = true;
            this.SaveMailInfoButton.Click += new System.EventHandler(this.SaveMailInfoButton_Click);
            // 
            // ReceiveMailBoxAddTextBox
            // 
            this.ReceiveMailBoxAddTextBox.Location = new System.Drawing.Point(155, 104);
            this.ReceiveMailBoxAddTextBox.Name = "ReceiveMailBoxAddTextBox";
            this.ReceiveMailBoxAddTextBox.Size = new System.Drawing.Size(250, 21);
            this.ReceiveMailBoxAddTextBox.TabIndex = 5;
            // 
            // SendMailBoxPasswordTextBox
            // 
            this.SendMailBoxPasswordTextBox.Location = new System.Drawing.Point(155, 73);
            this.SendMailBoxPasswordTextBox.Name = "SendMailBoxPasswordTextBox";
            this.SendMailBoxPasswordTextBox.PasswordChar = '*';
            this.SendMailBoxPasswordTextBox.Size = new System.Drawing.Size(250, 21);
            this.SendMailBoxPasswordTextBox.TabIndex = 4;
            // 
            // SendMailBoxAddTextBox
            // 
            this.SendMailBoxAddTextBox.Location = new System.Drawing.Point(155, 41);
            this.SendMailBoxAddTextBox.Name = "SendMailBoxAddTextBox";
            this.SendMailBoxAddTextBox.Size = new System.Drawing.Size(250, 21);
            this.SendMailBoxAddTextBox.TabIndex = 3;
            // 
            // ReceiveMailBoxAddLabel
            // 
            this.ReceiveMailBoxAddLabel.AutoSize = true;
            this.ReceiveMailBoxAddLabel.Location = new System.Drawing.Point(14, 104);
            this.ReceiveMailBoxAddLabel.Name = "ReceiveMailBoxAddLabel";
            this.ReceiveMailBoxAddLabel.Size = new System.Drawing.Size(107, 12);
            this.ReceiveMailBoxAddLabel.TabIndex = 2;
            this.ReceiveMailBoxAddLabel.Text = "接收邮件邮箱地址:";
            // 
            // SendMailBoxPasswordLabel
            // 
            this.SendMailBoxPasswordLabel.AutoSize = true;
            this.SendMailBoxPasswordLabel.Location = new System.Drawing.Point(14, 73);
            this.SendMailBoxPasswordLabel.Name = "SendMailBoxPasswordLabel";
            this.SendMailBoxPasswordLabel.Size = new System.Drawing.Size(107, 12);
            this.SendMailBoxPasswordLabel.TabIndex = 1;
            this.SendMailBoxPasswordLabel.Text = "发送邮件邮箱密码:";
            // 
            // SendMailBoxAddLabel
            // 
            this.SendMailBoxAddLabel.AutoSize = true;
            this.SendMailBoxAddLabel.Location = new System.Drawing.Point(12, 41);
            this.SendMailBoxAddLabel.Name = "SendMailBoxAddLabel";
            this.SendMailBoxAddLabel.Size = new System.Drawing.Size(107, 12);
            this.SendMailBoxAddLabel.TabIndex = 0;
            this.SendMailBoxAddLabel.Text = "发送邮件邮箱地址:";
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 447);
            this.Controls.Add(this.MonitorIPChange);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ShowMoniter);
            this.Controls.Add(this.P2Pport);
            this.Controls.Add(this.P2Pip);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MoniterPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MoniterIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "第三方配置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MonitorIPChange.ResumeLayout(false);
            this.MonitorIPChange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MoniterIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MoniterPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox P2Pip;
        private System.Windows.Forms.TextBox P2Pport;
        private System.Windows.Forms.TextBox ShowMoniter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel KZyuyinS;
        private System.Windows.Forms.ToolStripStatusLabel KZyuyinM;
        private System.Windows.Forms.GroupBox MonitorIPChange;
        private System.Windows.Forms.TextBox ReceiveMailBoxAddTextBox;
        private System.Windows.Forms.TextBox SendMailBoxPasswordTextBox;
        private System.Windows.Forms.TextBox SendMailBoxAddTextBox;
        private System.Windows.Forms.Label ReceiveMailBoxAddLabel;
        private System.Windows.Forms.Label SendMailBoxPasswordLabel;
        private System.Windows.Forms.Label SendMailBoxAddLabel;
        private System.Windows.Forms.Label MonitorStateLabel;
        private System.Windows.Forms.Button StopIPChangeMonitorButton;
        private System.Windows.Forms.Button StartIPChangeMonitorButton;
        private System.Windows.Forms.Button ChangeMailInfoButton;
        private System.Windows.Forms.Button SaveMailInfoButton;
        private System.Windows.Forms.Timer timer2;
    }
}

