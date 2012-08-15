using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Comm
{
    //时间限制类别
    public enum TimeLimitType
    {
        /// <summary>
        /// 今天
        /// </summary>
        Today = 1,
        /// <summary>
        /// 明天
        /// </summary>
        Tomorrow = 2,
        /// <summary>
        /// 本周
        /// </summary>
        ThisWeek = 3,
        /// <summary>
        /// 本周末
        /// </summary>
        ThisWeekEnd = 4,
        /// <summary>
        /// 周末
        /// </summary>
        WeekEnd = 5,
        /// <summary>
        /// 任意
        /// </summary>
        Any = 6
    }
}
