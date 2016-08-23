namespace 电机闭环控制
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.scannbut = new System.Windows.Forms.Button();
            this.getdata = new System.Windows.Forms.Button();
            this.KPtra = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.angleLab = new System.Windows.Forms.Label();
            this.gyroLab = new System.Windows.Forms.Label();
            this.KPlab = new System.Windows.Forms.Label();
            this.EPlab = new System.Windows.Forms.Label();
            this.KDlab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EIlab = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.KDtra = new System.Windows.Forms.TrackBar();
            this.EPtra = new System.Windows.Forms.TrackBar();
            this.EItra = new System.Windows.Forms.TrackBar();
            this.Lseedtra = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.Lspeedlab = new System.Windows.Forms.Label();
            this.Rspeedtra = new System.Windows.Forms.TrackBar();
            this.Rspeedlab = new System.Windows.Forms.Label();
            this.Standcheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.KPtra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KDtra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPtra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EItra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lseedtra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rspeedtra)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(260, 100);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // scannbut
            // 
            this.scannbut.Location = new System.Drawing.Point(12, 118);
            this.scannbut.Name = "scannbut";
            this.scannbut.Size = new System.Drawing.Size(75, 23);
            this.scannbut.TabIndex = 2;
            this.scannbut.Text = "扫描";
            this.scannbut.UseVisualStyleBackColor = true;
            this.scannbut.Click += new System.EventHandler(this.scannbut_Click);
            // 
            // getdata
            // 
            this.getdata.Location = new System.Drawing.Point(200, 118);
            this.getdata.Name = "getdata";
            this.getdata.Size = new System.Drawing.Size(75, 23);
            this.getdata.TabIndex = 7;
            this.getdata.Text = "获取数据";
            this.getdata.UseVisualStyleBackColor = true;
            this.getdata.Click += new System.EventHandler(this.getdata_Click);
            // 
            // KPtra
            // 
            this.KPtra.Location = new System.Drawing.Point(306, 12);
            this.KPtra.Maximum = 1500;
            this.KPtra.Name = "KPtra";
            this.KPtra.Size = new System.Drawing.Size(263, 45);
            this.KPtra.TabIndex = 8;
            this.KPtra.Value = 3;
            this.KPtra.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "KP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "EP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Angle:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Gyro:";
            // 
            // angleLab
            // 
            this.angleLab.AutoSize = true;
            this.angleLab.Location = new System.Drawing.Point(76, 156);
            this.angleLab.Name = "angleLab";
            this.angleLab.Size = new System.Drawing.Size(11, 12);
            this.angleLab.TabIndex = 16;
            this.angleLab.Text = "0";
            // 
            // gyroLab
            // 
            this.gyroLab.AutoSize = true;
            this.gyroLab.Location = new System.Drawing.Point(76, 180);
            this.gyroLab.Name = "gyroLab";
            this.gyroLab.Size = new System.Drawing.Size(11, 12);
            this.gyroLab.TabIndex = 17;
            this.gyroLab.Text = "0";
            // 
            // KPlab
            // 
            this.KPlab.AutoSize = true;
            this.KPlab.Location = new System.Drawing.Point(575, 21);
            this.KPlab.Name = "KPlab";
            this.KPlab.Size = new System.Drawing.Size(11, 12);
            this.KPlab.TabIndex = 18;
            this.KPlab.Text = "0";
            // 
            // EPlab
            // 
            this.EPlab.AutoSize = true;
            this.EPlab.Location = new System.Drawing.Point(575, 76);
            this.EPlab.Name = "EPlab";
            this.EPlab.Size = new System.Drawing.Size(11, 12);
            this.EPlab.TabIndex = 19;
            this.EPlab.Text = "0";
            // 
            // KDlab
            // 
            this.KDlab.AutoSize = true;
            this.KDlab.Location = new System.Drawing.Point(575, 49);
            this.KDlab.Name = "KDlab";
            this.KDlab.Size = new System.Drawing.Size(11, 12);
            this.KDlab.TabIndex = 20;
            this.KDlab.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "KD:";
            // 
            // EIlab
            // 
            this.EIlab.AutoSize = true;
            this.EIlab.Location = new System.Drawing.Point(575, 107);
            this.EIlab.Name = "EIlab";
            this.EIlab.Size = new System.Drawing.Size(11, 12);
            this.EIlab.TabIndex = 23;
            this.EIlab.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "EI:";
            // 
            // KDtra
            // 
            this.KDtra.Location = new System.Drawing.Point(306, 43);
            this.KDtra.Maximum = 1500;
            this.KDtra.Name = "KDtra";
            this.KDtra.Size = new System.Drawing.Size(263, 45);
            this.KDtra.TabIndex = 24;
            this.KDtra.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // EPtra
            // 
            this.EPtra.Location = new System.Drawing.Point(306, 74);
            this.EPtra.Maximum = 1500;
            this.EPtra.Name = "EPtra";
            this.EPtra.Size = new System.Drawing.Size(263, 45);
            this.EPtra.TabIndex = 25;
            this.EPtra.Value = 3;
            this.EPtra.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // EItra
            // 
            this.EItra.Location = new System.Drawing.Point(306, 107);
            this.EItra.Maximum = 1500;
            this.EItra.Name = "EItra";
            this.EItra.Size = new System.Drawing.Size(263, 45);
            this.EItra.TabIndex = 26;
            this.EItra.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Lseedtra
            // 
            this.Lseedtra.Location = new System.Drawing.Point(108, 202);
            this.Lseedtra.Maximum = 6000;
            this.Lseedtra.Minimum = -6000;
            this.Lseedtra.Name = "Lseedtra";
            this.Lseedtra.Size = new System.Drawing.Size(461, 45);
            this.Lseedtra.TabIndex = 28;
            this.Lseedtra.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "SPEED：";
            // 
            // Lspeedlab
            // 
            this.Lspeedlab.AutoSize = true;
            this.Lspeedlab.Location = new System.Drawing.Point(575, 206);
            this.Lspeedlab.Name = "Lspeedlab";
            this.Lspeedlab.Size = new System.Drawing.Size(11, 12);
            this.Lspeedlab.TabIndex = 29;
            this.Lspeedlab.Text = "0";
            this.Lspeedlab.Click += new System.EventHandler(this.label8_Click);
            // 
            // Rspeedtra
            // 
            this.Rspeedtra.Location = new System.Drawing.Point(108, 235);
            this.Rspeedtra.Maximum = 6000;
            this.Rspeedtra.Minimum = -6000;
            this.Rspeedtra.Name = "Rspeedtra";
            this.Rspeedtra.Size = new System.Drawing.Size(461, 45);
            this.Rspeedtra.TabIndex = 30;
            this.Rspeedtra.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // Rspeedlab
            // 
            this.Rspeedlab.AutoSize = true;
            this.Rspeedlab.Location = new System.Drawing.Point(575, 235);
            this.Rspeedlab.Name = "Rspeedlab";
            this.Rspeedlab.Size = new System.Drawing.Size(11, 12);
            this.Rspeedlab.TabIndex = 31;
            this.Rspeedlab.Text = "0";
            // 
            // Standcheck
            // 
            this.Standcheck.AutoSize = true;
            this.Standcheck.Location = new System.Drawing.Point(12, 264);
            this.Standcheck.Name = "Standcheck";
            this.Standcheck.Size = new System.Drawing.Size(60, 16);
            this.Standcheck.TabIndex = 32;
            this.Standcheck.Text = "站起来";
            this.Standcheck.UseVisualStyleBackColor = true;
            this.Standcheck.CheckedChanged += new System.EventHandler(this.Standcheck_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 292);
            this.Controls.Add(this.Standcheck);
            this.Controls.Add(this.Rspeedlab);
            this.Controls.Add(this.Rspeedtra);
            this.Controls.Add(this.Lspeedlab);
            this.Controls.Add(this.Lseedtra);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EItra);
            this.Controls.Add(this.EPtra);
            this.Controls.Add(this.KDtra);
            this.Controls.Add(this.EIlab);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.KDlab);
            this.Controls.Add(this.EPlab);
            this.Controls.Add(this.KPlab);
            this.Controls.Add(this.gyroLab);
            this.Controls.Add(this.angleLab);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KPtra);
            this.Controls.Add(this.getdata);
            this.Controls.Add(this.scannbut);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KPtra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KDtra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPtra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EItra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lseedtra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rspeedtra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button scannbut;
        private System.Windows.Forms.Button getdata;
        private System.Windows.Forms.TrackBar KPtra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label angleLab;
        private System.Windows.Forms.Label gyroLab;
        private System.Windows.Forms.Label KPlab;
        private System.Windows.Forms.Label EPlab;
        private System.Windows.Forms.Label KDlab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label EIlab;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar KDtra;
        private System.Windows.Forms.TrackBar EPtra;
        private System.Windows.Forms.TrackBar EItra;
        private System.Windows.Forms.TrackBar Lseedtra;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Lspeedlab;
        private System.Windows.Forms.TrackBar Rspeedtra;
        private System.Windows.Forms.Label Rspeedlab;
        private System.Windows.Forms.CheckBox Standcheck;
    }
}

