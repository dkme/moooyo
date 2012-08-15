///
/// 功能描述：问问操作控制器
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/27
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;

namespace Moooyo.WebUI.Controllers
{
    public class WenWenController : Controller
    {
        #region 视图方法
        /// <summary>
        /// 问问显示(前三条数据)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult WenWen()
        {
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            int wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenCount(null);
            int[] indexlist = new int[3];
            Random ran = new Random();
            for (int i = 0; i < 3; i++) { indexlist[i] = ran.Next(wenwencount); }
            IList<BiZ.WenWen.WenWen> wenwens = new List<BiZ.WenWen.WenWen>();
            foreach (int index in indexlist) { wenwens.Add(BiZ.WenWen.WenWenProvider.GetWenWens(index, 1)[0]); }
            IList<BiZ.InterestCenter.Interest> interests = new List<BiZ.InterestCenter.Interest>();
            foreach (var obj in wenwens)
            {
                BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID);
                interests.Add(interest);
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwens, interests);
            ViewData["pagecount"] = wenwencount;
            model.UserID = userId;
            model.MemberID = userId;
            model.AlreadyLogon = alreadyLogin;
            return View(model);
        }
        /// <summary>
        /// 问问发布1
        /// </summary>
        /// <returns></returns>
        /// 

        [Authorize]
        public ActionResult AddWenWen()
        {
            String userid = HttpContext.User.Identity.Name;
            int wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenCount(userid);
            int wenwenanswercount = (int)BiZ.WenWen.WenWenProvider.GetWenWenAnswerForMemberCount(userid);
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwencount, wenwenanswercount, interests);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = true;
            return View(model);
        }
        /// <summary>
        /// 问问发布2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddWenWens(String id)
        {
            String userid = HttpContext.User.Identity.Name;
            int wenwencount = BiZ.WenWen.WenWenProvider.GetWenWenCount(userid);
            int wenwenanswercount = (int)BiZ.WenWen.WenWenProvider.GetWenWenAnswerForMemberCount(userid);
            BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(id);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwencount, wenwenanswercount, interest);
            model.UserID = userid;
            model.MemberID = userid;
            model.AlreadyLogon = true;
            return View(model);
        }
        /// <summary>
        /// 问问详情
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ShowWenWen()
        {
            #region metas version
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
            #endregion

            string wenwenid;
            if (Request.QueryString["wwid"] != null)
            {
                wenwenid = Request.QueryString["wwid"].ToString();
                if (wenwenid == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            }
            //只允许已登录用户访问自己
            bool alreadyLogin = true;
            String userId = HttpContext.User.Identity.Name;
            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userId);

            #region 获取兴趣问问
            BiZ.WenWen.WenWen wenwenobj = BiZ.WenWen.WenWenProvider.GetWenWen(wenwenid);
            wenwenobj.AnswerCount = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(wenwenobj.ID, 0, 0, 1).Count;
            string page = Request.QueryString["pn"];
            //页码
            int pageNo = 1;
            if (!Int32.TryParse(page, out pageNo)) pageNo = 1;
            int pageSize = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PlazaTopicPageSize"));

            IList<BiZ.WenWen.WenWenAnswer> answerlist = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(wenwenid, pageSize, pageNo, -1);
            #endregion

            //页面数据对象
            Models.PageModels.MemberInterestModel interestModel = new Models.PageModels.MemberInterestModel(memberDisplayObj);
            interestModel.UserID = userId;
            interestModel.MemberID = userId;
            interestModel.AlreadyLogon = alreadyLogin;
            interestModel.wenwenobj = wenwenobj;
            interestModel.interestObj = BiZ.InterestCenter.InterestFactory.GetInterest(wenwenobj.InterestID);
            interestModel.answerlist = answerlist;
            interestModel.interestlist = BiZ.InterestCenter.InterestFactory.GetMemberInterest(wenwenobj.Creater.MemberID, 10, 1);
            //interestModel.ifmembertofans = BiZ.InterestCenter.InterestFactory.IsFans(wenwenobj.InterestID, userId);
            interestModel.iffliter = new BiZ.FilterText.FilterTextOperation().FindFliter(BiZ.WenWen.WenWen.GetCollectionName(), wenwenobj.ID);
            interestModel.wwmemberobj = BiZ.MemberManager.MemberManager.GetMember(wenwenobj.Creater.MemberID);

            double answercount = (double)BiZ.WenWen.WenWenProvider.GetWenWenAnswer(wenwenid, 0, 0, 1).Count;
            interestModel.answercount = (int)answercount;

            interestModel.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling(answercount / pageSize);
            interestModel.Pagger.PageCount = pages;
            interestModel.Pagger.PageSize = pageSize;
            interestModel.Pagger.PageNo = pageNo;
            interestModel.Pagger.PageUrl = "/WenWen/ShowWenWen?wwid=" + wenwenid + "&pn=";
            #endregion

            return View(interestModel);
        }
        #endregion

        #region 数据与业务方法
        /// <summary>
        /// 获取下个条问问信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetNextWenWen(int page)
        {
            IList<BiZ.WenWen.WenWen> wenwens = BiZ.WenWen.WenWenProvider.GetWenWens(page, 1);
            BiZ.WenWen.WenWen obj = new BiZ.WenWen.WenWen();
            if (wenwens != null && wenwens.Count > 0)
            {
                obj = wenwens[0];
            }
            BiZ.InterestCenter.Interest interestobj = BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(obj, interestobj);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 添加问问
        /// </summary>
        /// <param name="id">兴趣id</param>
        /// <param name="title">问问标题</param>
        /// <param name="content">问问内容</param>
        /// <returns></returns>
        //[ValidateInput(true)]
        //public ActionResult InsertWenWen(String id, String title, String content,String contentimage)
        //{
        //    String userid = HttpContext.User.Identity.Name;
        //    //BiZ.InterestCenter.Interest interest = BiZ.InterestCenter.InterestFactory.GetInterest(id);
        //    BiZ.WenWen.WenWen obj = BiZ.Member.Activity.ActivityController.AddQuestToMyInterest(userid, id, title, content, contentimage);
        //    //添加问问动态
        //    if (obj != null)
        //        return Json(new JavaScriptSerializer().Serialize(true));
        //    else
        //        return Json(new JavaScriptSerializer().Serialize(false));
        //}
        /// <summary>
        /// 修改问问
        /// </summary>
        /// <param name="wwid">问问id</param>
        /// <param name="title">问问标题</param>
        /// <param name="content">问问内容</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult UpdateWenWen(String wwid, String wwcontenthidden, String wwcontentimghidden)
        {
            String userid = HttpContext.User.Identity.Name;
            String title = wwcontenthidden.Length > 10 ? wwcontenthidden.Substring(0, 10) + "..." : wwcontenthidden;
            String fileName = string.Empty;
            String upimgstr = string.Empty;
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
                        upimgstr += topicimgobj.FileName + ",";
                    }
                }
            }
            upimgstr = upimgstr + wwcontentimghidden;

            CBB.ExceptionHelper.OperationResult obj = BiZ.WenWen.WenWenProvider.UpdateWenWen(wwid, title, wwcontenthidden, upimgstr);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel();
            model.wenwen = BiZ.WenWen.WenWenProvider.GetWenWen(wwid);
            return RedirectToAction("ShowWenWen", "WenWen", new { wwid = wwid });
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetWWFilterStatus(String id)
        {
            Boolean iffilter = new BiZ.FilterText.FilterTextOperation().FindFliter(BiZ.WenWen.WenWen.GetCollectionName(), id);
            return Json(new JavaScriptSerializer().Serialize(iffilter));
        }
        /// <summary>
        /// 添加问问回答
        /// </summary>
        /// <param name="interstid">兴趣id</param>
        /// <param name="wenwenid">问问id</param>
        /// <param name="content">回答内容</param>
        /// <returns></returns>
        [ValidateInput(true)]
        [HttpPost]
        [Authorize]
        public ActionResult InsertWenWenAnswer(String interestid, String wenwenid, String content, bool upordown)
        {
            String userid = HttpContext.User.Identity.Name;
            if (content != null && content != "")
            {
                BiZ.WenWen.WenWenAnswer obj = BiZ.Member.Activity.ActivityController.AddWenWenAnswer(userid, wenwenid, upordown, content);
            }
            else
            {
                CBB.ExceptionHelper.OperationResult obj = BiZ.WenWen.WenWenProvider.UpdateWenWenUpDown(wenwenid, upordown);
            }
            IList<BiZ.WenWen.WenWenAnswer> answers = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(wenwenid, 0, 0, -1);
            Models.PageModels.MemberInterestModel models = new Models.PageModels.MemberInterestModel();
            models.answerlist = answers;
            return Json(new JavaScriptSerializer().Serialize(models));
        }
        /// <summary>
        /// 修改问问回答
        /// </summary>
        /// <param name="id">回答id</param>
        /// <param name="content">回答内容</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult UpdateWenWenAnswer(String id, String content)
        {
            String userid = HttpContext.User.Identity.Name;
            CBB.ExceptionHelper.OperationResult obj = BiZ.WenWen.WenWenProvider.UpdateWenWenAnswer(id, content);
            Models.PageModels.MemberInterestModel model = new Models.PageModels.MemberInterestModel();
            model.answerobj = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(id);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetAnswerFilterStatus(String id)
        {
            Boolean iffilter = new BiZ.FilterText.FilterTextOperation().FindFliter(BiZ.WenWen.WenWenAnswer.GetCollectionName(), id);
            return Json(new JavaScriptSerializer().Serialize(iffilter));
        }
        /// <summary>
        /// 根据问问id加载问问答案
        /// </summary>
        /// <param name="wenwenid">问问id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetWenWenAnswer(String wenwenid, int pagesize, int pageno, int createdtimeorder)
        {
            int count = (int)BiZ.WenWen.WenWenProvider.GetWenWenAnswerCount(wenwenid);
            int pagecount = count % pagesize == 0 ? (int)count / pagesize : ((int)count / pagesize) + 1;
            int nextpageno = pageno + 1;
            IList<BiZ.WenWen.WenWenAnswer> answers = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(wenwenid, pagesize, pageno, createdtimeorder);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(count, pagecount, nextpageno, answers);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 获取当前用户参与的问问数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetWenWenAnswerForMemberCount()
        {
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userid, 0, 0);
            return Json(new JavaScriptSerializer().Serialize(wenwenlist.Count));
        }
        /// <summary>
        /// 获取当前用户参与的问问
        /// </summary>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageno">第几页</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetWenWenAnswerForMember(int pagesize, int pageno)
        {
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwenlist = BiZ.WenWen.WenWenProvider.GetWenWenForMemberIDs(userid, pagesize, pageno);
            IList<int> wenwenanswerlist = new List<int>();
            IList<BiZ.InterestCenter.Interest> interestlist = new List<BiZ.InterestCenter.Interest>();
            foreach (var obj in wenwenlist)
            {
                wenwenanswerlist.Add(obj.AnswerCount);
                interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID));
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwenlist, wenwenanswerlist, interestlist);
            return Json(new JavaScriptSerializer().Serialize(model));
            //return View();
        }
        /// <summary>
        /// 获取当前用户的问问数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetWenWenForMemberCount()
        {
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwen = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userid, 0, 0);
            return Json(new JavaScriptSerializer().Serialize(wenwen.Count));
        }
        /// <summary>
        /// 获取当前用户的问问
        /// </summary>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageno">第几页</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetWenWenForMember(int pagesize, int pageno)
        {
            String userid = HttpContext.User.Identity.Name;
            IList<BiZ.WenWen.WenWen> wenwen = BiZ.WenWen.WenWenProvider.GetWenWenForMember(userid, pagesize, pageno);
            IList<BiZ.WenWen.WenWen> wenwenlist = new List<BiZ.WenWen.WenWen>();
            IList<int> wenwenanswerlist = new List<int>();
            IList<BiZ.InterestCenter.Interest> interestlist = new List<BiZ.InterestCenter.Interest>();
            foreach (var obj in wenwen)
            {
                IList<BiZ.WenWen.WenWenAnswer> answer = BiZ.WenWen.WenWenProvider.GetWenWenAnswer(obj.ID, 0, 0, 1);
                wenwenlist.Add(obj);
                wenwenanswerlist.Add(answer.Count);
                interestlist.Add(BiZ.InterestCenter.InterestFactory.GetInterest(obj.InterestID));
            }
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel(wenwenlist, wenwenanswerlist, interestlist);
            return Json(new JavaScriptSerializer().Serialize(model));
            //return View();
        }
        /// <summary>
        /// 根据回复用户的id获取用户的兴趣
        /// </summary>
        /// <param name="Memberid">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetAnswerInterest(String memberid)
        {
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberid, 10, 1);
            Models.PageModels.InterestWenWenModel model = new Models.PageModels.InterestWenWenModel();
            model.interests = interests;
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 添加我喜欢
        /// </summary>
        /// <param name="id">话题id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddMyLike(String id)
        {
            String userid = User.Identity.Name;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.LikeData>(userid, id, BiZ.Like.LikeType.WenWen, BiZ.Like.LikeData.GetCollectionName());
            if (!ifLiked)
            {
                BiZ.Like.LikeDataFactory.AddLikeData(userid, id, BiZ.Like.LikeType.WenWen);

                //喜爱兴趣下话题时记录用户对兴趣下话题的喜好数据
                BiZ.Recommendation.TopicTrainingData inttTrai = new BiZ.Recommendation.TopicTrainingData(userid, id, BiZ.Recommendation.TopicTrainingDataType.Like);
                //喜爱兴趣下话题时改变兴趣下话题排名的分值
                CBB.RankingHelper.IRankingAble irk = BiZ.WenWen.WenWenProvider.GetWenWen(id);
                CBB.RankingHelper.RankingProvider.AddScores(irk, 1);
                //更新兴趣问问被喜欢的数量，加一
                BiZ.WenWen.WenWenProvider.UpdateLikeCount(id, 1, 0, userid, true);
                return Json(new JavaScriptSerializer().Serialize(BiZ.WenWen.WenWenProvider.GetWenWen(id).Likecount));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 用户喜欢兴趣话题操作
        /// </summary>
        /// <param name="topicId">兴趣话题编号</param>
        /// <returns>兴趣话题被用户喜欢数</returns>
        [HttpPost]
        [Authorize]
        public ActionResult MemberLikeInterestTopics(String topicId)
        {
            String userId = User.Identity.Name;
            Boolean ifLikedTopic = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.LikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.LikeData.GetCollectionName());
            if (!ifLikedTopic)
            {
                BiZ.Like.LikeDataFactory.AddLikeData(userId, topicId, BiZ.Like.LikeType.WenWen);

                //用户喜爱兴趣下话题时记录用户对兴趣下话题的喜好数据
                BiZ.Recommendation.TopicTrainingData inttTrai = new BiZ.Recommendation.TopicTrainingData(userId, topicId, BiZ.Recommendation.TopicTrainingDataType.Like);
                //用户喜爱兴趣下话题时改变兴趣下话题排名的分值
                CBB.RankingHelper.IRankingAble irk = BiZ.WenWen.WenWenProvider.GetWenWen(topicId);
                CBB.RankingHelper.RankingProvider.AddScores(irk, 1);
                //用户更新兴趣问问被喜欢的数量，加一
                BiZ.WenWen.WenWenProvider.UpdateLikeCount(topicId, 1, 0, userId, true);
                return Json(new JavaScriptSerializer().Serialize(BiZ.WenWen.WenWenProvider.GetWenWen(topicId).Likecount));
            }
            else
            {
                BiZ.Like.LikeDataFactory.DeleteLikeData<BiZ.Like.LikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.LikeData.GetCollectionName());

                //更新兴趣问问喜欢数，减一
                BiZ.WenWen.WenWenProvider.UpdateLikeCount(topicId, -1, 0, userId, false);
                return Json(new JavaScriptSerializer().Serialize(BiZ.WenWen.WenWenProvider.GetWenWen(topicId).Likecount));
            }
        }
        /// <summary>
        /// 用户是否喜欢指定兴趣话题
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="topicId">话题编号</param>
        /// <returns>是否已喜欢</returns>
        public static Boolean IfMemberLikedTopic(String userId, String topicId)
        {
            Boolean ifLikedTopic = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.LikeData>(userId, topicId, BiZ.Like.LikeType.WenWen, BiZ.Like.LikeData.GetCollectionName());

            return ifLikedTopic;
        }
        /// <summary>
        /// 按兴趣话题发布者性别获取精选兴趣话题
        /// </summary>
        /// <param name="createrSex">兴趣话题发布者性别</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣话题列表</returns>
        public static IList<BiZ.WenWen.WenWen> GetFeaturedInterestTopic(int createrSex, int pageSize = 0, int pageNo = 0)
        {
            //获取1200个兴趣话题日排名，取较大（240*5，去掉异性的补替，取较大）
            IList<CBB.RankingHelper.RankingList> dailyTopicRankingList = BiZ.Ranking.TopicRanking.GetDailyTopicRankingList(1200);
            //IList<CBB.RankingHelper.RankingList> dailyTopicRankingList = BiZ.Ranking.TopicRanking.GetWeeklyTopicRankingList(1200); //获取兴趣话题周排名

            List<String> topicIdList = new List<string>();
            foreach (var topic in dailyTopicRankingList) topicIdList.Add(topic.ObjID);
            String[] topicIds = (String[])topicIdList.ToArray();

            //按兴趣编号数组获取兴趣话题按喜欢数降序
            IList<BiZ.WenWen.WenWen> dailyInterestRankingTopicList = BiZ.WenWen.WenWenProvider.GetTopicIdArrWenWenLikeCountSorted(topicIds, 1, 0);

            //排序开始  **********

            //取240条算法选（48*5，算法选可能与管理员推荐选覆盖，取较大）
            //取出指定性别
            List<BiZ.WenWen.WenWen> dailyInterestRankingTopicList2 = new List<BiZ.WenWen.WenWen>();
            if (createrSex != 0)
            {
                foreach (BiZ.WenWen.WenWen topic in dailyInterestRankingTopicList)
                {
                    if (topic.Creater.Sex == createrSex) dailyInterestRankingTopicList2.Add(topic);
                    else continue;
                    if (dailyInterestRankingTopicList2.Count >= 240) break;
                }
            }
            else
            {
                dailyInterestRankingTopicList2.AddRange(dailyInterestRankingTopicList.Take(240));
            }

            //获取1800条管理员推荐选，取较大（360*5，去掉异性的补替，取较大）
            IList<BiZ.WenWen.WenWen> adminLikeTopicsList = BiZ.WenWen.WenWenProvider.GetAdminLikeOrNotTopics("", 1800, 1, true);

            //取出72条管理员推荐选指定性别创建者性别的兴趣话题
            List<BiZ.WenWen.WenWen> adminLikeTopicsList2 = new List<BiZ.WenWen.WenWen>();
            if (createrSex != 0)
            {
                foreach (BiZ.WenWen.WenWen topic in adminLikeTopicsList)
                {
                    if (topic.Creater.Sex == createrSex) adminLikeTopicsList2.Add(topic);
                    else continue;
                    if (adminLikeTopicsList2.Count >= 72) break;
                }
            }
            else
            {
                adminLikeTopicsList2.AddRange(adminLikeTopicsList.Take(72));
            }

            //去掉算法选在管理员推荐选中重复项
            var queryDailyInterestRankingTopicList =
                from tTopicList in dailyInterestRankingTopicList2
                where !(from tpa in adminLikeTopicsList2 select tpa.ID).Contains(tTopicList.ID)
                select tTopicList;

            List<BiZ.WenWen.WenWen> dailyInterestRankingTopicList3 = new List<BiZ.WenWen.WenWen>();
            //取出48条算法选
            dailyInterestRankingTopicList3.AddRange(queryDailyInterestRankingTopicList.Take(48));

            //分页，当前页
            pageNo = (pageNo == 0) ? 1 : pageNo;

            //
            ////算法选页大小10条，管理员推荐选页大小20条
            //int sysSelectTopicsPageSize = pageSize >= 10 ? 0 : pageSize, adminSelectTopicsPageSize = pageSize > 20 ? 0 : pageSize;

            ////分页算法选，取第一页，算法选10条
            //if (pageSize >= 1) dailyInterestRankingTopicList3 = dailyInterestRankingTopicList3.Skip((pageNo - 1) * (pageSize - adminSelectTopicsPageSize)).Take(pageSize - adminSelectTopicsPageSize).ToList();

            ////分页管理员推荐选，取第一页，管理员推荐选20条
            //if (pageSize >= 1) adminLikeTopicsList2 = adminLikeTopicsList2.Skip((pageNo - 1) * (pageSize - sysSelectTopicsPageSize)).Take(pageSize - sysSelectTopicsPageSize).ToList();
            //

            //合并算法选和管理员推荐选并按话题用户喜欢数排序
            var queryFeaturedInterestTopicList = (
                from lTopics in adminLikeTopicsList2 orderby lTopics.Likecount descending select lTopics
                ).Concat(
                from rTopics in dailyInterestRankingTopicList3 orderby rTopics.Likecount descending select rTopics
                ).Distinct().ToList();

            //合并算法选和管理员推荐选后按话题用户喜欢数排序
            var querySortedLikeCountFeaturedInterestTopicList = (
                from topics in queryFeaturedInterestTopicList orderby topics.Likecount descending select topics);

            //排序结束  **********

            List<BiZ.WenWen.WenWen> sortedLikeCountFeaturedInterestTopicList = new List<BiZ.WenWen.WenWen>();
            sortedLikeCountFeaturedInterestTopicList.AddRange(querySortedLikeCountFeaturedInterestTopicList);

            if (pageSize >= 1) sortedLikeCountFeaturedInterestTopicList = sortedLikeCountFeaturedInterestTopicList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            return sortedLikeCountFeaturedInterestTopicList;
        }
        /// <summary>
        /// 按兴趣编号获取问问
        /// </summary>
        /// <param name="interestId">兴趣编号</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetInterestIdTopic(String interestId, String pageSize, String pageNo)
        {
            int pgSize, pgNo;
            Int32.TryParse(pageNo, out pgNo);
            Int32.TryParse(pageSize, out pgSize);

            IList<BiZ.WenWen.WenWen> topicList = BiZ.WenWen.WenWenProvider.GetWenWen(interestId, pgSize, pgNo);
            return Json(new JavaScriptSerializer().Serialize(topicList));
        }
        /// <summary>
        /// 按用户编号获取话题总数
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetTopicCount(String memberId)
        {
            int topicCount = BiZ.WenWen.WenWenProvider.GetWenWenCount(memberId);
            return Json(new JavaScriptSerializer().Serialize(topicCount));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetBriefMemberIdTopics(String briefMemberId, String pageSize, String pageNo)
        {
            String memberId = "";

            //按转换后的编号和编号类型获取原始的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberId = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(briefMemberId, BiZ.Comm.UniqueNumber.IDType.MemberID);
            //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
            if (uNumberId != null) memberId = uNumberId.DefaultId;

            if (memberId == "") return Json(new JavaScriptSerializer().Serialize(null));

            int pgSize, pgNo;
            Int32.TryParse(pageNo, out pgNo);
            Int32.TryParse(pageSize, out pgSize);

            IList<BiZ.WenWen.WenWen> topicList = BiZ.WenWen.WenWenProvider.GetWenWenForMember(memberId, pgSize, pgNo);

            return Json(new JavaScriptSerializer().Serialize(topicList));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetBriefMemberIdTopicsCount(String briefMemberId)
        {
            long topicCount = 0;
            String memberId = GetBriefMemberIdOriginal(briefMemberId);
            if (memberId != "") topicCount = BiZ.WenWen.WenWenProvider.GetWenWenCount(memberId);
            return Json(new JavaScriptSerializer().Serialize(topicCount));
        }
        public String GetBriefMemberIdOriginal(String briefMemberId)
        {
            String memberId = "";

            //按转换后的编号和编号类型获取原始的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberId = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(briefMemberId, BiZ.Comm.UniqueNumber.IDType.MemberID);
            //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
            if (uNumberId != null) memberId = uNumberId.DefaultId;

            return memberId;
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetTypesetFeaturedInterestTopic(int publishedTopicSex, int pageSize = 0, int pageNo = 0)
        {
            IList<BiZ.WenWen.WenWen> featuredInterestTopicList = GetFeaturedInterestTopic(publishedTopicSex, pageSize, pageNo);
            Dictionary<BiZ.WenWen.WenWen, BiZ.WenWen.WenWen> leftRightFeaturedTopicLists = WenWenController.TypesettingFeaturedInterestTopics(featuredInterestTopicList);
            List<Moooyo.BiZ.WenWen.WenWen> leftTopicList = leftRightFeaturedTopicLists.Keys.ToList();
            List<Moooyo.BiZ.WenWen.WenWen> rightTopicList = leftRightFeaturedTopicLists.Values.ToList();
            Models.PageModels.MemberFeaturedInterestTopicModel memberFeaturedInterestTopicModel = new Models.PageModels.MemberFeaturedInterestTopicModel(leftTopicList, rightTopicList);
            return Json(new JavaScriptSerializer().Serialize(memberFeaturedInterestTopicModel));
        }
        /// <summary>
        /// 排版的精选兴趣话题
        /// </summary>
        /// <param name="featuredInterestTopicList">精选兴趣话题列表</param>
        /// <returns>左右兴趣话题键值对列表</returns>
        public static Dictionary<BiZ.WenWen.WenWen, BiZ.WenWen.WenWen> TypesettingFeaturedInterestTopics(IList<BiZ.WenWen.WenWen> featuredInterestTopicList)
        {
            int flag = 0, leftItemCount = 0, rightItemCount = 0;
            long topicCount = featuredInterestTopicList.Count;
            Dictionary<BiZ.WenWen.WenWen, BiZ.WenWen.WenWen> leftRightFeaturedTopicList = new Dictionary<BiZ.WenWen.WenWen, BiZ.WenWen.WenWen>();
            List<Moooyo.BiZ.WenWen.WenWen> leftTopicList = new List<Moooyo.BiZ.WenWen.WenWen>();
            List<Moooyo.BiZ.WenWen.WenWen> rightTopicList = new List<Moooyo.BiZ.WenWen.WenWen>();

            if (featuredInterestTopicList != null)
            {
                foreach (Moooyo.BiZ.WenWen.WenWen topic in featuredInterestTopicList)
                {
                    if (topic.ContentImage == "" || topic.ContentImage == null)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 2;
                    }

                    if (leftItemCount <= rightItemCount)
                    {
                        leftTopicList.Add(topic);
                        leftItemCount = flag + leftItemCount;
                    }
                    else
                    {
                        rightTopicList.Add(topic);
                        rightItemCount = flag + rightItemCount;
                    }
                }
            }
            int leftTopicListCount = leftTopicList.Count, rightTopicListCount = rightTopicList.Count, topicListCountFlag;
            if (leftTopicListCount >= rightTopicListCount) topicListCountFlag = leftTopicListCount;
            else topicListCountFlag = rightTopicListCount;
            for (int i = 0; i < topicListCountFlag; i++)
            {
                if (leftTopicList.Count > i && rightTopicList.Count > i)
                    leftRightFeaturedTopicList.Add(leftTopicList[i], rightTopicList[i]);
            }

            return leftRightFeaturedTopicList;
        }
        #endregion 
    }
}
