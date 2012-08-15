///
/// 功能描述：我想内容子类的数据结构
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
    /// 我想内容子类，继承内容公共表
    /// </summary>
    //使MongoDB查询表时，可以忽略额外的元素 
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class IWantContent : PublicContent
    {
        private String content;//我想的内容
        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private List<Image> imageList;//图片集合
        public List<Image> ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }
        private ImageLayoutType layOutType;//图片布局类型
        public ImageLayoutType LayOutType
        {
            get { return layOutType; }
            set { layOutType = value; }
        }
        private String type;//计划类型
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public IWantContent() { }
        public IWantContent(String MemberID, ContentPermissions ContentPermissions, List<String> InterestID, String Content, List<Image> ImageList, ImageLayoutType LayOutType, String Type)
        {
            PublicContent obj = new PublicContent(MemberID, ContentPermissions, InterestID, ContentType.IWant);
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
            this.ImageList = ImageList;
            this.LayOutType = LayOutType;
            this.Type = Type;
        }
        public IWantContent(String contentID)
        {
            try
            {
                IWantContent obj = new IWantContent();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<IWantContent> mc = md.GetCollection<IWantContent>(IWantContent.GetCollectionName());
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
                this.ImageList = obj.ImageList;
                this.LayOutType = obj.LayOutType;
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
        /// 我想内容的添加或更新
        /// </summary>
        /// <param name="obj">我想内容对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public IWantContent Save(IWantContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<IWantContent> mc = md.GetCollection<IWantContent>(PublicContent.GetCollectionName());
                mc.Save(obj);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, PublicContent.GetCollectionName(), obj.ID, "Content", obj.MemberID);
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
