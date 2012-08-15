///
/// 功能描述：系统状态的数据获取
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/3/31
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys
{
    public class SystemInfo
    {
        public static List<string> times=new List<string>();
        public static List<long> GetCount(String time1, String time2, string collectionname, string ordername)
        {
            try
            {
                DateTime begintime = time1 != null && time1 != "" ? DateTime.Parse(time1 + " 00:00:00") : DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime endtime = time2 != null && time2 != "" ? DateTime.Parse(time2 + " 23:59:59") : DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection(collectionname);
                List<long> counts = new List<long>();
                #region 暂时保留 获取一段时间的系统状态列表
                //DateTime timeone = new DateTime();
                //DateTime timetwo = new DateTime();
                //times.RemoveRange(0, times.Count);
                //timeone = DateTime.Parse(begintime.Year + "-" + begintime.Month + "-" + (begintime.Day) + " 00:00:00");
                //timetwo = DateTime.Parse(begintime.Year + "-" + begintime.Month + "-" + (begintime.Day) + " 23:59:59");
                //times.Add(timeone.ToString("yyyy-MM-dd"));
                //counts.Add(mc.Count(Query.And(Query.GTE(ordername, time1), Query.LTE(ordername, time2))));
                //for (int i = 0; i < (endtime - begintime).Days; i++)
                //{
                //    if (timeone.Month == 1 || timeone.Month == 3 || timeone.Month == 5 || timeone.Month == 7 || timeone.Month == 8 || timeone.Month == 10 || timeone.Month == 12)
                //    {
                //        if (timeone.Day + 1 > 31)
                //            timeone = DateTime.Parse(timeone.Year + "-" + (timeone.Month + 1) + "-1" + " 00:00:00");
                //        else
                //            timeone = DateTime.Parse(timeone.Year + "-" + timeone.Month + "-" + (timeone.Day + 1) + " 00:00:00");
                //    }
                //    else if (timeone.Month == 4 || timeone.Month == 6 || timeone.Month == 9 || timeone.Month == 11)
                //    {
                //        if (timeone.Day + 1 > 30)
                //            timeone = DateTime.Parse(timeone.Year + "-" + (timeone.Month + 1) + "-1" + " 00:00:00");
                //        else
                //            timeone = DateTime.Parse(timeone.Year + "-" + timeone.Month + "-" + (timeone.Day + 1) + " 00:00:00");
                //    }
                //    else if (timeone.Month == 2)
                //    {
                //        if ((DateTime.IsLeapYear(timeone.Year) && timeone.Day + 1 > 29) || (!DateTime.IsLeapYear(timeone.Year) && timeone.Day + 1 > 28))
                //            timeone = DateTime.Parse(timeone.Year + "-" + (timeone.Month + 1) + "-1" + " 00:00:00");
                //        else
                //            timeone = DateTime.Parse(timeone.Year + "-" + timeone.Month + "-" + (timeone.Day + 1) + " 00:00:00");
                //    }
                //    timetwo = DateTime.Parse(timeone.Year + "-" + timeone.Month + "-" + (timeone.Day) + " 23:59:59");
                //    times.Add(timeone.ToString("yyyy-MM-dd"));
                //    counts.Add(mc.Count(Query.And(Query.GTE(ordername, timeone), Query.LTE(ordername, timetwo))));
                //}
                #endregion
                counts.Add(mc.Count(Query.And(Query.GTE(ordername, begintime), Query.LTE(ordername, endtime))));
                return counts;
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
        /// 获取当天新增的用户数
        /// </summary>
        /// <returns></returns>
        public static List<long> GetNewMember(String time1, String time2)
        {
            return GetCount(time1, time2, BiZ.Member.Member.GetCollectionName(), "CreatedTime");
        }
        /// <summary>
        /// 当天新增的兴趣数
        /// </summary>
        /// <returns></returns>
        public static List<long> GetNewInterest(String time1, String time2)
        {
            return GetCount(time1, time2, BiZ.InterestCenter.Interest.GetCollectionName(), "CreatedTime");
        }
        /// <summary>
        /// 当天新增的话题数
        /// </summary>
        /// <returns></returns>
        public static List<long> GetNewWenWen(String time1, String time2)
        {
            return GetCount(time1, time2, BiZ.WenWen.WenWen.GetCollectionName(), "CreatedTime");
        }
        /// <summary>
        /// 当天新增的回复数
        /// </summary>
        /// <returns></returns>
        public static List<long> GetNewAnswer(String time1, String time2)
        {
            return GetCount(time1, time2, BiZ.WenWen.WenWenAnswer.GetCollectionName(), "CreatedTime");
        }
    }
}
