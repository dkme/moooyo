///
/// 功能描述：内容的公共类数据结构
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Content
{
    /// <summary>
    /// 内容的公共类，继承ICanBeenComment接口，实现ICanBeenComment接口中的方法
    /// </summary>
    public class PublicContent : Comment.ICanBeenComment
    {
        public ObjectId _id;//ID
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        private String memberID;//创建者编号
        public String MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        private Moooyo.BiZ.Creater.Creater creater;//创建者对象
        public Moooyo.BiZ.Creater.Creater Creater
        {
            get { return creater; }
            set { creater = value; }
        }
        private ContentPermissions contentPermissions;//内容访问权限
        public ContentPermissions ContentPermissions
        {
            get { return contentPermissions; }
            set { contentPermissions = value; }
        }
        private IList<String> myFriends;//我的好友
        public IList<String> MyFriends
        {
            get { return myFriends; }
            set { myFriends = value; }
        }
        private String city;//创建者城市
        public String City
        {
            get { return city; }
            set { city = value; }
        }
        private SexType sex;//创建者性别
        public SexType Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        private List<String> interestID;//内容所对应的兴趣集合
        public List<String> InterestID
        {
            get { return interestID; }
            set { interestID = value; }
        }
        private ContentType contentType;//内容类型
        public ContentType ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }
        private DateTime createdTime;//创建时间
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        private DateTime updateTime;//更新时间
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
        private long answerCount;//内容回复的总数量
        public long AnswerCount
        {
            get { return answerCount; }
            set { answerCount = value; }
        }
        private List<Comment.Comment> answerList;//内容回复的显示集合
        public List<Comment.Comment> AnswerList
        {
            get { return answerList; }
            set { answerList = value; }
        }
        private long likeCount;//内容被喜欢的总数量
        public long LikeCount
        {
            get { return likeCount; }
            set { likeCount = value; }
        }
        private List<BiZ.Like.LikeMember> likeList;//内容被喜欢的显示集合
        public List<BiZ.Like.LikeMember> LikeList
        {
            get { return likeList; }
            set { likeList = value; }
        }
        private Comm.DeletedFlag deleteFlag;//内容的删除状态
        public Comm.DeletedFlag DeleteFlag
        {
            get { return deleteFlag; }
            set { deleteFlag = value; }
        }

        public PublicContent() { }
        public PublicContent(String MemberID, ContentPermissions ContentPermissions, List<String> InterestID, ContentType ContentType)
        {
            this.MemberID = MemberID;
            this.Creater = new Creater.Creater(MemberID);
            this.ContentPermissions = ContentPermissions;
            this.MyFriends = ContentProvider.getMyFriends(MemberID);
            this.City = this.Creater.City.Split('|')[this.Creater.City.Split('|').Length - 1];
            this.Sex = this.Creater.Sex == 1 ? BiZ.Content.SexType.Boy : BiZ.Content.SexType.Girl;
            this.InterestID = InterestID;
            this.ContentType = ContentType;
            this.CreatedTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.AnswerCount = 0;
            this.AnswerList = new List<Comment.Comment>();
            this.LikeCount = 0;
            this.LikeList = new List<BiZ.Like.LikeMember>();
            this.DeleteFlag = Comm.DeletedFlag.No;
        }

        public static String GetCollectionName()
        {
            return "Content";
        }
        /// <summary>
        /// 内容删除
        /// </summary>
        /// <param name="ID">内容编号</param>
        /// <returns>删除是否成功</returns>
        public static Boolean Remove(String contentID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<CallForContent> mc = md.GetCollection<CallForContent>(PublicContent.GetCollectionName());
                mc.Update(Query.EQ("_id", ObjectId.Parse(contentID)), Update.Set("DeleteFlag", Comm.DeletedFlag.No));
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 更新内容对应用户的好友集合
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <returns></returns>
        public static Boolean UpdateMyFriends(String memberID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<CallForContent> mc = md.GetCollection<CallForContent>(PublicContent.GetCollectionName());
                IList<String> myfriends = ContentProvider.getMyFriends(memberID);
                mc.Update(Query.EQ("MemberID", ObjectId.Parse(memberID)), Update.Set("MyFriends", BsonArray.Create(myfriends)));
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 更新喜欢的数量
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <returns></returns>
        public static Boolean UpdateLikeCount(String contentID)
        {
            try
            {
                PublicContent obj = new PublicContent();
                obj = obj.getPublicContent(contentID);
                obj.LikeCount = Like.LikeDataFactory.GetLike<Like.LikeData>(null, contentID, Like.LikeType.Content, Like.LikeData.GetCollectionName()).Count;
                obj.UpdateTime = DateTime.Now;
                obj.savePublicContent(obj);
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /*
        /// <summary>
        /// 更新喜欢的集合
        /// </summary>
        /// <param name="comment">回复的内容</param>
        /// <returns>操作是否成功</returns>
        public static Boolean UpdateLikeList(Comment.Comment comment)
        {
            try
            {
                PublicContent obj = new PublicContent();
                obj = obj.getPublicContent(comment.CommentToID);
                obj.LikeList.Insert(0, comment);
                obj.savePublicContent(obj);
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }*/
        /// <summary>
        /// 更新访谈内容的访谈集合
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <param name="interviewID">访谈编号</param>
        /// <returns></returns>
        public static Boolean UpdateInterViewList(String contentID, String interviewID)
        {
            try
            {
                PublicContent obj = new PublicContent();
                InterViewContent interviewcontent = (InterViewContent)obj.getPublicContent(contentID);
                InterView.InterView interview = InterView.InterViewProvider.GetInterView(interviewID);
                for (int i = 0; i < interviewcontent.InterviewList.Count; i++)
                {
                    if (interviewcontent.InterviewList[i].ID == interviewID)
                    {
                        interviewcontent.InterviewList.RemoveAt(i);
                        interviewcontent.InterviewList.Insert(i, interview);
                        break;
                    }
                }
                obj.savePublicContent((PublicContent)interviewcontent);
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 更新内容的回复显示集合，工厂方法
        /// </summary>
        /// <param name="ID">回复对象</param>
        /// <returns>操作是否成功</returns>
        public Boolean UpdateCommentList(Comment.Comment comment)
        {
            try
            {
                PublicContent obj = new PublicContent();
                obj = obj.getPublicContent(comment.CommentToID);
                if (obj.AnswerList.Count >= 6)
                {
                    obj.AnswerList.Insert(0, comment);
                    obj.AnswerList.RemoveAt(obj.AnswerList.Count - 1);
                }
                else
                {
                    obj.AnswerList.Insert(0, comment);
                }
                obj.AnswerCount = Comment.CommentProvider.findForContentCount(comment.CommentToID, Comm.DeletedFlag.No);
                obj.UpdateTime = DateTime.Now;
                obj.savePublicContent(obj);
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }

        public PublicContent getPublicContent(String id)
        {
            PublicContent obj = new PublicContent();
            switch (ContentProvider.GetContentType(id))
            {
                case ContentType.CallFor:
                    obj = (PublicContent)(new CallForContent(id));
                    break;
                case ContentType.Image:
                    obj = (PublicContent)(new ImageContent(id));
                    break;
                case ContentType.InterView:
                    obj = (PublicContent)(new InterViewContent(id));
                    break;
                case ContentType.IWant:
                    obj = (PublicContent)(new IWantContent(id));
                    break;
                case ContentType.Mood:
                    obj = (PublicContent)(new MoodContent(id));
                    break;
                case ContentType.SuiSuiNian:
                    obj = (PublicContent)(new SuiSuiNianContent(id));
                    break;
                case ContentType.Interest:
                    obj = (PublicContent)(new InterestContent(id));
                    break;
                case ContentType.Member:
                    obj = (PublicContent)(new MemberContent(id));
                    break;
            }
            return obj;
        }
        public PublicContent savePublicContent(PublicContent obj)
        {
            switch (obj.ContentType)
            {
                case ContentType.CallFor:
                    CallForContent callfor = (CallForContent)obj;
                    callfor.Save(callfor);
                    break;
                case ContentType.Image:
                    ImageContent image = (ImageContent)obj;
                    image.Save(image);
                    break;
                case ContentType.InterView:
                    InterViewContent interview = (InterViewContent)obj;
                    interview.Save(interview);
                    break;
                case ContentType.IWant:
                    IWantContent iwant = (IWantContent)obj;
                    iwant.Save(iwant);
                    break;
                case ContentType.Mood:
                    MoodContent mood = (MoodContent)obj;
                    mood.Save(mood);
                    break;
                case ContentType.SuiSuiNian:
                    SuiSuiNianContent suisuinian = (SuiSuiNianContent)obj;
                    suisuinian.Save(suisuinian);
                    break;
                case ContentType.Interest:
                    InterestContent interest = (InterestContent)obj;
                    interest.Save(interest);
                    break;
                case ContentType.Member:
                    MemberContent member = (MemberContent)obj;
                    member.Save(member);
                    break;
            }
            return obj;
        }
    }
}
