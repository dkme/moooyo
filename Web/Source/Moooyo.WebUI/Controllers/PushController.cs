using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class PushController : Controller
    {
        #region 视图方法
        /// <summary>
        /// 应用推送
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AppPush()
        {
            IList<BiZ.Sys.Applications.Application> list = BiZ.Sys.Applications.ApplicationsFactory.GetApp();
            Models.PageModels.ApplicationModel model = null;
            if (list != null && list.Count > 0)
            {
                Random ran = new Random();
                int index = ran.Next(0, list.Count);
                model = new Models.PageModels.ApplicationModel(list[index]);
            }
            return View(model);
        }
        /// <summary>
        /// 与我/TA兴趣相同的人
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult SameInterestingMemberOverMe(String id)
        {
            bool alreadyLogin = true;
            String userid = HttpContext.User.Identity.Name;
            Dictionary<String, Dictionary<String, float>> interestmemberlist = BiZ.Recommendation.MemberRecommendation.GetMembersWhoTasterLikeMe(id != null ? id : userid, 6);
            Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>> memberList = new Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>>();
            if (interestmemberlist.Count > 0)
            {
                foreach (var obj in interestmemberlist)
                {
                    IList<BiZ.InterestCenter.Interest> interestlist = new List<BiZ.InterestCenter.Interest>();
                    BiZ.Member.Member memberobj = BiZ.MemberManager.MemberManager.GetMember(obj.Key);
                    if (memberobj != null)
                    {
                        foreach (var interestobj in obj.Value)
                        {
                            interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(interestobj.Key));
                        }
                        memberList.Add(memberobj, interestlist);
                    }
                }
            }
            IList<BiZ.Member.Relation.Favorer> funslist = BiZ.Member.Relation.RelationProvider.GetFavorers(userid, 0, 0);
            Models.PageModels.InterestOverMeMemberModel interestmembermodel = new Models.PageModels.InterestOverMeMemberModel(memberList, funslist);
            interestmembermodel.UserID = userid;
            interestmembermodel.MemberID = id != null ? id : userid;
            interestmembermodel.AlreadyLogon = alreadyLogin;
            return View(interestmembermodel);
            //return View();
        }
        /// <summary>
        /// 猜你喜欢（兴趣）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult GuessYourInterest()
        {
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            #region 构造页面数据对象
            //获取推荐的兴趣6个
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.Recommendation.InterestRecommendation.GuessYourInterest(userId, 6);
            //页面数据对象
            Models.PageModels.InterestListControlModel interestModel = new Models.PageModels.InterestListControlModel(interestList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            #endregion

            return View(interestModel);
        }
        /// <summary>
        /// 猜你喜欢（兴趣For我的兴趣话题）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult GuessYourInterests()
        {
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            #region 构造页面数据对象
            //获取推荐的兴趣6个
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.Recommendation.InterestRecommendation.GuessYourInterest(userId, 6);
            //页面数据对象
            Models.PageModels.InterestListControlModel interestModel = new Models.PageModels.InterestListControlModel(interestList);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            #endregion

            return View(interestModel);
        }
        /// <summary>
        /// 我/TA喜欢的人都喜欢(兴趣)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult TheyLike(String id)
        {
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.Recommendation.InterestRecommendation.GetMemberFavorMembersTaste(id != null ? id : userId, 6);
            Models.PageModels.InterestListControlModel interestModel = new Models.PageModels.InterestListControlModel(interestList);
            interestModel.UserID = userId;
            interestModel.MemberID = id != null ? id : userId;
            interestModel.AlreadyLogon = alreadyLogin;
            return View(interestModel);
        }
        /// <summary>
        /// 喜欢它的人还喜欢（兴趣）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult TheyFavorsInteresting()
        {
            String objid = HttpContext.Request["iid"];
            Models.PageModels.InterestListControlModel interestmodel = null;
            if (objid != null)
            {
                IList<BiZ.InterestCenter.Interest> interestlist = BiZ.Recommendation.InterestRecommendation.GetElseInterestIDsMemberLikeWhereWhoLikeThis(objid, 8);
                interestmodel = new Models.PageModels.InterestListControlModel(interestlist);
            }
            return View(interestmodel);
        }
        /// <summary>
        /// 喜欢我/TA的人还喜欢（用户）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult TheyFavorsMember(String id)
        {
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.Member.Member> memberList = BiZ.Recommendation.MemberRecommendation.GetElseMemberIDsMemberLikeWhereWhoLikeThis(id != null ? id : userId, 6);
            Models.PageModels.TheyFavorsMemberModel membermodel = new Models.PageModels.TheyFavorsMemberModel(memberList);
            membermodel.UserID = userId;
            membermodel.MemberID = id != null ? id : userId;
            membermodel.AlreadyLogon = alreadyLogin;
            return View(membermodel);
        }
        
        
        /// <summary>
        /// 兴趣周排名
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult WeeklyInterestRanking()
        {
            bool alreadyLogin = true;
            String userid = HttpContext.User.Identity.Name;
            IList<CBB.RankingHelper.RankingList> rankinglist = BiZ.Ranking.InterestRanking.GetWeeklyInterestRankingList(10);//取10个
            List<String> idlist = new List<string>();
            foreach (var obj in rankinglist)
                idlist.Add(obj.ObjID);
            String[] ids = (String[])idlist.ToArray();
            IList<BiZ.InterestCenter.Interest> interestlist = BiZ.InterestCenter.InterestFactory.GetInterest(ids);
            Models.PageModels.WeeklyInterestRankingModel interestmodel = new Models.PageModels.WeeklyInterestRankingModel(rankinglist, interestlist);
            interestmodel.UserID = userid;
            interestmodel.MemberID = userid;
            interestmodel.AlreadyLogon = alreadyLogin;
            return View(interestmodel);
            //return View();
        }
        /// <summary>
        /// 他们在问（问问）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Question(String id)
        {
            bool alreadyLogin = true;
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwens = new List<BiZ.WenWen.WenWen>();
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            if (id != null)
            {
                interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(id, 0, 0);
            }
            else
            {
                interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            }
            String[] ids = new String[interests.Count];
            if (interests != null && interests.Count > 0)
            {
                int i = 0;
                foreach (BiZ.InterestCenter.Interest interest in interests)
                {
                    ids[i] = interest.ID;
                    i++;
                }
                wenwens = BiZ.WenWen.WenWenProvider.GetRandomWenWen(6, ids);
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwens);
            model.UserID = userid;
            model.MemberID = id == null ? userid : id;
            model.AlreadyLogon = alreadyLogin;
            return View(model);
        }
        /// <summary>
        /// 兴趣的粉丝
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult InterestFans()
        {
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
            BiZ.Member.Member memberobj = BiZ.MemberManager.MemberManager.GetMember(userId);
            IList<BiZ.InterestCenter.InterestFans> fans = BiZ.InterestCenter.InterestFactory.GetInterestFans(interestId, 8, 1);
            long fanscount = BiZ.InterestCenter.InterestFactory.GetInterestFansCount(interestId);
            Models.PageModels.InterestFansModel model = new Models.PageModels.InterestFansModel(fans, fanscount);
            model.UserID = userId;
            model.MemberID = userId;
            model.AlreadyLogon = alreadyLogin;
            return View(model);
        }
        /// <summary>
        /// 我加粉的兴趣(去15个)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyInterest()
        {
            string userid = User.Identity.Name;
            long interestcount = BiZ.InterestCenter.InterestFactory.GetMemberInterestCount(userid);
            IList<BiZ.InterestCenter.Interest> interest = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 9, 1);
            Models.PageModels.MyInterestModel model = new Models.PageModels.MyInterestModel(interest, interestcount);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = true;
            return View(model);
        }
        #endregion

        #region 数据与业务方法
        
        /// <summary>
        /// 他们在问（问问:换一批）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQustion(String id)
        {
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwens = new List<BiZ.WenWen.WenWen>();
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            if (id != null)
            {
                interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(id, 0, 0);
            }
            else
            {
                interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            }
            String[] ids = new String[interests.Count];
            if (interests != null && interests.Count > 0)
            {
                int i = 0;
                foreach (BiZ.InterestCenter.Interest interest in interests)
                {
                    ids[i] = interest.ID;
                    i++;
                }
                wenwens = BiZ.WenWen.WenWenProvider.GetRandomWenWen(6, ids);
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwens);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        #endregion

        /// <summary>
        /// 话题详情最新话题
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult NewQuestion()
        {
            IList<BiZ.WenWen.WenWen> wenwens = BiZ.WenWen.WenWenProvider.GetWenWens(1, 10);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwens);
            return View(model);
        }
        /// <summary>
        /// 获取我参与的话题(我发布的和我回复的)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyWenWen()
        {
            string userid = User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwen = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userid, 0, 0);
            IList<BiZ.WenWen.WenWen> wenwens = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userid, 0, 0);
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            foreach (BiZ.WenWen.WenWen obj in wenwen)
            {
                if (wenwens.IndexOf(obj) <= 0)
                {
                    wenwens.Add(obj);
                }
            }
            foreach (BiZ.WenWen.WenWen objs in wenwens)
            {
                if (objs.InterestID == null) continue;
                interests.Add(BiZ.InterestCenter.InterestFactory.GetInterest(objs.InterestID));
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwens, interests);
            return View(model);
        }
        /// <summary>
        /// 设置米柚账号
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult WeiBoUser(String url)
        {
            ViewData["url"] = url;
            return View();
        }
        [HttpPost]
        public ActionResult setWeiBoUser(String userword, String password)
        {
            string userid = User.Identity.Name;
            if (userword != null && password != null)
            {
                BiZ.MemberManager.MemberManager.UpdateWeiBoUser(userid, userword, password);
            }
            return Json(new JavaScriptSerializer().Serialize(true));
        }
        [Authorize]
        public ActionResult MyInterestTowwItem(List<BiZ.WenWen.WenWen> topiclist, int showcount)
        {
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel();
            model.UserID = HttpContext.User.Identity.Name;
            model.wenwenlist = topiclist;
            ViewData["showcount"] = showcount;
            return View(model);
        }
    }
}
