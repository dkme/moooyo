///
/// 功能描述：兴趣推荐方法集合
/// 作   者：刘安
/// 修改扩展者:刘安
/// 修改日期：2012/2/19
/// 附加信息：
///          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Recommendation
{
    public class InterestRecommendation
    {
        /// <summary>
        /// 猜你喜欢
        /// </summary>
        /// <param name="mid">用户ID</param>
        /// <param name="count">获取兴趣个数</param>
        /// <returns>兴趣列表</returns>
        public static IList<BiZ.InterestCenter.Interest> GuessYourInterest(String mid, int count)
        {
            //获取用户推荐数据
            IList<BiZ.Recommendation.InterestRecommendationData> recommendationDatas =
                CBB.RecommendationHelper.RecommendationDataProvider.GetMyRecommendationDatas<BiZ.Recommendation.InterestRecommendationData>(
                new BiZ.Recommendation.InterestRecommendationData(), mid, count);

            String[] ids = new String[recommendationDatas.Count];
            for (int i = 0; i < recommendationDatas.Count; i++)
            {
                ids[i] = recommendationDatas[i].ObjectID;
            }

            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterest(ids);
            return interests;
        }
        /// <summary>
        /// TA们喜欢的人都喜欢
        /// </summary>
        /// <param name="mid">用户ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>兴趣列表</returns>
        public static IList<BiZ.InterestCenter.Interest> GetMemberFavorMembersTaste(String mid, int count)
        {
            //我喜欢的用户列表
            IList<Member.Relation.Favorer> myFavors = Member.Relation.RelationProvider.GetFavorers(mid, 0, 0);

            String[] myFavorMemberIDs = new String[myFavors.Count];
            for (int i = 0; i < myFavors.Count; i++)
            {
                myFavorMemberIDs[i] = myFavors[i].FromMember;
            }

            IList<Member.Relation.Favorer> myFavorLikers = Member.Relation.RelationProvider.GetFavorers(myFavorMemberIDs, 0, 0);

            String[] myFavorLikerIDs = new String[myFavorLikers.Count];
            for (int i = 0; i < myFavorLikers.Count; i++)
            {
                myFavorLikerIDs[i] = myFavorLikers[i].FromMember;
            }

            //获取用户对兴趣的喜好数据
            IList<BiZ.Recommendation.InterestTrainingData> myFavorTastes
                = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetMyTrainingDatas(
                new BiZ.Recommendation.InterestTrainingData(), myFavorLikerIDs);

            //获取排名最前的6个兴趣
            String[] interestids = CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.SortTrainingDatas(myFavorTastes, count);
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterest(interestids);
            return interests;
        }
        /// <summary>
        /// 喜欢它的人也喜欢
        /// </summary>
        /// <param name="objid">兴趣ID</param>
        /// <param name="count">兴趣ID数量</param>
        /// <returns>喜欢这个兴趣的用户们，还喜欢的兴趣列表，已按喜好兴趣的用户数量排序</returns>
        public static IList<BiZ.InterestCenter.Interest> GetElseInterestIDsMemberLikeWhereWhoLikeThis(String objid, int count)
        {
            //至少有一个相同的兴趣的用户ID字典
            //Dictionary<兴趣ID,Dictionary<喜好它的用户ID,喜好的分数>>
            Dictionary<String, Dictionary<String, float>> objs =
                CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetObjectIDsWhenThemBeenLikeWithThis(
                new BiZ.Recommendation.InterestTrainingData(), objid, count);

            String[] ids = new String[objs.Count];
            int i = 0;
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in objs)
            {
                ids[i] = keyp.Key;
                i++;
            }

            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterest(ids);
            return interests;
        }
        /// <summary>
        /// 喜欢它们的人也喜欢
        /// </summary>
        /// <param name="objid">兴趣ID</param>
        /// <param name="count">兴趣ID数量</param>
        /// <returns>喜欢这个兴趣的用户们，还喜欢的兴趣列表，已按喜好兴趣的用户数量排序</returns>
        public static IList<BiZ.InterestCenter.Interest> GetElseInterestIDsMemberLikeWhereWhoLikeThis(String[] objids, int count)
        {
            //至少有一个相同的兴趣的用户ID字典
            //Dictionary<兴趣ID,Dictionary<喜好它的用户ID,喜好的分数>>
            Dictionary<String, Dictionary<String, float>> objs =
                CBB.RecommendationHelper.TrainingDataProvider<BiZ.Recommendation.InterestTrainingData>.GetObjectIDsWhenThemBeenLikeWithThis(
                new BiZ.Recommendation.InterestTrainingData(), objids, count);

            String[] ids = new String[objs.Count];
            int i = 0;
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in objs)
            {
                ids[i] = keyp.Key;
                i++;
            }

            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetInterest(ids);
            return interests;
        }
    }
}
