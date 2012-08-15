/*************************************************
 * Functional description ：处理自动增加编号集合数据业务逻辑
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/3/21 Wednesday 
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Comm.IncrementIds
{
    /// <summary>
    /// 处理自动增加编号集合数据业务逻辑
    /// </summary>
    public class IncrementIdsProvider
    {
        /// <summary>
        /// 自动递增编号并返回自动增加后的编号
        /// </summary>
        /// <param name="idTypes">自动增加编号类型</param>
        /// <returns>自动增加后的编号</returns>
        public static long IncrementingID(IdTypes idTypes)
        {
            long icmetId;
            try
            {
                //initialIncrementIdValue(idTypes, 9999); //初始化自动增加编号的种子初始值

                MongoDatabase mgDB = MongoDBHelper.MongoDB;
                MongoCollection<IncrementIds> mgClect = mgDB.GetCollection<IncrementIds>(IncrementIds.GetCollectionName());
                IMongoQuery iMgQury = Query.EQ("IdTypes", idTypes);
                IMongoSortBy iMgStBy = SortBy.Descending(new string[] { "IcmetId" }); //按字段降序
                IMongoUpdate iMgUpdt = Update.Inc("IcmetId", 1); //对指定字段增减数值操作
                //因为findAndModify是一个方法完成更新查找两个操作，所以具有原子性，多线程不会冲突。
                FindAndModifyResult result = mgClect.FindAndModify(iMgQury, iMgStBy, iMgUpdt);
                icmetId = GetIncrementedID(idTypes); //获取自动增加后的编号
                return icmetId;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 获取自动增加后的编号
        /// </summary>
        /// <param name="idTypes">自动增加编号类型</param>
        /// <returns>自动增加后的编号</returns>
        public static long GetIncrementedID(IdTypes idTypes)
        {
            long icmetId;
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<IncrementIds> mgColt;
                MongoCursor<IncrementIds> mgCurs;
                List<IncrementIds> listIcmetID = new List<IncrementIds>();
                IncrementIds icmetIDs = new IncrementIds();

                mgColt = mgDb.GetCollection<IncrementIds>(IncrementIds.GetCollectionName());
                mgCurs = mgColt.FindAll().SetSortOrder(new SortByDocument("IcmetId", -1)).SetLimit(1); //按字段降序取一条记录
                listIcmetID.AddRange(mgCurs);
                if (listIcmetID.Count == 1)
                {
                    icmetIDs = listIcmetID[0];
                    icmetId = icmetIDs.IcmetId;
                }
                else icmetId = -1;

                return icmetId;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 初始化自动增加编号的种子初始值
        /// </summary>
        /// <param name="idTypes">自动增加编号类型</param>
        /// <param name="incrementIdValue">递增种子值</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult initialIncrementIdValue(IdTypes idTypes, long incrementIdValue)
        {
            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<IncrementIds> mgColt = mgDb.GetCollection<IncrementIds>(IncrementIds.GetCollectionName());
            IncrementIds icmetIds = new IncrementIds();
            icmetIds.IdTypes = idTypes;
            icmetIds.IcmetId = incrementIdValue;
            mgColt.Insert(icmetIds);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
    }
}
