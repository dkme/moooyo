using System;
using System.Collections.Generic;

namespace Moooyo.BiZ.Core.Member.Activity
{
    /// <summary>
    /// 用户动态
    /// </summary>
    public class Activity
    {
        public ActivityType type;
        public int ActivityCount;
        public String Title;
        public List<String> Content;
        public DateTime CreatedTime;
    }
}

