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

namespace WoXi.BiZ.Recommendation
{
    /// <summary>
    /// 训练数据操作类
    /// </summary>
    public class RecommendationData
    {
        /// <summary>
        /// 获取训练数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static IList GetTrainingDatas(String collectionName)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RecommendationTraining> mcoll = md.GetCollection<RecommendationTraining>(collectionName);
                MongoCursor<RecommendationTraining> mc = mcoll.FindAll();

                List<RecommendationTraining> objs = new List<RecommendationTraining>();
                objs.AddRange(mc);
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
        /// 获取单笔训练数据
        /// </summary>
        public static T GetTrainingData<T>(String collectionName, String memberId, String objectID) where T : RecommendationTraining
        {
            T obj;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(collectionName);
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
        /// 保存训练数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static OperationResult.OperationResult SaveTrainingData<T>(T obj) where T : RecommendationTraining
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(obj.GetCollectionName());
                mc.Save(obj);

                return new OperationResult.OperationResult(true);
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
