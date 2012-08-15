/*————————————————————————————————
 * Functional description ：通用标记
 * Author：Lau Tao
 * Modify the expansion：Tao Lau 
 * Modified date：2012/7/3 Tuesday 
 * Remarks：
————————————————————————————————*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Comm
{
    /// <summary>
    /// 使用或启用标记
    /// </summary>
    public enum UsedFlag
    {
        No = 0, Yes = 1, Unknown = 2
    }
    /// <summary>
    /// 删除标记
    /// </summary>
    public enum DeletedFlag
    {
        No = 0,
        Yes = 1
    }
}
