using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;

namespace CBB.ConfigurationHelper
{
    public class AppSettingHelper
    {
        //配置缓存
        private static Hashtable ht;
        /// <summary>
        /// 获得config文件中键对应的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetConfig(string key)
        {
            if (ht == null) ht = new Hashtable();
            if (ht[key] != null) return ht[key].ToString();

            //return ConfigurationSettings.AppSettings[key];
            if (ConfigurationManager.AppSettings[key] != null)
            {
                String value = ConfigurationManager.AppSettings[key].ToString().Trim();
                ht.Add(key,value);
                return value;
            }

            return "";
        }
    }
}
