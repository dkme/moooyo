using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.MemberContent
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberContentFactory
    {
        public static IList<MemberContent> GetMemberContents(String type, bool isAudited,int pagesize, int pageno)
        {
            try
            {
                List<MemberContent> objs = new List<MemberContent>();
                objs = getMemberContents(type, isAudited,pagesize, pageno);
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
        public static long GetAllMemberContentCount(String type, bool isAudited)
        {
            try
            {
                QueryComplete qc = null;
                if (type == "jb")//举报
                    qc = Query.LT("type", 20);
                if (type == "jy")//小编内容
                    qc = Query.EQ("type", 21);
                if (type == "yj")//意见建议bug
                    qc = Query.EQ("type", 22);

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mcoll = md.GetCollection<MemberContent>("MemberContent");
                long mc = mcoll.Count(Query.And(qc, Query.EQ("IsAudited", isAudited)));
                return mc;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static IList<MemberContent> GetAllMemberContents()
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mcoll = md.GetCollection<MemberContent>("MemberContent");
                MongoCursor<MemberContent> mc = mcoll.FindAll();

                List<MemberContent> objs = new List<MemberContent>();
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
        private static List<MemberContent> getMemberContents(String type, bool isAudited,int pagesize, int pageno)
        {
            BsonArray bv = new BsonArray();

            QueryComplete qc=null;
            if (type=="jb") //举报
                    qc = Query.LT("type", 20);
            if (type=="jy") //小编内容
                    qc = Query.EQ("type",21);
            if (type == "yj")//意见、建议、bug
                qc = Query.EQ("type", 22);
            MongoCursor<MemberContent> mc = MongoDBHelper.GetCursor<MemberContent>(
                    "MemberContent",
                    Query.And(qc, Query.EQ("IsAudited", isAudited)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

            List<MemberContent> objs = new List<MemberContent>();
            objs.AddRange(mc);
            return objs;
        }       

        /// <summary>
        /// 分页查询某个人提交的建议、意见等
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="userid">用户id</param>
        /// <param name="pagesize"></param>
        /// <param name="pageno"></param>
        /// <returns></returns>
        public static List<MemberContent> getMemberContentsByuserId(int type, string userid, int pagesize, int pageno)
        {
            BsonArray bv = new BsonArray();

            QueryComplete qc = Query.EQ("type", type);
            MongoCursor<MemberContent> mc = MongoDBHelper.GetCursor<MemberContent>(
                    "MemberContent",
                    Query.And(qc, Query.EQ("Writter",userid)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);
            List<MemberContent> objs = new List<MemberContent>();
            objs.AddRange(mc);
            return objs;
        }

        /// <summary>
        /// 查询某个人提交的建议、意见总数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static long getMemberContentsByuserIdCount(int type,string userid) 
        {
            try
            {
                QueryComplete qc = Query.EQ("type", type);
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mcoll = md.GetCollection<MemberContent>("MemberContent");
                long mc = mcoll.Count(Query.And(qc, Query.EQ("Writter", userid)));
                return mc;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }   
        }

        public static MemberContent GetMemberContent(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>("MemberContent");
                MemberContent sw = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
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


        /// <summary>
        /// 添加建议意见等
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="mid"></param>
        /// <param name="content">内容</param>
        /// <param name="witter"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddMemberContent(int type,String mid, String content,String witter)
        {
            MemberContent membercontent = new MemberContent();
            membercontent.type = (MemberContentType)type;
            membercontent.Writter = witter;
            membercontent.Content = content;
            membercontent.MemberID = mid;
            membercontent.Result = "";
            membercontent.IsAudited = false;
            membercontent.CreatedTime = DateTime.Now;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>("MemberContent");

                mc.Save(membercontent);
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
        public static CBB.ExceptionHelper.OperationResult UpdateMemberContent(String id, bool isaudited, String result)
        {
            try
            {
                MemberContent obj = GetMemberContent(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.IsAudited = isaudited;
                obj.Result = result;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>("MemberContent");
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
        public static CBB.ExceptionHelper.OperationResult DelMemberContent(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>("MemberContent");

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
