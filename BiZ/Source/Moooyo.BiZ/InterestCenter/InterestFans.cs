using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.InterestCenter
{
    //兴趣的粉丝
    public class InterestFans : Fans.Fans
    {
        //用户在兴趣下发布的内容数量
        private long contentCount;
        public long ContentCount
        {
            get { return contentCount; }
            set { contentCount = value; }
        }
        /// <summary>
        /// 更新用户在兴趣下发布的内容数量
        /// </summary>
        /// <param name="iid">兴趣编号</param>
        /// <param name="mid">用户编号</param>
        /// <returns></returns>
        public static Boolean updateContentCount(String iid, String mid)
        {
            try
            {
                InterestFans obj = InterestFactory.GetInterestFans(iid, mid);
                if (obj.ContentCount == null)
                {
                    obj.ContentCount = 0;
                }
                obj.ContentCount = obj.ContentCount + 1;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                mc.Save(obj);
                return true;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static String GetCollectionName()
        {
            return "InterestFans";
        }
    }
}
