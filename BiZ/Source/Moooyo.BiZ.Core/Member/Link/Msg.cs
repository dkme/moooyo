using System;

namespace Moooyo.BiZ.Core.Member.Link
{
    /// <summary>
    /// 用户交谈
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// ID
        /// </summary>
        public String ID
        {
			get;set;
        }
        /// <summary>
        /// 源用户ID
        /// </summary>
        public String FromMember
        {
            get { return this.fromMember; }
            set { this.fromMember = value; }
        }
        private String fromMember;
        /// <summary>
        /// 目标用户ID
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
        /// 留言类别
        /// </summary>
        public Core.Member.Activity.ActivityType TalkType
        {
            get { return this.talkType; }
            set { this.talkType = value; }
        }
        private Core.Member.Activity.ActivityType talkType;
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
        /// 
        /// </summary>
        public DateTime ReadTime
        {
            get { return this.readTime; }
            set { this.readTime = value; }
        }
        private DateTime readTime;

        public Msg() { }

        public static String GetCollectionName()
        {
            return "Msg";
        }
    }
}

