using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.Wants
{
    public class SystemWantsFactory
    {
        public static IList<SystemWants> GetSystemWants(WantType type, int count)
        {
            try
            {
                List<SystemWants> objs = new List<SystemWants>();
                //随机取count个记录
                double random = new Random(DateTime.Now.Second).NextDouble();
                objs = getwants(type, count, random, Query.GTE("Random", random));
                if (objs.Count < count)
                {
                    objs = getwants(type, count, random, Query.LTE("Random", random));
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
        public static IList<SystemWants> GetAllSystemWants()
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mcoll = md.GetCollection<SystemWants>("SystemWants");
                MongoCursor<SystemWants> mc = mcoll.FindAll();

                List<SystemWants> objs = new List<SystemWants>();
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
        private static List<SystemWants> getwants(WantType type, int count, double random, QueryConditionList qc)
        {
            MongoCursor<SystemWants> mc = MongoDBHelper.GetCursor<SystemWants>(
                    "SystemWants",
                    Query.And(Query.EQ("type", type), qc),
                    new SortByDocument("Random", 1),
                    1,
                    count);

            List<SystemWants> objs = new List<SystemWants>();
            objs.AddRange(mc);
            return objs;
        }
        public static CBB.ExceptionHelper.OperationResult AddSystemWants(int type, bool isaudited, String witter, String iWantStr, String content)
        {
            SystemWants obj = new SystemWants();
            obj.Random = new Random().NextDouble();
            obj.type = (WantType)type;
            obj.Writter = witter;
            obj.IWantStr = iWantStr;
            obj.Content = content;
            obj.IsAudited = isaudited;
            obj.UseCount = 0;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mc = md.GetCollection<SystemWants>("SystemWants");

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
        public static SystemWants GetSystemWants(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mc = md.GetCollection<SystemWants>("SystemWants");
                SystemWants sw = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
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
        public static String GetSystemWantsContentByWantsStr(String iwantstr)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mc = md.GetCollection<SystemWants>("SystemWants");
                SystemWants sw = mc.FindOne(Query.EQ("IWantStr", iwantstr));
                if (sw != null)
                    return sw.Content;
                else
                    return "";
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult UpdateSystemWants(String id, int type, bool isaudited, String witter, String iWantStr, String content)
        {
            try
            {
                SystemWants obj = GetSystemWants(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.type = (WantType)type;
                obj.Writter = witter;
                obj.IWantStr = iWantStr;
                obj.Content = content;
                obj.IsAudited = isaudited;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mc = md.GetCollection<SystemWants>("SystemWants");
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
        public static CBB.ExceptionHelper.OperationResult DelSystemWants(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemWants> mc = md.GetCollection<SystemWants>("SystemWants");

                mc.Remove(Query.EQ("_id",ObjectId.Parse(id)));

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
