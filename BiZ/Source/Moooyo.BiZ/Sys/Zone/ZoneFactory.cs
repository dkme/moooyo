using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CBB.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Moooyo.BiZ.Sys
{
    /// <summary>
    /// 省市区查询工厂类
    /// </summary>
    public class ZoneFactory
    {
        /// <summary>
        /// 获取省区列表
        /// </summary>
        /// <returns></returns>
        public static IList<String> GetProvinces()
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection zones = md.GetCollection("zones");

            IEnumerable<BsonValue> mc = zones.Distinct("Province");
            List<BsonValue> zlist = new List<BsonValue>();
            zlist.AddRange(mc);

            IList<String> zl = new List<String>();

            foreach (BsonValue bv in zlist)
            {
               zl.Add(bv.AsString);
            }

            return zl;
        }
        /// <summary>
        /// 获取省内市区列表
        /// </summary>
        /// <returns></returns>
        public static IList<String> GetCitysByProvinceName(String prov)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Zone> zones = md.GetCollection<Zone>("zones");
            IMongoQuery qc = Query.EQ("Province", prov);
            MongoCursor<Zone> mc = zones.Find(qc);
            List<Zone> zlist = new List<Zone>();
            zlist.AddRange(mc);

            IList<String> zl = new List<String>();

            foreach (Zone bv in zlist)
            {
                zl.Add(bv.City);
            }

            return zl;
        }
        /// <summary>
        /// 获取市区
        /// </summary>
        /// <returns></returns>
        public static Zone GetCityByName(String cityName)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Zone> zones = md.GetCollection<Zone>("zones");
            IMongoQuery qc = Query.EQ("City", cityName);
            Zone z = zones.FindOne(qc);
            return z;
        }
        /// <summary>
        /// 获取所有地区
        /// </summary>
        /// <returns></returns>
        public static IList<Zone> GetAllZone()
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Zone> zones = md.GetCollection<Zone>("zones");
            MongoCursor<Zone> mc = zones.FindAll();

            List<Zone> zl = new List<Zone>();
            zl.AddRange(mc);

            return zl;
        }
    }
}
