///
/// 功能描述：TA们喜欢的都喜欢
/// 作   者：刘安
/// 修改扩展者:刘安
/// 修改日期：2012/2/17
/// 附加信息：获取用户加粉的用户们的兴趣集合
///          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YuJian.BiZ.Recommendation
{
    public class FavorMembersTaste
    {
        /// <summary>
        /// 喜欢的用户的口味
        /// </summary>
        public static String[] GetMemberFavorMembersTaste(String mid,int count)
        {
            //我喜欢的用户列表
            IList<Member.Relation.Favorer> myFavors = Member.Relation.RelationProvider.GetFavorers(mid,0,0);
            
            String[] myFavorMemberIDs = new String[myFavors.Count];
            for (int i=0;i<myFavors.Count;i++)
            {
                myFavorMemberIDs[i] = myFavors[i].FromMember;
            }

            IList<Member.Relation.Favorer> myFavorLikers = Member.Relation.RelationProvider.GetFavorers(myFavorMemberIDs,0,0);

            String[] myFavorLikerIDs = new String[myFavorLikers.Count];
            for (int i=0;i<myFavorLikers.Count;i++)
            {
                myFavorLikerIDs[i] = myFavorLikers[i].FromMember;
            }

            //获取用户对兴趣的喜好数据
            IList<BiZ.Recommendation.InterestTrainingData> myFavorTastes 
                = CBB.RecommendationHelper.TrainingDataProvider.GetMyTrainingDatas<BiZ.Recommendation.InterestTrainingData>(
                new BiZ.Recommendation.InterestTrainingData().GetCollectionName(),myFavorLikerIDs);

            //获取排名最前的6个兴趣
            return SortInterestTrainingDatas(myFavorTastes, 6);
        }
        /// <summary>
        /// 批量用户喜好训练数据排序
        /// </summary>
        /// <param name="interestTrainingDatas"></param>
        /// <param name="count"></param>
        /// <returns>喜好兴趣ID数组</returns>
        private static String[] SortInterestTrainingDatas(IList<BiZ.Recommendation.InterestTrainingData> interestTrainingDatas,int count)
        {
            //分值累加
            Dictionary<String,float> scores = new Dictionary<string,float>();
            foreach (BiZ.Recommendation.InterestTrainingData itd in interestTrainingDatas)
            {
                if (scores.ContainsKey(itd.ObjectID)) scores[itd.ObjectID] = scores[itd.ObjectID] + itd.Value;
                else
                    scores.Add(itd.ObjectID,itd.Value);
            }
            //排序
            IEnumerable<KeyValuePair<String,float>> result = (from score in scores orderby score.Value descending select score).Take(count);
            String[] ids = new String[result.Count<KeyValuePair<String, float>>()];
            int i = 0;
            foreach (KeyValuePair<String,float> score in result)
            {
                ids[i] = score.Key;
                i++;
            }

            return ids;
        }
    }
}
