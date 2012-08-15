using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace CBB.OAuth.RenRen.APIUtility
{
    public class HttpUtil
    {
        //根据文件名获取文件类型
        public static string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(ext);

            if (registryKey != null && registryKey.GetValue("Content Type") != null)
            {
                contentType = registryKey.GetValue("Content Type").ToString();
            }

            return contentType;
        }

        //根据query String获取parameter数据
        public static List<APIParameter> GetQueryParameters(string queryString)
        {
            if (queryString.StartsWith("?"))
            {
                queryString = queryString.Remove(0, 1);
            }

            List<APIParameter> result = new List<APIParameter>();

            if (!string.IsNullOrEmpty(queryString))
            {
                string[] p = queryString.Split('&');
                foreach (string s in p)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(new APIParameter(temp[0], temp[1]));
                        }
                    }
                }
            }

            return result;
        }

        // 从Parameters中获取数据
        public static string GetQueryFromParas(List<APIParameter> paras)
        {
            if (paras == null || paras.Count == 0)
                return "";
            StringBuilder sbList = new StringBuilder();
            int count = 1;
            foreach (APIParameter para in paras)
            {
                sbList.AppendFormat("{0}={1}",para.Name,para.Value);
                if (count < paras.Count)
                    sbList.Append("&");
                count++;
            }
            return sbList.ToString(); ;
        }

        // 把APIParameter中加入URL中
        public static string AddParametersToURL(string url, List<APIParameter> paras)
        {
            string querystring = GetQueryFromParas(paras);
            if (querystring != "")
            {
                url += "?"+querystring;
            }
            return url;
        }

        // MD5 加密
        public static string MD5Encrpt(string plainText)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            StringBuilder sbList = new StringBuilder();
            foreach (byte d in data)
            {
                sbList.Append(d.ToString("x2"));
            }
            return sbList.ToString();
        }


    }
}
