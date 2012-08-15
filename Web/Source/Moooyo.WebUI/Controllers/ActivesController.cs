using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class ActivesController : Controller
    {
        //
        // GET: /Actives/

        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图控制器
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult SuperMan()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            Models.PageModels.SuperManModel nowsupermodel = GetNowSuper(1);
            Models.PageModels.SuperManModel everydaysupermodel = GetEverydaySuper(1);
            Models.PageModels.SuperManModel model = new Models.PageModels.SuperManModel();
            model.UserID = User.Identity.Name;
            model.MemberID = User.Identity.Name;
            model.AlreadyLogon = true;
            model.nowsuperobj = nowsupermodel.nowsuperobj;
            model.nowsuperinterest = nowsupermodel.nowsuperinterest;
            model.nowsuperpagecount = nowsupermodel.nowsuperpagecount;
            model.nowsuperpageno = nowsupermodel.nowsuperpageno;
            model.nowsuperpagesize = nowsupermodel.nowsuperpagesize;
            model.nowsupercount = nowsupermodel.nowsupercount;
            model.everydaysuperobj = everydaysupermodel.everydaysuperobj;
            model.everydaysuperpagecount = everydaysupermodel.everydaysuperpagecount;
            model.everydaysuperpageno = everydaysupermodel.everydaysuperpageno;
            model.everydaysuperpagesize = everydaysupermodel.everydaysuperpagesize;
            model.everydaysupercount = everydaysupermodel.everydaysupercount;
            return View(model);
        }
        [Authorize]
        public ActionResult Groove()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            return View();
        }
        #endregion

        #region 数据与业务方法
        [Authorize]
        public ActionResult ShowNowSuper(int pageno)
        {
            return Json(new JavaScriptSerializer().Serialize(GetNowSuper(pageno)));
        }
        [Authorize]
        public ActionResult ShowEverydaySuper(int pageno)
        {
            return Json(new JavaScriptSerializer().Serialize(GetEverydaySuper(pageno)));
        }
        public Models.PageModels.SuperManModel GetNowSuper(int pageno)
        {
            int nowsupercount = 0;
            int nowsuperpagesize = 5;
            int nowsuperpagecount = 0;
            int nowsuperpageno = 0;
            if (pageno == 0) nowsuperpageno = 1;
            else nowsuperpageno = pageno;
            nowsupercount = Active.SuperMan.SuperFactory.GetSuper(0, 0).Count;
            nowsuperpagecount = nowsupercount % nowsuperpagesize == 0 ? (nowsupercount / nowsuperpagesize) : (nowsupercount / nowsuperpagesize) + 1;
            IList<Active.SuperMan.Super> list = Active.SuperMan.SuperFactory.GetSuper(nowsuperpagesize, nowsuperpageno);

            List<Models.PageModels.SuperManModel> nowsupers = new List<Models.PageModels.SuperManModel>();
            foreach (var obj in list) { nowsupers.Add(new Models.PageModels.SuperManModel(obj, BiZ.MemberManager.MemberManager.GetMember(obj.ToMemberID))); }
            IList<IList<BiZ.InterestCenter.Interest>> nowsuperinterests = new List<IList<BiZ.InterestCenter.Interest>>();
            foreach (var objs in list) { nowsuperinterests.Add(BiZ.InterestCenter.InterestFactory.GetInterestForMember(objs.ToMemberID, 20, 1)); }

            Models.PageModels.SuperManModel model = new Models.PageModels.SuperManModel();
            model.nowsuperobj = nowsupers;
            model.nowsuperinterest = nowsuperinterests;
            model.nowsuperpagecount = nowsuperpagecount;
            model.nowsuperpageno = nowsuperpageno;
            model.nowsuperpagesize = nowsuperpagesize;
            model.nowsupercount = nowsupercount;
            return model;
        }
        public Models.PageModels.SuperManModel GetEverydaySuper(int pageno)
        {
            int everydaysupercount = 0;
            int everydaysuperpagesize = 14;
            int everydaysuperpagecount = 0;
            int everydaysuperpageno = 0;
            if (pageno == 0) everydaysuperpageno = 1;
            else everydaysuperpageno = pageno;
            everydaysupercount = Active.SuperMan.SuperFactory.GetEveryDaySuper(0, 0).Count;
            everydaysuperpagecount = everydaysupercount % everydaysuperpagesize == 0 ? (everydaysupercount / everydaysuperpagesize) : (everydaysupercount / everydaysuperpagesize) + 1;
            IList<Active.SuperMan.Super> list = Active.SuperMan.SuperFactory.GetEveryDaySuper(everydaysuperpagesize, everydaysuperpageno);

            List<Models.PageModels.SuperManModel> everydaysupers = new List<Models.PageModels.SuperManModel>();
            foreach (var obj in list) { everydaysupers.Add(new Models.PageModels.SuperManModel(obj, BiZ.MemberManager.MemberManager.GetMember(obj.ToMemberID))); }

            Models.PageModels.SuperManModel model = new Models.PageModels.SuperManModel();
            model.everydaysuperobj = everydaysupers;
            model.everydaysuperpagecount = everydaysuperpagecount;
            model.everydaysuperpageno = everydaysuperpageno;
            model.everydaysuperpagesize = everydaysuperpagesize;
            model.everydaysupercount = everydaysupercount;
            return model;
        }

        [Authorize]
        public ActionResult AddGroove(string content)
        {
            String userid = HttpContext.User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.Sys.MemberContent.MemberContentFactory.AddMemberContent(22, "", content, userid);

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                userid,
                "",
                BiZ.Sys.MemberActivity.MemberActivityType.SubmitOpinion,
                "/Content/TaContent/" + userid);

            return Json(new JavaScriptSerializer().Serialize(result));
        }

        [Authorize]
        public ActionResult GetGroove(int pagesize,int pageno)
        {
            String userid = HttpContext.User.Identity.Name;
            List<Moooyo.BiZ.Sys.MemberContent.MemberContent> list = new List<BiZ.Sys.MemberContent.MemberContent>();
            list = BiZ.Sys.MemberContent.MemberContentFactory.getMemberContentsByuserId(22,userid,pagesize,pageno);
            return Json(new JavaScriptSerializer().Serialize(list));
        }

        [Authorize]
        public ActionResult GetGrooveCount()
        {
            String userid = HttpContext.User.Identity.Name;
            long listcount = BiZ.Sys.MemberContent.MemberContentFactory.getMemberContentsByuserIdCount(22, userid);
            return Json(new JavaScriptSerializer().Serialize(listcount));
        }
        #endregion
    }
}
