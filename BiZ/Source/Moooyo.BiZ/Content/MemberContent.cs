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
    public class MemberContent : PublicContent
    {
        private Double lat;//经度
        public Double Lat
        {
            get { return lat; }
            set { lat = value; }
        }
        private Double lng;//纬度
        public Double Lng
        {
            get { return lng; }
            set { lng = value; }
        }
        private String type;//类型
        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string MemberAvatar
        {
            get { return memberAvatar; }
            set { memberAvatar = value; }
        }
        public string memberAvatar;
        public MemberContent() { }
        public MemberContent(String MemberID, ContentPermissions ContentPermissions, List<String> InterestID, Double Lat, Double Lng, String Type)
        {
            PublicContent obj = new PublicContent(MemberID, ContentPermissions, InterestID, ContentType.Member);
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
            this.Lat = Lat;
            this.Lng = Lng;
            this.Type = Type;
        }
        public MemberContent(String contentID)
        {
            try
            {
                MemberContent obj = new MemberContent();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>(MoodContent.GetCollectionName());
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
                this.Lat = obj.Lat;
                this.Lng = obj.Lng;
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
        /// <param name="obj">用户操作内容对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public MemberContent Save(MemberContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>(PublicContent.GetCollectionName());
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
