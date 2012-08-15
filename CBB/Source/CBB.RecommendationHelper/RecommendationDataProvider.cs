using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace CBB.RecommendationHelper
{
    /// <summary>
    /// 推荐数据操作类
    /// </summary>
    public class RecommendationDataProvider
    {
        /// <summary>
        /// 获取单笔用户推荐数据
        /// </summary>
        public static T GetRecommendationData<T>(T tobj,String memberId, String objectID) where T : RecommendationData
        {
            T obj;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                obj = mc.FindOne(Query.And(Query.EQ("MemberID", memberId), Query.EQ("ObjectID", objectID)));
                return obj;
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
        /// 获取用户推荐数据
        /// </summary>
        public static IList<T> GetMyRecommendationDatas<T>(T tobj,String memberId,int count) where T : RecommendationData
        {
            try
            {
                MongoCursor<T> mc = MongoDBHelper.GetCursor<T>(
                tobj.GetCollectionName(),
                Query.And(Query.EQ("MemberID", memberId),Query.GTE("Value",3)),
                new SortByDocument("Value", -1),
                0,
                0);

                List<T> objs = new List<T>();
                objs.AddRange(mc);

                long tick = DateTime.Now.Ticks;
                Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)); 
                int rndint = rnd.Next(0,objs.Count);
                IEnumerable<T> rndlist = objs.OrderBy(x => rndint).Take(count);
                List<T> randomobjs = rndlist.ToList();
                objs = randomobjs;

                return objs;
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
        /// 保存推荐数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult SaveRecommendationDatas<T>(T tobj,IList<T> objs) where T : RecommendationData
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());

                foreach (T obj in objs)
                {
                    //去除值为float.NaN的对象
                    if (obj.Value==obj.Value)
                        mc.Save(obj);
                }

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
        /// <summary>
        /// 移除用户推荐数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult RemoveRecommendationDatas<T>(T tobj,String memberId) where T :RecommendationData
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                mc.Remove(Query.EQ("MemberID", memberId));

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
    }
}
