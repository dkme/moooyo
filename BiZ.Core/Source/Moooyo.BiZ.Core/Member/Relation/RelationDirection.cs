using System;

namespace Moooyo.BiZ.Core.Member.Relation
{
    /// <summary>
    /// 关联方向
    /// </summary>
    public enum RelationDirection
    {
        FromMe=1,
        FromMeToYou=2,
        ToMe=3,
        ToMeFromYou=4,
        Both=5
    }
}

