/*******************************************************************
 * Functional description ：精选内容数据提供类
 * Author：Lau Tao
 * Modify the expansion：Tao Lau 
 * Modified date：2012/7/4 Wednesday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.FeaturedContent
{
    public class FeaturedContentProvider
    {
        /// <summary>
        /// 按使用标记获取多条精选内容
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>精选内容对象列表</returns>
        public static IList<FeaturedContent> GetFeaturedContents(int pageSize, int pageNo, Comm.UsedFlag usedFlag = Comm.UsedFlag.Unknown)
        {
            QueryComplete qc = null;
            try
            {
                if (usedFlag != Comm.UsedFlag.Unknown)
                    qc = Query.EQ("UsedFlag", usedFlag);
                MongoCursor<FeaturedContent> mc = MongoDBHelper.GetCursor<FeaturedContent>(
                    FeaturedContent.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<FeaturedContent> objs = new List<FeaturedContent>();
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
        /// 按使用标记获取精选内容总数
        /// </summary>
        /// <returns>精选内容总数</returns>
        public static int GetFeaturedContentsCount(Comm.UsedFlag usedFlag = Comm.UsedFlag.Unknown)
        {
            QueryComplete qc = null;
            try
            {
                if (usedFlag != Comm.UsedFlag.Unknown)
                    qc = Query.EQ("UsedFlag", usedFlag);
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(FeaturedContent.GetCollectionName(), qc);
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
        /// 按精选内容编号获取一条精选内容对象
        /// </summary>
        /// <param name="featuredContentId">精选内容编号</param>
        /// <returns>精选内容对象</returns>
        public static FeaturedContent GetFeaturedContent(String featuredContentId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FeaturedContent> mc = md.GetCollection<FeaturedContent>(FeaturedContent.GetCollectionName());
                FeaturedContent obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(featuredContentId)));
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
        /// 按精选内容编号和使用状态获取一条精选内容对象
        /// </summary>
        /// <param name="featuredContentId">精选内容编号</param>
        /// <param name="usedFlag">使用标记</param>
        /// <returns>精选内容对象</returns>
        public static FeaturedContent GetFeaturedContent(String featuredContentId, Comm.UsedFlag usedFlag)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FeaturedContent> mc = md.GetCollection<FeaturedContent>(FeaturedContent.GetCollectionName());
                FeaturedContent obj = mc.FindOne(Query.And(Query.EQ("_id", ObjectId.Parse(featuredContentId)), Query.EQ("UsedFlag", usedFlag)));
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
        /// 添加一条精选内容
        /// </summary>
        /// <param name="featuredContentId">精选内容编号</param>
        /// <param name="usedFlag">使用标记</param>
        /// <returns>精选内容对象</returns>
        public static CBB.ExceptionHelper.OperationResult AddFeaturedContent(String image, String content, String creator, Comm.UsedFlag usedFlag)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result = null;
                FeaturedContent featContent = new FeaturedContent(image, content, creator, usedFlag);
                result = Add(featContent);
                return result;
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
        /// 更新精选内容
        /// </summary>
        /// <param name="obj">精选内容对象</param>
        /// <returns>精选内容对象</returns>
        public static FeaturedContent Save(FeaturedContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FeaturedContent> mc = md.GetCollection<FeaturedContent>(FeaturedContent.GetCollectionName());
                mc.Save(obj);
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
        /// 按精选内容编号删除精选内容
        /// </summary>
        /// <param name="featContentId">精选内容编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult Delete(String featContentId)
        {
            try
            {
                CBB.ExceptionHelper.OperationResult result = new CBB.ExceptionHelper.OperationResult(true);

                FeaturedContent featCent = GetFeaturedContent(featContentId);

                //删除内容时同时删除图片
                if (featCent != null)
                {
                    if (featCent.Image != null && featCent.Image != "")
                    {
                        Photo.Photo photo = Photo.PhotoManager.GetNamePhoto(featCent.Image);
                        if (photo != null)
                        {
                            result = Photo.PhotoManager.DeletePhoto(photo.ID);
                        }
                    }
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FeaturedContent> mc = md.GetCollection<FeaturedContent>(FeaturedContent.GetCollectionName());
                mc.Remove(Query.EQ("_id", ObjectId.Parse(featContentId)));
                return result;
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
        /// 添加精选内容
        /// </summary>
        /// <param name="obj">精选内容对象</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult Add(FeaturedContent obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FeaturedContent> mc = md.GetCollection<FeaturedContent>(FeaturedContent.GetCollectionName());
                mc.Insert(obj);
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
    }
}
