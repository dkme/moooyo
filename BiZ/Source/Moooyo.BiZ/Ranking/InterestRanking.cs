///
/// 功能描述：兴趣排名方法集合
/// 作   者：刘安
/// 修改扩展者:刘安
/// 修改日期：2012/2/20
/// 附加信息：
///        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Ranking
{
    public class InterestRanking
    {
        /// <summary>
        /// 获取兴趣日排名
        /// </summary>
        /// <returns></returns>
        public static IList<CBB.RankingHelper.RankingList> GetDailyInterestRankingList(int count)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetDailyHistoryRankingTable(new BiZ.InterestCenter.Interest().GetDailyRankResultTableName(), year, dayofyear, 1, count);
            return rkl;
        }
        /// <summary>
        /// 获取兴趣周排名
        /// </summary>
        /// <returns></returns>
        public static IList<CBB.RankingHelper.RankingList> GetWeeklyInterestRankingList(int count)
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

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetWeeklyHistoryRankingTable(new BiZ.InterestCenter.Interest().GetWeeklyRankResultTableName(), year, weekofyear,1,count);
            return rkl;
        }
        /// <summary>
        /// 获取兴趣月排名
        /// </summary>
        /// <returns></returns>
        public static IList<CBB.RankingHelper.RankingList> GetMonthInterestRankingList(int count)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetMonthHistoryRankingTable(new BiZ.InterestCenter.Interest().GetPerMonthRankResultTableName(), year, month, 1, count);
            return rkl;
        }
        /// <summary>
        /// 获取兴趣年排名
        /// </summary>
        /// <returns></returns>
        public static IList<CBB.RankingHelper.RankingList> GetYearInterestRankingList(int count)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = CBB.RankingHelper.RankingProvider.DatePart(date);

            IList<CBB.RankingHelper.RankingList> rkl = CBB.RankingHelper.RankingProvider.GetYearHistoryRankingTable(new BiZ.InterestCenter.Interest().GetEachYearRankResultTableName(), year, 1, count);
            return rkl;
        }
    }
}
