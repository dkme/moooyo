///
/// 功能描述：内容回复的数据提供类
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CBB.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Moooyo.BiZ.Comment
{
    public class CommentProvider
    {
        /// <summary>
        /// 返回所有回复的集合
        /// </summary>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>回复集合</returns>
        public static IList<Comment> findAll(Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            try
            {
                QueryComplete qc = Query.EQ("DeleteFlag", deleteFlag);
                MongoCursor<Comment> mc = MongoDBHelper.GetCursor<Comment>(Comment.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<Comment> objs = new List<Comment>();
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
        /// 返回所有回复的集合的数量
        /// </summary>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>回复集合的数量</returns>
        public static long findAllCount(Comm.DeletedFlag deleteFlag)
        {
            try
            {
                QueryComplete qc = Query.EQ("DeleteFlag", deleteFlag);
                return MongoDBHelper.GetCount(Comment.GetCollectionName(), qc);
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
        /// 按回复对象编号返回回复集合
        /// </summary>
        /// <param name="contentID">回复对象编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>回复集合</returns>
        public static IList<Comment> findForContent(String contentID, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("CommentToID", contentID), Query.EQ("DeleteFlag", deleteFlag));
                MongoCursor<Comment> mc = MongoDBHelper.GetCursor<Comment>(Comment.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<Comment> objs = new List<Comment>();
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
        /// 按回复对象编号返回回复集合的数量
        /// </summary>
        /// <param name="contentID">回复对象编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>回复集合的数量</returns>
        public static long findForContentCount(String contentID, Comm.DeletedFlag deleteFlag)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("CommentToID", contentID), Query.EQ("DeleteFlag", deleteFlag));
                return MongoDBHelper.GetCount(Comment.GetCollectionName(), qc);
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
        /// 按回复对象编号和回复类型返回回复集合
        /// </summary>
        /// <param name="contentID">回复对象编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>回复集合</returns>
        public static IList<Comment> findForType(String contentID, CommentType type, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("CommentToID", contentID), Query.EQ("CommentType", type), Query.EQ("DeleteFlag", deleteFlag));
                MongoCursor<Comment> mc = MongoDBHelper.GetCursor<Comment>(Comment.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<Comment> objs = new List<Comment>();
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
        /// 按回复对象编号和回复类型返回回复集合的数量
        /// </summary>
        /// <param name="contentID">回复对象编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>回复集合的数量</returns>
        public static long findForTypeCount(String contentID, CommentType type, Comm.DeletedFlag deleteFlag)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("CommentToID", contentID), Query.EQ("CommentType", type), Query.EQ("DeleteFlag", deleteFlag));
                return MongoDBHelper.GetCount(Comment.GetCollectionName(), qc);
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
        /// 按用户编号查询回复集合（用户参与的）
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>回复集合</returns>
        public static IList<Comment> findForMember(String memberID, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("MemberID", memberID), Query.EQ("DeleteFlag", deleteFlag));
                MongoCursor<Comment> mc = MongoDBHelper.GetCursor<Comment>(Comment.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<Comment> objs = new List<Comment>();
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
        /// 按用户编号查询回复集合的数量
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>回复集合的数量</returns>
        public static long findForMemberCount(String memberID, Comm.DeletedFlag deleteFlag)
        {
            try
            {
                QueryComplete qc = Query.And(Query.EQ("MemberID", memberID), Query.EQ("DeleteFlag", deleteFlag));
                return MongoDBHelper.GetCount(Comment.GetCollectionName(), qc);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static IList<Comment> GetMemberNameContentIDComment(String contentID, String memberLable, Comm.DeletedFlag deleteFlag, int pageNo, int pageSize)
        {
            try
            {
                QueryComplete qc = null;
                if (memberLable != "")
                {
                    Regex regex;
                    regex = new Regex(memberLable, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    qc = Query.And(Query.EQ("CommentToID", contentID), Query.Matches("Content", regex), Query.EQ("DeleteFlag", deleteFlag));
                }
                else
                {
                    qc = Query.And(Query.EQ("CommentToID", contentID), Query.EQ("DeleteFlag", deleteFlag));
                }
                MongoCursor<Comment> mc = MongoDBHelper.GetCursor<Comment>(
                    Comment.GetCollectionName(), 
                    qc, 
                    new SortByDocument("UpdateTime", -1), 
                    pageNo, 
                    pageSize);
                List<Comment> objs = new List<Comment>();
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
        /// 按回复对象编号获取一条回复
        /// </summary>
        /// <param name="id">回复对象编号</param>
        /// <returns>回复对象</returns>
        public static Comment GetComment(String id)
        {
            Comment comment = new Comment();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Comment> mc = md.GetCollection<Comment>(Comment.GetCollectionName());
                comment = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                return comment;
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
