using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;


namespace Moooyo.BiZ.Gift
{
    /// <summary>
    /// 礼品服务类
    /// </summary>
    public class GiftProvider
    {
        public static GiftDefs GetGiftDefs(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<GiftDefs> mc = md.GetCollection<GiftDefs>("GiftDefs");
                return mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
    }
}
