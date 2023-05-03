using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace openGPS_Common
{
    public class IpHelper
    {
        public static long ConvertToNumber(string ipv4)
        {
            long ip = 0;
            string[] ipStrArray = ipv4.Split('.');
            long[] longArray = new long[]{
                    long.Parse(ipStrArray[0]),
                    long.Parse(ipStrArray[1]),
                    long.Parse(ipStrArray[2]),
                    long.Parse(ipStrArray[3])
                };
            ip = longArray[0] * Convert.ToInt64(Math.Pow(256, 3).ToString())
                + longArray[1] * Convert.ToInt64(Math.Pow(256, 2).ToString())
                + longArray[2] * Convert.ToInt64(Math.Pow(256, 1).ToString())
                + longArray[3] * Convert.ToInt64(Math.Pow(256, 0).ToString());
            return ip;
        }

        public static string ConvertToString(long ipAddress)
        {
            long ui1 = ipAddress & 0xFF000000;
            ui1 = ui1 >> 24;
            long ui2 = ipAddress & 0x00FF0000;
            ui2 = ui2 >> 16;
            long ui3 = ipAddress & 0x0000FF00;
            ui3 = ui3 >> 8;
            long ui4 = ipAddress & 0x000000FF;
            string IPstr = "";
            IPstr = System.Convert.ToString(ui1) + "."
            + System.Convert.ToString(ui2) + "."
            + System.Convert.ToString(ui3)
            + "." + System.Convert.ToString(ui4);
            return IPstr;
        }
        public static bool Ping(string ip, int timeoutMs = 500)
        {
            try
            {
                IPAddress ipa;
                if (!IPAddress.TryParse(ip, out ipa))
                    return false;

                Ping p = new Ping();
                PingReply reply = p.Send(ip, timeoutMs);
                if (reply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public static bool CheckPort(string ip, int port, out string errorMsg, int timeoutMs = 1000)
        {
            errorMsg = "";
            bool success = false;
            try
            {
                Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
                s.ReceiveBufferSize = s.SendBufferSize = 1;
                s.NoDelay = true;
                IPAddress ipa = IPAddress.Parse(ip);
                IAsyncResult connResult = s.BeginConnect(ipa, port, null, null);
                connResult.AsyncWaitHandle.WaitOne(timeoutMs, true);  //等待2秒

                if (!connResult.IsCompleted)
                {
                    success = false;
                    errorMsg = "请求超时";
                }
                else
                {
                    success = s.Connected;
                    if (!success)
                    {
                        errorMsg = "拒绝连接";
                    }
                    else
                    {
                        errorMsg = "成功";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = "系统异常：" + ex.Message;
                success = false;
            }
            return success;
        }
    }
}
