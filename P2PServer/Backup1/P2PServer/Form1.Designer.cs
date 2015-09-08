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
            this.label4 = new System.Windows.Forms.Label();
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
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.statusStrip1.SuspendLayout();
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "手机端信息";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "索引服务器信息";
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
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
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
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(13, 191);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(604, 231);
            this.treeView2.TabIndex = 32;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 447);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ShowMoniter);
            this.Controls.Add(this.P2Pport);
            this.Controls.Add(this.P2Pip);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MoniterPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MoniterIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "完善 索引服务器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MoniterIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MoniterPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox P2Pip;
        private System.Windows.Forms.TextBox P2Pport;
        private System.Windows.Forms.TextBox ShowMoniter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ToolStripStatusLabel KZyuyinS;
        private System.Windows.Forms.ToolStripStatusLabel KZyuyinM;
    }
}

