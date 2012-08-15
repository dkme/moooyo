using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class MsgController : Controller
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
        public ActionResult MessagesList(String you, String page)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;
            int pageNo = 1, pageSize = 27;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            if (you == null)
            {
                IList<BiZ.Member.Relation.LastMsger> lastmsger = BiZ.Member.Relation.RelationProvider.GetLastMsgers(userid, 1, 1);
                if (lastmsger.Count > 0)
                {
                    you = lastmsger[0].ToMember == userid ? lastmsger[0].FromMember : lastmsger[0].ToMember;
                }
            }
            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.MemberFullDisplayObj yourObj = null;
            if (you != null) yourObj = Models.DisplayObjProvider.getMemberFullDisplayObj(you);
            double pageCount = BiZ.Member.Relation.RelationProvider.GetLastMsgers(userid, 0, 0).Count;
            pageCount += BiZ.Sys.SystemMsg.SystemMsgProvider.GetMsgCount(userid);

            //页面数据对象
            Models.PageModels.MsgsModel model = new Models.PageModels.MsgsModel(
                    memberDisplayObj,
                    yourObj);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.pageTotal = (long)pageCount;
            model.Pagger = new Models.PaggerObj();
            int pageCounts = (int)Math.Ceiling(pageCount / pageSize);
            model.Pagger.PageCount = pageCounts;
            model.Pagger.PageSize = pageSize;
            model.Pagger.PageNo = pageNo;
            model.Pagger.PageUrl = "/Msg/MessagesList/" + you + "/";
            #endregion

            //被访问时数据处理
            BiZ.MemberManager.MemberManager.SetUnReadSystemMsgCountZero(userid);

            return View(model);
        }
        [Authorize]
        public ActionResult MessageDetails(String you, String page)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;
            int pageNo = 1, pageSize = 18;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            if (you == null)
            {
                IList<BiZ.Member.Relation.LastMsger> lastmsger = BiZ.Member.Relation.RelationProvider.GetLastMsgers(userid, 1, 1);
                if (lastmsger.Count > 0)
                {
                    you = lastmsger[0].ToMember == userid ? lastmsger[0].FromMember : lastmsger[0].ToMember;
                }
            }
            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.MemberFullDisplayObj yourObj = null;
            if (you != null) yourObj = Models.DisplayObjProvider.getMemberFullDisplayObj(you);
            double pageCount = BiZ.Member.Link.MsgProvider.GetMsgs(userid, you, 0, 0).Count;

            //页面数据对象
            Models.PageModels.MsgsModel model = new Models.PageModels.MsgsModel(
                    memberDisplayObj,
                    yourObj);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.pageTotal = (long)pageCount;
            model.Pagger = new Models.PaggerObj();
            int pageCounts = (int)Math.Ceiling(pageCount / pageSize);
            model.Pagger.PageCount = pageCounts;
            model.Pagger.PageSize = pageSize;
            model.Pagger.PageNo = pageNo;
            model.Pagger.PageUrl = "/Msg/MessageDetails/" + you + "/";
            #endregion

            return View(model);
        }
        [Authorize]
        public ActionResult SystemMsgs(string p)
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
            model.Pagger.PageUrl = "/Msg/SystemMsgs";
            #endregion

            //被访问时数据处理
            BiZ.MemberManager.MemberManager.SetUnReadSystemMsgCountZero(userid);

            return View(model);
        }
        //[Authorize]
        //public ActionResult Vistors(string p)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        //    #endregion

        //    //页码
        //    int pageno = 1;
        //    if (!Int32.TryParse(p, out pageno)) pageno = 1;

        //    //只允许已登录用户访问自己
        //    bool alreadylogin = true;
        //    String userid = HttpContext.User.Identity.Name;

        //    #region 构造页面数据对象
        //    Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
        //    int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
        //    IList<Models.RelationDisplayObj> vistorlist = Models.DisplayObjProvider.getVisitorList(userid, pagesize, pageno);
        //    //页面数据对象
        //    Models.PageModels.VistorModel model = new Models.PageModels.VistorModel(
        //            memberDisplayObj,
        //            vistorlist);
        //    model.UserID = userid;
        //    model.MemberID = userid;
        //    model.AlreadyLogon = alreadylogin;
        //    model.Pagger = new Models.PaggerObj();
        //    int pages = (int)Math.Floor((double)BiZ.Member.Relation.RelationProvider.GetVistorsCount(userid) / pagesize);
        //    model.Pagger.PageCount = pages + 1;
        //    model.Pagger.PageSize = pagesize;
        //    model.Pagger.PageNo = pageno;
        //    model.Pagger.PageUrl = "/Msg/Vistors";
        //    #endregion

        //    //被访问时数据处理
        //    BiZ.MemberManager.MemberManager.SetUnReadBeenViewedTimesZero(userid);
        //    //BiZ.MemberManager.MemberManager.SetUnReadMarkCountZero(userid);
        //    //BiZ.MemberManager.MemberManager.SetUnReadScoreCountZero(userid);

        //    return View(model);
        //}
        [Authorize]
        public ActionResult AboutMeActivity(string p)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //页码
            int pageno = 1, pagesize = 6;
            if (!Int32.TryParse(p, out pageno)) pageno = 1;
            pagesize = (pageno == 1 ? pagesize : 18);

            //只允许已登录用户访问自己
            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            //int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RelationPageSize"));
            int pageTotal = BiZ.Member.Activity.ActivityController.GetMemberRelationActivitysCount(userid);
            //pageTotal += BiZ.Member.Relation.RelationProvider.GetVistorsCount(userid);

            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> aboutMeList = BiZ.Member.Activity.ActivityController.GetMemberRelationActivitys(userid, pagesize, pageno);
            //var qAboutMeList = (
            //    from aboutMe in aboutMeList orderby aboutMe.LastOperationTime, aboutMe.Date descending select aboutMe);
            //List<BiZ.Member.Activity.ActivityHolderRelatedToMe> aboutMeList2 = new List<BiZ.Member.Activity.ActivityHolderRelatedToMe>();
            //aboutMeList2.AddRange(qAboutMeList);

            IList<Models.RelationDisplayObj> relatList = Models.DisplayObjProvider.GetAboutMeActivityList(userid, pagesize, pageno);
            //List<String> aboutMeStrList = new List<String>();
            //foreach (BiZ.Member.Activity.ActivityHolderRelatedToMe aboutMe in aboutMeList)
            //{
            //    aboutMeStrList.Add(MemberController.GetActivityStr(aboutMe));
            //}

            //页面数据对象
            Models.PageModels.MyActivitysModel model = new Models.PageModels.MyActivitysModel(
                    memberDisplayObj,
                    aboutMeList,
                    relatList//r,
                    //aboutMeStrList
                    );
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = alreadylogin;
            model.pageTotal = (long)pageTotal;
            model.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling((double)pageTotal / pagesize);
            model.Pagger.PageCount = pages;
            model.Pagger.PageSize = pagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Msg/MyActivitys";
            #endregion

            //被访问时数据处理
            BiZ.MemberManager.MemberManager.SetUnReadBeenViewedTimesZero(userid);
            //重置计数器
            BiZ.MemberManager.MemberManager.SetUnReadActivitysAboutMeCountZero(userid);

            return View(model);
        }
        //[Authorize]
        //public ActionResult AboutMeActivityPanel(BiZ.Member.Activity.ActivityHolderRelatedToMe obj)
        //{
        //    BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(obj.FromMemberID);
        //    if (member == null) return null;
        //    Models.RelationDisplayObj displayObj = Models.DisplayObjProvider.getRelationDisplayObj(obj.MemberID, member);
        //    Models.PageModels.MyActivityListPanelModel listobj = new Models.PageModels.MyActivityListPanelModel(displayObj, obj);
        //    return View(listobj);
        //}
        [Authorize]
        public ActionResult AboutMeActivityPanel(
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> aObjs, 
            IList<Models.RelationDisplayObj> rObjs)
        {
            Models.PageModels.MyActivityListPanelModel listobj = new Models.PageModels.MyActivityListPanelModel(aObjs, rObjs);
            return View(listobj);
        }
        //[Authorize]
        //public ActionResult I(String you)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
        //    #endregion

        //    String mid = HttpContext.User.Identity.Name;
        //    Models.MsgerDisplayObj obj = Models.DisplayObjProvider.getMsgerDisplayObj(mid, you);
        //    BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(mid);
        //    ViewData["me"] = mid;
        //    ViewData["you"] = you;
        //    ViewData["metype"] = me.MemberType;
        //    ViewData["hot"] = obj.Hot;
        //    ViewData["iwantsexstr"] = me.Sex == 1 ? "男生" : "女生";
        //    if (me.Status != null)
        //        ViewData["metotalmsgcount"] = me.Status.Last24HOutCallsCount;
        //    else
        //        ViewData["metotalmsgcount"] = 0;

        //    ViewData["NomalLevelMaxSendMsgLimit"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("NomalLevelMaxSendMsgLimit");

        //    return View(obj);
        //}
        #endregion

        #region 数据与业务方法
        //[Authorize]
        //public ActionResult GetLastMsgers(string pagesize, string pageno)
        //{
        //    int size = 0;
        //    if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    int no = 0;
        //    if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String mid = HttpContext.User.Identity.Name;
        //    return Json(Models.DisplayObjProvider.getLastMsgersJson(mid, size, no));
        //}
        [HttpGet]
        public ActionResult GetPrivateAndSysMesges(string pagesize, string pageno)
        {
            int size = 0;
            if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int no = 0;
            if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mid = HttpContext.User.Identity.Name;
            return Json(Models.DisplayObjProvider.GetPrivateAndSysMesgesJson(mid, size, no), JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        public ActionResult GetLastMsgerTo(string you)
        {
            String mid = HttpContext.User.Identity.Name;
            return Json(Models.DisplayObjProvider.getMsgerJson(mid, you));
        }
        [Authorize]
        [HttpPost]
        public ActionResult GetMsgs(string you, string pagesize, string pageno)
        {
            int size = 0;
            if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int no = 0;
            if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String me = HttpContext.User.Identity.Name;

            BiZ.Member.Relation.RelationProvider.SetMyUnreadMsgCountZero(me, you);
            return Json(Models.DisplayObjProvider.getMsgsJson(me, you, size, no));
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetMessageCount(string you)
        {
            String me = HttpContext.User.Identity.Name;
            long msgCount = BiZ.Member.Link.MsgProvider.GetMsgs(me, you, 0, 0).Count;
            return Json(msgCount, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        public ActionResult GetLastMsageCount()
        {
            String me = HttpContext.User.Identity.Name;
            long msgCount = BiZ.Member.Relation.RelationProvider.GetLastMsgers(me, 0, 0).Count;
            return Json(msgCount);
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteLastMsger(string you)
        {
            String mid = HttpContext.User.Identity.Name;
            return Json(BiZ.Member.Relation.RelationProvider.DeleteLastMsger(mid, you));
        }
        [HttpPost]
        public ActionResult GetAboutMeActivity(String pageSize, String pageNo)
        {
            int size = 0;
            if (!int.TryParse(pageSize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int no = 0;
            if (!int.TryParse(pageNo, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> objs = BiZ.Member.Activity.ActivityController.GetMemberRelationActivitys(
                userId, size, no);
            //var qAboutMeList = (
            //    from aboutMe in objs orderby aboutMe.LastOperationTime, aboutMe.Date descending select aboutMe);
            //List<BiZ.Member.Activity.ActivityHolderRelatedToMe> aboutMeList = new List<BiZ.Member.Activity.ActivityHolderRelatedToMe>();
            //aboutMeList.AddRange(qAboutMeList);

            IList<Models.RelationDisplayObj> rObjs = Models.DisplayObjProvider.GetAboutMeActivityList(userId, size, no);
            List<String> aboutMeActivityStrList = new List<String>();

            for (int i = 0; i < objs.Count; i++)
            {
                String actiStr = ActivityController.GetActivityStr(objs[i], rObjs[i]);
                aboutMeActivityStrList.Add(actiStr);
            }

            Models.PageModels.MyActivityListPanelModel obj = new Models.PageModels.MyActivityListPanelModel(
                objs, rObjs, aboutMeActivityStrList);

            return Json(new JavaScriptSerializer().Serialize(obj));
        }
        #endregion
    }
}
