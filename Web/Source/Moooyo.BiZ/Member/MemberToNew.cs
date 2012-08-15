///
/// 功能描述：新用户的数据结构
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/7/19
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

namespace Moooyo.BiZ.Member
{
    public class MemberToNew
    {
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        private String memberID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public String MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        private String toObjectID;
        /// <summary>
        /// 对象编号
        /// </summary>
        public String ToObjectID
        {
            get { return toObjectID; }
            set { toObjectID = value; }
        }
        private MemberToNewType type;
        /// <summary>
        /// 对象类型
        /// </summary>
        public MemberToNewType Type
        {
            get { return type; }
            set { type = value; }
        }
        private Creater.Creater creater;
        /// <summary>
        /// 创建者
        /// </summary>
        public Creater.Creater Creater
        {
            get { return creater; }
            set { creater = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        public MemberToNew() { }
        public MemberToNew(String MemberID, String ToObjectID, MemberToNewType Type)
        {
            this.MemberID = MemberID;
            this.ToObjectID = ToObjectID;
            this.Type = Type;
            this.Creater = new Creater.Creater(MemberID);
            this.CreatedTime = DateTime.Now;
        }
        public MemberToNew(String MemberID)
        {
            try
            {
                MemberToNew obj = new MemberToNew();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberToNew> mc = md.GetCollection<MemberToNew>(MemberToNew.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(MemberID)));
                this._id = ObjectId.Parse(obj.ID);
                this.MemberID = obj.MemberID;
                this.ToObjectID = obj.ToObjectID;
                this.Type = obj.Type;
                this.Creater = obj.Creater;
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
        /// <summary>
        /// 获取新用户数据的表名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName()
        {
            return "MemberToNew";
        }
        /// <summary>
        /// 更新新用户
        /// </summary>
        /// <param name="memberObj">新用户对象</param>
        /// <returns></returns>
        public MemberToNew Save(MemberToNew memberObj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberToNew> mc = md.GetCollection<MemberToNew>(MemberToNew.GetCollectionName());
                mc.Save(memberObj);
                return memberObj;
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
    public enum MemberToNewType
    {
        ImageContent = 1
    }
}
