/* * * * *****************************************************************************************************
 * 人人网开放平台的客户端调用类，主要的操作可以通过调用这个类来获得各种数据
 * 1：GetAuthorizationCode()使用这个方法后，如果执行成功，
 *    服务器会把你的页面地址转到你callback上并附加code参数，code即为你要获得的Authorization code
 * 2：调用接口方法，使用CallMethod，如果有文件上传，调用CallMethodWithFile。
 * 3：API Key、Secre Key、各个请求URI，Format都是在Web.config中配置的
 *    (对应web开发，如果是客户端开发，自行配置APP.config).然后通过APIConfig类进行获取。
 * 4：本SDK的http请求功能有异步和同步两个方面的，但是本类中只对应同步请求的，
 *    如果想使用异步，请自行使用AsyncHttp.cs这个类。
 * 5：此SDK是从腾讯微博开放平台上搬过来的，原作者是vincent，由于腾讯没有具体给出vincent的联系方式，
 *    所以在此也只能给出作者的名字，如果知道原作者的联系方式，请告知。
 *    有问题大家可以互相讨论，我的联系方式：qdkll1985@163.com。
 * * *********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.OAuth.RenRen.APIUtility;
using Newtonsoft.Json.Linq;

namespace CBB.OAuth.RenRen
{
    public class RenrenApiClient
    {
        public RenrenApiClient() { }
        public RenrenApiClient(string apiKey, string apiKeySecret,string authorizationUrl, string callbackUrl,string accessUrl, string apiUrl, string sessionURL)
        {
            APIConfig.apiKey = apiKey;
            APIConfig.secretKey = apiKeySecret;
            APIConfig.authorizationURL = authorizationUrl;
            APIConfig.apiUrl = apiUrl;
            APIConfig.accessUrl = accessUrl;
            APIConfig.callBackURL = callbackUrl;
            APIConfig.sessionURL = sessionURL;
            APIConfig.format = "json";
        }
        /// <summary>
        /// 获取 Authorization code
        /// 执行此方法后，将会访问callback地址，
        /// 返回需要访问的URL地址，形式如：http://www.exaple.com?code=xxxx
        /// code就为需要获得的Authorization code。
        /// </summary>
        public void GetAuthorizationCode()
        {
            string authorizationUrl = APIConfig.AuthorizationURL;
            List<APIParameter> paras = new List<APIParameter>() { 
                new APIParameter("client_id",APIConfig.ApiKey),
                new APIParameter("response_type","code"),
                new APIParameter("scope","publish_share,photo_upload,publish_feed"),
                new APIParameter("redirect_uri",APIConfig.CallBackURL)
            };
            string requestUrl = HttpUtil.AddParametersToURL(authorizationUrl, paras);
            System.Web.HttpContext.Current.Response.Redirect(requestUrl,false);
        }

        /// <summary>
        /// 执行接口方法
        /// 传入参数列表，比如接口名字，format形式等
        /// 还可以上传文件
        /// </summary>
        /// <param name="paras">参数列表</param>
        /// <param name="files">文件列表</param>
        /// <returns>服务器响应数据</returns>
        public string CallMethodWithFile(List<APIParameter> paras, List<APIParameter> files)
        {
            APIValidation av = new APIValidation();
            string responseData = "";
            string session_key = av.GetSessionKey();
            if (session_key == "" || paras == null || paras.Count == 0||files==null||files.Count==0)
                return "";
            paras.Add(new APIParameter("api_key", APIConfig.ApiKey));
            paras.Add(new APIParameter("call_id", DateTime.Now.Millisecond.ToString()));
            paras.Add(new APIParameter("v", "1.0"));
            paras.Add(new APIParameter("session_key", session_key));
            paras.Add(new APIParameter("format",APIConfig.Format));
            string strSig = av.CalSig(paras);
            if (strSig == "")
                return "";
            paras.Add(new APIParameter("sig", strSig));
            responseData = new SyncHttp().HttpPostWithFile(APIConfig.RenRenAPIURL, paras,files);
            return responseData;
        }
        /// <summary>
        /// 执行接口方法
        /// 传入执行方法名称，传入上传文件列表
        /// </summary>
        /// <param name="methodName">要执行的接口方法</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>服务器响应</returns>
        public string CallMethodWithFile(string methodName, List<APIParameter> files)
        {
            if (methodName == ""||files==null||files.Count==0)
                return "";
            List<APIParameter> paras = new List<APIParameter>() { 
                new APIParameter("method",methodName)
            };
            return CallMethodWithFile(paras, files);
        }
        /// <summary>
        /// 执行接口方法
        /// 传入参数列表，比如接口名字，format形式等
        /// </summary>
        /// <param name="paras">参数列表</param>
        /// <returns>服务器响应数据</returns>
        public string CallMethod(List<APIParameter> paras)
        {
            APIValidation av = new APIValidation();
            string responseData = "";
            string session_key = av.GetSessionKey();
            if (session_key == "" || paras == null || paras.Count == 0)
                return "";
            paras.Add(new APIParameter("api_key", APIConfig.ApiKey));
            paras.Add(new APIParameter("call_id", DateTime.Now.Millisecond.ToString()));
            paras.Add(new APIParameter("v", "1.0"));
            paras.Add(new APIParameter("session_key", session_key));
            paras.Add(new APIParameter("format", APIConfig.Format));
            string strSig = av.CalSig(paras);
            if (strSig == "")
                return "";
            paras.Add(new APIParameter("sig", strSig));
            responseData = new SyncHttp().HttpPost(APIConfig.RenRenAPIURL, HttpUtil.GetQueryFromParas(paras));
            return responseData;
        }
        /// <summary>
        /// 执行接口的方法，之所以在这里加上这个format，实验发现人人网返回的数据形式默认的根本不是json而是xml。
        /// </summary>
        /// <param name="methodName">传入要执行的接口的名称</param>
        /// <returns>服务器响应数据</returns>
        public string CallMethod(string methodName)
        {
            if (methodName == "")
                return "";
            List<APIParameter> paras = new List<APIParameter>() { 
                new APIParameter("method",methodName)
            };
            return CallMethod(paras);
        }

    }
}
