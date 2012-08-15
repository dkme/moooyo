using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CBB.Logger
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogToFile
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">日志内容</param>
        /// <param name="LogFilePath">日志文件路径</param>
        public static void LogMessage(string Message, string LogFilePath)
        {
            _日志("", "", Message, LogFilePath);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="err">错误日志内容</param>
        /// <param name="Method">调用方法名</param>
        /// <param name="Message">日志内容</param>
        /// <param name="LogFilePath">日志文件路径</param>
        public static void _日志(String err, String Method, String Message, string LogFilePath)
        {
            try
            {
                String filepath = LogFilePath;
                FileStream fs = (!File.Exists(filepath)) ? new FileStream(filepath, FileMode.Create) : File.Open(filepath, FileMode.Append);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(" -DateTime:" + DateTime.Now.ToString() + "-Err:" + err + " -Method:" + Method + "  -Message:" + Message + " \r\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch
            {
            }
        }
    }
}
