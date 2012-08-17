using System;

namespace Moooyo.App.Core.BiZ.Member.Relation
{
    /// <summary>
    /// 联系中用户基类
    /// </summary>
    public abstract class RelationMember
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
			get;set;
        }
        /// <summary>
        /// 从（源）用户
        /// </summary>
        public String FromMember
        {
            get { return this.frommember; }
            set { this.frommember = value; }
        }
        private String frommember;
        /// <summary>
        /// 向（目标）用户
        /// </summary>
        public String ToMember
        {
            get { return this.tomember; }
            set { this.tomember = value; }
        }
        private String tomember;
        /// <summary>
        /// 备注
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

        public abstract String GetCollectionName();
    }
}

