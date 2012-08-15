using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Moooyo.BiZ.Member.Connector
{
    /// <summary>
    /// 腾讯微博连接类
    /// </summary>
    public class TXWeiboConnector : Connector
    {
        private PlatformDef platformDef = PlatformProvider.GetPlatformDef(Platform.TencentWeibo);
        private OpenTSDK.Tencent.OAuth txoauth;

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
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override string Connect(String addparams)
        {
            this.PlatformType = BiZ.Member.Connector.Platform.TencentWeibo;
            txoauth = new OpenTSDK.Tencent.OAuth(platformDef.ApiKey, platformDef.ApiKeySecret);
            //Session["txoath"] = txoauth;

            //获取请求Token
            if (txoauth.GetRequestToken(platformDef.CallbackUrl + addparams))
            {
                return "https://open.t.qq.com/cgi-bin/authorize?oauth_token=" + txoauth.Token;
                //Response.Redirect("https://open.t.qq.com/cgi-bin/authorize?oauth_token=" + txoauth.Token, false);
            }

            return "err";
        }
        public override CBB.ExceptionHelper.OperationResult SetDisable()
        {
            return ConnectorProvider.SaveConnectorEnableStatus(ConnectID, PlatformType, false);
        }
        public override CBB.ExceptionHelper.OperationResult SetEnable()
        {
            return ConnectorProvider.SaveConnectorEnableStatus(ConnectID, PlatformType, true);
        }
        public override string SendInfo(string url,string info)
        {
            if (txoauth==null)
                txoauth = new OpenTSDK.Tencent.OAuth(platformDef.ApiKey, platformDef.ApiKeySecret);

            txoauth.Token = OAuth_Token;
            txoauth.TokenSecret = OAuth_Token_Secret;

            OpenTSDK.Tencent.Objects.TweetOperateResult result = new OpenTSDK.Tencent.API.Twitter(txoauth).Add(info +","+ url, "127.0.0.1");
            return result.Msg;
        }
        public override string SendInfoWithPic(string url, string info, string pic)
        {
            if (txoauth == null)
                txoauth = new OpenTSDK.Tencent.OAuth(platformDef.ApiKey, platformDef.ApiKeySecret);

            txoauth.Token = OAuth_Token;
            txoauth.TokenSecret = OAuth_Token_Secret;

            OpenTSDK.Tencent.Objects.TweetOperateResult result = new OpenTSDK.Tencent.API.Twitter(txoauth).Add(info + "," + url, pic, "127.0.0.1");
            return result.Msg;
        }
        public bool Connect(string oauth_verifier, string oauth_token)
        {
            try
            {
                String name;
                txoauth.GetAccessToken(oauth_verifier, out name);
                OpenTSDK.Tencent.API.User user = new OpenTSDK.Tencent.API.User(txoauth);
                OpenTSDK.Tencent.Objects.UserProfileData<OpenTSDK.Tencent.Objects.UserProfile> upd = user.GetProfile();
                bool hasprovince = false;
                if (upd.Profile.Location != "" & upd.Profile.Location != "未知")
                {
                    if (upd.Profile.Location.Trim().Split(' ').Length > 1)
                    {
                        string[] city = upd.Profile.Location.Trim().Split(' ');
                        this.Province = city[0];
                        this.City = city[1];
                        hasprovince = true;
                    }
                }
                if (!hasprovince)
                {
                    this.Province = "北京";
                    this.City = "北京";
                }
                this.Sex = "1";
                this.SexStr = "男";
                if (upd.Profile.Sex.ToString() != "Male")
                {
                    this.Sex = "2";
                    this.SexStr = "女";
                }
                this.ConnectID = upd.Profile.Uid;
                this.Name = upd.Profile.NickName != "" ? upd.Profile.NickName : upd.Profile.Name;
                if (this.ConnectID == null || this.ConnectID == "")
                {
                    this.ConnectID = upd.Profile.Name;
                }
                this.Enable = true;
                this.OAuth_Token = txoauth.Token;
                this.OAuth_Token_Secret = txoauth.TokenSecret;

            }
            catch (Exception err)
            {
                return false;
            }
            return true;
        }
    }
}
