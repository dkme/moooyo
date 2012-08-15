/*************************************************
 * Functional description ：唯一编号数据实体类
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
    /// 唯一编号的数据实体
    /// </summary>
    public class UniqueNumber
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID
        {
            get { if (_id != null)return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 默认编号
        /// </summary>
        public String DefaultId {
            get { return this.defaultId; }
            set { this.defaultId = value; }
        }
        private String defaultId;
        /// <summary>
        /// 转化后的编号
        /// </summary>
        public long ConvertedID {
            get { return this.convertedID; }
            set { this.convertedID = value; }
        }
        private long convertedID;
        /// <summary>
        /// 域名编号
        /// </summary>
        public String DomainNameID
        {
            get { return this.domainNameID; }
            set { this.domainNameID = value; }
        }
        private String domainNameID;
        /// <summary>
        /// 编号的类型
        /// </summary>
        public IDType IDType
        {
            get { return this.idType; }
            set { this.idType = value; }
        }
        private IDType idType;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 表（集合）名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName() {
            return "UniqueNumber";
        }

        public UniqueNumber() 
        { 
        }
        public UniqueNumber(String memberId)
        {
            try
            {
                Member.Member obj = new Member.Member();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>(Member.Member.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(memberId)));
                this.DefaultId = obj.UniqueNumber.DefaultId;
                this.ConvertedID = obj.UniqueNumber.ConvertedID;
                this.DomainNameID = obj.UniqueNumber.DomainNameID;
                this.IDType = obj.UniqueNumber.IDType;
                this.CreatedTime = obj.UniqueNumber.CreatedTime;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public UniqueNumber(Member.Member member)
        {
            try
            {
                this.DefaultId = member.UniqueNumber.DefaultId;
                this.ConvertedID = member.UniqueNumber.ConvertedID;
                this.DomainNameID = member.UniqueNumber.DomainNameID;
                this.IDType = member.UniqueNumber.IDType;
                this.CreatedTime = member.UniqueNumber.CreatedTime;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public UniqueNumber(String unId, String defaultId)
        {
            try
            {
                UniqueNumber obj = new UniqueNumber();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<UniqueNumber> mc = md.GetCollection<UniqueNumber>(UniqueNumber.GetCollectionName());
                obj = mc.FindOne(Query.And(Query.EQ("_id", ObjectId.Parse(unId)), Query.EQ("DefaultId", defaultId)));
                this._id = obj._id;
                this.DefaultId = obj.DefaultId;
                this.ConvertedID = obj.ConvertedID;
                this.DomainNameID = obj.DomainNameID;
                this.IDType = obj.IDType;
                this.CreatedTime = obj.CreatedTime;
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
