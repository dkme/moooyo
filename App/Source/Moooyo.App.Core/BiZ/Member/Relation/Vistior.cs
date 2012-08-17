using System;

namespace Moooyo.App.Core.BiZ.Member.Relation
{
    /// <summary>
    /// 访客
    /// </summary>
    public class Visitor : RelationMember
    {
        public Visitor() { }
        public override String GetCollectionName() { return "Vistor"; }
    }
}

