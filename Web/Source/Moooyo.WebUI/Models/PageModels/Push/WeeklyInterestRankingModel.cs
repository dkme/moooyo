///
/// 功能描述：周排名控件的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/20
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 周排名控件的页面数据模型
    /// </summary>
    public class WeeklyInterestRankingModel : Moooyo.WebUI.Models.PageModels.PageModelBase
    {
        public IList<CBB.RankingHelper.RankingList> rankinglist;
        public IList<BiZ.InterestCenter.Interest> interestlist;
        public WeeklyInterestRankingModel(
            IList<CBB.RankingHelper.RankingList> rankinglist,
            IList<BiZ.InterestCenter.Interest> interestlist)
        {
            this.rankinglist = rankinglist;
            this.interestlist = interestlist;
        }
    }
}