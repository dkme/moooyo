using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    public enum RelationOperationType
    {
        //一直允许操作
        AlwaysAllowOperation,
        //每天只允许操作一次
        OneOperationEachDay,
        //每小时一次
        OneOperationEachHour,
        //只允许一次
        OnlyOneTimes
    }
}
