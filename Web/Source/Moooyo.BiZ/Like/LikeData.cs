using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Like
{
    public class LikeData : LikeDataEntity
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns>表名</returns>
        public static String GetCollectionName()
        {
            return "LikeData";
        }
    }
}
