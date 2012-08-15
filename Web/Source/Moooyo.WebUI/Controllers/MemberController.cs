using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Security.Principal;
using MongoDB.Bson;

namespace Moooyo.WebUI.Controllers
{
    public class MemberController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }
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

        #region 视图方法
        [Authorize]
        public ActionResult I(String id, String pn)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //id参数检查
            if (id != null)
            {
                //按转换后的编号和编号类型获取原始的编号
                BiZ.Comm.UniqueNumber.UniqueNumber uNumberID = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(id, BiZ.Comm.UniqueNumber.IDType.MemberID);
                //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
                if (uNumberID != null) id = uNumberID.DefaultId;

                ObjectId objid;
                if (!ObjectId.TryParse(id, out objid)) return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            }

            bool alreadyLogin = isalreadylogin("Member");
            //ViewData["alreadylogin"] = alreadyLogin;

            String userid = HttpContext.User.Identity.Name;
            //ViewData["me"] = userid;

            if (!alreadyLogin)
            {
                return RedirectToAction("Login", "Account");
            }

            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userid == null || userid == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userid;
            }

            #region 是否浏览自己的主页
            bool isme = IsMe(id, userid);
            //ViewData["isMe"] = isme;
            //ViewData["mid"] = id;
            if (!isme)
            {
                return RedirectToAction("Ta", "Member", new { membId = id });
            }
            #endregion

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);

            #region 内测期跳转检测
            if (!mym.AllowLogin)
            {
                return RedirectToAction("Welcome", "Account");
            }
            #endregion

            if (alreadyLogin)
            {
                //增加来访
                if (!isme)
                {
                    BiZ.Member.Activity.ActivityController.VisitMember(userid, id);
                }
            }

            //页码
            int pageNo = 1;
            if (!Int32.TryParse(pn, out pageNo)) pageNo = 1;
            int pageSize = 6;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            IList<BiZ.Member.Activity.ActivityHolder> activityHolderList = BiZ.Member.Activity.ActivityController.GetFavorMemberActivitys(id, pageSize, pageNo);
            IList<BiZ.Member.Relation.Visitor> visitorList = BiZ.Member.Relation.RelationProvider.GetVistors(userid, 6, 1);
            foreach (var activityobj in activityHolderList)
            {
                activityobj.IsRealPhotoIdentification = BiZ.MemberManager.MemberManager.GetMember(activityobj.MemberID).MemberPhoto.IsRealPhotoIdentification;
            }
            //页面数据对象
            Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj, activityHolderList, visitorList);
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = alreadyLogin;
            memberModel.Member.MemberFavoredMeCount = Models.DisplayObjProvider.getFavorMeCount(userid);
            memberModel.Pagger = new Models.PaggerObj();
            double activityHolderCount = (double)BiZ.Member.Activity.ActivityController.GetFavorMemberActivitysCount(userid);
            int pages = (int)Math.Ceiling(activityHolderCount / pageSize);
            memberModel.Pagger.PageCount = pages;
            memberModel.Pagger.PageSize = pageSize;
            memberModel.Pagger.PageNo = pageNo;
            memberModel.Pagger.PageUrl = "/Member/I/" + userid;
            //按原始编号和编号类型获取转换后的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberID2 = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(userid, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if (uNumberID2 != null) memberModel.MemberUrl = uNumberID2.ConvertedID.ToString();
            memberModel.photoType = "99";
            #endregion

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Ta(String membId, String pn)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            if (membId == null || membId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            //id参数检查
            if (membId != null)
            {
                ObjectId objid;
                if (!ObjectId.TryParse(membId, out objid)) return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            }
            String userId = HttpContext.User.Identity.Name;
            if (userId == null || userId == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            #region 是否浏览自己的主页
            bool isme = IsMe(membId, userId);
            if (!isme)
            {
                //增加来访
                BiZ.Member.Activity.ActivityController.VisitMember(userId, membId);
                //用户点击他人时记录用户对该用户的关注度
                BiZ.Recommendation.MemberFavorTrainingData membFavoTrai = new BiZ.Recommendation.MemberFavorTrainingData(userId, membId, BiZ.Recommendation.MemberFavorTrainingDataType.Click);
            }
            else { return RedirectToAction("I", "Member"); }
            #endregion
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(membId);
            bool alreadyLogin = isalreadylogin("Member");

            //页码
            int pageNo = 1;
            if (!Int32.TryParse(pn, out pageNo)) pageNo = 1;
            //int pageSize = 6;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(membId);
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(membId, 8, 1);
            IList<BiZ.InterestCenter.Interest> commonInterestList = BiZ.InterestCenter.InterestFactory.GetIAndTACommonInterest(userId, membId, 15, 1);
            IList<BiZ.Photo.Photo> photoList = BiZ.Photo.PhotoManager.GetPhotos(membId, (BiZ.Photo.PhotoType)0, 4, 1);
            IList<BiZ.InterView.InterView> interViewList = BiZ.InterView.InterViewProvider.GetInterViews(membId, 12, 1);

            //页面数据对象
            Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj, interestList, photoList, interViewList, commonInterestList);
            memberModel.UserID = userId;
            memberModel.MemberID = membId;
            memberModel.AlreadyLogon = alreadyLogin;
            //按用户编号获取一条指定用户为Ta贡献了多少分魅力值
            memberModel.memberForTaContributionGlamourCount = BiZ.Member.GlamourCounts.GlamourCountProvider.GetMemberForTaContributionGlamourCount(userId, membId).ToString();
            //获取指定用户在TA的粉丝中排名
            memberModel.iInTaFansRank = BiZ.Member.GlamourCounts.GlamourCountProvider.GetIInTaFansRank(membId, userId).ToString();
            //按原始编号和编号类型获取转换后的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberID = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(membId, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if (uNumberID != null) memberModel.MemberUrl = uNumberID.ConvertedID.ToString();

            //memberModel.Pagger = new Models.PaggerObj();
            //double activityHolderCount = (double)BiZ.Member.Activity.ActivityController.GetFavorMemberActivitysCount(userid);
            //int pages = (int)Math.Ceiling(activityHolderCount / pageSize);
            //memberModel.Pagger.PageCount = pages;
            //memberModel.Pagger.PageSize = pageSize;
            //memberModel.Pagger.PageNo = pageNo;
            //memberModel.Pagger.PageUrl = "/Member/I/" + userid;
            #endregion

            return View(memberModel);

        }
        [Authorize]
        public ActionResult InterView(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            bool alreadylogin = isalreadylogin("Member");
            String userid = HttpContext.User.Identity.Name;

            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userid;
            }

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            IList<BiZ.InterView.InterView> intviewlist = BiZ.InterView.InterViewProvider.GetInterViews(id, 0, 0);
            string[] alreadyanswered = new string[intviewlist.Count];
            for (int i = 0; i < intviewlist.Count; i++)
            {
                alreadyanswered[i] = intviewlist[i].SystemQuestionID;
            }
            IList<BiZ.Sys.SystemInterView> systeminterviews = new SystemFuncController().GetSystemInterViewList("", alreadyanswered);
            //页面数据对象
            Models.PageModels.MemberInterViewModel interviewmodel = new Models.PageModels.MemberInterViewModel(
                    memberDisplayObj,
                    intviewlist,
                    systeminterviews);
            interviewmodel.UserID = userid;
            interviewmodel.MemberID = id;
            interviewmodel.AlreadyLogon = alreadylogin;
            #endregion

            return View(interviewmodel);
        }
        [Authorize]
        public ActionResult Interest(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            bool alreadylogin = isalreadylogin("Member");
            ViewData["alreadylogin"] = alreadylogin;

            if (!alreadylogin)
                return RedirectToAction("Login", "Account");

            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;

            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
                else
                    id = userid;
            }

            #region 是否浏览自己的主页
            bool isme = IsMe(id, userid);
            ViewData["isMe"] = isme;
            ViewData["mid"] = id;
            #endregion

            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            int pagesize = Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberInterestsPageSize"));
            IList<BiZ.InterestCenter.Interest> interestList =
                BiZ.InterestCenter.InterestFactory.GetMemberInterest(id, pagesize, 0);
            Models.PageModels.MemberInterestModel obj =
                new Models.PageModels.MemberInterestModel(memberDisplayObj, interestList);


            #region 用户城市、昵称、年龄、性别
            ViewData["membercity"] = memberDisplayObj.City;
            ViewData["nickname"] = memberDisplayObj.Name;
            ViewData["memberage"] = memberDisplayObj.Age;
            ViewData["sex"] = memberDisplayObj.Sex;
            ViewData["membersex"] = memberDisplayObj.SexStr;
            #endregion

            return View(obj);
        }
        //[Authorize]
        //public ActionResult Skill(String id)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        //    #endregion

        //    bool alreadylogin = isalreadylogin("Member");
        //    String userid = HttpContext.User.Identity.Name;
        //    bool ifmy = true;//判断访问的是不是自己的技能

        //    //如果访问ID为空，则访问自己的主页
        //    if (id == null)
        //    {
        //        if (userid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //        else
        //            id = userid;
        //    }
        //    else
        //        ifmy = false;

        //    #region 构造页面数据对象
        //    Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
        //    IList<BiZ.Skills.Skill> skilllist = BiZ.Skills.SkillProvider.GetSkills(id, 0, 0);
        //    IList<BiZ.Skills.Skill> knownlist = new List<BiZ.Skills.Skill>();
        //    IList<BiZ.Skills.Skill> wanttoknowlist = new List<BiZ.Skills.Skill>();
        //    List<String> tkowns = new List<String>();//还有谁会
        //    List<String> twants = new List<String>();//还有谁想学
        //    foreach (BiZ.Skills.Skill obj in skilllist)
        //    {
        //        if (obj.SkillType == BiZ.Skills.SkillType.IKnow)
        //        {
        //            knownlist.Add(obj);
        //            tkowns.Add(obj.SkillName);
        //        }
        //        else
        //        {
        //            wanttoknowlist.Add(obj);
        //            twants.Add(obj.SkillName);
        //        }
        //    }
        //    IList<BiZ.Skills.Skill> tkownlist = new List<BiZ.Skills.Skill>();
        //    IList<BiZ.Skills.Skill> twantlist = new List<BiZ.Skills.Skill>();
        //    String[] kowns = (String[])tkowns.ToArray();
        //    String[] wants = (String[])twants.ToArray();
        //    //如果是访问自己的技能
        //    if (ifmy)
        //    {
        //        //加载那些人想学
        //        tkownlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyWantLearn(id, kowns, 100);
        //        //加载那些人会
        //        twantlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyKnows(id, wants, 100);
        //    }
        //    //如果访问别人的技能
        //    else
        //    {
        //        //加载那些人也会
        //        tkownlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyKnows(id, kowns, 100);
        //        //加载那些人也想学
        //        twantlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyWantLearn(id, wants, 100);
        //    }

        //    //页面数据对象
        //    Models.PageModels.MemberSkillModel model = new Models.PageModels.MemberSkillModel(
        //            memberDisplayObj,
        //            knownlist,
        //            wanttoknowlist,
        //            tkownlist,
        //            twantlist);
        //    model.UserID = userid;
        //    model.MemberID = id;
        //    model.AlreadyLogon = alreadylogin;
        //    #endregion

        //    return View(model);
        //}
        //[Authorize]
        //public ActionResult SayHi(String you, String type)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
        //    #endregion

        //    ViewData["you"] = you;

        //    BiZ.Sys.HiType hitype = BiZ.Sys.HiType.m_normal;
        //    if (type == null)
        //    {
        //        String mid = HttpContext.User.Identity.Name;
        //        BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);
        //        if (mym.Sex == 2) hitype = BiZ.Sys.HiType.f_normal;
        //        ViewData["sexnum"] = (int)mym.Sex;
        //    }
        //    else
        //    {
        //        hitype = (BiZ.Sys.HiType)Int32.Parse(type);
        //        ViewData["sexnum"] = Math.Floor((int)hitype / 10.0);
        //    }
        //    ViewData["type"] = (int)hitype;

        //    return View();
        //}
        [Authorize]
        public ActionResult DateTo(String you)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion

            ViewData["you"] = you;
            BiZ.Member.Member yo = BiZ.MemberManager.MemberManager.GetMember(you);
            ViewData["youstr"] = yo.MemberInfomation.IWant;

            String mid = HttpContext.User.Identity.Name;
            ViewData["me"] = mid;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);
            ViewData["mestr"] = mym.MemberInfomation.IWant;

            return View();
        }
        //[Authorize]
        //public ActionResult SelectSkill(String containername)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
        //    #endregion
        //    ViewData["containername"] = containername;
        //    return View();
        //}
        [Authorize]
        public ActionResult SelectIWant(String containername)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion
            ViewData["containername"] = containername;
            return View();
        }
        [Authorize]
        public ActionResult SetFansGroupName()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
            #endregion
            return View();
        }
        [Authorize]
        public ActionResult ShowMemberCove(String id)
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            bool isme = IsMe(id, userid);
            ViewData["isMe"] = isme;
            ViewData["mid"] = id;

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);
            if (mym == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            ViewData["nickname"] = mym.MemberInfomation.NickName;
            ViewData["iconpath"] = mym.MemberInfomation.IconPath.Split('.')[0] + "_p" + ".jpg";
            ViewData["mid"] = mym.ID;
            //ViewData["scoreavg"] = mym.Status.ScoreAvg.ToString("#0.0");
            //ViewData["markdata"] = new JavaScriptSerializer().Serialize(mym.MemberRelations.Marks);

            return View();
        }
        [Authorize]
        public ActionResult Upgrade()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            String userid = HttpContext.User.Identity.Name;
            ViewData["me"] = userid;
            ViewData["mid"] = userid;

            return View();
        }
        [Authorize]
        public ActionResult WenWen()
        {
            string memberID = HttpContext.User.Identity.Name;
            string _pageno = Request.QueryString["p"];
            string _type = Request.QueryString["t"];
            int type = 1;
            int pageno = 1;
            int pagesize = 10;
            if (!Int32.TryParse(_type, out type)) type = 1;
            if (!Int32.TryParse(_pageno, out pageno)) pageno = 1;

            IList<Moooyo.BiZ.WenWen.WenWen> listq;
            IList<Moooyo.BiZ.WenWen.WenWenAnswer> lista;
            Models.PageModels.InterestWenWenModel Model = null;
            int count = 0;
            if (type == 1)
            {
                listq = Moooyo.BiZ.WenWen.WenWenProvider.GetWenWenForMember(memberID, pagesize, pageno);
                Model = new Models.PageModels.InterestWenWenModel(listq);
                count = (int)Moooyo.BiZ.WenWen.WenWenProvider.GetWenWenAnswerForMemberCount(memberID);
            }
            else if (type == 2)
            {
                lista = Moooyo.BiZ.WenWen.WenWenProvider.GetWenWenAnswerForMember(memberID, pagesize, pageno);
                Model = new Models.PageModels.InterestWenWenModel(lista);
                count = Moooyo.BiZ.WenWen.WenWenProvider.GetWenWenCount(memberID);
            }
            Model.Pagger = new Models.PaggerObj();
            Model.Pagger.PageCount = (count + pagesize - 1) / pagesize;
            Model.Pagger.PageNo = pageno;
            Model.Pagger.PageSize = pagesize;
            Model.Pagger.PageUrl = "/Member/WenWen/?t=" + type + "&p=";
            ViewData["type"] = type;
            return View(Model);
        }
        [Authorize]
        public ActionResult MyInterest()
        {

            #region metas version
            SetMetasVersion();
            #endregion
            string topictoboy = "1";
            string topictogirl = "1";
            String boycookie = Common.Comm.GetCookie("TopicToBoy");
            String girlcookie = Common.Comm.GetCookie("TopicToGirl");

            if (boycookie != null) { topictoboy = boycookie; }
            if (girlcookie != null) { topictogirl = girlcookie; }

            if (Request.QueryString["topictoboy"] != null && Request.QueryString["topictoboy"] != "")
                topictoboy = Request.QueryString["topictoboy"];
            if (Request.QueryString["topictogirl"] != null && Request.QueryString["topictogirl"] != "")
                topictogirl = Request.QueryString["topictogirl"];

            Common.Comm.SetCookie("TopicToBoy", topictoboy, Common.CookieOrSessionExpiresTime.OneMonth);
            Common.Comm.SetCookie("TopicToGirl", topictogirl, Common.CookieOrSessionExpiresTime.OneMonth);

            ViewData["topictoboy"] = topictoboy;
            ViewData["topictogirl"] = topictogirl;
            Models.PageModels.MyInterestToWenWenModel model = getmtinterest(1, topictoboy == "1" ? true : false, topictogirl == "1" ? true : false);
            return View(model);
        }
        [Authorize]
        public ActionResult MemberDomain(String id)
        {
            String userId = HttpContext.User.Identity.Name;
            String memberId = "";
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int intNumMemberId = 0;
            if (Int32.TryParse(id, out intNumMemberId))
            {
                //按转换后的编号和编号类型获取原始的编号
                BiZ.Comm.UniqueNumber.UniqueNumber trfmId = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(id, BiZ.Comm.UniqueNumber.IDType.MemberID);
                //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
                if (trfmId != null)
                {
                    memberId = trfmId.DefaultId;
                    if (userId != null)
                    {
                        if (userId == memberId)
                            return RedirectToAction("IContent", "Content");
                    }
                    return RedirectToAction("TaContent", "Content", new { memberID = memberId });
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { errorno = "0" });
                }
            }

            //按转换后的编号和编号类型获取原始的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberID = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDomainDefaultId(id, BiZ.Comm.UniqueNumber.IDType.MemberID);
            //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
            if (uNumberID != null)
            {
                memberId = uNumberID.DefaultId;
                if (userId != null)
                {
                    if (userId == memberId)
                        return RedirectToAction("IContent", "Content");
                }
                return RedirectToAction("TaContent", "Content", new { memberID = memberId });
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
        }
        #endregion

        #region 数据与业务方法
        //获取鼠标移入时用户的信息
        [HttpPost]
        public ActionResult getMemberInfo(String membId)
        {

            if (membId == null || membId == "")
                return null;
            if (membId != null)
            {
                ObjectId objid;
                if (!ObjectId.TryParse(membId, out objid)) return null;
            }
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(membId);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(memberDisplayObj));
        }
        //获取距离
        [HttpPost]
        public ActionResult GetWeDistance(string meId, String heId)
        {
            String distance = Models.DisplayObjProvider.GetWeDistance(meId, heId);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(distance));
        }
        //是否已粉
        [HttpPost]
        public ActionResult IsInFavor(string meId, String taId)
        {
            bool isInFavor = BiZ.Member.Relation.RelationProvider.IsInFavor(meId, taId);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return Json(ser.Serialize(isInFavor));
        }
        //获取指定用户当前在线状态
        [HttpPost]
        public ActionResult GetOnlineStr(string memberid)
        {
            return Json(new JavaScriptSerializer().Serialize(BiZ.MemberManager.MemberManager.GetMember(memberid).OnlineStr));
        }
        [HttpPost]
        public ActionResult getmyinterest(int pageno, bool topictoboy, bool topictogirl)
        {
            return Json(new JavaScriptSerializer().Serialize(getmtinterest(pageno + 1, topictoboy, topictogirl)));
        }
        public Models.PageModels.MyInterestToWenWenModel getmtinterest(int pageno, bool topictoboy, bool topictogirl)
        {
            String userid = User.Identity.Name;
            int xqpagesize = 10, xqpageno = 1, xqpagecount = 1, xqcount = 0;
            xqpageno = pageno == 0 ? 1 : pageno;
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterestToWenWen(userid, xqpagesize, xqpageno, topictoboy, topictogirl);
            xqcount = (int)BiZ.InterestCenter.InterestFactory.GetMemberInterestToWenWenCount(userid, topictoboy, topictogirl);
            xqpagecount = xqcount % xqpagesize == 0 ? (int)(xqcount / xqpagesize) : (int)(xqcount / xqpagesize) + 1;
            Models.PageModels.MyInterestToWenWenModel model = new Models.PageModels.MyInterestToWenWenModel();
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = true;
            model.interests = interests;
            model.myinterest = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            model.interestcount = (int)BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(userid);
            model.interestcounttowenwens = xqcount;
            model.interestpagecount = xqpagecount;
            model.interestpagesize = xqpagesize;
            model.interestpageno = xqpageno;
            return model;
        }
        /// <summary>
        /// 积分增长进度值归零，增加给定的米果数
        /// </summary>
        /// <param name="points">增加的米果数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult setPointsScheduleZero(int points)
        {
            String mid=User.Identity.Name;
            if (mid != null)
            {
                CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetPointsScheduleZero(mid);
                if (result.ok)
                {
                    CBB.ExceptionHelper.OperationResult result1 = BiZ.MemberManager.MemberManager.ModifyPoints(mid, BiZ.MemberManager.StatusModifyType.Add, points);
                    if (result1.ok)
                    {
                        return Json(new JavaScriptSerializer().Serialize(true));
                    }
                }
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        #endregion

        #region profile

        #region 视图方法
        //[Authorize]
        //public ActionResult Profile()
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    ViewData["mid"] = HttpContext.User.Identity.Name;

        //    return View();
        //}
        //[Authorize]
        //public ActionResult ProfileControlRight(String mid, String showPhotowall)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    //profile样式
        //    ViewData["showPhotowall"] = showPhotowall;
        //    #region 是否浏览自己的主页
        //    String userid = HttpContext.User.Identity.Name;
        //    ViewData["isMe"] = IsMe(mid, userid);
        //    ViewData["mid"] = mid;
        //    bool alreadylogin = isalreadylogin("Member");

        //    ViewData["alreadylogin"] = alreadylogin;
        //    if (!alreadylogin & mid == null)
        //        return RedirectToAction("Login", "Account");
        //    #endregion

        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

        //    #region 用户头像
        //    if ((mym.MemberPhoto == null) || (mym.MemberPhoto.IconID == null) || (mym.MemberPhoto.IconID == ""))
        //        ViewData["iconurl"] = "/pics/noicon.jpg";
        //    else
        //    {

        //        if (mym.MemberInfomation != null & mym.MemberInfomation.IconPath != "")
        //        {
        //            ViewData["iconurl"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
        //            ViewData["bigimg"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath;
        //        }
        //        else
        //        {
        //            ViewData["iconurl"] = "/pics/noicon.jpg";
        //            ViewData["bigimg"] = "/pics/nobigimg.jpg";
        //        }
        //    }
        //    #endregion 用户头像
        //    #region 用户城市、昵称、年龄、性别
        //    ViewData["membercity"] = mym.MemberInfomation.City.Replace("|", " ");
        //    ViewData["membernickname"] = mym.MemberInfomation.NickName;
        //    ViewData["memberage"] = mym.MemberInfomation.Age.ToString();
        //    ViewData["sex"] = mym.Sex;
        //    if (mym.Sex == 1)
        //    {
        //        ViewData["membersex"] = "男";
        //        ViewData["ta"] = "他";
        //    }
        //    else
        //    {
        //        ViewData["membersex"] = "女";
        //        ViewData["ta"] = "她";
        //    }
        //    #endregion
        //    #region 视频认证、会员级别、热度、在线、关注
        //    if (mym.MemberPhoto != null)
        //        ViewData["realPhotoIdentification"] = mym.MemberPhoto.IsRealPhotoIdentification;
        //    if (mym.MemberType == BiZ.Member.MemberType.Level0)
        //        ViewData["memberLevel"] = "普通会员";
        //    if (mym.MemberType == BiZ.Member.MemberType.Level1)
        //        ViewData["memberLevel"] = "正式会员";
        //    if (mym.MemberType == BiZ.Member.MemberType.Level2)
        //        ViewData["memberLevel"] = "VIP";
        //    if (mym.Status == null)
        //        ViewData["memberhot"] = "normal";
        //    else
        //    {
        //        if (mym.Status.Last24HInCallsCount < 6)
        //            ViewData["memberhot"] = "normal";
        //        if (mym.Status.Last24HInCallsCount >= 6 & mym.Status.Last24HInCallsCount < 10)
        //            ViewData["memberhot"] = "hot1";
        //        if (mym.Status.Last24HInCallsCount >= 10 & mym.Status.Last24HInCallsCount < 20)
        //            ViewData["memberhot"] = "hot2";
        //        if (mym.Status.Last24HInCallsCount >= 20)
        //            ViewData["memberhot"] = "hot3";

        //        ViewData["IsOnline"] = mym.OnlineStr;

        //    }
        //    bool infavor = BiZ.Member.Relation.RelationProvider.IsInFavor(userid, mid);
        //    ViewData["infavor"] = infavor;
        //    #endregion
        //    #region 经纬度坐标
        //    bool isgeoset = false;
        //    if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0)
        //        isgeoset = true;
        //    String juli = "? 米";
        //    if (mym.Settings != null)
        //        ViewData["HiddenMyLoc"] = mym.Settings.HiddenMyLoc;
        //    else
        //        ViewData["HiddenMyLoc"] = false;
        //    ViewData["geoisset"] = isgeoset;
        //    ViewData["Lat"] = mym.MemberInfomation.Lat;
        //    ViewData["Lng"] = mym.MemberInfomation.Lng;
        //    ViewData["juli"] = juli;

        //    //计算距离
        //    if (isgeoset)
        //    {
        //        ViewData["IAlreadyMarkMyPosition"] = false;

        //        if (userid != null & userid != "" & alreadylogin)
        //        {
        //            BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(userid);

        //            if (me.MemberInfomation.Lat != 0 & me.MemberInfomation.Lng != 0)
        //            {
        //                ViewData["juli"] = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
        //                me.MemberInfomation.Lng,
        //                me.MemberInfomation.Lat,
        //                mym.MemberInfomation.Lng,
        //                mym.MemberInfomation.Lat);

        //                ViewData["IAlreadyMarkMyPosition"] = true;
        //            }
        //        }
        //    }

        //    #endregion
        //    #region 信息、独白
        //    ViewData["height"] = (mym.MemberInfomation.Height == "") ? "问我" : mym.MemberInfomation.Height;
        //    ViewData["figure"] = (mym.MemberInfomation.Figure == "") ? "问我" : mym.MemberInfomation.Figure;
        //    ViewData["star"] = (mym.MemberInfomation.Star == "") ? "问我" : mym.MemberInfomation.Star;
        //    ViewData["homecity"] = (mym.MemberInfomation.Hometown == "") ? "问我" : mym.MemberInfomation.Hometown.Replace("|", " ");
        //    ViewData["educationalBackground"] = (mym.MemberInfomation.EducationalBackground == "") ? "问我" : mym.MemberInfomation.EducationalBackground;
        //    ViewData["career"] = (mym.MemberInfomation.Career == "") ? "问我" : mym.MemberInfomation.Career;
        //    ViewData["gainings"] = (mym.MemberInfomation.Gainings == "") ? "问我" : mym.MemberInfomation.Gainings;
        //    ViewData["haveCar"] = (mym.MemberInfomation.HaveCar == "") ? "问我" : mym.MemberInfomation.HaveCar;
        //    ViewData["carband"] = (mym.MemberInfomation.CarBand == "") ? "问我" : mym.MemberInfomation.CarBand;
        //    ViewData["livingStatus"] = (mym.MemberInfomation.LivingStatus == "") ? "问我" : mym.MemberInfomation.LivingStatus;
        //    ViewData["haveHouse"] = (mym.MemberInfomation.HaveHouse == "") ? "问我" : mym.MemberInfomation.HaveHouse;
        //    //ViewData["Soliloquize"] = mym.MemberInfomation.Soliloquize;
        //    #endregion
        //    #region 状态表
        //    //ViewData["ScoreAvg"] = mym.Status.ScoreAvg;
        //    //ViewData["MarkedTimes"] = mym.Status.MarkedTimes;
        //    ViewData["BeenViewedTimes"] = mym.Status.BeenViewedTimes;
        //    //ViewData["ScoredTimes"] = mym.Status.ScoredTimes;
        //    ViewData["FavorMemberCount"] = mym.Status.FavorMemberCount;
        //    ViewData["MemberFavoredMeCount"] = mym.Status.MemberFavoredMeCount;
        //    ViewData["TotalMsgCount"] = mym.Status.TotalMsgCount;
        //    ViewData["UnReadBeenViewedTimes"] = mym.Status.UnReadBeenViewedTimes;
        //    ViewData["UnReadMsgCount"] = mym.Status.UnReadMsgCount;
        //    //ViewData["UnReadMarkCount"] = mym.Status.UnReadMarkCount;
        //    //ViewData["UnReadScoreCount"] = mym.Status.UnReadScoreCount;
        //    ViewData["UnReadBeenFavorCount"] = mym.Status.UnReadBeenFavorCount;
        //    #endregion

        //    //if (mym.MemberRelations != null)
        //    //{
        //    //    if (mym.MemberRelations.Marks.Count != 0)
        //    //    {
        //    //        ViewData["marks"] = new JavaScriptSerializer().Serialize(mym.MemberRelations.Marks);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    ViewData["marks"] = "[]";
        //    //}
        //    return View();
        //}
        //[Authorize]
        //public ActionResult ProfileControlTop(String mid)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    #region 是否浏览自己的主页
        //    String userid = HttpContext.User.Identity.Name;
        //    ViewData["isMe"] = IsMe(mid, userid);
        //    ViewData["mid"] = mid;
        //    #endregion

        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

        //    #region 用户头像
        //    if ((mym.MemberPhoto == null) || (mym.MemberPhoto.IconID == null) || (mym.MemberPhoto.IconID == ""))
        //        ViewData["iconurl"] = "/pics/noicon.jpg";
        //    else
        //    {
        //        BiZ.Photo.Photo pho = BiZ.Photo.PhotoManager.GetPhoto(mym.MemberPhoto.IconID);
        //        if (pho != null)
        //            ViewData["iconurl"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + pho.FileName.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
        //        else
        //            ViewData["iconurl"] = "/pics/noicon.jpg";
        //    }
        //    #endregion 用户头像
        //    #region 用户城市、昵称、年龄、性别
        //    ViewData["membercity"] = mym.MemberInfomation.City.Replace("|", " ");
        //    ViewData["membernickname"] = mym.MemberInfomation.NickName;
        //    ViewData["memberage"] = mym.MemberInfomation.Age.ToString();
        //    ViewData["sex"] = mym.Sex;
        //    if (mym.Sex == 1)
        //    {
        //        ViewData["membersex"] = "男";
        //        ViewData["ta"] = "他";
        //    }
        //    else
        //    {
        //        ViewData["membersex"] = "女";
        //        ViewData["ta"] = "她";
        //    }
        //    #endregion
        //    #region 视频认证、会员级别、热度、在线
        //    if (mym.MemberPhoto != null)
        //        ViewData["realPhotoIdentification"] = mym.MemberPhoto.IsRealPhotoIdentification;
        //    if (mym.MemberType == BiZ.Member.MemberType.Level1)
        //        ViewData["memberLevel"] = "正式会员";
        //    if (mym.MemberType == BiZ.Member.MemberType.Level2)
        //        ViewData["memberLevel"] = "VIP";
        //    if (mym.Status == null)
        //        ViewData["memberhot"] = "normal";
        //    else
        //    {
        //        if (mym.Status.Last24HInCallsCount < 6)
        //            ViewData["memberhot"] = "normal";
        //        if (mym.Status.Last24HInCallsCount > 5 & mym.Status.Last24HInCallsCount < 10)
        //            ViewData["memberhot"] = "hot1";
        //        if (mym.Status.Last24HInCallsCount > 10 & mym.Status.Last24HInCallsCount < 15)
        //            ViewData["memberhot"] = "hot2";
        //        if (mym.Status.Last24HInCallsCount > 15 & mym.Status.Last24HInCallsCount < 20)
        //            ViewData["memberhot"] = "hot3";

        //        ViewData["IsOnline"] = mym.OnlineStr;
        //    }
        //    #endregion
        //    #region 经纬度坐标
        //    bool isgeoset = false;
        //    if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0)
        //        isgeoset = true;
        //    String juli = "? 米";
        //    ViewData["geoisset"] = isgeoset;
        //    ViewData["Lat"] = mym.MemberInfomation.Lat;
        //    ViewData["Lng"] = mym.MemberInfomation.Lng;
        //    ViewData["juli"] = juli;

        //    //计算距离
        //    if (isgeoset)
        //    {
        //        if (userid != null)
        //        {
        //            ViewData["IAlreadyMarkMyPosition"] = false;
        //            BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(userid);
        //            if (me.MemberInfomation.Lat != 0 & me.MemberInfomation.Lng != 0)
        //            {
        //                double distant = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistance(
        //                me.MemberInfomation.Lng,
        //                me.MemberInfomation.Lat,
        //                mym.MemberInfomation.Lng,
        //                mym.MemberInfomation.Lat);

        //                int distantint = Int32.Parse(Math.Floor(distant).ToString());
        //                if (distantint > 1000)
        //                {
        //                    double km = distantint / 1000;
        //                    ViewData["juli"] = Math.Floor(km).ToString() + " 千米";
        //                }
        //                else
        //                {
        //                    ViewData["juli"] = distantint + " 米";
        //                }
        //                ViewData["IAlreadyMarkMyPosition"] = true;
        //            }

        //        }
        //    }

        //    #endregion
        //    #region 信息、独白
        //    ViewData["height"] = (mym.MemberInfomation.Height == "") ? "问我" : mym.MemberInfomation.Height;
        //    ViewData["figure"] = (mym.MemberInfomation.Figure == "") ? "问我" : mym.MemberInfomation.Figure;
        //    ViewData["star"] = (mym.MemberInfomation.Star == "") ? "问我" : mym.MemberInfomation.Star;
        //    ViewData["homecity"] = (mym.MemberInfomation.Hometown == "") ? "问我" : mym.MemberInfomation.Hometown.Replace("|", " ");
        //    ViewData["educationalBackground"] = (mym.MemberInfomation.EducationalBackground == "") ? "问我" : mym.MemberInfomation.EducationalBackground;
        //    ViewData["career"] = (mym.MemberInfomation.Career == "") ? "问我" : mym.MemberInfomation.Career;
        //    ViewData["gainings"] = (mym.MemberInfomation.Gainings == "") ? "问我" : mym.MemberInfomation.Gainings;
        //    ViewData["haveCar"] = (mym.MemberInfomation.HaveCar == "") ? "问我" : mym.MemberInfomation.HaveCar;
        //    ViewData["carband"] = (mym.MemberInfomation.CarBand == "") ? "问我" : mym.MemberInfomation.CarBand;
        //    ViewData["livingStatus"] = (mym.MemberInfomation.LivingStatus == "") ? "问我" : mym.MemberInfomation.LivingStatus;
        //    ViewData["haveHouse"] = (mym.MemberInfomation.HaveHouse == "") ? "问我" : mym.MemberInfomation.HaveHouse;
        //    //ViewData["Soliloquize"] = mym.MemberInfomation.Soliloquize;
        //    ViewData["iwant"] = mym.MemberInfomation.IWant;
        //    #endregion
        //    //if (mym.MemberRelations != null)
        //    //{
        //    //    if (mym.MemberRelations.Marks.Count != 0)
        //    //    {
        //    //        ViewData["marks"] = new JavaScriptSerializer().Serialize(mym.MemberRelations.Marks);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    ViewData["marks"] = "[]";
        //    //}
        //    return View();
        //}
        //[Authorize]
        //public ActionResult ProfileControlTopSimple(String mid)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    #region 是否浏览自己的主页
        //    String userid = HttpContext.User.Identity.Name;
        //    ViewData["isMe"] = IsMe(mid, userid);
        //    ViewData["mid"] = mid;
        //    ViewData["me"] = userid;
        //    #endregion

        //    bool alreadylogin = isalreadylogin("Member");

        //    if (!alreadylogin & mid == null)
        //        return RedirectToAction("Login", "Account");

        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

        //    #region 用户头像
        //    if ((mym.MemberPhoto == null) || (mym.MemberPhoto.IconID == null) || (mym.MemberPhoto.IconID == ""))
        //        ViewData["iconurl"] = "/pics/noicon.jpg";
        //    else
        //    {
        //        if (mym.MemberInfomation != null & mym.MemberInfomation.IconPath != "")
        //            ViewData["iconurl"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
        //        else
        //            ViewData["iconurl"] = "/pics/noicon.jpg";
        //    }
        //    ViewData["membernickname"] = mym.MemberInfomation.NickName;
        //    ViewData["iwant"] = mym.MemberInfomation.IWant;
        //    ViewData["iwantsexstr"] = (mym.Sex == 1) ? "女生" : "男生";
        //    #endregion 用户头像

        //    return View();
        //}
        #endregion

        #region 数据与业务方法
        
        private bool IsMe(String mid, String userid)
        {
            return mid == userid;
        }

        [Authorize]
        [HttpPost]
        public ActionResult getmemberprofile(String mid)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(HttpContext.User.Identity.Name);
            String infojson = "";
            if (mym.MemberInfomation != null)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                infojson = ser.Serialize(mym.MemberInfomation);
            }

            return Json(infojson);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SetMemberIconPhoto(String id)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userid = HttpContext.User.Identity.Name;

            CBB.ExceptionHelper.OperationResult result = null;

            //如果视频认证为通过则改为不通过
            result = SetPhotoIdentNotPass(userid);
            result = BiZ.MemberManager.MemberManager.SetMemberIconPhoto(userid, id);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        /// <summary>
        /// 如果视频认证为通过则改为不通过
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult SetPhotoIdentNotPass(String userid)
        {
            //如果视频认证为通过则改为不通过
            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(userid);
            if (member.MemberPhoto.IsRealPhotoIdentification)
            {
                member.MemberPhoto.IsRealPhotoIdentification = false;
                List<ObjectId> obj = new List<ObjectId>();
                obj.Add(ObjectId.Parse(userid));
                //修改认证为不通过
                BiZ.MemberManager.MemberManager.CheckRealPhoto(obj, false);
                //删除以前的认证照片（如果存在）
                Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.RemoveCheckPhotosByusrid(userid);
                //发送站内信
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(userid, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertificationagain"));
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        //[Authorize]
        //public ActionResult GetOneSoliloquizedMember()
        //{
        //    String userid = HttpContext.User.Identity.Name;
        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userid);
        //    int sex = 1;
        //    if (mym.Sex == 1) sex = 2;

        //    BiZ.Member.Member somym = BiZ.MemberManager.MemberManager.GetOneSoliloquizedMember(sex, mym.MemberInfomation.City.Split('|')[0]);
        //    if (somym == null) return Json(new JavaScriptSerializer().Serialize(""));
        //    return Json(new JavaScriptSerializer().Serialize(somym));
        //}
        //[HttpPost]
        //[ValidateInput(true)]
        //public ActionResult SetSoliloquize(String soliloquize)
        //{
        //    if (soliloquize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String userid = HttpContext.User.Identity.Name;

        //    CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetSoliloquize(userid, soliloquize);
        //    return Json(new JavaScriptSerializer().Serialize(result));

        //    //String userid = HttpContext.User.Identity.Name;
        //    //ViewData["SetSoliloquizeSuccess"] = "null";
        //    //if (soliloquize != null)
        //    //{
        //    //    CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetSoliloquize(userid, soliloquize);
        //    //    ViewData["SetSoliloquizeSuccess"] = result.ok.ToString();
        //    //}

        //    //#region metas version
        //    //ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    //ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    //ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    //ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath");
        //    //#endregion

        //    //bool alreadyLogin = isalreadylogin("Member");

        //    //#region 构造页面数据对象
        //    //Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
        //    ////页面数据对象
        //    //Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj);
        //    //memberModel.UserID = userid;
        //    //memberModel.MemberID = userid;
        //    //memberModel.AlreadyLogon = alreadyLogin;
        //    //#endregion
        //    //return View(memberModel);
        //}
        [Authorize]
        [HttpPost]
        public ActionResult SetIWant(String iwant)
        {
            if (iwant == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userid = HttpContext.User.Identity.Name;

            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetIWant(userid, iwant);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        
        //public ActionResult ProfileLeftPanel()
        //{
        //    String userid = HttpContext.User.Identity.Name;
        //    Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);

        //    //页面数据对象
        //    Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj, null);
        //    memberModel.UserID = userid;
        //    memberModel.MemberID = userid;
        //    memberModel.AlreadyLogon = true;
        //    return View(memberModel);
        //}

        #endregion

        #endregion

        #region InterView

        #region 视图方法
        #endregion

        #region 数据与业务方法
        [Authorize]
        [HttpPost]
        public ActionResult GetInterViews(String mid, String pagesize, String pageno)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int size = 0, no = 0;
            if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            return Json(new JavaScriptSerializer().Serialize(BiZ.InterView.InterViewProvider.GetInterViews(mid, size, no)));
        }
        [Authorize]
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult AddInterView(String mid, String systemQuestionID, String question, String answer)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (systemQuestionID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (answer == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (question == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            return Json(new JavaScriptSerializer().Serialize(BiZ.InterView.InterViewProvider.AddInterView(mid, systemQuestionID, question, answer)));
        }
        [Authorize]
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult UpdateInterView(String id, String mid, String answer)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (answer == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userid = HttpContext.User.Identity.Name;
            if (userid != mid) return RedirectToAction("Error", "Error", new { errorno = "0" });
            return Json(new JavaScriptSerializer().Serialize(BiZ.InterView.InterViewProvider.UpdateInterView(id, mid, answer)));
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteInterView(String id, String mid)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userid = HttpContext.User.Identity.Name;
            if (userid != mid) return RedirectToAction("Error", "Error", new { errorno = "0" });

            return Json(new JavaScriptSerializer().Serialize(BiZ.InterView.InterViewProvider.RemoveInterView(id, mid)));
        }
        #endregion
        
        #endregion

        //#region Skill

        //#region 视图方法
        //#endregion

        //#region 数据与业务方法
        //[Authorize]
        //public ActionResult GetSkills(String mid, String pagesize, String pageno)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    IList<BiZ.Skills.Skill> list = GetSkillList(mid, pagesize, pageno);
        //    IList<BiZ.Skills.Skill> kownlist = new List<BiZ.Skills.Skill>();
        //    IList<BiZ.Skills.Skill> wanttoknowlist = new List<BiZ.Skills.Skill>();
        //    List<String> tkowns = new List<String>();//还有谁会
        //    List<String> twants = new List<String>();//还有谁想学
        //    foreach (BiZ.Skills.Skill obj in list)
        //    {
        //        if (obj.SkillType == BiZ.Skills.SkillType.IKnow)
        //        {
        //            kownlist.Add(obj);
        //            tkowns.Add(obj.SkillName);
        //        }
        //        else
        //        {
        //            wanttoknowlist.Add(obj);
        //            twants.Add(obj.SkillName);
        //        }
        //    }
        //    IList<BiZ.Skills.Skill> tkownlist = new List<BiZ.Skills.Skill>();
        //    IList<BiZ.Skills.Skill> twantlist = new List<BiZ.Skills.Skill>();
        //    String[] kowns = (String[])tkowns.ToArray();
        //    String[] wants = (String[])twants.ToArray();
        //    tkownlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyWantLearn(mid, kowns, 100);
        //    twantlist = BiZ.Recommendation.SkillInfoRecommendation.GetTheyKnows(mid, wants, 100);
        //    Models.PageModels.MemberSkillModel model = new Models.PageModels.MemberSkillModel(null, kownlist, wanttoknowlist, tkownlist, twantlist);
        //    return Json(new JavaScriptSerializer().Serialize(model));
        //}
        //public IList<BiZ.Skills.Skill> GetSkillList(String mid, String pagesize, String pageno)
        //{
        //    int size = 0, no = 0;
        //    if (!int.TryParse(pagesize, out size)) throw new Exception("不正确的参数:pagesize");
        //    if (!int.TryParse(pageno, out no)) throw new Exception("不正确的参数:pageno");
        //    return BiZ.Skills.SkillProvider.GetSkills(mid, size, no);
        //}
        //
        //[Authorize]
        //public ActionResult GetProvinceKnows(String province,String skillname, String pagesize, String pageno)
        //{
        //    if (province == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (skillname == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    int size = 0, no = 0;
        //    if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    String userid = HttpContext.User.Identity.Name;
        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userid);
        //    int sex = 1;
        //    if (mym.Sex == 1) sex = 2;

        //    if (province == "") province = mym.MemberInfomation.City.Split('|')[0];

        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Skills.SkillProvider.GetProvinceSkills(province, skillname,sex, BiZ.Skills.SkillType.IKnow, size, no)));
        //}
        //[Authorize]
        //public ActionResult GetProvinceWantLearns(String province, String skillname, String pagesize, String pageno)
        //{
        //    if (province == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (skillname == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    int size = 0, no = 0;
        //    if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    String userid = HttpContext.User.Identity.Name;
        //    BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userid);
        //    int sex = 1;
        //    if (mym.Sex == 1) sex = 2;

        //    if (province == "") province = mym.MemberInfomation.City.Split('|')[0];

        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Skills.SkillProvider.GetProvinceSkills(province, skillname, sex, BiZ.Skills.SkillType.WantLearn, size)));
        //}
        // */
        //[Authorize]
        //[ValidateInput(true)]
        //public ActionResult AddSkill(String mid, String type, String skillname, String skilllevel, String payment, String comment)
        //{
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (skillname == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    int typeno = 0;
        //    if (!int.TryParse(type, out typeno)) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    BiZ.Skills.SkillType typeobj = (BiZ.Skills.SkillType)typeno;
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Skills.SkillProvider.AddSkill(mid, typeobj, skillname, skilllevel, payment, comment)));
        //}
        //[Authorize]
        //[ValidateInput(true)]
        //public ActionResult UpdateSkill(String id, String mid, String skillname, String skilllevel, String payment, String comment)
        //{
        //    if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (skillname == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String userid = HttpContext.User.Identity.Name;
        //    if (userid != mid) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Skills.SkillProvider.UpdateSkill(id, mid, skillname, skilllevel, payment, comment)));
        //}
        //[Authorize]
        //public ActionResult DeleteSkill(String id, String mid)
        //{
        //    if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String userid = HttpContext.User.Identity.Name;
        //    if (userid != mid) return RedirectToAction("Error", "Error", new { errorno = "0" });

        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Skills.SkillProvider.RemoveSkill(id, mid)));
        //}
        //#endregion
        
        //#endregion
    }
}
