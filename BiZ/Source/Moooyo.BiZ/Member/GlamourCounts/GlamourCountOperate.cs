using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.GlamourCounts
{
    /// <summary>
    /// 魅力值操作
    /// </summary>
    public class GlamourCountOperate
    {
        /// <summary>
        /// 魅力值类型
        /// </summary>
        public enum GlamourCountType
        {
            Present = 1, //赠送
            ContentLike=2 //内容喜欢
        }
        //修改魅力值
        public enum ModifyGlamourValue
        {
            One = 1 //加一
        }
    }
}
