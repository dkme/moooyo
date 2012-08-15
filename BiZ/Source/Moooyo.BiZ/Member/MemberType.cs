using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member
{
    /// <summary>
    /// 会员类别
    /// </summary>
    public enum MemberType
    {
        /// <summary>
        /// 初级会员 - 未发展任何会员的，浏览权限受限制的
        /// </summary>
        Level0=0,
        /// <summary>
        /// 会员 - 已发展了会员，浏览权限不受限制
        /// </summary>
        Level1=1,
        /// <summary>
        /// 高级会员 - 缴纳会员费用的，享受高级会员权限的
        /// </summary>
        Level2=2
    }
}
