using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RankingHelper
{
    //排序接口
    public interface IRankingAble
    {   
        //获取ID
        String GetObjID();
        //获取排序表名
        String GetPerMonthRankingTableName();
        String GetWeeklyRankingTableName();
        String GetEachYearRankingTableName();
        String GetDailyRankingTableName();
        //排名结果表名
        String GetPerMonthRankResultTableName();
        String GetWeeklyRankResultTableName();
        String GetEachYearRankResultTableName();
        String GetDailyRankResultTableName();
    }
}
