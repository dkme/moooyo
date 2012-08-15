using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.SystemMsg
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class SystemMsg
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
        /// 对方用户ID
        /// </summary>
        public String FromMember
        {
            get { return this.fromMember; }
            set { this.fromMember = value; }
        }
        private String fromMember;
        /// <summary>
        /// 对方用户ID
        /// </summary>
        public String ToMember
        {
            get { return this.toMember; }
            set { this.toMember = value; }
        }
        private String toMember;
        /// <summary>
        /// 留言
        /// </summary>
        public String Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }
        private String comment;
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
        /// 已读
        /// </summary>
        public bool Readed
        {
            get { return this.readed; }
            set { this.readed = value; }
        }
        private bool readed;
        /// <summary>
        /// 读取时间
        /// </summary>
        public DateTime ReadTime
        {
            get { return this.readTime; }
            set { this.readTime = value; }
        }
        private DateTime readTime;
    }
}
