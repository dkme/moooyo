using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    /// <summary>
    /// 邀请注册用户
    /// </summary>
    public class RegInviter : BiZ.Member.RelationMember
    {
        public RegInviter()
        { }

        public override String GetCollectionName() { return "RegisterInviter"; }
    }
}
