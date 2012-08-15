using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CBB.OAuth.RenRen.APIUtility
{
    public class APIValidation
    {
        /// <summary>
        /// 获取Access Token,
        /// 通过第一步返回的URL获得参数Code的值，就为Authorization Code
        /// </summary>
        /// <returns>返回获得的Access Token</returns>
        public string GetAccessToken()
        {
            string accessToken = "";
            try
            {
                if (System.Web.HttpContext.Current.Session["accessToken"] == null)
                {
                    string authorizationCode = System.Web.HttpContext.Current.Request["code"] ?? "";
                    if (authorizationCode != "")
                    {
                        List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("grant_type","authorization_code"),
                            new APIParameter("code",authorizationCode),
                            new APIParameter("client_id",APIConfig.ApiKey),
                            new APIParameter("client_secret",APIConfig.SecretKey),
                            new APIParameter("redirect_uri",APIConfig.CallBackURL)
                        };
                        string requestUrl = HttpUtil.AddParametersToURL(APIConfig.AccessURL, paras);
                        string content = new SyncHttp().HttpPost(requestUrl, "");
                        JObject jo = JObject.Parse(content);
                        accessToken = jo["access_token"].ToString();
                        System.Web.HttpContext.Current.Session["accessToken"] = accessToken;
                    }
                }
                else
                {
                    accessToken = System.Web.HttpContext.Current.Session["accessToken"] as string;
                }
            }
            catch
            {
                accessToken = "";
            }
            // 由于获得Json字符串通过JSON.NET获取之后，还是以字符串形式存在，形如“xxxxx”，包括双引号
            // 所以必须替换掉双引号
            accessToken = accessToken.Replace("\"", "");
            return accessToken;
        }

        /// <summary>
        /// 获得Session key
        /// </summary>
        /// <returns>返回Session key</returns>
        public string GetSessionKey()
        {
            string sessionKey = "";
            // 首先从session中读取，如果不存在再重新获取。
            if (System.Web.HttpContext.Current.Session["accessToken"] == null)
            {
                string accessToken = GetAccessToken();
                if (accessToken == "")
                    return "";
                try
                {
                    accessToken = accessToken.Replace("\"", "");
                    List<APIParameter> paras = new List<APIParameter>() { 
                    new APIParameter("oauth_token",accessToken)
                };
                    string requestUrl = HttpUtil.AddParametersToURL(APIConfig.SessionURL, paras);
                    string content = new SyncHttp().HttpPost(requestUrl, "");
                    JObject jo = JObject.Parse(content);
                    sessionKey = jo["renren_token"]["session_key"].ToString();
                    System.Web.HttpContext.Current.Session["sessionKey"] = sessionKey;
                }
                catch
                {
                    sessionKey = "";
                }
            }
            else
            {
                sessionKey = System.Web.HttpContext.Current.Session["sessionKey"] as string;
            }
            // 由于获得Json字符串通过JSON.NET获取之后，还是以字符串形式存在，形如“xxxxx”，包括双引号
            // 所以必须替换掉双引号
            sessionKey = sessionKey.Replace("\"", "");
            return sessionKey;
        }

        /// <summary>
        /// 计算签名
        /// 此方法传入的是所有签名需要的参数
        /// </summary>
        /// <param name="paras">传入需要的参数</param>
        /// <returns></returns>
        public string CalSig(List<APIParameter> paras)
        {
            paras.Sort(new ParameterComparer());
            StringBuilder sbList = new StringBuilder();
            foreach (APIParameter para in paras)
            {
                sbList.AppendFormat("{0}={1}", para.Name, para.Value);
            }
            sbList.Append(APIConfig.SecretKey);
            return HttpUtil.MD5Encrpt(sbList.ToString());
        }
    }
}
