using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moooyo.BiZ.PhotoCheck;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.PhotoCheck
{
    //邱志明
    //2012-02-25
    //视频认证审核
    public class PhotoCheckStatusFactory
    {
        public static List<PhotoCheckModel> GetCheckPhotoList(CheckPhotoStatus cps, int pageno, int pagesize)
        {
            try
            {
                return new PhotoCheckOperation().GetCheckPotoList(cps, pageno, pagesize);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static long GetCheckPhotoCount(CheckPhotoStatus cps)
        {
            try
            {
                return new PhotoCheckOperation().GetCount(cps);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static CBB.ExceptionHelper.OperationResult UpdateCheckPhotoStatuss(List<ObjectId> idlist, CheckPhotoStatus cps, string adminid, List<ObjectId> useridlist)
        {
            try
            {
                new PhotoCheckOperation().UpdateCheckPhotoStatuss(idlist, cps, adminid,useridlist);
                if (cps == CheckPhotoStatus.auditpass) 
                {
                    //更新用户的视频认证为成功！
                    MemberManager.MemberManager.CheckRealPhoto(useridlist, true);
                }
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static CBB.ExceptionHelper.OperationResult UpdateCheckPhotoStatus(ObjectId id, CheckPhotoStatus cps, string adminid,string userid)
        {
            try
            {
                new PhotoCheckOperation().UpdateCheckPhotoStatus(id, cps, adminid);
                if (cps == CheckPhotoStatus.auditpass)
                {
                    //更新用户的视频认证为成功！
                    List<ObjectId> list = new List<ObjectId>();
                    list.Add(ObjectId.Parse(userid));
                    MemberManager.MemberManager.CheckRealPhoto(list, true);
                }
                else if (cps == CheckPhotoStatus.waitaudit || cps == CheckPhotoStatus.audidel)
                {
                    //更新用户的视频认证为不通过！
                    List<ObjectId> list = new List<ObjectId>();
                    list.Add(ObjectId.Parse(userid));
                    MemberManager.MemberManager.CheckRealPhoto(list, false);
                }
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 批量彻底删除认证照片
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="imgNames"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult RemoveCheckPhotos(List<ObjectId> idlist,List<string> imgNames)
        {
            try
            {
                new PhotoCheckOperation().RemoveCheckPhotos(idlist);
                //删除用户视频认证照片
                Moooyo.BiZ.Photo.PhotoManager.DelPhotoLot(imgNames);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 依据用户id彻底删除用户认证照片
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult RemoveCheckPhotosByusrid(string userid)
        {
            try
            {
                PhotoCheckModel pcm = new BiZ.PhotoCheck.PhotoCheckOperation().GetCheckPhotoByUserid(userid);
                List<string> imgNames = new List<string>();
                imgNames.Add(pcm.CheckImgPath);
                //删除用户视频认证照片
                Moooyo.BiZ.Photo.PhotoManager.DelPhotoLot(imgNames);
                List<ObjectId> idlist = new List<ObjectId>();
                //删除该用户所有认证照片
                new PhotoCheckOperation().RemoveCheckPhotosByuserid(userid);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static CBB.ExceptionHelper.OperationResult AddCheckPhoto(PhotoCheckModel pcm)
        {
            try
            {
                new PhotoCheckOperation().AddCheckPhoto(pcm);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static PhotoCheckModel GetCheckPhotoById(string _id)
        {
            try
            {
                return new PhotoCheckOperation().GetCheckPhotoByid(ObjectId.Parse(_id));
                
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static PhotoCheckModel GetCheckPhotoByUserId(string userid)
        {
            try
            {
                return new PhotoCheckOperation().GetCheckPhotoByUserid(userid);

            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        public static PhotoCheckModel GetCheckPhotoByUserId(string userid, BiZ.PhotoCheck.CheckPhotoStatus cps)
        {
            try
            {
                return new PhotoCheckOperation().GetCheckPhotoByUserid(userid, cps);

            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

    }
}
