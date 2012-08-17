using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.App.Core.BiZ.InterestCenter
{
    //兴趣的粉丝
    public class InterestFans : Fans.Fans
    {
        //用户在兴趣下发布的内容数量
        private long contentCount;
        public long ContentCount
        {
            get { return contentCount; }
            set { contentCount = value; }
        }
        public static String GetCollectionName()
        {
            return "InterestFans";
        }
    }
}
