/*************************************************
 * Functional description ：兴趣中心数据提供类
 * Author：Lau An
 * Modify the expansion：Lau Tao
 * Modified date：2012/3/17 Saturday  
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.InterestCenter
{
    /// <summary>
    /// 处理兴趣中心的数据逻辑
    /// </summary>
    public class InterestFactory
    {
        #region 兴趣分类
        /// <summary>
        /// 分页获取多条兴趣分类
        /// </summary>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣分类列表集合</returns>
        public static IList<InterestClass> GetInterestClass(int pagesize, int pageno)
        {
            try
            {
                MongoCursor<InterestClass> mc = MongoDBHelper.GetCursor<InterestClass>(
                    InterestClass.GetCollectionName(),
                    null,
                    new SortByDocument("Order", 1),
                    pageno,
                    pagesize);

                List<InterestClass> objs = new List<InterestClass>();
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
        /// 跳过指定条记录分页获取多条兴趣分类
        /// </summary>
        /// <param name="skipNumber">跳过数</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣分类列表</returns>
        public static IList<InterestClass> GetInterestClass(int skipNumber, int pagesize, int pageno)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mgColl = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                IMongoSortBy sort = new SortByDocument("Order", 1); //排序，升序
                pagesize = (pagesize == 0) ? 1 : pagesize;
                MongoCursor<InterestClass> mgCur;
                if (pageno < 1)
                    mgCur = mgColl.FindAll().SetSortOrder(sort);
                else
                    //SetSortOrder排序，SetSkip跳过，SetLimit取指定条
                    mgCur = mgColl.FindAll().SetSortOrder(sort).SetSkip(((pageno - 1) * pagesize) + skipNumber).SetLimit(pagesize); 

                List<InterestClass> objs = new List<InterestClass>();
                objs.AddRange(mgCur);

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
        /// 获取所有兴趣分类
        /// </summary>
        /// <returns>兴趣分类列表</returns>
        public static IList<InterestClass> GetAllInterestClass()
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mcoll = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                MongoCursor<InterestClass> mc = mcoll.FindAll();

                List<InterestClass> objs = new List<InterestClass>();
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
        /// 按兴趣分类编号获取一条兴趣分类
        /// </summary>
        /// <param name="id">兴趣分类编号</param>
        /// <returns>兴趣分类</returns>
        public static InterestClass GetInterestClass(String id)
        {
            InterestClass iv = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
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
        /// <summary>
        /// 按兴趣分类标题获取一条兴趣分类
        /// </summary>
        /// <param name="title">兴趣分类标题</param>
        /// <returns>兴趣分类</returns>
        public static InterestClass GetInterestClassByTitle(String title)
        {
            InterestClass iv = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iv = mc.FindOne(Query.EQ("Title", title)); //FindOne检索一条
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
        /// <summary>
        /// 添加一条兴趣分类
        /// </summary>
        /// <param name="title">兴趣分类标题</param>
        /// <param name="iconpath">兴趣分类图片地址</param>
        /// <param name="order">兴趣分类排序</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddInterestClass(String title, String iconpath, int order)
        {
            InterestClass obj = new InterestClass();
            obj.Title = title;
            obj.ICONPath = iconpath;
            obj.Order = order;
            obj.CreatedTime = DateTime.Now;
            obj.InterestCount = GetClassInterestCount(title); //按兴趣分类标题获取兴趣分类总数
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
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
        /// 按兴趣分类编号更新一条兴趣分类
        /// </summary>
        /// <param name="id">兴趣分类编号</param>
        /// <param name="title">兴趣分类标题</param>
        /// <param name="iconpath">兴趣分类图片地址</param>
        /// <param name="order">兴趣分类排序</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateInterestClass(String id, String title, String iconpath, int order)
        {
            try
            {
                InterestClass obj = GetInterestClass(id); //按兴趣分类编号获取一条兴趣分类
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.Title = title;
                obj.ICONPath = iconpath;
                obj.Order = order;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
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
        /// 按兴趣分类编号删除一条兴趣分类
        /// </summary>
        /// <param name="id">兴趣分类编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DelInterestClass(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());

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
        /// <summary>
        /// 获取所有兴趣分类总数
        /// </summary>
        /// <returns>所有兴趣分类总数</returns>
        public static int GetInterestClassCount()
        {
            int iCCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iCCount = (int)mc.Count();
                return iCCount;
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
        /// 获取跳过指定条记录后所有兴趣分类总数
        /// </summary>
        /// <param name="skipNumber">跳过数</param>
        /// <returns>兴趣分类总数</returns>
        public static int GetInterestClassCount(int skipNumber)
        {
            int iCCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iCCount = (int)mc.Count() - skipNumber;
                return iCCount;
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
        /// 按兴趣分类标题获取兴趣分类总数
        /// </summary>
        /// <param name="interestClassTitle">兴趣分类标题</param>
        /// <returns>兴趣分类总数</returns>
        public static int GetInterestClassCount(String interestClassTitle)
        {
            int iCCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                string[] interestClassTitleArr = interestClassTitle.Split(','); //参数中的的兴趣分类标题是用逗号隔开的一个字符串所以用Split打散
                iCCount = (int)mc.Count(Query.In("Title", new BsonArray(interestClassTitleArr))); //用In查询兴趣分类标题数组
                return iCCount;
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
        /// 按兴趣分类标题获取兴趣总数
        /// </summary>
        /// <param name="interestClassTitle">兴趣分类标题</param>
        /// <returns>兴趣总数</returns>
        public static int GetClassInterestCount(String interestClassTitle)
        {
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("Title", interestClassTitle));
                if (iC == null) return 0;
                return iC.InterestCount;
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

        #region 兴趣
        /// <summary>
        /// 按兴趣分类标题，粉丝数降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestClassTitle">兴趣分类标题</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetInterest(String interestClassTitle, int pagesize, int pageno)
        {
            try
            {
                // 按兴趣分类标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Classes", interestClassTitle, Interest.GetCollectionName(), "FansCount", -1, pagesize, pageno, false);
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
        /// 按兴趣分类标题，创建时间降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestClassTitle">兴趣分类标题</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetSortedInterestCreatedTimeDesc(String interestClassTitle, int pagesize, int pageno)
        {
            try
            {
                // 按兴趣分类标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Classes", interestClassTitle, Interest.GetCollectionName(), "CreatedTime", -1, pagesize, pageno, false);
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
        /// 按兴趣分类标题，兴趣粉丝创建时间降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestClassTitle">兴趣分类标题</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetInterestClassSortedInterestFansCreatedTimeDesc(String interestClassTitle, int pagesize, int pageno)
        {
            try
            {
                //按字段获取兴趣列表按兴趣粉丝创建时间降序的兴趣列表
                IList<Interest> sortedInterest = GetOnFieldInterestSortedInterestFansCreatedTimeDesc("Classes", interestClassTitle, pagesize, pageno);
                return sortedInterest;
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
        /// 按兴趣编号获取一条兴趣
        /// </summary>
        /// <param name="id">兴趣编号</param>
        /// <returns>兴趣</returns>
        public static Interest GetInterest(String id)
        {
            Interest iv = new Interest();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
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
        /// <summary>
        /// 按兴趣标题，粉丝数降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestTitle">兴趣标题</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetTitleInterest(String interestTitle, int pageSize, int pageNo)
        {
            List<Interest> interestObj = new List<Interest>();
            try
            {
                // 按兴趣标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Title", interestTitle, Interest.GetCollectionName(), "FansCount", -1, pageSize, pageNo, false);
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
        /// 按忽略大小写兴趣标题，粉丝数降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestTitle">兴趣标题</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetIgnoreCaseTitleInterest(String interestTitle, int pageSize, int pageNo)
        {
            List<Interest> interestObj = new List<Interest>();
            try
            {
                // 按兴趣标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Title", interestTitle, Interest.GetCollectionName(), "FansCount", -1, pageSize, pageNo, true);
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
        /// 按兴趣标题，创建时间降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestTitle">兴趣标题</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetSortedTitleInterestCreatedTimeDesc(String interestTitle, int pageSize, int pageNo)
        {
            try
            {
                // 按兴趣标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Title", interestTitle, Interest.GetCollectionName(), "CreatedTime", -1, pageSize, pageNo, false);
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
        /// 按兴趣标题，兴趣粉丝创建时间降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestTitle">兴趣标题</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetInterestTitleSortedInterestFansCreatedTimeDesc(String interestTitle, int pageSize, int pageNo)
        {
            try
            {
                //按字段获取兴趣列表按兴趣粉丝创建时间降序的兴趣列表
                IList<Interest> sortedInterest = GetOnFieldInterestSortedInterestFansCreatedTimeDesc("Title", interestTitle, pageSize, pageNo);
                return sortedInterest;
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
        /// 按兴趣标题获取兴趣总数
        /// </summary>
        /// <param name="interestTitle">兴趣标题</param>
        /// <returns>兴趣总数</returns>
        public static long GetTitleInterestCount(String interestTitle)
        {
            long iCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                iCount = mc.Count(Query.Matches("Title", ".*" + interestTitle + ".*")); //先模糊查询，再查总数
                return iCount;
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
        /// 按兴趣编号数组获取多条兴趣
        /// </summary>
        /// <param name="ids">兴趣编号数组</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetInterest(String[] ids)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Interest> objs = new List<Interest>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                    else
                        return objs;
                }
                MongoCursor<Interest> mc = MongoDBHelper.GetCursor<Interest>(
                    Interest.GetCollectionName(),
                    Query.In("_id", new BsonArray(iIds.ToArray())),
                    null,
                    0,
                    1);

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
        /// 按兴趣编号数组分页获取多条兴趣
        /// </summary>
        /// <param name="ids">兴趣编号数组</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetInterest(String[] ids, int pageSize, int pageNo)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Interest> objs = new List<Interest>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                    else
                        return objs;
                }
                MongoCursor<Interest> mc = MongoDBHelper.GetCursor<Interest>(
                    Interest.GetCollectionName(), Query.In("_id", new BsonArray(iIds.ToArray())), new SortByDocument("CreatedTime", -1), pageNo, pageSize);

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
        /// 按兴趣标题数组分页获取多条兴趣
        /// </summary>
        /// <param name="ids">兴趣标题数组</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetTitleInterest(String[] titles, int pageSize, int pageNo)
        {
            try
            {
                List<Interest> objs = new List<Interest>();
                
                MongoCursor<Interest> mc = MongoDBHelper.GetCursor<Interest>(
                    Interest.GetCollectionName(), Query.In("Title", new BsonArray(titles)), new SortByDocument("CreatedTime", -1), pageNo, pageSize);

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
        //用户创建兴趣
        public static Interest CreateInterest(String memberid, String title, String content, String classes, String iconid, String iconpath, string selfhoodPictureId, string selfhoodPicture)
        {
            Interest obj = AddInterest(memberid, title, content, classes, iconid, iconpath, selfhoodPictureId, selfhoodPicture);
            if (obj.ID != "")
            {
                UpdateInterestClassInterestCount(obj.Classes);
                AddInterestFans(obj.ID, memberid);

                //增加动态
                BiZ.Member.Activity.ActivityController.AddActivity(
                    memberid,
                    Member.Activity.ActivityType.AddInterest,
                    BiZ.Member.Activity.ActivityController.GetActivityContent_AddInterest_Title(),
                    BiZ.Member.Activity.ActivityController.GetActivityContent_AddInterest(obj.ID, title, content, iconpath),
                    false);

                return obj;
            }
            else
            {
                //保存兴趣错误
                return null;
            }
        }
        /// <summary>
        /// 按用户编号分页获取多条用户加粉的兴趣
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetMemberInterest(String memberId, int pagesize, int pageno)
        {
            //try
            //{
                MongoDatabase md = MongoDBHelper.MongoDB;
                //查出 List<InterestFans> 对象中的 InterestFans.Creater.MemberID 属性的值列表
                MongoCollection<InterestFans> mgColl = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                MongoCursor<InterestFans> mgCur = mgColl.Find(Query.EQ("Creater.MemberID", memberId));
                List<ObjectId> iIds = new List<ObjectId>();
                foreach (InterestFans iFans in mgCur)
                {
                    iIds.Add(ObjectId.Parse(iFans.ObjectID));
                }
                MongoCursor<Interest> mgCur2 = MongoDBHelper.GetCursor<Interest>(Interest.GetCollectionName(),
                   Query.In("_id", new BsonArray(iIds.ToArray())),
                   new SortByDocument("CreatedTime", -1),
                   pageno,
                   pagesize);
                List<Interest> objs = new List<Interest>();
                objs.AddRange(mgCur2);
                return objs;
            //}
            //catch (System.Exception err)
            //{
            //    throw new CBB.ExceptionHelper.OperationException(
            //        CBB.ExceptionHelper.ErrType.SystemErr,
            //        CBB.ExceptionHelper.ErrNo.DBOperationError,
            //        err);
            //}
        }
        /// <summary>
        /// 获取用户创建的兴趣
        /// </summary>
        /// <param name="mid">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>想去列表</returns>
        public static IList<Interest> GetInterestForMember(String mid, int pagesize, int pageno)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCursor<Interest> mgCur = MongoDBHelper.GetCursor<Interest>(Interest.GetCollectionName(), Query.EQ("Creater.MemberID", mid), new SortByDocument("CreatedTime", -1), pageno, pagesize);
                List<Interest> objs = new List<Interest>();
                objs.AddRange(mgCur);
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
        //获取用户创建的兴趣的数量
        public static long GetInterestForMemberCount(String mid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mgCollInte = md.GetCollection<Interest>(Interest.GetCollectionName());
                long count = mgCollInte.Count(Query.EQ("Creater.MemberID", mid));
                return count;
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
        /// 根据用户编号分页查询多条用户加粉的兴趣，并且去除没有话题的兴趣
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <param name="topictyboy">是否选择大米</param>
        /// <param name="topictogirl">是否仙则柚子</param>
        /// <returns>兴趣列表</returns>
        public static IList<Interest> GetMemberInterestToWenWen(String memberId, int pagesize, int pageno, bool topictyboy, bool topictogirl)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                //查出 List<InterestFans> 对象中的 InterestFans.Creater.MemberID 属性的值列表
                MongoCollection<InterestFans> mgColl = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                MongoCursor<InterestFans> mgCur = mgColl.Find(Query.EQ("Creater.MemberID", memberId));
                List<ObjectId> iIds = new List<ObjectId>();
                MongoCursor<Interest> mgCur2 = null;
                foreach (InterestFans iFans in mgCur)
                {
                    iIds.Add(ObjectId.Parse(iFans.ObjectID));
                }
                if (topictyboy && !topictogirl)
                {
                    mgCur2 = MongoDBHelper.GetCursor<Interest>(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWensToBoy", 1), Query.Size("WenWensToBoy", 2), Query.Size("WenWensToBoy", 3), Query.Size("WenWensToBoy", 4), Query.Size("WenWensToBoy", 5), Query.Size("WenWensToBoy", 6))),
                       new SortByDocument("UpdateTimeToWenWen", -1),
                       pageno,
                       pagesize);
                }
                else if (!topictyboy && topictogirl)
                {
                    mgCur2 = MongoDBHelper.GetCursor<Interest>(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWensToGirl", 1), Query.Size("WenWensToGirl", 2), Query.Size("WenWensToGirl", 3), Query.Size("WenWensToGirl", 4), Query.Size("WenWensToGirl", 5), Query.Size("WenWensToGirl", 6))),
                       new SortByDocument("UpdateTimeToWenWen", -1),
                       pageno,
                       pagesize);
                }
                else
                {
                    mgCur2 = MongoDBHelper.GetCursor<Interest>(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWens", 1), Query.Size("WenWens", 2), Query.Size("WenWens", 3), Query.Size("WenWens", 4), Query.Size("WenWens", 5), Query.Size("WenWens", 6))),
                       new SortByDocument("UpdateTimeToWenWen", -1),
                       pageno,
                       pagesize);
                }
                List<Interest> objs = new List<Interest>();
                objs.AddRange(mgCur2);
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
        /// 根据用户编号分页查询多条用户加粉的兴趣，并且去除没有话题的兴趣(数量)
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <param name="topictyboy">是否选择大米</param>
        /// <param name="topictogirl">是否仙则柚子</param>
        /// <returns>数量</returns>
        public static long GetMemberInterestToWenWenCount(String memberId, bool topictyboy, bool topictogirl)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                //查出 List<InterestFans> 对象中的 InterestFans.Creater.MemberID 属性的值列表
                MongoCollection<InterestFans> mgColl = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                MongoCursor<InterestFans> mgCur = mgColl.Find(Query.EQ("Creater.MemberID", memberId));
                List<ObjectId> iIds = new List<ObjectId>();
                long mgCur2 = 0;
                foreach (InterestFans iFans in mgCur)
                {
                    iIds.Add(ObjectId.Parse(iFans.ObjectID));
                }
                if (topictyboy && !topictogirl)
                {
                    mgCur2 = MongoDBHelper.GetCount(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWensToBoy", 1), Query.Size("WenWensToBoy", 2), Query.Size("WenWensToBoy", 3), Query.Size("WenWensToBoy", 4), Query.Size("WenWensToBoy", 5), Query.Size("WenWensToBoy", 6))));
                }
                else if (!topictyboy && topictogirl)
                {
                    mgCur2 = MongoDBHelper.GetCount(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWensToGirl", 1), Query.Size("WenWensToGirl", 2), Query.Size("WenWensToGirl", 3), Query.Size("WenWensToGirl", 4), Query.Size("WenWensToGirl", 5), Query.Size("WenWensToGirl", 6))));
                }
                else
                {
                    mgCur2 = MongoDBHelper.GetCount(Interest.GetCollectionName(),
                        Query.And(Query.In("_id", new BsonArray(iIds.ToArray())), Query.Or(Query.Size("WenWens", 1), Query.Size("WenWens", 2), Query.Size("WenWens", 3), Query.Size("WenWens", 4), Query.Size("WenWens", 5), Query.Size("WenWens", 6))));
                }
                return mgCur2;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //按兴趣ID和用户ID获取兴趣
        public static Interest GetMemberInterest(String intersertId, String memberId)
        {
            Interest itert = new Interest();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                itert = mc.FindOne(Query.And(Query.EQ("_id", ObjectId.Parse(intersertId)), Query.EQ("Creater.MemberID", memberId)));
                return itert;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //获取用户加粉的兴趣总数
        public static long GetMemberInterestCount(String memberId)
        {
            long count;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                //查出 List<InterestFans> 对象中的 InterestFans.Creater.MemberID 属性的值列表
                MongoCollection<InterestFans> mgColl = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                MongoCursor<InterestFans> mgCur = mgColl.Find(Query.EQ("Creater.MemberID", memberId));
                List<ObjectId> iIds = new List<ObjectId>();
                foreach (InterestFans iFans in mgCur)
                {
                    iIds.Add(ObjectId.Parse(iFans.ObjectID));
                }

                MongoCollection<Interest> mgCollInte = md.GetCollection<Interest>(Interest.GetCollectionName());
                count = mgCollInte.Count(Query.In("_id", new BsonArray(iIds.ToArray())));
                return count;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //增加或减少兴趣粉丝数量
        public static CBB.ExceptionHelper.OperationResult UpdateInterestFansCount(String objid, int count)
        {
            try
            {
                Interest obj = GetInterest(objid);
                if (obj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");

                obj.FansCount += count;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
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
        /// 添加兴趣
        /// </summary>
        /// <param name="memberid">用户id</param>
        /// <param name="title">兴趣名称</param>
        /// <param name="content">介绍</param>
        /// <param name="classes">标签（逗号分隔）</param>
        /// <param name="iconpath">兴趣图标ID</param>
        /// <param name="iconpath">兴趣图标地址</param>
        /// <returns>兴趣</returns>
        private static Interest AddInterest(String memberid, String title, String content, String classes, String iconid, String iconpath, string selfhoodPictureId, string selfhoodPicture)
        {
            Interest obj = new Interest();
            obj.Title = title;
            obj.Content = content;
            obj.ICONID = iconid;
            obj.ICONPath = iconpath;
            obj.SelfhoodPictureId = selfhoodPictureId;
            obj.SelfhoodPicture = selfhoodPicture;
            obj.Creater = new Creater.Creater(memberid);
            obj.FansCount = 0;
            obj.Classes = classes;
            obj.IsAudited = false;
            obj.IsEnable = true;
            obj.CreatedTime = DateTime.Now;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                mc.Save(obj);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(content, Interest.GetCollectionName(), obj.ID, "Content", memberid);
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(title, Interest.GetCollectionName(), obj.ID, "Title", memberid);
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(classes, Interest.GetCollectionName(), obj.ID, "Classes", memberid);
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
        //获取所有兴趣总数
        public static int GetAllInterestCount()
        {
            int iCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                iCount = (int)mc.Count();
                return iCount;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //按兴趣分类ID获取兴趣集合
        public static IList<Interest> GetClassInterest(String interestClassId, String interestTitle, int pagesize, int pageno)
        {
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));
                //如果查询某分类的兴趣
                QueryComplete qc = null;
                if (iC != null)
                    qc = Query.And(Query.Matches("Classes", ".*" + iC.Title + ".*"), Query.Matches("Title", ".*" + interestTitle + ".*"));

                MongoCursor<Interest> mCur = MongoDBHelper.GetCursor<Interest>(
                    Interest.GetCollectionName(), qc,
                    new SortByDocument("FansCount", -1),
                    pageno,
                    pagesize);

                List<Interest> objs = new List<Interest>();
                objs.AddRange(mCur);

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
        //按兴趣分类ID和兴趣标题获取兴趣集合
        public static IList<Interest> GetClassInterest(String interestClassId, int pagesize, int pageno)
        {
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));

                // 按兴趣分类标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Classes", (iC != null ? iC.Title : ""), Interest.GetCollectionName(), "FansCount", -1, pagesize, pageno, false);
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
        //按兴趣分类ID和兴趣标题获取兴趣集合
        public static IList<Interest> GetClassInterestCreatedTimeDesc(String interestClassId, int pagesize, int pageno)
        {
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));

                // 按兴趣分类标题模糊查询排序分页获取多条兴趣
                IList<Interest> objs = GetFuzzyQuerySorted<Interest>("Classes", (iC != null ? iC.Title : ""), Interest.GetCollectionName(), "CreatedTime", -1, pagesize, pageno, false);
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
        /// 按兴趣分类标题，兴趣粉丝创建时间降序分页获取多条兴趣
        /// </summary>
        /// <param name="interestClassId">兴趣分类编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>排序后的兴趣列表</returns>
        public static IList<Interest> GetInterestClassIdSortedInterestFansCreatedTimeDesc(String interestClassId, int pagesize, int pageno)
        {
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));

                // 按兴趣分类标题模糊查询排序分页获取多条兴趣
                IList<Interest> sortedInterestList = GetInterestClassSortedInterestFansCreatedTimeDesc((iC != null ? iC.Title : ""), pagesize, pageno);
                return sortedInterestList;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //按兴趣分类ID获取兴趣总数
        public static int GetClassIdInterestCount(String interestClassId)
        {
            int iCount = 0;
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));
                //如果查询某分类的兴趣
                QueryComplete qc = null;
                if (iC != null)
                    qc = Query.Matches("Classes", iC.Title);
                MongoCollection<Interest> mgColl2 = md.GetCollection<Interest>(Interest.GetCollectionName());
                iCount = (int)mgColl2.Count(qc);
                return iCount;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //按兴趣分类ID和兴趣标题获取兴趣总数
        public static long GetClassIdInterestCount(String interestClassId, String interestTitle)
        {
            long iCount = 0;
            InterestClass iC = new InterestClass();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                iC = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestClassId)));
                //如果查询某分类的兴趣
                QueryComplete qc = null;
                if (iC != null)
                    qc = Query.And(Query.Matches("Classes", ".*" + iC.Title + ".*"), Query.Matches("Title", ".*" + interestTitle + ".*"));
                MongoCollection<Interest> mgColl2 = md.GetCollection<Interest>(Interest.GetCollectionName());
                iCount = mgColl2.Count(qc);
                return iCount;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //按兴趣分类标题更新兴趣分类兴趣总数
        private static CBB.ExceptionHelper.OperationResult UpdateInterestClassInterestCount(String interestClassTitle)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestClass> mc = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                string[] interestClassTitleArr = interestClassTitle.Split(',');
                MongoCursor<InterestClass> mCur = mc.Find(Query.In("Title", new BsonArray(interestClassTitleArr)));
                List<InterestClass> iCObjs = new List<InterestClass>();
                iCObjs.AddRange(mCur);

                foreach (InterestClass ic in iCObjs)
                {
                    MongoCollection<InterestClass> mc2 = md.GetCollection<InterestClass>(InterestClass.GetCollectionName());
                    mc2.Update(
                        Query.EQ("_id", ObjectId.Parse(ic.ID)),
                        Update.Inc("InterestCount", 1)
                    );
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
        //更新兴趣
        public static CBB.ExceptionHelper.OperationResult UpdateInterest(String mId, String interestId, String title, String content, String classes, String iconid, String iconpath, String selfhoodPictureId, String selfhoodPicture)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                if (iconid != "" && iconpath != "" && selfhoodPictureId != "" && selfhoodPicture != "")
                {
                    mc.Update(
                        Query.And(Query.EQ("_id", ObjectId.Parse(interestId)), Query.EQ("Creater.MemberID", mId)),
                        Update.Combine(
                            Update.Set("Title", title),
                            Update.Set("Content", content),
                            Update.Set("Classes", classes),
                            Update.Set("ICONID", iconid),
                            Update.Set("ICONPath", iconpath),
                            Update.Set("SelfhoodPictureId", selfhoodPictureId),
                            Update.Set("SelfhoodPicture", selfhoodPicture)
                            )
                        );
                }
                else if (iconid != "" && iconpath != "" && selfhoodPictureId == "" && selfhoodPicture == "")
                {
                    mc.Update(
                        Query.And(Query.EQ("_id", ObjectId.Parse(interestId)), Query.EQ("Creater.MemberID", mId)),
                        Update.Combine(
                            Update.Set("Title", title),
                            Update.Set("Content", content),
                            Update.Set("Classes", classes),
                            Update.Set("ICONID", iconid),
                            Update.Set("ICONPath", iconpath)
                            )
                        );
                }
                else if (iconid == "" && iconpath == "" && selfhoodPictureId != "" && selfhoodPicture != "")
                {
                    mc.Update(
                        Query.And(Query.EQ("_id", ObjectId.Parse(interestId)), Query.EQ("Creater.MemberID", mId)),
                        Update.Combine(
                            Update.Set("Title", title),
                            Update.Set("Content", content),
                            Update.Set("Classes", classes),
                            Update.Set("SelfhoodPictureId", selfhoodPictureId),
                            Update.Set("SelfhoodPicture", selfhoodPicture)
                            )
                        );
                }
                else
                {
                    mc.Update(
                       Query.And(Query.EQ("_id", ObjectId.Parse(interestId)), Query.EQ("Creater.MemberID", mId)),
                       Update.Combine(
                           Update.Set("Title", title),
                           Update.Set("Content", content),
                           Update.Set("Classes", classes)
                           )
                       );
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
        //更新兴趣话题时更新兴趣
        public static CBB.ExceptionHelper.OperationResult UpdateInterestToWenWen(String interestID, IList<WenWen.WenWen> wenwens)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Interest> mc = md.GetCollection<Interest>(Interest.GetCollectionName());
                Interest obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(interestID)));
                obj.WenWensToBoy = new List<WenWen.WenWen>();
                obj.WenWensToGirl = new List<WenWen.WenWen>();
                if (obj.UpdateTimeToWenWen == null) { obj.UpdateTimeToWenWen = DateTime.Now; }

                List<WenWen.WenWen> boylist = new List<WenWen.WenWen>();
                List<WenWen.WenWen> girllist = new List<WenWen.WenWen>();
                List<WenWen.WenWen> boyandgirllist = new List<WenWen.WenWen>();

                foreach (WenWen.WenWen wwobj in wenwens)
                {
                    if (boylist.Count < 6)
                    {
                        if (wwobj.Creater.Sex == 1)
                            boylist.Add(wwobj);
                    }
                    if (girllist.Count < 6)
                    {
                        if (wwobj.Creater.Sex != 1)
                            girllist.Add(wwobj);
                    }
                    if (boyandgirllist.Count < 6)
                    {
                        boyandgirllist.Add(wwobj);
                    }
                    if (boylist.Count == 6 && girllist.Count == 6 && boyandgirllist.Count == 6)
                        break;
                }
                obj.WenWens = boyandgirllist;
                obj.WenWensToBoy = boylist;
                obj.WenWensToGirl = girllist;
                obj.UpdateTimeToWenWen = DateTime.Now;
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
        //我与TA的共同兴趣爱好
        public static IList<Interest> GetIAndTACommonInterest(String fromMemberID, String toMemberID, int pageSize, int pageNo)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;

                //我的兴趣ID列表
                List<ObjectId> fromMemInteIDs = new List<ObjectId>();
                IList<Interest> fromMemIntes = GetMemberInterest(fromMemberID, 1, 0); //按用户编号分页获取多条用户加粉的兴趣
                foreach (Interest inte in fromMemIntes)
                {
                    fromMemInteIDs.Add(ObjectId.Parse(inte.ID));
                }

                //TA的兴趣ID列表
                List<ObjectId> toMemInteIDs = new List<ObjectId>();
                IList<Interest> toMemIntes = GetMemberInterest(toMemberID, 1, 0); //按用户编号分页获取多条用户加粉的兴趣
                foreach (Interest inte in toMemIntes)
                {
                    toMemInteIDs.Add(ObjectId.Parse(inte.ID));
                }

                //我们共同的兴趣列表
                List<String> commonMemInteIDs = new List<String>();
                foreach (ObjectId toMemInteID in toMemInteIDs)
                {
                    foreach (ObjectId fromMemInteID in fromMemInteIDs)
                    {
                        if (fromMemInteID == toMemInteID) commonMemInteIDs.Add(toMemInteID.ToString());
                    }
                }

                IList<Interest> commonMemInte = GetInterest(commonMemInteIDs.ToArray(), pageSize, pageNo); //按兴趣编号数组获取多条兴趣

                return commonMemInte;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //我与TA的共同兴趣爱好总数
        public static int GetIAndTACommonInterestCount(String fromMemberID, String toMemberID)
        {
            return GetIAndTACommonInterest(fromMemberID, toMemberID, 1, 0).Count;
        }
        #endregion

        #region 兴趣粉丝
        /// <summary>
        /// 根据兴趣编号和用户编号查找粉丝
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <param name="memberid">用户编号</param>
        /// <returns></returns>
        public static InterestFans GetInterestFans(String interestid, String memberid)
        {
            try
            {
                InterestFans obj = new InterestFans();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                obj = mc.FindOne(Query.And(Query.EQ("ObjectID", interestid), Query.EQ("Creater.MemberID", memberid)));
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
        /// 按兴趣ID获取兴趣粉丝
        /// </summary>
        /// <param name="intersertId">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣粉丝列表集合</returns>
        public static IList<InterestFans> GetInterestFans(String intersertId, int pagesize, int pageno)
        {
            QueryComplete qc = null;
            try
            {
                if (intersertId != "")
                    qc = Query.EQ("ObjectID", intersertId); //等于查询

                MongoCursor<InterestFans> mc = MongoDBHelper.GetCursor<InterestFans>(
                    InterestFans.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageno, pagesize);

                List<InterestFans> objs = new List<InterestFans>();
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
        ///// <summary>
        ///// 按兴趣ID获取有头像的兴趣粉丝
        ///// </summary>
        ///// <param name="intersertId">兴趣编号</param>
        ///// <param name="pagesize">每页条数</param>
        ///// <param name="pageno">当前页</param>
        ///// <returns>兴趣粉丝列表集合</returns>
        //public static IList<InterestFans> GetInterestFansToIconPath(String intersertId, int pagesize, int pageno)
        //{
        //    QueryComplete qc = null;
        //    try
        //    {
        //        if (intersertId != "")
        //            qc = Query.And(Query.EQ("ObjectID", intersertId), Query.Exists("Creater.ICONPath", true), Query.NE("Creater.ICONPath", "")); //等于查询

        //        MongoCursor<InterestFans> mc = MongoDBHelper.GetCursor<InterestFans>(
        //            InterestFans.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageno, pagesize);

        //        List<InterestFans> objs = new List<InterestFans>();
        //        objs.AddRange(mc);

        //        return objs;
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        /// <summary>
        /// 按兴趣ID获取兴趣粉丝(按活跃度排序)
        /// </summary>
        /// <param name="intersertId">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣粉丝列表集合</returns>
        public static IList<InterestFans> GetInterestFansToHot(String intersertId, int pagesize, int pageno)
        {
            QueryComplete qc = null;
            try
            {
                if (intersertId != "")
                    qc = Query.EQ("ObjectID", intersertId); //等于查询

                MongoCursor<InterestFans> mc = MongoDBHelper.GetCursor<InterestFans>(
                    InterestFans.GetCollectionName(), qc, new SortByDocument("ContentCount", -1), pageno, pagesize);

                List<InterestFans> objs = new List<InterestFans>();
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
        /// 按兴趣编号数组获取兴趣粉丝
        /// </summary>
        /// <param name="arrIntertId">兴趣编号数组</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣粉丝列表集合</returns>
        public static IList<InterestFans> GetInterestFans(String[] arrIntertId, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<InterestFans> mc = MongoDBHelper.GetCursor<InterestFans>(
                    InterestFans.GetCollectionName(),
                    Query.In("ObjectID",  new BsonArray(arrIntertId)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<InterestFans> objs = new List<InterestFans>();
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
        /// 按兴趣ID获取兴趣异性粉丝
        /// </summary>
        /// <param name="sex">性别</param>
        /// <param name="intersertId">兴趣编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页</param>
        /// <returns>兴趣异性粉丝列表集合</returns>
        public static IList<InterestFans> GetInterestHtxualFans(int sex, String intersertId, int pagesize, int pageno)
        {
            try
            {
                sex = sex == 1 ? 2 : 1;
                MongoCursor<InterestFans> mc = MongoDBHelper.GetCursor<InterestFans>(
                    InterestFans.GetCollectionName(),
                    Query.And(Query.EQ("ObjectID", intersertId), Query.EQ("Creater.Sex", sex)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<InterestFans> objs = new List<InterestFans>();
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
        /// 按兴趣ID获取兴趣异性粉丝总数
        /// </summary>
        /// <param name="sex">性别</param>
        /// <param name="intersertId">兴趣编号</param>
        /// <returns>兴趣异性粉丝总数</returns>
        public static int GetInterestHtxualFansCount(int sex, String intersertId)
        {
            return GetInterestHtxualFans(sex, intersertId, 1, 0).Count;
        }
        /// <summary>
        /// 按兴趣ID获取兴趣粉丝总数
        /// </summary>
        /// <param name="intersertId">兴趣编号</param>
        /// <returns>兴趣粉丝总数</returns>
        public static long GetInterestFansCount(String intersertId)
        {
            try
            {

                long count = MongoDBHelper.GetCount(
                    InterestFans.GetCollectionName(),
                    Query.EQ("ObjectID", intersertId));

                return count;
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
        /// 按兴趣ID获取兴趣粉丝总数
        /// </summary>
        /// <param name="intersertId">兴趣编号</param>
        /// <returns>兴趣粉丝总数</returns>
        public static long GetInterestFansToIconPathCount(String intersertId)
        {
            try
            {

                long count = MongoDBHelper.GetCount(
                    InterestFans.GetCollectionName(),
                    Query.And(Query.EQ("ObjectID", intersertId), Query.Exists("Creater.ICONPath", true), Query.NE("Creater.ICONPath", "")));

                return count;
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
        /// 获取所有兴趣粉丝总数
        /// </summary>
        /// <returns>所有兴趣粉丝总数</returns>
        public static int GetInterestFansCount()
        {
            int iFCount;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                iFCount = (int)mc.Count();
                return iFCount;
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
        /// 加粉
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <param name="memberid">用户编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddInterestFans(String interestid, String memberid)
        {
            InterestFans obj = new InterestFans();
            obj.ObjectID = interestid;
            obj.Creater = new Creater.Creater(memberid);
            obj.CreatedTime = DateTime.Now;
            obj.ContentCount = 0;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                mc.Save(obj);

                //增加兴趣粉丝数量
                UpdateInterestFansCount(obj.ObjectID, 1);
                IList<WenWen.WenWen> wenwens = WenWen.WenWenProvider.GetWenWens(interestid, 0, 0);
                UpdateInterestToWenWen(interestid, wenwens);
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
        /// 删粉
        /// </summary>
        /// <param name="interestid">兴趣编号</param>
        /// <param name="memberid">用户编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DelInterestFans(String interestid, String memberid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());

                mc.Remove(
                        Query.And(
                        Query.EQ("Creater.MemberID", memberid),
                        Query.EQ("ObjectID", interestid)
                        )
                    );

                //减少兴趣粉丝数量
                UpdateInterestFansCount(interestid, -1);

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
        /// 是否是粉丝
        /// </summary>
        /// <param name="interestId">兴趣编号</param>
        /// <param name="memberId">用户编号</param>
        /// <returns>是或否</returns>
        public static bool IsFans(string interestId, String memberId)
        {
            bool isFans = false;
            long count = MongoDBHelper.GetCount(
                    InterestFans.GetCollectionName(),
                    Query.And(Query.EQ("ObjectID", interestId), Query.EQ("Creater.MemberID", memberId)));
            if (count > 0) { isFans = true; }
            return isFans;
        }
        /// <summary>
        /// 按用户编号和头像地址更新多条兴趣粉丝
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="avatar">头像地址</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateMemberIdInterestFansAvatar(string memberId, string avatar)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
                mc.Update(
                            Query.EQ("Creater.MemberID", memberId),
                            Update.Set("Creater.ICONPath", avatar),
                            UpdateFlags.Multi,
                            SafeMode.True
                        );
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
        ///// <summary>
        ///// 用户登录后更新兴趣粉丝中对应用户的最后登录时间
        ///// </summary>
        ///// <param name="memberId">用户编号</param>
        ///// <param name="time">最后登录时间</param>
        ///// <returns></returns>
        //public static CBB.ExceptionHelper.OperationResult UpdateFansLastOperationTime(String memberId, DateTime time)
        //{
        //    try
        //    {
        //        MongoDatabase md = MongoDBHelper.MongoDB;
        //        MongoCollection<InterestFans> mc = md.GetCollection<InterestFans>(InterestFans.GetCollectionName());
        //        mc.Update(
        //                    Query.EQ("Creater.MemberID", memberId),
        //                    Update.Set("Creater.LastOperationTime", time)
        //                );
        //        return new CBB.ExceptionHelper.OperationResult(true);
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        #endregion


        /// <summary>
        /// 按字段和字段值和表名模糊查询并按字段排序分页查询多个集合
        /// </summary>
        /// <typeparam name="T">泛型类型（集合（表））</typeparam>
        /// <param name="field">字段</param>
        /// <param name="fieldValue">字段的值</param>
        /// <param name="tableName">表（集合）名</param>
        /// <param name="sortBy">按字段排序</param>
        /// <param name="lift">升或降序</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns>集合列表</returns>
        private static IList<T> GetFuzzyQuerySorted<T>(String field, String fieldValue, String tableName, String sortByField, int lift, int pageSize, int pageNo, bool ignoreCase)
        {
            QueryComplete qc = null;
            IMongoSortBy sort = null;

            if (fieldValue != "")
            {
                Regex regex;
                if (ignoreCase) regex = new Regex(fieldValue, RegexOptions.IgnoreCase);
                else regex = new Regex(fieldValue);
                qc = Query.Matches(field, regex); //模糊查询
            }
            if (sortByField != "" & lift != 0)
                sort = sort = new SortByDocument(sortByField, lift); //排序

            MongoCursor<T> mc = MongoDBHelper.GetCursor<T>(tableName, qc, sort, pageNo, pageSize); //分页

            List<T> objs = new List<T>();
            objs.AddRange(mc);

            return objs;
        }
        /// <summary>
        /// 获取兴趣列表右外连接兴趣粉丝列表并按兴趣粉丝创建时间降序后的列表
        /// </summary>
        /// <param name="interestList">兴趣列表</param>
        /// <param name="intertFansList">兴趣粉丝列表</param>
        /// <returns>排序后的兴趣列表</returns>
        private static IList<Interest> GetInterestLeftJoinInterestFansCreatedTimeSorted(IList<Interest> interestList, IList<InterestFans> intertFansList, int pageSize = 0, int pageNo = 0)
        {
            var query = (
                from inte in interestList
                 join inteFans in intertFansList on inte.ID equals inteFans.ObjectID
                 orderby inteFans.CreatedTime descending
                select inte).Distinct().ToList();

            List<Interest> sortedIntertList = new List<Interest>();
            foreach (Interest interest in query)
            {
                sortedIntertList.Add(interest);
            }

            //分页
            pageNo = (pageNo == 0) ? 1 : pageNo;
            if (pageSize >= 1) sortedIntertList = sortedIntertList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            return sortedIntertList;
        }
        /// <summary>
        /// 按字段获取兴趣列表按兴趣粉丝创建时间降序的兴趣列表
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="fieldValue">字段的值</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>兴趣列表</returns>
        private static IList<Interest> GetOnFieldInterestSortedInterestFansCreatedTimeDesc(String field, String fieldValue, int pageSize, int pageNo)
        {
            //按兴趣分类标题模糊查询排序分页获取多条兴趣
            IList<Interest> interestList = GetFuzzyQuerySorted<Interest>(field, fieldValue, Interest.GetCollectionName(), "", 0, 1, 0, true);

            //获取所有兴趣粉丝
            IList<InterestFans> interestFansList = GetInterestFans("", 1, 0);

            //获取兴趣列表右外连接兴趣粉丝列表并按兴趣粉丝创建时间降序后的列表
            interestList = GetInterestLeftJoinInterestFansCreatedTimeSorted(interestList, interestFansList, pageSize, pageNo);
            return interestList;
        }
    }
    /// <summary>
    /// Interest Distinct 去除重复规则比较器 
    /// </summary>
    //public class InterestIdRowComparer : IEqualityComparer<Interest>
    //{
    //    public bool Equals(Interest i1, Interest i2)
    //    {
    //        return (i1.ID == i2.ID);
    //    }
    //    public int GetHashCode(Interest i)
    //    {
    //        return i.ToString().GetHashCode();
    //    }
    //}   
}
