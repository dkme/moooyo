using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.SystemManager
{
    public class SystemManagerProvider
    {
        private static List<SystemManager> GetSystemManagers(int level, QueryConditionList qc)
        {
            MongoCursor<SystemManager> mc = MongoDBHelper.GetCursor<SystemManager>(
                    "SystemManager",
                    Query.EQ("Level", level),
                    new SortByDocument("_id", 1),
                    0,
                    0);

            List<SystemManager> objs = new List<SystemManager>();
            objs.AddRange(mc);
            return objs;
        }

        public static CBB.ExceptionHelper.OperationResult AddSystemManager(String name, String pwd,int level,bool allowlogin)
        {
            SystemManager obj = new SystemManager();
            obj.Name = name;
            obj.Password = CBB.Security.MD5Helper.getMd5Hash(pwd);
            obj.Level = level;
            obj.AllowLogin = allowlogin;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemManager> mc = md.GetCollection<SystemManager>("SystemManager");

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
        public static CBB.ExceptionHelper.OperationResult UpdateSystemManager(String id, String name, String pwd, int level, bool allowlogin)
        {
            try
            {
                SystemManager obj = GetSystemManager(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");
                obj.Name = name;
                obj.Password = CBB.Security.MD5Helper.getMd5Hash(pwd);
                obj.Level = level;
                obj.AllowLogin = allowlogin;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemManager> mc = md.GetCollection<SystemManager>("SystemManager");
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
        public static CBB.ExceptionHelper.OperationResult DelSystemManager(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemManager> mc = md.GetCollection<SystemManager>("SystemManager");

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

         public static SystemManager GetSystemManager(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemManager> mc = md.GetCollection<SystemManager>("SystemManager");
                SystemManager sw = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
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
         public static SystemManager Login(String name, String pwd)
         {
             try
             {
                 MongoDatabase md = MongoDBHelper.MongoDB;
                 MongoCollection<SystemManager> mc = md.GetCollection<SystemManager>("SystemManager");
                 SystemManager sw = mc.FindOne(Query.And(Query.EQ("Name", name), Query.EQ("Password", CBB.Security.MD5Helper.getMd5Hash(pwd))));
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
    }
}
