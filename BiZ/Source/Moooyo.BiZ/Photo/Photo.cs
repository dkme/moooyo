using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;

namespace Moooyo.BiZ.Photo
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class Photo
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// MemberID 
        /// </summary>
        public String MemberID
        {
            get { return this.memberID; }
            set { this.memberID = value; }
        }
        private String memberID;
        /// <summary>
        /// 照片类别 
        /// </summary>
        public PhotoType PhotoType
        {
            get { return this.photoType; }
            set { this.photoType = value; }
        }
        private PhotoType photoType;
        /// <summary>
        /// 是否已审核 
        /// </summary>
        public int IsAudited
        {
            get { return this.isAudited; }
            set { this.isAudited = value; }
        }
        private int isAudited;
        /// <summary>
        /// 照片标题
        /// </summary>
        public String Title
        {
            get { if (this.title == null) return ""; else return this.title; }
            set { this.title = value; }
        }
        private String title;
        /// <summary>
        /// 照片说明
        /// </summary>
        public String Content
        {
            get { if (this.content == null) return ""; else return this.content; }
            set { this.content = value; }
        }
        private String content;
        /// <summary>
        /// 照片文件路径
        /// </summary>
        public String FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }
        private String fileName;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 浏览次数 
        /// </summary>
        public int ViewCount
        {
            get { return this.viewCount; }
            set { this.viewCount = value; }
        }
        private int viewCount;
        /// <summary>
        /// 评论个数
        /// </summary>
        public int CommentCount
        {
            get { return this.commentCount; }
            set { this.commentCount = value; }
        }
        private int commentCount;

        /// <summary>
        /// comments 
        /// </summary>
        public IList<Coment.Coment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
        private IList<Coment.Coment> comments;

        public Photo() { }

        public static String GetCollectionName()
        {
            return "Photos";
        }
     }
}
