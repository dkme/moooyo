using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemInterViewFactory
    {
        public static IList<SystemInterView> GetSystemInterView(InterViewType type, int count,String[] exceptobjs)
        {
            try
            {
                List<SystemInterView> objs = new List<SystemInterView>();
                //随机取count个记录
                double random = new Random(DateTime.Now.Second).NextDouble();
                objs = getinterview(InterViewType.m_normal, count, exceptobjs, random, Query.GTE("Random", random));
                if (objs.Count < count)
                {
                    objs = getinterview(InterViewType.m_normal, count, exceptobjs, random, Query.LTE("Random", random));
                }
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
        public static IList<SystemInterView> GetAllSystemInterView()
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemInterView> mcoll = md.GetCollection<SystemInterView>("SystemInterview");
                MongoCursor<SystemInterView> mc = mcoll.FindAll();

                List<SystemInterView> objs = new List<SystemInterView>();
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
        private static List<SystemInterView> getinterview(InterViewType type, int count, String[] exceptobjs, double random, QueryConditionList qc)
        {
            BsonArray bv = new BsonArray();
            foreach(string s in exceptobjs)
            {
                bv.Add(ObjectId.Parse(s));
            }

            MongoCursor<SystemInterView> mc = MongoDBHelper.GetCursor<SystemInterView>(
                    "SystemInterview",
                    Query.And(Query.EQ("type", type), qc, Query.NotIn("_id", bv)),
                    new SortByDocument("Random", 1),
                    1,
                    count);

            List<SystemInterView> objs = new List<SystemInterView>();
            objs.AddRange(mc);
            return objs;
        }
        public static SystemInterView GetSystemInterView(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemInterView> mc = md.GetCollection<SystemInterView>("SystemInterview");
                SystemInterView sw = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                return sw;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult AddSystemInterview(int type, bool isaudited, String witter, String question, String answer)
        {
            SystemInterView interview = new SystemInterView();
            interview.Random = new Random().NextDouble();
            interview.type = (InterViewType)type;
            interview.Writter = witter;
            interview.Question = question;
            interview.Answer = answer;
            interview.IsAudited = isaudited;
            interview.UseCount = 0;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemInterView> mc = md.GetCollection<SystemInterView>("SystemInterview");

                mc.Save(interview);
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
        public static CBB.ExceptionHelper.OperationResult UpdateSystemInterview(String id, bool isaudited, String witter, String question, String answer)
        {
            try
            {
                SystemInterView obj = GetSystemInterView(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.IsAudited = isaudited;
                obj.Writter = witter;
                obj.Question = question;
                obj.Answer = answer;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemInterView> mc = md.GetCollection<SystemInterView>("SystemInterview");
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
        public static CBB.ExceptionHelper.OperationResult DelSystemInterview(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemInterView> mc = md.GetCollection<SystemInterView>("SystemInterview");

                mc.Remove(Query.EQ("_id", ObjectId.Parse(id)));

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
