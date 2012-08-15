using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Moooyo.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        public ActionResult Login(String op, String ReturnUrl)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            if (op == "nousername")
                ViewData["errinfo"] = "用户名或密码错误，检查!";
            if (ReturnUrl != null)
                ViewData["ReturnUrl"] = ReturnUrl;

            return View();
        }
        [Authorize]
        public ActionResult CallAdmin(String id, int tp)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion

            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            ViewData["type"] = tp;
            ViewData["mid"] = id;
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult userCall()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult dic(String t)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            #endregion

            #region 系统应用图片上传
            string fileName;
            HttpFileCollectionBase files = Request.Files;
            if (files != null && files.Count > 0)
            {
                HttpPostedFileBase postedFile = files[0];
                fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                if (fileName != "")
                {
                    CBB.ImageHelper.ImageSizeType[] imagetypes = new CBB.ImageHelper.ImageSizeType[]{
                        new CBB.ImageHelper.ImageSizeType(360,320,false,false,"", CBB.ImageHelper.ImageMakeMode.W,null)
                    };

                    new CBB.ImageHelper.ImageUpload().AddImageToGridFS(fileName, postedFile.InputStream, imagetypes);
                    ViewData["imgpath"] = fileName;
                }
            }
            #endregion

            ViewData["t"] = t;
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult verify(String t)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            #endregion

            ViewData["t"] = t;
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult accountmanager()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            String userid = HttpContext.User.Identity.Name;

            #endregion
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult SystemManager()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            ViewData["managerId"] = HttpContext.User.Identity.Name;
            return View();
        }

        [Authorize(Roles = "Manager1")]
        public ActionResult checkphoto()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult photoaudit(String type)
        {
            SetMetasVersion();
            ViewData["type"] = type;

            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult newuser()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            #endregion

            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult MemberManager(String p, String province, String city, int sex, int beenband, int finishedreg, int hasphoto, String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            int pageno = 1;
            if (p != null)
                Int32.TryParse(p, out pageno);

            long membercount = BiZ.MemberManager.MemberManager.SearchMembersCount(beenband, finishedreg, hasphoto, province, city, sex, id);
            ViewData["membercount"] = membercount;
            int pages = (int)Math.Floor((double)membercount / 30);
            ViewData["pagecount"] = pages + 1;
            ViewData["pageno"] = pageno;
            ViewData["members"] = new JavaScriptSerializer().Serialize(BiZ.MemberManager.MemberManager.SearchMembers(beenband, finishedreg, hasphoto, province, city, sex, id, 30, pageno));

            return View();
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult MemberInfo(String type)
        {
            SetMetasVersion();

            String time1 = DateTime.Now.ToString("yyyy-MM-dd");
            String time2 = DateTime.Now.ToString("yyyy-MM-dd");
            time1 = Request.QueryString["time1"] != null ? Server.UrlDecode(Request.QueryString["time1"]) : time1;
            time2 = Request.QueryString["time2"] != null ? Server.UrlDecode(Request.QueryString["time2"]) : time2;
            List<long> newmembers = BiZ.Sys.SystemInfo.GetNewMember(time1, time2);
            List<long> newinterests = BiZ.Sys.SystemInfo.GetNewInterest(time1, time2);
            List<long> newwenwens = BiZ.Sys.SystemInfo.GetNewWenWen(time1, time2);
            List<long> newanswers = BiZ.Sys.SystemInfo.GetNewAnswer(time1, time2);
            //List<string> times = BiZ.Sys.SystemInfo.times;
            Models.PageModels.SystemInfoModels model = new Models.PageModels.SystemInfoModels(newmembers, newinterests, newwenwens, newanswers, null);
            model.time = time1 + "  " + time2;
            model.type = type;

            return View(model);
            //return View();
        }
        /// <summary>
        /// 活动
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult Actives(String type)
        {
            #region metas version
            SetMetasVersion();
            #endregion
            Models.PageModels.ActivesModel model = new Models.PageModels.ActivesModel(Active.ActiveReflectionFactory.GetActiviesForAll());

            ViewData["type"] = type;
            return View(model);
        }
        /// <summary>
        /// 推荐数据
        /// </summary>
        /// <param name="t">类别</param>
        /// <returns>视图</returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult RecommendedData(String t)
        {
            #region metas version
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            #endregion

            //#region 系统应用图片上传
            //string fileName;
            //HttpFileCollectionBase files = Request.Files;
            //if (files != null && files.Count > 0)
            //{
            //    HttpPostedFileBase postedFile = files[0];
            //    fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
            //    if (fileName != "")
            //    {
            //        CBB.ImageHelper.ImageSizeType[] imagetypes = new CBB.ImageHelper.ImageSizeType[]{
            //            new CBB.ImageHelper.ImageSizeType(360,320,false,false,"", CBB.ImageHelper.ImageMakeMode.W,null)
            //        };

            //        new CBB.ImageHelper.ImageUpload().AddImageToGridFS(fileName, postedFile.InputStream, imagetypes);
            //        ViewData["imgpath"] = fileName;
            //    }
            //}
            //#endregion

            if (t == "" || t == null) t = "FeaturedInterestTopic";
            ViewData["t"] = t;
            ViewData["pushImagePath"] = Request.QueryString["photo"];
            ViewData["contentId"] = Request.QueryString["ctd"];
            return View();
        }
        #endregion

        #region 数据与业务方法
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult userlogin(String name, String password, String ReturnUrl)
        {
            if (name == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (password == "") return RedirectToAction("Error", "Error", new { errorno = "0" });

            bool rememberflag = false;

            BiZ.Sys.SystemManager.SystemManager sysm = BiZ.Sys.SystemManager.SystemManagerProvider.Login(name, password);
            if (sysm != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                sysm.ID.ToString(),
                DateTime.Now,
                DateTime.Now.AddHours(1),
                rememberflag,
                "Manager" + sysm.Level.ToString(),
                FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                if ((ReturnUrl != null) & (ReturnUrl != "")) return Redirect(ReturnUrl);
                else
                    return RedirectToAction("dic", "Admin", new { t = "iwants" });
            }

            return RedirectToAction("login", "admin", new { op = "nousername" });
        }

        [Authorize(Roles = "Manager1")]
        public ActionResult GetCallCount(String type, String isaudited)
        {
            bool is_audited =false;
            if (isaudited.Equals("true"))
            {
                is_audited = true;
                
            }
            return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.MemberContent.MemberContentFactory.GetAllMemberContentCount(type, is_audited)));
        }

        [Authorize]
        public ActionResult connectAdmin(String mid, int type, String content)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (content == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            string userid = User.Identity.Name;

            CBB.ExceptionHelper.OperationResult result = BiZ.Sys.MemberContent.MemberContentFactory.AddMemberContent(type, mid, content, userid);

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                userid,
                mid,
                BiZ.Sys.MemberActivity.MemberActivityType.SubmitOpinion,
                "/Content/TaContent/" + userid);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        
        [Authorize(Roles = "Manager1")]
        public ActionResult setallowlogin(String mid, bool allowlogin)
        {
            return Json(BiZ.MemberManager.MemberManager.SetAllowLogin(mid, allowlogin));
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult setmembertype(String mid, int membertype)
        {
            return Json(BiZ.MemberManager.MemberManager.SetMemberType(mid, (BiZ.Member.MemberType)membertype));
        }

        #region 照片注册审核
        [Authorize(Roles = "Manager1")]
        public ActionResult getauditnewusers(int pagesize, int pageno)
        {
            return Json(BiZ.MemberManager.MemberManager.GetNewUsersToAudit(pagesize, pageno), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult setmembersauditresult(String mids, int auditresult, String marks, int score, int sex)
        {
            String[] midlist = mids.Split('|');
            foreach (string m in midlist)
            {
                if (m == "") continue;
                setauditresult(m, auditresult, marks, score, sex);
            }

            return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(true)));
        }

        private static void setauditresult(String mid, int auditresult, String marks, int score, int sex)
        {
            BiZ.MemberManager.MemberManager.SetAuditResult(mid, sex, auditresult);

            if (auditresult == -1)
            {
                return;
            }

            //String[] sysmarks = marks.Split('|');

            //foreach (string m in sysmarks)
            //{
            //    if (m == "") continue;

            //    BiZ.Member.Activity.ActivityController.SystemMarkMember(mid, m);
            //}

            //BiZ.Member.Activity.ActivityController.SystemScoreMember(mid, score);
        }
        #endregion

        #region 图片审核
        [Authorize(Roles = "Manager1")]
        public ActionResult getauditphotos(int photoType, String isaudited, int pagesize, int pageno)
        {
            if (isaudited == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int isauditedint = 0;
            int.TryParse(isaudited, out isauditedint);
            return Json(BiZ.Photo.PhotoManager.GetAuditPhotos((BiZ.Photo.PhotoType)photoType, isauditedint, pagesize, pageno), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult beautyphoto(String id, String auditresult)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (auditresult == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int auditresultint = 0;
            int.TryParse(auditresult, out auditresultint);

            if (auditresultint != 1)
            {
                String adminid = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SystemAdminID");
                BiZ.Photo.Photo ph = BiZ.Photo.PhotoManager.GetPhoto(id);

                if (ph != null)
                {

                    //发消息给用户
                    BiZ.Member.Activity.ActivityController.MsgToMember(adminid, ph.MemberID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PhotoIsNotPassAuditStr"), BiZ.Member.Activity.ActivityType.SystemAdminDelAction);
                    //删除照片
                    BiZ.Photo.PhotoManager.DeletePhoto(ph.MemberID, id);
                }
            }
            else
            {
                //审核通过
                BiZ.Photo.PhotoManager.SetAuditedResult(id, auditresultint);

                BiZ.Photo.Photo ph = BiZ.Photo.PhotoManager.GetPhoto(id);
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(ph.MemberID);
                mym.Au = 2;
                BiZ.MemberManager.MemberManager.SaveMember(mym);
            }

            return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(true)));
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult auditphoto(String id, String auditresult, string pictureType)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (auditresult == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pictureType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int auditresultint = 0;
            int.TryParse(auditresult, out auditresultint);

            auditphoto(id, auditresultint, pictureType);

            return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(true)));
        }
        [Authorize(Roles = "Manager1")]
        private static void auditphoto(String id, int auditresultint, string pictureType)
        {
            if (auditresultint != 1)
            {
                BiZ.Photo.Photo p = BiZ.Photo.PhotoManager.GetPhoto(id);

                if (p != null)
                {
                    string msg = "";
                    if (pictureType == "1")
                    {
                        msg = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberAvatarIsNotPassAuditStr");                  
                        //如果视频认证为通过则改为不通过
                        MemberController.SetPhotoIdentNotPass(p.MemberID);
                    }
                    else
                    {
                        msg = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PhotoIsNotPassAuditStr");
                    }
                    //屏蔽照片
                    BiZ.Photo.PhotoManager.BindedPhoto(id);
                    //发消息给用户
                    BiZ.Member.Activity.ActivityController.SystemMsgToMember(p.MemberID, msg);
                }
            }
            else
            {
                //审核通过
                BiZ.Photo.PhotoManager.SetAuditedResult(id, auditresultint);
            }
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult auditphotos(String ids, String auditresult, string pictureType)
        {
            if (ids == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (auditresult == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pictureType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int auditresultint = 0;
            int.TryParse(auditresult, out auditresultint);
            Array idlist = ids.Split(',');
            foreach (String id in idlist)
            {
                if (id != "")
                    auditphoto(id, auditresultint, pictureType);
            }

            return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(true)));
        }
        #endregion

        #region 举报和建议查询
        [Authorize(Roles = "Manager1")]
        public ActionResult getSystemCall(String type, String isAudited, int pagesize, int pageno)
        {
            bool isaudit = false;
            if (isAudited == "true") isaudit = true;
            IList<BiZ.Sys.MemberContent.MemberContent> list = BiZ.Sys.MemberContent.MemberContentFactory.GetMemberContents(type, isaudit, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region 获取用户短编号
        [Authorize(Roles = "Manager1")]
        public ActionResult getUserMin_Id(String memberid)
        {
            string id="";
            BiZ.Comm.UniqueNumber.UniqueNumber ti = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(memberid, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if(ti != null)
            {
                id = ti.ConvertedID.ToString();
            }
            return Json(new JavaScriptSerializer().Serialize(id), JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region 处理举报和建议
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">创建人id</param>
        /// <param name="result">处理结果</param>
        /// <param name="resulttxt">消息结果</param>
        /// <param name="temprs">原始内容</param>
        /// <returns>异常布尔值</returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult setSystemCall(String id,string uid, string result, string resulttxt, string originaltext)
        {
            CBB.ExceptionHelper.OperationResult or = BiZ.Sys.MemberContent.MemberContentFactory.UpdateMemberContent(id, true, result);
            if (or.ok)
            {
                if (!result.Trim().Equals("已采纳"))
                {
                    string content = "";
                    if (resulttxt.Trim().Length <= 0)
                    {
                        content = "亲爱的米柚会员，非常抱歉，您的吐槽我们没有能够采纳.<br/><span style=\"font-weight:bold;\">吐槽内容：</span><br/>" + originaltext;
                    }
                    else
                    {
                        content = "亲爱的米柚会员，非常抱歉，您的吐槽我们没有能够采纳.<br/><span style=\"font-weight:bold;\">米柚回复：<br/></span>" + resulttxt + "<br/><span style=\"font-weight:bold;\">吐槽内容：</span><br/>" + originaltext;
                    }

                    BiZ.Member.Activity.ActivityController.SystemMsgToMember(uid, content);
                }
                else
                {
                    string content = "";
                    if (resulttxt.Trim().Length <= 0)
                    {
                        content = "恭喜，您的吐槽米柚网已经采纳了！" + "<br/><span style=\"font-weight:bold;\">吐槽内容：</span><br/>" + originaltext;
                    }
                    else
                    {
                        content = "恭喜，您的吐槽已被采纳.<br/><span style=\"font-weight:bold;\">米柚回复：</span><br/>" + resulttxt + "<br/><span style=\"font-weight:bold;\">吐槽内容：</span><br/>" + originaltext;
                    }

                    BiZ.Member.Activity.ActivityController.SystemMsgToMember(uid, content);
                }
            }
            return Json(new JavaScriptSerializer().Serialize(or));
        }
        #endregion 

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult AddActives(String dllname, String spacename, String functionname, String type, Boolean enable)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion
            Active.ActiveReflectionFactory.AddActive(dllname, spacename, functionname, type, enable);
            Models.PageModels.ActivesModel model = new Models.PageModels.ActivesModel(Active.ActiveReflectionFactory.GetActiviesForAll());
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 删除活动
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult DelActives(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion
            Active.ActiveReflectionFactory.DelActive(id);
            Models.PageModels.ActivesModel model = new Models.PageModels.ActivesModel(Active.ActiveReflectionFactory.GetActiviesForAll());
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 开启或关闭活动
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        public ActionResult UpActives(String id, Boolean enable)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion
            Active.ActiveReflectionFactory.CloseOrOpenActive(id, enable);
            Models.PageModels.ActivesModel model = new Models.PageModels.ActivesModel(Active.ActiveReflectionFactory.GetActiviesForAll());
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        #endregion
    }
}
