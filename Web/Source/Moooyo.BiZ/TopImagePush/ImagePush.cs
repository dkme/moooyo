///
/// 功能描述：顶部图片推送表数据结构
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

namespace Moooyo.BiZ.TopImagePush
{
    /// <summary>
    /// 顶部图片的实体类
    /// </summary>
    public class ImagePush
    {
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;//ID
        private String contentID;//内容编号
        public String ContentID
        {
            get { return contentID; }
            set { contentID = value; }
        }
        private String memberID;//用户编号
        public String MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        private Creater.Creater creater;//创建者
        public Creater.Creater Creater
        {
            get { return creater; }
            set { creater = value; }
        }
        private List<Content.Image> imageList;//图片集合
        public List<Content.Image> ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }
        private String imageContent;//图片描述
        public String ImageContent
        {
            get { return imageContent; }
            set { imageContent = value; }
        }
        private ImagePushCount imagePushCount;//显示次数和点击次数
        public ImagePushCount ImagePushCount
        {
            get { return imagePushCount; }
            set { imagePushCount = value; }
        }
        private DateTime createdTime;//创建时间
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        private Comm.DeletedFlag deleteFlag;//删除状态
        public Comm.DeletedFlag DeleteFlag
        {
            get { return deleteFlag; }
            set { deleteFlag = value; }
        }

        public ImagePush() { }
        public ImagePush(String contentID)
        {
            try
            {
                ImagePush obj = new ImagePush();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ImagePush> mc = md.GetCollection<ImagePush>(ImagePush.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(contentID)));
                this._id = obj._id;
                this.ContentID = obj.ContentID;
                this.MemberID = obj.MemberID;
                this.Creater = new Creater.Creater(obj.MemberID);
                this.ImageList = obj.ImageList;
                this.ImageContent = obj.ImageContent;
                this.ImagePushCount = obj.ImagePushCount;
                this.CreatedTime = obj.CreatedTime;
                this.DeleteFlag = obj.DeleteFlag;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public ImagePush(String ContentID, String MemberID, List<Content.Image> ImageList, String ImageContent, ImagePushCount ImagePushCount, DateTime CreatedTime, Comm.DeletedFlag DeleteFlag)
        {
            this.ContentID = ContentID;
            this.MemberID = MemberID;
            this.Creater = new BiZ.Creater.Creater(MemberID);
            this.ImageList = ImageList;
            this.ImageContent = ImageContent;
            this.ImagePushCount = ImagePushCount;
            this.CreatedTime = CreatedTime;
            this.DeleteFlag = DeleteFlag;
        }

        public static String GetCollectionName()
        {
            return "ImagePush";
        }
        /// <summary>
        /// 内容回复的添加或更新
        /// </summary>
        /// <param name="obj">回复对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public ImagePush Save(ImagePush obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ImagePush> mc = md.GetCollection<ImagePush>(ImagePush.GetCollectionName());
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
        /// <summary>
        /// 回复删除
        /// </summary>
        /// <param name="ID">回复编号</param>
        /// <returns>删除是否成功</returns>
        public static Boolean Remove(String ID)
        {
            try
            {
                ImagePush imagePush = new ImagePush(ID);
                imagePush.DeleteFlag = Comm.DeletedFlag.No;
                imagePush.Save(imagePush);
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
    }
}
