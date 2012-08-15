///
/// 功能描述：用户推荐方法集合
/// 作   者：刘安
/// 修改扩展者:刘安
/// 修改日期：2012/2/17
/// 附加信息：
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Recommendation
{
    public class MemberRecommendation
    {
        /// <summary>
        /// 获取和我兴趣相同的用户
        /// </summary>
        /// <param name="mid">用户ID</param>
        /// <param name="count">获取的用户ID数量</param>
        /// <returns/>和我兴趣相同的用户字典（用户ID（兴趣ID，喜好分值）），已按喜好数量排序</returns>
        public static Dictionary<String, Dictionary<String, float>> GetMembersWhoTasterLikeMe(String mid, int count) 
        {
            //用户对兴趣的喜好数据
            IList<BiZ.Recommendation.InterestTrainingData> myFavorTastes
                = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetMyTrainingDatas(
                new BiZ.Recommendation.InterestTrainingData(), mid);

            //排名最前的6个兴趣
            String[] myInterestTasters = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.SortTrainingDatas(myFavorTastes, 8);//获取我最喜欢的8个兴趣

            //至少有一个相同的兴趣的用户ID字典
            //Dictionary<用户ID,Dictionary<喜好的兴趣ID,喜好的分数>>
            Dictionary<String, Dictionary<String, float>> objs =
                CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetMemberIDsWhoHadSameObjectID(
                new BiZ.Recommendation.InterestTrainingData(), myInterestTasters, count+1);//要把自己去掉
            //去掉我自己
            objs.Remove(mid);

            return objs;
        }
        /// <summary>
        /// 获取和他们兴趣相同的用户
        /// </summary>
        /// <param name="memberIDs">他们的用户ID集合</param>
        /// <param name="count">获取的用户ID数量</param>
        /// <returns>和他们兴趣相同的用户列表</returns>
        public static IList<BiZ.Member.Member> GetMembersWhoTasterLikeThem(String[] memberIDs, int count)
        {
            //用户对兴趣的喜好数据
            IList<BiZ.Recommendation.InterestTrainingData> myFavorTastes
                = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetMyTrainingDatas(
                new BiZ.Recommendation.InterestTrainingData(), memberIDs);

            //排名最前的6个兴趣
            String[] myInterestTasters = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.SortTrainingDatas(myFavorTastes, 6);

            //至少有一个相同的兴趣的用户ID字典
            //Dictionary<用户ID,Dictionary<喜好的兴趣ID,喜好的分数>>
            Dictionary<String, Dictionary<String, float>> objs =
                CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetMemberIDsWhoHadSameObjectID(
                new BiZ.Recommendation.InterestTrainingData(), myInterestTasters, count);

            String[] ids = new String[objs.Count];
            int i = 0;
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in objs)
            {
                ids[i] = keyp.Key;
                i++;
            }

            IList<BiZ.Member.Member> members = BiZ.MemberManager.MemberManager.GetMember(ids);
            return members;
        }
        /// <summary>
        /// 喜欢TA的人也喜欢
        /// </summary>
        /// <param name="mid">用户ID</param>
        /// <param name="count">用户ID数量</param>
        /// <returns>喜欢这个用户的用户们，还喜欢的用户字典（用户ID（用户ID，喜好分值）），已按喜好用户的用户数量排序</returns>
        public static IList<BiZ.Member.Member> GetElseMemberIDsMemberLikeWhereWhoLikeThis(String mid, int count)
        {
            //至少有一个相同的兴趣的用户ID字典
            //字典集合：对象ID（喜好用户ID，喜好分值）
            Dictionary<String, Dictionary<String, float>> objs =
                CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.MemberFavorTrainingData>.GetObjectIDsWhenThemBeenLikeWithThis(
                new BiZ.Recommendation.MemberFavorTrainingData(), mid, count);

            String[] ids = new String[objs.Count];
            int i = 0;
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in objs)
            {
                ids[i] = keyp.Key;
                i++;
            }

            IList<BiZ.Member.Member> members = BiZ.MemberManager.MemberManager.GetMember(ids);
            return members;
        }
    }
}
