using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.RecommendationHelper;

namespace Moooyo.BiZ.Recommendation
{
    /// <summary>
    /// 用户喜好推荐数据
    /// </summary>
    public class MemberFavorRecommendationData : RecommendationData
    {
        //源用户和目标用户是否相同性别
        public bool SameSex;
        //源用户和目标用户是否相同地区
        public bool SameZone;

        public override String GetCollectionName() { return "MemberRecommendationData"; }

        public override MongoDB.Driver.Builders.QueryComplete AddicitonalQuery()
        {
            MongoDB.Driver.Builders.QueryComplete qc = 
                MongoDB.Driver.Builders.Query.And(
                MongoDB.Driver.Builders.Query.EQ("SameSex", false),
                MongoDB.Driver.Builders.Query.EQ("SameZone",true)
                );

            return qc;
        }
    }
}
