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
    /// Msg服务类
    /// </summary>
    public class LinkMsgProvider
    {
        /// <summary>
        /// 新消息
        /// </summary>
        /// <param name="fromMember"></param>
        /// <param name="toMember"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddMsg(String fromMember, String toMember, Activity.ActivityType type)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<LinkMsg> mc = md.GetCollection<LinkMsg>("LinkMsg");
                LinkMsg msg = new LinkMsg();
                msg.CreatedTime = DateTime.Now;
                msg.LinkMsgType = type;
                msg.Readed = false;
                msg.SendFrom = fromMember;
                msg.To = toMember;
                mc.Insert(msg);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }

        }
        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult ReadMsg(String ID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<LinkMsg> mc = md.GetCollection<LinkMsg>("LinkMsg");
                LinkMsg msg = GetMsg(ID);
                msg.ReadTime = DateTime.Now;
                msg.Readed = true;
                mc.Save(msg);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static LinkMsg GetMsg(String ID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<LinkMsg> mc = md.GetCollection<LinkMsg>("LinkMsg");
                IMongoQuery qc = Query.EQ("_id",ObjectId.Parse(ID));
                LinkMsg msg = mc.FindOne(qc);
                return msg;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
    }
}
