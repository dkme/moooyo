/**
 * Functional description ：用户动态数据业务逻辑
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/7/13 Friday 
 * Remarks：
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;
using System.Text.RegularExpressions;

namespace Moooyo.BiZ.Sys.MemberActivity
{
    public class MemberActivityProvider
    {
        /// <summary>
        /// 创建用户动态
        /// </summary>
        /// <param name="activityObj">用户动态对象</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult CreateMemberActivity(
            string fromMember, 
            string toMember, 
            MemberActivityType activityType, 
            String operateUrl)
        {
            CBB.ExceptionHelper.OperationResult result = null;
            try
            {
                MemberActivity activityObj = new MemberActivity();
                if (fromMember != "" && fromMember != null && fromMember != "4eb0fde42101b0824e2b018f")
                    activityObj.FromMember = new Creater.Creater(fromMember);
                else
                    activityObj.FromMember = null;
                if (toMember != "" && toMember != null && toMember != "4eb0fde42101b0824e2b018f")
                    activityObj.ToMember = new Creater.Creater(toMember);
                else
                    activityObj.ToMember = null;
                activityObj.CreatedTime = DateTime.Now;
                activityObj.ActivityType = activityType;
                activityObj.OperateUrl = operateUrl;

                result = Add(activityObj);

                return result;
            }
            catch (Exception excep)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   excep);
            }
        }

        /// <summary>
        /// 添加用户动态
        /// </summary>
        /// <param name="activityObj">用户动态对象</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult Add(MemberActivity activityObj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberActivity> mc = md.GetCollection<MemberActivity>(MemberActivity.GetCollectionName());
                mc.Insert(activityObj);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception excep)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   excep);
            }
        }

        /// <summary>
        /// 更新用户动态
        /// </summary>
        /// <param name="activityObj">用户动态对象</param>
        /// <returns>用户动态对象</returns>
        public static MemberActivity Save(MemberActivity activityObj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberActivity> mc = md.GetCollection<MemberActivity>(MemberActivity.GetCollectionName());
                mc.Save(activityObj);
                return activityObj;
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
        /// 获取多条用户动态
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户动态对象列表</returns>
        public static IList<MemberActivity> GetMemberActivitys(int pageSize, int pageNo)
        {
            QueryComplete qc = null;
            try
            {
                MongoCursor<MemberActivity> mc = MongoDBHelper.GetCursor<MemberActivity>(
                    MemberActivity.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<MemberActivity> objs = new List<MemberActivity>();
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
        /// 按操作类别分页获取多条用户动态
        /// </summary>
        /// <param name="activityType">操作类别</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户动态对象列表</returns>
        public static IList<MemberActivity> GetMemberActivitys(MemberActivityType activityType, int pageSize, int pageNo)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.EQ("ActivityType", activityType);
                MongoCursor<MemberActivity> mc = MongoDBHelper.GetCursor<MemberActivity>(
                    MemberActivity.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<MemberActivity> objs = new List<MemberActivity>();
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
        /// 按源用户分页获取多条用户动态
        /// </summary>
        /// <param name="fromMember">源用户</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户动态对象列表</returns>
        public static IList<MemberActivity> GetMemberActivitys(long fromMember, int pageSize, int pageNo)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.EQ("FromMember.UniqueNumber.ConvertedID", fromMember);
                MongoCursor<MemberActivity> mc = MongoDBHelper.GetCursor<MemberActivity>(
                    MemberActivity.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<MemberActivity> objs = new List<MemberActivity>();
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
        /// 按操作时间分页获取多条用户动态
        /// </summary>
        /// <param name="startTime">操作时间起</param>
        /// <param name="endTime">操作时间止</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户动态对象列表</returns>
        public static IList<MemberActivity> GetMemberActivitys(DateTime startTime, DateTime endTime, int pageSize, int pageNo)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.And(
                    Query.GTE("CreatedTime", startTime.Date.AddHours(8)),
                    Query.LT("CreatedTime", endTime.Date.AddDays(1).AddHours(8))
                    );
                MongoCursor<MemberActivity> mc = MongoDBHelper.GetCursor<MemberActivity>(
                    MemberActivity.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<MemberActivity> objs = new List<MemberActivity>();
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
        /// 获取所有用户动态总数
        /// </summary>
        /// <returns>用户动态总数</returns>
        public static int GetMemberActivityCount()
        {
            QueryComplete qc = null;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(MemberActivity.GetCollectionName(), qc);
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
        /// 按操作类别获取用户动态总数
        /// </summary>
        /// <returns>用户动态总数</returns>
        public static int GetTypeMemberActivityCount(MemberActivityType activityType)
        {
            QueryComplete qc = null;
            try
            {
                
                qc = Query.EQ("ActivityType", activityType);
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(MemberActivity.GetCollectionName(), qc);
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
        /// 按源用户获取用户动态总数
        /// </summary>
        /// <returns>用户动态总数</returns>
        public static int GetFromMemberActivityCount(long fromMember)
        {
            QueryComplete qc = null;
            try
            {

                qc = Query.EQ("FromMember.UniqueNumber.ConvertedID", fromMember);
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(MemberActivity.GetCollectionName(), qc);
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
        /// 按操作时间获取用户动态总数
        /// </summary>
        /// <returns>用户动态总数</returns>
        public static int GetTimeMemberActivityCount(DateTime startTime, DateTime endTime)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.And(
                    Query.GTE("CreatedTime", startTime.Date.AddHours(8)),
                    Query.LT("CreatedTime", endTime.Date.AddDays(1).AddHours(8))
                    );
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(MemberActivity.GetCollectionName(), qc);
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
        /// 按用户动态编号获取一条用户动态对象
        /// </summary>
        /// <param name="inviteCode">用户动态编号</param>
        /// <returns>用户动态对象</returns>
        public static MemberActivity GetMemberActivity(string activityId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberActivity> mc = md.GetCollection<MemberActivity>(MemberActivity.GetCollectionName());
                MemberActivity obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(activityId)));
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
    }
}
