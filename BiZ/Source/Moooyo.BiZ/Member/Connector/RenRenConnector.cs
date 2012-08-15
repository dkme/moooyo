using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace Moooyo.BiZ.Member.Connector
{
    /// <summary>
    /// 人人网连接类
    /// </summary>
    public class RenRenConnector : Connector
    {
        private PlatformDef platform = PlatformProvider.GetPlatformDef(Platform.RenRen);
        public CBB.OAuth.RenRen.RenrenApiClient client;

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
            this.PlatformType = Platform.RenRen;
            client = new CBB.OAuth.RenRen.RenrenApiClient(
                platform.ApiKey,
                platform.ApiKeySecret,
                platform.RequestTokenUri,
                platform.CallbackUrl + addparams,
                platform.ACCESS_TOKEN,
                platform.APIUrl,
                platform.SessionUrl);

            client.GetAuthorizationCode();
            return "true";
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
            string response = sendcontent(url, info, "");
            return response;
        }
        public override string SendInfoWithPic(string url, string info, string picurl)
        {
            string response = sendcontent(url, info, picurl);
            return response;
        }

        public string sendcontent(string url, string content, string picurl)
        {
            client = new CBB.OAuth.RenRen.RenrenApiClient(
                platform.ApiKey,
                platform.ApiKeySecret,
                platform.RequestTokenUri,
                platform.CallbackUrl,
                OAuth_Token_Secret,
                platform.APIUrl,
                platform.SessionUrl);

            System.Web.HttpContext.Current.Session["accessToken"] = OAuth_Token;
            System.Web.HttpContext.Current.Session["sessionKey"] = OAuth_Token_Secret;

            string title = content.Length > 20 ? content.Substring(0, 20) + ".." : content;
            List<CBB.OAuth.RenRen.APIUtility.APIParameter> paraList = new List<CBB.OAuth.RenRen.APIUtility.APIParameter>();
            paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("method", "feed.publish"));
            paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("template_id", "1"));
            paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("title_data", "{\"title\":\"" + title + "\",\"content\":\"" + content + "\"}"));
            paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("body_data", "{\"title\":\"" + title + "\",\"content\":\"" + content + "\"}"));
            paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("desc", content));
            if (picurl != "")
            {
                paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("attachment", "{\"src\":\"" + picurl + "\",\"href\":\"" + picurl + "\"}"));
            }
            if (content != "")
            {
                paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("action_links", "{\"href\":\"" + url + "\",\"text\":\"" + content + "\"}"));
            }
            else
            {
                paraList.Add(new CBB.OAuth.RenRen.APIUtility.APIParameter("action_links", "{\"href\":\"" + url + "\",\"text\":\"米柚\"}"));
            }

            string responseData = client.CallMethod(paraList);

            return responseData;
        }
        public bool getUserinfo()
        {
            try
            {
                client = new CBB.OAuth.RenRen.RenrenApiClient(
                    platform.ApiKey,
                    platform.ApiKeySecret,
                    platform.RequestTokenUri,
                    platform.CallbackUrl,
                    platform.ACCESS_TOKEN,
                    platform.APIUrl,
                    platform.SessionUrl);
                string responseData = client.CallMethod("users.getInfo");
                if (responseData == "") return false;
                OAuth_Token = System.Web.HttpContext.Current.Session["accessToken"].ToString();
                OAuth_Token_Secret = System.Web.HttpContext.Current.Session["sessionKey"].ToString();
                JArray arr = JArray.Parse(responseData);
                if (arr.Count < 1) return false;
                JObject obj = arr[0] as JObject;
                this.ConnectID = obj["uid"].ToString();
                this.Sex = obj["sex"].ToString();
                this.ICONPath = obj["tinyurl"].ToString();
                this.Name = obj["name"].ToString();
                Enable = true;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}