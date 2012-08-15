using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用户精选兴趣话题实体
    /// </summary>
    public class MemberFeaturedInterestTopicModel : MemberInterestModel
    {
        public IList<CBB.RankingHelper.RankingList> dailyInterestRankingList;
        public IList<BiZ.InterestCenter.Interest> dailyInterestRankingInterestList;
        public IList<BiZ.WenWen.WenWen> latestWenWenList, featuredInterestTopicList, leftTopicList, rightTopicList;
        public string boyPublishedTopics = "1", girlPublishedTopics = "0";
        public long allMemberCount;
        public int publishedTopicSex;

        public MemberFeaturedInterestTopicModel(IList<BiZ.WenWen.WenWen> leftTopicList, IList<BiZ.WenWen.WenWen> rightTopicList)
        {
            this.leftTopicList = leftTopicList;
            this.rightTopicList = rightTopicList;
        }
        public MemberFeaturedInterestTopicModel(MemberFullDisplayObj memberFullDisplayObj,
            IList<CBB.RankingHelper.RankingList> dailyInterestRankingList, IList<BiZ.InterestCenter.Interest> dailyInterestRankingInterestList, IList<BiZ.WenWen.WenWen> latestWenWenList, IList<BiZ.WenWen.WenWen> leftTopicList, IList<BiZ.WenWen.WenWen> rightTopicList)
        {
            this.Member = memberFullDisplayObj;
            this.dailyInterestRankingList = dailyInterestRankingList;
            this.dailyInterestRankingInterestList = dailyInterestRankingInterestList;
            this.latestWenWenList = latestWenWenList;
            this.leftTopicList = leftTopicList;
            this.rightTopicList = rightTopicList;
        }
    }
}