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
    /// 训练数据操作类
    /// </summary>
    public class TrainingDataProvider<T>
        where T : TrainingData
    {
        /// <summary>
        /// 获取训练数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static IList GetTrainingDatas(T tobj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mcoll = md.GetCollection<T>(tobj.GetCollectionName());
                MongoCursor<T> mc = mcoll.FindAll();

                List<T> objs = new List<T>();
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
        public static T GetTrainingData(T tobj, String memberId, String objectID)
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
        /// 获取用户训练数据
        /// </summary>
        public static IList<T> GetMyTrainingDatas(T tobj, String memberId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                MongoCursor<T> listmc = mc.Find(
                    Query.And(
                    Query.EQ("MemberID", memberId
                    )));
                List<T> objs = new List<T>();
                objs.AddRange(listmc);
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
        /// 获取用户训练数据
        /// </summary>
        public static IList<T> GetMyTrainingDatas(T tobj, String[] memberIds)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                MongoCursor<T> listmc = mc.Find(Query.And(Query.In("MemberID", new BsonArray(memberIds))));
                List<T> objs = new List<T>();
                objs.AddRange(listmc);
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
        /// 获取至少喜欢（Value>=keyValue）其中一种对象ID的用户训练数据集合
        /// </summary>
        /// <param name="collectionName">训练数据表名</param>
        /// <param name="keyValue">喜好阀值</param>
        /// <param name="objectIDs">对象ID数组</param>
        /// <returns>训练数据集合</returns>
        public static IList<T> GetTrainingDatasWhenMemberHadOneObjectID(T tobj, String[] objectIDs, float keyValue)
        {
            try
            {
                //获取对该对象有喜好的用户
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                MongoCursor<T> listmc = mc.Find(
                    Query.And(
                    Query.In("ObjectID", new BsonArray(objectIDs)),
                    Query.GTE("Value", keyValue),
                    tobj.AddicitonalQuery()
                    ));
                List<T> objs = new List<T>();
                objs.AddRange(listmc);

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
        /// 获取用户喜好的其他对象
        /// </summary>
        /// <param name="tobj"></param>
        /// <param name="memberIDs"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static IList<T> GetTrainingDatasMembersLikeElse(T tobj, String[] memberIDs, float keyValue)
        {
            try
            {
                //获取用户喜好的其他对象
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(tobj.GetCollectionName());
                MongoCursor<T> listmcelse = mc.Find(
                   Query.And(
                   Query.In("MemberID", new BsonArray(memberIDs)),
                   Query.GTE("Value", keyValue),
                   tobj.AddicitonalQuery()
                   ));
                List<T> objelse = new List<T>();
                objelse.AddRange(listmcelse);
                return objelse;
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
        public static CBB.ExceptionHelper.OperationResult SaveTrainingData(T obj)
        {
            try
            {
                if (!CBB.MongoDB.Utils.CheckObjectID(obj.MemberID) || !CBB.MongoDB.Utils.CheckObjectID(obj.ObjectID))
                    new CBB.ExceptionHelper.OperationResult(false, "参数不正确");

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(obj.GetCollectionName());
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
        /// <summary>
        /// 批量用户喜好训练数据排序
        /// （1、排序单个用户训练数据，得出指定个数的喜好排序结果）
        /// （2、排序多个用户的训练数据，得出多个用户的喜好数据排序结果）
        /// </summary>
        /// <param name="trainingDatas">训练数据集合</param>
        /// <param name="count">排序靠前的对象ID个数</param>
        /// <returns>喜好ID数组</returns>
        public static String[] SortTrainingDatas(IList<T> trainingDatas, int count)
        {
            //分值累加
            Dictionary<String, float> scores = new Dictionary<string, float>();
            foreach (TrainingData itd in trainingDatas)
            {
                if (scores.ContainsKey(itd.ObjectID)) scores[itd.ObjectID] = scores[itd.ObjectID] + itd.Value;
                else
                    scores.Add(itd.ObjectID, itd.Value);
            }
            //排序
            IEnumerable<KeyValuePair<String, float>> result = (from score in scores orderby score.Value descending select score).Take(count);
            String[] ids = new String[result.Count<KeyValuePair<String, float>>()];
            int i = 0;
            foreach (KeyValuePair<String, float> score in result)
            {
                ids[i] = score.Key;
                i++;
            }

            return ids;
        }
        /// <summary>
        /// 获取具有相同喜好ID的用户
        /// </summary>
        /// <typeparam name="T">训练数据类型</typeparam>
        /// <param name="collectionName">训练数据表名</param>
        /// <param name="objectIDs">喜好ID数组</param>
        /// <param name="count">排序靠前的对象ID个数</param>
        /// <returns>字典集合：用户ID（相同对象ID，喜好分值）</returns>
        public static Dictionary<String, Dictionary<String, float>> GetMemberIDsWhoHadSameObjectID(T tobj, String[] objectIDs, int count)
        {
            //结果数据
            Dictionary<String, Dictionary<String, float>> likes = new Dictionary<String, Dictionary<String, float>>();
            //通过对象ID获取对其中某一对象有喜好（Value>3）的用户
            IList<T> trainingDatasWhenMemberHadOneObjectID = GetTrainingDatasWhenMemberHadOneObjectID(tobj, objectIDs, 3);

            foreach (T obj in trainingDatasWhenMemberHadOneObjectID)
            {
                if (!likes.Keys.Contains<string>(obj.MemberID))
                {
                    Dictionary<string, float> objvalue = new Dictionary<string, float>();
                    objvalue.Add(obj.ObjectID, obj.Value);
                    likes.Add(obj.MemberID, objvalue);
                }
                else
                {
                    Dictionary<string, float> objvalue = likes[obj.MemberID];
                    objvalue.Add(obj.ObjectID, obj.Value);
                }
            }

            //按相同喜好的个数排序
            var result =
                (from like in likes orderby like.Value.Keys.Count descending select like).Take(count);

            Dictionary<String, Dictionary<String, float>> dicts = new Dictionary<string, Dictionary<string, float>>();
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in result)
            {
                dicts.Add(keyp.Key, keyp.Value);
            }

            return dicts;
        }
        /// <summary>
        /// 获取喜欢objectid的用户们还喜欢的对象
        /// </summary>
        /// <typeparam name="T">训练数据类型</typeparam>
        /// <param name="collectionName">训练数据集合名</param>
        /// <param name="objectid">对象ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>字典集合：对象ID（喜好用户ID，喜好分值）</returns>
        public static Dictionary<String, Dictionary<String, float>> GetObjectIDsWhenThemBeenLikeWithThis(T tobj, String objectid, int count)
        {
            //结果数据
            Dictionary<String, Dictionary<String, float>> likes = new Dictionary<String, Dictionary<String, float>>();
            //通过对象ID获取对该对象有喜好（Value>5）的用户
            IList<T> trainingDatasWhenMemberHadOneObjectID = GetTrainingDatasWhenMemberHadOneObjectID(tobj, new String[] { objectid }, 3);

            List<String> memberids = new List<String>();
            foreach (T obj in trainingDatasWhenMemberHadOneObjectID)
            {
                memberids.Add(obj.MemberID);
            }

            IList<T> trainingDatasMemberLikeElse = GetTrainingDatasMembersLikeElse(tobj, memberids.ToArray(), 3);

            foreach (T obj in trainingDatasMemberLikeElse)
            {
                //去掉和种子对象相同的对象
                if (obj.ObjectID == objectid) continue;

                if (!likes.ContainsKey(obj.ObjectID))
                {
                    Dictionary<string, float> objvalue = new Dictionary<string, float>();
                    objvalue.Add(obj.MemberID, obj.Value);
                    likes.Add(obj.ObjectID, objvalue);
                }
                else
                {
                    Dictionary<string, float> objvalue = likes[obj.ObjectID];
                    objvalue.Add(obj.MemberID, obj.Value);
                }
            }

            //按相同喜好的用户个数排序
            var result =
                (from like in likes orderby like.Value.Keys.Count descending select like).Take(count);

            Dictionary<String, Dictionary<String, float>> dicts = new Dictionary<string, Dictionary<string, float>>();
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in result)
            {
                dicts.Add(keyp.Key, keyp.Value);
            }

            return dicts;
        }
        /// <summary>
        /// 获取喜欢objectid的用户们还喜欢的对象
        /// </summary>
        /// <typeparam name="T">训练数据类型</typeparam>
        /// <param name="collectionName">训练数据集合名</param>
        /// <param name="objectid">对象ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>字典集合：对象ID（喜好用户ID，喜好分值）</returns>
        public static Dictionary<String, Dictionary<String, float>> GetObjectIDsWhenThemBeenLikeWithThis(T tobj, String[] objectids, int count)
        {
            //结果数据
            Dictionary<String, Dictionary<String, float>> likes = new Dictionary<String, Dictionary<String, float>>();
            //通过对象ID获取对该对象有喜好（Value>5）的用户
            IList<T> trainingDatasWhenMemberHadOneObjectID = GetTrainingDatasWhenMemberHadOneObjectID(tobj, objectids, 3);

            List<String> memberids = new List<String>();
            foreach (T obj in trainingDatasWhenMemberHadOneObjectID)
            {
                memberids.Add(obj.MemberID);
            }

            IList<T> trainingDatasMemberLikeElse = GetTrainingDatasMembersLikeElse(tobj, memberids.ToArray(), 3);

            foreach (T obj in trainingDatasMemberLikeElse)
            {
                //去掉和种子对象相同的对象
                if (objectids.ToList().IndexOf(obj.ObjectID) >= 0) continue;

                if (!likes.ContainsKey(obj.ObjectID))
                {
                    Dictionary<string, float> objvalue = new Dictionary<string, float>();
                    objvalue.Add(obj.MemberID, obj.Value);
                    likes.Add(obj.ObjectID, objvalue);
                }
                else
                {
                    Dictionary<string, float> objvalue = likes[obj.ObjectID];
                    objvalue.Add(obj.MemberID, obj.Value);
                }
            }

            //按相同喜好的用户个数排序
            var result =
                (from like in likes orderby like.Value.Keys.Count descending select like).Take(count);

            Dictionary<String, Dictionary<String, float>> dicts = new Dictionary<string, Dictionary<string, float>>();
            foreach (KeyValuePair<String, Dictionary<String, float>> keyp in result)
            {
                dicts.Add(keyp.Key, keyp.Value);
            }

            return dicts;
        }
    }
}
