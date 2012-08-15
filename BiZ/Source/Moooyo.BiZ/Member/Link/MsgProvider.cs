using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Member.Link
{
    /// <summary>
    /// 聊天提供类
    /// </summary>
    public class MsgProvider
    {
        public static CBB.ExceptionHelper.OperationResult MsgToMember(String fromMember, String toMember,String comment,Activity.ActivityType type)
        {
            Msg m = new Msg();
            m.FromMember = fromMember;
            m.ToMember = toMember;
            m.TalkType = type;
            m.Readed = false;
            m.Comment = comment;
            m.CreatedTime = DateTime.Now;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Msg> mc = md.GetCollection<Msg>("Msg");
                mc.Insert(m);

                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(comment, Msg.GetCollectionName(), m.ID, "Comment", m.FromMember);

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
        public static IList<Msg> GetMsgs(String me,String you, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Msg> mcvistor = MongoDBHelper.GetCursor<Msg>(
                    "Msg",
                    Query.Or(
                    Query.And(Query.EQ("FromMember", me), Query.EQ("ToMember", you)),
                    Query.And(Query.EQ("FromMember", you), Query.EQ("ToMember", me))),
                    new SortByDocument("CreatedTime",-1),
                    pageno,
                    pagesize);

                List<Msg> objs = new List<Msg>();
                objs.AddRange(mcvistor);
                //objs.Sort(delegate(Msg small, Msg big) { return small.CreatedTime.CompareTo(big.CreatedTime); });
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
        public static int GetMsgCount(String me, String you)
        {
            try
            {
                IMongoQuery qc = 
                    Query.Or(
                        Query.And(Query.EQ("FromMember", you), Query.EQ("ToMember", me)),
                        Query.And(Query.EQ("FromMember", me), Query.EQ("ToMember", you))
                    );

                long count = MongoDBHelper.GetCount(
                    "Msg",
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
