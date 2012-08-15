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
    //使MongoDB查询表时，可以忽略额外的元素 
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class InterestContent : PublicContent
    {
        private String content;//内容
        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private InterestCenter.Interest interest;//兴趣对象
        public InterestCenter.Interest Interest
        {
            get { return interest; }
            set { interest = value; }
        }
        private String type;//类型
        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        public InterestContent() { }
        public InterestContent(String MemberID, ContentPermissions ContentPermissions, List<String> InterestID,String Content, InterestCenter.Interest Interest, String Type)
        {
            PublicContent obj = new PublicContent(MemberID, ContentPermissions, InterestID, ContentType.Interest);
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
            this.Content = Content;
            this.Interest = Interest;
            this.Type = Type;
        }
        public InterestContent(String contentID)
        {
            try
            {
                InterestContent obj = new InterestContent();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestContent> mc = md.GetCollection<InterestContent>(MoodContent.GetCollectionName());
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
                this.Content = obj.Content;
                this.Interest = obj.Interest;
                this.Type = obj.Type;
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
        /// 心情内容的添加或更新
        /// </summary>
        /// <param name="obj">兴趣操作内容对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public InterestContent Save(InterestContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestContent> mc = md.GetCollection<InterestContent>(PublicContent.GetCollectionName());
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
