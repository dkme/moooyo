using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member.Connector
{
    /// <summary>
    /// 平台提供类
    /// </summary>
    public class PlatformProvider
    {
        //缓存
        public static Hashtable CatchHT;
        /// <summary>
        /// 获取平台定义
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static PlatformDef GetPlatformDef(Platform platform)
        {
            if (CatchHT == null) CatchHT = new Hashtable();
            if (CatchHT[platform] != null) return CatchHT[platform] as PlatformDef;

            PlatformDef obj = new PlatformDef();
            switch (platform)
            {
                case Platform.SinaWeibo:
                    obj = new PlatformDef();
                    obj.ApiKey = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaWeiboKey");//申请的App Key
                    obj.ApiKeySecret = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaWeiboKeySecret");//申请的App Secret
                    obj.RequestTokenUri = "http://api.t.sina.com.cn/oauth/request_token";
                    obj.AUTHORIZE = "http://api.t.sina.com.cn/oauth/authorize";
                    obj.ACCESS_TOKEN = "http://api.t.sina.com.cn/oauth/access_token";
                    obj.OAuthType = OAuthType.Type1;
                    obj.CallbackUrl = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaWeiboCallback");
                    CatchHT[platform] = obj;
                    break;
                case Platform.TencentWeibo:
                    obj = new PlatformDef();
                    obj.ApiKey = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("TXWeiboKey");//申请的App Key
                    obj.ApiKeySecret = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("TXWeiboKeySecret");//申请的App Secret
                    obj.RequestTokenUri = "https://open.t.qq.com/cgi-bin/request_token";
                    obj.AUTHORIZE = "https://open.t.qq.com/cgi-bin/authorize";
                    obj.ACCESS_TOKEN = "https://open.t.qq.com/cgi-bin/access_token";
                    obj.OAuthType = OAuthType.Type2;
                    obj.CallbackUrl = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("TXWeiboCallback");
                    CatchHT[platform] = obj;
                    break;
                case Platform.RenRen:
                    obj = new PlatformDef();
                    obj.ApiKey = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RenRenKey");//申请的App Key
                    obj.ApiKeySecret = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RenRenKeySecret");//申请的App Secret
                    obj.RequestTokenUri = "https://graph.renren.com/oauth/authorize";
                    obj.AUTHORIZE = "https://graph.renren.com/oauth/authorize";
                    obj.ACCESS_TOKEN = "https://graph.renren.com/oauth/token";
                    obj.APIUrl = "http://api.renren.com/restserver.do";
                    obj.SessionUrl = "https://graph.renren.com/renren_api/session_key";
                    obj.OAuthType = OAuthType.Type1;
                    obj.CallbackUrl = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RenRenCallback");
                    CatchHT[platform] = obj;
                    break;
                case Platform.Douban:
                    obj = new PlatformDef();
                    obj.ApiKey = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("DouBanKey");//申请的App Key
                    obj.ApiKeySecret = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("DouBanKeySecret");//申请的App Secret
                    obj.RequestTokenUri = "http://www.douban.com/service/auth/request_token";
                    obj.AUTHORIZE = "http://www.douban.com/service/auth/authorize";
                    obj.ACCESS_TOKEN = "http://www.douban.com/service/auth/access_token";
                    obj.APIUrl = "http://api.douban.com/miniblog/saying";
                    obj.OAuthType = OAuthType.Type1;
                    obj.CallbackUrl = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("DouBanCallback");
                    CatchHT[platform] = obj;
                    break;
                default:
                    obj = null;
                    break;
            }

            return obj;
        }
    }
    /// <summary>
    /// 外部平台定义
    /// </summary>
    public class PlatformDef
    {
        public Platform Platform;
        //申请的App Key
        public String ApiKey;
        //申请的App Secret
        public String ApiKeySecret;
        //RequestToken地址
        public String RequestTokenUri;
        //AUTHORIZE地址
        public String AUTHORIZE;
        //ACCESS_TOKEN地址
        public String ACCESS_TOKEN;
        //OAuth协议类型
        public OAuthType OAuthType;
        //返回页面地址
        public String CallbackUrl;
        //API地址
        public String APIUrl;
        //Session地址
        public String SessionUrl;
    }
    /// <summary>
    /// 外部平台
    /// </summary>
    public enum Platform
    {
        SinaWeibo=1,
        TencentWeibo=2,
        RenRen=3,
        Douban=4
    }
    /// <summary>
    /// OAuth验证类型：
    /// Type1-OAuth1.0
    /// Type2-OAuth2.0
    /// </summary>
    public enum OAuthType
    {
        Type1 = 1,
        Type2 = 2
    }
}
