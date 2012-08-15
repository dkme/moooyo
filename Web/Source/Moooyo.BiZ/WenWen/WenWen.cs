using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.WenWen
{
    /// <summary>
    /// 问问
    /// </summary>
    public class WenWen : CBB.RankingHelper.IRankingAble
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
        //问问ID
        public String InterestID;
        //标题
        public String Title;
        //内容
        public String Content;
        //图片
        public String ContentImage;
        //创建时间
        public DateTime CreatedTime;
        //更新时间
        public DateTime UpdateTime;
        //创建者
        public Creater.Creater Creater;
        //UpDown
        public UpDown.UpDown UpDowner;
        //回答数
        public int AnswerCount;
        //喜欢数
        public int Likecount;
        /// <summary>
        /// 管理员喜欢次数
        /// </summary>
        public int AdminLikeCount
        {
            get { return this.adminLikeCount; }
            set { this.adminLikeCount = value; }
        }
        private int adminLikeCount;
        /// <summary>
        /// 问问回答
        /// </summary>
        public IList<WenWenAnswer> AnswerList;

        public static String GetCollectionName()
        {
            return "WenWen";
        }

        //获取ID
        public String GetObjID() { return ID; }
        //获取排序表名
        public String GetPerMonthRankingTableName() { return "RankMonthTopic"; }
        public String GetWeeklyRankingTableName() { return "RankWeeklyTopic"; }
        public String GetEachYearRankingTableName() { return "RankYearTopic"; }
        public String GetDailyRankingTableName() { return "RankDailyTopic"; }
        //获取排名结果表名
        public String GetPerMonthRankResultTableName() { return "RankResultMonthTopic"; }
        public String GetWeeklyRankResultTableName() { return "RankResultWeeklyTopic"; }
        public String GetEachYearRankResultTableName() { return "RankResultYearTopic"; }
        public String GetDailyRankResultTableName() { return "RankResultDailyTopic"; }
    }
}
