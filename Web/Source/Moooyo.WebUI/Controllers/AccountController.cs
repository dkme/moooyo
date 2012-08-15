using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Moooyo.WebUI.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        SystemFuncController sfc = new SystemFuncController();

        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        //public ActionResult mylogin(String ReturnUrl)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    //登录时验证记住用户名和密码，开始 ***********
        //    HttpCookie UserNameCookie = Request.Cookies["loginUserNameCookie"];
        //    HttpCookie UserPasswordCookie = Request.Cookies["loginUserPasswordCookie"];
        //    String userNameCookie, userPasswordCookie;
        //    if (UserNameCookie != null && UserPasswordCookie != null)
        //    {
        //        userNameCookie = UserNameCookie.Values["loginUserName"];
        //        userPasswordCookie = UserPasswordCookie.Values["loginUserPassword"];//直接获取COOKIE中的密码值
        //        String encryptionUserName = CBB.Security.MD5Helper.getMd5Hash(userNameCookie + "moooyo.com");
        //        String transformedPassword = userPasswordCookie.Substring(encryptionUserName.Length, (userPasswordCookie.Length - encryptionUserName.Length));

        //        String userId = Moooyo.BiZ.MemberManager.MemberManager.MemberPasswordLogin(userNameCookie, transformedPassword, Request.UserHostAddress.ToString().Trim());
        //        BiZ.Member.Member mym = null;
        //        if (userId != "")
        //        {
        //            mym = MemberLoginStep2(userNameCookie, transformedPassword, userId, true);

        //            if (mym.AllowLogin)
        //            {
        //                if ((ReturnUrl != null) & (ReturnUrl != ""))
        //                    return Redirect(ReturnUrl);
        //                else
        //                    return RedirectToAction("FeaturedInterestTopic", "InterestCenter");
        //            }
        //            else
        //            {
        //                return RedirectToAction("WelCome", "Account");
        //            }
        //        }
        //    }
        //    //登录时验证记住用户名和密码，结束 ***********

        //    if (null != Request.QueryString["No"] && Request.QueryString["No"].ToString().Trim().Length >= 0)
        //    {
        //        string memeberid = Request.QueryString["No"].ToString().Trim();
        //        Session["Inviter"] = memeberid;
        //    }
        //    if ((HttpContext.User.Identity.Name != "") || (HttpContext.User.Identity.Name != string.Empty))
        //    {
        //        if ((ReturnUrl != null) & (ReturnUrl != ""))
        //            return Redirect(ReturnUrl);
        //        else
        //            return RedirectToAction("FeaturedInterestTopic", "InterestCenter");
        //    }
        //    else
        //    {
        //        ViewData["ReturnUrl"] = ReturnUrl;
        //        return View();
        //    }
        //}
        //public ActionResult alertlogin(string op, String ReturnUrl)
        //{

        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    ViewData["OP"] = op;
        //    if (ReturnUrl != null)
        //        ViewData["ReturnUrl"] = ReturnUrl;

        //    return View();
        //}
        public ActionResult loginfromweibo()
        {
            SetMetasVersion();
            String mid = Session["UserID"].ToString().Trim();
            if (mid != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                mid,
                DateTime.Now,
                DateTime.Now.AddYears(1),
                false,
                "Member",
                FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
            //该session在外部平台登录时记录
            Session.Remove("UserID");

            string notSelectedInterest = "false";
            string notActivated = "false";
            string hasBeenActivatedAndSelectedInterested = "false";
            string notActivatedAndSelectedInterest = "false";
            long memberInterestCount = 0;

            string memberId = mid;
            if (memberId != null)
            {
                BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(memberId);

                if(member != null)
                {
                    memberInterestCount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(memberId);

                    if (member.AllowLogin)
                    {

                        if (memberInterestCount < 1)
                        {
                            //未选兴趣
                            notSelectedInterest = "true";
                        }
                        else
                        {
                            //已激活并且选择了兴趣
                            hasBeenActivatedAndSelectedInterested = "true";
                        }
                    }
                    else
                    {
                        if (memberInterestCount < 1)
                        {
                            //未激活并且未选择兴趣
                            notActivatedAndSelectedInterest = "true";
                        }
                        else
                        {
                            //未激活
                            notActivated = "true";
                        }
                    }
                }
            }

            ViewData["notSelectedInterest"] = notSelectedInterest; //未选兴趣
            ViewData["notActivated"] = notActivated; //未激活
            ViewData["hasBeenActivatedAndSelectedInterested"] = hasBeenActivatedAndSelectedInterested; //已激活并且选择了兴趣
            ViewData["notActivatedAndSelectedInterest"] = notActivatedAndSelectedInterest; //未激活并且未选择兴趣

            //if (notSelectedInterest == "true" || notActivatedAndSelectedInterest == "true")
            //{
            //    Session["ifweibo"] = true;
            //}
            //else
            //{
            //    Session.Remove("ifweibo");
            //}
            return View();
        }
        [Authorize]
        public ActionResult LogOut()
        {
            string memberId = HttpContext.User.Identity.Name;
            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(memberId);

            //增加用户动态到后台 begin ————
            string operateUrl = "";
            if (memberId == "4eb0fde42101b0824e2b018f")
                operateUrl = "4eb0fde42101b0824e2b018f";
            else
            {
                if (member.UniqueNumber != null)
                    operateUrl = "/u/" + member.UniqueNumber.ConvertedID;
                else
                    operateUrl = "/Content/TaContent/" + member.ID;
            }
            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                memberId,
                "",
                BiZ.Sys.MemberActivity.MemberActivityType.Logout,
                operateUrl);
            //增加用户动态到后台 end ————

            FormsAuthentication.SignOut();

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

            return RedirectToAction("Index", "Home");
        }
        [ValidateInput(true)]
        public ActionResult ResetPassword(String back)
        {
            SetMetasVersion();

            if (back == null)
            {
                ViewData["back"] = "/Account/Login";
            }
            else
                ViewData["back"] = back;

            #region 构造页面数据对象
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel();
            #endregion

            return View(memberModel);
        }
        //无权限页面
        [Authorize]
        public ActionResult Restricted(string p)
        {

            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            #endregion
            string memberID = HttpContext.User.Identity.Name;
            ViewData["memberid"] = memberID;
            ViewData["activNo"] = Moooyo.BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(memberID, BiZ.Comm.UniqueNumber.IDType.MemberID).ConvertedID;

            //页码m
            int pageno = 1;
            if (!Int32.TryParse(p, out pageno)) pageno = 1;

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
            IList<BiZ.Sys.SystemMsg.SystemMsg> objs = BiZ.Sys.SystemMsg.SystemMsgProvider.GetMsgs(userid, pagesize, pageno);
            //页面数据对象
            Models.PageModels.SystemMsgsModel model = new Models.PageModels.SystemMsgsModel(
                    memberDisplayObj,
                    objs);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Floor((double)BiZ.Sys.SystemMsg.SystemMsgProvider.GetMsgCount(userid) / pagesize);
            model.Pagger.PageCount = pages + 1;
            model.Pagger.PageSize = pagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Account/Restricted";
            #endregion

            //被访问时数据处理
            BiZ.MemberManager.MemberManager.SetUnReadSystemMsgCountZero(userid);

            return View(model);
            // return View();
        }
        [ValidateInput(true)]
        public ActionResult useralertlogin(String email, String password, String verificationCode, String remember, String ReturnUrl, String fromPage)
        {
            if (email == null || email == "") return Content("<script type=\"text/javascript\">window.parent.location=\"/Error/Error?errorno=0\"; </script>");
            if (password == null || password == "") return Content("<script type=\"text/javascript\">window.parent.location=\"/Error/Error?errorno=0\"; </script>");
            if (remember == "") return Content("<script type=\"text/javascript\">window.parent.location=\"/Error/Error?errorno=0\"; </script>");

            email = email.ToLower(); //用户邮箱转换成小写，让登陆不区分大小写

            bool rememberflag1 = false;
            if (remember == "on")
                rememberflag1 = true;

            if (verificationCode != null)
            {
                string code = TempData["SecurityCode"] as string;//获取验证码
                if (!code.Equals(verificationCode, StringComparison.OrdinalIgnoreCase))//判断验证码是否正确
                {
                    if (fromPage == "login")
                    {
                        return Redirect("/Account/Login?op=errorcode");
                    }
                    else if (fromPage == "index")
                        return Redirect("/Home/Index?op=errorcode");
                    else
                        return Redirect("/Home/Index?op=errorcode");
                }
            }

            String uid1 = Moooyo.BiZ.MemberManager.MemberManager.MemberLogin(email, password, Request.UserHostAddress.ToString().Trim());
            BiZ.Member.Member mym = null;
            if (uid1 != "")
            {
                Session.Remove("loginerrorcount");//清除登录错误计数器

                String encryptionPassword = CBB.Security.MD5Helper.getMd5Hash(password);
                mym = MemberLoginStep2(email, encryptionPassword, uid1, rememberflag1);
                string operateUrl = "";
                if (mym.UniqueNumber != null)
                    operateUrl = "/u/" + mym.UniqueNumber.ConvertedID;
                else
                    operateUrl = "/Content/TaContent/" + mym.ID;
                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    mym.ID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.Login,
                    operateUrl);

                //if (mym.AllowLogin)
                //{
                //    if ((ReturnUrl != null) & (ReturnUrl != ""))
                //        return Content("<script type=\"text/javascript\">window.parent.location=\"" + ReturnUrl + "\"; </script>");
                //    else
                //        return Content("<script type=\"text/javascript\">window.parent.location=\"/Content/IFavorerContent\"; </script>");
                //}
                //else
                //{
                //    return Content("<script type=\"text/javascript\">window.parent.location=\"/Account/Welcome\"; </script>");
                //}

                string jumpUrl = GetConditionLoginJump(mym.ID, ReturnUrl);
                return Content("<script type=\"text/javascript\">window.parent.location=\"" + jumpUrl + "\"; </script>");
            }

            if (fromPage == "login")
            {
                return RedirectToAction("Login", "Account", new { op = "nousername" });
            }
            else if (fromPage == "index")
                return RedirectToAction("Index", "Home", new { op = "nousername" });
            else
                return RedirectToAction("Index", "Home", new { op = "nousername" });
        }
        [Authorize]
        public ActionResult Welcome(String ActivationCode)
        {
            SetMetasVersion();

            String userid = User.Identity.Name;
            if (userid == null || userid == "")
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }

            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(userid);
            if (member.AllowLogin)
                return RedirectToAction("IFavorerContent", "Content");

            ViewData["membercount"] = BiZ.MemberManager.MemberManager.GetAllMemberCount();

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = true;
            #endregion

            return View(memberModel);
        }
        //[HttpPost]
        //[ValidateInput(true)]
        //public ActionResult userlogin(String email, String password, String codestr, String remember, String ReturnUrl)
        //{
        //    if (email == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (password == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (remember == "") return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    if (codestr != null)
        //    {
        //        string code = TempData["SecurityCode"] as string;//获取验证码
        //        if (code.Equals(codestr, StringComparison.OrdinalIgnoreCase))//判断验证码是否正确
        //        {
        //            bool rememberflag = false;
        //            if (remember == "on")
        //                rememberflag = true;

        //            String uid = Moooyo.BiZ.MemberManager.MemberManager.MemberLogin(email, password, Request.UserHostAddress.ToString().Trim());

        //            if (uid != "")
        //            {
        //                Session.Remove("loginerrorcount");//清除登录错误计数器

        //                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(uid);
        //                Session["logintimes"] = mym.Status.LoginTimes.ToString();
        //                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
        //                (1,
        //                uid.ToString(),
        //                DateTime.Now,
        //                DateTime.Now.AddYears(1),
        //                rememberflag,
        //                "Member",
        //                FormsAuthentication.FormsCookiePath);
        //                string encTicket = FormsAuthentication.Encrypt(ticket);
        //                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        //                if ((ReturnUrl != null) & (ReturnUrl != "")) 
        //                    return Redirect(ReturnUrl);
        //                else
        //                    return Redirect("/InterestCenter/FeaturedInterestTopic");
        //            }
        //            return Redirect("/Account/Login?op=nousername");
        //        }
        //        else
        //        {
        //            return Redirect("/Account/Login?op=errorcode");
        //        }
        //    }
        //    else
        //    {
        //        bool rememberflag1 = false;
        //        if (remember == "on")
        //            rememberflag1 = true;

        //        String uid1 = Moooyo.BiZ.MemberManager.MemberManager.MemberLogin(email, password, Request.UserHostAddress.ToString().Trim());

        //        if (uid1 != "")
        //        {
        //            Session.Remove("loginerrorcount");//清楚登录错误计数器

        //            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(uid1);
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
        //            (1,
        //            uid1.ToString(),
        //            DateTime.Now,
        //            DateTime.Now.AddYears(1),
        //            rememberflag1,
        //            "Member",
        //            FormsAuthentication.FormsCookiePath);
        //            string encTicket = FormsAuthentication.Encrypt(ticket);
        //            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        //            if ((ReturnUrl != null) & (ReturnUrl != "")) 
        //                return Redirect(ReturnUrl);
        //            else
        //                return Redirect("/InterestCenter/FeaturedInterestTopic");
        //        }
        //        return Redirect("/Account/Login?op=nousername");
        //    }
        //}
        //public ActionResult Login(String op, String ReturnUrl)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    if (op == "nousername")
        //    {
        //        ViewData["errinfo"] = "用户名或密码错误。";
        //        if (Session["loginerrorcount"] == null)
        //            Session["loginerrorcount"] = "1";
        //        else
        //            Session["loginerrorcount"] = (int.Parse(Session["loginerrorcount"].ToString()) + 1).ToString();
        //    }
        //    if (op == "errorcode")
        //        ViewData["errinfo"] = "验证码错误。";
        //    if (ReturnUrl != null)
        //        ViewData["ReturnUrl"] = ReturnUrl;
        //    if ((HttpContext.User.Identity.Name != "") || (HttpContext.User.Identity.Name != string.Empty))
        //        return RedirectToAction("I", "Member");
        //    else
        //        return View();
        //}

        public ActionResult Login(String op, String ReturnUrl)
        {
            SetMetasVersion();

            //登录时验证记住用户名和密码，开始 ***********
            HttpCookie UserNameCookie = Request.Cookies["loginUserNameCookie"];
            HttpCookie UserPasswordCookie = Request.Cookies["loginUserPasswordCookie"];
            String userNameCookie, userPasswordCookie;
            if (UserNameCookie != null && UserPasswordCookie != null)
            {
                userNameCookie = UserNameCookie.Values["loginUserName"];

                userNameCookie = userNameCookie.ToLower(); //用户邮箱转换成小写，让登陆不区分大小写
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
        #endregion

        #region 数据与业务方法

        #region 验证码
        public ActionResult SecurityCode()
        {
            string oldcode = TempData["SecurityCode"] as string;
            string code = CreateRandomCode(5);
            TempData["SecurityCode"] = code;
            return File(CreateValidateGraphic(code), "image/GIF");
        }
        /// <summary>
        /// 获取验证码字符串
        /// </summary>
        /// <param name="codeCount">验证码长度</param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                int t = rand.Next(allCharArray.Length - 1);
                if (temp == t)
                    return CreateRandomCode(codeCount);
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 15.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion

        private String GetResetPasswordEmailContent(string newpwd)
        {
            String domainName = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Domain");

            String str = @"
                您在[米柚]的密码已经重设为：" + newpwd;
            str += "请点击 http://" + domainName + "/Account/Login 输入以上密码登录。";
            return str;
        }
        private String randomPassword(int count)
        {
            string pwd = "";
            Random r = new Random(DateTime.Now.Millisecond);
            while (pwd.Length < count)
            {
                pwd += r.Next(1000).ToString();
            }
            return pwd;
        }

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
            email = email.ToLower(); //用户邮箱转换成小写，让登陆不区分大小写
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
        [Authorize]
        public ActionResult CheckActivationCode(String ActivationCode)
        {
            String userid = User.Identity.Name;

            //String code = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ActivationCode");
            int code = 0;

            if (ActivationCode != null && ActivationCode != ""
                //&& ActivationCode.Length == 6
                )
            {
                int inviCode = 0;
                if (!int.TryParse(ActivationCode, out inviCode))
                    return Json(new JavaScriptSerializer().Serialize("error"));
                BiZ.Sys.InvitationCode.InvitationCode inviteCodeObj = BiZ.Sys.InvitationCode.InvitationCodeProvider.GetInviteCode(inviCode, BiZ.Comm.UsedFlag.No);

                if (inviteCodeObj != null)
                {
                    //设置已经使用
                    CBB.ExceptionHelper.OperationResult result = SystemFuncController.SetInviteCodeHasUsed(inviteCodeObj.InviteCode.ToString(), userid);

                    List<MongoDB.Bson.ObjectId> objids = new List<MongoDB.Bson.ObjectId>();
                    objids.Add(MongoDB.Bson.ObjectId.Parse(userid));
                    CBB.ExceptionHelper.OperationResult opr = BiZ.MemberManager.MemberManager.UpdateMemberAllowLogin(objids, true);

                    BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(userid);
                    //增加用户动态到后台
                    string operateUrl = "";
                    if (member.UniqueNumber != null)
                        operateUrl = "/u/" + member.UniqueNumber.ConvertedID;
                    else
                        operateUrl = "/Content/TaContent/" + member.ID;
                    BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                        member.ID,
                        "",
                        BiZ.Sys.MemberActivity.MemberActivityType.AllowLogin,
                        operateUrl
                        );

                    return Json(new JavaScriptSerializer().Serialize("ok"));
                }
                else
                {
                    return Json(new JavaScriptSerializer().Serialize("no"));
                }
            }
            else
            {
                return Json(new JavaScriptSerializer().Serialize("error"));
            }
        }

        [ValidateInput(true)]
        public ActionResult BackAndResetPassword(String email)
        {
            String returnInfo = "";
            if (email != null && email != "")
            {
                bool returnvalue = BiZ.MemberManager.MemberManager.GetIsEmailUsed(email);
                if (!returnvalue)
                {
                    String newpwd = randomPassword(6);
                    if (CBB.NetworkingHelper.EmailHelper.SendMail(email, "[米柚] 密码找回邮件", GetResetPasswordEmailContent(newpwd)))
                    {
                        BiZ.MemberManager.MemberManager.SetNewPwd(email, newpwd);
                        return Content("{\"status\":\"success\",\"information\":\"" + email + "\"}");
                    }
                    else
                    {
                        returnInfo = "密码重设邮件发送失败。";
                        return Content("{\"status\":\"failure\",\"information\":\"" + returnInfo + "\"}");
                    }
                }
                else
                {
                    returnInfo = "邮箱" + email + "不是有效的用户邮箱，请重新输入。";
                    return Content("{\"status\":\"failure\",\"information\":\"" + returnInfo + "\"}");
                }
            }
            else
                return Content("");
            
        }
        /// <summary>
        /// 条件登录跳转
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <returns>跳转地址</returns>
        internal static string GetConditionLoginJump(string memberId, string returnUrl)
        {
            bool notSelectedInterest = false;
            bool notActivated = false;
            bool hasBeenActivatedAndSelectedInterested = false;
            bool notActivatedAndSelectedInterest = false;
            long memberInterestCount = 0;
            string returnUrl2 = "";

            if (memberId != null)
            {
                BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(memberId);

                if (member != null)
                {
                    memberInterestCount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(memberId);

                    if (member.AllowLogin)
                    {

                        if (memberInterestCount < 1)
                        {
                            //未选兴趣
                            notSelectedInterest = true;
                        }
                        else
                        {
                            //已激活并且选择了兴趣
                            hasBeenActivatedAndSelectedInterested = true;
                            returnUrl2 = returnUrl;
                        }
                    }
                    else
                    {
                        if (memberInterestCount < 1)
                        {
                            //未激活并且未选择兴趣
                            notActivatedAndSelectedInterest = true;
                        }
                        else
                        {
                            //未激活
                            notActivated = true;
                        }
                    }
                }
            }
            string jumpUrl = "";
            if (hasBeenActivatedAndSelectedInterested)
            {
                if (returnUrl2 != "" && returnUrl2 != null)
                    jumpUrl = returnUrl2;
                else
                    jumpUrl = "/Content/IFavorerContent";
            }
            if (notSelectedInterest)
            {
                jumpUrl = "/Register/Regist?ifweibo=true";
            }
            if (notActivated)
            {
                jumpUrl = "/Account/Welcome";
            }
            if (notActivatedAndSelectedInterest)
            {
                jumpUrl = "/Register/Regist?ifweibo=true";
            }
            return jumpUrl;
        }
        #endregion
    }
}
