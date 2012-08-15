using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using CBB.MongoDB;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Moooyo.BiZ.PhotoCheck
{
    public enum CheckPhotoStatus 
    {
        /// <summary>
        /// 待审
        /// </summary>
        waitaudit = 7,
        /// <summary>
        /// 通过
        /// </summary>
        auditpass = 8,
        /// <summary>
        /// 删除
        /// </summary>
        audidel = 9
    }

    //邱志明
    //2012-02-27
    //视频认证照片数据操作类
    public class PhotoCheckOperation
    {
        /// <summary>
        /// 添加待审照片
        /// </summary>
        /// <param name="pcm">待审照片模型</param>
        public void AddCheckPhoto(PhotoCheckModel pcm)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                mc.Insert(pcm);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 依据审核状态批量查询待审文本
        /// </summary>
        /// <param name="cps">待审状态枚举</param>
        /// <returns></returns>
        public List<PhotoCheckModel> GetCheckPotoList(CheckPhotoStatus cps, int pageno, int pagesize)
        {
            List<PhotoCheckModel> list = new List<PhotoCheckModel>();
            try
            {
                MongoCursor<PhotoCheckModel> mc = MongoDBHelper.GetCursor<PhotoCheckModel>(
                "PhotoCheck",
                Query.EQ("CheckStatus", 
                Convert.ToInt32(cps)),
                new SortByDocument("CreatedTime", 1),
                pageno,
                pagesize
                );
                List<PhotoCheckModel> objs = new List<PhotoCheckModel>();
                objs.AddRange(mc);
                return objs;
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="verifyStatus">类型枚举</param>
        /// <returns></returns>
        public long GetCount(CheckPhotoStatus cps)
        {
            long count;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                count = mc.Count(Query.EQ("CheckStatus", Convert.ToInt32(cps)));
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
         /// 批量修改状态
         /// </summary>
         /// <param name="_id">id集合</param>
         /// <param name="cps">状态</param>
        /// <param name="adminid">审核人Id</param>
        public void UpdateCheckPhotoStatuss(List<ObjectId> _id, CheckPhotoStatus cps, string adminid)
        {

            if (_id.Count <= 0 || cps == CheckPhotoStatus.waitaudit)
                return;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                QueryConditionList qcl = Query.In("_id", new BsonArray(_id.ToArray()));
                mc.Update(qcl, Update.Set("CheckStatus", Convert.ToInt32(cps)).Set("AdminId", adminid).Set("JionTime", DateTime.Now), UpdateFlags.Multi);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }


        /// <summary>
         /// 批量修改状态
         /// </summary>
         /// <param name="_id">id集合</param>
         /// <param name="cps">状态</param>
        /// <param name="adminid">审核人Id</param>
        /// <param name="useridlist">要更新的用户id集合</param>
        public void UpdateCheckPhotoStatuss(List<ObjectId> _id, CheckPhotoStatus cps, string adminid, List<ObjectId> useridlist)
        {

            if (_id.Count <= 0 || cps == CheckPhotoStatus.waitaudit)
                return;
            UpdateCheckPhotoStatuss(_id, cps, adminid);
        }



        /// <summary>
        /// 修改单个待审照片状态
        /// </summary>
        /// <param name="_id">id</param>
        /// <param name="cps">状态</param>
        /// <param name="adminid">审核人</param>
        public void UpdateCheckPhotoStatus(ObjectId _id, CheckPhotoStatus cps, string adminid)
        {
            try
            {
                if (cps == CheckPhotoStatus.waitaudit)
                    return;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");

                mc.Update(Query.EQ("_id", _id),
                    Update.Set("CheckStatus", Convert.ToInt32(cps))
                    .Set("AdminId", adminid)
                    .Set("JionTime", DateTime.Now)
                );
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 批量删除存档
        /// </summary>
        /// <param name="objids"></param>
        public void RemoveCheckPhotos(List<ObjectId> objids)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                QueryConditionList qcl = Query.In("_id", new BsonArray(objids.ToArray()));
                mc.Remove(qcl);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 批量删除存档依据用户id
        /// </summary>
        /// <param name="objids"></param>
        public void RemoveCheckPhotosByuserid(string userid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                QueryConditionList qcl = Query.In("UserId", userid);
                mc.Remove(qcl);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取一个CheckPhoto模型
        /// </summary>
        /// <param name="_id">id</param>
        public PhotoCheckModel GetCheckPhotoByid(ObjectId _id) 
        {
            try 
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                IMongoQuery qc = Query.EQ("_id", _id);
                return mc.FindOne(qc);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取一个CheckPhoto模型
        /// </summary>
        /// <param name="_id">用户id</param>
        public PhotoCheckModel GetCheckPhotoByUserid(string userid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                QueryComplete qc = Query.EQ("UserId", userid);
                return mc.FindOne(qc);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取一个CheckPhoto模型
        /// </summary>
        /// <param name="userid">userid</param>
        /// <param name="cps">状态</param>
        /// <returns></returns>
        public PhotoCheckModel GetCheckPhotoByUserid(string userid, BiZ.PhotoCheck.CheckPhotoStatus cps)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<PhotoCheckModel> mc = md.GetCollection<PhotoCheckModel>("PhotoCheck");
                IMongoQuery qc = Query.And(Query.EQ("UserId", userid), Query.EQ("CheckStatus",Convert.ToInt32(cps)));
                return mc.FindOne(qc);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
    }
}
