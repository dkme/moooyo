using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.SystemMsg
{
    //系统消息操作类
    public class SystemMsgProvider
    {
        public static CBB.ExceptionHelper.OperationResult MsgToMember(String toMember, String comment)
        {
            SystemMsg m = new SystemMsg();
            m.FromMember = "";
            m.ToMember = toMember;
            m.Readed = false;
            m.Comment = comment;
            m.CreatedTime = DateTime.Now;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<SystemMsg> mc = md.GetCollection<SystemMsg>("SystemMsg");
                mc.Insert(m);

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
        public static IList<SystemMsg> GetMsgs(String mid, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<SystemMsg> mcvistor = MongoDBHelper.GetCursor<SystemMsg>(
                    "SystemMsg",
                    Query.EQ("ToMember", mid),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<SystemMsg> objs = new List<SystemMsg>();
                objs.AddRange(mcvistor);
                
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
        public static int GetMsgCount(String mid)
        {
            try
            {
                IMongoQuery qc =
                 Query.EQ("ToMember", mid);

                long count = MongoDBHelper.GetCount(
                    "SystemMsg",
                    qc);

                return (int)count;
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
