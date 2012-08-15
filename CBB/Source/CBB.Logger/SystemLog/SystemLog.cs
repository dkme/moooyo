using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.Logger.SystemLog
{
    /// <summary>
    /// LogHelper的摘要说明。
    /// </summary>
    public class LogHelper
    {
        private LogHelper()
        {
        }

        public static readonly log4net.ILog logInfo = log4net.LogManager.GetLogger("logInformation");

        public static readonly log4net.ILog logError = log4net.LogManager.GetLogger("logError");

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        //public static void SetConfig(FileInfo configFile)
        //{
        //    log4net.Config.XmlConfigurator.Configure(configFile); 
        //}

        public static void WriteLog(string info)
        {
            if (logInfo.IsInfoEnabled)
            {
                logInfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception ex)
        {
            if (logError.IsErrorEnabled)
            {
                logError.Error(info, ex);
            }
        }
    }
}
