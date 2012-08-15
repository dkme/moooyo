using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Like
{
    public class LikeDataFactory
    {
        #region 用户喜欢
        
        /// <summary>
        /// 添加喜欢基础数据
        /// </summary>
        /// <param name="fromid">被喜欢的id</param>
        /// <param name="toid">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddLikeData(String fromid, String toid, LikeType type)
        {
            try
            {
                LikeData obj = new LikeData();
                obj.FromId = fromid;
                obj.ToId = toid;
                obj.LikeType = type;
                obj.CreatedTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<LikeData> mc = md.GetCollection<LikeData>(LikeData.GetCollectionName());
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

        #region 管理员喜欢
        /// <summary>
        /// 添加管理员喜欢基础数据
        /// </summary>
        /// <param name="fromid">被喜欢的id</param>
        /// <param name="toid">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddAdminLikeData(String fromid, String toid, LikeType type)
        {
            try
            {
                AdminLikeData obj = new AdminLikeData();
                obj.FromId = fromid;
                obj.ToId = toid;
                obj.LikeType = type;
                obj.CreatedTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<AdminLikeData> mc = md.GetCollection<AdminLikeData>(AdminLikeData.GetCollectionName());
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

        #region 用户和管理员喜欢通用
        /// <summary>
        /// 添加用户或管理员喜欢基础数据
        /// </summary>
        /// <typeparam name="T">喜欢实体对象</typeparam>
        /// <param name="fromId">被喜欢的id</param>
        /// <param name="toId">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <param name="tableName">表名</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddMemberOrAdminData<T>(String fromId, String toId, LikeType type, String collectionName) where T : LikeDataEntity, new()
        {
            T obj = new T();
            obj.FromId = fromId;
            obj.ToId = toId;
            obj.LikeType = type;
            obj.CreatedTime = DateTime.Now;
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<T> mc = md.GetCollection<T>(collectionName);
            mc.Save(obj);
            return new CBB.ExceptionHelper.OperationResult(true);

        }
        /// <summary>
        /// 查看是否已经喜欢
        /// </summary>
        /// <param name="fromid">被喜欢的id</param>
        /// <param name="toid">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <param name="collectionName">表名</param>
        /// <returns>是否已经喜欢</returns>
        public static Boolean IfLiked<T>(String fromid, String toid, LikeType type, String collectionName)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(collectionName);
                T obj = mc.FindOne(Query.And(Query.EQ("FromId", fromid), Query.EQ("ToId", toid), Query.EQ("LikeType", type)));
                if (obj != null) { return true; }
                else { return false; }
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
        /// 获取喜欢
        /// </summary>
        /// <typeparam name="T">喜欢实体对象</typeparam>
        /// <param name="fromid">被喜欢的id</param>
        /// <param name="toid">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <param name="collectionName">表名</param>
        /// <returns>喜欢对象列表</returns>
        public static IList<T> GetLike<T>(String fromid, String toid, LikeType type, String collectionName)
        {
            QueryComplete qc = null;
            if ((fromid == null || fromid == "") && (toid != null && toid != "")) qc = Query.And(Query.EQ("ToId", toid), Query.EQ("LikeType", type));
            else if ((fromid == null || fromid == "") && (toid == null || toid == "")) qc = Query.And(Query.EQ("LikeType", type));
            else if ((fromid != null && fromid != "") && (toid == null || toid == "")) qc = Query.And(Query.EQ("FromId", fromid), Query.EQ("LikeType", type));
            else qc = Query.And(Query.EQ("FromId", fromid), Query.EQ("ToId", toid), Query.EQ("LikeType", type));

            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<T> mgColect = mgDb.GetCollection<T>(collectionName);
            MongoCursor<T> mgCur = mgColect.Find(qc);

            List<T> objs = new List<T>();
            objs.AddRange(mgCur);
            return objs;
        }
        /// <summary>
        /// 删除喜欢基础数据
        /// </summary>
        /// <param name="fromid">被喜欢的id</param>
        /// <param name="toid">喜欢的id</param>
        /// <param name="type">喜欢的类别</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DeleteLikeData<T>(String fromid, String toid, LikeType type, String collectionName)
        {
            QueryComplete qc = null;
            try
            {
                if (toid == "" || toid == null) qc = Query.And(Query.EQ("FromId", fromid), Query.EQ("LikeType", type));
                if ((fromid != "" && fromid != null) && (toid != "" && toid != null))
                    qc = Query.And(Query.EQ("FromId", fromid), Query.EQ("ToId", toid), Query.EQ("LikeType", type));

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(collectionName);
                mc.Remove(qc);

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
    }
}
