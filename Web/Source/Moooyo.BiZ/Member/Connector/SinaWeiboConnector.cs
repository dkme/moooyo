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
    /// <summary>
    /// 新浪微博连接类
    /// </summary>
    public class SinaWeiboConnector : Connector
    {
        private PlatformDef platform = PlatformProvider.GetPlatformDef(Platform.SinaWeibo);
        private CBB.OAuth.Sina.OAuthBase oAuth = new CBB.OAuth.Sina.OAuthBase();

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
            return getRequestToken(addparams);
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
            string response = sendcontent(info + "," + url);
            return response;
        }
        public override string SendInfoWithPic(string url, string info, string pic)
        {
            string response = sendcontent(info + "," + url, pic);
            return response;
        }
        public bool Connect(string oauth_verifier, string oauth_token)
        {
            this.PlatformType = BiZ.Member.Connector.Platform.SinaWeibo;
            string responsebody = "";
            try
            {
                responsebody = getAccessToken(oauth_token, oauth_verifier);
            }
            catch (Exception err)
            {
                return false;
            }
            this.Enable = true;

            XmlDocument xx = new XmlDocument();
            xx.LoadXml(responsebody);//加载xml
            XmlNodeList xxList = xx.GetElementsByTagName("user"); //取得节点名为row的XmlNode集合
            this.ConnectID = xxList[0]["id"].InnerText;
            this.Name = xxList[0]["name"].InnerText;
            string[] city = xxList[0]["location"].InnerText.Split(' ');
            if (city.Length > 1)
            {
                this.Province = city[0];
                this.City = city[1];
            }
            else
            {
                this.Province = "北京";
                this.City = "北京";
            }
            string sexstr = xxList[0]["gender"].InnerText;
            if (sexstr == "m")
            {
                this.Sex = "1";
                this.SexStr = "男";
            }
            else
            {
                this.Sex = "2";
                this.SexStr = "女";
            }
            return true;
        }
        private String getRequestToken(String addparams)
        {
            Uri uri = new Uri(platform.RequestTokenUri);
            string nonce = oAuth.GenerateNonce();//获取随机生成的字符串，防止攻击
            string timeStamp = oAuth.GenerateTimeStamp();//发起请求的时间戳
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(uri, platform.ApiKey, platform.ApiKeySecret, string.Empty, string.Empty, "GET", timeStamp, nonce, string.Empty, out normalizeUrl, out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);
            //构造请求Request Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", platform.ApiKey);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_version={0}", "1.0");
            //请求Request Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            int intOTS = responseBody.IndexOf("oauth_token=");
            int intOTSS = responseBody.IndexOf("&oauth_token_secret=");

            OAuth_Token = responseBody.Substring(intOTS + 12, intOTSS - (intOTS + 12));
            OAuth_Token_Secret = responseBody.Substring((intOTSS + 20), responseBody.Length - (intOTSS + 20));
            return platform.AUTHORIZE + "?oauth_token=" + OAuth_Token + "&oauth_callback=" + platform.CallbackUrl + addparams;
        }
        private string getAccessToken(string requestToken, string oauth_verifier)
        {
            Uri uri = new Uri(platform.ACCESS_TOKEN);
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(
            uri,
            platform.ApiKey,
            platform.ApiKeySecret,
            requestToken,
            OAuth_Token_Secret,
            "Get",
            timeStamp,
            nonce,
            oauth_verifier,
            out normalizeUrl,
            out normalizedRequestParameters);
            sig = oAuth.UrlEncode(sig);
            //构造请求Access Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", platform.ApiKey);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_token={0}&", requestToken);
            sb.AppendFormat("oauth_verifier={0}", oauth_verifier);
            //请求Access Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            int intOTS = responseBody.IndexOf("oauth_token=");
            int intOTSS = responseBody.IndexOf("&oauth_token_secret=");
            int intUser = responseBody.IndexOf("&user_id=");
            OAuth_Token = responseBody.Substring(intOTS + 12, intOTSS - (intOTS + 12));
            OAuth_Token_Secret = responseBody.Substring((intOTSS + 20), intUser - (intOTSS + 20));
            ConnectID = responseBody.Substring((intUser + 9), responseBody.Length - (intUser + 9));
            //新浪微博OAuth1.0不会过期
            ExpireDate = DateTime.Now.AddYears(100);
            return verify_credentials();
        }
        public string verify_credentials()
        {
            Uri uri = new Uri("http://api.t.sina.com.cn/account/verify_credentials.xml");
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(
            uri,
            platform.ApiKey,
            platform.ApiKeySecret,
            OAuth_Token,
            OAuth_Token_Secret,
            "Get",
            timeStamp,
            nonce,
            string.Empty,
            out normalizeUrl,
            out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", platform.ApiKey);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_token={0}&", OAuth_Token);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            return responseBody;//用户个人信息在这个里面了！！
        }
        public string sendcontent(string content)
        {
            CBB.OAuth.Sina.Environment.AccessToken = new CBB.OAuth.Sina.Common.OAuthAccessToken() { Token = OAuth_Token, Secret = OAuth_Token_Secret };
            CBB.OAuth.Sina.DataContract.UpdateStatusInfo usi = new CBB.OAuth.Sina.DataContract.UpdateStatusInfo();
            usi.Status = content;
            CBB.OAuth.Sina.DataContract.StatusInfo si = CBB.OAuth.Sina.AMicroblog.PostStatus(usi);
            return si.Text;
        }
        public string sendcontent(string content, string pic)
        {
            CBB.OAuth.Sina.Environment.AccessToken = new CBB.OAuth.Sina.Common.OAuthAccessToken() { Token = OAuth_Token, Secret = OAuth_Token_Secret };
            CBB.OAuth.Sina.DataContract.UpdateStatusWithPicInfo usi = new CBB.OAuth.Sina.DataContract.UpdateStatusWithPicInfo();
            usi.Status = content;
            usi.Pic = pic;
            CBB.OAuth.Sina.DataContract.StatusInfo si = CBB.OAuth.Sina.AMicroblog.PostStatusWithPic(usi);
            return si.Text;
        }
    }
}
