using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
            ip =  longArray[0] * Convert.ToInt64(Math.Pow(256, 3).ToString())
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
    }
}
