using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CBB.OAuth.RenRen.APIUtility
{
    public class APIConfig
    {
        public static string apiKey;
        public static string secretKey;
        public static string format;
        public static string accessUrl;
        public static string sessionURL;
        public static string authorizationURL;
        public static string callBackURL;
        public static string apiUrl;

        public APIConfig()
        {
        }

        public static string GetValueFromConfig(string key)
        {
            string value = String.Empty;
            return ConfigurationManager.AppSettings[key];
        }

        public static string ApiKey
        {
            get { return apiKey; }
        }
        public static string SecretKey
        {
            get { return secretKey; }
        }

        public static string Format
        {
            get { return format; }
        }

        // url
        public static string AuthorizationURL
        {
            get { return authorizationURL; }
        }
        public static string AccessURL
        {
            get { return accessUrl; }
        }
        public static string SessionURL
        {
            get { return sessionURL; }
        }
        public static string RenRenAPIURL
        {
            get { return apiUrl; }
        }
        public static string CallBackURL
        {
            get { return callBackURL; }
        }
    }
}
