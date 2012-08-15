using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;

namespace Moooyo.BiZ.Member.Connector
{
    public class DouBanConnector : Connector
    {
        private PlatformDef platform = PlatformProvider.GetPlatformDef(Platform.Douban);
        private CBB.OAuth.DouBan.OAuthBase oAuth = new CBB.OAuth.DouBan.OAuthBase();
        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns></returns>
        public override ConnectStatus GetConnectStatus()
        {
            if (this.OAuth_Token != null && this.OAuth_Token != "" && this.ConnectID != null && this.ConnectID != "" && this.ExpireDate < DateTime.Now)
                return ConnectStatus.Available;
            if (this.ExpireDate > DateTime.Now) return ConnectStatus.Expired;
            if (this.OAuth_Token != null || this.OAuth_Token != "" || this.ConnectID != null || this.ConnectID != "") return ConnectStatus.Unauthorized;

            return ConnectStatus.Unknow;
        }
        public override string Connect(String addparams)
        {
            Uri uri = new Uri(platform.RequestTokenUri);
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, platform.ApiKey, platform.ApiKeySecret, string.Empty, string.Empty, "GET", timeStamp, nonce, CBB.OAuth.DouBan.OAuthBase.SignatureTypes.HMACSHA1, out normalizeUrl, out normalizedRequestParameters));
            //构造请求Request Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", platform.ApiKey);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}", sig);
            //请求Request Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            //解析返回的Request Token和Request Token Secret
            Dictionary<string, string> responseValues = parseResponse(responseBody);
            HttpContext.Current.Session["requestToken"] = responseValues["oauth_token"];
            HttpContext.Current.Session["requestTokenSecret"] = responseValues["oauth_token_secret"];
            //生成引导用户授权的url
            string url = platform.AUTHORIZE + "?oauth_token=" + responseValues["oauth_token"] + "&oauth_callback=" + platform.CallbackUrl + addparams;
            return url;
        }
        public override CBB.ExceptionHelper.OperationResult SetDisable()
        {
            return ConnectorProvider.SaveConnectorEnableStatus(ConnectID, PlatformType, false);
        }
        public override CBB.ExceptionHelper.OperationResult SetEnable()
        {
            return ConnectorProvider.SaveConnectorEnableStatus(ConnectID, PlatformType, true);
        }
        public override string SendInfo(string url, string info)
        {
            string response = "";
            sendMiniBlog(this.OAuth_Token, this.OAuth_Token_Secret, info + " " + url);
            return response;
        }
        public override string SendInfoWithPic(string url, string info, string pic)
        {
            string response = "";
            sendMiniBlog(this.OAuth_Token, this.OAuth_Token_Secret, info + " " + url);
            return response;
        }
        public Boolean Connect()
        {
            try
            {
                Dictionary<string, string> responseValues = getOauthCode();
                string accessToken = responseValues["oauth_token"];
                string accessTokenSecret = responseValues["oauth_token_secret"];
                Uri uri = new Uri("http://api.douban.com/people/%40me");
                string nonce = oAuth.GenerateNonce();
                string timeStamp = oAuth.GenerateTimeStamp();
                string normalizeUrl, normalizedRequestParameters;
                // 签名
                string sig = HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, platform.ApiKey, platform.ApiKeySecret, accessToken, accessTokenSecret, "POST", timeStamp, nonce, CBB.OAuth.DouBan.OAuthBase.SignatureTypes.HMACSHA1, out normalizeUrl, out normalizedRequestParameters));
                //构造OAuth头部
                StringBuilder oauthHeader = new StringBuilder();
                oauthHeader.AppendFormat("OAuth realm=\"\", oauth_consumer_key={0}, ", platform.ApiKey);
                oauthHeader.AppendFormat("oauth_nonce={0}, ", nonce);
                oauthHeader.AppendFormat("oauth_timestamp={0}, ", timeStamp);
                oauthHeader.AppendFormat("oauth_signature_method={0}, ", "HMAC-SHA1");
                oauthHeader.AppendFormat("oauth_version={0}, ", "1.0");
                oauthHeader.AppendFormat("oauth_signature={0}, ", sig);
                oauthHeader.AppendFormat("oauth_token={0}", accessToken);
                // Http Request的设置
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Set("Authorization", oauthHeader.ToString());
                request.ContentType = "application/atom+xml";
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                string responseBody = stream.ReadToEnd();
                stream.Close();
                response.Close();
                System.Xml.XmlDocument xmld = new System.Xml.XmlDocument();
                xmld.LoadXml(responseBody);
                System.Xml.XmlNodeList xxList = xmld.GetElementsByTagName("entry");
                this.ConnectID = xxList[0]["db:uid"].InnerText;
                this.Name = xxList[0]["title"].InnerText;
                this.Province = xxList[0]["db:location"].InnerText;
                this.City = xxList[0]["db:location"].InnerText;
                this.Sex = "-1";
                this.SexStr = "未知";
                this.OAuth_Token = accessToken;
                this.OAuth_Token_Secret = accessTokenSecret;
                this.PlatformType = Platform.Douban;
                this.Enable = true;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private Dictionary<string, string> parseResponse(string parameters)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(parameters))
            {
                string[] p = parameters.Split('&');
                foreach (string s in p)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s.IndexOf('=') > -1) { string[] temp = s.Split('='); result.Add(temp[0], temp[1]); }
                        else { result.Add(s, string.Empty); }
                    }
                }
            } 
            return result;
        }
        //解析返回的Request Token和Request Token Secret
        public Dictionary<string, string> getOauthCode()
        {
            Uri uri = new Uri(platform.ACCESS_TOKEN);
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            string requestToken = HttpContext.Current.Session["requestToken"] != null ? HttpContext.Current.Session["requestToken"].ToString() : "";
            string requestTokenSecret = HttpContext.Current.Session["requestTokenSecret"] != null ? HttpContext.Current.Session["requestTokenSecret"].ToString() : "";
            // 签名
            string sig = HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, platform.ApiKey, platform.ApiKeySecret, requestToken, requestTokenSecret, "GET", timeStamp, nonce, CBB.OAuth.DouBan.OAuthBase.SignatureTypes.HMACSHA1, out normalizeUrl, out normalizedRequestParameters));
            //构造请求Access Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", platform.ApiKey);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_token={0}&", requestToken);
            //请求Access Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            //解析返回的Request Token和Request Token Secret
            Dictionary<string, string> responseValues = parseResponse(responseBody);
            return responseValues;
        }
        //发布广播
        public Boolean sendMiniBlog(string accessToken, string accessTokenSecret, string content)
        {
            Uri uri = new Uri(platform.APIUrl);
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, platform.ApiKey, platform.ApiKeySecret, accessToken, accessTokenSecret, "POST", timeStamp, nonce, CBB.OAuth.DouBan.OAuthBase.SignatureTypes.HMACSHA1, out normalizeUrl, out normalizedRequestParameters));
            //构造OAuth头部
            StringBuilder oauthHeader = new StringBuilder();
            oauthHeader.AppendFormat("OAuth realm=\"\", oauth_consumer_key={0}, ", platform.ApiKey);
            oauthHeader.AppendFormat("oauth_nonce={0}, ", nonce);
            oauthHeader.AppendFormat("oauth_timestamp={0}, ", timeStamp);
            oauthHeader.AppendFormat("oauth_signature_method={0}, ", "HMAC-SHA1");
            oauthHeader.AppendFormat("oauth_version={0}, ", "1.0");
            oauthHeader.AppendFormat("oauth_signature={0}, ", sig);
            oauthHeader.AppendFormat("oauth_token={0}", accessToken);
            //构造请求
            StringBuilder requestBody = new StringBuilder("<?xml version='1.0' encoding='UTF-8'?>");
            requestBody.Append("<entry xmlns:ns0=\"http://www.w3.org/2005/Atom\" xmlns:db=\"http://www.douban.com/xmlns/\">");
            requestBody.Append("<content>" + content + "</content>");
            requestBody.Append("</entry>");
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(requestBody.ToString());
            // Http Request的设置
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Set("Authorization", oauthHeader.ToString());
            request.ContentType = "application/atom+xml";
            request.Method = "POST";
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                string responseBody = stream.ReadToEnd();
                stream.Close();
                response.Close();
                return true;
            }
            catch (WebException e)
            {
                StreamReader stream = new StreamReader(e.Response.GetResponseStream(), System.Text.Encoding.UTF8);
                string responseBody = stream.ReadToEnd();
                stream.Close();
                return false;
            }
        }
    }
}
