using System;

namespace Moooyo.BiZ.Core.Member.Relation
{
    /// <summary>
    /// 访客
    /// </summary>
    public class Visitor : BiZ.Core.Member.Relation.RelationMember
    {
        public Visitor() { }
        public override String GetCollectionName() { return "Vistor"; }
    }
}

