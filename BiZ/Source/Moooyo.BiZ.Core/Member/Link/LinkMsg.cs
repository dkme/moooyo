using System;

namespace Moooyo.BiZ.Core.Member.Link
{
    /// <summary>
    /// 用于管理用户间关系动作的消息类
    /// </summary>
    public class LinkMsg
    {
        /// <summary>
        /// ID
        /// </summary>
        public String ID
        {
			get;set;
        }
        /// <summary>
        /// From Member ID
        /// </summary>
        public String SendFrom
        {
            get { return this.sendFrom; }
            set { this.sendFrom = value; }
        }
        private String sendFrom;
        /// <summary>
        /// To Member ID
        /// </summary>
        public String To
        {
            get { return this.to; }
            set { this.to = value; }
        }
        private String to;
        /// <summary>
        /// Msg类型
        /// </summary>
        public Activity.ActivityType LinkMsgType
        {
            get { return this.linkMsgType; }
            set { this.linkMsgType = value; }
        }
        private Activity.ActivityType linkMsgType;
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

        public LinkMsg() { }
    }
}

