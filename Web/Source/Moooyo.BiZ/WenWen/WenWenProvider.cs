///
/// 功能描述：兴趣问问数据提供类
/// 作   者：刘安
/// 修改扩展者:刘安
/// 修改日期：2012/2/19
/// 附加信息：
///    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.WenWen
{
    public class WenWenProvider
    {
        #region 兴趣问问
        /// <summary>
        /// 获取问问
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        public static IList<WenWen> GetWenWens(int pn, int count)
        {
            try
            {
                Int32 page = pn != 0 ? pn : 1;
                Int32 itemCount = count != 0 ? count : 0;
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), null, new SortByDocument("_id", -1), page, itemCount);
                List<WenWen> objs = new List<WenWen>();
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
        /// 问问搜索
        /// </summary>
        /// <param name="search">搜索内容</param>
        /// <returns></returns>
        public static IList<WenWen> GetWenWens(string interestid, string search, int pagesize, int pageno)
        {
            try
            {
                QueryComplete qc = null;
                qc = Query.And(Query.EQ("InterestID", interestid), Query.Or(Query.Matches("Title", search), Query.Matches("Content", search)));
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), qc, new SortByDocument("_id", -1), pageno, pagesize);
                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
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
        /// 根据兴趣id获取问问（按更新时间排序）
        /// </summary>
        /// <param name="interestid">兴趣id</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        public static IList<WenWen> GetWenWens(string interestid, int pagesize, int pageno)
        {
            try
            {
                QueryComplete qc = Query.EQ("InterestID", interestid);
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
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
        /// 获取问问推荐
        /// </summary>
        /// <param name="count">推荐数量</param>
        /// <param name="ids">当前页面用户的兴趣id组</param>
        /// <returns></returns>
        public static IList<WenWen> GetRandomWenWen(int count, String[] ids)
        {
            try
            {
                List<String> iIds = new List<String>();
                foreach (String id in ids)
                {
                    iIds.Add(id);
                }
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), Query.In("InterestID", new BsonArray(iIds.ToArray())), new SortByDocument("_id", -1), 0, 0);
                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                if (objs.Count < count)
                {
                    return objs;
                }
                Random ran = new Random();
                int pagecount = (int)(objs.Count / count);
                int index = ran.Next(pagecount);
                List<WenWen> objlist = new List<WenWen>();
                for (int i = index * count; i < (index * count) + count; i++)
                {
                    objlist.Add(objs[i]);
                }
                return objlist;
            }
            catch (Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //获取用户兴趣问问数
        public static int GetWenWenCount(String mid)
        {
            QueryComplete qc = null;
            try
            {
                if (mid != "" && mid != null) qc = Query.EQ("Creater.MemberID", mid);

                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1));
                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                if (objs == null && objs.Count <= 0) { return 0; }
                return objs.Count;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //根据用户id获取兴趣问问
        public static IList<WenWen> GetWenWenForMember(String MemberID, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(
                    WenWen.GetCollectionName(),
                    Query.EQ("Creater.MemberID", MemberID),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }

                return wenwens;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //根据兴趣id组获取兴趣问问
        public static IList<WenWen> GetWenWenForInterests(String[] interestsids, int pagesize, int pageno)
        {
            try
            {
                IList<WenWen> wenwens = GetInterestIdArrWenWenSorted(interestsids, "_id", -1, pagesize, pageno);
                return wenwens;
            }
            catch (Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 按兴趣编号数组获取兴趣问问按喜欢数排序
        /// </summary>
        /// <param name="interestsids">兴趣编号数组</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>话题列表</returns>
        public static IList<WenWen> GetWenWenLikeCountForInterests(String[] interestsids, int pagesize, int pageno)
        {
            try
            {
                IList<WenWen> wenwens = GetInterestIdArrWenWenSorted(interestsids, "Likecount", -1, pagesize, pageno);
                return wenwens;
            }
            catch (Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 按兴趣编号数组获取兴趣问问按指定字段排序
        /// </summary>
        /// <param name="interestsids">兴趣编号数组</param>
        /// <param name="sortByField">排序字段</param>
        /// <param name="lift">升或降序</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns></returns>
        private static IList<WenWen> GetInterestIdArrWenWenSorted(String[] interestsids, String sortByField, int lift, int pagesize, int pageno)
        {
            MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), Query.In("InterestID", new BsonArray(interestsids.ToArray())), new SortByDocument(sortByField, lift), pageno, pagesize);
            List<WenWen> objs = new List<WenWen>();
            objs.AddRange(mc);
            IList<WenWen> wenwens = new List<WenWen>();
            foreach (var obj in objs)
            {
                obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                wenwens.Add(obj);
            }
            return wenwens;
        }
        /// <summary>
        /// 按兴趣话题编号数组获取兴趣问问按喜欢数排序
        /// </summary>
        /// <param name="interestsids">兴趣编号数组</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>话题列表</returns>
        public static IList<WenWen> GetTopicIdArrWenWenLikeCountSorted(String[] topicIds, int pagesize, int pageno)
        {
            try
            {
                IList<WenWen> wenwens = GetTopicIdArrWenWenSorted(topicIds, "Likecount", -1, pagesize, pageno);
                return wenwens;
            }
            catch (Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 按兴趣话题编号数组获取兴趣问问按指定字段排序
        /// </summary>
        /// <param name="interestsids">兴趣话题编号数组</param>
        /// <param name="sortByField">排序字段</param>
        /// <param name="lift">升或降序</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣话题列表</returns>
        private static IList<WenWen> GetTopicIdArrWenWenSorted(String[] topicIds, String sortByField, int lift, int pagesize, int pageno)
        {
            IList<WenWen> wenwens = new List<WenWen>();
            List<ObjectId> objTopicIds = new List<ObjectId>();
            foreach (String topicId in topicIds)
            {
                ObjectId objTopicId;
                bool parseFlag = ObjectId.TryParse(topicId, out objTopicId);
                if (parseFlag)
                    objTopicIds.Add(objTopicId);
                else
                    return wenwens;
            }

            MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), Query.In("_id", new BsonArray(objTopicIds.ToArray())), new SortByDocument(sortByField, lift), pageno, pagesize);
            List<WenWen> objs = new List<WenWen>();
            objs.AddRange(mc);
            
            foreach (var obj in objs)
            {
                obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                wenwens.Add(obj);
            }
            return wenwens;
        }
        //获取兴趣问问
        public static IList<WenWen> GetWenWen(String interestID, int pagesize, int pageno)
        {
            QueryComplete qc = null;
            try
            {
                if (interestID != "") qc = Query.EQ("InterestID", interestID);

                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(
                    WenWen.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
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
        /// 按兴趣编号和发布用户的性别获取话题
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <param name="sex">话题发布者性别</param>
        /// <returns>话题列表</returns>
        public static IList<WenWen> GetInterestIDSexTopics(String interestID, int pagesize, int pageno, int sex = 0)
        {
            QueryComplete qc = null;
            try
            {
                if (interestID != "" && sex == 0) qc = Query.EQ("InterestID", interestID);
                if (interestID != "" && sex != 0)
                {
                    qc = Query.And(Query.EQ("InterestID", interestID), Query.EQ("Creater.Sex", sex));
                }
                if (interestID == "" && sex != 0)
                {
                    qc = Query.EQ("Creater.Sex", sex);
                }

                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(
                    WenWen.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageno, pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
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
        /// 获取管理员喜欢或不喜欢的兴趣话题
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="pagesize">没页条数</param>
        /// <param name="pageno">当前页</param>
        /// <param name="likeOrNot">喜欢或不喜欢</param>
        /// <returns>话题列表</returns>
        public static IList<WenWen> GetAdminLikeOrNotTopics(String interestID, int pagesize, int pageno, Boolean likeOrNot)
        {
            QueryComplete qc = null;
            try
            {
                if (interestID != "" && interestID != null)
                {
                    if (likeOrNot) qc = Query.And(Query.EQ("InterestID", interestID), Query.GT("AdminLikeCount", 0));
                    else qc = Query.And(Query.EQ("InterestID", interestID), Query.Or(Query.LT("AdminLikeCount", 1), Query.Exists("AdminLikeCount", false)));
                }
                else
                {
                    if (likeOrNot) qc = Query.GT("AdminLikeCount", 0);
                    else qc = Query.Or(Query.LT("AdminLikeCount", 1), Query.Exists("AdminLikeCount", false));
                }
                
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(WenWen.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1),
                    pageno, pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static long GetAdminLikeOrNotTopicsCount(String interestID, Boolean likeOrNot)
        {
            QueryComplete qc = null;
            try
            {
                if (interestID != "" && interestID != null)
                {
                    if (likeOrNot) qc = Query.And(Query.EQ("InterestID", interestID), Query.GT("AdminLikeCount", 0));
                    else qc = Query.And(Query.EQ("InterestID", interestID), Query.LT("AdminLikeCount", 1), Query.Exists("AdminLikeCount", false));
                }
                else
                {
                    if (likeOrNot) qc = Query.GT("AdminLikeCount", 0);
                    else qc = Query.Or(Query.LT("AdminLikeCount", 1), Query.Exists("AdminLikeCount", false));
                }

                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mgColect = mgDb.GetCollection<WenWen>(WenWen.GetCollectionName());
                long adminLikeOrNotTopicsCount = mgColect.Count(qc);

                return adminLikeOrNotTopicsCount;
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
        /// 按兴趣编号获取排序后的兴趣问问
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <param name="sortByField">按指定字段排序</param>
        /// <param name="lift">升或降序</param>
        /// <returns>话题列表</returns>
        public static IList<WenWen> GetSortedWenWen(String interestID, int pagesize, int pageno, String sortByField, int lift)
        {
            QueryComplete qc = null;
            try
            {
                if (interestID != "") qc = Query.EQ("InterestID", interestID);

                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(
                    WenWen.GetCollectionName(),
                    qc,
                    new SortByDocument(sortByField, lift),
                    pageno,
                    pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
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
        /// 根据用户id查询用户参与的问问
        /// </summary>
        /// <param name="memberid">用户id</param>
        /// <param name="pagesize">分页条数</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        public static IList<WenWen> GetWenWenForMemberIDs(String memberid, int pagesize, int pageno)
        {
            try
            {
                IList<WenWenAnswer> answer = GetWenWenAnswerForMember(memberid, 0, 0);
                IList<ObjectId> wenwenids = new List<ObjectId>();
                foreach (var obj in answer) { wenwenids.Add(new ObjectId(obj.WenWenID)); }
                IList<ObjectId> wenwenidstwo = new List<ObjectId>();
                bool ifcontinu = false;
                foreach (ObjectId id in wenwenids)
                {
                    foreach (ObjectId idtwo in wenwenidstwo)
                    {
                        if (id == idtwo) { ifcontinu = true; break; }
                        else { ifcontinu = false; continue; }
                    }
                    if (!ifcontinu)
                    {
                        wenwenidstwo.Add(id);
                    }
                }
                MongoCursor<WenWen> mc = MongoDBHelper.GetCursor<WenWen>(
                    WenWen.GetCollectionName(),
                    Query.In("_id", new BsonArray(wenwenidstwo.ToArray())),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<WenWen> objs = new List<WenWen>();
                objs.AddRange(mc);
                IList<WenWen> wenwens = new List<WenWen>();
                foreach (var obj in objs)
                {
                    obj.AnswerCount = GetWenWenAnswer(obj.ID, 0, 0, 1).Count;
                    wenwens.Add(obj);
                }
                return wenwens;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //通过ID获取兴趣问问
        public static WenWen GetWenWen(String id)
        {
            WenWen iv = new WenWen();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
                iv = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //用户创建兴趣问问
        public static WenWen AddWenWen(String memberid, String interestid, String title, String content,String contentimage)
        {
            WenWen obj = new WenWen();
            obj.Title = title;
            obj.Content = content;
            obj.ContentImage = contentimage;
            obj.Creater = new Creater.Creater(memberid);
            obj.AnswerCount = 0;
            obj.InterestID = interestid;
            obj.CreatedTime = DateTime.Now;
            obj.UpdateTime = DateTime.Now;
            obj.UpDowner = new UpDown.UpDown(0, 0);
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
                mc.Save(obj);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, WenWen.GetCollectionName(), obj.ID, "Content", memberid);
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Title, WenWen.GetCollectionName(), obj.ID, "Title", memberid);

                //更新兴趣
                IList<WenWen> wenwens = GetWenWens(interestid, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(interestid, wenwens);

                //添加粉丝超人数据
                BiZ.InterestCenter.Interest interestobj = BiZ.InterestCenter.InterestFactory.GetInterest(interestid);
                Active.ActiveReflectionFactory.AddActiveMessage("1", interestobj.Creater.MemberID + "," + memberid);
                return obj;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }

            //文本过滤处理


        }
        //更新兴趣问问
        public static CBB.ExceptionHelper.OperationResult UpdateWenWen(String id, String title, String content, String contentimage)
        {
            try
            {
                WenWen obj = GetWenWen(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");
                obj.Title = title;
                obj.Content = content;
                obj.ContentImage = contentimage;
                obj.UpdateTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
                mc.Save(obj);

                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, WenWen.GetCollectionName(), obj.ID, "Content", obj.Creater.MemberID);
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Title, WenWen.GetCollectionName(), obj.ID, "Title", obj.Creater.MemberID);

                //更新兴趣
                IList<WenWen> wenwens = GetWenWens(obj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(obj.InterestID, wenwens);

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
        /// 更新兴趣问问被喜欢的数量
        /// </summary>
        /// <param name="id">问问编号</param>
        /// <param name="memberPlusMinusValue">用户加减值，正数为加负数为减零为不动</param>
        /// <param name="memberPlusMinusValue">管理加减值，正数为加负数为减零为不动</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateLikeCount(String topicId, int memberPlusMinusValue, int adminPlusMinusValue, String frommemberID,Boolean ifaddactivity)
        {
            try
            {
                WenWen obj = GetWenWen(topicId);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.Likecount = obj.Likecount + memberPlusMinusValue;
                obj.AdminLikeCount = obj.AdminLikeCount + adminPlusMinusValue;
                obj.UpdateTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
                mc.Save(obj);

                //更新兴趣
                IList<WenWen> wenwens = GetWenWens(obj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(obj.InterestID, wenwens);

                if (ifaddactivity)
                {
                    //增加动态
                    BiZ.Member.Activity.ActivityController.AddActivity(
                        frommemberID,
                        Member.Activity.ActivityType.Like,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_LikeTopic_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_LikeTopic(obj.ID, obj.Content),
                        false);
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
        /// 按用户编号、管理员喜欢数增减值更新所有话题管理员喜欢数
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="adminPlusMinusValue">管理员喜欢增减值</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateAllTopicsAdminLikeCount(String userId, int adminPlusMinusValue)
        {
            try
            {
                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<Like.AdminLikeData> mgLikesColect = mgDb.GetCollection<Like.AdminLikeData>(Like.AdminLikeData.GetCollectionName());
                //查出所有按用户和喜欢类型的管理员喜欢数据
                MongoCursor<Like.AdminLikeData> mgLikesCurs = mgLikesColect.Find(Query.And(Query.EQ("FromId", userId), Query.EQ("LikeType", Like.LikeType.WenWen)));
                //如果查不到管理员喜欢数据
                if (mgLikesCurs.Count() < 1) return new CBB.ExceptionHelper.OperationResult(false, "没有管理员喜欢的记录"); 

                List<ObjectId> adminLikeToIdList = new List<ObjectId>();
                foreach (Like.AdminLikeData adminLikeData in mgLikesCurs) adminLikeToIdList.Add(ObjectId.Parse(adminLikeData.ToId));

                //更新所有被喜欢编号为问问编号的问问的管理员喜欢数
                MongoCollection<WenWen> mgTopicsColect = mgDb.GetCollection<WenWen>(WenWen.GetCollectionName());
                mgTopicsColect.Update(
                    Query.In("_id", new BsonArray(adminLikeToIdList.ToArray())),
                    Update.Inc("AdminLikeCount", adminPlusMinusValue),
                    UpdateFlags.Multi, SafeMode.True
                );

                //删除所有管理员喜欢的数据
                mgLikesColect.Remove(Query.And(Query.EQ("FromId", userId), Query.EQ("LikeType", Like.LikeType.WenWen)), RemoveFlags.None, SafeMode.True);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr, CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        //删除兴趣问问
        public static CBB.ExceptionHelper.OperationResult DelWenWen(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());

                mc.Remove(Query.EQ("_id", ObjectId.Parse(id)));

                WenWen obj = GetWenWen(id);
                //更新兴趣
                IList<WenWen> wenwens = GetWenWens(obj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(obj.InterestID, wenwens);

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
        //更新问问UpDown
        public static CBB.ExceptionHelper.OperationResult UpdateWenWenUpDown(String id, bool upordown)
        {
            try
            {
                WenWen obj = GetWenWen(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.UpDowner.Update(upordown);

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
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
        //更新兴趣问问更新时间
        public static CBB.ExceptionHelper.OperationResult UpdateTime(String id)
        {
            try
            {
                WenWen obj = GetWenWen(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.UpdateTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
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
        //更新话题回复数
        public static CBB.ExceptionHelper.OperationResult UpdateAnswerCount(String id)
        {
            try
            {
                WenWen obj = GetWenWen(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.AnswerCount = (int)GetWenWenAnswerCount(obj.ID);

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWen> mc = md.GetCollection<WenWen>(WenWen.GetCollectionName());
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
        #endregion

        #region 问问答案
        //根据用户id获取回复集合
        public static IList<WenWenAnswer> GetWenWenAnswerForMember(String MemberID, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<WenWenAnswer> mc = MongoDBHelper.GetCursor<WenWenAnswer>(
                    WenWenAnswer.GetCollectionName(),
                    Query.EQ("Creater.MemberID", MemberID),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);
                List<WenWenAnswer> objs = new List<WenWenAnswer>();
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
        //获取用户id获取回复集合数量
        public static long GetWenWenAnswerForMemberCount(String MemberID)
        {
            try
            {
                long mc = MongoDBHelper.GetCount(WenWenAnswer.GetCollectionName(), Query.EQ("Creater.MemberID", MemberID));
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
        //获取兴趣问问答案
        public static IList<WenWenAnswer> GetWenWenAnswer(String wenwenID, int pagesize, int pageno, int createdtimeorder)
        {
            QueryComplete qc = null;
            try
            {
                if (wenwenID != "") qc = Query.EQ("WenWenID", wenwenID);

                MongoCursor<WenWenAnswer> mc = MongoDBHelper.GetCursor<WenWenAnswer>(
                    WenWenAnswer.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", createdtimeorder),
                    pageno,
                    pagesize);

                List<WenWenAnswer> objs = new List<WenWenAnswer>();
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
        //获取兴趣问问答案数量
        public static long GetWenWenAnswerCount(String wenwenID)
        {
            try
            {
                long answercount = MongoDBHelper.GetCount(WenWenAnswer.GetCollectionName(), Query.EQ("WenWenID", wenwenID));
                return answercount;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //通过答案ID获取兴趣问问答案
        public static WenWenAnswer GetWenWenAnswer(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWenAnswer> mc = md.GetCollection<WenWenAnswer>(WenWenAnswer.GetCollectionName());
                WenWenAnswer iv = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //用户创建兴趣问问
        public static WenWenAnswer AddWenWenAnswer(String memberid, String wenwenID, bool upordown, String content)
        {
            try
            {
                WenWenAnswer obj = new WenWenAnswer();
                obj.WenWenID = wenwenID;
                obj.Content = content;
                obj.Creater = new Creater.Creater(memberid);
                obj.IsBestAnswer = false;
                obj.CreatedTime = DateTime.Now;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWenAnswer> mc = md.GetCollection<WenWenAnswer>(WenWenAnswer.GetCollectionName());
                mc.Save(obj);

                //更新问问UpDown
                UpdateWenWenUpDown(wenwenID, upordown);
                //更新问问UpdateTime
                UpdateTime(wenwenID);
                //更新问问回答数量
                UpdateAnswerCount(wenwenID);

                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, WenWenAnswer.GetCollectionName(), obj.ID, "Content", memberid);
                //更新兴趣
                WenWen wwobj = GetWenWen(obj.WenWenID);
                IList<WenWen> wenwens = GetWenWens(wwobj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(wwobj.InterestID, wenwens);

                //添加粉丝超人数据
                BiZ.InterestCenter.Interest interestobj = BiZ.InterestCenter.InterestFactory.GetInterest(wwobj.InterestID);
                Active.ActiveReflectionFactory.AddActiveMessage("1", interestobj.Creater.MemberID + "," + memberid);
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
        //更新兴趣问问
        public static CBB.ExceptionHelper.OperationResult UpdateWenWenAnswer(String id, String content)
        {
            try
            {
                WenWenAnswer obj = GetWenWenAnswer(id);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");
                obj.Content = content;
                obj.CreatedTime = DateTime.Now;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWenAnswer> mc = md.GetCollection<WenWenAnswer>(WenWenAnswer.GetCollectionName());
                mc.Save(obj);

                //更新问问UpdateTime
                UpdateTime(obj.WenWenID);

                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, WenWenAnswer.GetCollectionName(), obj.ID, "Content", obj.Creater.MemberID);

                //更新兴趣
                WenWen wwobj = GetWenWen(obj.WenWenID);
                IList<WenWen> wenwens = GetWenWens(wwobj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(wwobj.InterestID, wenwens);
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
        //删除兴趣问问
        public static CBB.ExceptionHelper.OperationResult DelWenWenAnswer(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<WenWenAnswer> mc = md.GetCollection<WenWenAnswer>(WenWenAnswer.GetCollectionName());

                mc.Remove(Query.EQ("_id", ObjectId.Parse(id)));

                //更新兴趣
                WenWenAnswer answer = GetWenWenAnswer(id);
                WenWen wwobj = GetWenWen(answer.WenWenID);
                IList<WenWen> wenwens = GetWenWens(wwobj.InterestID, 0, 0);
                BiZ.InterestCenter.InterestFactory.UpdateInterestToWenWen(wwobj.InterestID, wenwens);

                //更新问问回答数量
                UpdateAnswerCount(wwobj.ID);

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
        #endregion
    }
}
