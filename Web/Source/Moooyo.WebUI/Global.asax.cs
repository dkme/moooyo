using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using System.Timers;

namespace Moooyo.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_OnRequest(Object sender, EventArgs e)
        { }
        protected void Application_OnPostAuthenticateRequest(Object sender, EventArgs e)
        {
            IPrincipal contextUser = Context.User;
            var id = Context.User.Identity as FormsIdentity;
            if (id != null && id.IsAuthenticated)
            {
                var roles = id.Ticket.UserData.Split(',');
                Context.User = new System.Security.Principal.GenericPrincipal(id, roles);
                Thread.CurrentPrincipal = HttpContext.Current.User;
            }
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Vistors", "Msg/Vistors/{p}",
                new { controller = "Msg", action = "Vistors", p = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Restricted", "Account/Restricted/{p}",
                new { controller = "Account", action = "Restricted", p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AboutMeActivity", "Msg/AboutMeActivity/{p}",
                new { controller = "Msg", action = "AboutMeActivity", p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Favors", "Relation/Favors/{p}",
                new { controller = "Relation", action = "Favors", p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Fans", "Relation/Fans/{page}",
                new { controller = "Relation", action = "Fans", page = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Setting", "Member/Setting/{type}",
                new { controller = "Member", action = "Setting" }
            );
            routes.MapRoute(
                "userCall", "Admin/userCall/{t}/{isauditedstr}/{p}",
                new { controller = "Admin", action = "userCall", t = UrlParameter.Optional, isauditedstr = UrlParameter.Optional, p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "MemberManager", "Admin/MemberManager/{p}",
                new { controller = "Admin", action = "MemberManager", p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Admin", "Admin/dic/{t}",
                new { controller = "Admin", action = "dic", t = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AdminRecommendedData", "Admin/RecommendedData/{t}",
                new { controller = "Admin", action = "RecommendedData", t = UrlParameter.Optional }
            );
            routes.MapRoute(
               "Verify", "Admin/verify/{t}",
               new { controller = "Admin", action = "verify", t = UrlParameter.Optional }
           );
            routes.MapRoute(
                "Topic", "Plaza/Topic/{topicid}/{p}",
                new { controller = "Plaza", action = "Topic", p = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    "PlazaSimple", "Plaza/I",
            //    new { controller = "Plaza", action = "I" }
            //);

            //routes.MapRoute(
            //    "Plaza", "Plaza/I/{plazaid}/{p}",
            //    new { controller = "Plaza", action = "I", p = UrlParameter.Optional, plazaid = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    "PlazaSay", "Plaza/Say/{plazaid}",
            //    new { controller = "Plaza", action = "Say" }
            //);
            routes.MapRoute(
                "GetPhoto", "Photo/Get/{id}",
                new { controller = "Photo", action = "Get" }
            );
            //routes.MapRoute(
            //    "Sys", "Sys/SysHi/{you}/{type}",
            //    new { controller = "Msg", action = "I" }
            //);
            routes.MapRoute(
                "MessagesList", "Msg/MessagesList/{you}/{page}",
                new { controller = "Msg", action = "MessagesList", you = UrlParameter.Optional, page = UrlParameter.Optional }
            );
            routes.MapRoute(
                "MessageDetails", "Msg/MessageDetails/{you}/{page}",
                new { controller = "Msg", action = "MessageDetails", you = UrlParameter.Optional, page = UrlParameter.Optional }
            );
            routes.MapRoute(
                "SystemMsgs", "Msg/SystemMsgs/{p}",
                new { controller = "Msg", action = "SystemMsgs", p = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Msg", "Msg/I/{you}",
                new { controller = "Msg", action = "I", you = UrlParameter.Optional }
            );
            routes.MapRoute(
                "MemberInterView", "Member/I/{id}/InterView",
                new { controller = "Member", action = "InterView", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    "MemberSkill", "Member/I/{id}/Skill",
            //    new { controller = "Member", action = "Skill", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                "Member", "Member/I/{id}/{pn}",
                new { controller = "Member", action = "I", id = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "MemberUrl", "u/{id}",
                new { controller = "Member", action = "MemberDomain", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    "MemberAlbum", "u/p/{id}",
            //    new { controller = "photo", action = "mplist", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                "MemberTa", "Member/Ta/{membId}/{pn}",
                new { controller = "Member", action = "Ta", membId = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Relation", "Relation/I/{t}/{p}",
                new { controller = "Relation", action = "I", t = UrlParameter.Optional, p = UrlParameter.Optional }
            );
            routes.MapRoute(
               "FeaturedInterestTopic", "InterestCenter/FeaturedInterestTopic/{sex}/{pn}",
               new { controller = "InterestCenter", action = "FeaturedInterestTopic", sex = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //   "BBMGirl", "BBM/ProfileGirl/{t}/{bid}",
            //   new { controller = "BBM", action = "ProfileGirl", t = UrlParameter.Optional, bid = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //   "BBMBoy", "BBM/ProfileBoy/{t}/{jid}",
            //   new { controller = "BBM", action = "ProfileBoy", t = UrlParameter.Optional, jid = UrlParameter.Optional }
            //);
            routes.MapRoute(
                "ShowInterest", "InterestCenter/ShowInterest/{iID}",
                new { controller = "InterestCenter", action = "ShowInterest", iID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "IContent", "Content/IContent/{contenttype}/{pn}",
                new { controller = "Content", action = "IContent", contenttype = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "IFavorerContent", "Content/IFavorerContent/{contenttype}/{pn}",
                new { controller = "Content", action = "IFavorerContent", contenttype = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "IndexContent", "Content/IndexContent/{interestID}/{city}/{sex}/{pn}",
                new { controller = "Content", action = "IndexContent", interestID = UrlParameter.Optional, city = UrlParameter.Optional, sex = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "TaContent", "Content/TaContent/{memberID}/{contenttype}/{pn}",
                new { controller = "Content", action = "TaContent", memberID = UrlParameter.Optional, contenttype = UrlParameter.Optional, pn = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AddImageContent", "Content/AddImageContent/{contentID}",
                new { controller = "Content", action = "AddImageContent", contentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AddSuiSuiNianContent", "Content/AddSuiSuiNianContent/{contentID}",
                new { controller = "Content", action = "AddSuiSuiNianContent", contentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AddCallForContent", "Content/AddCallForContent/{contentID}",
                new { controller = "Content", action = "AddCallForContent", contentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "UpdateInterest", "InterestCenter/UpdateInterest/{interestid}",
                new { controller = "InterestCenter", action = "UpdateInterest", interestid = UrlParameter.Optional }
            );
            routes.MapRoute(
                "UpdateInterestFansIcon", "InterestCenter/UpdateInterestFansIcon/{interestid}",
                new { controller = "InterestCenter", action = "UpdateInterestFansIcon", interestid = UrlParameter.Optional }
            );
            routes.MapRoute(
                "UpdateInterestFansList", "InterestCenter/UpdateInterestFansList/{interestid}",
                new { controller = "InterestCenter", action = "UpdateInterestFansList", interestid = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Error", "Error/Error/{errorno}/{errorinfo}",
                new { controller = "Error", action = "Error", errorno = UrlParameter.Optional, errorinfo = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    "Regist", "{ifweibo}", // 路由名称，带有参数的 URL 
            //    new { controller = "Register", action = "Regist", ifweibo = UrlParameter.Optional } // 参数默认值
            //);
            //routes.MapRoute(
            //    "MemberDomain", "{id}",
            //    new { controller = "Member", action = "MemberDomain", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "Index", "{id}", // 路由名称，带有参数的 URL 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );
            //routes.MapRoute(
            //    "OnlinePage", "{}", // 路由名称，带有参数的 URL 
            //    new { controller = "Home", action = "OnlinePage"} // 参数默认值
            //);
            routes.MapRoute(
                "Default", "{controller}/{action}/{id}/{tp}", // 路由名称，带有参数的 URL，{id}通常是用户ID，{tp}通常是type
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, tp = UrlParameter.Optional } // 参数默认值
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            //加载log4net配置信息
            CBB.Logger.SystemLog.LogHelper.SetConfig();

            ////超人获取 86400000
            //System.Timers.Timer myTimer = new System.Timers.Timer(1000);
            ////关联事件 
            //myTimer.Elapsed += new System.Timers.ElapsedEventHandler(setSuperMan);
            //myTimer.AutoReset = true;
            //myTimer.Enabled = true;
            //myTimer.Start();           
            
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //Exception objExp = HttpContext.Current.Server.GetLastError();
            //Moooyo.BiZ.Comm.SystemLog.LogHelper.WriteLog("\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, objExp);
        }

        /*
        private void setSuperMan(object sender, ElapsedEventArgs e) 
        {
            // 得到 hour minute second 如果等于某个值就开始执行 
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            // 定制时间,在00：00：00 的时候执行 
            int iHour = 00;
            int iMinute = 00;
            int iSecond = 00;

            // 设置 每天的00：00：00开始执行程序 
            if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            {
                //为头一天的超人颁发超人徽章
                IList<Moooyo.Active.SuperMan.Super> superlist = Moooyo.Active.SuperMan.SuperFactory.GetEveryDaySuper(1, 1);
                if (null != superlist && superlist.Count > 0)
                {
                    Moooyo.BiZ.MemberManager.MemberManager.AddBadgeList(superlist[0].ToMemberID, BiZ.Member.BadegType.superman, 1);
                }
            }             
        }*/

        // Begin Fix for the Flash Player Cookie bug in Non-IE browsers.
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            /* Fix for the Flash Player Cookie bug in Non-IE browsers.
             * Since Flash Player always sends the IE cookies even in FireFox
             * we have to bypass the cookies by sending the values as part of the POST or GET
             * and overwrite the cookies with the passed in values.
             * 
             * The theory is that at this point (BeginRequest) the cookies have not been read by
             * the Session and Authentication logic and if we update the cookies here we'll get our
             * Session and Authentication restored correctly
             */

            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Session");
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Forms Authentication");
            }
        }
        //private void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        // 先取得該使用者的 FormsIdentity
        //        FormsIdentity id = (FormsIdentity)User.Identity;
        //        // 再取出使用者的 FormsAuthenticationTicket
        //        FormsAuthenticationTicket ticket = id.Ticket;
        //        // 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
        //        string[] roles = ticket.UserData.Split(new char[] { ',' });
        //        // 指派角色到目前這個 HttpContext 的 User 物件去
        //        Context.User = new GenericPrincipal(Context.User.Identity, roles);
        //    }
        //}
        private void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }
        // End Fix for the Flash Player Cookie bug in Non-IE browsers.
    }
}
