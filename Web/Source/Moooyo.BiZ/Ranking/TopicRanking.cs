/*************************************************
 * Functional description ：兴趣下话题排名数据提供类
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/4/18 Wednesday  
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Ranking
{
    public class TopicRanking
    {
        /// <summary>
        /// 获取兴趣话题日排名
        /// </summary>
        /// <param name="count">取多少条</param>
        /// <returns>名次列表</returns>
        public static IList<CBB.RankingHelper.RankingList> GetDailyTopicRankingList(int count)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);

            IList<CBB.RankingHelper.RankingList> rankingList = CBB.RankingHelper.RankingProvider.GetDailyHistoryRankingTable(new BiZ.WenWen.WenWen().GetDailyRankResultTableName(), year, dayofyear, 1, count);
            return rankingList;
        }

        /// <summary>
        /// 获取兴趣话题周排名
        /// </summary>
        /// <returns></returns>
        public static IList<CBB.RankingHelper.RankingList> GetWeeklyTopicRankingList(int count)
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);
            if (weekofyear < 1)
            {
                year = year - 1;
                weekofyear = CBB.RankingHelper.RankingProvider.GetYearWeekCount(year);
            }

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetWeeklyHistoryRankingTable(new BiZ.WenWen.WenWen().GetWeeklyRankResultTableName(), year, weekofyear, 1, count);
            return rkl;
        }

        /// <summary>
        /// 获取兴趣话题月排名
        /// </summary>
        /// <param name="count">取多少条</param>
        /// <returns>名次列表</returns>
        public static IList<CBB.RankingHelper.RankingList> GetMonthTopicRankingList(int count)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetMonthHistoryRankingTable(new BiZ.WenWen.WenWen().GetPerMonthRankResultTableName(), year, month, 1, count);
            return rkl;
        }
    }
}
