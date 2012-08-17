using System;
using System.Collections.Generic;

namespace Moooyo.App.Core.BiZ.Member
{
    /// <summary>
    /// 用户间关系
    /// </summary>
    public class MemberRelations
    {
        #region 用户联系信息
        /// <summary>
        /// 收藏的用户
        /// </summary>
        public List<Relation.Favorer> FavorMembers
        {
            get { return this.favorMembers; }
            set { this.favorMembers = value; }
        }
        private List<Relation.Favorer> favorMembers;
        /// <summary>
        /// 被用户收藏
        /// </summary>
        public List<Relation.Favorer> MembersWhoFavoredMe
        {
            get { return this.membersWhoFavoredMe; }
            set { this.membersWhoFavoredMe = value; }
        }
        private List<Relation.Favorer> membersWhoFavoredMe;
        /// <summary>
        /// 来访者
        /// </summary>
        public List<Relation.Visitor> Visitors
        {
            get { return this.visitors; }
            set { this.visitors = value; }
        }
        private List<Relation.Visitor> visitors;
        /// <summary>
        /// 访问过
        /// </summary>
        public List<Relation.Visitor> VisitOut
        {
            get { return this.visitOut; }
            set { this.visitOut = value; }
        }
        private List<Relation.Visitor> visitOut;
        /// <summary>
        /// 收到的礼物
        /// </summary>
        public List<Relation.Giftor> Giftors
        {
            get { return this.giftors; }
            set { this.giftors = value; }
        }
        private List<Relation.Giftor> giftors;
        /// <summary>
        /// 送出礼物
        /// </summary>
        public List<Relation.Giftor> GiftOut
        {
            get { return this.giftOut; }
            set { this.giftOut = value; }
        }
        private List<Relation.Giftor> giftOut;
        #endregion
    }

}

