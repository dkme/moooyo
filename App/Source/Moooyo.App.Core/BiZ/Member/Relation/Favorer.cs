using System;

namespace Moooyo.App.Core.BiZ.Member.Relation
{
    /// <summary>
    /// 收藏的用户
    /// </summary>
    public class Favorer : RelationMember
    {
        private long visitCount;//收藏用户对被收藏用户的访问数量
        public long VisitCount
        {
            get { return visitCount; }
            set { visitCount = value; }
        }
        public Favorer()
        { }

        public override String GetCollectionName() { return "Favorer"; }
    }
}

