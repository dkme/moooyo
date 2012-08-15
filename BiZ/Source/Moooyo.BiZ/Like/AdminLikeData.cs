/*************************************************
 * Functional description ：管理员喜欢数据实体
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/4/19 Thursday   
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Like
{
    public class AdminLikeData : LikeDataEntity
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns>表名</returns>
        public static String GetCollectionName()
        {
            return "AdminLikeData";
        }
    }
}
