/*******************************************************************
 * Functional description ：用户类型数据实体
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/6/13 Wednesday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Comm
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 会员
        /// </summary>
        Member = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 1,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 2
    }
}
