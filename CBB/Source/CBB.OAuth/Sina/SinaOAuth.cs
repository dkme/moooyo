using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.OAuth.Sina
{
    public static class SinaOAuthSetting
    {
        public static string ApiKey
        {
            get
            {
                if (apiKey == null) apiKey = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaApiKey");
                return apiKey;
            }
        }
        private static string apiKey;
        public static string ApiKeySecret
        {
            get
            {
                if (apiKeySecret == null) apiKeySecret = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaApiKeySecret");
                return apiKeySecret;
            }
        }
        private static string apiKeySecret;
        public static string RequestTokenUri
        {
            get
            {
                if (requestTokenUri == null) requestTokenUri = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaRequestTokenUri");
                return requestTokenUri;
            }
        }
        private static string requestTokenUri;
        public static string AUTHORIZE
        {
            get
            {
                if (aUTHORIZE == null) aUTHORIZE = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaAUTHORIZEUri");
                return aUTHORIZE;
            }
        }
        private static string aUTHORIZE;
        public static string ACCESS_TOKEN
        {
            get
            {
                if (aCCESS_TOKEN == null) aCCESS_TOKEN = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SinaACCESS_TOKENUri");
                return aCCESS_TOKEN;
            }
        }
        private static string aCCESS_TOKEN;
    }
}
