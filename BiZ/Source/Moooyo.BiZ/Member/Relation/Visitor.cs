using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    /// <summary>
    /// 访客
    /// </summary>
    public class Visitor : BiZ.Member.RelationMember
    {
        public Visitor() { }
        public override String GetCollectionName() { return "Vistor"; }
    }
}
