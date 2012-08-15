using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CBB.NetworkingHelper
{
    public class PingEx
    {
        public static class Ping
        {
            #region codes
            private const int TIME_OUT = 100;
            private const int PACKET_SIZE = 512;
            private const int TRY_TIMES = 1;
            private static Regex _reg = new Regex(@"Minimum\s*=\s*\d+ms,\s*Maximum\s*=\s*\d+ms,\s*Average\s*=\s*(\d+)ms", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            private static float LaunchPing(string strCommandline, int packetSize)
            {
                Process proc = new Process();
                proc.StartInfo.Arguments = strCommandline;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "ping.exe";
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                string strBuffer = proc.StandardOutput.ReadToEnd();
                proc.Close();
                return ParseResult(strBuffer, packetSize);
            }

            private static float ParseResult(string strBuffer, int packetSize)
            {
                if (strBuffer.Length < 1) return 0.0F;

                MatchCollection mc = _reg.Matches(strBuffer);
                if (mc == null || mc.Count < 1 || mc[0].Groups == null) return 0.0F;
                int avg;
                if (!int.TryParse(mc[0].Groups[1].Value, out avg)) return 0.0F;
                if (avg <= 0) return 0.1F;

                return (float)avg;
            }
            #endregion codes

            /// <summary>
            /// 获得ping值
            /// </summary>
            /// <param name="strHost">主机名或ip</param>
            /// <returns>kbps/s</returns>
            public static float UrlAdd(string strHost)
            {
                return LaunchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TRY_TIMES, PACKET_SIZE, TIME_OUT), PACKET_SIZE);
            }

            /// <summary>
            /// 获得ping值
            /// </summary>
            /// <param name="strHost">主机名或ip</param>
            /// <param name="PacketSize">发送测试包大小</param>
            /// <param name="TimeOut">超时</param>
            /// <param name="TryTimes">测试次数</param>
            /// <returns>kbps/s</returns>
            public static float UrlAdd(string strHost, int PacketSize, int TimeOut, int TryTimes)
            {

                float avg = 0;
                for (int i = 0; i < TryTimes; i++)
                {
                    avg += LaunchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TryTimes, PacketSize, TimeOut), PacketSize);
                }
                avg = avg / TryTimes;
                return PacketSize / (avg * 1000) / 1024;
            }
        }
    }
}
