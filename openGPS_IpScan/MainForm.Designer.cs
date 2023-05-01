
namespace openGPS_IpScan
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnScanIp = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtIpTo = new System.Windows.Forms.TextBox();
            this.txtIpFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeoutMs = new System.Windows.Forms.TextBox();
            this.btnOutLog = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScanIp
            // 
            this.btnScanIp.Location = new System.Drawing.Point(172, 6);
            this.btnScanIp.Name = "btnScanIp";
            this.btnScanIp.Size = new System.Drawing.Size(54, 21);
            this.btnScanIp.TabIndex = 4;
            this.btnScanIp.Text = "开始";
            this.btnScanIp.UseVisualStyleBackColor = true;
            this.btnScanIp.Click += new System.EventHandler(this.btnScanIp_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMsg.Location = new System.Drawing.Point(0, 265);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(237, 21);
            this.txtMsg.TabIndex = 8;
            this.txtMsg.Text = "提示消息";
            // 
            // txtIpTo
            // 
            this.txtIpTo.Location = new System.Drawing.Point(71, 33);
            this.txtIpTo.MaxLength = 15;
            this.txtIpTo.Name = "txtIpTo";
            this.txtIpTo.Size = new System.Drawing.Size(95, 21);
            this.txtIpTo.TabIndex = 2;
            this.txtIpTo.Text = "127.0.0.100";
            // 
            // txtIpFrom
            // 
            this.txtIpFrom.Location = new System.Drawing.Point(71, 6);
            this.txtIpFrom.MaxLength = 15;
            this.txtIpFrom.Name = "txtIpFrom";
            this.txtIpFrom.Size = new System.Drawing.Size(95, 21);
            this.txtIpFrom.TabIndex = 1;
            this.txtIpFrom.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "终止IP：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "起始IP：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 87);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(213, 140);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(213, 172);
            this.dataGridView1.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "超时ms：";
            // 
            // txtTimeoutMs
            // 
            this.txtTimeoutMs.Location = new System.Drawing.Point(71, 60);
            this.txtTimeoutMs.MaxLength = 10;
            this.txtTimeoutMs.Name = "txtTimeoutMs";
            this.txtTimeoutMs.Size = new System.Drawing.Size(95, 21);
            this.txtTimeoutMs.TabIndex = 3;
            this.txtTimeoutMs.Text = "1000";
            // 
            // btnOutLog
            // 
            this.btnOutLog.Location = new System.Drawing.Point(172, 59);
            this.btnOutLog.Name = "btnOutLog";
            this.btnOutLog.Size = new System.Drawing.Size(54, 21);
            this.btnOutLog.TabIndex = 6;
            this.btnOutLog.Text = "导出";
            this.btnOutLog.UseVisualStyleBackColor = true;
            this.btnOutLog.Click += new System.EventHandler(this.btnOutLog_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(172, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(54, 21);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 286);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOutLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnScanIp);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.txtTimeoutMs);
            this.Controls.Add(this.txtIpTo);
            this.Controls.Add(this.txtIpFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(253, 266);
            this.Name = "MainForm";
            this.Text = "IP扫描工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScanIp;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtIpTo;
        private System.Windows.Forms.TextBox txtIpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeoutMs;
        private System.Windows.Forms.Button btnOutLog;
        private System.Windows.Forms.Button btnStop;
    }
}

