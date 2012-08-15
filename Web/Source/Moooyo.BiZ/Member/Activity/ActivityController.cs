using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Member.Activity
{

    public class ActivityController
    {
        #region 动态

        #region 用户动态文本
        public static String GetActivityContent_UpdateInterView_Title()
        {
            string str = "@COUNT";
            return str;
        }
        public static String GetActivityContent_UpdateInterView(String question, String answer)
        {
            string str = question + "|" + answer;
            return str;
        }
        //public static String GetActivityContent_UpdateWantLearn_Title()
        //{
        //    string str = "@COUNT";
        //    return str;
        //}
        //public static String GetActivityContent_UpdateWantLearn(String skillname, String content)
        //{
        //    string str = skillname + "|" + content;
        //    return str;
        //}
        //public static String GetActivityContent_UpdateIKnow_Title()
        //{
        //    string str = "@COUNT";
        //    return str;
        //}
        //public static String GetActivityContent_UpdateIKnow(String skillname,String content)
        //{
        //    string str = skillname + "|" + content;
        //    return str;
        //}
        //public static String GetActivityContent_UpdateSoliloquize_Title()
        //{
        //    string str = "";
        //    return str;
        //}
        //public static String GetActivityContent_UpdateSoliloquize(String content)
        //{
        //    string str = content;
        //    return str;
        //}
        public static String GetActivityContent_UpdateIWant_Title()
        {
            string str = "";
            return str;
        }
        public static String GetActivityContent_UpdateIWant(String content)
        {
            string str = content;
            return str;
        }
        public static String GetActivityContent_UpdatePhotoContent_Title()
        {
            string str = "@COUNT";
            return str;
        }
        public static String GetActivityContent_UpdatePhotoContent(String filename,String title,String content)
        {
            string str = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + filename.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg" + "|" + title + "|" + content;
            return str;
        }
        public static String GetActivityContent_UploadPhoto_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_UploadPhoto(String filename)
        {
            string content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + filename.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
            return content;
        }
        //public static String GetActivityContent_BeenMarked_Title()
        //{
        //    string content = "@COUNT";
        //    return content;
        //}
        //public static String GetActivityContent_BeenMarked(String comment)
        //{
        //    string content = comment;
        //    return content;
        //}
        //public static String GetActivityContent_BeenScored_Title()
        //{
        //    string content = "@COUNT";
        //    return content;
        //}
        //public static String GetActivityContent_BeenScored(String comment)
        //{
        //    string content = comment;
        //    return content;
        //}
        public static String GetActivityContent_SetLocation_Title()
        {
            string content = "";
            return content;
        }
        public static String GetActivityContent_SetICON_Title()
        {
            string content = "";
            return content;
        }
        public static String GetActivityContent_SetICON(Member mym)
        {
            string content = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
            return content;
        }
        //public static String GetActivityContent_AddRequest_Title()
        //{
        //    string content = "";
        //    return content;
        //}
        //public static String GetActivityContent_AddRequest(String title, String content)
        //{
        //    string str = title+"|"+content;
        //    return str;
        //}
        public static String GetActivityContent_PresentGlamourValue()
        {
            string content = "";
            return content;
        }
        public static String GetActivityContent_PresentGlamourValue(float value, String additional)
        {
            String str = Convert.ToString(value) + '|' + additional;
            return str;
        }
        public static String GetActivityContent_BeenFavored_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_BeenFavored()
        {
            string content = "";
            return content;
        }
        public static String GetActivityContent_WenWenQuestionBeenAnswered_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_WenWenQuestionBeenAnswered(String wenwenID,String wenwen,String comment)
        {
            string content = wenwenID + "|" + wenwen + "|" + comment;
            return content;
        }
        public static String GetActivityContent_PhotoBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_PhotoBeenCommented(String photoid,String filename, String comment)
        {
            string content = photoid + "|" + filename + "|" + comment;
            return content;
        }
        public static String GetActivityContent_JoinToMyInterestFansGroup_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_JoinToMyInterestFansGroup(String interestid, String IconPath, String title)
        {
            string content = interestid + "|" + IconPath + "|" + title;
            return content;
        }
        public static String GetActivityContent_BeenVisited_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_BeenVisited()
        {
            string content = "";
            return content;
        }
        public static String GetActivityContent_AddQuestToMyInterest_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_AddQuestToMyInterest(String interestid,String iconpath, String title,String comment)
        {
            string content = interestid + "|" + iconpath + "|" + title + "|" + comment;
            return content;
        }
        public static String GetActivityContent_AddInterest_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_AddInterest(String interestid, String title, String content, String iconpath)
        {
            string str = interestid + "|" + title + "|" + content + "|" + iconpath;
            return str;
        }
        public static String GetActivityContent_LikeTopic_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_LikeTopic(String wwid,String wwContent)
        {
            string str = wwid + "|" + wwContent;
            return str;
        }

        public static String GetActivityContent_ImageContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_ImageContentBeenCommented(
            String commentID, String comment, String imagesStr, String contentID, string strContent)
        {
            string content = commentID + "|" + comment + "|" + imagesStr + "|" + contentID + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_ImageContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_ImageContentBeenLiked(
            String commentID, String comment, String imagesStr, String contentID, string strContent)
        {
            string content = commentID + "|" + comment + "|" + imagesStr + "|" + contentID + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_ImageContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_ImageContentBeenLikeAndCommented(
            String commentID, String comment, String imagesStr, String contentID, string strContent)
        {
            string content = commentID + "|" + comment + "|" + imagesStr + "|" + contentID + "|" + strContent;
            return content;
        }

        public static String GetActivityContent_InterestContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_InterestContentBeenCommented(
            String commentID, String comment, String contentID, String interestId, String iconPath, String title, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + interestId + "|" + iconPath + "|" + title + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_InterestContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_InterestContentBeenLiked(
            String commentID, String comment, String contentID, String interestId, String iconPath, String title, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + interestId + "|" + iconPath + "|" + title + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_InterestContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_InterestContentBeenLikeAndCommented(
            String commentID, String comment, String contentID, String interestId, String iconPath, String title, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + interestId + "|" + iconPath + "|" + title + "|" + strContent;
            return content;
        }

        public static String GetActivityContent_CallForContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_CallForContentBeenCommented(
            String commentID, String comment, String contentID, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_CallForContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_CallForContentBeenLiked(
            String commentID, String comment, String contentID, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_CallForContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_CallForContentBeenLikeAndCommented(
            String commentID, String comment, String contentID, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + images + "|" + strContent;
            return content;
        }

        public static String GetActivityContent_IWantContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_IWantContentBeenCommented(
            String commentID, String comment, String contentID, String iWantContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + iWantContent + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_IWantContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_IWantContentBeenLiked(
            String commentID, String comment, String contentID, String iWantContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + iWantContent + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_IWantContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_IWantContentBeenLikeAndCommented(
            String commentID, String comment, String contentID, String iWantContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + iWantContent + "|" + images + "|" + strContent;
            return content;
        }

        public static String GetActivityContent_InterViewContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_InterViewContentBeenCommented(String commentID, String comment, String contentID)
        {
            string content = commentID + "|" + comment + "|" + contentID;
            return content;
        }
        public static String GetActivityContent_InterViewContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_InterViewContentBeenLiked(String commentID, String comment, String contentID)
        {
            string content = commentID + "|" + comment + "|" + contentID;
            return content;
        }

        public static String GetActivityContent_MoodContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MoodContentBeenCommented(
            String commentID, String comment, String mood, String status, String contentID, String moodContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + mood + "|" + status + "|" + moodContent;
            return content;
        }
        public static String GetActivityContent_MoodContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MoodContentBeenLiked(
            String commentID, String comment, String mood, String status, String contentID, String moodContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + mood + "|" + status + "|" + moodContent;
            return content;
        }
        public static String GetActivityContent_MoodContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MoodContentBeenLikeAndCommented(
            String commentID, String comment, String mood, String status, String contentID, String moodContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + mood + "|" + status + "|" + moodContent;
            return content;
        }

        public static String GetActivityContent_TalkAboutContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_TalkAboutContentBeenCommented(
            String commentID, String comment, String contentID, String talkAboutContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + talkAboutContent + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_TalkAboutContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_TalkAboutContentBeenLiked(
            String commentID, String comment, String contentID, String talkAboutContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + talkAboutContent + "|" + images + "|" + strContent;
            return content;
        }
        public static String GetActivityContent_TalkAboutContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_TalkAboutContentBeenLikeAndCommented(
            String commentID, String comment, String contentID, String talkAboutContent, string images, string strContent)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + talkAboutContent + "|" + images + "|" + strContent;
            return content;
        }

        public static String GetActivityContent_MemberContentBeenCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MemberContentBeenCommented(
            String commentID, String comment, String contentID, String type, String status, String addition)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + type + "|" + status + "|" + addition;
            return content;
        }
        public static String GetActivityContent_MemberContentBeenLiked_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MemberContentBeenLiked(
            String commentID, String comment, String contentID, String type, String status, String addition)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + type + "|" + status + "|" + addition;
            return content;
        }
        public static String GetActivityContent_MemberContentBeenLikeAndCommented_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_MemberContentBeenLikeAndCommented(
            String commentID, String comment, String contentID, String type, String status, String addition)
        {
            string content = commentID + "|" + comment + "|" + contentID + "|" + type + "|" + status + "|" + addition;
            return content;
        }

        public static String GetActivityContent_CommentBeenReplied_Title()
        {
            string content = "@COUNT";
            return content;
        }
        public static String GetActivityContent_CommentBeenReplied(
            String contentID, String reply, String comment, string contentType, string images)
        {
            string content = contentID + "|" + reply + "|" + comment + "|" + contentType + "|" + images;
            return content;
        }

        #endregion

        #region 增加动态
        public static CBB.ExceptionHelper.OperationResult AddActivity(String mid, ActivityType type, String Title,String Content,bool OnlySaveLastContent)
        {
            Member mym = MemberManager.MemberManager.GetMember(mid);
            return AddActivity(mym, type, Title,Content,OnlySaveLastContent);
        }
        public static CBB.ExceptionHelper.OperationResult AddActivity(Member mym, ActivityType type, String Title, String Content, bool OnlySaveLastContent)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ActivityHolder> mc = md.GetCollection<ActivityHolder>("Activity");
                ActivityHolder obj = mc.FindOne(Query.And(Query.EQ("MemberID", mym.ID), Query.EQ("Date", DateTime.Now.Date.ToString())));

                //如果当天没有动态，则新增
                if (obj == null)
                {
                    obj = new ActivityHolder();
                    obj.Date = DateTime.Now.Date.ToString();
                    obj.MemberID = mym.ID;
                    obj.MemberType = mym.MemberType;
                    obj.Activitys = new List<Activity>();
                    obj.Sex = obj.Sex;
                }
                obj.LastOperationTime = DateTime.Now;
                obj.MemberInfomation = mym.MemberInfomation;

                Activity at = null;
                //如果已有相同类别活动，则更新原有活动
                bool alreadyhas = false;
                foreach (Activity atobj in obj.Activitys)
                {
                    if (atobj.type == type)
                    {
                        at = atobj;
                        alreadyhas = true;
                    }
                }
                //否则新增活动
                if (at == null) at = new Activity();

                at.type = type;
                at.ActivityCount++;
                at.Title = Title.Replace("@COUNT", at.ActivityCount.ToString());
                if (at.Content == null) at.Content = new List<string>();
                if (OnlySaveLastContent)
                {
                    at.Content.Clear();
                }
               
                at.Content.Add(Content);
                at.CreatedTime = DateTime.Now;

                if (!alreadyhas)
                    obj.Activitys.Add(at);
                

                mc.Save(obj);
                //审核关键字
                //刘安注释，因为会引起图片路径的误判
                //new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(Content, ActivityHolder.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count -1));
                string[] arr = Content.Split('|');
                string outtext = String.Empty;
                Moooyo.BiZ.FilterWord.FilterWordController fwc =  new Moooyo.BiZ.FilterWord.FilterWordController();
                bool istrial = false;  //是否要审核
                bool isshift = false;  //是否要更改
                
                foreach (string i in arr) 
                {
                    if (i.IndexOf(".jpg") != -1)
                    {
                        outtext = outtext  +  i + "|";
                    }
                    else
                    {
                        #region
                        string temptext = i;
                        List<string> listdelete = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.delete);
                        string temptexdelete = temptext;                        
                        List<string> listshift = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.shift);
                        List<string> listtrial = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.trial);
                        if (null != listshift && listshift.Count > 0)
                        {
                            outtext = outtext + "内容审核中..." + "|";
                            if (!istrial)
                                istrial = true;
                            if (!isshift)
                                isshift = true;
                            
                        }
                        else if (null != listtrial && listtrial.Count > 0)
                        {
                            outtext = outtext + temptexdelete + "|";
                            if (!istrial)
                                istrial = true;
                        }
                        else if (null != listdelete && listdelete.Count > 0)
                        {
                            outtext = outtext + temptexdelete + "|";
                            if (!isshift) 
                            {
                                isshift = true;
                            }
                        }
                        else 
                        {
                            outtext = outtext + i + "|";
                        }
                        # endregion
                    }
                }
                if (istrial && isshift)
                {
                    fwc.AddFilterText(Content, ActivityHolder.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), true, outtext);
                }
                else 
                {
                    if (istrial && !isshift)
                    {
                        fwc.AddFilterText(Content, ActivityHolder.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), false, "");
                    }
                    else if (!istrial && isshift)
                    {
                        fwc.UpdateFilterText(ActivityHolder.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), outtext);
                    }
                    
                }

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult AddActivityRelatedToMe(String mid, String fromMemberID,ActivityType type, String Title, String Content, bool OnlySaveLastContent)
        {
            Member fromMember = MemberManager.MemberManager.GetMember(fromMemberID);
            return AddActivityRelatedToMe(mid, fromMember, type, Title, Content, OnlySaveLastContent);
        }
        public static CBB.ExceptionHelper.OperationResult AddActivityRelatedToMe(String mid, Member fromMember, ActivityType type, String Title, String Content, bool OnlySaveLastContent)
        {
            //如果是自己对自己的操作，则直接返回，不保存关联动态。
            if (mid == fromMember.ID) return new CBB.ExceptionHelper.OperationResult(true);

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ActivityHolderRelatedToMe> mc = md.GetCollection<ActivityHolderRelatedToMe>(ActivityHolderRelatedToMe.GetCollectionName());
                ActivityHolderRelatedToMe obj = mc.FindOne(Query.And(Query.EQ("MemberID", mid), Query.EQ("FromMemberID", fromMember.ID), Query.EQ("Date", DateTime.Now.Date.ToString())));

                //如果当天没有动态，则新增
                if (obj == null)
                {
                    obj = new ActivityHolderRelatedToMe();
                    obj.Date = DateTime.Now.Date.ToString();
                    obj.MemberID = mid;
                    obj.FromMemberID = fromMember.ID;
                    obj.Activitys = new List<Activity>();
                }
                obj.LastOperationTime = DateTime.Now;
                obj.FromMemberInfomation = fromMember.MemberInfomation;

                Activity at = null;
                //如果已有相同类别活动，则更新原有活动
                bool alreadyhas = false;
                foreach (Activity atobj in obj.Activitys)
                {
                    if (atobj.type == type)
                    {
                        at = atobj;
                        alreadyhas = true;
                    }
                }
                //否则新增活动
                if (at == null) at = new Activity();

                at.type = type;
                at.ActivityCount++;
                at.Title = Title.Replace("@COUNT", at.ActivityCount.ToString());
                if (at.Content == null) at.Content = new List<string>();
                if (OnlySaveLastContent)
                {
                    at.Content.Clear();
                }

                at.Content.Add(Content);
                at.CreatedTime = DateTime.Now;

                if (!alreadyhas)
                    obj.Activitys.Add(at);

                mc.Save(obj);
                //审核关键字
                string[] arr = Content.Split('|');
                string outtext = String.Empty;
                Moooyo.BiZ.FilterWord.FilterWordController fwc = new Moooyo.BiZ.FilterWord.FilterWordController();
                bool istrial = false;  //是否要审核
                bool isshift = false;  //是否要更改

                foreach (string i in arr)
                {
                    ObjectId objid;
                    if (i.IndexOf(".jpg") != -1 || ObjectId.TryParse(i,out objid))
                    {
                        outtext = outtext + i + "|";
                    }
                    else
                    {
                        #region
                        string temptext = i;
                        List<string> listdelete = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.delete);
                        string temptexdelete = temptext;
                        List<string> listshift = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.shift);
                        List<string> listtrial = fwc.FilterText(ref temptext, CBB.CheckHelper.FilterWord.word_type.trial);
                        if (null != listshift && listshift.Count > 0)
                        {
                            outtext = outtext + "内容审核中..." + "|";
                            if (!istrial)
                                istrial = true;
                            if (!isshift)
                                isshift = true;

                        }
                        else if (null != listtrial && listtrial.Count > 0)
                        {
                            outtext = outtext + temptexdelete + "|";
                            if (!istrial)
                                istrial = true;
                        }
                        else if (null != listdelete && listdelete.Count > 0)
                        {
                            outtext = outtext + temptexdelete + "|";
                            if (!isshift)
                            {
                                isshift = true;
                            }
                        }
                        else
                        {
                            outtext = outtext + i + "|";
                        }
                        # endregion
                    }
                }
                if (istrial && isshift)
                {
                    fwc.AddFilterText(Content, ActivityHolderRelatedToMe.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), true, outtext);
                }
                else
                {
                    if (istrial && !isshift)
                    {
                        fwc.AddFilterText(Content, ActivityHolderRelatedToMe.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), false, "");
                    }
                    else if (!istrial && isshift)
                    {
                        fwc.UpdateFilterText(ActivityHolderRelatedToMe.GetCollectionName(), obj.ID, "Activitys." + (obj.Activitys.Count - 1) + ".Content." + (at.Content.Count - 1), outtext);
                    }

                }

                //增加计数器
                BiZ.MemberManager.MemberManager.ModifyUnReadActivitysAboutMeCount(mid, MemberManager.StatusModifyType.Add);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        public static IList<ActivityHolder> GetFavorMemberActivitys(String mid, int pagesize, int pageno)
        {
            IList<Relation.Favorer> favors = Relation.RelationProvider.GetFavorers(mid, 0, 0);
            List<String> fmemberids = new List<string>();
            foreach (Relation.Favorer f in favors)
            {
                fmemberids.Add(f.ToMember);
            }
            //加上自己
            fmemberids.Add(mid);

            MongoCursor<ActivityHolder> mc = MongoDBHelper.GetCursor<ActivityHolder>(
                    "Activity",
                    Query.In("MemberID", new BsonArray(fmemberids.ToArray())),
                    new SortByDocument("LastOperationTime", -1),
                    pageno,
                    pagesize);

            List<ActivityHolder> objs = new List<ActivityHolder>();
            objs.AddRange(mc);

            return objs;
        }
        public static int GetFavorMemberActivitysCount(String mid)
        {
            IList<Relation.Favorer> favors = Relation.RelationProvider.GetFavorers(mid, 0, 0);
            List<String> fmemberids = new List<string>();
            foreach (Relation.Favorer f in favors)
            {
                fmemberids.Add(f.ToMember);
            }

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<ActivityHolder> mc = md.GetCollection<ActivityHolder>("Activity");
            return (int)mc.Count(Query.In("MemberID", new BsonArray(fmemberids.ToArray())));
        }
        public static IList<Activity> GetFavorMemberActivitys(String aHId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ActivityHolder> mgColl = md.GetCollection<ActivityHolder>("Activity");
                IMongoSortBy sortBy = new SortByDocument("CreatedTime", 1);
                MongoCursor<ActivityHolder> mgCurs = mgColl.Find(Query.EQ("_id", ObjectId.Parse(aHId))).SetSortOrder(sortBy);

                List<ActivityHolder> aHObjs = new List<ActivityHolder>();
                aHObjs.AddRange(mgCurs);
                List<Activity> aObjs = new List<Activity>();
                foreach (ActivityHolder aHObj in aHObjs) aObjs.AddRange(aHObj.Activitys);

                return aObjs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static IList<ActivityHolderRelatedToMe> GetMemberRelationActivitys(String mid, int pagesize, int pageno)
        {
            MongoCursor<ActivityHolderRelatedToMe> mc = MongoDBHelper.GetCursor<ActivityHolderRelatedToMe>(
                    ActivityHolderRelatedToMe.GetCollectionName(),
                    Query.EQ("MemberID", mid),
                    new SortByDocument("LastOperationTime", -1),
                    pageno,
                    pagesize);

            List<ActivityHolderRelatedToMe> objs = new List<ActivityHolderRelatedToMe>();
            objs.AddRange(mc);

            return objs;
        }
        public static int GetMemberRelationActivitysCount(String mid)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<ActivityHolderRelatedToMe> mc = md.GetCollection<ActivityHolderRelatedToMe>(ActivityHolderRelatedToMe.GetCollectionName());
            return (int)mc.Count(Query.EQ("MemberID", mid));
        }
        public static IList<Activity> GetMemberRelationActivitys(String aHId)
        {
            try
            {
                ActivityHolderRelatedToMe aHObj = GetActivityRelatedToMe(aHId);

                List<Activity> aObjs = new List<Activity>();
                aObjs.AddRange(aHObj.Activitys);

                return aObjs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static ActivityHolderRelatedToMe GetActivityRelatedToMe(String aId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ActivityHolderRelatedToMe> mgColl = md.GetCollection<ActivityHolderRelatedToMe>(ActivityHolderRelatedToMe.GetCollectionName());
                ActivityHolderRelatedToMe aHObj = mgColl.FindOne(Query.EQ("_id", ObjectId.Parse(aId)));

                return aHObj;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 聊天
        public static CBB.ExceptionHelper.OperationResult MsgToMember(String fromMember, String toMember, String comment, ActivityType type)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result;
                //自己不能给自己发私信
                if ((fromMember == toMember) && (type == BiZ.Member.Activity.ActivityType.Talk))
                {
                    return new CBB.ExceptionHelper.OperationResult(false, "自己不能给自己发私信"); 
                }
                //如果对方已经屏蔽我，则更改类型
                if (BiZ.Member.Relation.RelationProvider.IsMemberDisableMe(toMember, fromMember))
                {
                    if (type == BiZ.Member.Activity.ActivityType.Talk || type == BiZ.Member.Activity.ActivityType.AskToDate)
                    //if (type == BiZ.Member.Activity.ActivityType.Talk || type == BiZ.Member.Activity.ActivityType.SaiHi || type == BiZ.Member.Activity.ActivityType.AskToDate)
                        type = BiZ.Member.Activity.ActivityType.DisabledTalk;
                    else
                        type = BiZ.Member.Activity.ActivityType.DisabledMsg;
                }

                result = BiZ.Member.Link.MsgProvider.MsgToMember(fromMember, toMember, comment, type);

                if (result.ok)
                {
                    if (type != ActivityType.DisabledTalk & type != ActivityType.DisabledMsg)
                    {
                        //增加未读信息数量
                        result = BiZ.MemberManager.MemberManager.ModifyUnReadMsgCount(toMember, MemberManager.StatusModifyType.Add, 1);
                    }
                    //增加总发出信息条数
                    result = BiZ.MemberManager.MemberManager.ModifyTotalMsgCount(fromMember, MemberManager.StatusModifyType.Add);
                    
                    //如果是用户聊天
                    if (type == ActivityType.Talk)
                    {
                        //如果用户设置中，设置了自动关注，则增加关注
                        Member mym = MemberManager.MemberManager.GetMember(fromMember);
                        if (mym.Settings != null)
                            if (mym.Settings.AutoAddOutCallingToMyFavorList)
                            {
                                result = FavorMember(fromMember, toMember);
                            }
                    }

                    bool isFristConnect = false;
                    result = Relation.RelationProvider.AddLastMsger(fromMember, toMember, comment, type, out isFristConnect);

                    //用户对他人第一次主动发起时记录用户对该用户的关注度
                    if (isFristConnect)
                    {
                        Recommendation.MemberFavorTrainingData membFavoTrai = new Recommendation.MemberFavorTrainingData(fromMember, toMember, Recommendation.MemberFavorTrainingDataType.Call);
                    }

                    //增加用户动态到后台
                    BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                        fromMember,
                        toMember,
                        BiZ.Sys.MemberActivity.MemberActivityType.PrivateLetter,
                        "/Content/TaContent/" + fromMember);
                }
                return result;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 系统消息
        public static CBB.ExceptionHelper.OperationResult SystemMsgToMember(String toMember, String comment)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result = BiZ.Sys.SystemMsg.SystemMsgProvider.MsgToMember(toMember, comment);
                if (result.ok)
                {
                    //增加未读信息数量
                    result = BiZ.MemberManager.MemberManager.ModifyUnReadSystemMsgCount(toMember, MemberManager.StatusModifyType.Add, 1);
                }
                return result;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 浏览用户
        public static CBB.ExceptionHelper.OperationResult VisitMember(String fromMember, String toMember)
        {
            try
            {
                //CBB.ExceptionHelper.OperationResult result = Relation.RelationProvider.AddVistor(fromMember, toMember);

                //增加用户关联动态
                CBB.ExceptionHelper.OperationResult result =
                    BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                        toMember,
                        fromMember,
                        ActivityType.BeenVisit,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_BeenVisited_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_BeenVisited(),
                        false);

                if (result.ok)
                {
                    result = BiZ.MemberManager.MemberManager.ModifyBeenViewedTimes(toMember, MemberManager.StatusModifyType.Add);
                    result = BiZ.MemberManager.MemberManager.ModifyUnReadBeenViewedTimes(toMember, MemberManager.StatusModifyType.Add, 1);
                }
                return result;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 喜欢用户
        public static CBB.ExceptionHelper.OperationResult FavorMember(String fromMember, String toMember)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result = Relation.RelationProvider.AddFavorer(fromMember, toMember);
                if (result.ok)
                {
                    BiZ.MemberManager.MemberManager.ModifyFavorMemberCount(fromMember, MemberManager.StatusModifyType.Add);
                    BiZ.MemberManager.MemberManager.ModifyMemberFavoredMeCount(toMember, MemberManager.StatusModifyType.Add);
                    BiZ.MemberManager.MemberManager.ModifyUnReadBeenFavorCountCount(toMember, MemberManager.StatusModifyType.Add);
                    //关注他人时记录用户对该用户的关注度
                    Recommendation.MemberFavorTrainingData membFavoTrai = new Recommendation.MemberFavorTrainingData(fromMember, toMember, Recommendation.MemberFavorTrainingDataType.AddToFansGroup);

                    //增加用户关联关注
                    AddActivityRelatedToMe(
                        toMember, 
                        fromMember, 
                        ActivityType.BeenFavored,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_BeenFavored_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_BeenFavored(),
                        false);
                }

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult UpdateFavorComment(String fromMember, String toMember, String comment)
        {
            try
            {
                Relation.RelationProvider.UpdateFavorer(fromMember, toMember, comment);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 删除喜欢用户
        public static CBB.ExceptionHelper.OperationResult DeleteFavorMember(String fromMember, String toMember)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result = Relation.RelationProvider.DeleteFavorer(fromMember, toMember);
                if (result.ok)
                {
                    BiZ.MemberManager.MemberManager.ModifyFavorMemberCount(fromMember, MemberManager.StatusModifyType.Decrease);
                    BiZ.MemberManager.MemberManager.ModifyMemberFavoredMeCount(toMember, MemberManager.StatusModifyType.Decrease);
                }
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 给用户送礼物
        public static CBB.ExceptionHelper.OperationResult AddGiftor(String fromMember, String toMember, String giftID, String giftName, String comment)
        {
            try
            {
                Relation.RelationProvider.AddGiftor(fromMember, toMember, giftID,giftName,comment);
                //增加用户魅力指数
                Gift.GiftDefs g = Gift.GiftProvider.GetGiftDefs(giftID);
                if (g.Type == Gift.ValueType.Hot)
                    BiZ.MemberManager.MemberManager.ModifyGlamourCount(toMember, MemberManager.StatusModifyType.Add,g.Value);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 对用户静默
        public static CBB.ExceptionHelper.OperationResult SilentToMember(String fromMember, String toMember)
        {
            try
            {
                Relation.RelationProvider.AddSilentor(fromMember, toMember);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 将用户屏蔽
        public static CBB.ExceptionHelper.OperationResult DisableMember(String fromMember, String toMember)
        {
            try
            {
                Relation.RelationProvider.AddDisabler(fromMember, toMember);
                DeleteFavorMember(fromMember, toMember);
                Relation.RelationProvider.DeleteLastMsger(fromMember, toMember);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult DeleteDisableMember(String fromMember, String toMember)
        {
            try
            {
                Relation.RelationProvider.DeleteDisabler(fromMember, toMember);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 邀请用户注册成功
        public static CBB.ExceptionHelper.OperationResult RegInviterMember(String fromMember, String toMember)
        {
            try
            {
                Member mym = MemberManager.MemberManager.GetMember(fromMember);
                if (mym.Status.PhotoCount<1) return new CBB.ExceptionHelper.OperationResult(false,"用户没有上传照片，不能升级为正式用户");
                if (mym.MemberPhoto.IconID == "") return new CBB.ExceptionHelper.OperationResult(false,"用户没有设置头像，不能升级为正式用户");

                CBB.ExceptionHelper.OperationResult result = Relation.RelationProvider.AddRegInviter(fromMember, toMember);
                if (result.ok)
                {
                    if ((int)mym.MemberType < 1)
                        result = MemberManager.MemberManager.SetMemberType(fromMember, MemberType.Level1);
                    else
                        result = new CBB.ExceptionHelper.OperationResult(false, "用户已是正式会员");
                }

                return result;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);

            }
        }
        #endregion

        #region 给用户添加魅力值
        public static CBB.ExceptionHelper.OperationResult AddGlamourValue(
            String fromMember, 
            String toMember,
            GlamourCounts.GlamourCountOperate.GlamourCountType glamType, 
            GlamourCounts.GlamourCountOperate.ModifyGlamourValue modiGlamVal,
            String additional)
        {
            //添加魅力值
            CBB.ExceptionHelper.OperationResult result = Relation.RelationProvider.AddGlamourValue(fromMember, toMember, glamType, modiGlamVal);
            if (!result.ok) { return new CBB.ExceptionHelper.OperationResult(false, result.err); }
            //增加用户魅力值计数
            result = BiZ.MemberManager.MemberManager.ModifyGlamourCount(toMember, MemberManager.StatusModifyType.Add, 1);

            //增加用户关联动态
            float glamVal = (float)modiGlamVal;
            result = BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                toMember,
                fromMember,
                BiZ.Member.Activity.ActivityType.PresentGlamourValue,
                BiZ.Member.Activity.ActivityController.GetActivityContent_PresentGlamourValue(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_PresentGlamourValue(glamVal, additional),
                false
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion

        #region 评论用户的照片
        public static CBB.ExceptionHelper.OperationResult AddCommentToMemberPhoto(String photoID, String mid, String NickName, String iconpath, String Content)
        {
            CBB.ExceptionHelper.OperationResult result = Photo.PhotoManager.AddComment(photoID, mid, NickName, iconpath, Content);
            BiZ.Photo.Photo photo = BiZ.Photo.PhotoManager.GetPhoto(photoID);
            
            if (result.ok && photo!=null)
            {
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(photo.MemberID);
                //增加用户关联动态
                BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                    photo.MemberID,
                    mid,
                    ActivityType.PhotoBeenCommented,
                    BiZ.Member.Activity.ActivityController.GetActivityContent_PhotoBeenCommented_Title(),
                    BiZ.Member.Activity.ActivityController.GetActivityContent_PhotoBeenCommented(photo.ID,photo.FileName, Content),
                    false);
            }

            return result;
        }
        #endregion

        #region 回答我的问问
        public static WenWen.WenWenAnswer AddWenWenAnswer(String memberid, String wenwenID, bool upordown, String content)
        {
            WenWen.WenWenAnswer wenwenAnswer = WenWen.WenWenProvider.AddWenWenAnswer(memberid, wenwenID, upordown, content);
            CBB.ExceptionHelper.OperationResult result = new CBB.ExceptionHelper.OperationResult(false);
            if (wenwenAnswer != null)
            {
                WenWen.WenWen wenwen = WenWen.WenWenProvider.GetWenWen(wenwenID);
                if (wenwen != null)
                {
                    //增加用户关联动态
                    result = 
                    BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                    wenwen.Creater.MemberID,
                    memberid,
                    ActivityType.AddAskAnswer,
                    BiZ.Member.Activity.ActivityController.GetActivityContent_WenWenQuestionBeenAnswered_Title(),
                    BiZ.Member.Activity.ActivityController.GetActivityContent_WenWenQuestionBeenAnswered(wenwenID,wenwen.Content, content),
                    false);
                }
            }
            return wenwenAnswer;
        }
        #endregion

        #region 加入用户创建的兴趣粉丝团
        public static CBB.ExceptionHelper.OperationResult AddToInterestFansGroup(String interestid, String memberid)
        {
            CBB.ExceptionHelper.OperationResult result = InterestCenter.InterestFactory.AddInterestFans(interestid, memberid);
            InterestCenter.Interest interest = InterestCenter.InterestFactory.GetInterest(interestid);
            if (interest != null)
            {
                //增加用户关联动态
                result =
                BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                interest.Creater.MemberID,
                memberid,
                ActivityType.JoinToMyInterestFansGroup,
                BiZ.Member.Activity.ActivityController.GetActivityContent_JoinToMyInterestFansGroup_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_JoinToMyInterestFansGroup(interestid, interest.ICONPath, interest.Title),
                false);
            }
            return result;
        }
        #endregion

        #region 在用户创建的兴趣下发布问问
        public static WenWen.WenWen AddQuestToMyInterest(String memberid, String interestid, String title, String content,String contentimage)
        {
            WenWen.WenWen wenwen = WenWen.WenWenProvider.AddWenWen(memberid, interestid, title, content, contentimage);
            InterestCenter.Interest interest = InterestCenter.InterestFactory.GetInterest(interestid);
            if (interest != null)
            {
                //增加用户关联动态
                BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                interest.Creater.MemberID,
                memberid,
                ActivityType.AddQuestToMyInterest,
                BiZ.Member.Activity.ActivityController.GetActivityContent_AddQuestToMyInterest_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_AddQuestToMyInterest(interestid,interest.ICONPath,title,content),
                false);
            }
            return wenwen;
        }
        #endregion
    }
}
