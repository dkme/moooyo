using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    /// <summary>
    /// 关系控制器
    /// </summary>
    public class ActivityController: Controller
    {
        #region 视图方法

        #endregion

        #region 数据与业务方法
        [Authorize]
        public ActionResult GetMemberUnReadCount()
        {
            String userid = HttpContext.User.Identity.Name;

            int count = 0;
            if (userid!=null)
             count = BiZ.MemberManager.MemberManager.GetMemberUnReadCount(userid);
            //设置用户在线
            BiZ.MemberManager.MemberManager.SetOnline(userid);

            return Json(new JavaScriptSerializer().Serialize(count));
        }
        [Authorize]
        public ActionResult GetUnReadCounters()
        {
            String userId = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(userId);
            string[] counts = {
                              memberObj.Status.UnReadMsgCount.ToString(),
                              memberObj.Status.UnReadActivitysAboutMeCount.ToString(),
                              memberObj.Status.UnReadBeenViewedTimes.ToString(),
                              memberObj.Status.UnReadSystemMsgCount.ToString()
                              };

            return Json(new JavaScriptSerializer().Serialize(counts));
        }
        [Authorize]
        public ActionResult GetUnReadMsgCount()
        {
            String userId = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(userId);
            int count = memberObj.Status.UnReadMsgCount;

            return Json(new JavaScriptSerializer().Serialize(count));
        }
        [Authorize]
        public ActionResult GetUnReadSystemMsgCount()
        {
            String userId = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(userId);
            int count = memberObj.Status.UnReadSystemMsgCount;

            return Json(new JavaScriptSerializer().Serialize(count));
        }
        [Authorize]
        public ActionResult GetUnReadBeenFavorCount(string userID)
        {
            if(userID == "") userID = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(userID);
            int count = memberObj.Status.UnReadBeenFavorCount;

            return Json(new JavaScriptSerializer().Serialize(count));
        }
        [Authorize]
        public ActionResult GetUnReadActivitysAboutMeCount()
        {
            String userId = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(userId);
            int count = memberObj.Status.UnReadActivitysAboutMeCount;

            return Json(new JavaScriptSerializer().Serialize(count));
        }
        [Authorize]
        public ActionResult MsgToMember(String toMember, String comment,String type)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int t = 0;
            if (!int.TryParse(type, out t)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String fromMember = User.Identity.Name;
            return Json(BiZ.Member.Activity.ActivityController.MsgToMember(fromMember, toMember, comment, (BiZ.Member.Activity.ActivityType)t));
        }
        [Authorize]
        public ActionResult DateToMember(String toMember, String comment)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int t = 133;

            String fromMember = User.Identity.Name;

            String datesendcontent = BiZ.Sys.Wants.SystemWantsFactory.GetSystemWantsContentByWantsStr(comment);
            if (datesendcontent != "")
                BiZ.Member.Activity.ActivityController.MsgToMember(fromMember, toMember, datesendcontent, (BiZ.Member.Activity.ActivityType)t);
            else
            {
                String datestr = "我想约你和我一起" + comment + "哦";
                BiZ.Member.Activity.ActivityController.MsgToMember(fromMember, toMember, datestr, (BiZ.Member.Activity.ActivityType)t);
            }
            return Json(new CBB.ExceptionHelper.OperationResult(true));
        }
        [Authorize]
        public ActionResult VisitMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.VisitMember(fromMember, toMember));
        }
        [Authorize]
        public ActionResult FavorMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                fromMember,
                toMember,
                BiZ.Sys.MemberActivity.MemberActivityType.FavorOther,
                "/Content/TaContent/" + toMember);

            return Json(BiZ.Member.Activity.ActivityController.FavorMember(fromMember, toMember));
        }
        [Authorize]
        public ActionResult UpdateFavorComment(String toMember, String comment)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.UpdateFavorComment(fromMember, toMember, comment));
        }
        [Authorize]
        public ActionResult DeleteFavorMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;
            return Json(BiZ.Member.Activity.ActivityController.DeleteFavorMember(fromMember, toMember));
        }
        [Authorize]
        public ActionResult DeleteMemberFavor(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.DeleteFavorMember(toMember, fromMember));
        }
        //[Authorize]
        //public ActionResult ScoreMember(String toMember, String score)
        //{
        //    if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String fromMember = User.Identity.Name;

        //    int scoreint = -1;
        //    Int32.TryParse(score, out scoreint);

        //    switch (scoreint)
        //    {
        //        case 8:
        //            MsgToMember(toMember, getscoremsg().Replace("@score", scoreint.ToString()), "140");
        //            break;
        //        case 9:
        //            MsgToMember(toMember, getscoremsg().Replace("@score",scoreint.ToString()), "140");
        //            break;
        //        case 10:
        //            MsgToMember(toMember, getscoremsg().Replace("@score", scoreint.ToString()), "140");
        //            break;
        //    }

        //    return Json(new JavaScriptSerializer().Serialize(BiZ.Member.Activity.ActivityController.ScoreMember(fromMember, toMember,scoreint)));
        //}
        private String getscoremsg()
        {
            List<string> msgs = new List<string>();
            msgs.Add("@score分都无法形容我的对你的喜欢，能聊几句吗？");
            msgs.Add("@score分只是我对你的喜欢你的冰山一角而已，更多的让我们聊聊吧。");
            msgs.Add("Hi, 先给你@score分，更多的在后面呢...聊聊吧。");
            msgs.Add("你已经把我迷倒了，不得不给你@score分，可以聊聊吗？");
            msgs.Add("对于你这种类型，我似乎没什么抵抗力，给你@score分不算多，应该很多人都这么说吧");

            Random rnd = new Random();
            IEnumerable<string> rndlist = msgs.OrderBy(x => rnd.Next()).Take(1);
            return rndlist.ToList<string>()[0];
        }
        //[Authorize]
        //public ActionResult MarkMember(String toMember,String markType, String content, String contentsend)
        //{
        //    if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
        //    String fromMember = User.Identity.Name;

        //    if (fromMember == toMember)
        //        return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(false,"不能给自己贴标签")));

        //    int markTypeint = -1;
        //    Int32.TryParse(markType,out markTypeint);

        //    CBB.ExceptionHelper.OperationResult result = BiZ.Member.Activity.ActivityController.MarkMember(fromMember, toMember, (BiZ.Mark.MarkType)markTypeint, content);

        //    if (result.ok)
        //    {
        //        MsgToMember(toMember, contentsend, "139");
        //    }

        //    return Json(new JavaScriptSerializer().Serialize(result));
        //}
        [Authorize]
        public ActionResult AddGiftor(String toMember,String giftID,String giftName,String comment)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.AddGiftor(fromMember, toMember,giftID,giftName,comment));
        }
        [Authorize]
        public ActionResult SilentToMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.SilentToMember(fromMember, toMember));
        }
        [Authorize]
        public ActionResult DisableMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.DisableMember(fromMember, toMember));
        }
        [Authorize]
        public ActionResult DeleteDisableMember(String toMember)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;

            return Json(BiZ.Member.Activity.ActivityController.DeleteDisableMember(fromMember, toMember));
        }
        [HttpPost]
        public ActionResult PresentGlamourValue(String toMember, String glamourType, String glamourValue)
        {
            if (toMember == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (glamourType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (glamourValue == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String fromMember = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result;
            if(!Models.DisplayObjProvider.IsInFavor(fromMember, toMember))
                return Json(new CBB.ExceptionHelper.OperationResult(false, "关注TA才能给TA赠送魅力值"));
            int glamType = Convert.ToInt32(glamourType), glamValue = Convert.ToInt32(glamourValue);
            result = BiZ.Member.Activity.ActivityController.AddGlamourValue(fromMember, toMember, (BiZ.Member.GlamourCounts.GlamourCountOperate.GlamourCountType)glamType, (BiZ.Member.GlamourCounts.GlamourCountOperate.ModifyGlamourValue)glamValue, "");
            return Json(result);
        }

        #region Activity
        [Authorize]
        public ActionResult GetFavorMemberActivitys(String mid, String pagesize, String pageno)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int size = 0, no = 0;
            if (!int.TryParse(pagesize, out size)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pageno, out no)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            return Json(new JavaScriptSerializer().Serialize(BiZ.Member.Activity.ActivityController.GetFavorMemberActivitys(mid, size, no)));
        }

        public static string GetActivityStr(BiZ.Member.Activity.ActivityHolderBase aHLObjs, Moooyo.WebUI.Models.RelationDisplayObj relatObj)
        {
            string str = "";
            foreach (Moooyo.BiZ.Member.Activity.Activity aObjs in aHLObjs.Activitys)
            {
                switch ((int)aObjs.type)
                {
                    case 113:
                        str += ActivityController.GetMemberLikeTopic(aObjs);
                        break;
                    //case 139:
                    //    str += ActivityController.GetFavorMemberActivityBeenMarked(aObjs, aHLObjs.MemberID);
                    //    break;
                    //case 140:
                    //    str += ActivityController.GetFavorMemberActivityBeenScored(aObjs, aHLObjs.MemberID);
                    //    break;
                    //case 210:
                    //    str += ActivityController.GetFavorMemberActivityUploadPhoto(aObjs, aHLObjs.MemberID);
                    //    break;
                    case 215:
                        str += ActivityController.GetFavorMemberActivitySetIcon(aObjs, aHLObjs.MemberID);
                        break;
                    case 216:
                        str += ActivityController.GetFavorMemberActivityUpdateLocation(aObjs, aHLObjs.MemberID);
                        break;
                    case 217:
                        str += ActivityController.GetFavorMemberActivityUpdatePhotoComment(aObjs, aHLObjs.MemberID);
                        break;
                    case 218:
                        str += ActivityController.GetFavorMemberActivityUpdateIWant(aObjs, aHLObjs.MemberID);
                        break;
                    //case 219:
                    //    str += ActivityController.GetFavorMemberActivityUpdateSoliloquize(aObjs, aHLObjs.MemberID);
                    //    break;
                    //case 220:
                    //    str += ActivityController.GetFavorMemberActivityUpdateIKnow(aObjs, aHLObjs.MemberID);
                    //    break;
                    //case 221:
                    //    str += ActivityController.GetFavorMemberActivityUpdateWantLearn(aObjs, aHLObjs.MemberID);
                    //    break;
                    case 222:
                        str += ActivityController.GetFavorMemberActivityUpdateInterview(aObjs, aHLObjs.MemberID);
                        break;
                    case 302:
                        str += ActivityController.GetFavorMemberActivityAddInterest(aObjs, aHLObjs.MemberID);
                        break;
                    case 701:
                        str += ActivityController.GetMemberBeenFavored(aObjs);
                        break;
                    case 704:
                        str += ActivityController.GetMemberPhotoBeenCommented(aObjs);
                        break;
                    case 705:
                        str += ActivityController.GetMemberJoinToMyInterestFansGroup(aObjs);
                        break;
                    //case 706:
                    //    str += ActivityController.GetMemberAddQuestToMyInterest(aObjs);
                    //    break;
                    case 707:
                        str += ActivityController.GetMemberActivityPresentGlamourValue(aObjs);
                        break;
                    //case 708:
                    //    str += ActivityController.GetMemberWenWenQuestionBeenAnswered(aObjs);
                    //    break;
                    case 709:
                        str += ActivityController.GetMemberBeenVisited(aObjs);
                        break;
                    case 711:
                        str += ActivityController.GetMemberCallForBeenLiked(aObjs);
                        break;
                    case 712:
                        str += ActivityController.GetMemberCallForBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 713:
                        str += ActivityController.GetMemberCallForBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 714:
                        str += ActivityController.GetMemberImageBeenLiked(aObjs);
                        break;
                    case 715:
                        str += ActivityController.GetMemberImageBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 716:
                        str += ActivityController.GetMemberImageBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 717:
                        str += ActivityController.GetMemberInterestBeenLiked(aObjs);
                        break;
                    case 718:
                        str += ActivityController.GetMemberInterestBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 719:
                        str += ActivityController.GetMemberInterestBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 729:
                        str += ActivityController.GetMemberInterViewBeenLiked(aObjs);
                        break;
                    case 730:
                        str += ActivityController.GetMemberInterViewBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 720:
                        str += ActivityController.GetMemberIWantBeenLiked(aObjs);
                        break;
                    case 721:
                        str += ActivityController.GetMemberIWantBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 722:
                        str += ActivityController.GetMemberIWantBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 723:
                        str += ActivityController.GetMemberMoodBeenLiked(aObjs);
                        break;
                    case 724:
                        str += ActivityController.GetMemberMoodBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 725:
                        str += ActivityController.GetMemberMoodBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 726:
                        str += ActivityController.GetMemberTalkAboutBeenLiked(aObjs);
                        break;
                    case 727:
                        str += ActivityController.GetMemberTalkAboutBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 728:
                        str += ActivityController.GetMemberTalkAboutBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 731:
                        str += ActivityController.GetMemberMembeSetLocationBeenLiked(aObjs);
                        break;
                    case 732:
                        str += ActivityController.GetMemberMembeSetLocationBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 733:
                        str += ActivityController.GetMemberMembeSetLocationBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 734:
                        str += ActivityController.GetMemberBadMoodBeenLiked(aObjs);
                        break;
                    case 735:
                        str += ActivityController.GetMemberBadMoodBeenCommented(aObjs, relatObj.Name);
                        break;
                    case 736:
                        str += ActivityController.GetMemberBadMoodBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 737:
                        str += ActivityController.GetMemberMembeSetAvatarBeenLiked(aObjs);
                        break;
                    case 738:
                        str += ActivityController.GetMemberMembeSetAvatarBeenCommented(aObjs, relatObj.Name, relatObj.Name);
                        break;
                    case 739:
                        str += ActivityController.GetMemberMembeSetAvatarBeenLikeAndCommented(aObjs, relatObj.Name);
                        break;
                    case 740:
                        str += ActivityController.GetMemberCommentBeenReplied(aObjs, relatObj.Name, relatObj.Name);
                        break;
                    default: break;
                }
                str += "<div class='cgray' style='clear:both;height:30px;line-height:30px'>" + Moooyo.WebUI.Common.Comm.getTimeSpan(aObjs.CreatedTime) + "</div>";
            }
            return str;
        }

        //public static string GetFavorMemberActivityUploadPhoto(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>上传了(" + activityObj.Title + ")张照片：</span> <a href=\"/photo/mplist/" + memberID + "?t=-1\" target=\"_blank\">[去看看]</a></div><ul class=\"changephoto\">";
        //    foreach (string content in activityObj.Content)
        //    {
        //        str += "<li><img src=" + content + " onerror=\"$(this).parent().hide();\" /></li>";
        //    }
        //    str += "</ul><div class='clear'></div>";
        //    return str;
        //}
        public static string GetFavorMemberActivitySetIcon(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class=\"colorf90\">更新了头像：</span> <a href=\"/Content/TaContent/" + memberID + "/all/1\" target=\"_blank\">[去看看]</a></div><div>";
            foreach (string content in activityObj.Content)
            {
                str += "<img src=\"" + content + "\" border=\"0\" width=\"100\" height=\"100\" />";
            }
            str += "</div>";
            return str;
        }
        public static string GetFavorMemberActivityUpdateLocation(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>更新了Ta的位置。</span> <a href='/Content/TaContent/" + memberID + "/all/1' target=\"_blank\">[点击看看Ta在哪]</a></div>";
            return str;
        }
        public static string GetFavorMemberActivityUpdatePhotoComment(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>更新了(" + activityObj.Title + ")张照片的内容：</span> <a href='/photo/mplist/" + memberID + "?t=-1' target='_blank'>[去看看]</a></div><ul class='changephotoinfor'>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 3) { continue; }
                str += "<li><img height='50' src='" + arrStrs[0] + "' class='fl mr5'/><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "<br/>" + arrStrs[2];
                str += "</p></li>";
            }
            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetFavorMemberActivityUpdateIWant(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>更新了想做的事：</span>";
            str += "<span class='cgreen'>";
            foreach (string content in activityObj.Content)
            {
                str += "“我想和Ta" + content + "”";
            }
            str += "</span></div>";
            return str;
        }
        //public static string GetFavorMemberActivityUpdateSoliloquize(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>更新了签名档：</span> <a href='/member/i/" + memberID + "' target='_blank'>[去看看]</a></div><div>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        str += "“" + content + "”";
        //    }
        //    str += "</div>";
        //    return str;
        //}
        //public static string GetFavorMemberActivityUpdateIKnow(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>更新了(" + activityObj.Title + ")个才艺：</span> <a href='/member/i/" + memberID + "/skill' target='_blank'>[去看看]</a></div><ul>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        string[] arrStrs = content.Split('|');
        //        str += "<li>我会" + arrStrs[0] + "，" + arrStrs[1] + "。</li>";
        //    }
        //    str += "</ul>";
        //    return str;
        //}
        //public static string GetFavorMemberActivityUpdateWantLearn(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>更新了(" + activityObj.Title + ")个想学：</span> <a href='/member/i/" + memberID + "/skill' target='_blank'>[去看看]</a></div><ul>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        if (content == null) continue;
        //        string[] arrStrs = content.Split('|');
        //        str += "<li>我想学" + arrStrs[0] + "，" + arrStrs[1] + "。</li>";
        //    }
        //    str += "</ul>";
        //    return str;
        //}
        public static string GetFavorMemberActivityUpdateInterview(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>更新了(" + activityObj.Title + ")个小编访问：</span> <a href='/Content/TaContent/" + memberID + "/all/1' target='_blank'>[去看看]</a></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                str += "<li>小编：" + arrStrs[0] + "</li>";
                str += "<li class='color0e6ece'>回答：" + arrStrs[1] + "</li>";
            }
            str += "</ul>";
            return str;
        }
        //public static string GetFavorMemberActivityBeenMarked(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>收到了(" + activityObj.Title + ")个新印象：</span></div><div class='changemark'>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        str += "<span class='markdiv'>“" + content + "”</span>";
        //    }
        //    str += "</div><div class='clear'></div>";
        //    return str;
        //}
        //public static string GetFavorMemberActivityBeenScored(BiZ.Member.Activity.Activity activityObj, String memberID)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>收到了(" + activityObj.Title + ")个评分：</span></div>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        if (Convert.ToInt16(content) == 10)
        //            str += "<div class='hs_vinscore2 f-img fwhite fl magR5'>" + content + "</div>";
        //        else
        //            str += "<div class='hs_vinscore f-img fwhite fl magR5 top3'>" + content + "</div>";
        //    }
        //    str += "<div class='clear'></div>";
        //    return str;
        //}
        public static string GetMemberActivityBeenFavored(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>粉丝团增加了(" + activityObj.Title + ")个新粉丝：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                str += "<li class='fl'><a href='/Content/TaContent/" + arrStrs[0] + "/all/1' target='_blank'><img src='" + Moooyo.WebUI.Common.Comm.getImagePath(arrStrs[1], Common.ImageType.Icon) + "' alt='" + arrStrs[1] + "'/></a></li>";
            }
            str += "<li class='clearfix'/></ul>";
            return str;
        }
        public static string GetMemberActivityPresentGlamourValue(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "<ul>";

            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 2) continue;
                str += "<li><span class='colorf90'>mo了你的“" + arrStrs[1] + "”动态，同时赠送你" + arrStrs[0] + "分魅力值。</span></li>";
                //str += "<li><span class='colorf90'>给你赠送了" + arrStrs[0] + "分魅力值。</li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberBeenFavored(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>加入了你的粉丝团。</span></div>";
            return str;
        }
        //public static string GetMemberWenWenQuestionBeenAnswered(BiZ.Member.Activity.Activity activityObj)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90 mt10'>参与了你发表的话题(" + activityObj.Title + ")：</span></div><ul>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        string[] arrStrs = content.Split('|');
        //        str += "<li><a href='/WenWen/ShowWenWen?wwid=" + arrStrs[0] + "' target='_blank'><span class='cgray'>你：" + (arrStrs[1].Length > 78 ? arrStrs[1].Substring(0, 78) + "<span class=\"letspa--3\">...</span>" : arrStrs[1]) + "</span><br/>Ta：" + (arrStrs[2].Length > 78 ? arrStrs[2].Substring(0, 78) + "<span class=\"letspa--3\">...</span>" : arrStrs[2]) + "</a></li>";
        //    }
        //    str += "</ul>";
        //    return str;
        //}
        public static string GetMemberPhotoBeenCommented(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>在你的相册写了(" + activityObj.Title + ")个评论：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                str += "<li><a href='/Photo/Show/" + arrStrs[0] + "' target='_blank'><img src='" + Common.Comm.getImagePath(arrStrs[1], Common.ImageType.Icon) + "'/> Ta：" + arrStrs[2] + "</a></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberJoinToMyInterestFansGroup(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>加入了你创建的(" + activityObj.Title + ")个兴趣：</span></div><ul>";
            String title = "";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                if(arrStrs.Length >= 3)
                    title = arrStrs[2];
                str += "<li><a href='/InterestCenter/ShowInterest/" + arrStrs[0] + "' title=\"" + title + "\"><img src='" + Common.Comm.getImagePath(arrStrs[1], Common.ImageType.Icon) + "'/></a></li>";
            }
            str += "</ul>";
            return str;
        }
        //public static string GetMemberAddQuestToMyInterest(BiZ.Member.Activity.Activity activityObj)
        //{
        //    string str = "";
        //    str += "<div><span class='colorf90'>在你创建的(" + activityObj.Title + ")个兴趣中发布了话题：</span></div><ul>";
        //    foreach (string content in activityObj.Content)
        //    {
        //        string[] arrStrs = content.Split('|');
        //        str += "<li><a href='/InterestCenter/InterestFans?iid=" + arrStrs[0] + "' target='_blank'><img src='" + Common.Comm.getImagePath(arrStrs[1], Common.ImageType.Icon) + "'/>" + arrStrs[3] + "</a></li>";
        //    }
        //    str += "</ul>";
        //    return str;
        //}
        public static string GetMemberLikeTopic(BiZ.Member.Activity.Activity activityObj)
        {
            string str = string.Empty;
            str += "<div><span class='colorf90'>mo了(" + activityObj.Title + ")个话题：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                str += "<li><a href='/WenWen/ShowWenWen?wwid=" + arrStrs[0] + "' target='_blank'>" + arrStrs[1] + "</a></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetFavorMemberActivityAddInterest(BiZ.Member.Activity.Activity activityObj, String memberID)
        {
            string str = "";
            str += "<div><span class='colorf90'>创建了(" + activityObj.Title + ")个兴趣</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                str += "<li class=\"clearBoth adaptive-height min-height-60\"><div class=\"fl wh-50 adaptive-height min-height-60\"><a href='/InterestCenter/ShowInterest/" + arrStrs[0] + "' title=\"" + arrStrs[1] + "\"><img src='" + Common.Comm.getImagePath(arrStrs[3], Common.ImageType.Icon) + "'/></a></div><div class=\"adaptive-height min-height-60 w450 fl pl-5\">标题：" + arrStrs[1] + "<br />内容：" + arrStrs[2] + "</div></li>";
            }
            str += "</ul>";
            return str;
        }

        public static string GetMemberBeenVisited(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>访问了你的主页(" + activityObj.Title + ")。</span></div>";
            return str;
        }

        public static string GetMemberCallForBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>mo了你的号召(" + activityObj.Title + ")。</span></div>";
            return str;
        }
        public static string GetMemberCallForBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            //String[] arrContentDetails = activityObj.Content[2].Split('|');
            str += "<div><span class='colorf90'>评论了你的号召(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            str += GetActivityCommentReplyStr(activityObj.Content, memberName);

            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberCallForBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>mo了并评论了你的号召(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            str += GetActivityCommentReplyStr(activityObj.Content, memberName);

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberImageBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>mo了你发布的图片(" + activityObj.Title + ")。</span></div><ul class='changephotoinfor'>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                String imgStrs = arrStrs[2];
                String[] arrImgStrs = imgStrs.Split(',');
                foreach (String imgStr in arrImgStrs)
                {
                    str += "<li class=\"image\"><img width='50' src='" + imgStr + "' class='fl mr5'/></li>";
                }
            }
            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberImageBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 4)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[3].ToString();
            str += "<div><span class='colorf90'>评论了你发布的图片(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            str += GetActivityImageCommentReplyStr(activityObj.Content, memberName);

            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberImageBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 4)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[3].ToString();
            str += "<div><span class='colorf90'>mo了并评论了你发布的图片(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            str += GetActivityImageCommentReplyStr(activityObj.Content, memberName);

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberInterestBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            string inteTitle = "";
            string inteImage = "";
            str += "<div><span class='colorf90'>mo你的兴趣(" + activityObj.Title + ")。</span></div><ul class='changephotoinfor'>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length >= 6)
                {
                    inteTitle = arrStrs[5];
                    inteImage = "<img src='" + arrStrs[4] + "'/>";
                }
                str += "<li><a href='/InterestCenter/ShowInterest/" + arrStrs[0] + "' title=\"" + inteTitle + "\">" + inteImage + "</a></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberInterestBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的兴趣(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 5) { continue; }
                str += "<li><a href='/InterestCenter/ShowInterest/" + arrStrs[0] + "' title=\"" + arrStrs[5] + "\"><img height='50' src='" + arrStrs[4] + "' class='fl mr5'/></a><br />";
                str += arrStrs[1];
                str += "</li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberInterestBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>mo了并评论了你的兴趣(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 5) { continue; }
                str += "<li><a href='/InterestCenter/ShowInterest/" + arrStrs[0] + "' title=\"" + arrStrs[5] + "\"><img height='50' src='" + arrStrs[4] + "' class='fl mr5'/></a><p>";
                str += arrStrs[1];
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberInterViewBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>mo你的访谈(" + activityObj.Title + ")。</span></div>";
            return str;
        }
        public static string GetMemberInterViewBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的访谈(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            str += GetActivityCommentReplyStr(activityObj.Content, memberName);

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberIWantBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>mo你的“我想”(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                str += "<li><span class=\"cgreen\">“" + arrStrs[3] + "”</span></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberIWantBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的“我想”(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[3] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberIWantBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>mo了并评论了你的“我想”(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[3] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberMoodBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";

            str += "<div><span class='colorf90'>你的心情被关注(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { return ""; }
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li><p><span class=\"cgreen\">" + arrStrs[5] + "</span></p></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberMoodBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的“心情”(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[5] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberMoodBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 5)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[4].ToString();
            String content2 = activityObj.Content[0];
            if (content2 == null) { return ""; }
            string[] arrStrs2 = content2.Split('|');
            if (arrStrs2.Length < 4) { return ""; }
            str += "<div><span class='colorf90'>你的心情被关注，并评论(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[5] + "</span></p></li>";
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberBadMoodBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>你的心情被关注(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { return ""; }
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "</span></p></li>";
                str += "<li><p><span class=\"cgreen\">" + arrStrs[5] + "</span></p></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberBadMoodBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 5)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[4].ToString();
            str += "<div><span class='colorf90'>评论了你的“心情”(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[5] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberBadMoodBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 5)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[4].ToString();
            String content2 = activityObj.Content[0];
            if (content2 == null) { return ""; }
            string[] arrStrs2 = content2.Split('|');
            if (arrStrs2.Length < 4) { return ""; }
            str += "<div><span class='colorf90'>你的心情被关注，并评论(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[5] + "</span></p></li>";
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberTalkAboutBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";
            str += "<div><span class='colorf90'>mo你的说说(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) continue;
                str += "<li><span class=\"cgreen\">“" + arrStrs[3] + "”</span></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberTalkAboutBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的说说(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[3] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul><div class='clear'></div>";
            return str;
        }
        public static string GetMemberTalkAboutBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>mo了并评论了你的说说(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul class='changephotoinfor'>";

            foreach (string content in activityObj.Content)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                str += "<li><p><span class='cgreen'>" + arrStrs[3] + "</span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        public static string GetMemberMembeSetLocationBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";

            str += "<div><span class='colorf90'>你的新位置被关注(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                String[] arrStrs2 = arrStrs[5].Split('&');
                if (arrStrs.Length < 6) { return ""; }
                if (arrStrs2.Length < 2) { return ""; }
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li><p><span class=\"cgreen\"><img src=\"http://maps.google.com/maps/api/staticmap?center=" + arrStrs2[0] + "," + arrStrs2[1] + "&zoom=13&size=150x100&maptype=roadmap&markers=color:red|label:A|" + arrStrs2[0] + "," + arrStrs2[1] + "&sensor=false\" onload=\"getContentImageFristLoad($(this))\"/></span></p></li>";
            }
            str += "</ul>";

            return str;
        }
        public static string GetMemberMembeSetLocationBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>评论了你的新位置(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { return ""; }
                String[] arrStrs2 = arrStrs[5].Split('&');
                if (arrStrs2.Length < 2) { return ""; }
                str += "<li><p><span class=\"cgreen\"><img src=\"http://maps.google.com/maps/api/staticmap?center=" + arrStrs2[0] + "," + arrStrs2[1] + "&zoom=13&size=150x100&maptype=roadmap&markers=color:red|label:A|" + arrStrs2[0] + "," + arrStrs2[1] + "&sensor=false\" onload=\"getContentImageFristLoad($(this))\"/></span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul>";
            return str;
        }
        public static string GetMemberMembeSetLocationBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();
            str += "<div><span class='colorf90'>关注并评论了你的新位置(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                String[] arrStrs2 = arrStrs[5].Split('&');
                if (arrStrs.Length < 6) { return ""; }
                if (arrStrs2.Length < 2) { return ""; }
                str += "<li><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li><p><span class=\"cgreen\"><img src=\"http://maps.google.com/maps/api/staticmap?center=" + arrStrs2[0] + "," + arrStrs2[1] + "&zoom=13&size=150x100&maptype=roadmap&markers=color:red|label:A|" + arrStrs2[0] + "," + arrStrs2[1] + "&sensor=false\" onload=\"getContentImageFristLoad($(this))\"/></span></p></li>";
                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul>";
            return str;
        }

        public static string GetMemberMembeSetAvatarBeenLiked(BiZ.Member.Activity.Activity activityObj)
        {
            string str = "";

            str += "<div><span class='colorf90'>你的新头像被关注(" + activityObj.Title + ")。</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                String[] arrStrs2 = arrStrs[5].Split('&');
                if (arrStrs.Length < 6) { return ""; }
                if (arrStrs2.Length < 1) { return ""; }
                str += "<li style=\"float:none; clear:both;\"><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li style=\"float:none; clear:both;\"><p><span class=\"cgreen\"><img height='100' src='" + arrStrs[5] + "' class='fl mr5'/></span></p></li>";
            }
            str += "</ul>";

            return str;
        }
        public static string GetMemberMembeSetAvatarBeenCommented(BiZ.Member.Activity.Activity activityObj, String memberID, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();

            str += "<div><span class='colorf90'>评论了你的新头像(" + activityObj.Title + ") <a href=\"/Content/ContentDetail/" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 6) { return ""; }
                String[] arrStrs2 = arrStrs[5].Split('&');

                if (arrStrs2.Length < 1) { return ""; }
                str += "<li style=\"float:none; clear:both;\"><p><span class=\"cgreen\"><img height='100' src='" + arrStrs[5] + "' class='fl mr5'/></span></p></li>";
                str += "<li style=\"float:none; clear:both;\"><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul>";

            return str;
        }
        public static string GetMemberMembeSetAvatarBeenLikeAndCommented(BiZ.Member.Activity.Activity activityObj, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";
            if (activityObj.Content[0].Split('|').Length >= 3)
                contentDetailPath = "/Content/ContentDetail/" + activityObj.Content[0].Split('|')[2].ToString();

            str += "<div><span class='colorf90'>关注并评论了你的新头像(" + activityObj.Title + ") <a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a>：</span></div><ul>";
            foreach (string content in activityObj.Content)
            {
                if (content == null) { return ""; }
                string[] arrStrs = content.Split('|');
                String[] arrStrs2 = arrStrs[5].Split('&');
                if (arrStrs.Length < 6) { return ""; }
                if (arrStrs2.Length < 1) { return ""; }
                str += "<li style=\"float:none; clear:both;\"><p><span class=\"cgreen\">" + arrStrs[4] + "了你的“" + arrStrs[3] + "”</span></p></li>";
                str += "<li style=\"float:none; clear:both;\"><p><span class=\"cgreen\"><img height='100' src='" + arrStrs[5] + "' class='fl mr5'/></span></p></li>";
                str += "<li style=\"float:none; clear:both;\"><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                str += "</p></li>";
            }
            str += "</ul>";

            return str;
        }

        public static string GetMemberCommentBeenReplied(BiZ.Member.Activity.Activity activityObj, String memberID, String memberName)
        {
            string str = "", contentDetailPath = "javascript:;";

            str += "<div><span class='colorf90'>评论了你发布的评论(" + activityObj.Title + ")：</span></div><ul class=''>";

            foreach (string content in activityObj.Content)
            {
                string[] arrStrs = content.Split('|');

                if (content.Split('|').Length >= 1)
                    contentDetailPath = "/Content/ContentDetail/" + content.Split('|')[0].ToString();
                str += "<li>";
                if (arrStrs.Length > 3)
                {
                    str += "在" + arrStrs[3] + "内容中<br />";
                }
                if (arrStrs.Length > 4)
                {
                    String imgStrs = arrStrs[4];
                    if (imgStrs != "")
                    {
                        String[] arrImgStrs = imgStrs.Split(',');
                        str += "<ul class='changephotoinfor' style=\"clear:both; float:none;\">";
                        foreach (String imgStr in arrImgStrs)
                        {
                            str += "<li class=\"image\" style=\"float:left; display:block; clear:none;\"><img width='50' src='" + imgStr + "' class='fl mr5'/></li>";
                        }
                        str += "</ul>";
                    }
                   
                }
                str += "<div style=\"clear:both;\"><a href='javascript:;'><span class='cgray'>你：" + (arrStrs[2].Length > 78 ? arrStrs[2].Substring(0, 78) + "<span class=\"letspa--3\">...</span>" : arrStrs[2]) + "</span><br/>Ta：" + (arrStrs[1].Length > 78 ? arrStrs[1].Substring(0, 78) + "<span class=\"letspa--3\">...</span>" : arrStrs[1]) + "</a><br /><a href=\"" + contentDetailPath + "\" target=\"_blank\">[去看看]</a></div></li>";
            }

            str += "</ul><div class='clear'></div>";
            return str;
        }

        private static String GetActivityCommentReplyStr(IList<String> contentList, String memberName)
        {
            //IList<BiZ.Comment.Comment> commentList;
            //int commenCount = 0;
            String str = "";
            foreach (string content in contentList)
            {
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 3) { continue; }
                //commentList = BiZ.Comment.CommentProvider.GetMemberNameContentIDComment(arrStrs[0], "回应" + memberName + "：", BiZ.Comm.DeletedFlag.No, 0, 0);
                //commenCount = commentList.Count;

                str += "<li><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                //str += "<span>&nbsp;&nbsp;<a href=\"javascript:;\" class=\"gray01\" onclick=\"showHideReplyArea('" + arrStrs[0] + "', 'show')\">回应</a><span class=\"gray01\">（<span id=\"activityCommentReplyCount" + arrStrs[0] + "\">" + commenCount + "</span>）</span></span>";
                //str += "<div class=\"detail_ans\" id=\"reply" + arrStrs[0] + "\"><textarea class=\"txtanswer\" onkeyup=\"textareasize(this)\" id=\"activityComment" + arrStrs[0] + "\" onPropertyChange=\"checkLen($(this).val())\">回应" + memberName + "：</textarea><br /><a href=\"javascript:;\" class=\"gray01\" onclick=\"addActivityReplyComment('" + arrStrs[2] + "','activityComment','false','" + arrStrs[0] + "','" + memberName + "')\">保存</a>&nbsp;<a href=\"javascript:;\" class=\"gray01\" onclick=\"showHideReplyArea('" + arrStrs[0] + "', 'hide')\">取消</a></div>";

                //str += "<div class=\"detail_info\" id=\"activityCommentReply" + arrStrs[0] + "\">";

                //for (int i = 0; i < commentList.Count; i++)
                //{
                //    str += "<div class=\"clearfix\">";
                //    str += "<div class=\"ans_com\">";
                //    //str += "<div class=\"ans_compic\"></div>";
                //    str += "<div class=\"ans_span\">";
                //    str += "<span class=\"left_s\">";
                //    //str += "<a href=\"/Content/IContent/\" target=\"_blank\" class=\"blue02\">" + commentList[i].Creater.NickName + "</a>";
                //    str += "&nbsp;&nbsp;<font class=\"huise\">" + commentList[i].Content + "</font>";
                //    str += "&nbsp;<font class=\"gray01\">" + Common.Comm.getTimeSpan(commentList[i].CreatedTime) + "</font>";
                //    str += "</span>";
                //    str += "</div>";
                //    str += "</div></div>";
                //}

                //str += "</div>";
                str += "</p></li>";
            }
            return str;
        }
        private static String GetActivityImageCommentReplyStr(IList<String> contentList, String memberName)
        {
            //IList<BiZ.Comment.Comment> commentList;
            //int commentCount = 0;
            String str = "";

            foreach (string content in contentList)
            {
                //内容
                if (content == null) { continue; }
                string[] arrStrs = content.Split('|');
                if (arrStrs.Length < 4) { continue; }
                //图片
                String imgStrs = arrStrs[2];
                String[] arrImgStrs = imgStrs.Split(',');
                //评论
                //commentList = BiZ.Comment.CommentProvider.GetMemberNameContentIDComment(arrStrs[0], "回应" + memberName + "：", BiZ.Comm.DeletedFlag.No, 0, 0);
                //commentCount = commentList.Count;

                foreach (String imgStr in arrImgStrs)
                {
                    str += "<li class=\"image\"><img width='50' src='" + imgStr + "' class='fl mr5'/></li>";
                }

                str += "<li class=\"text\"><p>";
                str += "<span class='blue03'>" + arrStrs[1] + "</span>";
                //str += "<span>&nbsp;&nbsp;<a href=\"javascript:;\" class=\"gray01\" onclick=\"showHideReplyArea('" + arrStrs[0] + "', 'show')\">回应</a><span class=\"gray01\">（<span id=\"activityCommentReplyCount" + arrStrs[0] + "\">" + commentCount + "</span>）</span></span>";
                //str += "<div class=\"detail_ans\" id=\"reply" + arrStrs[0] + "\"><textarea class=\"txtanswer\" onkeyup=\"textareasize(this)\" id=\"activityComment" + arrStrs[0] + "\" onPropertyChange=\"checkLen($(this).val())\">回应" + memberName + "：</textarea><br /><a href=\"javascript:;\" class=\"gray01\" onclick=\"addActivityReplyComment('" + arrStrs[3] + "','activityComment','false','" + arrStrs[0] + "','" + memberName + "')\">保存</a>&nbsp;<a href=\"javascript:;\" class=\"gray01\" onclick=\"showHideReplyArea('" + arrStrs[0] + "', 'hide')\">取消</a></div>";

                //str += "<div class=\"detail_info\" id=\"activityCommentReply" + arrStrs[0] + "\">";

                //for (int i = 0; i < commentList.Count; i++)
                //{
                //    str += "<div class=\"clearfix\">";
                //    str += "<div class=\"ans_com\">";
                //    //str += "<div class=\"ans_compic\"></div>";
                //    str += "<div class=\"ans_span\">";
                //    str += "<span class=\"left_s\">";
                //    //str += "<a href=\"/Content/IContent/\" target=\"_blank\" class=\"blue02\">" + commentList[i].Creater.NickName + "</a>";
                //    str += "&nbsp;&nbsp;<font class=\"huise\">" + commentList[i].Content + "</font>";
                //    str += "&nbsp;<font class=\"gray01\">" + Common.Comm.getTimeSpan(commentList[i].CreatedTime) + "</font>";
                //    str += "</span>";
                //    str += "</div>";
                //    str += "</div></div>";
                //}

                //str += "</div>";
                str += "</p></li>";
            }
            return str;
        }
        #endregion

        #region Content dynamically
        //Content dynamically begin 
        internal static string GetMemberActivityContentImages(BiZ.Content.PublicContent content)
        {
            string imagesStr = "";
            IList<BiZ.Content.Image> imageList = new List<BiZ.Content.Image>();
            switch (content.ContentType)
            {
                case BiZ.Content.ContentType.CallFor: imageList = ((BiZ.Content.CallForContent)content).ImageList; break;
                case BiZ.Content.ContentType.Image: imageList = ((BiZ.Content.ImageContent)content).ImageList; break;
                case BiZ.Content.ContentType.SuiSuiNian: imageList = ((BiZ.Content.SuiSuiNianContent)content).ImageList; break;
            }
            foreach (BiZ.Content.Image image in imageList)
            {
                imagesStr += Common.Comm.getImagePath(image.ImageUrl, Common.ImageType.Middle) + ",";
            }
            if (imagesStr != "")
                imagesStr = imagesStr.Substring(0, imagesStr.Length - 1);
            return imagesStr;
        }
        /// <summary>
        /// 添加内容相关动态
        /// </summary>
        /// <param name="contentobj">内容对象</param>
        /// <param name="comment">评论文本</param>
        /// <param name="userid">用户编号</param>
        /// <param name="type">对内容的操作类别</param>
        /// <returns>操作状态</returns>
        internal static CBB.ExceptionHelper.OperationResult addActivityToContent(
            BiZ.Content.PublicContent contentobj, BiZ.Comment.Comment comment, String userid, string type)
        {
            CBB.ExceptionHelper.OperationResult result = null;

            if (contentobj.MemberID != userid)
            {
                string answercontent = comment != null ? comment.Content : "";
                BiZ.Content.ContentType contenttype = contentobj.ContentType;
                String activityTitle = "",
                    activityContent = "",
                    imagesStr = "",
                    strContent = "";
                BiZ.Member.Activity.ActivityType activityType = new BiZ.Member.Activity.ActivityType();
                //获取内容图片地址，拼接成字符串。
                imagesStr = GetMemberActivityContentImages(contentobj);
                switch (contenttype)
                {
                    case BiZ.Content.ContentType.CallFor:
                        //imagesStr = GetMemberActivityContentImages(contentobj);
                        strContent = ((BiZ.Content.CallForContent)contentobj).Content;
                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenLiked(
                                    "", "", contentobj.ID, imagesStr, strContent);
                                activityType = BiZ.Member.Activity.ActivityType.CallForBeenLiked;
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenCommented(
                                    comment.ID, comment.Content, contentobj.ID, imagesStr, strContent);
                                activityType = BiZ.Member.Activity.ActivityType.CallForBeenCommented;
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenLikeAndCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_CallForContentBeenLikeAndCommented(
                                    comment.ID, comment.Content, contentobj.ID, imagesStr, strContent);
                                activityType = BiZ.Member.Activity.ActivityType.CallForBeenLikeAndCommented;
                                break;
                            default: break;
                        }
                        break;
                    case BiZ.Content.ContentType.Image:
                        //imagesStr = GetMemberActivityContentImages(contentobj);
                        strContent = ((BiZ.Content.ImageContent)contentobj).Content;
                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenLiked(
                                    "",
                                    "",
                                    imagesStr,
                                    contentobj.ID,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.ImageBeenLiked;
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenCommented_Title();
                                activityContent =
                                    BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenCommented(
                                    comment.ID,
                                    comment.Content,
                                    imagesStr,
                                    contentobj.ID,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.ImageBeenCommented;
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenLikeAndCommented_Title();
                                activityContent =
                                    BiZ.Member.Activity.ActivityController.GetActivityContent_ImageContentBeenLikeAndCommented(
                                    comment.ID,
                                    comment.Content,
                                    imagesStr,
                                    contentobj.ID,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.ImageBeenLikeAndCommented;
                                break;
                            default: break;
                        }
                        break;
                    case BiZ.Content.ContentType.Interest:
                        BiZ.Content.InterestContent interContent = new BiZ.Content.InterestContent(contentobj.ID);
                        strContent = ((BiZ.Content.InterestContent)contentobj).Content;
                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenLiked_Title();
                                activityContent =
                                    BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenLiked(
                                    "",
                                    "",
                                    contentobj.ID,
                                    interContent.Interest.ID,
                                    Common.Comm.getImagePath(interContent.Interest.ICONPath, Common.ImageType.Middle),
                                    interContent.Interest.Title,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.InterestBeenLiked;
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenCommented_Title();
                                activityContent =
                                    BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    interContent.Interest.ID,
                                    Common.Comm.getImagePath(interContent.Interest.ICONPath, Common.ImageType.Middle),
                                    interContent.Interest.Title,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.InterestBeenCommented;
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenLikeAndCommented_Title();
                                activityContent =
                                    BiZ.Member.Activity.ActivityController.GetActivityContent_InterestContentBeenLikeAndCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    interContent.Interest.ID,
                                    Common.Comm.getImagePath(interContent.Interest.ICONPath, Common.ImageType.Middle),
                                    interContent.Interest.Title,
                                    strContent);

                                activityType = BiZ.Member.Activity.ActivityType.InterestBeenLikeAndCommented;
                                break;
                            default: break;
                        }
                        break;
                    case BiZ.Content.ContentType.InterView:
                        switch (type)
                        {
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_InterViewContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_InterViewContentBeenCommented(
                                    comment.ID, comment.Content, contentobj.ID);
                                activityType = BiZ.Member.Activity.ActivityType.InterViewBeenCommented;
                                break;
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_InterViewContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_InterViewContentBeenLiked("", "", contentobj.ID);
                                activityType = BiZ.Member.Activity.ActivityType.InterViewBeenLiked;
                                break;
                        }
                        break;
                    case BiZ.Content.ContentType.IWant:
                        BiZ.Content.IWantContent iWantContent = new BiZ.Content.IWantContent(contentobj.ID);
                        //imagesStr = GetMemberActivityContentImages(contentobj);
                        strContent = ((BiZ.Content.IWantContent)contentobj).Content;
                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenLiked(
                                    "",
                                    "",
                                    contentobj.ID,
                                    iWantContent.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.IWantBeenLiked;
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    iWantContent.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.IWantBeenCommented;
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenLikeAndCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_IWantContentBeenLikeAndCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    iWantContent.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.IWantBeenLikeAndCommented;
                                break;
                            default: break;
                        }
                        break;
                    case BiZ.Content.ContentType.Member:
                        string statusMember = ((BiZ.Content.MemberContent)contentobj).Type;
                        String strStatusMember = "",
                            strStatusMemberContent = "",
                            addition = "";
                        BiZ.Content.MemberContent memberContent = new BiZ.Content.MemberContent(contentobj.ID);
                        switch (statusMember)
                        {
                            case "0":
                                strStatusMemberContent = "新位置";
                                strStatusMember = "mo";
                                addition = memberContent.Lat + "&" + memberContent.Lng;
                                break;
                            case "1":
                                strStatusMemberContent = "新头像";
                                strStatusMember = "mo";
                                addition = Common.Comm.getImagePath(memberContent.Creater.ICONPath, Common.ImageType.Middle);
                                break;
                            default:
                                break;
                        }

                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenLiked(
                                    "", "", contentobj.ID, strStatusMemberContent, strStatusMember, addition);

                                switch (statusMember)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MembeSetLocationBeenLiked;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.MembeSetAvatarBeenLiked;
                                        break;
                                    default: break;
                                }
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenCommented(
                                    comment.ID, comment.Content, contentobj.ID, strStatusMemberContent, strStatusMember, addition);

                                switch (statusMember)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MemberSetLocationBeenCommented;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.MemberSetAvatarBeenCommented;
                                        break;
                                    default: break;
                                }
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenLikeAndCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MemberContentBeenLikeAndCommented(
                                    comment.ID, comment.Content, contentobj.ID, strStatusMemberContent, strStatusMember, addition);

                                switch (statusMember)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MemberSetLocationBeenLikeAndCommented;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.MemberSetAvatarBeenLikeAndCommented;
                                        break;
                                    default: break;
                                }
                                break;
                            default: break;
                        }
                        break;
                    case BiZ.Content.ContentType.Mood:
                        BiZ.Content.MoodContent moodContent = new BiZ.Content.MoodContent(contentobj.ID);
                        string statusMood = ((BiZ.Content.MoodContent)contentobj).Type;
                        String strStatusMood = "", strMood = "";
                        switch (statusMood)
                        {
                            case "0": strMood = "好心情";
                                strStatusMood = "羡慕";
                                break;
                            case "1": strMood = "坏心情";
                                strStatusMood = "安慰";
                                break;
                            default: break;
                        }

                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenLiked(
                                    "", "", strMood, strStatusMood, contentobj.ID, moodContent.Content);
                                activityType = BiZ.Member.Activity.ActivityType.MoodBeenLiked;

                                switch (statusMood)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MoodBeenLiked;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.BadMoodBeenLiked;
                                        break;
                                    default: break;
                                }
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenCommented(
                                    comment.ID, comment.Content, strMood, strStatusMood, contentobj.ID, moodContent.Content);

                                switch (statusMood)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MoodBeenCommented;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.BadMoodBeenCommented;
                                        break;
                                    default: break;
                                }
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenLikeAndCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_MoodContentBeenLikeAndCommented(
                                    comment.ID, comment.Content, strMood, strStatusMood, contentobj.ID, moodContent.Content);

                                switch (statusMood)
                                {
                                    case "0": activityType = BiZ.Member.Activity.ActivityType.MoodBeenLikeAndCommented;
                                        break;
                                    case "1": activityType = BiZ.Member.Activity.ActivityType.BadMoodBeenLikeAndCommented;
                                        break;
                                    default: break;
                                }
                                break;
                            default: break;
                        }

                        break;
                    case BiZ.Content.ContentType.SuiSuiNian:
                        BiZ.Content.SuiSuiNianContent talkAbout = new BiZ.Content.SuiSuiNianContent(contentobj.ID);
                        //imagesStr = GetMemberActivityContentImages(contentobj);
                        strContent = ((BiZ.Content.SuiSuiNianContent)contentobj).Content;
                        switch (type)
                        {
                            case "addLike":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenLiked_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenLiked(
                                    "",
                                    "",
                                    contentobj.ID,
                                    talkAbout.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.TalkAboutBeenLiked;
                                break;
                            case "addComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    talkAbout.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.TalkAboutBeenCommented;
                                break;
                            case "addLikeToComment":
                                activityTitle = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenLikeAndCommented_Title();
                                activityContent = BiZ.Member.Activity.ActivityController.GetActivityContent_TalkAboutContentBeenLikeAndCommented(
                                    comment.ID,
                                    comment.Content,
                                    contentobj.ID,
                                    talkAbout.Content,
                                    imagesStr,
                                    strContent);
                                activityType = BiZ.Member.Activity.ActivityType.TalkAboutBeenLikeAndCommented;
                                break;
                            default: break;
                        }
                        break;
                    default: break;
                }

                result = BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                        contentobj.MemberID,
                        userid,
                        activityType,
                        activityTitle,
                        activityContent,
                        false);
            }
            return result;
        }
        //Content dynamically end
        #endregion

        #endregion
    }
}