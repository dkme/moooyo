using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.Logger
{
    /// <summary>
    /// 日志对象
    /// </summary>
    public class Log
    {
        //日志级别
        public LogType LogType;
        //时间
        public DateTime CreatedTime;
        //信息
        public String LogStr;
        //来源
        public String Source;

        public Log(LogType LogType, String LogStr, String Source)
        {
            this.LogType = LogType;
            this.LogStr = LogStr;
            this.Source = Source;
            this.CreatedTime = DateTime.Now;
        }
        //日志文本输出
        public override String ToString()
        {
            return this.LogType.ToString() + "：" + this.LogStr + "（" + this.CreatedTime + "）";
        }
    }
    public enum LogType
    {
        //调试信息
        Debug = 1,
        //运行信息
        Info=2,
        //警告
        Warning=3,
        //严重警告
        Critical=4

    }
}
