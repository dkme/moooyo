using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Guide
{
    public class GuideDataProvider
    {
        //生成已完成引导数据记录
        //已完成返回true，未完成返回false
        public static bool CheckGuide(GuideData guideData)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Guide.GuideData> mc = md.GetCollection<BiZ.Guide.GuideData>("GuideData");
            IMongoQuery qc = Query.And(Query.EQ("MemberID", guideData.MemberID), Query.EQ("GuideID", guideData.GuideID));
            long has = mc.Count(qc);

            if (has <= 0)
            {
                mc.Save(guideData);
                return false;
            }

            return true;
        }
    }
}
