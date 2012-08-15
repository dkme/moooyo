///
/// 功能描述：顶部图片推送的数据提供类
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.TopImagePush
{
    public class ImagePushProvider
    {
        /// <summary>
        /// 返回所有的顶部图片推送集合
        /// </summary>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>顶部图片推送集合</returns>
        public static List<ImagePush> findAll(Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            try
            {
                QueryComplete qc = Query.EQ("DeleteFlag", deleteFlag);
                MongoCursor<ImagePush> mc = MongoDBHelper.GetCursor<ImagePush>(ImagePush.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), pageno, pagesize);
                List<ImagePush> objs = new List<ImagePush>();
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
        /// 返回所有的顶部图片推送集合
        /// </summary>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>顶部图片推送集合</returns>
        public static List<ImagePush> findAll(int pageNo, int pageSize)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ImagePush> mcoll = md.GetCollection<ImagePush>(ImagePush.GetCollectionName());
                MongoCursor<ImagePush> mc = mcoll.FindAll().SetSortOrder(new SortByDocument("CreatedTime", -1));

                List<ImagePush> objs = new List<ImagePush>();
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
        /// 返回所有的顶部图片推送集合的数量
        /// </summary>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>顶部图片推送集合的数量</returns>
        public static long findAllCount(Comm.DeletedFlag deleteFlag)
        {
            try
            {
                QueryComplete qc = Query.EQ("DeleteFlag", deleteFlag);
                return MongoDBHelper.GetCount(ImagePush.GetCollectionName(), qc);
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
        /// 获取所有推送图片总数
        /// </summary>
        /// <returns>所有推送图片总数</returns>
        public static long GetAllPushImageCount()
        {
            try
            {
                return MongoDBHelper.GetCount(ImagePush.GetCollectionName());
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
        /// 判断内容是否已推送
        /// </summary>
        /// <param name="contentid">内容编号</param>
        /// <returns></returns>
        public static Boolean ifImagePush(string contentid)
        {
            try
            {
                QueryComplete qc = Query.EQ("ContentID", contentid);
                MongoCursor<ImagePush> mc = MongoDBHelper.GetCursor<ImagePush>(ImagePush.GetCollectionName(), qc, new SortByDocument("UpdateTime", -1), 0, 0);
                List<ImagePush> objs = new List<ImagePush>();
                objs.AddRange(mc);
                return objs != null && objs.Count > 0 ? true : false;
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
        /// 设置推送图片删除状态
        /// </summary>
        /// <param name="pushImageId">推送图片编号</param>
        /// <param name="showStatus">删除状态</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult SetPushImageDeleteFlag(String pushImageId, Comm.DeletedFlag deleteFlag)
        {
            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<ImagePush> mgColect = mgDb.GetCollection<ImagePush>(ImagePush.GetCollectionName());

            if (deleteFlag == Comm.DeletedFlag.No)
            {
                mgColect.Update(
                    Query.EQ("_id", ObjectId.Parse(pushImageId)),
                    Update.Combine(
                        Update.Set("DeleteFlag", deleteFlag),
                        Update.Inc("ImagePushCount.ShowCount", 1)
                    )
                );
            }
            else
            {
                mgColect.Update(
                    Query.EQ("_id", ObjectId.Parse(pushImageId)),
                    Update.Set("DeleteFlag", deleteFlag)
                );
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        /// 删除推送图片
        /// </summary>
        /// <param name="pushImgId">推送图片编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DeletePushImage(String pushImgId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<ImagePush> mc = md.GetCollection<ImagePush>(ImagePush.GetCollectionName());

                mc.Remove(Query.EQ("_id", ObjectId.Parse(pushImgId)));

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
        /// 添加推送置顶图片
        /// </summary>
        /// <param name="contentId">内容编号</param>
        /// <param name="photoPath">图片路径</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult AddPushImage(String contentId, String photoPath)
        {
            Content.ImageContent imagecontent = new Content.ImageContent(contentId);
            TopImagePush.ImagePushCount pushcount = new BiZ.TopImagePush.ImagePushCount();
            List<Content.Image> listImage = new List<Content.Image>();
            Content.Image image = new Content.Image(photoPath);
            listImage.Add(image);
            BiZ.TopImagePush.ImagePush imagepush = new BiZ.TopImagePush.ImagePush(
                contentId,
                imagecontent.MemberID,
                listImage,
                imagecontent.Content,
                pushcount,
                DateTime.Now,
                Comm.DeletedFlag.Yes);

            imagepush.Save(imagepush);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
    }
}
