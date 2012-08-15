using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        public ActionResult Become()
        {
            #region metas version
            SetMetasVersion();
            #endregion
            ViewData["photoType"] = Request.QueryString["pt"];
            return View();
        }
        public ActionResult UploadFace(string pt)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            String path = HttpContext.Request.PhysicalApplicationPath + "images\\";

            #region 参数检查
            if (pt == "")
                pt = Request.QueryString["pt"];
            if ((pt == null) || (pt == "-1")) pt = "99";
            int photoTypeNum = 0;
            if (!Int32.TryParse(pt, out photoTypeNum)) return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            //if ((photoTypeNum < 1) || (photoTypeNum > 2)) return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            #endregion

            ViewData["photoType"] = pt;
            //String mid = HttpContext.User.Identity.Name;
            //ViewData["mid"] = mid;
            return View();
        }
        //public ActionResult Single()
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion
        //    return View();
        //}

        public ActionResult Regist(String err, String ifweibo)
        {
            SetMetasVersion();

            ViewData["err"] = err == null ? "" : err;
            if (ifweibo != null && ifweibo == "true")
            {
                ViewData["ifweibo"] = ifweibo.ToString();
                //Session.Remove("ifweibo");
            }

            return View();
        }
        [ValidateInput(true)]
        public ActionResult VerifyEmail(String email, String back)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetNotActivationEmailMember(email);
            if (mym != null)
            {
                bool sendState = CBB.NetworkingHelper.EmailHelper.SendMail(email, "[米柚]注册验证邮件", Common.Comm.GetRegisterEmailContent(email, mym.Password));
                if (!sendState)
                    return RedirectToAction("Error", "Error", new { errorno = "3" });
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "3" });
            }

            ViewData["email"] = email;
            ViewData["emailurl"] = "mail." + email.Split('@')[1];
            ViewData["back"] = back;

            #region 构造页面数据对象
            String memberId = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberId);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberId;
            memberModel.MemberID = memberId;
            memberModel.AlreadyLogon = true;
            #endregion

            return View(memberModel);
        }
        public ActionResult RegStep2(String PreID)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            #endregion

            ViewData["preID"] = PreID;

            return View();
        }
        [Authorize]
        public ActionResult RegAddInterest()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            #endregion
            //该session用于豆瓣登录选择性别
            Session.Remove("isDoubanLogin");

            string id = User.Identity.Name;

            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(id);
            string memberAllowLogin = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberAllowLogin");
            member.AllowLogin = Convert.ToBoolean(memberAllowLogin);
            BiZ.MemberManager.MemberManager.SaveMember(member);

            long memberInterestCount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(member.ID);
            if (memberInterestCount >= 3)
                return RedirectToAction("IFavorerContent", "Content");

            IList<BiZ.InterestCenter.Interest> interestList = GetRegRecommendInterest(1);
            Models.PageModels.InterestForRegModel model = new Models.PageModels.InterestForRegModel(interestList);
            model.UserID = id;
            model.MemberID = id;
            model.AlreadyLogon = true;
            return View(model);
        }
        public ActionResult UploadPhoto(String pid, String type)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(pid);
            if (mym == null) return View();

            ViewData["iconpath"] = mym.MemberInfomation.IconPath;
            ViewData["mid"] = mym.ID;

            if (type != null)
                ViewData["type"] = type;
            else
                ViewData["type"] = "";

            return View();
        }
        public ActionResult ShowCove(String id, String type)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);
            if (mym == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            ViewData["nickname"] = mym.MemberInfomation.NickName;
            ViewData["iconpath"] = mym.MemberInfomation.IconPath.Split('.')[0] + "_p" + ".jpg";
            ViewData["mid"] = mym.ID;
            //ViewData["scoreavg"] = mym.Status.ScoreAvg.ToString("#0.0");
            ViewData["type"] = (type == null) ? "" : type;
            //ViewData["markdata"] = new JavaScriptSerializer().Serialize(mym.MemberRelations.Marks);

            return View();
        }
        public ActionResult FinishReg(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            ViewData["id"] = id;

            ViewData["markers"] = Models.DisplayObjProvider.getVisitorListJson(id, 10, 1);

            return View();
        }
        [ValidateInput(true)]
        public ActionResult FinishReg2(String id, String email, String sex)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            ViewData["id"] = id;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);
            if (mym == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mym.FinishedReg) return RedirectToAction("Error", "Error", new { errNo = "用户已完成注册。" });

            //检查Email地址
            if (!CBB.CheckHelper.StringCheckTools.IsEmail(email))
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!BiZ.MemberManager.MemberManager.GetIsEmailUsed(email))
                return RedirectToAction("Error", "Error", new { errNo = "Email已经被使用。" });
            int sexint = 0;
            if (!Int32.TryParse(sex, out sexint))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.MemberManager.MemberManager.SetEmailAndSex(id, email, sexint);

            return View();
        }
        [Authorize]
        public ActionResult Greeting()
        {
            SetMetasVersion();

            if (Session["isDoubanLogin"] != null)
            {
                bool isDoubanLogin = Convert.ToBoolean(Session["isDoubanLogin"]);
                if (isDoubanLogin) ViewData["IsDoubanLogin"] = true;
            }

            return View();
        }
        [Authorize]
        public ActionResult UploadAvatar(string pt)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            String path = HttpContext.Request.PhysicalApplicationPath + "images\\";

            #region 参数检查
            if (pt == "")
                pt = Request.QueryString["pt"];
            if ((pt == null) || (pt == "-1") || (pt == "")) pt = "1";
            int photoTypeNum = 0;
            if (!Int32.TryParse(pt, out photoTypeNum)) return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            #endregion
            Session["shareUserIconImage"] = "true";
            ViewData["photoType"] = pt;
            return View();
        }
        [Authorize]
        public ActionResult UploadFinish()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            #endregion
            string memberID = User.Identity.Name;
            String imagepath = "";
            if (Session["shareWeiBoImage"] != null)
            {
                Session.Remove("shareWeiBoImage");
            }
            imagepath = Models.DisplayObjProvider.getUserIconToShare(memberID);
            Session["shareWeiBoImage"] = Server.MapPath(imagepath);
            ViewData["imagepath"] = imagepath;

            //获取兴趣集合
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberID, 0, 0);
            IList<BiZ.InterestCenter.Interest> interestList = new List<BiZ.InterestCenter.Interest>();
            String interesttitles = "";
            //最多只能有三个兴趣
            foreach (var obj in interests)
            {
                if (interestList.Count < 3)
                {
                    interestList.Add(obj);
                    interesttitles += obj.Title + ",";
                }
                else
                    break;
            }
            ViewData["interestTitles"] = interesttitles.Substring(0, interesttitles.Length - 1);

            Models.PageModels.MemberPageModel model = new Models.PageModels.MemberPageModel();
            model.UserID = memberID;
            model.MemberID = memberID;
            model.AlreadyLogon = true;
            if (model.User.BindedPlatforms == null || model.User.BindedPlatforms.Trim() == "")
            {
                if (Session["shareWeiBoImage"] != null)
                {
                    Session.Remove("shareWeiBoImage");
                }
            }
            return View(model);
        }
        #endregion

        #region 数据与业务方法
        /// <summary>
        /// 检查用户昵称是否包含关键字
        /// </summary>
        /// <returns>json对象</returns>
        public ActionResult CheckUserNick(string nickname)
        {
            if (nickname == null || nickname == "") return RedirectToAction("Error", "Error", new { errorno = "0" });

            string tempname = nickname;
            IList<string> result_list = null;
            result_list = new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(ref tempname, CBB.CheckHelper.FilterWord.word_type.delete);
            if (null != result_list && result_list.Count > 0) return Json(new JavaScriptSerializer().Serialize(false), JsonRequestBehavior.AllowGet);
            result_list = new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(ref tempname, CBB.CheckHelper.FilterWord.word_type.shift);
            if (null != result_list && result_list.Count > 0) return Json(new JavaScriptSerializer().Serialize(false), JsonRequestBehavior.AllowGet);
            result_list = new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(ref tempname, CBB.CheckHelper.FilterWord.word_type.trial);
            if (null != result_list && result_list.Count > 0) return Json(new JavaScriptSerializer().Serialize(false), JsonRequestBehavior.AllowGet);
            return Json(new JavaScriptSerializer().Serialize(true), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegFinishedByPhoto(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            #endregion

            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member newbie = BiZ.MemberManager.MemberManager.GetMember(id);
            if (newbie.FinishedReg) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String uid = id;

            if (uid != "")
            {
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(uid);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                uid.ToString(),
                DateTime.Now,
                DateTime.Now.AddYears(1),
                false,
                "Member",
                FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
            else
            {
                return RedirectToAction("login", "account", new { op = "nousername" });
            }

            return RedirectToAction("I", "Member", new { t = "reg", id = id });
        }
        public ActionResult RegGetInterestForPage(String classid, int nowpageno)
        {
            Models.PageModels.InterestForRegModel model = RegGetInterest(classid, nowpageno);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        public Models.PageModels.InterestForRegModel RegGetInterest(String classid, int nowpageno)
        {
            string userid = User.Identity.Name;
            int interestscount = 0;
            if (classid != null && classid != "")
            {
                interestscount = BiZ.InterestCenter.InterestFactory.GetClassInterest(classid, 0, 0).Count;
            }
            else
            {
                interestscount = BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            }
            int pagesize = 8;
            int pagecount = interestscount % pagesize == 0 ? (int)(interestscount / pagesize) : (int)(interestscount / pagesize) + 1;
            int pageindex = nowpageno < pagecount ? nowpageno + 1 : 1;
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            if (classid != null && classid != "")
            {
                interests = BiZ.InterestCenter.InterestFactory.GetClassInterest(classid, pagesize, pageindex);
            }
            else
            {
                interests = BiZ.InterestCenter.InterestFactory.GetInterest("", pagesize, pageindex);
            }
            IList<BiZ.InterestCenter.Interest> interestformember = new List<BiZ.InterestCenter.Interest>();
            IList<BiZ.InterestCenter.Interest> endinterest = new List<BiZ.InterestCenter.Interest>();
            interestformember = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            foreach (var obj in interests)
            {
                bool ifinsert = true;
                foreach (var objtwo in interestformember)
                {
                    if (objtwo.ID == obj.ID) { ifinsert = false; break; }
                    else { continue; }
                }
                if (ifinsert) { endinterest.Add(obj); }
            }
            IList<BiZ.InterestCenter.InterestClass> interestclass = BiZ.InterestCenter.InterestFactory.GetAllInterestClass();
            Models.PageModels.InterestForRegModel model = new Models.PageModels.InterestForRegModel(endinterest, pageindex, interestclass);
            return model;
        }
        [ValidateInput(true)]
        public ActionResult RegGetInterestForLike(String content)
        {
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetTitleInterest(content, 0, 0);
            Models.PageModels.InterestForRegModel model = new Models.PageModels.InterestForRegModel(interests, 0, null);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        [HttpGet]
        [ValidateInput(true)]
        public ActionResult RegFinished(String Email, String Pwd, String t)
        {
            String uid = Moooyo.BiZ.MemberManager.MemberManager.MemberLogin(Email, Pwd, Request.UserHostAddress.ToString().Trim());
            BiZ.Member.Member mym;
            ViewData["me"] = uid;

            if (uid != "")
            {
                mym = BiZ.MemberManager.MemberManager.GetMember(uid);
                if (mym != null)
                {
                    Session["logintimes"] = mym.Status.LoginTimes.ToString();
                }
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                uid.ToString(),
                DateTime.Now,
                DateTime.Now.AddYears(1),
                false,
                "Member",
                FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
            else
            {
                return RedirectToAction("Login", "Account", new { op = "nousername" });
            }

            //return RedirectToAction("UploadFace", "Register", new { pt = t });
            return RedirectToAction("Greeting", "Register", new { pt = t });
        }
        [ValidateInput(true)]
        public ActionResult IsEmailUsed(String email)
        {
            //检查Email地址
            if (!CBB.CheckHelper.StringCheckTools.IsEmail(email))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            bool returnvalue = BiZ.MemberManager.MemberManager.GetIsEmailUsed(email);

            return Json(returnvalue, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(true)]
        public ActionResult FinishReg3(String id, String pwd, String nickName, String year, String month, String day, String province, String city, String inviterID)
        {
            String birthday = year.ToString().Trim() + "/" + month.ToString().Trim() + "/" + day.ToString().Trim();

            DateTime bd = new DateTime();
            if (!DateTime.TryParse(birthday, out bd))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetPreMemberByID(id);

            if (mym != null)
            {
                //发消息给用户
                String adminid = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SystemAdminID");

                BiZ.MemberManager.MemberManager.SetBaseInfo(mym.ID, pwd, nickName, bd, province + "|" + city, true);
                //发消息给用户
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(inviterID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RegisterFinished"));

                if (inviterID != "")
                {
                    CBB.ExceptionHelper.OperationResult result = BiZ.Member.Activity.ActivityController.RegInviterMember(inviterID, mym.ID);
                    if (result.ok)
                    {
                        //发消息给用户
                        BiZ.Member.Activity.ActivityController.SystemMsgToMember(inviterID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UpgradeToLevel1OK"));
                    }
                }
                return Json(new JavaScriptSerializer().Serialize(1));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(-1));

        }
        [HttpPost]
        public ActionResult RegByPhotoStep1()
        {
            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.AddMember("", "", "", 0);
            if (mym == null) return Json(new int[] { -1 });

            BiZ.Photo.Photo myp = new BiZ.Photo.Photo();
            if (Request.Files != null && Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string fname = file.FileName;
                    string ext = fname.Substring(fname.LastIndexOf('.') + 1);
                    string month = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                    Random ran = new Random(file.ContentLength);
                    string nFilename = month + "_" + CBB.Security.MD5Helper.getMd5Hash(fname + mym.ID + DateTime.Now.ToString()) + "." + ext;
                    myp = Moooyo.BiZ.Photo.PhotoManager.AddPhotoOrgin(mym.ID, BiZ.Photo.PhotoType.RegisterPhoto, ext, fname, ext, nFilename);

                    myp.FileName = nFilename;

                    CBB.ImageHelper.ImageWaterMark wm = new CBB.ImageHelper.ImageWaterMark();
                    if (CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrOn") == "true")
                    {
                        wm.WaterStrOn = true;
                        wm.WaterStrMarginRight = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginRight") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginRight"));
                        wm.WaterStrMarginBottom = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginBottom") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginBottom"));
                        wm.WaterStrFontSize = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrFontSize");
                        wm.WaterStrFont = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrFont");
                        wm.WaterStr = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStr");
                    }
                    if (CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicOn") == "true")
                    {
                        wm.WaterPicOn = true;
                        wm.WaterPicMarginRight = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginRight") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginRight"));
                        wm.WaterPicMarginBottom = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginBottom") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginBottom"));
                        wm.WaterPicPath = Server.MapPath(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrFontSize"));
                    }

                    CBB.ImageHelper.ImageSizeType[] imagetypes = new CBB.ImageHelper.ImageSizeType[]{
                        new CBB.ImageHelper.ImageSizeType(600,600,false,false,"", CBB.ImageHelper.ImageMakeMode.W,wm),
                        new CBB.ImageHelper.ImageSizeType(240,240,false,false,"_s", CBB.ImageHelper.ImageMakeMode.W,null),
                        new CBB.ImageHelper.ImageSizeType(110,110,false,false,"_p", CBB.ImageHelper.ImageMakeMode.Cut,null),
                        new CBB.ImageHelper.ImageSizeType(50,50,false,false,"_i", CBB.ImageHelper.ImageMakeMode.Cut,null)
                    };

                    new CBB.ImageHelper.ImageUpload().AddImageToGridFS(nFilename, file.InputStream, imagetypes);
                }
            }

            mym.MemberPhoto.IconID = myp.ID;
            mym.MemberInfomation.IconPath = myp.FileName;
            mym.Status.PhotoCount = 1;
            BiZ.MemberManager.MemberManager.SaveMember(mym);

            return Json(mym);
        }
        [HttpPost]
        public ActionResult GetAuditingMember(String mid)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetAuditingMember(mid);
            if (mym == null) return Json(new int[] { -1 });

            return Json(new JavaScriptSerializer().Serialize(mym));
        }
        //[HttpPost]
        //[ValidateInput(true)]
        //public ActionResult ProcRegStep1(String email, String sex)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    email = email.ToLower();

        //    //检查Email地址
        //    if (!CBB.CheckHelper.StringCheckTools.IsEmail(email))
        //        return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    //判断email是否已完成注册
        //    if (!BiZ.MemberManager.MemberManager.GetIsEmailUsed(email)) return RedirectToAction("Error", "Error", new { errNo = "Email已完成注册。" });
        //    //判断email是否有未完成的注册
        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetPreMemberByEmail(email);
        //    if (mym == null)
        //    {
        //        string pwd = DateTime.Now.ToString() + "moooyo";
        //        int sexint = 0;
        //        if (!Int32.TryParse(sex, out sexint))
        //            return RedirectToAction("Error", "Error", new { errorno = "0" });

        //        //如果返回预注册ID为空，则预注册保存失败。
        //        mym = BiZ.MemberManager.MemberManager.AddNewPreMemberByEmailRegister(email, sexint, pwd);
        //        if (mym == null)
        //            return RedirectToAction("Error", "Error", new { errNo = "注册保存失败。" });
        //    }
        //    //判断是否需要验证邮箱
        //    bool EmailNeedVerify = true;
        //    bool.TryParse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailNeedVerify"), out EmailNeedVerify);
        //    if (!EmailNeedVerify)
        //    {
        //        return RedirectToAction("RegStep2", "Register", new { PreID = mym.ID });
        //    }
        //    else
        //    {
        //        //if (!CBB.NetworkingHelper.EmailHelper.SendMail(email, "[米柚]注册验证邮件", Common.Comm.GetRegisterEmailContent(email, mym.Password)))
        //        //   return RedirectToAction("Error", "Error", new { errNo = "邮件发送失败。" });

        //        return RedirectToAction("VerifyEmail", "Register", new { email = email });
        //    }
        //}
        [ValidateInput(true)]
        public ActionResult RegStep2ActiveEmail(String email, String Pwd)
        {
            if (email == null || Pwd == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member pre = BiZ.MemberManager.MemberManager.GetNotActivationEmailMember(email);
            if (pre != null)
            {
                //更新邮件验证状态
                bool result = BiZ.MemberManager.MemberManager.UpdateEmailIsVaild(email, Pwd);
                if (!result)
                    return RedirectToAction("Error", "Error", new { errorno = "2" });

                return RedirectToAction("IFavorerContent", "Content", new { preID = pre.ID });
            }
            else
                return RedirectToAction("Error", "Error", new { errorno = "2" });
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult ProcRegStep2(
            String email,
            //String preID, 
            String pwd,
            String nickName,
            String year,
            String month,
            String day,
            String sex,
            //String province, 
            //String city, 
            String inviterID,
            String t)
        {
            email = email.ToLower();
            String preID = "";

            //检查Email地址
            if (!CBB.CheckHelper.StringCheckTools.IsEmail(email))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            //判断email是否已完成注册
            if (!BiZ.MemberManager.MemberManager.GetIsEmailUsed(email)) return RedirectToAction("Error", "Error", new { errNo = "Email已完成注册。" });
            //判断email是否有未完成的注册
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetPreMemberByEmail(email);
            if (mym == null)
            {
                string pwd2 = DateTime.Now.ToString() + "moooyo";
                int sexint = 0;
                if (!Int32.TryParse(sex, out sexint))
                    return RedirectToAction("Error", "Error", new { errorno = "0" });

                //如果返回预注册ID为空，则预注册保存失败。
                mym = BiZ.MemberManager.MemberManager.AddNewPreMemberByEmailRegister(email, sexint, pwd2);
                if (mym == null)
                    return RedirectToAction("Error", "Error", new { errNo = "注册保存失败。" });
            }
            //判断是否需要验证邮箱
            bool EmailNeedVerify = true;
            bool.TryParse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailNeedVerify"), out EmailNeedVerify);
            if (!EmailNeedVerify)
            {
                //return RedirectToAction("RegStep2", "Register", new { PreID = mym.ID });
                preID = mym.ID;
            }
            else
            {
                //if (!CBB.NetworkingHelper.EmailHelper.SendMail(email, "[米柚]注册验证邮件", Common.Comm.GetRegisterEmailContent(email, mym.Password)))
                //   return RedirectToAction("Error", "Error", new { errNo = "邮件发送失败。" });

                return RedirectToAction("VerifyEmail", "Register", new { email = email });
            }


            String birthday = year.ToString().Trim() + "/" + month.ToString().Trim() + "/" + day.ToString().Trim();

            DateTime bd = new DateTime();
            if (!DateTime.TryParse(birthday, out bd))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member mym2 = BiZ.MemberManager.MemberManager.GetPreMemberByID(preID);

            if (mym2 != null)
            {
                //BiZ.MemberManager.MemberManager.SetBaseInfo(mym2.ID, pwd, nickName, bd, province + "|" + city, true);
                BiZ.MemberManager.MemberManager.SetBaseInfo(mym2.ID, pwd, nickName, bd, "", true);

                //发消息给用户
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(mym2.ID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RegisterFinished"));
                //内测邀请
                if (null != Session["Inviter"] && Session["Inviter"].ToString().Trim().Length > 0)
                {
                    CBB.ExceptionHelper.OperationResult result = Moooyo.BiZ.Member.Relation.RelationProvider.AddRegInviter(Session["Inviter"].ToString(), mym2.ID);
                    if (result.ok)
                    {
                        int Invite_count = Moooyo.BiZ.MemberManager.MemberManager.GetRegInviterMember(Session["Inviter"].ToString()).Count;
                        string InviterName = Moooyo.BiZ.MemberManager.MemberManager.GetMemberInfomation(Session["Inviter"].ToString()).NickName;
                        //发消息给邀请者
                        BiZ.Member.Activity.ActivityController.SystemMsgToMember(Session["Inviter"].ToString(), CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InvertMemberToRegSuccess"));
                        //发消息给被邀请者
                        if (Invite_count < 3)
                        {
                            BiZ.Member.Activity.ActivityController.SystemMsgToMember(mym2.ID, "恭喜，您已经接受准会员“" + InviterName + "”的邀请注册成功！该邀请团目前共有" + (Invite_count + 1) + "人成功加入！加入人数达到4人，" + InviterName + "的邀请团所有成员的账号都将被激活！");
                        }
                        else
                        {
                            BiZ.Member.Activity.ActivityController.SystemMsgToMember(mym2.ID, "恭喜，您已经接受准会员“" + InviterName + "”的邀请注册成功！该邀请团已经满足激活条件，管理员将在12小时内审核完毕！");
                        }
                    }
                }
                //邀请注册升级
                //if (inviterID != "")
                //{
                //    CBB.ExceptionHelper.OperationResult result = BiZ.Member.Activity.ActivityController.RegInviterMember(inviterID, mym.ID);

                //    if (result.ok)
                //    {
                //        //发消息给用户
                //        BiZ.Member.Activity.ActivityController.SystemMsgToMember(inviterID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UpgradeToLevel1OK"));
                //    }
                //}

                //增加用户动态到后台
                string operateUrl = "";
                if (mym.UniqueNumber != null)
                    operateUrl = "/u/" + mym.UniqueNumber.ConvertedID;
                else
                    operateUrl = "/Content/TaContent/" + mym.ID;
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    mym.ID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.Register,
                    operateUrl
                    );

                return RedirectToAction("RegFinished", "Register", new { Email = mym2.Email, Pwd = pwd, t = t });
            }
            else
                return RedirectToAction("Error", "Error", new { errNo = "注册失败。" });
        }
        [HttpPost]
        public ActionResult GetRegRecomInterest(String pageNo)
        {
            if (pageNo == null || pageNo == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            int pgeNo = 1;
            if (!Int32.TryParse(pageNo, out pgeNo)) pgeNo = 1;
            IList<BiZ.InterestCenter.Interest> interestList = GetRegRecommendInterest(pgeNo);
            return Json(new JavaScriptSerializer().Serialize(interestList));
        }
        private IList<BiZ.InterestCenter.Interest> GetRegRecommendInterest(int pageNo)
        {
            String interestTitles = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RegRecomInterest");
            String[] arrGroupInterests = interestTitles.Split('|');
            List<String> listGroupInterests = new List<String>();
            listGroupInterests.AddRange(arrGroupInterests[pageNo - 1].Split(','));

            //随机排序数组
            List<string> listGroupIntes = Common.Comm.ArrayRandomSort(listGroupInterests.ToArray()).ToList();

            //分页
            //pageNo = (pageNo == 0) ? 1 : pageNo;
            int pageSize = 8;
            listGroupIntes = listGroupIntes.Take(pageSize).ToList();

            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetInterest(listGroupIntes.ToArray(), 8, 1);
            return interestList;
        }
        public ActionResult SelectSex(String sex)
        {
            if (sex == null || sex == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            byte numSex = 1;
            if (!byte.TryParse(sex, out numSex))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            string memberId = HttpContext.User.Identity.Name;
            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(memberId);
            member.Sex = numSex;
            BiZ.MemberManager.MemberManager.SaveMember(member);
            CBB.ExceptionHelper.OperationResult result = new CBB.ExceptionHelper.OperationResult(true);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        #endregion

    }
}
