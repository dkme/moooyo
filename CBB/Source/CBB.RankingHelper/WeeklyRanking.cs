using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RankingHelper
{
    //周排名
    public class WeeklyRanking : Ranking
    {
        //年
        public int Year;
        //周
        public int WeekOfYear;
    }
}
