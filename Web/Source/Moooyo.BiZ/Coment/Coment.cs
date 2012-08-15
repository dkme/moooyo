using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Coment
{
    /// <summary>
    /// 
    /// </summary>
    public class Coment
    {
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
        /// 昵称 
        /// </summary>
        public String NickName
        {
            get { return this.nickname; }
            set { this.nickname = value; }
        }
        private String nickname;
        /// <summary>
        /// 头像地址
        /// </summary>
        public String ICONPath
        {
            get { return this.iconpath; }
            set { this.iconpath = value; }
        }
        private String iconpath;
        /// <summary>
        /// Content 
        /// </summary>
        public String Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        private String content;
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
        /// 未读标记
        /// </summary>
        public bool UnRead
        {
            get { return this.unRead; }
            set { this.unRead = value; }
        }
        private bool unRead;
        /// <summary>
        /// 未读次数
        /// </summary>
        public int UnreadCommentCount
        {
            get { return this.unreadCommentCount; }
            set { this.unreadCommentCount = value; }
        }
        private int unreadCommentCount;
        /// <summary>
        /// comments 
        /// </summary>
        public IList<Coment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
        private IList<Coment> comments;
        public Coment() { }
    }
}
