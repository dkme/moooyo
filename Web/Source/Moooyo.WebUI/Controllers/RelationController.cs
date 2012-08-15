using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Security.Principal;

namespace Moooyo.WebUI.Controllers
{
    /// <summary>
    /// 关系控制器
    /// </summary>
    public class RelationController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        [Authorize]
        public ActionResult Favors(String p)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            //页码
            int pageno = 1;
            int pagesize = 48;
            if (!Int32.TryParse(p, out pageno)) pageno = 1;
            pagesize = (pageno == 1 ? pagesize : 144);

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            
            IList<Models.RelationDisplayObj> list = Models.DisplayObjProvider.getIFavoredList(userid, pagesize, pageno);
            //页面数据对象
            Models.PageModels.MemberRelationsModel model = new Models.PageModels.MemberRelationsModel(
                    memberDisplayObj,
                    list);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.Pagger = new Models.PaggerObj();
            model.Member.FavorMemberCount = Models.DisplayObjProvider.getIFavoredCount(userid);
            int pages = (int)Math.Ceiling((double)model.Member.FavorMemberCount / pagesize);
            model.Pagger.PageCount = pages;
            model.Pagger.PageSize = pagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Relation/Favors";
            #endregion

            return View(model);
        }
        [Authorize]
        public ActionResult Disables(String p)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            //页码
            int pageno = 1;
            if (!Int32.TryParse(p, out pageno)) pageno = 1;

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
            IList<Models.RelationDisplayObj> list = Models.DisplayObjProvider.getDisablesList(userid, pagesize, pageno);
            //页面数据对象
            Models.PageModels.MemberRelationsModel model = new Models.PageModels.MemberRelationsModel(
                    memberDisplayObj,
                    list);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.Pagger = new Models.PaggerObj();
            model.Member.FavorMemberCount = list.Count;
            int pages = (int)Math.Floor((double)model.Member.FavorMemberCount / pagesize);
            model.Pagger.PageCount = pages + 1;
            model.Pagger.PageSize = pagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Relation/Disables";
            #endregion

            return View(model);
        }
        //[Authorize]
        //public ActionResult I(String t, String p)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");

        //    int pageno = 1;
        //    if (!Int32.TryParse(p, out pageno)) pageno = 1;
        //    ViewData["pageno"] = pageno;
        //    int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
        //    ViewData["pagesize"] = pagesize;
        //    #endregion

        //    String mid = HttpContext.User.Identity.Name;
        //    ViewData["mid"] = mid;
        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

        //    ViewData["UnReadBeenViewedTimes"] = mym.Status.UnReadBeenViewedTimes;
        //    //ViewData["UnReadMarkCount"] = mym.Status.UnReadMarkCount;
        //    ViewData["UnReadMsgCount"] = mym.Status.UnReadMsgCount;
        //    ViewData["UnReadBeenFavorCount"] = mym.Status.UnReadBeenFavorCount;

        //    JavaScriptSerializer js = new JavaScriptSerializer();

        //    if (t == null)
        //    {
        //        if (mym.Status.UnReadMsgCount > 0) t = "lastmsg";
        //        else
        //            if (mym.Status.UnReadBeenFavorCount > 0) t = "favorme";
        //            else
        //                if (mym.Status.UnReadBeenViewedTimes > 0) t = "vin";
        //                //if (mym.Status.UnReadBeenViewedTimes > 0 || mym.Status.UnReadMarkCount > 0) t = "vin";
        //                else
        //                    t = "activity";
        //    }

        //    ViewData["t"] = t;

        //    if (t == "activity")
        //    {
        //        int pages = (int)Math.Floor((double)BiZ.Member.Activity.ActivityController.GetFavorMemberActivitysCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["url"] = "/relation/i/activity";
        //    }
        //    if (t == "vin")
        //    {
        //        BiZ.MemberManager.MemberManager.SetUnReadBeenViewedTimesZero(mid);
        //        //BiZ.MemberManager.MemberManager.SetUnReadMarkCountZero(mid);
        //        //BiZ.MemberManager.MemberManager.SetUnReadScoreCountZero(mid);
        //        ViewData["Vistors"] = Models.DisplayObjProvider.getVisitorListJson(mid, pagesize, pageno);
        //        int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetVistorsCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["url"] = "/relation/i/vin";
        //    }
        //    if (t == "favorme")
        //    {
        //        BiZ.MemberManager.MemberManager.SetUnReadBeenFavorCountZero(mid);
        //        ViewData["Favors"] = Models.DisplayObjProvider.getFavorMeListJson(mid, pagesize, pageno);
        //        int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetListWhoFavoredMeCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["url"] = "/relation/i/favorme";
        //    }
        //    if (t == "ifavored")
        //    {
        //        ViewData["Favors"] = Models.DisplayObjProvider.getIFavoredListJson(mid, pagesize, pageno);
        //        int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetFavorersCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["url"] = "/relation/i/ifavored";
        //    }
        //    if (t == "lastmsg")
        //    {
        //        int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetLastMsgersCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["lastmsg"] = Models.DisplayObjProvider.getLastMsgersJson(mid, pagesize, pageno);
        //        ViewData["url"] = "/relation/i/lastmsg";
        //    }
        //    if (t == "disabled")
        //    {
        //        int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetDisablersCount(mid) / pagesize);
        //        ViewData["pagecount"] = pages + 1;
        //        ViewData["disables"] = Models.DisplayObjProvider.getDisablesListJson(mid, pagesize, pageno);
        //        ViewData["url"] = "/relation/i/disabled";
        //    }

        //    bool alreadyLogin = isalreadylogin("Member");

        //    #region 构造页面数据对象
        //    Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(mid);

        //    //页面数据对象
        //    Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
        //    memberModel.UserID = mid;
        //    memberModel.MemberID = mid;
        //    memberModel.AlreadyLogon = alreadyLogin;
        //    #endregion

        //    return View(memberModel);
        //}
        [Authorize]
        public ActionResult MsgerPanel(String me, String you)
        {
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            Models.MsgerDisplayObj obj = Models.DisplayObjProvider.getMsgerDisplayObj(me, you);
            return View(obj);
        }
        [Authorize]
        public ActionResult RelationListPanel(IList<Models.RelationDisplayObj> objList)
        {
            Models.PageModels.MemberRelationsModel model = new Models.PageModels.MemberRelationsModel(
                    objList);
            return View(model);
        }
        [Authorize]
        public ActionResult Fans(String page)
        {
            SetMetasVersion();

            //页码
            int pageno = 1;
            int pagesize = 32;
            if (!Int32.TryParse(page, out pageno)) pageno = 1;
            pagesize = (pageno == 1 ? pagesize : 96);

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            //int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
            IList<Models.RelationDisplayObj> list = Models.DisplayObjProvider.getFavorMeList(userid, pagesize, pageno);
            //页面数据对象
            Models.PageModels.MemberRelationsModel relationsModel = new Models.PageModels.MemberRelationsModel(
                    memberDisplayObj,
                    list);
            relationsModel.UserID = userid;
            relationsModel.MemberID = userid;
            relationsModel.AlreadyLogon = alreadylogin;
            relationsModel.Pagger = new Models.PaggerObj();
            relationsModel.Member.MemberFavoredMeCount = Models.DisplayObjProvider.getFavorMeCount(userid);
            int pages = (int)Math.Ceiling((double)relationsModel.Member.MemberFavoredMeCount / pagesize);
            relationsModel.Pagger.PageCount = pages;
            relationsModel.Pagger.PageSize = pagesize;
            relationsModel.Pagger.PageNo = pageno;
            relationsModel.Pagger.PageUrl = "/Relation/Fans";
            #endregion

            return View(relationsModel);
        }
        [Authorize]
        public ActionResult FansGlamourToMeRank(String memberID, String skin)
        {
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            if (memberID == null) { memberID = userId; }
            Dictionary<String, float> memberGlamourCounts = 
                BiZ.Member.GlamourCounts.GlamourCountProvider.GetMembersForTaContributionGlamourCount(memberID);

            //String[] arrGlamourCount;
            //if (memberGlamourCounts.Count > 0)
            //{
            //    foreach (var mGCObj in memberGlamourCounts)
            //    {
            //        IList<BiZ.InterestCenter.Interest> interestList = new List<BiZ.InterestCenter.Interest>();
            //        BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(mILObj.Key);
            //        foreach (var interestList in mILObj.Value)
            //        {
            //            interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(interestobj.Key));
            //        }
            //        memberList.Add(memberobj, interestlist);
            //    }
            //}
            //Models.PageModels.InterestOverMeMemberModel interestmembermodel = new Models.PageModels.InterestOverMeMemberModel(memberList);
            //interestmembermodel.UserID = userid;
            //interestmembermodel.MemberID = id != null ? id : userid;
            //interestmembermodel.AlreadyLogon = alreadyLogin;
            //return View(interestmembermodel);

            //Dictionary<String, Dictionary<BiZ.Member.MemberInfomation, IList<BiZ.InterestCenter.Interest>>> memberGlamourInterestList = new Dictionary<String, Dictionary<BiZ.Member.MemberInfomation, IList<BiZ.InterestCenter.Interest>>>();
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            List<String> memberGlamourCountList = new List<String>();
            Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>> memberInterestList = new Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>>();
            BiZ.Member.Member memberInfoObj;
            IList<BiZ.InterestCenter.Interest> interestList;
            foreach (var memberGlamourCount in memberGlamourCounts)
            {
                memberInfoObj = BiZ.MemberManager.MemberManager.GetMember(memberGlamourCount.Key);
                //if ((memberDisplayObj.Sex == memberInfoObj.Sex) || (memberGlamourCount.Key == memberID)) continue;
                if (memberGlamourCount.Key == memberID) continue;
                interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberGlamourCount.Key, 5, 1);
                memberInterestList.Add(memberInfoObj, interestList);
                memberGlamourCountList.Add(memberGlamourCount.Key + "|" + memberGlamourCount.Value);
                if (memberGlamourCountList.Count >= 3) break;
            }

            Models.PageModels.GlamourCountsModel memberGlamourInterestModel = new Models.PageModels.GlamourCountsModel(memberDisplayObj, skin, memberGlamourCountList, memberInterestList);
            memberGlamourInterestModel.UserID = userId;
            memberGlamourInterestModel.MemberID = memberID;
            memberGlamourInterestModel.AlreadyLogon = alreadyLogin;

            return View(memberGlamourInterestModel);
        }
        #endregion

        #region 数据与业务方法
        public bool isalreadylogin(string role)
        {
            IPrincipal contextUser = HttpContext.User;
            var id = HttpContext.User.Identity as FormsIdentity;
            if (id != null && id.IsAuthenticated)
            {
                var roles = id.Ticket.UserData.Split(',');
                if (roles[0] == role)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult GetFavoredList(String pageSize, String pageNo)
        {
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userid = HttpContext.User.Identity.Name;
            IList<Models.RelationDisplayObj> list = Models.DisplayObjProvider.getIFavoredList(userid, pSize, pNo);
            List<Models.RelationDisplayObj> list2 = new List<Models.RelationDisplayObj>();
            foreach (Models.RelationDisplayObj rdo in list)
            {
                if ((rdo.FromMember != rdo.ToMember) && rdo.DisplayFromOrTo == "to")
                {
                    if (Models.DisplayObjProvider.IsInFavor(rdo.FromMember, rdo.ToMember))
                        rdo.IsFavor = true;
                    else
                        rdo.IsFavor = false;
                    list2.Add(rdo);
                }
            }

            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            return Json(jsSer.Serialize(list2)); 
        }
        #endregion

        #region 关系操作方法
        [HttpPost]
        public ActionResult GetVistors(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            return Json(Models.DisplayObjProvider.getVisitorListJson(mid, pagesize, pageno), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetVistors(String toMember, int pageSize, int pageNo)
        //{
        //    String memberID = HttpContext.User.Identity.Name;
        //    toMember = toMember == "" ? memberID : toMember;
        //    if (toMember == null) return RedirectToAction("Error", "Error", new { errNo="需要提供完整参数。" });

        //    IList<BiZ.Member.Relation.Visitor> visitorList = BiZ.Member.Relation.RelationProvider.GetVistors(toMember, 1, 0);
        //    String[] arrFromMember = null;
        //    for (int i = 0; i < visitorList.Count; i++)
        //    {
        //        arrFromMember[i] = visitorList[i].FromMember;
        //    }
        //    BiZ.MemberManager.MemberManager.GetMember(arrFromMember);
        //}
        [HttpPost]
        public ActionResult GetFavorers(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<BiZ.Member.Relation.Favorer> list = BiZ.Member.Relation.RelationProvider.GetFavorers(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetListWhoFavoredMe(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<Models.RelationDisplayObj> list = Models.DisplayObjProvider.getFavorMeList(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetGiftors(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<BiZ.Member.Relation.Giftor> list = BiZ.Member.Relation.RelationProvider.GetGiftors(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMySendedGifts(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<BiZ.Member.Relation.Giftor> list = BiZ.Member.Relation.RelationProvider.GetMySendedGifts(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetMarkers(int pagesize, int pageno)
        //{
        //    String mid = HttpContext.User.Identity.Name;
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    IList<BiZ.Member.Relation.Marker> list = BiZ.Member.Relation.RelationProvider.GetMarkers(mid, pagesize, pageno);
        //    return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetMyMarkedMembers(int pagesize, int pageno)
        //{
        //    String mid = HttpContext.User.Identity.Name;
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    IList<BiZ.Member.Relation.Marker> list = BiZ.Member.Relation.RelationProvider.GetMyMarkedMembers(mid, pagesize, pageno);
        //    return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult GetSilentors(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<BiZ.Member.Relation.Silentor> list = BiZ.Member.Relation.RelationProvider.GetSilentors(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetDisablers(int pagesize, int pageno)
        {
            String mid = HttpContext.User.Identity.Name;

            IList<BiZ.Member.Relation.Disabler> list = BiZ.Member.Relation.RelationProvider.GetDisablers(mid, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetNewMembersToMark(int sex, int pagesize, int pageno)
        {
            IList<BiZ.Member.Member> list = BiZ.MemberManager.MemberManager.GetNewUsersToMark(sex, pagesize, pageno);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFavoredCount(String fromMember)
        {
            if (fromMember == "" || fromMember == "null")
            {
                fromMember = HttpContext.User.Identity.Name;
            }
            long count = BiZ.Member.Relation.RelationProvider.GetFavorersCount(fromMember);
            return Json(new JavaScriptSerializer().Serialize(count), JsonRequestBehavior.AllowGet);
        }
        //[Authorize]
        //public ActionResult GetMarks(String mid)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    IList<BiZ.Mark.Mark> list = BiZ.MemberManager.MemberManager.GetMark(mid);
        //    return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}
