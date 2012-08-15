using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Moooyo.WebUI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        public ActionResult Index(String op, String ReturnUrl)
        {
            SetMetasVersion();

            IList<BiZ.Sys.FeaturedContent.FeaturedContent> featuredContent = BiZ.Sys.FeaturedContent.FeaturedContentProvider.GetFeaturedContents(0, 0, BiZ.Comm.UsedFlag.Yes);
            int featCentCount = featuredContent.Count;
            String[] arrChoiceImage = new string[featCentCount];
            string[] arrChoiceContent = new string[featCentCount];
            for (int i = 0; i < featCentCount; i++)
            {
                arrChoiceImage[i] = featuredContent[i].Image;
                arrChoiceContent[i] = featuredContent[i].Content;
            }
            Random random = new Random(DateTime.Now.Millisecond);
            int randomNum;
            randomNum = random.Next(featCentCount);

            ViewData["choiceImage"] = arrChoiceImage[randomNum];
            ViewData["choiceContent"] = arrChoiceContent[randomNum];

            //登录时验证记住用户名和密码，开始 ***********
            HttpCookie UserNameCookie = Request.Cookies["loginUserNameCookie"];
            HttpCookie UserPasswordCookie = Request.Cookies["loginUserPasswordCookie"];
            String userNameCookie, userPasswordCookie;
            if (UserNameCookie != null && UserPasswordCookie != null)
            {
                userNameCookie = UserNameCookie.Values["loginUserName"];
                userPasswordCookie = UserPasswordCookie.Values["loginUserPassword"];//直接获取COOKIE中的密码值
                String encryptionUserName = CBB.Security.MD5Helper.getMd5Hash(userNameCookie + "moooyo.com");
                String transformedPassword = userPasswordCookie.Substring(encryptionUserName.Length, (userPasswordCookie.Length - encryptionUserName.Length));

                String userId = Moooyo.BiZ.MemberManager.MemberManager.MemberPasswordLogin(userNameCookie, transformedPassword, Request.UserHostAddress.ToString().Trim());
                BiZ.Member.Member mym = null;
                if (userId != "")
                {
                    mym = MemberLoginStep2(userNameCookie, transformedPassword, userId, true);

                    string jumpUrl = AccountController.GetConditionLoginJump(mym.ID, ReturnUrl);
                    return Redirect(jumpUrl);
                }
            }
            //登录时验证记住用户名和密码，结束 ***********

            if (null != Request.QueryString["No"] && Request.QueryString["No"].ToString().Trim().Length >= 0)
            {
                string memeberid = Request.QueryString["No"].ToString().Trim();
                Session["Inviter"] = memeberid;
            }
            string memberId = HttpContext.User.Identity.Name;
            if ((memberId != "") || (memberId != string.Empty))
            {
                string jumpUrl = AccountController.GetConditionLoginJump(memberId, ReturnUrl);
                return Redirect(jumpUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                if (op == "nousername")
                {
                    ViewData["errinfo"] = "用户名或密码错误。";
                    if (Session["loginerrorcount"] == null)
                        Session["loginerrorcount"] = "1";
                    else
                        Session["loginerrorcount"] = (int.Parse(Session["loginerrorcount"].ToString()) + 1).ToString();
                }
                if (op == "errorcode")
                    ViewData["errinfo"] = "验证码错误。";
                return View();
            }
        }
        public ActionResult Index2(String op, String ReturnUrl)
        {
            SetMetasVersion();

            //bool isalreadylogin = Common.Comm.isalreadylogin("Member", HttpContext.User);

            //if (isalreadylogin)
            //    return RedirectToAction("IndexContent", "Content");
            //else
            //    return RedirectToAction("Login", "Account");


            IList<BiZ.Sys.FeaturedContent.FeaturedContent> featuredContent = BiZ.Sys.FeaturedContent.FeaturedContentProvider.GetFeaturedContents(0, 0, BiZ.Comm.UsedFlag.Yes);
            int featCentCount = featuredContent.Count;
            String[] arrChoiceImage = new string[featCentCount];
            string[] arrChoiceContent = new string[featCentCount];
            for (int i = 0; i < featCentCount; i++)
            {
                arrChoiceImage[i] = featuredContent[i].Image;
                arrChoiceContent[i] = featuredContent[i].Content;
            }
            Random random = new Random(DateTime.Now.Millisecond);
            int randomNum;
            randomNum = random.Next(featCentCount);

            ViewData["choiceImage"] = arrChoiceImage[randomNum];
            ViewData["choiceContent"] = arrChoiceContent[randomNum];

            //登录时验证记住用户名和密码，开始 ***********
            HttpCookie UserNameCookie = Request.Cookies["loginUserNameCookie"];
            HttpCookie UserPasswordCookie = Request.Cookies["loginUserPasswordCookie"];
            String userNameCookie, userPasswordCookie;
            if (UserNameCookie != null && UserPasswordCookie != null)
            {
                userNameCookie = UserNameCookie.Values["loginUserName"];
                userPasswordCookie = UserPasswordCookie.Values["loginUserPassword"];//直接获取COOKIE中的密码值
                String encryptionUserName = CBB.Security.MD5Helper.getMd5Hash(userNameCookie + "moooyo.com");
                String transformedPassword = userPasswordCookie.Substring(encryptionUserName.Length, (userPasswordCookie.Length - encryptionUserName.Length));

                String userId = Moooyo.BiZ.MemberManager.MemberManager.MemberPasswordLogin(userNameCookie, transformedPassword, Request.UserHostAddress.ToString().Trim());
                BiZ.Member.Member mym = null;
                if (userId != "")
                {
                    mym = MemberLoginStep2(userNameCookie, transformedPassword, userId, true);

                    string jumpUrl = AccountController.GetConditionLoginJump(mym.ID, ReturnUrl);
                    return Redirect(jumpUrl);
                }
            }
            //登录时验证记住用户名和密码，结束 ***********

            if (null != Request.QueryString["No"] && Request.QueryString["No"].ToString().Trim().Length >= 0)
            {
                string memeberid = Request.QueryString["No"].ToString().Trim();
                Session["Inviter"] = memeberid;
            }
            string memberId = HttpContext.User.Identity.Name;
            if ((memberId != "") || (memberId != string.Empty))
            {
                string jumpUrl = AccountController.GetConditionLoginJump(memberId, ReturnUrl);
                return Redirect(jumpUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                if (op == "nousername")
                {
                    ViewData["errinfo"] = "用户名或密码错误。";
                    if (Session["loginerrorcount"] == null)
                        Session["loginerrorcount"] = "1";
                    else
                        Session["loginerrorcount"] = (int.Parse(Session["loginerrorcount"].ToString()) + 1).ToString();
                }
                if (op == "errorcode")
                    ViewData["errinfo"] = "验证码错误。";
                return View();
            }

        }

        public ActionResult About()
        {
            return View();
        }
        #endregion

        #region 数据与业务方法
        /// <summary>
        /// 用户登录步骤2
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="userId">用户编号</param>
        /// <param name="rememberflag1">记住用户标记</param>
        /// <returns>用户对象</returns>
        public BiZ.Member.Member MemberLoginStep2(String email, String password, String userId, Boolean rememberflag1)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userId);
            Session["logintimes"] = mym.Status.LoginTimes.ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
            (1,
            userId.ToString(),
            DateTime.Now,
            DateTime.Now.AddYears(1),
            rememberflag1,
            "Member",
            FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            //登录时记住用户名和密码，开始 ***********
            String encryptionUserName = CBB.Security.MD5Helper.getMd5Hash(email + "moooyo.com");
            HttpCookie UserNameCookie = Request.Cookies["loginUserNameCookie"];
            HttpCookie UserPasswordCookie = Request.Cookies["loginUserPasswordCookie"];
            if (rememberflag1)
            {
                //保存用户名和密码到cookie
                if (UserNameCookie == null)
                {
                    UserNameCookie = new HttpCookie("loginUserNameCookie");
                    UserNameCookie.Values.Add("loginUserName", email);
                    UserNameCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(UserNameCookie);
                }
                else if (UserNameCookie.Values["loginUserName"] != email) //修改COOKIE
                {
                    UserNameCookie.Values["loginUserName"] = email;
                    UserNameCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(UserNameCookie);
                }

                if (UserPasswordCookie == null)
                {
                    UserPasswordCookie = new HttpCookie("loginUserPasswordCookie");
                    //如果重新指定用户密码，则重新加密密码
                    string password1 = password;
                    UserPasswordCookie.Values.Add("loginUserPassword", encryptionUserName + password1);
                    UserPasswordCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(UserPasswordCookie);
                }
                else if (UserPasswordCookie.Values["loginUserPassword"] != (encryptionUserName + password))
                {
                    UserPasswordCookie.Values["loginUserPassword"] = encryptionUserName + password;
                    UserPasswordCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(UserPasswordCookie);
                }
            }
            else
            {
                //从cookie删除用户名和密码
                if (Response.Cookies["loginUserNameCookie"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("loginUserNameCookie");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
                if (Response.Cookies["loginUserPasswordCookie"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("loginUserPasswordCookie");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
            }
            //登录时记住用户名和密码，结束 ***********

            return mym;
        }
        #endregion
    }
}
