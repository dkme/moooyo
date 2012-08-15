using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using System.IO;

using MongoDB.Driver;
using CBB.MongoDB;
using Moooyo.BiZ.FilterText;
using MongoDB.Driver.Builders;

namespace Moooyo.WebUI.Controllers
{
    public class SystemFuncController : Controller
    {
        #region 视图方法
        #endregion

        #region 数据与业务方法
        /* mongodb导入数据
        public ActionResult importp()
        {
            IList<BiZ.Sys.Zone.Zone> zlist = BiZ.Sys.Zone.ZoneFactory.GetAllZone();

            foreach (BiZ.Sys.Zone.Zone z in zlist)
            {
                BsonDocument zo = new BsonDocument 
                {
                  { "Province", z.Province },
                  { "City", z.City },
                  { "Area" ,z.Area },
                  { "CenterLat" ,z.CenterLat },
                  { "CenterLng" ,z.CenterLng }

                };

                CBB.MongoDB.MongoDBHelper.Collection("zones").Insert(zo);
            };

            return Json(1, JsonRequestBehavior.AllowGet);
        }
         */
        [HttpGet]
        [Authorize]
        public ActionResult GetProvince()
        {
            IList<String> zlist = BiZ.Sys.ZoneFactory.GetProvinces();

            return Json(zlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetCitysByProvinceName(String prov)
        {
            IList<String> zlist = BiZ.Sys.ZoneFactory.GetCitysByProvinceName(prov);

            return Json(zlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetCityByName(String city)
        {
            BiZ.Sys.Zone z = BiZ.Sys.ZoneFactory.GetCityByName(city);

            return Json(z, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetSystemMarks(String sex)
        //{
        //    IList<BiZ.Mark.SystemMark> objs = BiZ.Mark.MarkProvider.GetSystemMarks(Int32.Parse(sex));
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    String infophotojson = ser.Serialize(objs);
        //    return Json(infophotojson, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetSystemHi(String type)
        //{
        //    int hitype = 11;
        //    int.TryParse(type, out hitype);
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.HiFactory.GetSystemHi((BiZ.Sys.HiType)hitype, 5)));
        //}
        //public ActionResult GetAllSystemHi()
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.HiFactory.GetAllHi()));
        //}
        //public ActionResult AddSystemHi(String type, String isaudited, String witter, String comment)
        //{
        //    bool audited = false;
        //    bool.TryParse(isaudited, out audited);
        //    int hitype = 1;
        //    int.TryParse(type, out hitype);
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.HiFactory.AddHi(hitype, audited, witter, comment)), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult DelSystemHi(String id)
        //{
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.HiFactory.DelHi(id)), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [Authorize]
        public ActionResult GetSystemInterView(String type, String alreadyanswered)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            String[] objs = ser.Deserialize<String[]>(alreadyanswered);

            return Json(ser.Serialize(GetSystemInterViewList(type, objs)));
        }
        [Authorize]
        public IList<BiZ.Sys.SystemInterView> GetSystemInterViewList(String type, String[] alreadyanswered)
        {
            int hitype = 11;
            int.TryParse(type, out hitype);
            return BiZ.Sys.SystemInterViewFactory.GetSystemInterView((BiZ.Sys.InterViewType)hitype, 10, alreadyanswered);
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetAllSystemInterView()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.SystemInterViewFactory.GetAllSystemInterView()));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddSystemInterView(String type, String isaudited, String witter, String question, String answer)
        {
            bool audited = false;
            bool.TryParse(isaudited, out audited);
            int typeint = 1;
            int.TryParse(type, out typeint);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.SystemInterViewFactory.AddSystemInterview(typeint, audited, witter, question, answer)));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DelSystemInterView(String id)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.SystemInterViewFactory.DelSystemInterview(id)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetSystemWants(String type, String count)
        {
            int typeint = 0;
            int.TryParse(type, out typeint);
            int countint = 0;
            int.TryParse(count, out countint);

            JavaScriptSerializer ser = new JavaScriptSerializer();

            return Json(ser.Serialize(BiZ.Sys.Wants.SystemWantsFactory.GetSystemWants((BiZ.Sys.Wants.WantType)typeint, countint)));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetAllSystemWants()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.Wants.SystemWantsFactory.GetAllSystemWants()));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult AddSystemWants(String type, String isaudited, String witter, String iwantstr, String content)
        {
            bool audited = false;
            bool.TryParse(isaudited, out audited);
            int typeint = 1;
            int.TryParse(type, out typeint);
            return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.Wants.SystemWantsFactory.AddSystemWants(typeint, audited, witter, iwantstr, content)), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult DelSystemWants(String id)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.Wants.SystemWantsFactory.DelSystemWants(id)), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetSystemMarks(String sex)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Marks.SystemMarksFactory.GetSystemMarks(int.Parse(sex), 30)));
        //}
        //public ActionResult GetAllSystemMarks()
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Marks.SystemMarksFactory.GetAllSystemSystemMarks()));
        //}
        //public ActionResult AddSystemMark(String sex, String isaudited, String witter, String content, String contentsend, String contentcove)
        //{
        //    int sexint = 1;
        //    int.TryParse(sex, out sexint);
        //    bool audited = false;
        //    bool.TryParse(isaudited, out audited);

        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Marks.SystemMarksFactory.AddSystemMark(sexint, audited, witter, content, contentsend, contentcove)));
        //}
        //public ActionResult DelSystemMark(String id)
        //{
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.Marks.SystemMarksFactory.DelSystemMark(id)), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetSystemSkills(String type, String count)
        //{
        //    int countint = 0;
        //    int.TryParse(count, out countint);

        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Skill.SystemSkillsFactory.GetSystemSkills(type, countint)));
        //}
        //public ActionResult GetAllSystemSkills()
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Skill.SystemSkillsFactory.GetAllSystemSkills()));
        //}
        //public ActionResult AddSystemSkill(String type, String isaudited, String witter, String skillname, String contentsend)
        //{
        //    bool audited = false;
        //    bool.TryParse(isaudited, out audited);

        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.Sys.Skill.SystemSkillsFactory.AddSystemSkill(type, audited, witter, skillname, contentsend)));
        //}
        //public ActionResult DelSystemSkill(String id)
        //{
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.Skill.SystemSkillsFactory.DelSystemSkill(id)), JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region 关键字管理，数据与业务，非视图
        [HttpPost]
        [Authorize]
        public ActionResult AddSystemWord(string wordname, bool word_is_enable, int wordtype) 
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.FilterWord.SystemFilterWordFactory.AddFilterWord(wordname,word_is_enable,wordtype)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetAllSystemFilterWord(int type)
        {
            CBB.CheckHelper.FilterWord.word_type wt = (CBB.CheckHelper.FilterWord.word_type)type;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<Moooyo.BiZ.FilterWord.FilterWordMoldel> list = BiZ.Sys.FilterWord.SystemFilterWordFactory.GetAllSystemFilterWord(wt);
            return Json(ser.Serialize(list));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult DisableFilterWords(string idlist) 
        {
            string [] idarr = idlist.Split('&');           
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.FilterWord.SystemFilterWordFactory.ChangeFilterWords(getObjcetId(idarr), false)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult EnableFilterWords(string idlist)
        {
            string[] idarr = idlist.Split('&');           
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.FilterWord.SystemFilterWordFactory.ChangeFilterWords(getObjcetId(idarr), true)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult DeleteFilterWords(string idlist)
        {
            string[] idarr = idlist.Split('&');
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.FilterWord.SystemFilterWordFactory.DeleteFilterWords(getObjcetId(idarr))));
        }
        [Authorize(Roles = "Manager1")]
        public ActionResult UploadFilterWords()
        {
            try
            {
                HttpPostedFileBase postfile = HttpContext.Request.Files[0];
                string file = postfile.FileName;
                file = HttpContext.Server.MapPath("~/temp_up_file/" + file);
                postfile.SaveAs(file);
                StreamReader sr = new StreamReader(file, System.Text.Encoding.Default);
                string s = "";
                List<string> list = new List<string>();
                while ((s = sr.ReadLine()) != null)
                {
                    list.Add(s);
                }
                int type = Convert.ToInt32(Request.Form["selectwordtype"]);
                CBB.CheckHelper.FilterWord.word_type wt = (CBB.CheckHelper.FilterWord.word_type) type;
                Moooyo.BiZ.Sys.FilterWord.SystemFilterWordFactory.UploadFilterWords(list, wt);
                sr.Dispose();
                System.IO.File.Delete(file);
                return RedirectToAction("dic/FilterWord", "Admin");

            }catch(Exception es)
            {
                string s = es.Message;
                throw new CBB.ExceptionHelper.OperationException(
                  CBB.ExceptionHelper.ErrType.SystemErr,
                  CBB.ExceptionHelper.ErrNo.DBOperationError,
                  es);
            }
        }
                
        private List<ObjectId> getObjcetId(string[] sarr) 
        {
            if(sarr.Length <=0)
                return null;
            List<ObjectId> list = new List<ObjectId>();
            foreach (string s in sarr) 
            {
                list.Add(ObjectId.Parse(s));
            }
            return list;
        }
        #endregion 

        #region 待审内容管理，数据与业务，非视图
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetFilterText(int verifyStatus,int pageno, int pagesize) 
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Moooyo.BiZ.FilterText.VerifyStatus vs = (Moooyo.BiZ.FilterText.VerifyStatus)verifyStatus;
            return Json(ser.Serialize(BiZ.Sys.FilterText.SystemFilterTextFactory.GetAllFilterText(vs,pageno,pagesize)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetFilterTextCount(int type) 
        {
            Moooyo.BiZ.FilterText.VerifyStatus vs = (Moooyo.BiZ.FilterText.VerifyStatus)type;
            long allcount = BiZ.Sys.FilterText.SystemFilterTextFactory.GetCount(vs);
            return Json(new JavaScriptSerializer().Serialize(allcount));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult UpdateFilterTexts(string idlist, int verifyStatus, string adminid, string texts) 
        {
            List<string> listtext = new List<string>();
            List<ObjectId> listid = new List<ObjectId>();
            listid.AddRange(getObjcetId(idlist.Split('&')));
            foreach (string tx in texts.Split('&'))
            {
                listtext.Add(HttpUtility.UrlDecode(tx));
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Moooyo.BiZ.FilterText.VerifyStatus vs = (Moooyo.BiZ.FilterText.VerifyStatus)verifyStatus;
            return Json(ser.Serialize(BiZ.Sys.FilterText.SystemFilterTextFactory.UpdateFilterTexts(listid, vs, adminid, listtext)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult UpdateFilterText(string id, int verifyStatus, string adminid, string text)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Moooyo.BiZ.FilterText.VerifyStatus vs = (Moooyo.BiZ.FilterText.VerifyStatus)verifyStatus;
            return Json(ser.Serialize(BiZ.Sys.FilterText.SystemFilterTextFactory.UpdateFilterText(ObjectId.Parse(id), vs, adminid, text)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult DeleteFilterTexts(string idList)
        {
            List<ObjectId> list = getObjcetId(idList.Split('&').ToArray());
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.FilterText.SystemFilterTextFactory.DeleteFilterText(list)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult DeleteFilterText(string id)
        {
            List<ObjectId> list = new List<ObjectId>();
            list.Add(ObjectId.Parse(id));
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.Sys.FilterText.SystemFilterTextFactory.DeleteFilterText(list)));
        }
        #endregion

        #region  视频认证审核，数据与业务，非视图
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetCheckPhotoCount(int type) 
        {
            //Moooyo.BiZ.PhotoCheck.PhotoCheckModel pc = new Moooyo.BiZ.PhotoCheck.PhotoCheckModel();
            //pc.AdminId = "4eb0fde42101b0824e2b018f";
            //pc.CheckImgPath = "aa.jpg";
            //pc.CheckStatus = 7;
            //pc.JionTime = DateTime.Now;
            //pc.UserHeadName = "cc.jpg";
            //pc.UserId = "4eb0fde42101b0824e2b018f";
            //Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.AddCheckPhoto(pc);

            Moooyo.BiZ.PhotoCheck.CheckPhotoStatus cps = (BiZ.PhotoCheck.CheckPhotoStatus)type;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.GetCheckPhotoCount(cps)));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetCheckPhotos(int type, int pageno, int pagesize)
        {
            Moooyo.BiZ.PhotoCheck.CheckPhotoStatus cps = (BiZ.PhotoCheck.CheckPhotoStatus)type;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.GetCheckPhotoList(cps, pageno, pagesize)));
        }

        /// <summary>
        /// 更新用户是否认证视频
        /// </summary>
        /// <param name="type">是否认证0否1是</param>
        /// <param name="useridlist">用户ID集合</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateUserPhotoisReal(int type,string useridlist)
        {
            List<ObjectId> uslist = getObjcetId(useridlist.Split(','));
            bool is_real = false;
            if (type == 1) is_real = true;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(new JavaScriptSerializer().Serialize(BiZ.MemberManager.MemberManager.CheckRealPhoto(uslist,is_real)));
        }

        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateCheckPhotos(string idlis, int type,string adminid,string useridlist)
        {
            
            List<ObjectId> list = getObjcetId(idlis.Split('&'));
            List<ObjectId> userlist = getObjcetId(useridlist.Split('&'));
            Moooyo.BiZ.PhotoCheck.CheckPhotoStatus cps = (BiZ.PhotoCheck.CheckPhotoStatus)type;
            sendMsg(cps, userlist);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.UpdateCheckPhotoStatuss(list,cps,adminid,userlist)));
        }

        //发送站内信
        private void sendMsg(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus cps, List<ObjectId> userid) 
        {
            string content = "";
            if (cps == BiZ.PhotoCheck.CheckPhotoStatus.auditpass)
            {
                content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertification");
            }
            else if(cps == BiZ.PhotoCheck.CheckPhotoStatus.audidel)
            {
                content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertificationisnotthrough");
            }
            foreach(ObjectId uid in userid)
            {
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(uid.ToString(), content);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateCheckPhoto(string id, int type, string adminid, string userid)
        {

            Moooyo.BiZ.PhotoCheck.CheckPhotoStatus cps = (BiZ.PhotoCheck.CheckPhotoStatus)type;
            //发送站内信
            string content = "";
            if (cps == BiZ.PhotoCheck.CheckPhotoStatus.auditpass)
            {
                content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertification");

            }
            else if (cps == BiZ.PhotoCheck.CheckPhotoStatus.audidel)
            {
                content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertificationisnotthrough");
            }
            BiZ.Member.Activity.ActivityController.SystemMsgToMember(userid, content);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.UpdateCheckPhotoStatus(ObjectId.Parse(id), cps, adminid, userid)));
        }

        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult RemoveCheckPhotos(string idlist,string imglist) 
        {
            List<ObjectId> list = getObjcetId(idlist.Split('&'));
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<string> imglists = new List<string>();
            imglists.AddRange(imglist.Split('&').ToArray());
            return Json(ser.Serialize(Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.RemoveCheckPhotos(list,imglists)));
        }
        #endregion

        #region 账户审核，数据与业务，非视图
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetAllAccount(int type, int usersel, int ssel, string scontent, int pageno, int pagesize)
        {
            bool allowlogin = type == 1 ? true : false;
            List<Moooyo.BiZ.Member.Member> list = Moooyo.BiZ.MemberManager.MemberManager.GetMemberByAllowLogin(allowlogin, usersel, ssel, scontent, pageno, pagesize);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(list));
        }

        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetAllAcountCount(int type, int usersel, int ssel, string scontent) 
        {
            bool allowlogin = type == 1 ? true : false;
            long count = Moooyo.BiZ.MemberManager.MemberManager.GetAllowLoginMemberCount(allowlogin, usersel, ssel, scontent);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(count));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateAllowLogin(string ids, int type)
        {
            bool allowLogin = type == 1 ? true : false;
            List<ObjectId> objids = new List<ObjectId>();
            List<string> Inviterids = new List<string>();
            string[] idstrs = ids.Split(',');
            
            objids = getObjcetId(idstrs);
            foreach (string str in idstrs)
            {
                IList<BiZ.Member.Member> members = BiZ.MemberManager.MemberManager.GetRegInviterMember(str);
                if (members.Count > 0)
                {
                    foreach (var obj in members) { objids.Add(ObjectId.Parse(obj.ID)); }
                    Inviterids.Add(str);
                }
            }
            CBB.ExceptionHelper.OperationResult  opr =  BiZ.MemberManager.MemberManager.UpdateMemberAllowLogin(objids, allowLogin);
            if(opr.ok){
                foreach (string id in Inviterids)
                {
                    BiZ.Member.Activity.ActivityController.SystemMsgToMember(id, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InvertSuccess"));
                }
            }
            return Json(new JavaScriptSerializer().Serialize(true));
        }
        #endregion

        #region 系统应用相关方法，数据与业务，非视图
        /// <summary>
        /// 获取所有系统应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetApplication()
        {
            IList<BiZ.Sys.Applications.Application> applist = BiZ.Sys.Applications.ApplicationsFactory.GetApp();
            return Json(new JavaScriptSerializer().Serialize(applist));
        }
        /// <summary>
        /// 添加系统应用
        /// </summary>
        /// <param name="imgpath">图片地址</param>
        /// <param name="description">应用描述</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddApplication(String imgpath, String des, String linkurl)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.Sys.Applications.ApplicationsFactory.AddApp(imgpath, des, linkurl)));
        }
        /// <summary>
        /// 删除系统应用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DelApplication(String id, String imgname)
        {
            CBB.ExceptionHelper.OperationResult ifok = BiZ.Sys.Applications.ApplicationsFactory.DeleteApp(id);
            if (ifok.ok)
            {
                new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(imgname);
            }
            return Json(new JavaScriptSerializer().Serialize(ifok));
        }
        #endregion

        //#region 帮帮忙相关方法，数据与业务，非视图
        //[HttpPost]
        //public ActionResult AddSystemBBM(String content, String contentsend, String iconpath)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.BBM.RequestFactory.AddSystemRequest(content, contentsend, iconpath)));
        //}
        //[HttpPost]
        //public ActionResult SaveBBMIcon(FormCollection post)
        //{
        //    string memberID = HttpContext.User.Identity.Name;
        //    int holderNo = 1;
        //    if (Request.Files == null || Request.Files.Count == 0)
        //    {
        //        //上传文件为空，返回
        //        return Json(new int[] { -1 });
        //    }
        //    UpController uc = new UpController();
        //    HttpPostedFileBase httpFile = Request.Files[0];
        //    Moooyo.BiZ.Photo.Photo myp = uc.SavePhoto(memberID, holderNo, httpFile.InputStream,httpFile.FileName);

        //    return Content(myp.FileName);

        //}
        //[HttpPost]
        //public ActionResult GetAllSystemBBMs()
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return Json(ser.Serialize(BiZ.BBM.RequestFactory.GetAllRequests()));
        //}
        //[HttpPost]
        //public ActionResult DelSystemBBM(String id)
        //{
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.BBM.RequestFactory.DelSystemRequest(id)), JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region 兴趣相关方法，数据与业务，非视图
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddInterestClass(String title, String icon, int order)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.InterestCenter.InterestFactory.AddInterestClass(title, icon, order)));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetAllInterestClass()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(BiZ.InterestCenter.InterestFactory.GetAllInterestClass()));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DelInterestClass(String id)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.DelInterestClass(id)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult SaveInterestClassIcon(FormCollection postFormColl)
        {
            string memberID = HttpContext.User.Identity.Name;
            int holderNo = 30;
            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }
            UpController uc = new UpController();
            HttpPostedFileBase httpFile = Request.Files[0];
            Moooyo.BiZ.Photo.Photo myp = uc.SavePhoto(memberID, holderNo, httpFile.InputStream, httpFile.FileName);
            return Content(myp.FileName);
        }
        #endregion

        #region 管理员相关方法，数据与业务，非视图
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetSystemManager(String managerId)
        {
            JavaScriptSerializer jSS = new JavaScriptSerializer();
            BiZ.Sys.SystemManager.SystemManager sysMgr = BiZ.Sys.SystemManager.SystemManagerProvider.GetSystemManager(managerId);
            return Json(jSS.Serialize(sysMgr));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateSystemManager(String id, String name, String pwd, int level, bool allowlogin)
        {
            JavaScriptSerializer jSS = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = BiZ.Sys.SystemManager.SystemManagerProvider.UpdateSystemManager(id, name, pwd, level, allowlogin);
            return Json(jSS.Serialize(result));
        }
        #endregion

        #region 推荐数据，数据与业务，非视图
        /// <summary>
        /// 添加管理员喜欢话题
        /// </summary>
        /// <param name="id">话题编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddAdminLikeTopic(String userId, String topicId)
        {
            if(userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.AdminLikeData.GetCollectionName());
            if (!ifLiked)
            {
                BiZ.Like.LikeDataFactory.AddAdminLikeData(userId, topicId, BiZ.Like.LikeType.WenWen);

                //管理员喜爱兴趣下话题时记录管理员对兴趣下话题的喜好数据
                BiZ.Recommendation.TopicTrainingData inttTrai = new BiZ.Recommendation.TopicTrainingData(userId, topicId, BiZ.Recommendation.TopicTrainingDataType.AdminLike);
                //管理员喜爱兴趣下话题时改变兴趣下话题排名的分值
                CBB.RankingHelper.IRankingAble irk = BiZ.WenWen.WenWenProvider.GetWenWen(topicId);
                if (irk != null)
                {
                    CBB.RankingHelper.RankingProvider.AddScores(irk, 1);
                }
                //更新兴趣问问被喜欢的数量，加一
                BiZ.WenWen.WenWenProvider.UpdateLikeCount(topicId, 0, 1, userId, false);
                return Json(new JavaScriptSerializer().Serialize(BiZ.WenWen.WenWenProvider.GetWenWen(topicId).AdminLikeCount));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 删除管理员喜欢话题
        /// </summary>
        /// <param name="id">话题编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DeleteAdminLikeTopic(String userId, String topicId)
        {
            if(userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.AdminLikeData.GetCollectionName());
            if (ifLiked)
            {
                BiZ.Like.LikeDataFactory.DeleteLikeData<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.AdminLikeData.GetCollectionName());

                //更新管理员兴趣问问喜欢数，减一
                BiZ.WenWen.WenWenProvider.UpdateLikeCount(topicId, 0, -1, userId, false);
                return Json(new JavaScriptSerializer().Serialize(BiZ.WenWen.WenWenProvider.GetWenWen(topicId).AdminLikeCount));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 添加管理员喜欢内容
        /// </summary>
        /// <param name="id">话题编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddAdminLikeTopicContent(String userId, String topicId)
        {
            if (userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.Content, BiZ.Like.AdminLikeData.GetCollectionName());
            if (!ifLiked)
            {
                BiZ.Like.LikeDataFactory.AddAdminLikeData(userId, topicId, BiZ.Like.LikeType.Content);

                //管理员喜爱兴趣下话题时记录管理员对兴趣下话题的喜好数据
                BiZ.Recommendation.TopicTrainingData inttTrai = new BiZ.Recommendation.TopicTrainingData(userId, topicId, BiZ.Recommendation.TopicTrainingDataType.AdminLike);
                ////管理员喜爱兴趣下话题时改变兴趣下话题排名的分值
                //CBB.RankingHelper.IRankingAble irk = BiZ.WenWen.WenWenProvider.GetWenWen(topicId);
                //if (irk != null)
                //{
                //    CBB.RankingHelper.RankingProvider.AddScores(irk, 1);
                //}
                //更新兴趣问问被喜欢的数量，加一
                BiZ.Content.PublicContent.UpdateLikeCount(topicId);
                return Json(new JavaScriptSerializer().Serialize(true));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 删除管理员喜欢内容
        /// </summary>
        /// <param name="id">话题编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DeleteAdminLikeTopicContent(String userId, String topicId)
        {
            if (userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.Content, BiZ.Like.AdminLikeData.GetCollectionName());
            if (ifLiked)
            {
                BiZ.Like.LikeDataFactory.DeleteLikeData<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.Content, BiZ.Like.AdminLikeData.GetCollectionName());

                //更新管理员兴趣问问喜欢数，减一
                BiZ.Content.PublicContent.UpdateLikeCount(topicId);
                return Json(new JavaScriptSerializer().Serialize(true));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 推荐顶部图片内容
        /// </summary>
        /// <param name="contentid">内容编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddTopImagePush(string contentid)
        {
            BiZ.Content.ImageContent imagecontent=new BiZ.Content.ImageContent(contentid);
            BiZ.TopImagePush.ImagePushCount pushcount = new BiZ.TopImagePush.ImagePushCount();
            BiZ.TopImagePush.ImagePush imagepush = new BiZ.TopImagePush.ImagePush(
                contentid,
                imagecontent.MemberID,
                imagecontent.ImageList,
                imagecontent.Content,
                pushcount,
                DateTime.Now,
                BiZ.Comm.DeletedFlag.No);
            imagepush.Save(imagepush);
            return Json(new JavaScriptSerializer().Serialize(true));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddTopPushImage(String contentId, String photoPath)
        {
            if (contentId == null) 
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (photoPath == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            CBB.ExceptionHelper.OperationResult result = BiZ.TopImagePush.ImagePushProvider.AddPushImage(contentId, photoPath);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DeleteTopImagePush(string pushImgId)
        {
            if (pushImgId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            CBB.ExceptionHelper.OperationResult result = BiZ.TopImagePush.ImagePushProvider.DeletePushImage(pushImgId);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        /// <summary>
        /// 判断图片内容是否已推送
        /// </summary>
        /// <param name="contentid">内容编号</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult IfImagePush(string contentid)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.TopImagePush.ImagePushProvider.ifImagePush(contentid)));
        }
        /// <summary>
        /// 获取所有内容图片推送
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetAllTopImagesPush(String pageSize, String pageNo)
        {
            int pSize = 0, pNo = 0;
            if (!Int32.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!Int32.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            List<BiZ.TopImagePush.ImagePush> objs = BiZ.TopImagePush.ImagePushProvider.findAll(pNo, pSize);
            return Json(new JavaScriptSerializer().Serialize(objs));
        }
        /// <summary>
        /// 按删除标记获取所有内容图片推送
        /// </summary>
        /// <param name="deleteFlag">删除标标记</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetTopImagesPush(String deleteFlag, String pageSize, String pageNo)
        {
            int pSize = 0, pNo = 0;
            byte deleFlag = 0;
            BiZ.Comm.DeletedFlag deleFlagObj = BiZ.Comm.DeletedFlag.No;
            if (!Int32.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!Int32.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!Byte.TryParse(deleteFlag, out deleFlag)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            switch (deleFlag)
            {
                case 0:
                    deleFlagObj = BiZ.Comm.DeletedFlag.No;
                    break;
                case 1:
                    deleFlagObj = BiZ.Comm.DeletedFlag.Yes;
                    break;
                default: 
                    break;
            }
            List<BiZ.TopImagePush.ImagePush> objs = BiZ.TopImagePush.ImagePushProvider.findAll(deleFlagObj, pNo, pSize);
            return Json(new JavaScriptSerializer().Serialize(objs));
        }
        /// <summary>
        /// 按用户编号更新所有话题管理员喜欢数减一
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <returns>操作状态</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult UpdateAllTopicsAdminLikeCount(String userId)
        {
            if (userId == "" || userId == null) userId = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult operResu = BiZ.WenWen.WenWenProvider.UpdateAllTopicsAdminLikeCount(userId, -1);
            return Json(new JavaScriptSerializer().Serialize(operResu));
        }
        /// <summary>
        /// 管理员是否喜欢指定兴趣话题
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <param name="topicId">话题编号</param>
        /// <returns>是否已喜欢</returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult IfAdminLikedTopic(String userId, String topicId)
        {
            if (userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLikedTopic = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.AdminLikeData.GetCollectionName());

            return Json(new JavaScriptSerializer().Serialize(ifLikedTopic));
        }
        /// <summary>
        /// 管理员是否喜欢指定内容
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <param name="topicId">话题编号</param>
        /// <returns>是否已喜欢</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult IfAdminLikedTopicContent(String userId, String topicId)
        {
            if (userId == "" || userId == null) userId = User.Identity.Name;
            Boolean ifLikedTopic = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.AdminLikeData>(userId, topicId, BiZ.Like.LikeType.Content, BiZ.Like.AdminLikeData.GetCollectionName());

            return Json(new JavaScriptSerializer().Serialize(ifLikedTopic));
        }
        /// <summary>
        ///  获取管理员喜欢或不喜欢的兴趣话题
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <param name="likeOrNot">喜欢或不喜欢</param>
        /// <returns>话题列表</returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetAdminLikeOrNotTopics(String interestID, int pageSize, int pageNo, Boolean likeOrNot)
        {
            IList<BiZ.WenWen.WenWen> topicsList = BiZ.WenWen.WenWenProvider.GetAdminLikeOrNotTopics(interestID, pageSize, pageNo, likeOrNot);
            return Json(new JavaScriptSerializer().Serialize(topicsList));
        }
        /// <summary>
        /// 获取管理员喜欢或不喜欢的兴趣话题总数
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="likeOrNot">喜欢或不喜欢</param>
        /// <returns>兴趣话题总数</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetAdminLikeOrNotTopicsCount(String interestID, Boolean likeOrNot)
        {
            long adminLikeOrNotTopicsCount = BiZ.WenWen.WenWenProvider.GetAdminLikeOrNotTopicsCount(interestID, likeOrNot);
            return Json(new JavaScriptSerializer().Serialize(adminLikeOrNotTopicsCount));
        }
        /// <summary>
        /// 设置推送图片显示状态
        /// </summary>
        /// <param name="pushImageId">推送图片编号</param>
        /// <param name="showStatus">显示状态</param>
        /// <returns>操作状态</returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult SetPushImageShowStatus(String pushImageId, String showStatus)
        {
            if (pushImageId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (showStatus == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Comm.DeletedFlag deletedFlag = (BiZ.Comm.DeletedFlag)Enum.Parse(typeof(BiZ.Comm.DeletedFlag), showStatus);
            CBB.ExceptionHelper.OperationResult result = BiZ.TopImagePush.ImagePushProvider.SetPushImageDeleteFlag(pushImageId, deletedFlag);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        /// <summary>
        /// 按是否显示获取所有推送图片总数
        /// </summary>
        /// <param name="deleteFlag">是否显示</param>
        /// <returns>推送图片总数</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetPushImageCount(String deleteFlag)
        {
            if (deleteFlag == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Comm.DeletedFlag deletedFlag = (BiZ.Comm.DeletedFlag)Enum.Parse(typeof(BiZ.Comm.DeletedFlag), deleteFlag);
            long pushImageCount = BiZ.TopImagePush.ImagePushProvider.findAllCount(deletedFlag);
            return Json(new JavaScriptSerializer().Serialize(pushImageCount));
        }
        /// <summary>
        /// 获取所有推送图片总数
        /// </summary>
        /// <returns>推送图片总数</returns>
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetAllPushImageCount()
        {
            long pushImageCount = BiZ.TopImagePush.ImagePushProvider.GetAllPushImageCount();
            return Json(new JavaScriptSerializer().Serialize(pushImageCount));
        }
        #endregion 

        #region 用户皮肤，数据与业务
        /// <summary>
        /// 添加用户皮肤
        /// </summary>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景图片</param>
        /// <returns>操作状态</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult AddMemberSkin(FormCollection postFormColl, string kidneyPicture, string kidneyBgPicture)
        {
            if((kidneyPicture == null || kidneyPicture == "") && (kidneyBgPicture == null || kidneyBgPicture == ""))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userId = HttpContext.User.Identity.Name;

            int ppHolderNo = 11, pbpHolderNo = 12;
            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }
            UpController uc = new UpController();
            HttpPostedFileBase ppHttpFile = null;
            HttpPostedFileBase pbpHttpFile = null;
            BiZ.Photo.Photo ppPhoto = null;
            BiZ.Photo.Photo pbpPhoto = null;

            if ((kidneyPicture != null || kidneyPicture != "") && (kidneyBgPicture == null || kidneyBgPicture == ""))
            {
                ppHttpFile = Request.Files["personalityPicture"];
                ppPhoto = uc.SavePhoto(userId, ppHolderNo, ppHttpFile.InputStream, ppHttpFile.FileName);
            }
            else if ((kidneyPicture == null || kidneyPicture == "") && (kidneyBgPicture != null || kidneyBgPicture != ""))
            {
                pbpHttpFile = Request.Files["personalityBackgroundPicture"];
                pbpPhoto = uc.SavePhoto(userId, pbpHolderNo, pbpHttpFile.InputStream, pbpHttpFile.FileName);
            }
            else if ((kidneyPicture != null || kidneyPicture != "") && (kidneyBgPicture != null || kidneyBgPicture != ""))
            {
                ppHttpFile = Request.Files["personalityPicture"];
                pbpHttpFile = Request.Files["personalityBackgroundPicture"];

                ppPhoto = uc.SavePhoto(userId, ppHolderNo, ppHttpFile.InputStream, ppHttpFile.FileName);
                pbpPhoto = uc.SavePhoto(userId, pbpHolderNo, pbpHttpFile.InputStream, pbpHttpFile.FileName);
            }

            string personalityPicture = ppPhoto != null ? ppPhoto.FileName : "";
            string personalityBackgroundPicture = pbpPhoto != null ? pbpPhoto.FileName : "";

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = null;

            ////按用户编号获取用户皮肤
            //BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.GetEmptyMemberSkin(null, userId);
            //if (ms != null)
            //{
            //    if ((ms.PersonalityPicture == null || ms.PersonalityPicture == "") && (ms.PersonalityBackgroundPicture != null || ms.PersonalityBackgroundPicture != "") && (personalityPicture != null && personalityPicture != "") && (personalityBackgroundPicture == null && personalityBackgroundPicture == ""))
            //    {
            //        //更新用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.UpdateMemberSkin(
            //            ms.ID,
            //            userId,
            //            personalityPicture,
            //            personalityBackgroundPicture);
            //    }
            //    else if ((ms.PersonalityPicture != null || ms.PersonalityPicture != "") && (ms.PersonalityBackgroundPicture == null || ms.PersonalityBackgroundPicture == "") && (personalityPicture == null && personalityPicture == "") && (personalityBackgroundPicture != null && personalityBackgroundPicture != ""))
            //    {
            //        //更新用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.UpdateMemberSkin(
            //            ms.ID,
            //            userId,
            //            personalityPicture,
            //            personalityBackgroundPicture);
            //    }
            //    else
            //    {
            //        //添加用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
            //            userId,
            //            BiZ.Comm.UserType.Administrator,
            //            personalityPicture,
            //            personalityBackgroundPicture
            //            );
            //    }
            //}
            //else
            //{

            //添加用户皮肤
            BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
                userId,
                BiZ.Comm.UserType.Administrator,
                personalityPicture,
                personalityBackgroundPicture
                );
            if (ms != null)
                result = new CBB.ExceptionHelper.OperationResult(true);
            else
                result = new CBB.ExceptionHelper.OperationResult(false);

            //}

            ////按用户编号获取用户皮肤
            //BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.GetEmptyMemberSkin2(userId, personalityPicture, personalityBackgroundPicture);
            //if (ms != null)
            //{

            //}

            if (result.ok)
                return Content("{\"ok\":\"true\"}");
            else
                return Content("{\"ok\":\"false\"}");
        }
        /// <summary>
        /// 删除用户皮肤
        /// </summary>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景图片</param>
        /// <returns>操作状态</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DeleteMemberSkin(String memberSkinId)
        {
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = BiZ.Member.MemberSkin.MemberSkinProvider.DeleteMemberSkin(memberSkinId);
            return Json(jsSerial.Serialize(result));
        }
        /// <summary>
        /// 分页获取所有用户皮肤
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户皮肤集合</returns>
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetMemberSkins(int pageSize, int pageNo)
        {
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            IList<BiZ.Member.MemberSkin.MemberSkin> objs = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkins(pageSize, pageNo);
            return Json(jsSerial.Serialize(objs));
        }
        #endregion

        #region 活动，数据与业务
        [HttpPost]
        [Authorize]
        public ActionResult GenerateInviteCodeToMember()
        {
            String memberId = HttpContext.User.Identity.Name;
            BiZ.Sys.InvitationCode.InvitationCode obj = BiZ.Sys.InvitationCode.InvitationCodeProvider.GenerateInviteCode(memberId);
            return Json(new JavaScriptSerializer().Serialize(obj));
        }
        [HttpPost]
        public ActionResult GetInviteCodeToMember()
        {
            String memberId = HttpContext.User.Identity.Name;
            BiZ.Sys.InvitationCode.InvitationCode lastobj = BiZ.Sys.InvitationCode.InvitationCodeProvider.GetInviteCodeToGeneratedMember(memberId);
            if (lastobj != null && lastobj.CreatedTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                return Json(new JavaScriptSerializer().Serialize(lastobj));
            }
            else
            {
                return Json(new JavaScriptSerializer().Serialize(true));
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetInviteCodes(String usedFlag, String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            if (usedFlag != null && usedFlag != "")
                usedFlagEnum = (BiZ.Comm.UsedFlag)Convert.ToByte(usedFlag);

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            IList<BiZ.Sys.InvitationCode.InvitationCode> objs = BiZ.Sys.InvitationCode.InvitationCodeProvider.GetInviteCodes(pSize, pNo, usedFlagEnum);
            return Json(jsSerial.Serialize(objs));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]  
        public ActionResult GenerateInviteCodes(string count, string generatedMemberId)
        {
            if (count == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (generatedMemberId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            short icCount = 0;
            if (!short.TryParse(count, out icCount)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            string generatedMember = HttpContext.User.Identity.Name;
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = BiZ.Sys.InvitationCode.InvitationCodeProvider.GenerateInviteCodes(icCount, generatedMember, generatedMemberId);
            return Json(jsSerial.Serialize(result));
        }
        [HttpPost]
        [Authorize]
        public ActionResult AjaxSetInviteCodeHasUsed(string inviteCode)
        {
            if (inviteCode == null) 
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = null;
            String memberId = HttpContext.User.Identity.Name;
            result = SetInviteCodeHasUsed(inviteCode, memberId);
            return Json(jsSerial.Serialize(result));
        }
        public static CBB.ExceptionHelper.OperationResult SetInviteCodeHasUsed(string inviteCode, String memberId)
        {
            if (inviteCode == null)
                return new CBB.ExceptionHelper.OperationResult(false, "邀请码不能为Null");

            int inviCode = 0;
            if (!int.TryParse(inviteCode, out inviCode))
                return new CBB.ExceptionHelper.OperationResult(false, "邀请码应为数字");

            BiZ.Sys.InvitationCode.InvitationCode inviteCodeObj = BiZ.Sys.InvitationCode.InvitationCodeProvider.GetInviteCode(inviCode);
            if (inviteCodeObj != null)
            {
                inviteCodeObj.UsedFlag = BiZ.Comm.UsedFlag.Yes;
                if (memberId != null && memberId != "")
                    inviteCodeObj.UsedMember = new BiZ.Creater.Creater(memberId);
                else
                    inviteCodeObj.UsedMember = null;
                BiZ.Sys.InvitationCode.InvitationCode invitationCodeObj = BiZ.Sys.InvitationCode.InvitationCodeProvider.Save(inviteCodeObj);
                if (invitationCodeObj != null)
                {
                    if (inviteCodeObj.GeneratedMember != null)
                    {
                        int points = Common.Comm.getInvitationCodeToPoints();
                        string userid = inviteCodeObj.UsedMember != null ? inviteCodeObj.UsedMember.MemberID : "";
                        string sysmes = "Hi\n你生成的邀请码已被成功使用。\n该柚子已进入米柚，开始Ta有爱的快乐单身时光啦~\n为了嘉奖你乐于将好东东分享给身边朋友的美好品德，佛祖决定奖励你" + points + "个米果~\n\n去看看你邀请来的柚子 [url_t1]{\"url\":\"/Content/TaContent/" + userid + "/all/1\",\"text\":\"传送门\"}[/url_t1]";
                        //\n米果有什么用？ [url_t1]{\"url\":\"/Content/AboutUsePoints\",\"text\":\"传送门\"}[/url_t1]
                        //系统对赠送者发赠送米果的私信
                        BiZ.Member.Activity.ActivityController.SystemMsgToMember(inviteCodeObj.GeneratedMember.MemberID, sysmes);
                        //系统对邀请码生成者赠送积分
                        BiZ.MemberManager.MemberManager.ModifyPoints(inviteCodeObj.GeneratedMember.MemberID, BiZ.MemberManager.StatusModifyType.Add, points);
                    }
                    return new CBB.ExceptionHelper.OperationResult(true);
                }
            }
            else
            {
                return new CBB.ExceptionHelper.OperationResult(false, "邀请码有误");
            }
            return new CBB.ExceptionHelper.OperationResult(true);
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetInviteCodesCount(String usedFlag)
        {
            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            if (usedFlag != null && usedFlag != "")
                usedFlagEnum = (BiZ.Comm.UsedFlag)Convert.ToByte(usedFlag);

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            int count = BiZ.Sys.InvitationCode.InvitationCodeProvider.GetInviteCodesCount(usedFlagEnum);
            return Json(jsSerial.Serialize(count));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetFeaturedContents(String usedFlag, String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            if (usedFlag != null && usedFlag != "")
                usedFlagEnum = (BiZ.Comm.UsedFlag)Convert.ToByte(usedFlag);

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            IList<BiZ.Sys.FeaturedContent.FeaturedContent> objs = BiZ.Sys.FeaturedContent.FeaturedContentProvider.GetFeaturedContents(pSize, pNo, usedFlagEnum);
            return Json(jsSerial.Serialize(objs));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult GetFeaturedContentsCount(String usedFlag)
        {
            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            if (usedFlag != null && usedFlag != "")
                usedFlagEnum = (BiZ.Comm.UsedFlag)Convert.ToByte(usedFlag);

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            int count = BiZ.Sys.FeaturedContent.FeaturedContentProvider.GetFeaturedContentsCount(usedFlagEnum);
            return Json(jsSerial.Serialize(count));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult SaveFeaturedContentImage(FormCollection postFormColl)
        {
            string memberID = HttpContext.User.Identity.Name;
            int holderNo = 205;
            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }
            UpController uc = new UpController();
            HttpPostedFileBase httpFile = Request.Files[0];
            Moooyo.BiZ.Photo.Photo myp = uc.SavePhoto(memberID, holderNo, httpFile.InputStream, httpFile.FileName);
            return Content(myp.FileName);
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult AddFeaturedContent(String image, String content, String usedFlag)
        {
            CBB.ExceptionHelper.OperationResult result;

            if (image == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (content == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (usedFlag == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String creator = HttpContext.User.Identity.Name;

            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            if (usedFlag != null && usedFlag != "")
                usedFlagEnum = (BiZ.Comm.UsedFlag)Convert.ToByte(usedFlag);

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            result = BiZ.Sys.FeaturedContent.FeaturedContentProvider.AddFeaturedContent(image, content, creator, usedFlagEnum);
            return Json(jsSerial.Serialize(result));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult DeleteFeaturedContent(String featContentId)
        {
            CBB.ExceptionHelper.OperationResult result;

            if (featContentId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            result = BiZ.Sys.FeaturedContent.FeaturedContentProvider.Delete(featContentId);
            return Json(jsSerial.Serialize(result));
        }
        [Authorize(Roles = "Manager1")]
        [HttpPost]
        public ActionResult AjaxSetFeaturedContentHasUsed(string featCtentId, String usedFlag)
        {
            if (featCtentId == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (usedFlag == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            byte usedFlagNum = 0;
            if(!byte.TryParse(usedFlag, out usedFlagNum))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Comm.UsedFlag usedFlagEnum = BiZ.Comm.UsedFlag.Unknown;
            usedFlagEnum = (BiZ.Comm.UsedFlag)usedFlagNum;

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = null;
            String memberId = HttpContext.User.Identity.Name;
            result = SetFeaturedContentHasUsed(featCtentId, usedFlagEnum);
            return Json(jsSerial.Serialize(result));
        }
        public static CBB.ExceptionHelper.OperationResult SetFeaturedContentHasUsed(string featCtentId, BiZ.Comm.UsedFlag usedFlag)
        {
            

            if (featCtentId == null)
                return new CBB.ExceptionHelper.OperationResult(false, "精选内容编号不能为Null");

            BiZ.Sys.FeaturedContent.FeaturedContent featContent = BiZ.Sys.FeaturedContent.FeaturedContentProvider.GetFeaturedContent(featCtentId);
            if (featContent != null)
            {
                featContent.UsedFlag = usedFlag;
                //显示次数增加一次
                if (usedFlag == BiZ.Comm.UsedFlag.Yes)
                {
                    featContent.ShowedCount += 1;
                }

                BiZ.Sys.FeaturedContent.FeaturedContent featContentObj = BiZ.Sys.FeaturedContent.FeaturedContentProvider.Save(featContent);
                if (featContentObj != null)
                    return new CBB.ExceptionHelper.OperationResult(true);
            }
            else
            {
                return new CBB.ExceptionHelper.OperationResult(false, "精选内容编号有误");
            }
            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion

        #region 用户状态，数据与业务
        [HttpGet]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetMemberInfoCount()
        {
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            int count = BiZ.Sys.MemberActivity.MemberActivityProvider.GetMemberActivityCount();
            return Json(jsSerial.Serialize(count), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetTypeMemberInfoCount(string activityType)
        {
            if (activityType == null) 
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            byte numActivityType = 20;
            if(!byte.TryParse(activityType, out numActivityType))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            BiZ.Sys.MemberActivity.MemberActivityType enumActivityType;

            enumActivityType = GetMemberInfoTypeEnum(numActivityType);

            int count = BiZ.Sys.MemberActivity.MemberActivityProvider.GetTypeMemberActivityCount(enumActivityType);
            return Json(jsSerial.Serialize(count), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetFromMemberInfoCount(string fromMember)
        {
            if (fromMember == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            long numFromMember = 0;
            if (!long.TryParse(fromMember, out numFromMember))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            int count = BiZ.Sys.MemberActivity.MemberActivityProvider.GetFromMemberActivityCount(numFromMember);
            return Json(jsSerial.Serialize(count));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetTimeMemberInfoCount(string startTime, string endTime)
        {
            if (startTime == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (endTime == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            DateTime startTime2, endTime2;
            if (!DateTime.TryParse(startTime, out startTime2))
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!DateTime.TryParse(endTime, out endTime2))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            int count = BiZ.Sys.MemberActivity.MemberActivityProvider.GetTimeMemberActivityCount(startTime2, endTime2);
            return Json(jsSerial.Serialize(count));
        }
        private static BiZ.Sys.MemberActivity.MemberActivityType GetMemberInfoTypeEnum(int numActivityType)
        {
            BiZ.Sys.MemberActivity.MemberActivityType enumActivityType;

            switch (numActivityType)
            {
                case 0: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.UploadAvatar;
                    break;
                case 1: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.UploadPicture;
                    break;
                case 2: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Comment;
                    break;
                case 3: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.SinaMicroblogLogin;
                    break;
                case 4: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.DoubanLogin;
                    break;
                case 5: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.VideoCertificate;
                    break;
                case 6: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.RemoveContent;
                    break;
                case 7: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.PrivateLetter;
                    break;
                case 8: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.PersonalInfoChange;
                    break;
                case 9: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.AddInterest;
                    break;
                case 10: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.AddInterview;
                    break;
                case 11: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.NewTalkAbout;
                    break;
                case 12: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.LikeOther;
                    break;
                case 13: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.FavorOther;
                    break;
                case 14: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.SubmitOpinion;
                    break;
                case 15: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Logout;
                    break;
                case 16: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Login;
                    break;
                case 17: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.CreateInterest;
                    break;
                case 18: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.TencentMicroblogLogin;
                    break;
                case 19: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.RenrenLogin;
                    break;
                case 20: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Unknown;
                    break;
                case 21: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Register;
                    break;
                case 22: enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.AllowLogin;
                    break;
                default:
                    enumActivityType = BiZ.Sys.MemberActivity.MemberActivityType.Unknown;
                    break;
            }
            return enumActivityType;
        }
        [HttpGet]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetMemberInfos(String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            IList<BiZ.Sys.MemberActivity.MemberActivity> objs = BiZ.Sys.MemberActivity.MemberActivityProvider.GetMemberActivitys(pSize, pNo);
            return Json(jsSerial.Serialize(objs), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetTypeMemberInfos(string activityType, String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            byte numActivityType = 20;
            if (!byte.TryParse(activityType, out numActivityType))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            BiZ.Sys.MemberActivity.MemberActivityType enumActivityType;
            enumActivityType = GetMemberInfoTypeEnum(numActivityType);
            IList<BiZ.Sys.MemberActivity.MemberActivity> objs = BiZ.Sys.MemberActivity.MemberActivityProvider.GetMemberActivitys(enumActivityType, pSize, pNo);
            return Json(jsSerial.Serialize(objs), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetFromMemberInfos(string fromMember, String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            long numFromMember = 0;
            if (!long.TryParse(fromMember, out numFromMember))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            IList<BiZ.Sys.MemberActivity.MemberActivity> objs = BiZ.Sys.MemberActivity.MemberActivityProvider.GetMemberActivitys(numFromMember, pSize, pNo);
            return Json(jsSerial.Serialize(objs));
        }
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetTimeMemberInfos(string startTime, string endTime, String pageSize, String pageNo)
        {
            if (startTime == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (endTime == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            DateTime startTime2, endTime2;
            if(!DateTime.TryParse(startTime, out startTime2))
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!DateTime.TryParse(endTime, out endTime2))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            IList<BiZ.Sys.MemberActivity.MemberActivity> objs = BiZ.Sys.MemberActivity.MemberActivityProvider.GetMemberActivitys(startTime2, endTime2, pSize, pNo);
            return Json(jsSerial.Serialize(objs));
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateMemberActivity(
            string fromMember,
            string toMember,
            string activType,
            String operateUrl)
        {
            if (fromMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (activType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (operateUrl == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            byte numActivType = 20;
            if(!byte.TryParse(activType, out numActivType))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                fromMember,
                toMember,
                (BiZ.Sys.MemberActivity.MemberActivityType)numActivType,
                operateUrl);

            JavaScriptSerializer jsSeri = new JavaScriptSerializer();
            CBB.ExceptionHelper.OperationResult result = new CBB.ExceptionHelper.OperationResult(true);
            return Json(jsSeri.Serialize(result), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 图片审核，数据与业务
        [HttpPost]
        [Authorize(Roles = "Manager1")]
        public ActionResult GetTypeAuditPhotoCount(string photoType, string isAudited)
        {
            if (photoType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (isAudited == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            byte numIsAudited = 0,
                numPhotoType = 0;
            if(!byte.TryParse(isAudited, out numIsAudited))
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!byte.TryParse(photoType, out numPhotoType))
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Photo.PhotoType enumPhotoType = (BiZ.Photo.PhotoType)numPhotoType;

            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            long count = BiZ.Photo.PhotoManager.GetTypeAuditPhotoCount(enumPhotoType, numIsAudited);
            return Json(jsSerial.Serialize(count));
        }
        #endregion
    }
}
