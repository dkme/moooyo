using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace CBB.RankingHelper
{
    //排名服务类
    public class RankingProvider
    {
        /// <summary>
        /// 分值改变
        /// </summary>
        /// <param name="ira">IRankingAble对象</param>
        /// <param name="scoresIncrease">增加分值(减少传入负分值)</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddScores(IRankingAble ira, int scoresIncrease)
        {
            //排名对象
            String objid = ira.GetObjID();
            //排名时段
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int dayofyear = date.DayOfYear;
            int weekofyear = DatePart(date);
            //排名数据表名
            String yearrankingtablename = ira.GetEachYearRankingTableName();
            String permonthrankingtablename = ira.GetPerMonthRankingTableName();
            String weeklyrankingtablename = ira.GetWeeklyRankingTableName();
            String dailyrankingtablename = ira.GetDailyRankingTableName();

            CBB.ExceptionHelper.OperationResult result = new CBB.ExceptionHelper.OperationResult(true);

            if (yearrankingtablename != "")
            {
                result = UpdateEachYearRanking(yearrankingtablename, objid, year, scoresIncrease);
            }
            if (permonthrankingtablename != "")
            {
                result = UpdatePerMonthRanking(permonthrankingtablename, objid, year, month, scoresIncrease);
            }
            if (weeklyrankingtablename != "")
            {
                result = UpdateWeeklyRanking(weeklyrankingtablename, objid, year, weekofyear, scoresIncrease);
            }
            if (dailyrankingtablename != "")
            {
                result = UpdateDailyRanking(dailyrankingtablename, objid, year, dayofyear, scoresIncrease);
            }

            return result;

        }

        #region 创建历史排名（用于每天晚上调用生成,运算量大，不要实时使用）
        public static CBB.ExceptionHelper.OperationResult BuildRankingTable(CBB.RankingHelper.IRankingAble ira, int year, int month, int dayofyear, int weekofyear)
        {
            try
            {
                //生成每日排名
                CBB.RankingHelper.RankingProvider.BuildDailyRankingTable(ira.GetDailyRankResultTableName(),
                    ira.GetDailyRankingTableName(), year, dayofyear);
                //生成每周排名
                CBB.RankingHelper.RankingProvider.BuildWeeklyRankingTable(ira.GetWeeklyRankResultTableName(),
                    ira.GetWeeklyRankingTableName(), year, weekofyear);
                //生成每月排名
                CBB.RankingHelper.RankingProvider.BuildMonthRankingTable(ira.GetPerMonthRankResultTableName(),
                    ira.GetPerMonthRankingTableName(), year, dayofyear);
                //生成每年排名
                CBB.RankingHelper.RankingProvider.BuildYearRankingTable(ira.GetEachYearRankResultTableName(),
                    ira.GetEachYearRankingTableName(), year);

                return new ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult BuildYearRankingTable(String listtablename, String yearrankingtablename, int year)
        {
            IList<RankingList> rkl = CreateYearRankingList(yearrankingtablename, year);
            int oldyear = year-1;
            //获取历史排名
            foreach (RankingList obj in rkl)
            {
                RankingList listobj = GetYearHistoryRanking(listtablename, obj.ObjID, oldyear);

                if (listobj.PositionInRanking != 0)
                    obj.PositionIncrease = (obj.PositionInRanking - listobj.PositionInRanking) * -1;
                else
                    obj.PositionIncrease = (obj.PositionInRanking - rkl.Count) * -1;

                //如果本期有排名，则替换
                RankingList newobj = GetYearHistoryRanking(listtablename, obj.ObjID, year);
                newobj.ObjID = obj.ObjID;
                newobj.Year = obj.Year;
                newobj.Month = obj.Month;
                newobj.WeekOfYear = obj.WeekOfYear;
                newobj.DayOfYear = obj.DayOfYear;
                newobj.Scores = obj.Scores;
                newobj.PositionIncrease = obj.PositionIncrease;
                newobj.PositionInRanking = obj.PositionInRanking;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RankingList> mc = md.GetCollection<RankingList>(listtablename);
                mc.Save(newobj);
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult BuildMonthRankingTable(String listtablename, String monthrankingtablename, int year,int month)
        {
            IList<RankingList> rkl = CreateMonthRankingList(monthrankingtablename, year,month);
            int oldyear = year;
            int oldmonth = month - 1;
            if (oldmonth < 1)
            {
                oldyear = year - 1;
                month = 12;
            }
            //获取历史排名
            foreach (RankingList obj in rkl)
            {
                RankingList listobj = GetMonthHistoryRanking(listtablename, obj.ObjID, oldyear,oldmonth);

                if (listobj.PositionInRanking != 0)
                    obj.PositionIncrease = (obj.PositionInRanking - listobj.PositionInRanking) * -1;
                else
                    obj.PositionIncrease = (obj.PositionInRanking - rkl.Count) * -1;

                //如果本期有排名，则替换
                RankingList newobj = GetMonthHistoryRanking(listtablename, obj.ObjID, year, month);
                newobj.ObjID = obj.ObjID;
                newobj.Year = obj.Year;
                newobj.Month = obj.Month;
                newobj.WeekOfYear = obj.WeekOfYear;
                newobj.DayOfYear = obj.DayOfYear;
                newobj.Scores = obj.Scores;
                newobj.PositionIncrease = obj.PositionIncrease;
                newobj.PositionInRanking = obj.PositionInRanking;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RankingList> mc = md.GetCollection<RankingList>(listtablename);
                mc.Save(newobj);
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult BuildWeeklyRankingTable(String listtablename, String weeklyrankingtablename, int year, int weekofyear)
        {
            IList<RankingList> rkl = CreateWeeklyRankingList(weeklyrankingtablename, year, weekofyear);
            int oldyear = year;
            int oldweekofyear = weekofyear - 1;
            if (oldweekofyear < 1)
            {
                oldyear = year - 1;
                oldweekofyear = GetYearWeekCount(oldyear);
            }
            //获取历史排名
            foreach (RankingList obj in rkl)
            {
                RankingList listobj = GetWeeklyHistoryRanking(listtablename, obj.ObjID, oldyear, oldweekofyear);

                if (listobj.PositionInRanking != 0)
                    obj.PositionIncrease = (obj.PositionInRanking - listobj.PositionInRanking) * -1;
                else
                    obj.PositionIncrease = (obj.PositionInRanking - rkl.Count) * -1;

                //如果本期有排名，则替换
                RankingList newobj = GetWeeklyHistoryRanking(listtablename, obj.ObjID, year, weekofyear);
                newobj.ObjID = obj.ObjID;
                newobj.Year = obj.Year;
                newobj.Month = obj.Month;
                newobj.WeekOfYear = obj.WeekOfYear;
                newobj.DayOfYear = obj.DayOfYear;
                newobj.Scores = obj.Scores;
                newobj.PositionIncrease = obj.PositionIncrease;
                newobj.PositionInRanking = obj.PositionInRanking;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RankingList> mc = md.GetCollection<RankingList>(listtablename);
                mc.Save(newobj);
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult BuildDailyRankingTable(String listtablename, String dailyrankingtablename, int year, int dayofyear)
        {
            IList<RankingList> rkl = CreateDailyRankingList(dailyrankingtablename, year, dayofyear);
            int oldyear = year;
            int olddayofyear = dayofyear - 1;
            if (olddayofyear < 1)
            {
                oldyear = year - 1;
                olddayofyear = GetYearDaysCount(oldyear);
            }
            //获取历史排名
            foreach (RankingList obj in rkl)
            {
                RankingList listobj = GetDailyHistoryRanking(listtablename, obj.ObjID, oldyear,olddayofyear);

                if (listobj.PositionInRanking != 0)
                    obj.PositionIncrease = (obj.PositionInRanking - listobj.PositionInRanking) * -1;
                else
                    obj.PositionIncrease = (obj.PositionInRanking - rkl.Count) * -1;

                //如果本期有排名，则替换
                RankingList newobj = GetDailyHistoryRanking(listtablename, obj.ObjID, year, dayofyear);
                newobj.ObjID = obj.ObjID;
                newobj.Year = obj.Year;
                newobj.Month = obj.Month;
                newobj.WeekOfYear = obj.WeekOfYear;
                newobj.DayOfYear = obj.DayOfYear;
                newobj.Scores = obj.Scores;
                newobj.PositionIncrease = obj.PositionIncrease;
                newobj.PositionInRanking = obj.PositionInRanking;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RankingList> mc = md.GetCollection<RankingList>(listtablename);
                mc.Save(newobj);
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion

        #region 排名表创建
        /// <summary>
        /// 获取年度排名
        /// </summary>
        /// <param name="yearrankingtablename">排名表</param>
        /// <param name="year">年度</param>
        /// <param name="pageno">获取排名页码</param>
        /// <param name="pagesize">每页排名数量</param>
        /// <returns></returns>
        public static IList<RankingList> CreateYearRankingList(String yearrankingtablename, int year)
        {
            try
            {
                MongoCursor<EachYearRanking> mc = MongoDBHelper.GetCursor<EachYearRanking>(
                    yearrankingtablename,
                    Query.EQ("Year",year),
                    new SortByDocument("Scores", -1),
                    0,
                    0);

                List<EachYearRanking> objs = new List<EachYearRanking>();
                objs.AddRange(mc);

                //排名表
                List<RankingList> raklist = new List<RankingList>();
                for (int i=0;i<objs.Count;i++)
                {
                    RankingList rk = new RankingList();
                    rk.ObjID = objs[i].ObjID;
                    rk.Scores = objs[i].Scores;
                    rk.Year = objs[i].Year;
                    rk.Month = -1;
                    rk.DayOfYear = -1;
                    rk.WeekOfYear = -1;
                    rk.PositionInRanking = i + 1;
                    raklist.Add(rk);
                }

                return raklist;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 获取月度排名
        /// </summary>
        /// <param name="yearrankingtablename">排名表</param>
        /// <param name="year">年度</param>
        /// <param name="month">月</param>
        /// <param name="pageno">获取排名页码</param>
        /// <param name="pagesize">每页排名数量</param>
        /// <returns></returns>
        public static IList<RankingList> CreateMonthRankingList(String permonthrankingtablename, int year, int month)
        {
            try
            {
                MongoCursor<PerMonthRanking> mc = MongoDBHelper.GetCursor<PerMonthRanking>(
                    permonthrankingtablename,
                    Query.And(
                        Query.EQ("Year", year),
                        Query.EQ("Month", month)
                    ),
                    new SortByDocument("Scores", -1),
                    0,
                    0);

                List<PerMonthRanking> objs = new List<PerMonthRanking>();
                objs.AddRange(mc);

                //排名表
                List<RankingList> raklist = new List<RankingList>();
                for (int i = 0; i < objs.Count; i++)
                {
                    RankingList rk = new RankingList();
                    rk.ObjID = objs[i].ObjID;
                    rk.Scores = objs[i].Scores;
                    rk.Year = objs[i].Year;
                    rk.Month = objs[i].Month;
                    rk.DayOfYear = -1;
                    rk.WeekOfYear = -1;
                    rk.PositionInRanking = i + 1;
                    raklist.Add(rk);
                }

                return raklist;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 获取月度排名
        /// </summary>
        /// <param name="yearrankingtablename">排名表</param>
        /// <param name="year">年度</param>
        /// <param name="month">月</param>
        /// <param name="pageno">获取排名页码</param>
        /// <param name="pagesize">每页排名数量</param>
        /// <returns></returns>
        public static IList<RankingList> CreateWeeklyRankingList(String weeklyrankingtablename, int year, int weekofyear)
        {
            try
            {
                MongoCursor<WeeklyRanking> mc = MongoDBHelper.GetCursor<WeeklyRanking>(
                    weeklyrankingtablename,
                    Query.And(
                        Query.EQ("Year", year),
                        Query.EQ("WeekOfYear", weekofyear)
                    ),
                    new SortByDocument("Scores", -1),
                    0,
                    0);

                List<WeeklyRanking> objs = new List<WeeklyRanking>();
                objs.AddRange(mc);

                //排名表
                List<RankingList> raklist = new List<RankingList>();
                for (int i = 0; i < objs.Count; i++)
                {
                    RankingList rk = new RankingList();
                    rk.ObjID = objs[i].ObjID;
                    rk.Scores = objs[i].Scores;
                    rk.Year = objs[i].Year;
                    rk.Month = -1;
                    rk.DayOfYear = -1;
                    rk.WeekOfYear = objs[i].WeekOfYear;
                    rk.PositionInRanking = i + 1;
                    raklist.Add(rk);
                }

                return raklist;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 获取月度排名
        /// </summary>
        /// <param name="yearrankingtablename">排名表</param>
        /// <param name="year">年度</param>
        /// <param name="month">月</param>
        /// <param name="pageno">获取排名页码</param>
        /// <param name="pagesize">每页排名数量</param>
        /// <returns></returns>
        public static IList<RankingList> CreateDailyRankingList(String dailyrankingtablename, int year, int dayofyear)
        {
            try
            {
                MongoCursor<DailyRanking> mc = MongoDBHelper.GetCursor<DailyRanking>(
                    dailyrankingtablename,
                    Query.And(
                        Query.EQ("Year", year),
                        Query.EQ("DayOfYear", dayofyear)
                    ),
                    new SortByDocument("Scores", -1),
                    0,
                    0);

                List<DailyRanking> objs = new List<DailyRanking>();
                objs.AddRange(mc);

                //排名表
                List<RankingList> raklist = new List<RankingList>();
                for (int i = 0; i < objs.Count; i++)
                {
                    RankingList rk = new RankingList();
                    rk.ObjID = objs[i].ObjID;
                    rk.Scores = objs[i].Scores;
                    rk.Year = objs[i].Year;
                    rk.Month = -1;
                    rk.DayOfYear = objs[i].DayOfYear;
                    rk.WeekOfYear = -1;
                    rk.PositionInRanking = i + 1;
                    raklist.Add(rk);
                }

                return raklist;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 历史排名
        //获取年度历史排名
        public static RankingList GetYearHistoryRanking(String listtablename, String objid, int year)
        {
            return GetHistoryRanking(listtablename, objid, year, -1, -1, -1);
        }
        //获取月度历史
        public static RankingList GetMonthHistoryRanking(String listtablename, String objid, int year, int month)
        {
            return GetHistoryRanking(listtablename, objid, year, month, -1, -1);
        }
        //获取历史周排名
        public static RankingList GetWeeklyHistoryRanking(String listtablename, String objid, int year, int weekofyear)
        {
            return GetHistoryRanking(listtablename, objid, year, -1, weekofyear, -1);
        }
        //获取历史日排名
        public static RankingList GetDailyHistoryRanking(String listtablename, String objid, int year, int dayofyear)
        {
            return GetHistoryRanking(listtablename, objid, year, -1, -1, dayofyear);
        }
        private static RankingList GetHistoryRanking(String listtablename, String objid, int year, int month, int weekofyear, int dayofyear)
        {
            QueryComplete qc = Query.EQ("Year", year);
            if (month != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("Month", month));
            if (weekofyear != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("WeekOfYear", weekofyear));
            if (dayofyear != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("DayOfYear", dayofyear));

            RankingList iv = new RankingList();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RankingList> mc = md.GetCollection<RankingList>(listtablename);
                iv = mc.FindOne(
                        Query.And(
                            Query.EQ("ObjID", objid),
                            qc
                        )
                    );
                if (iv == null) iv = new RankingList();
                iv.ObjID = objid;
                iv.PositionInRanking = 0;
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //获取年度历史排名表
        public static IList<RankingList> GetYearHistoryRankingTable(String listtablename, int year, int pageno, int pagesize)
        {
            return GetHistoryRankingTable(listtablename, year, -1, -1, -1, pageno, pagesize);
        }
        //获取月度历史排名表
        public static IList<RankingList> GetMonthHistoryRankingTable(String listtablename, int year,int month, int pageno, int pagesize)
        {
            return GetHistoryRankingTable(listtablename, year, month, -1, -1, pageno, pagesize);
        }
        //获取周历史排名表
        public static IList<RankingList> GetWeeklyHistoryRankingTable(String listtablename, int year,int weekofyear, int pageno, int pagesize)
        {
            return GetHistoryRankingTable(listtablename, year, -1, weekofyear, -1, pageno, pagesize);
        }
        //获取日历史排名表
        public static IList<RankingList> GetDailyHistoryRankingTable(String listtablename, int year,int dayofyear, int pageno, int pagesize)
        {
            return GetHistoryRankingTable(listtablename, year, -1, -1, dayofyear, pageno, pagesize);
        }
        private static IList<RankingList> GetHistoryRankingTable(String listtablename, int year,int month,int weekofyear,int dayofyear, int pageno, int pagesize)
        {
            QueryComplete qc = Query.EQ("Year", year);
            if (month != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("Month", month));
            if (weekofyear != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("WeekOfYear", weekofyear));
            if (dayofyear != -1) qc = Query.And(Query.EQ("Year", year), Query.EQ("DayOfYear", dayofyear));

            try
            {
                MongoCursor<RankingList> mc = MongoDBHelper.GetCursor<RankingList>(
                    listtablename,
                    qc,
                    new SortByDocument("PositionInRanking", 1),
                    pageno,
                    pagesize);

                List<RankingList> objs = new List<RankingList>();
                objs.AddRange(mc);

                return objs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        #region 分值记录
        //获取Ranking对象
        private static EachYearRanking GetEachYearRanking(String yearrankingtablename, String id, int year)
        {
            EachYearRanking iv = new EachYearRanking();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<EachYearRanking> mc = md.GetCollection<EachYearRanking>(yearrankingtablename);
                iv = mc.FindOne(
                        Query.And(
                            Query.EQ("ObjID", id),
                            Query.EQ("Year", year)
                        )
                    );
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static PerMonthRanking GetPerMonthRanking(String permonthrankingtablename, String id, int year, int month)
        {
            PerMonthRanking iv = new PerMonthRanking();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PerMonthRanking> mc = md.GetCollection<PerMonthRanking>(permonthrankingtablename);
                iv = mc.FindOne(
                        Query.And(
                            Query.EQ("ObjID", id),
                            Query.EQ("Year", year),
                            Query.EQ("Month", month)
                        )
                    );
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static WeeklyRanking GetWeeklyRanking(String weeklyrankingtablename, String id, int year, int weekofyear)
        {
            WeeklyRanking iv = new WeeklyRanking();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WeeklyRanking> mc = md.GetCollection<WeeklyRanking>(weeklyrankingtablename);
                iv = mc.FindOne(
                        Query.And(
                            Query.EQ("ObjID", id),
                            Query.EQ("Year", year),
                            Query.EQ("WeekOfYear", weekofyear)
                        )
                    );
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static DailyRanking GetDailyRanking(String dailyrankingtablename, String id, int year, int dayofyear)
        {
            DailyRanking iv = new DailyRanking();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<DailyRanking> mc = md.GetCollection<DailyRanking>(dailyrankingtablename);
                iv = mc.FindOne(
                        Query.And(
                            Query.EQ("ObjID", id),
                            Query.EQ("Year", year),
                            Query.EQ("DayOfYear", dayofyear)
                        )
                    );
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }

        /// <summary>
        /// 保存Ranking对象
        /// </summary>
        /// <param name="yearrankingtablename">表名</param>
        /// <param name="id">对象id</param>
        /// <param name="year">年度</param>
        /// <param name="scoresIncrease">分值增长</param>
        /// <returns></returns>
        private static CBB.ExceptionHelper.OperationResult UpdateEachYearRanking(String yearrankingtablename, String id, int year, int scoresIncrease)
        {
            try
            {
                EachYearRanking obj = GetEachYearRanking(yearrankingtablename, id, year);
                //该时段没有分值记录
                if (obj == null)
                {
                    obj = new EachYearRanking();
                    obj.ObjID = id;
                    obj.Year = year;
                }

                obj.Scores += scoresIncrease;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<EachYearRanking> mc = md.GetCollection<EachYearRanking>(yearrankingtablename);
                mc.Save(obj);

                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static CBB.ExceptionHelper.OperationResult UpdatePerMonthRanking(String permonthrankingtablename, String id, int year, int month, int scoresIncrease)
        {
            try
            {
                PerMonthRanking obj = GetPerMonthRanking(permonthrankingtablename, id, year, month);
                //该时段没有分值记录
                if (obj == null)
                {
                    obj = new PerMonthRanking();
                    obj.ObjID = id;
                    obj.Year = year;
                    obj.Month = month;
                }

                obj.Scores += scoresIncrease;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PerMonthRanking> mc = md.GetCollection<PerMonthRanking>(permonthrankingtablename);
                mc.Save(obj);

                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static CBB.ExceptionHelper.OperationResult UpdateWeeklyRanking(String weeklyrankingtablename, String id, int year, int weekofyear, int scoresIncrease)
        {
            try
            {
                WeeklyRanking obj = GetWeeklyRanking(weeklyrankingtablename, id, year, weekofyear);
                //该时段没有分值记录
                if (obj == null)
                {
                    obj = new WeeklyRanking();
                    obj.ObjID = id;
                    obj.Year = year;
                    obj.WeekOfYear = weekofyear;
                }

                obj.Scores += scoresIncrease;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WeeklyRanking> mc = md.GetCollection<WeeklyRanking>(weeklyrankingtablename);
                mc.Save(obj);

                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static CBB.ExceptionHelper.OperationResult UpdateDailyRanking(String dailyrankingtablename, String id, int year, int dayofyear, int scoresIncrease)
        {
            try
            {
                DailyRanking obj = GetDailyRanking(dailyrankingtablename, id, year, dayofyear);
                //该时段没有分值记录
                if (obj == null)
                {
                    obj = new DailyRanking();
                    obj.ObjID = id;
                    obj.Year = year;
                    obj.DayOfYear = dayofyear;
                }

                obj.Scores += scoresIncrease;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<DailyRanking> mc = md.GetCollection<DailyRanking>(dailyrankingtablename);
                mc.Save(obj);

                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion

        //获取日期是当年的第几周
        public static int DatePart(System.DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);//今天星期几
            int daydiff = (-1) * (weeknow + 1);//今日与上周末的天数差
            int days = dt.AddDays(daydiff).DayOfYear;//上周末是本年第几天
            int weeks = days / 7;
            if (days % 7 != 0)
            {
                weeks++;
            }
            //此时，weeks为上周是本年的第几周
            return (weeks + 1);
        }
        /// <summary>
        /// 求某年有多少周
        /// 返回 int
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns>int</returns>
        public static int GetYearWeekCount(int strYear)
        {
            System.DateTime fDt = DateTime.Parse(strYear.ToString() + "-01-01");
            int k = Convert.ToInt32(fDt.DayOfWeek);//得到该年的第一天是周几 
            if (k == 1)
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;

            }
            else
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }

        }
        //获取某年有多少天
        public static int GetYearDaysCount(int year)
        {
            DateTime dt1 = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime dt2 = new DateTime(DateTime.Now.Year + 1, 1, 1);
            return (dt2 - dt1).Days;
        }
    }
}
