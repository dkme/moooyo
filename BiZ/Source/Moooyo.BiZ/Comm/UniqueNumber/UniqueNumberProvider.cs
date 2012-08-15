/*************************************************
 * Functional description ：唯一编号数据提供类
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/3/19 Monday  
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

namespace Moooyo.BiZ.Comm.UniqueNumber
{
    /// <summary>
    /// 唯一编号的数据业务逻辑
    /// </summary>
    public class UniqueNumberProvider
    {
        /// <summary>
        /// 转换用户编号
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="idType">编号类别</param>
        /// <returns>用户编号</returns>
        public static UniqueNumber ConvertMemberID(String memberId, IDType idType)
        {
            long convertedID;
            
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<UniqueNumber> mgColt = mgDb.GetCollection<UniqueNumber>(UniqueNumber.GetCollectionName());

                UniqueNumber uniqueNumber = new UniqueNumber();
                uniqueNumber.DefaultId = memberId;
                //自动递增编号并返回自动增加后的编号
                convertedID = Comm.IncrementIds.IncrementIdsProvider.IncrementingID(Comm.IncrementIds.IdTypes.TransformID);
                uniqueNumber.ConvertedID = convertedID; //递增convertedID字段
                uniqueNumber.DomainNameID = "";
                uniqueNumber.IDType = idType;
                uniqueNumber.CreatedTime = DateTime.Now;

                mgColt.Insert(uniqueNumber);

                return uniqueNumber;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 按转换后的编号和编号类型获取唯一编号对象
        /// </summary>
        /// <param name="memberId">转换后的编号</param>
        /// <param name="idType">编号类型</param>
        /// <returns>原始的编号</returns>
        public static UniqueNumber GetDefaultId(String convertedId, IDType idType)
        {
            long trfmID;
            if(!long.TryParse(convertedId, out trfmID)) return null;
            UniqueNumber uniqueNumber;
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<UniqueNumber> mgColt = mgDb.GetCollection<UniqueNumber>(UniqueNumber.GetCollectionName());
                uniqueNumber = mgColt.FindOne(Query.EQ("ConvertedID", trfmID));
                return uniqueNumber;
            }
            catch (System.Exception err) {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 按域名编号和编号类型获取唯一编号对象
        /// </summary>
        /// <param name="memberId">域名编号</param>
        /// <param name="idType">编号类型</param>
        /// <returns>原始的编号</returns>
        public static UniqueNumber GetDomainDefaultId(String domainNameID, IDType idType)
        {
            UniqueNumber uniqueNumber;
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<UniqueNumber> mgColt = mgDb.GetCollection<UniqueNumber>(UniqueNumber.GetCollectionName());
                uniqueNumber = mgColt.FindOne(Query.EQ("DomainNameID", domainNameID));
                return uniqueNumber;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 按原始编号和编号类型获取唯一编号对象
        /// </summary>
        /// <param name="memberId">原始编号</param>
        /// <returns>转换后的编号</returns>
        public static UniqueNumber GetConvertedMemberID(String defaultId, IDType idType)
        {
            UniqueNumber uniqueNumber;
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<UniqueNumber> mgColt = mgDb.GetCollection<UniqueNumber>(UniqueNumber.GetCollectionName());
                uniqueNumber = mgColt.FindOne(Query.EQ("DefaultId", defaultId));
                return uniqueNumber;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        /// <summary>
        /// 域名编号是否已使用
        /// </summary>
        /// <param name="domainNameID">域名编号</param>
        /// <returns>是否</returns>
        public static bool IsDomainNameIDUsed(String domainNameID, String memberId)
        {
            // 预定成功返回值
            long domainNameCount = 0;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection(UniqueNumber.GetCollectionName());
                IMongoQuery qc = Query.And(Query.EQ("DomainNameID", domainNameID), Query.NE("DefaultId", memberId));
                domainNameCount = mc.Count(qc);
                if (domainNameCount > 0) return false;
                return true;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, 
                    err);
            }
        }
    }
}
