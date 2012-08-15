using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Security.Principal;
using System.Web.Security;
using System.IO;
using System.Drawing;

namespace Moooyo.WebUI.Controllers
{
    [HandleError]
    public class InterestCenterController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        //[Authorize]
        //public ActionResult AddInterest()
        //{
        //    #region metas version
        //    SetMetasVersion();
        //    #endregion

        //    //只允许已登录用户访问自己
        //    bool alreadyLogin = true;
        //    String userId = HttpContext.User.Identity.Name;

        //    #region 构造页面数据对象
        //    Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
        //    IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(30, 1);
        //    //页面数据对象
        //    Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestClassList);
        //    interestModel.UserID = userId;
        //    interestModel.MemberID = userId;
        //    interestModel.AlreadyLogon = alreadyLogin;
        //    #endregion

        //    return View(interestModel);
        //}
        [Authorize]
        public ActionResult InterestFans(string id)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //if (iID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            //string mId = HttpContext.User.Identity.Name;
            //ViewData["iID"] = iID;
            //ViewData["mId"] = mId;

            string interestId;
            if (Request.QueryString["iid"] != null)
            {
                interestId = Request.QueryString["iid"].ToString();
                if (interestId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            //bool iffans = BiZ.InterestCenter.InterestFactory.IsFans(interestId, userId);
            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userId == null || userId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userId;
            }

            //单击兴趣时记录用户对兴趣的喜好数据
            BiZ.Recommendation.InterestTrainingData inttTrai = new BiZ.Recommendation.InterestTrainingData(id, interestId, BiZ.Recommendation.InterestTrainingDataType.Click);
            //单击兴趣时改变兴趣排名的分值
            CBB.RankingHelper.IRankingAble irk = BiZ.InterestCenter.InterestFactory.GetInterest(interestId);
            CBB.RankingHelper.RankingProvider.AddScores(irk, 1);
            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(interestId);
            //IList<BiZ.InterestCenter.InterestFans> interestHtxualFansList = BiZ.InterestCenter.InterestFactory.GetInterestFans(interestId, 14, 1);

            #region 获取兴趣问问
            string searchstr = "";
            if (Request.QueryString["search"] != null) searchstr = Server.UrlDecode(Request.QueryString["search"].ToString()).Trim();
            string page = Request.QueryString["pn"];
            //页码
            int pageNo = 1;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;
            int pageSize = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PlazaPageSize"));

            IList<BiZ.WenWen.WenWen> wenwenlist = new List<BiZ.WenWen.WenWen>();
            if (searchstr.Equals(""))
                wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWen(interestId, pageSize, pageNo);
            else
                wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWens(interestId, searchstr, pageSize, pageNo);
            #endregion

            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interest, null, wenwenlist);
            interestModel.UserID = userId;
            interestModel.MemberID = id;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.memberInterestObj = BiZ.InterestCenter.InterestFactory.GetMemberInterest(interestId, userId);
            //interestModel.ifmembertofans = iffans;
            //interestModel.interestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount(interestId);
            //interestModel.interestHtxualFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount(interestId);

            double wenwencount = 0;
            if (searchstr.Equals(""))
                wenwencount = (double)BiZ.WenWen.WenWenProvider.GetWenWen(interestId, 0, 0).Count;
            else
                wenwencount = (double)BiZ.WenWen.WenWenProvider.GetWenWens(interestId, searchstr, 0, 0).Count;
            interestModel.wenwencount = (int)wenwencount;

            interestModel.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling(wenwencount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            if (searchstr.Equals(""))
                interestModel.Pagger.PageUrl = "/InterestCenter/InterestFans/?iid=" + interestId + "&pn=";
            else
                interestModel.Pagger.PageUrl = "/InterestCenter/InterestFans/?iid=" + interestId + "&search=" + Server.UrlEncode(searchstr) + "&pn=";
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult InterestWenWen(String ifopenanswer, String wenwenid)
        {
            if (ifopenanswer != null && wenwenid != null)
                ViewData["openanswer"] = "true";
            else
                ViewData["openanswer"] = "false";
            string page = Request.QueryString["pn"];
            //页码
            int pageNo = 1;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;
            int pageSize = 10;

            string interestId;
            if (Request.QueryString["iid"] != null)
            {
                interestId = Request.QueryString["iid"].ToString();
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            //如果访问ID为空，则访问自己的主页
            #region 构造页面数据对象

            #region 获取兴趣问问
            IList<BiZ.WenWen.WenWen> wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWen(interestId, pageSize, pageNo);
            Dictionary<BiZ.WenWen.WenWen, IList<BiZ.WenWen.WenWenAnswer>> wenwens = new Dictionary<BiZ.WenWen.WenWen, IList<BiZ.WenWen.WenWenAnswer>>();
            foreach (BiZ.WenWen.WenWen obj in wenwenlist)
            {
                IList<BiZ.WenWen.WenWenAnswer> answers = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(obj.ID, 0, 0, 1);
                wenwens.Add(obj, answers);
            }
            #endregion

            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(wenwens);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            double interestCount = (double)BiZ.WenWen.WenWenProvider.GetWenWen(interestId, 0, 0).Count;
            int pages = (int)Math.Ceiling(interestCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            //interestModel.Pagger.PageUrl = "/InterestCenter/InterestWenWen/?iid=" + interestId + "" + (ifopenanswer != null && wenwenid != null ? "&ifopenanswer=true&wenwenid=" + wenwenid + "" : "") + "&pn=";
            #endregion
            return View(interestModel);
        }
        [Authorize]
        public ActionResult Interest(BiZ.InterestCenter.Interest interestObj, BiZ.InterestCenter.Interest memberInterestObj)
        {
            String userID = HttpContext.User.Identity.Name;
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(interestObj);
            interestModel.memberInterestObj = memberInterestObj;
            interestModel.UserID = userID;
            interestModel.isFans = BiZ.InterestCenter.InterestFactory.IsFans(interestObj.ID, userID);
            return View(interestModel);
        }
        [Authorize]
        public ActionResult Interests(string id, String type)
        {
            #region metas version
            SetMetasVersion();
            #endregion
            string page = Request.QueryString["pn"];
            //页码
            int pageNo = 1;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userId == null || userId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userId;
            }

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            int pageSize = 45;
            IList<BiZ.InterestCenter.Interest> interestList;
            switch (type)
            {
                case "meTaCommon": interestList = BiZ.InterestCenter.InterestFactory.GetIAndTACommonInterest(userId, id, pageSize, pageNo); break;
                default: interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(id, pageSize, pageNo); break;
            }

            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestList, type);
            interestModel.UserID = userId;
            interestModel.MemberID = id;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            double interestCount = (double)BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(id);
            int pages = (int)Math.Ceiling(interestCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            interestModel.Pagger.PageUrl = "/InterestCenter/Interests/" + id + "?pn=";
            interestModel.interestCount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(id);
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult InterestsForMy(string pageno)
        {
            int wenwenpageno = pageno != null ? int.Parse(pageno) : 1;
            #region metas version
            SetMetasVersion();
            #endregion
            //string type = Request.QueryString["type"] != null ? Request.QueryString["type"] : "1";
            string type = Request.QueryString["type"] != null ? Request.QueryString["type"] : "2";
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwens = new List<BiZ.WenWen.WenWen>();
            int wenwencount = 0;
            int pagesize = 20;
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userId, 0, 0);
            String[] ids = new String[interests.Count];
            for (int i = 0; i < interests.Count; i++) { ids[i] = interests[i].ID; }
            switch (type)
            {
                //case "1":
                //    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForInterests(ids, pagesize, wenwenpageno); 
                //    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForInterests(ids, 0, 0).Count; 
                //    break;
                case "2":
                    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userId, pagesize, wenwenpageno);
                    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userId, 0, 0).Count;
                    break;
                case "3":
                    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userId, pagesize, wenwenpageno);
                    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userId, 0, 0).Count;
                    break;
            }
            IList<BiZ.InterestCenter.Interest> interestlist = new List<BiZ.InterestCenter.Interest>();
            foreach (var obj in wenwens) { interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID)); }
            int pagecount = wenwencount % pagesize == 0 ? wenwencount / pagesize : wenwencount / pagesize + 1;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel(wenwens, interestlist, (long)interests.Count, wenwencount, pagecount, wenwenpageno);
            model.UserID = userId;
            model.MemberID = userId;
            model.AlreadyLogon = alreadyLogin;
            model.Member = memberDisplayObj;
            ViewData["type"] = type;
            ViewData["pagesize"] = pagesize;
            return View(model);
        }
        [Authorize]
        public ActionResult AllInterests(String ititle)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            string page = Request.QueryString["pn"] == null ? null : Request.QueryString["pn"].ToString();
            //页码
            int pageNo = 1;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
            int pageSize = 50;
            IList<BiZ.InterestCenter.Interest> interestList;
            double interestCount;
            if (ititle == null || ititle == "")
            {
                //按兴趣分类标题，兴趣粉丝创建时间降序分页获取多条兴趣
                interestList = BiZ.InterestCenter.InterestFactory.GetInterestClassSortedInterestFansCreatedTimeDesc("", pageSize, pageNo);
                interestCount = (double)BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            }
            else
            {
                //按兴趣标题，兴趣粉丝创建时间降序分页获取多条兴趣
                interestList = BiZ.InterestCenter.InterestFactory.GetInterestTitleSortedInterestFansCreatedTimeDesc(ititle, pageSize, pageNo);
                //按兴趣标题获取兴趣总数
                interestCount = BiZ.InterestCenter.InterestFactory.GetTitleInterestCount(ititle);
                ViewData["searchInterestCount"] = interestCount;
            }
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling(interestCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            if (ititle == null || ititle == "")
            {
                interestModel.Pagger.PageUrl = "/InterestCenter/AllInterests?pn=";
            }
            else
            {
                interestModel.Pagger.PageUrl = "/InterestCenter/AllInterests?ititle=" + ititle + "&pn=";
            }
            interestModel.allInterestCount = BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            interestModel.allInterestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount();
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult InterestFansGroup(string id)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            string interestId;
            if (Request.QueryString["iId"] != null)
            {
                interestId = Request.QueryString["iId"].ToString();
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }

            string page = Request.QueryString["pn"] == null ? null : Request.QueryString["pn"].ToString();
            //页码
            int pageNo = 1, pageSize = 6;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;

            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userId == null || userId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userId;
            }

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(interestId);
            //IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userId, 16, 1);
            IList<BiZ.InterestCenter.InterestFans> interestFansList = BiZ.InterestCenter.InterestFactory.GetInterestFans(interestId, pageSize, pageNo);
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interest, interestFansList);
            interestModel.UserID = userId;
            interestModel.MemberID = id;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            double interestFansCount = (double)BiZ.InterestCenter.InterestFactory.GetInterestFansCount(interestId);
            int pages = (int)Math.Ceiling(interestFansCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            interestModel.Pagger.PageUrl = "/InterestCenter/InterestFansGroup?iId=" + interestId + "&pn=";
            interestModel.memberInterestObj = BiZ.InterestCenter.InterestFactory.GetMemberInterest(interestId, userId);
            interestModel.interestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount(interestId);
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult MoreInterestClasses()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            string page = Request.QueryString["pn"] == null ? null : Request.QueryString["pn"].ToString();
            //页码和每页条数
            int pageNo = 1, pageSize = 40, skipNumber = 10;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
            IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(skipNumber, pageSize, pageNo);
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestClassList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            double interestClassCount = (double)BiZ.InterestCenter.InterestFactory.GetInterestClassCount(skipNumber);
            int pages = (int)Math.Ceiling(interestClassCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            interestModel.Pagger.PageUrl = "/InterestCenter/MoreInterestClasses?pn=";
            interestModel.allInterestCount = BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            interestModel.allInterestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount();
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult ClasseInterests(String ititle)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            string interestClassId;
            if (Request.QueryString["icid"] != null)
            {
                interestClassId = Request.QueryString["icid"].ToString();
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            string page = Request.QueryString["pn"] == null ? null : Request.QueryString["pn"].ToString();
            //页码
            int pageNo = 1, pageSize = 50;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);

            IList<BiZ.InterestCenter.Interest> interestList;
            double classIdInterestCount;
            if (ititle == null || ititle == "")
            {
                //按兴趣分类标题，兴趣粉丝创建时间降序分页获取多条兴趣
                interestList = BiZ.InterestCenter.InterestFactory.GetInterestClassIdSortedInterestFansCreatedTimeDesc(interestClassId, pageSize, pageNo);
                classIdInterestCount = (double)BiZ.InterestCenter.InterestFactory.GetClassIdInterestCount(interestClassId);
                ViewData["classIdInterestCount"] = classIdInterestCount;
            }
            else
            {
                //按兴趣标题，兴趣粉丝创建时间降序分页获取多条兴趣
                interestList = BiZ.InterestCenter.InterestFactory.GetInterestTitleSortedInterestFansCreatedTimeDesc(ititle, pageSize, pageNo);
                //按兴趣标题获取兴趣总数
                classIdInterestCount = BiZ.InterestCenter.InterestFactory.GetTitleInterestCount(ititle);
                ViewData["classIdInterestCount"] = classIdInterestCount;
            }
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling(classIdInterestCount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            if (ititle == null || ititle == "")
            {
                interestModel.Pagger.PageUrl = "/InterestCenter/ClasseInterests?icid=" + interestClassId + "&ictitle=" + Request.QueryString["ictitle"] + "&pn=";
            }
            else
            {
                interestModel.Pagger.PageUrl = "/InterestCenter/ClasseInterests?icid=" + interestClassId + "&ictitle=" + Request.QueryString["ictitle"] + "&ititle=" + ititle + "&pn=";
            }
            interestModel.allInterestCount = BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            interestModel.allInterestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount();
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult ModifyInterest()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            string interestId;
            if (Request.QueryString["iId"] != null)
            {
                interestId = Request.QueryString["iId"].ToString();
            }
            else
            {
                return null;
            }

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(interestId);
            IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(30, 1);
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interest, interestClassList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult LeftInterest(string memberID)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //页码和每页条数
            int pageNo = 1, pageSize = 10, memberPageNo = 1, memberInterestPageSize = 16;

            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            //如果访问ID为空，则访问自己的主页
            if (memberID == null)
            {
                if (userId == null || userId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    memberID = userId;
            }

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(pageSize, pageNo);
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberID, memberInterestPageSize, memberPageNo);
            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj, interestList, interestClassList);
            interestModel.UserID = userId;
            interestModel.MemberID = memberID;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.allInterestCount = BiZ.InterestCenter.InterestFactory.GetAllInterestCount();
            interestModel.memberInterestCount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(memberID);
            interestModel.allInterestFansCount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount();
            interestModel.interestClassCount = BiZ.InterestCenter.InterestFactory.GetInterestClassCount();
            #endregion

            return View(interestModel);
        }
        [Authorize]
        public ActionResult FeaturedInterestTopic(String sex, String pn)
        {
            #region Metas version
            SetMetasVersion();
            #endregion

            int publishedTopicSex = 0;

            String cookieFeaturedInterestTopic = Common.Comm.GetCookie("FeaturedInterestTopicMemberSex");
            if (cookieFeaturedInterestTopic == null)
            {
                if (sex == "" || sex == null)
                {
                    sex = "11";
                }
            }
            else
            {
                if (sex == "" || sex == null)
                {
                    sex = cookieFeaturedInterestTopic;
                }
            }

            switch (sex)
            {
                case "10": publishedTopicSex = 1; break;
                case "01": publishedTopicSex = 2; break;
                case "11": publishedTopicSex = 0; break;
                default: break;
            }

            Common.Comm.SetCookie("FeaturedInterestTopicMemberSex", sex, Common.CookieOrSessionExpiresTime.OneMonth);

            //Common.Comm.SetCookie("TopicToBoy", sex.Substring(0, 1), Models.CookieOrSessionExpiresTime.OneMonth);
            //Common.Comm.SetCookie("TopicToGirl", sex.Substring(1, 1), Models.CookieOrSessionExpiresTime.OneMonth);
            //String boycookie = Common.Comm.GetCookie("TopicToBoy");
            //String girlcookie = Common.Comm.GetCookie("TopicToGirl");
            //switch (boycookie + girlcookie)
            //{
            //    case "10": publishedTopicSex = 1; break;
            //    case "01": publishedTopicSex = 2; break;
            //    case "11": publishedTopicSex = 0; break;
            //    default: break;
            //}

            //只允许已经登录用户访问自己
            bool alreadyLogin = true;
            int pageNo = 1, pageSize = 10;

            if (!Int32.TryParse(pn, out pageNo)) pageNo = 1;
            pageSize = (pageNo == 1 ? 10 : 30);
            String userId = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);

            double pageCount = WenWenController.GetFeaturedInterestTopic(publishedTopicSex, 0, 0).Count;

            IList<CBB.RankingHelper.RankingList> dailyInterestRankingList = BiZ.Ranking.InterestRanking.GetDailyInterestRankingList(8); //获取兴趣日排名

            List<String> interestIdList = new List<string>();
            foreach (var interest in dailyInterestRankingList) interestIdList.Add(interest.ObjID);
            String[] interestIds = (String[])interestIdList.ToArray();
            //获取兴趣日排名
            IList<BiZ.InterestCenter.Interest> dailyInterestRankingInterestList = BiZ.InterestCenter.InterestFactory.GetInterest(interestIds);
            //按兴趣编号和发布用户的性别获取话题
            IList<BiZ.WenWen.WenWen> latestWenWenList = BiZ.WenWen.WenWenProvider.GetInterestIDSexTopics("", 4, 1, publishedTopicSex);
            //按兴趣话题发布者性别获取精选兴趣话题
            IList<BiZ.WenWen.WenWen> featuredInterestTopicList = WenWenController.GetFeaturedInterestTopic(publishedTopicSex, pageSize, pageNo);

            Dictionary<BiZ.WenWen.WenWen, BiZ.WenWen.WenWen> leftRightFeaturedTopicLists = WenWenController.TypesettingFeaturedInterestTopics(featuredInterestTopicList);
            List<Moooyo.BiZ.WenWen.WenWen> leftTopicList = leftRightFeaturedTopicLists.Keys.ToList();
            List<Moooyo.BiZ.WenWen.WenWen> rightTopicList = leftRightFeaturedTopicLists.Values.ToList();

            //页面数据对象
            Models.PageModels.MemberFeaturedInterestTopicModel memberFeaturedInterestTopicModel = new Models.PageModels.MemberFeaturedInterestTopicModel(memberDisplayObj, dailyInterestRankingList, dailyInterestRankingInterestList, latestWenWenList, leftTopicList, rightTopicList);

            memberFeaturedInterestTopicModel.UserID = userId;
            memberFeaturedInterestTopicModel.MemberID = userId;
            memberFeaturedInterestTopicModel.AlreadyLogon = alreadyLogin;
            memberFeaturedInterestTopicModel.Pagger = new Models.PaggerObj();
            int pageCounts = (int)Math.Ceiling(pageCount / pageSize);
            memberFeaturedInterestTopicModel.Pagger.PageCount = pageCounts;
            memberFeaturedInterestTopicModel.Pagger.PageSize = pageSize;
            memberFeaturedInterestTopicModel.Pagger.PageNo = pageNo;
            memberFeaturedInterestTopicModel.Pagger.PageUrl = "/InterestCenter/FeaturedInterestTopic/" + sex + "/";

            memberFeaturedInterestTopicModel.boyPublishedTopics = sex.Substring(0, 1);
            memberFeaturedInterestTopicModel.girlPublishedTopics = sex.Substring(1, 1);
            memberFeaturedInterestTopicModel.allMemberCount = BiZ.MemberManager.MemberManager.GetAllMemberCount();
            memberFeaturedInterestTopicModel.publishedTopicSex = publishedTopicSex;
            memberFeaturedInterestTopicModel.pagecount = Convert.ToInt32(pageCount);
            #endregion

            return View(memberFeaturedInterestTopicModel);
        }
        [Authorize]
        public ActionResult FeaturedInterestTopicNoImgPanel(BiZ.WenWen.WenWen topic)
        {
            ViewData["userId"] = HttpContext.User.Identity.Name;
            return View(topic);
        }
        [Authorize]
        public ActionResult FeaturedInterestTopicHaveImgPanel(BiZ.WenWen.WenWen topic)
        {
            ViewData["userId"] = HttpContext.User.Identity.Name;
            return View(topic);
        }
        public ActionResult ShowInterest(String iID)
        {
            SetMetasVersion();
            String userID = HttpContext.User.Identity.Name;
            if (iID == null || iID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = userID == null || userID == "" ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            BiZ.InterestCenter.Interest interestobj = BiZ.InterestCenter.InterestFactory.GetInterest(iID);
            IList<BiZ.InterestCenter.InterestFans> interestFans = BiZ.InterestCenter.InterestFactory.GetInterestFans(iID, 14, 1);
            IList<BiZ.InterestCenter.InterestFans> interestFansHot = BiZ.InterestCenter.InterestFactory.GetInterestFansToHot(iID, 14, 1);
            long interestCount = userID != null && userID != "" ? BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(userID) : 0;
            bool isfans = userID != null && userID != "" ? BiZ.InterestCenter.InterestFactory.IsFans(iID, userID) : true;
            IList<BiZ.InterestCenter.Interest> myInterest = userID != null && userID != "" ? BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0) : null;
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.Member = memberDisplayObj;
            model.AlreadyLogon = userID == "" || userID == null ? false : true;
            model.interestObj = interestobj;
            model.interestFansListObje = interestFans;
            model.interestHotFans = interestFansHot;
            model.interestCount = interestCount;
            model.isFans = isfans;
            model.interestlist = myInterest;
            return View(model);
        }
        #endregion

        #region 数据与业务方法
        [Authorize]
        public ActionResult InsertTopic(String interestid, String pattern)
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            String fileName = string.Empty;
            String imgstr = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (files != null && files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName = string.Empty;
                    HttpPostedFileBase fileobj = files[i];
                    if (fileobj.FileName != "")
                    {
                        fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + CBB.Security.MD5Helper.getMd5Hash(fileobj.FileName) + ".jpg";
                        Moooyo.BiZ.Photo.Photo topicimgobj = new UpController().SavePhoto(userid, (int)BiZ.Photo.PhotoType.TopicImage, fileobj.InputStream, fileName);
                        imgstr += topicimgobj.FileName + ",";
                    }
                }
            }
            String title = pattern.Length > 10 ? pattern.Substring(0, 10) + "..." : pattern;
            BiZ.WenWen.WenWen obj = BiZ.Member.Activity.ActivityController.AddQuestToMyInterest(userid, interestid, title, pattern, imgstr);
            return RedirectToAction("InterestFans", "InterestCenter", new { iid = interestid });
        }
        /// <summary>
        /// 获取兴趣话题
        /// </summary>
        /// <param name="pageno">页数</param>
        /// <param name="id">兴趣id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult GetWenWen(int pageno, String id)
        {
            int pageSize = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PlazaPageSize"));
            IList<BiZ.WenWen.WenWen> wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWen(id, pageSize, pageno);
            int wenwencount = BiZ.WenWen.WenWenProvider.GetWenWen(id, 0, 0).Count;
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel();
            model.wenwenpagecount = wenwencount % pageSize == 0 ? wenwencount / pageSize : (wenwencount / pageSize) + 1;
            model.wenwenlist = wenwenlist;
            return Json(new JavaScriptSerializer().Serialize(model));
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult AddMemberInterest(String title, String content, String classes, String iconid, string selfhoodPictureId, string selfhoodPicture)
        {
            String mid = HttpContext.User.Identity.Name;
            if (title == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (content == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (classes == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (iconid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (selfhoodPictureId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (selfhoodPicture == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            //获取用户创建的兴趣数量
            long interestcount = BiZ.InterestCenter.InterestFactory.GetInterestForMemberCount(mid);
            //定义用户创建兴趣所需的米果数集合
            Dictionary<int, int> InterestInsertToPoints = Common.Comm.getInterestInsertToPoints();
            int value = 0;
            if (Convert.ToInt32(interestcount) <= InterestInsertToPoints.Last().Key)
            {
                if (InterestInsertToPoints.ContainsKey(Convert.ToInt32(interestcount)))
                {
                    value = InterestInsertToPoints[Convert.ToInt32(interestcount)];
                }
                if (BiZ.MemberManager.MemberManager.GetMember(mid).Status.Points < value && value > 0)
                {
                    return RedirectToAction("AddInterestNoIntegral", "InterestCenter");
                }
                else
                {
                    String[] iconids = iconid.Split('|');
                    foreach (string id in iconids)
                    {
                        if (id != "")
                            iconid = id;
                    }
                    //比对去掉分类重复
                    String[] arrClasses = classes.Split(',');
                    List<String> listClasses = new List<String>();
                    foreach (String class1 in arrClasses)
                    {
                        if (!listClasses.Contains(class1)) listClasses.Add(class1);
                    }
                    String strClasses;
                    strClasses = String.Join(",", listClasses.ToArray());

                    BiZ.Photo.Photo myp = BiZ.Photo.PhotoManager.GetPhoto(iconid);
                    if (myp == null) return Json(-1);
                    //创建兴趣
                    BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.CreateInterest(mid, title, content, strClasses, iconid, myp.FileName, selfhoodPictureId, selfhoodPicture);
                    //添加兴趣时（同时对兴趣加粉）记录用户对兴趣的喜好数据
                    BiZ.Recommendation.InterestTrainingData inttTrai = new BiZ.Recommendation.InterestTrainingData(mid, interest.ID, BiZ.Recommendation.InterestTrainingDataType.CreateInterest);
                    //添加兴趣时（同时对兴趣加粉）并记录用户对兴趣的操作
                    BiZ.MemberManager.MemberManager.ModifyInterestCountCount(mid, BiZ.MemberManager.StatusModifyType.Add);
                    //添加兴趣时（同时对兴趣加粉）改变兴趣排名的分值
                    CBB.RankingHelper.IRankingAble irk = BiZ.InterestCenter.InterestFactory.GetInterest(interest.ID);
                    //分值改变
                    CBB.RankingHelper.RankingProvider.AddScores(irk, 3);
                    //添加兴趣时（同时对兴趣加粉），记录动态
                    //扣除米果
                    BiZ.MemberManager.MemberManager.ModifyPoints(mid, BiZ.MemberManager.StatusModifyType.Decrease, value);

                    //增加用户动态到后台
                    BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                        mid,
                        "",
                        BiZ.Sys.MemberActivity.MemberActivityType.CreateInterest,
                        "/InterestCenter/ShowInterest/" + interest.ID);

                    return Json(new JavaScriptSerializer().Serialize(interest));
                }
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errNo = "创建兴趣的数量已到上限，不能创建。" });
            }
        }
        [HttpPost]
        public ActionResult GetAllInterestClass()
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.GetAllInterestClass()));
        }
        [HttpPost]
        public ActionResult GetInterests(string mId, String pageSize, String pageNo)
        {
            if (mId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mId == "") { mId = HttpContext.User.Identity.Name; }
            if (pageSize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int pSize = 0, pNo = 0;
            if (!int.TryParse(pageSize, out pSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageNo, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.GetMemberInterest(mId, pSize, pNo)));
        }
        [HttpPost]
        public ActionResult GetAllInterestTitles(String q)
        {
            if (q == null || q == "") return Content("");
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            interests = BiZ.InterestCenter.InterestFactory.GetIgnoreCaseTitleInterest(q, 30, 1);
            String strInterestTitles = string.Empty;
            foreach (BiZ.InterestCenter.Interest interest in interests)
            {
                strInterestTitles += interest.Title + "\n";
            }
            return Content(strInterestTitles);
        }
        [HttpPost]
        public ActionResult AddInterestFans(string iID)
        {
            if (iID == null) return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            String mId = HttpContext.User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = AddInterestFansFunc(iID);

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                mId,
                "",
                BiZ.Sys.MemberActivity.MemberActivityType.AddInterest,
                "/InterestCenter/ShowInterest/" + iID);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        private CBB.ExceptionHelper.OperationResult AddInterestFansFunc(String iID)
        {
            String mId = HttpContext.User.Identity.Name;
            if (mId != null)
            {
                //获取当前登录用户的页面对象
                long interestcount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(mId);
                //获取用户兴趣达到一定数量后，再次添加所需的米果数集合
                Dictionary<int, int> interestaddfans = Common.Comm.getInterestAddFansToPoints();
                //根据用的情趣数量获取用户添加兴趣时所需的米果
                int value = 0;
                //判断加粉兴趣的数量是否超限
                if (Convert.ToInt32(interestcount) < interestaddfans.Last().Value)
                {
                    //加粉时记录用户对兴趣的喜好数据
                    BiZ.Recommendation.InterestTrainingData inttTrai = new BiZ.Recommendation.InterestTrainingData(mId, iID, BiZ.Recommendation.InterestTrainingDataType.AddToFansGroup);
                    //加粉时记录用户对兴趣的操作
                    BiZ.MemberManager.MemberManager.ModifyInterestCountCount(mId, BiZ.MemberManager.StatusModifyType.Add);
                    //加粉时改变兴趣排名的分值
                    CBB.RankingHelper.IRankingAble irk = BiZ.InterestCenter.InterestFactory.GetInterest(iID);
                    CBB.RankingHelper.RankingProvider.AddScores(irk, 3);
                    CBB.ExceptionHelper.OperationResult result = BiZ.Member.Activity.ActivityController.AddToInterestFansGroup(iID, mId);
                    //判断当前添加的兴趣是否需要扣除米果
                    bool ifin = interestaddfans.ContainsKey(Convert.ToInt32(interestcount));
                    if (ifin) { value = interestaddfans[Convert.ToInt32(interestcount)]; }
                    //获取当前登录用户的米果数量
                    var mypoints = BiZ.MemberManager.MemberManager.GetMember(mId).Status.Points;
                    if (ifin && mypoints >= value)
                    {
                        if (result.ok)
                        {
                            //兴趣添加成功后扣除相应的米果
                            BiZ.MemberManager.MemberManager.ModifyPoints(mId, BiZ.MemberManager.StatusModifyType.Decrease, value);
                        }
                    }
                    return result;
                }
            }
            return null;
        }
        [HttpPost]
        public ActionResult DelInterestFans(string iID)
        {
            if (iID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mId = HttpContext.User.Identity.Name;
            if (mId != null)
            {
                //删粉时记录用户对兴趣的操作
                BiZ.MemberManager.MemberManager.ModifyInterestCountCount(mId, BiZ.MemberManager.StatusModifyType.Decrease);
            }
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.DelInterestFans(iID, mId)));
        }
        [HttpPost]
        public ActionResult DelMemberInterestFans(string memberId, string interestId)
        {
            if (memberId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (interestId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            if (memberId != null)
            {
                //删粉时记录用户对兴趣的操作
                BiZ.MemberManager.MemberManager.ModifyInterestCountCount(memberId, BiZ.MemberManager.StatusModifyType.Decrease);
            }
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.DelInterestFans(interestId, memberId)));
        }
        public bool IsAlreadyLogin(string role)
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
        public ActionResult getinterestformy(string pageno, string type)
        {
            int wenwenpageno = pageno != null ? int.Parse(pageno) : 1;
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwens = new List<BiZ.WenWen.WenWen>();
            int wenwencount = 0;
            int pagesize = 20;
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userId, 0, 0);
            String[] ids = new String[interests.Count];
            for (int i = 0; i < interests.Count; i++) { ids[i] = interests[i].ID; }
            switch (type)
            {
                case "1":
                    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForInterests(ids, pagesize, wenwenpageno);
                    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForInterests(ids, 0, 0).Count;
                    break;
                case "2":
                    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userId, pagesize, wenwenpageno);
                    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userId, 0, 0).Count;
                    break;
                case "3":
                    wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userId, pagesize, wenwenpageno);
                    wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userId, 0, 0).Count;
                    break;
            }
            IList<BiZ.InterestCenter.Interest> interestlist = new List<BiZ.InterestCenter.Interest>();
            foreach (var obj in wenwens) { interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID)); }
            int pagecount = wenwencount % pagesize == 0 ? wenwencount / pagesize : wenwencount / pagesize + 1;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel(wenwens, interestlist, (long)interests.Count, wenwencount, pagecount, wenwenpageno);
            return Json(new JavaScriptSerializer().Serialize(model));
        }

        public bool GetMemberInterest(string iId)
        {
            bool isMyInterest;
            if (iId == null) return false;
            string mId = HttpContext.User.Identity.Name;
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetMemberInterest(iId, mId);
            //不是我的兴趣则interest为对象，反则interest为null
            if (interest != null) isMyInterest = true;
            else isMyInterest = false;
            return isMyInterest;
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult ModifyMemberInterest(String interestId, String title, String content, String classes, String iconid, String selfhoodPictureId, String selfhoodPicture)
        {
            String mId = HttpContext.User.Identity.Name;
            string iconName = "";
            if (title == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (content == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (classes == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (iconid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (selfhoodPictureId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (selfhoodPicture == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mId == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            if (iconid != "")
            {
                iconid = iconid.Replace("|", "");
                BiZ.Photo.Photo myp = BiZ.Photo.PhotoManager.GetPhoto(iconid);
                if (myp == null) return Json(-1);
                iconName = myp.FileName;
            }
            CBB.ExceptionHelper.OperationResult result = BiZ.InterestCenter.InterestFactory.UpdateInterest(mId, interestId, title, content, classes, iconid, iconName, selfhoodPictureId, selfhoodPicture);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        
        [HttpPost]
        public ActionResult GetInterest(string iID)
        {
            if (iID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterestCenter.InterestFactory.GetInterest(iID)));
        }
        public ActionResult GetInterestWenWen(String id)
        {
            IList<BiZ.WenWen.WenWen> wenwens = BiZ.WenWen.WenWenProvider.GetWenWen(id, 0, 0);
            return Json(new JavaScriptSerializer().Serialize(wenwens));
        }
        [HttpPost]
        public ActionResult IsFans(String iID)
        {
            if (iID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mId = HttpContext.User.Identity.Name;
            bool isFans = BiZ.InterestCenter.InterestFactory.IsFans(iID, mId);
            return Json(new JavaScriptSerializer().Serialize(isFans));
        }
        /// <summary>
        /// 更新兴趣粉丝的活跃度
        /// </summary>
        /// <param name="iid">兴趣编号</param>
        /// <param name="mid">用户编号</param>
        /// <returns></returns>
        [HttpPost]
        public static Boolean updateInterestHot(String iid,String mid)
        {
            return BiZ.InterestCenter.InterestFans.updateContentCount(iid, mid);
        }
        #endregion

        #region 添加新兴趣、兴趣加粉、兴趣管理
        public int getCreatedInterestToPoints(String userID)
        {
            //获取当前用户创建的兴趣组的数量
            long myCreatedInterestCount = BiZ.InterestCenter.InterestFactory.GetInterestForMemberCount(userID);
            //获取当前用户再次创建兴趣组时所需要的米果
            Dictionary<int, int> insertInterest = Common.Comm.getInterestInsertToPoints();
            //判断当前创建兴趣是否需要扣除米果
            bool ifin = insertInterest.ContainsKey(Convert.ToInt32(myCreatedInterestCount));
            int value = 0;
            if (ifin) { value = insertInterest[Convert.ToInt32(myCreatedInterestCount)]; }
            return value;
        }
        /// <summary>
        /// 用户兴趣加粉的页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddInterestFans()
        {
            SetMetasVersion();
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);

            Models.PageModels.Content.AddInterestFansModel model = new Models.PageModels.Content.AddInterestFansModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            ViewData["nowCreatedInterestToPoints"] = getCreatedInterestToPoints(userID);
            return View(model);
        }
        [HttpPost]
        public ActionResult getMyinterest()
        {
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.PageModels.Content.AddInterestFansModel model = getInterestModel(userID);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 兴趣加粉
        /// </summary>
        /// <param name="iID">兴趣编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddInterestFanss(string iID)
        {
            String mId = HttpContext.User.Identity.Name;
            if (iID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            CBB.ExceptionHelper.OperationResult result = AddInterestFansFunc(iID);
            if (result.ok)
            {
                return Json(new JavaScriptSerializer().Serialize(getInterestModel(mId)));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 获取兴趣加粉的页面模型
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        public Models.PageModels.Content.AddInterestFansModel getInterestModel(String userID)
        {
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0);
            //IList<IList<BiZ.InterestCenter.Interest>> interestLists = new List<IList<BiZ.InterestCenter.Interest>>();
            //foreach (BiZ.InterestCenter.Interest interest in interestList)
            //{
            //    interestLists.Add(BiZ.Recommendation.InterestRecommendation.GetElseInterestIDsMemberLikeWhereWhoLikeThis(interest.ID, 8));
            //}
            String[] iids = new String[interestList.Count];
            for (int i = 0; i < interestList.Count; i++)
            {
                var obj = interestList[i];
                if (obj != null) { iids[i] = obj.ID; }
            }
            IList<BiZ.InterestCenter.Interest> interestListTo = new List<BiZ.InterestCenter.Interest>();
            interestListTo = BiZ.Recommendation.InterestRecommendation.GetElseInterestIDsMemberLikeWhereWhoLikeThis(iids, 30);
            Models.PageModels.Content.AddInterestFansModel model = new Models.PageModels.Content.AddInterestFansModel();
            model.interestList = interestList;
            //model.interestLists = interestLists;
            model.interestListTo = interestListTo;
            return model;
        }

        /// <summary>
        /// 用户创建兴趣的页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddInterest()
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //获取用户创建的兴趣
            long interestcount = BiZ.InterestCenter.InterestFactory.GetInterestForMemberCount(userID);
            //定义用户创建兴趣所需的米果数集合
            Dictionary<int, int> InterestInsertToPoints = Common.Comm.getInterestInsertToPoints();
            int value = 0;
            if (interestcount <= InterestInsertToPoints.Last().Key)
            {
                if (InterestInsertToPoints.ContainsKey(Convert.ToInt32(interestcount)))
                {
                    value = InterestInsertToPoints[Convert.ToInt32(interestcount)];
                }
                if (memberDisplayObj.Points < value && value > 0)
                {
                    return RedirectToAction("AddInterestNoIntegral", "InterestCenter");
                }
            }
            else
            {
                ViewData["error_max"] = true;
                //return RedirectToAction("Error", "Error", new { errNo = "创建兴趣的数量已到上限，不能创建。" });
            }

            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0);
            IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(30, 1);
            //页面数据对象
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.interestClassListObje = interestClassList;
            model.interestlist = interestList;
            ViewData["nowCreatedInterestToPoints"] = getCreatedInterestToPoints(userID);
            return View(model);
        }
        /// <summary>
        /// 创建兴趣积分不够
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddInterestNoIntegral()
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //获取用户创建的兴趣
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterestForMember(userID, 0, 0);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.interestlist = interests;
            return View(model);
        }

        /// <summary>
        /// 显示当前登陆用户创建的兴趣组
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ShowMyInterest()
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //获取用户创建的兴趣
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterestForMember(userID, 0, 0);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.interestlist = interests;
            return View(model);
        }

        /// <summary>
        /// 修改兴趣
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult UpdateInterest(string interestid)
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            if (interestid == null || interestid == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0);
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(interestid);
            IList<BiZ.InterestCenter.InterestClass> interestClassList = BiZ.InterestCenter.InterestFactory.GetInterestClass(30, 1);
            //页面数据对象
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.interestObj = interest;
            model.interestClassListObje = interestClassList;
            model.interestlist = interestList;
            return View(model);
        }

        /// <summary>
        /// 管理兴趣粉丝(列表)
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult UpdateInterestFansIcon(string interestid)
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            if (interestid == null || interestid == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            int pagesize = 35;
            int pageno = 1;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //页面数据对象
            Models.PageModels.MemberInterestModel model = getInterestFansModel(interestid, pagesize, pageno);
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            return View(model);
        }
        /// <summary>
        /// 管理兴趣粉丝(详情)
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult UpdateInterestFansList(string interestid)
        {
            SetMetasVersion();
            //只允许已登录用户访问自己
            String userID = HttpContext.User.Identity.Name;
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            if (interestid == null || interestid == "")
                return RedirectToAction("Error", "Error", new { errNo = "需要提供完整参数。" });
            int pagesize = 15;
            int pageno = 1;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //页面数据对象
            Models.PageModels.MemberInterestModel model = getInterestFansModel(interestid, pagesize, pageno);
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            return View(model);
        }
        /// <summary>
        /// 加载兴趣管理中的粉丝
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateInterestFansAjax(string interestid, int pagesize, int pageno)
        {
            return Json(new JavaScriptSerializer().Serialize(getInterestFansModel(interestid, pagesize, pageno)));
        }
        /// <summary>
        /// 管理兴趣粉丝页面模型
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        public Models.PageModels.MemberInterestModel getInterestFansModel(string interestid, int pagesize, int pageno)
        {
            IList<BiZ.InterestCenter.InterestFans> interestfans = BiZ.InterestCenter.InterestFactory.GetInterestFans(interestid, 0, 0);
            long fanscount = interestfans.Count;
            string[] memberids = new string[fanscount];
            for (int i = 0; i < interestfans.Count; i++)
            {
                var obj = interestfans[i];
                if (obj != null && obj.Creater != null && obj.Creater.MemberID != null&&obj.Creater.MemberID!="")
                {
                    memberids[i] = obj.Creater.MemberID;
                }
            }
            long fanscounts = BiZ.MemberManager.MemberManager.GetMemberCountToIcon(memberids);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.interestObj = BiZ.InterestCenter.InterestFactory.GetInterest(interestid);
            //model.interestFansListObje = interestfans;
            //model.interestFansCount = fanscount;
            model.memberList = BiZ.MemberManager.MemberManager.GetMember(memberids, pageno, pagesize);
            model.pagesize = pagesize;
            model.pageno = pageno;
            model.pagecount = (int)(fanscounts % pagesize == 0 ? fanscounts / pagesize : (fanscounts / pagesize) + 1);
            return model;
        }
        #endregion
    }
}
