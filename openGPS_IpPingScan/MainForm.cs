using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openGPS_IpPingScan
{
    public partial class MainForm : Form
    {
        //Parallel并行计算
        ParallelOptions parallelOptions;
        CancellationTokenSource cts;

        //进度监控线程
        Task tMoniter = null;
        CancellationTokenSource tMoniter_cts = null;

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ipLocal = ip.ToString();
                    Console.WriteLine("IP Address = " + ipLocal);
                    this.txtIpFrom.Text = ipLocal.Substring(0, ipLocal.LastIndexOf(".")) + ".0";
                    this.txtIpTo.Text = ipLocal.Substring(0, ipLocal.LastIndexOf(".")) + ".255";
                }
            }
        }

        private void btnScanIp_Click(object sender, EventArgs e)
        {
            string ipFromStr = this.txtIpFrom.Text.Trim();
            string ipToStr = this.txtIpTo.Text.Trim();
            string timeoutMsStr = this.txtTimeoutMs.Text.Trim();
            IPAddress ipa;
            if (!IPAddress.TryParse(ipFromStr, out ipa))
            {
                ShowLog("起始IP格式错误！");
                return;
            }
            if (!IPAddress.TryParse(ipToStr, out ipa))
            {
                ShowLog("终止IP格式错误！");
                return;
            }
            int timeoutMs = 0;
            if (!int.TryParse(timeoutMsStr, out timeoutMs))
            {
                ShowLog("超时时间设置错误！");
                return;
            }
            this.btnStop.Enabled = true;
            this.dgvResult.Rows.Clear();

            long ipFrom = openGPS_Common.IpHelper.ConvertToNumber(ipFromStr);
            long ipTo = openGPS_Common.IpHelper.ConvertToNumber(this.txtIpTo.Text);

            for (long i = ipFrom; i < ipTo + 1; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>() {
                    { "IP", openGPS_Common.IpHelper.ConvertToString(i) },
                };
                ShowMsgInTable(this.dgvResult, i, "...", dic, Color.White);
            }
            this.dgvResult.Columns["IP"].DisplayIndex = 0;


            #region 并行计算
            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);

            int threadMax = completionPortThreads;
            if (ipTo - ipFrom < 1000)
                threadMax = Convert.ToInt32(ipTo - ipFrom);
            ThreadPool.SetMinThreads(threadMax, threadMax);

            DateTime dtStart = DateTime.Now;

            ParallelLoopResult result = new ParallelLoopResult();

            Task.Run(() =>
            {
                try
                {
                    ShowLog("启动扫描");
                    //计算核心
                    cts = new CancellationTokenSource();
                    parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = threadMax, CancellationToken = cts.Token, };
                    result = Parallel.For(ipFrom, ipTo + 1, parallelOptions, (x, pls) =>
                      {
                          bool success = false;
                          try
                          {
                              string ip = openGPS_Common.IpHelper.ConvertToString(x);
                              Ping p = new Ping();
                              PingReply reply = p.Send(ip, timeoutMs);
                              success = reply.Status == IPStatus.Success;
                              Dictionary<string, string> dic = new Dictionary<string, string>() {
                                { "IP",ip },
                              };
                              ShowMsgInTable(this.dgvResult, x, success ? reply.RoundtripTime.ToString() : "失败", dic, success ? Color.Green : Color.Red);
                          }
                          catch (Exception ex)
                          {
                              ShowMsgInTable(this.dgvResult, x, ex.Message, null, Color.Red);
                          }
                      });
                }
                catch (Exception ex)
                {
                    ShowLog(ex.Message);
                }
                finally
                {
                    TimeSpan ts = DateTime.Now - dtStart;
                    string tips = string.Format("起始值：{0}，终止值{1}，耗时：{2}", ipFrom, ipTo, ts.ToString());
                    ShowLog(tips);
                    this.btnStop.Invoke(new Action(() =>
                    {
                        this.btnStop.Enabled = false;
                    }));
                }
            });

            //进度监控
            tMoniter_cts = new CancellationTokenSource();
            tMoniter = new Task(() =>
            {
                DateTime dtEnd = DateTime.Now;
                TimeSpan ts = dtEnd - dtStart;
                try
                {
                    //Thread.Sleep(1 * 1000);//给result启动预留时间
                    while (!result.IsCompleted)
                    {
                        if (tMoniter_cts.Token.IsCancellationRequested)
                        {
                            ShowLog(string.Format("{0}，耗时：{1}", "检测取消", ts.ToString()));
                            return;
                        }

                        dtEnd = DateTime.Now;
                        ts = dtEnd - dtStart;
                        ShowLog(string.Format("{0}，耗时：{1}", "运行中", ts.ToString()));
                        Thread.Sleep(1 * 1000);
                    }
                    //完成
                    ShowLog(string.Format("{0}，耗时：{1}", "运行完成", ts.ToString()));
                }
                catch (Exception ex)
                {
                    ShowLog(ex.Message);
                }
            }, tMoniter_cts.Token);
            tMoniter.Start();
            #endregion

        }

        #region log
        private delegate void ShowLogCallback(string msg, bool isRedBack = false);
        private ShowLogCallback showLogMethodCallback;
        private void ShowLog(string msg, bool isRedBack = false)
        {
            if (this.InvokeRequired)
            {
                showLogMethodCallback = new ShowLogCallback(ShowLog);
                this.txtMsg.Invoke(showLogMethodCallback, new object[] { msg, isRedBack });
            }
            else
            {
                this.txtMsg.BackColor = Color.White;
                if (isRedBack)
                    this.txtMsg.BackColor = Color.Red;
                this.txtMsg.Text = msg;
            }
        }
        #endregion

        #region MyRegion
        delegate void ShowMsgInTableDelegate(DataGridView dgv, long id, string msg, Dictionary<string, string> dicData, Color backColor);
        /// <summary>
        /// 设置表格内容
        /// </summary>
        /// <param name="isRunning"></param>
        public void ShowMsgInTable(DataGridView dgv, long id, string msg, Dictionary<string, string> dicData, Color backColor)
        {
            if (dgv.InvokeRequired)
            {
                ShowMsgInTableDelegate stmd = new ShowMsgInTableDelegate(ShowMsgInTable);
                dgv.Invoke(stmd, new object[] { dgv, id, msg, dicData, backColor });
            }
            else
            {
                //强制指定ID列名
                string colId = "ID";
                string colMsg = "Msg";

                if (id == 0)
                {
                    //Common.Log.WriteLine(msg, ConsoleColor.Yellow);
                    //this.lblMsg.Text = msg;
                    return;
                }
                lock (dgv)
                {
                    //检查列
                    if (dgv.Columns[colId] == null)
                    {
                        dgv.Columns.Add(colId, colId);
                        dgv.Columns[colId].Visible = false;
                        dgv.Columns.Add(colMsg, "消息");
                    }
                    foreach (var item in dicData)
                    {
                        if (dgv.Columns[item.Key] == null)
                        {
                            dgv.Columns.Add(item.Key, item.Key);
                        }
                    }
                    //赋值
                    bool isExistRow = false;
                    foreach (DataGridViewRow dr in dgv.Rows)
                    {
                        if (Convert.ToInt64(dr.Cells[colId].Value) == id)
                        {
                            isExistRow = true;
                            //修改行
                            dr.Cells[colMsg].Value = msg;
                            foreach (var row in dicData)
                            {
                                if (dr.Cells[row.Key] != null)
                                    dr.Cells[row.Key].Value = row.Value;
                            }
                        }
                    }
                    if (!isExistRow)
                    {
                        //新建行
                        int rowIndex = dgv.Rows.Add();
                        dgv.Rows[rowIndex].Cells[colId].Value = id;
                        dgv.Rows[rowIndex].Cells[colMsg].Value = msg;
                        foreach (var item in dicData)
                        {
                            dgv.Rows[rowIndex].Cells[item.Key].Value = item.Value;
                        }
                    }

                    //处理背景颜色
                    foreach (DataGridViewRow dr in dgv.Rows)
                    {
                        string tempId = Convert.ToString(dr.Cells[colId].Value);
                        if (tempId == id.ToString())
                        {
                            //行背景色
                            dr.DefaultCellStyle.BackColor = backColor;
                            //单元格背景色
                            //dr.Cells["ID"].Style.BackColor = Color.Green;
                            break;
                        }
                    }
                }
            }

        }

        #endregion

        private void btnOutLog_Click(object sender, EventArgs e)
        {
            lock (this.dgvResult)
            {
                foreach (DataGridViewRow row in this.dgvResult.Rows)
                {
                    string log = "";
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        log += row.Cells[i].Value.ToString() + ",";
                    }
                    openGPS_Common.Filelog.WriteLogFile(log.TrimEnd(','));
                }
            }
            ShowLog("导出完成");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();

                if (tMoniter_cts != null)
                {
                    tMoniter_cts.Cancel();
                }
                ShowLog("停止运行");
            }
            else
            {
                ShowLog("未运行");
            }
        }
    }
}
