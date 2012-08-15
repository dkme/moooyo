using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Photo
{
    /// <summary>
    /// 照片管理类
    /// </summary>
    public class PhotoManager
    {
        /// <summary>
        /// 浏览照片
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult ViewPhoto(String photoID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(photoID));
                mc.Update(qc, Update.Inc("ViewCount", 1));
                
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 增加照片
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="photoType"></param>
        /// <param name="extName"></param>
        /// <param name="oFileName"></param>
        /// <param name="FileType"></param>
        /// <returns>照片ID</returns>
        public static Photo AddPhoto(String memberID, PhotoType photoType, String extName, String oFileName, String fileType, String newFileName)
        {
            try
            {
                //保存照片信息
                Photo myp = AddPhotoOrgin(memberID, photoType, extName, oFileName, fileType, newFileName);
                //用户照片类别为0-10
                if ((int)myp.PhotoType <= 10)
                {
                    //增加用户照片总数
                    MemberManager.MemberManager.ModifyPhotoCount(memberID, MemberManager.StatusModifyType.Add);

                    //增加动态
                    BiZ.Member.Activity.ActivityController.AddActivity(
                        memberID,
                        Member.Activity.ActivityType.UploadPhoto,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_UploadPhoto_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_UploadPhoto(newFileName),
                        false);
                }

                return myp;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static Photo AddPhotoOrgin(String memberID, PhotoType photoType, String extName, String oFileName, String fileType, String newFileName)
        {
            try
            {
                Photo myf = new Photo();
                myf.MemberID = memberID;
                myf.PhotoType = photoType;
                myf.FileName = newFileName;
                myf.CreatedTime = DateTime.Now;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                mc.Insert(myf);

                return myf;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 批量删除用户视频认证照片
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult DelPhotoLot(List<string> imgNames)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.In("FileName", BsonArray.Create(imgNames.ToArray()));
                mc.Remove(qc);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        
        /// <summary>
        /// 更新照片描述
        /// </summary>
        /// <param name="photoID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UpdatePhotoContent(String photoID, String Title, String Content)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(photoID));
                Photo myphoto = mc.FindOne(qc);
                myphoto.Title = Title;
                myphoto.Content = Content;
                mc.Save(myphoto);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(Title, Photo.GetCollectionName(), photoID, "Title", myphoto.MemberID);
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(Content, Photo.GetCollectionName(), photoID, "Content", myphoto.MemberID);

                //增加动态
                BiZ.Member.Activity.ActivityController.AddActivity(
                    myphoto.MemberID,
                    Member.Activity.ActivityType.UpdatePhotoComment,
                    BiZ.Member.Activity.ActivityController.GetActivityContent_UpdatePhotoContent_Title(),
                    BiZ.Member.Activity.ActivityController.GetActivityContent_UpdatePhotoContent(myphoto.FileName,Title, Content),
                    false);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static CBB.ExceptionHelper.OperationResult BatchUpdatePhotoInfo(String photoIDs, String photoTitles, String photoContents)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                Photo myphoto;
                IMongoQuery qc;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                string[] arrPhotoIDs = photoIDs.Split('|');
                string[] arrPhotoTitles = photoTitles.Split('|');
                string[] arrPhotoContents = photoContents.Split('|');
                for (int i = 0; i < (arrPhotoIDs.Length - 1); i++)
                {
                    qc = Query.EQ("_id", ObjectId.Parse(arrPhotoIDs[i]));
                    myphoto = mc.FindOne(qc);
                    myphoto.Title = arrPhotoTitles[i];
                    myphoto.Content = arrPhotoContents[i];
                    mc.Save(myphoto);
                    //审核关键字
                    new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(myphoto.Title, Photo.GetCollectionName(), myphoto.ID, "Title", myphoto.MemberID);
                    new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(myphoto.Content, Photo.GetCollectionName(), myphoto.ID, "Content", myphoto.MemberID);

                    //增加动态
                    BiZ.Member.Activity.ActivityController.AddActivity(
                        myphoto.MemberID,
                        Member.Activity.ActivityType.UpdatePhotoComment,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_UpdatePhotoContent_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_UpdatePhotoContent(myphoto.FileName, arrPhotoTitles[i], arrPhotoContents[i]),
                        false);
                }

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 增加照片评论
        /// </summary>
        /// <param name="photoID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddComment(String photoID, String mid, String NickName,String iconpath,String Content)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                Photo pt = mc.FindOne(Query.EQ("_id", ObjectId.Parse(photoID)));
                if (pt.Comments == null) pt.Comments = new List<Coment.Coment>();

                Coment.Coment cmt = new Coment.Coment();
                cmt.MemberID = mid;
                cmt.NickName = NickName;
                cmt.CreatedTime = DateTime.Now;
                cmt.Content = Content;
                cmt.ICONPath = iconpath;
                pt.Comments.Add(cmt);
                pt.CommentCount = pt.Comments.Count;
                mc.Save(pt);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(Content, Photo.GetCollectionName(), photoID, "Comments." + (pt.Comments.Count - 1) + ".Content", mid);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static CBB.ExceptionHelper.OperationResult DelComment(String photoID, String memberID, String cmtMemberID, DateTime createdtime)
        {
            try
            {
                BiZ.Photo.Photo p = Moooyo.BiZ.Photo.PhotoManager.GetPhoto(photoID);
                int d = -1;
                for (int i = 0; i < p.Comments.Count; i++)
                {
                    DateTime dt = p.Comments[i].CreatedTime.ToLocalTime().AddMilliseconds(-p.Comments[i].CreatedTime.ToLocalTime().Millisecond);

                    if (p.Comments[i].MemberID == cmtMemberID & dt.ToString() == createdtime.ToString())
                    {
                        d = i;
                    }
                }
                if (d != -1)
                {
                    p.Comments.RemoveAt(d);
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                mc.Save(p);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        
        /// <summary>
        /// 删除照片
        /// </summary>
        /// <param name="photoID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult DeletePhoto(String memberID, String photoID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(photoID)),Query.EQ("MemberID", memberID));
                mc.Remove(qc);

                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberID);
                if (photoID == mym.MemberPhoto.IconID)
                { 
                    mym.MemberPhoto.IconID = "";
                    mym.MemberInfomation.IconPath = "";
                }
                BiZ.MemberManager.MemberManager.SaveMember(mym);

                //减少用户照片总数
                MemberManager.MemberManager.ModifyPhotoCount(memberID, MemberManager.StatusModifyType.Decrease);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 审核图片不可访问
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult BindedPhoto(String photoID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(photoID)));

                Photo photo = PhotoManager.GetPhoto(photoID);

                String ReplacePic = "";
                if (photo.PhotoType == PhotoType.MemberAvatar)
                {
                    ReplacePic = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberAvatarIsNotPassAuditReplacePic");
                    //更改用户头像
                    BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler modiPicPathHand =
                        new BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler();
                    modiPicPathHand.MemberId = photo.MemberID;
                    modiPicPathHand.PictureId = "";
                    modiPicPathHand.PicturePath = ReplacePic;
                    //替换审核不通过的图片
                    modiPicPathHand.OldPicturePath = photo.FileName;
                    modiPicPathHand.ModifyPicPathEvent +=
                        new BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler.ModifyPicPathEventHandler(
                            new BiZ.Member.ModifyPicturePath.ModifyPicturePathProvider().ModifyMemberAvatarPicPath
                            ); //注册方法
                    modiPicPathHand.ModifyPicPath(); //自动调用注册过对象的方法
                    BiZ.MemberManager.MemberManager.RemoveMemberToNew(photo.MemberID);//从新用户列表中删除
                }
                else  if (photo.PhotoType== PhotoType.ImageContentPhoto || photo.PhotoType== PhotoType.SuoSuoContentPhoto || photo.PhotoType== PhotoType.CallForContentPhoto)
                {
                    ReplacePic = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("PhotoIsNotPassAuditReplacePic");
                    //删除内容
                    BiZ.Content.ContentProvider.RemoveContentWhereHasImage(photo.MemberID, photo.FileName);
                }
               
                DeletePhoto(photo.ID);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        
        }

        /// <summary>
        /// 删除照片
        /// </summary>
        /// <param name="photoID">图片编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DeletePhoto(String photoID)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(photoID)));

                Photo photo = PhotoManager.GetPhoto(photoID);
                if (photo != null)
                {
                    //删除图片文件
                    new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(photo.FileName);
                }

                //删除图片数据
                mc.Remove(qc);

                //删除图片文件
                string filenamepart1 = photo.FileName.Substring(0, photo.FileName.LastIndexOf('.'));
                CBB.ImageHelper.ImageUpload imu = new CBB.ImageHelper.ImageUpload();
                imu.DelImageFromGridFS(photo.FileName);
                imu.DelImageFromGridFS(filenamepart1 + "_s.jpg");
                imu.DelImageFromGridFS(filenamepart1 + "_p.jpg");
                imu.DelImageFromGridFS(filenamepart1 + "_i.jpg");

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 获取照片地址
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        ///         
        public static IList<Photo> GetPhotos(String memberID, int pagesize, int pageno)
        {
            List<Photo> GetPhotos = new List<Photo>();

            //用户照片类别从0-10
            MongoCursor<Photo> cur = MongoDBHelper.GetCursor<Photo>(
                   "Photos",
                   Query.And(Query.EQ("MemberID", memberID), Query.GTE("PhotoType", 0), Query.LTE("PhotoType", 10)),
                   new SortByDocument("CreatedTime", -1),
                   pageno,
                   pagesize);

            GetPhotos.AddRange(cur);

            return GetPhotos;
        }
        
        public static IList<Photo> GetPhotos(String memberID, PhotoType photoType,int pagesize,int pageno)
        {

            List<Photo> GetPhotos = new List<Photo>();

            MongoCursor<Photo> cur = MongoDBHelper.GetCursor<Photo>(
                   "Photos",
                    Query.And(Query.EQ("MemberID", memberID), Query.EQ("PhotoType", (int)photoType)),
                   new SortByDocument("CreatedTime", -1),
                   pageno,
                   pagesize);

            GetPhotos.AddRange(cur);

            return GetPhotos;
        }
        public static IList<PhotoSimple> GetSimplePhotos(String memberID, int pagesize, int pageno)
        {

            List<Photo> GetPhotos = new List<Photo>();

            MongoCursor<Photo> cur = MongoDBHelper.GetCursor<Photo>(
                "Photos",
                Query.And(Query.EQ("MemberID", memberID), Query.GTE("PhotoType", 0), Query.LTE("PhotoType", 10)),
                new SortByDocument("CreatedTime", -1),
                pageno,
                pagesize);

            GetPhotos.AddRange(cur);

            List<PhotoSimple> ps = new List<PhotoSimple>();
            foreach (Photo myp in GetPhotos)
            {
                PhotoSimple mys = new PhotoSimple();
                mys.ID = myp.ID;
                mys.FileName = myp.FileName;
                ps.Add(mys);
            }

            return ps;
        }
        public static IList<PhotoSimple> GetSimplePhotos(String memberID, PhotoType photoType, int pagesize, int pageno)
        {

            List<Photo> GetPhotos = new List<Photo>();

            MongoCursor<Photo> cur = MongoDBHelper.GetCursor<Photo>(
                    "Photos",
                    Query.And(Query.EQ("MemberID", memberID), Query.EQ("PhotoType", (int)photoType)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

            GetPhotos.AddRange(cur);

            List<PhotoSimple> ps = new List<PhotoSimple>();
            foreach (Photo myp in GetPhotos)
            {
                PhotoSimple mys = new PhotoSimple();
                mys.ID = myp.ID;
                mys.FileName = myp.FileName;
                ps.Add(mys);
            }

            return ps;
        }
        /// <summary>
        /// 获取照片
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static Photo GetPhoto(String id)
        {
            if (id == "") return null;

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
            IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(id));
            Photo myphoto = mc.FindOne(qc);
            return myphoto;
        }
        //通过图片名称获取图片
        public static Photo GetNamePhoto(String name)
        {
            if (name == "") return null;

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
            IMongoQuery qc = Query.EQ("FileName", name);
            Photo myphoto = mc.FindOne(qc);
            return myphoto;
        }
        /// <summary>
        /// 获取下一张照片
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static Photo GetNextPhoto(String id)
        {
            if (id == "") return null;

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
            IMongoQuery qc = Query.GT("_id", ObjectId.Parse(id));
            Photo myphoto = mc.FindOne(qc);
            return myphoto;
        }
        /// <summary>
        /// 获取照片评论列表
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static IList<Coment.Coment> GetPhotoComments(String photoID)
        {
            Photo p = GetPhoto(photoID);
            if (p.Comments == null || p.Comments.Count == 0)
                return new List<Coment.Coment>();
            else
                return p.Comments;
        }

        #region 图片审核
        public static IList<Photo> GetAuditPhotos(PhotoType photoType, int IsAudited, int pagesize, int pageno)
        {
            List<Photo> GetPhotos = new List<Photo>();

            MongoCursor<Photo> cur = MongoDBHelper.GetCursor<Photo>(
                   "Photos", Query.And(Query.EQ("IsAudited", IsAudited), Query.EQ("PhotoType", photoType)),
                   new SortByDocument("CreatedTime", -1), pageno, pagesize);
            //, Query.NE("PhotoType", PhotoType.RegisterPhoto)
            GetPhotos.AddRange(cur);

            return GetPhotos;
        }
        /// <summary>
        /// 设置审核结果
        /// </summary>
        /// <param name="photoID"></param>
        /// <param name="isAudited"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult SetAuditedResult(String photoID, int isAudited)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Photo> mc = md.GetCollection<Photo>("Photos");
                IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(photoID));
                Photo myphoto = mc.FindOne(qc);
                myphoto.IsAudited = isAudited;
                mc.Save(myphoto);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 按图片类别和审核状态获取多张图片
        /// </summary>
        /// <returns>图片总数</returns>
        public static long GetTypeAuditPhotoCount(PhotoType photoType, byte isAudited)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.And(
                    Query.EQ("IsAudited", isAudited), 
                    Query.EQ("PhotoType", photoType)
                    );
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(Photo.GetCollectionName(), qc);
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
        #endregion
    }
}
