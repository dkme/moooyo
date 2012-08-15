using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.RecommendationHelper;

namespace Moooyo.BiZ.Recommendation
{
    /// <summary>
    /// 兴趣推荐数据
    /// </summary>
    public class InterestRecommendationData : CBB.RecommendationHelper.RecommendationData
    {
        public override String GetCollectionName() { return "InterestRecommendationData"; }
        public override MongoDB.Driver.Builders.QueryComplete AddicitonalQuery()
        {
            return null;
        }
    }
}
