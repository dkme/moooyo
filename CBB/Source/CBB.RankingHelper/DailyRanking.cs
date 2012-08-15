using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RankingHelper
{
    //日排名
    public class DailyRanking : Ranking
    {
        //年
        public int Year;
        //当年第几天
        public int DayOfYear;
    }
}
