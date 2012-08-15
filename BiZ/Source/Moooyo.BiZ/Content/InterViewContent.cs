///
/// 功能描述：访谈内容子类的数据结构
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
    /// 访谈内容子类，继承内容公共表
    /// </summary>
    public class InterViewContent : PublicContent
    {
        private IList<InterView.InterView> interviewList;//访谈对象集合
        public IList<InterView.InterView> InterviewList
        {
            get { return interviewList; }
            set { interviewList = value; }
        }

        public InterViewContent() { }
        public InterViewContent(String MemberID, ContentPermissions ContentPermissions, List<String> InterestID, IList<InterView.InterView> InterviewList)
        {
            PublicContent obj = new PublicContent(MemberID, ContentPermissions, InterestID, ContentType.InterView);
            this.MemberID = obj.MemberID;
            this.Creater = obj.Creater;
            this.ContentPermissions = obj.ContentPermissions;
            this.MyFriends = obj.MyFriends;
            this.City = obj.City;
            this.Sex = obj.Sex;
            this.InterestID = obj.InterestID;
            this.ContentType = obj.ContentType;
            this.CreatedTime = obj.CreatedTime;
            this.UpdateTime = obj.UpdateTime;
            this.AnswerCount = obj.AnswerCount;
            this.AnswerList = obj.AnswerList;
            this.LikeCount = obj.LikeCount;
            this.LikeList = obj.LikeList;
            this.DeleteFlag = obj.DeleteFlag;
            this.InterviewList = InterviewList;
        }
        public InterViewContent(String contentID)
        {
            try
            {
                InterViewContent obj = new InterViewContent();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterViewContent> mc = md.GetCollection<InterViewContent>(InterViewContent.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(contentID)));
                this._id = ObjectId.Parse(obj.ID);
                this.MemberID = obj.MemberID;
                this.Creater = obj.Creater;
                this.ContentPermissions = obj.ContentPermissions;
                this.MyFriends = obj.MyFriends;
                this.City = obj.City;
                this.Sex = obj.Sex;
                this.InterestID = obj.InterestID;
                this.ContentType = obj.ContentType;
                this.CreatedTime = obj.CreatedTime;
                this.UpdateTime = obj.UpdateTime;
                this.AnswerCount = obj.AnswerCount;
                this.AnswerList = obj.AnswerList;
                this.LikeCount = obj.LikeCount;
                this.LikeList = obj.LikeList;
                this.DeleteFlag = obj.DeleteFlag;
                this.InterviewList = obj.InterviewList;
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
        /// 访谈内容的添加或更新
        /// </summary>
        /// <param name="obj">访谈内容对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public InterViewContent Save(InterViewContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterViewContent> mc = md.GetCollection<InterViewContent>(PublicContent.GetCollectionName());
                mc.Save(obj);
                return obj;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
    }
}
