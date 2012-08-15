using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RankingHelper
{
    //月排名
    public class PerMonthRanking : Ranking
    {
        //年
        public int Year;
        //月
        public int Month;
    }
}
