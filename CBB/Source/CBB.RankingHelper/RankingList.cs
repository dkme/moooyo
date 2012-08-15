using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RankingHelper
{
    //排名对象
    public class RankingList
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        //对象ID
        public String ObjID;
        //分值
        public long Scores;
        //名次
        public int PositionInRanking;
        //排名增长（负值代表下降）
        public int PositionIncrease;
        //年
        public int Year;
        //月
        public int Month;
        //周
        public int WeekOfYear;
        //当年第几天
        public int DayOfYear;
    }
}
