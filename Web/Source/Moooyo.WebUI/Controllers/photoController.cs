using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;

namespace Moooyo.WebUI.Controllers
{
    public class photoController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        [Authorize]
        public ActionResult show(String id)
        {
            SetMetasVersion();

            //照片ID
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            BiZ.Photo.Photo photo = BiZ.Photo.PhotoManager.GetPhoto(id);

            String mid = HttpContext.User.Identity.Name;
            bool alreadylogin = Common.Comm.isalreadylogin("Member", HttpContext.User);

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(photo.MemberID);
            Models.PageModels.MemberPageModel memberPage = new Models.PageModels.MemberPageModel(memberDisplayObj);

            memberPage.UserID = mid;
            memberPage.MemberID = photo.MemberID;
            memberPage.AlreadyLogon = alreadylogin;
            #endregion

            ViewData["imgid"] = id;
            ViewData["isMe"] = memberPage.AlreadyLogon;
            ViewData["photourl"] = photo.FileName.Replace("\\", "/");
            ViewData["phototitle"] = photo.Title;
            ViewData["photocontent"] = photo.Content;
            ViewData["photodate"] = photo.CreatedTime.ToLocalTime();
            ViewData["photocommentscount"] = photo.CommentCount;
            ViewData["photoviewscount"] = photo.ViewCount;

            if (photo.CommentCount > 0)
            {
                ViewData["photocomments"] = new JavaScriptSerializer().Serialize(photo.Comments);
            }
            else
            {
                ViewData["photocomments"] = "[]";
            }
            ViewData["mid"] = photo.MemberID;
            ViewData["photowalldata"] = GetSimplePhotoList(photo.MemberID, null, "0", "0");

            return View(memberPage);
        }
        [Authorize]
        public ActionResult mplist(String id, String t)
        {
            SetMetasVersion();

            //照片类别
            int type = 0;
            if (t == null) type = -1;
            if (t != null) Int32.TryParse(t, out type);

            bool alreadylogin = Common.Comm.isalreadylogin("Member", HttpContext.User);
            String userid = HttpContext.User.Identity.Name;

            //按转换后的编号和编号类型获取原始的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberId = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(id, BiZ.Comm.UniqueNumber.IDType.MemberID);
            //如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
            if (uNumberId != null) id = uNumberId.DefaultId;

            //如果访问ID为空，则访问自己的主页
            if (id == null)
            {
                if (userid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
                else id = userid;
            }

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(id);
            Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj);

            memberModel.UserID = userid;
            memberModel.MemberID = id;
            memberModel.AlreadyLogon = alreadylogin;
            //按原始编号和编号类型获取转换后的编号
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberId2 = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(id, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if (uNumberId != null) memberModel.MemberUrl = uNumberId.ConvertedID.ToString();
            #endregion

            ViewData["phototype"] = type;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult PhotoWallRight(String mid, String phototype)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            ViewData["photowalldata"] = GetSimplePhotoList(mid, phototype, "0", "0");
            ViewData["mid"] = mid;

            return View();
        }
        [Authorize]
        public ActionResult PhotoWallCenter(String mid, String phototype)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            ViewData["photowalldata"] = GetSimplePhotoList(mid, phototype, "0", "0");
            ViewData["mid"] = mid;

            return View();
        }
        [Authorize]
        public ActionResult photoupload(String t)
        {
            SetMetasVersion();

            //照片类别
            int type = 0;
            if (t == null) type = -1;
            if (t != null) Int32.TryParse(t, out type);

            bool alreadylogin = true;
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.PageModels.MemberPageModel memberPage = new Models.PageModels.MemberPageModel(memberDisplayObj);

            memberPage.UserID = userid;
            memberPage.MemberID = userid;
            memberPage.AlreadyLogon = alreadylogin;
            #endregion

            ViewData["phototype"] = type;

            return View(memberPage);
        }
        #endregion

        #region 数据与业务方法
        [HttpPost]
        public ActionResult GetSimplePhotos(String mid, String phototype, String pagesize, String pageno)
        {
            return Json(GetSimplePhotoList(mid, phototype, pagesize, pageno));
        }
        [HttpPost]
        private String GetSimplePhotoList(String mid, String phototype, String pagesize, String pageno)
        {
            //照片类别
            int type = -1;
            if (phototype != null) Int32.TryParse(phototype, out type);

            int pagesizeint = 0;
            if (pagesize != null) Int32.TryParse(pagesize, out pagesizeint);

            int pagenoint = 0;
            if (pageno != null) Int32.TryParse(pageno, out pagenoint);

            IList<BiZ.Photo.PhotoSimple> list;
            if (type != -1)
                list = BiZ.Photo.PhotoManager.GetSimplePhotos(mid, (BiZ.Photo.PhotoType)type, pagesizeint, pagenoint);
            else
                list = BiZ.Photo.PhotoManager.GetSimplePhotos(mid, pagesizeint, pagenoint);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Serialize(list);
        }
        [Authorize]
        [HttpPost]
        public ActionResult memberphotolist(String mid, String phototype)
        {
            if (mid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            return Json(GetPhotoList(mid, phototype, 0, 0), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        private String GetPhotoList(String mid, String phototype, int pagesize, int pageno)
        {
            //照片类别
            int type = -1;
            if (phototype != null) Int32.TryParse(phototype, out type);

            IList<BiZ.Photo.Photo> list;
            if (type != -1)
                list = BiZ.Photo.PhotoManager.GetPhotos(mid, (BiZ.Photo.PhotoType)type, pagesize, pageno);
            else
                list = BiZ.Photo.PhotoManager.GetPhotos(mid, pagesize, pageno);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Serialize(list);
        }
        [Authorize]
        [HttpPost]
        public ActionResult getPhoto(String id)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Photo.Photo photo = BiZ.Photo.PhotoManager.GetPhoto(id);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            String infophotojson = ser.Serialize(photo);
            return Json(infophotojson, JsonRequestBehavior.AllowGet);
        }
        //通过图片名称获取图片
        [HttpPost]
        public ActionResult getNamePhoto(String name)
        {
            if (name == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Photo.Photo photo = BiZ.Photo.PhotoManager.GetNamePhoto(name);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            String infophotojson = ser.Serialize(photo);
            return Json(infophotojson, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        public ActionResult getNextPhoto(String id)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Photo.Photo photo = BiZ.Photo.PhotoManager.GetNextPhoto(id);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            String infophotojson = ser.Serialize(photo);
            return Json(infophotojson, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult updatePhoto(String id, String title, String content)
        {
            CBB.ExceptionHelper.OperationResult result = BiZ.Photo.PhotoManager.UpdatePhotoContent(id, title, content);

            return Json(result);
        }
        [Authorize]
        [HttpPost]
        public ActionResult viewPhoto(String id)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            CBB.ExceptionHelper.OperationResult result = BiZ.Photo.PhotoManager.ViewPhoto(id);

            return Json(result);
        }
        [Authorize]
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult addComment(String id, String content)
        {
            string memberID = HttpContext.User.Identity.Name;
            BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(memberID);
            string iconpath = "";
            if (me.MemberPhoto != null)
            {
                if (me.MemberPhoto.IconID != "")
                    iconpath = BiZ.Photo.PhotoManager.GetPhoto(me.MemberPhoto.IconID).FileName;
            }

            CBB.ExceptionHelper.OperationResult result = BiZ.Member.Activity.ActivityController.AddCommentToMemberPhoto(
                id,
                memberID,
                me.MemberInfomation.NickName,
                iconpath,
                content);

            return Json(result);
        }
        [Authorize]
        [HttpPost]
        public ActionResult delComment(String mid, String photoid, String createdtime)
        {
            string userid = HttpContext.User.Identity.Name;
            BiZ.Photo.Photo p = Moooyo.BiZ.Photo.PhotoManager.GetPhoto(photoid);
            if (p == null) return Json(new CBB.ExceptionHelper.OperationResult(false, "照片未找到"));
            if (p.MemberID != userid)
                return Json(new CBB.ExceptionHelper.OperationResult(false, "只能删除自己照片的评论"));

            DateTime dt = JsonToDateTime(createdtime);

            CBB.ExceptionHelper.OperationResult result = BiZ.Photo.PhotoManager.DelComment(
                photoid,
                userid,
                mid,
                dt.ToLocalTime());

            return Json(result);
        }
        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }
        [Authorize]
        [HttpPost]
        public ActionResult getCommentList(String id)
        {
            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            return Json(new JavaScriptSerializer().Serialize(BiZ.Photo.PhotoManager.GetPhotoComments(id)));
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Get(String id)
        {
            //设置客户端缓存
            String dataflag = "Sat, 07 Mar 2009 13:28:57 GMT";
            Response.AddHeader("Cache-Control", "max-age = 315360000000");
            Response.AddHeader("Last-Modified", dataflag);

            if (Request.Headers["If-Modified-Since"]!=null)
                if (Request.Headers["If-Modified-Since"].ToString().Trim() == dataflag)
            {
                Response.StatusCode = 304;
                return Content("");
            }

            if (id == null) return RedirectToAction("Error", "Error", new { errorno = "404" });
            byte[] stream = new byte[0];
            bool errflag = false;

            try
            {
                stream = new CBB.ImageHelper.ImageLoader().loadimage(id);
            }
            catch
            {
                errflag = true;
            }

            if (stream != null && !errflag)
            {
                return base.File(stream, "image/jpeg");
            }
            else
            {
                try
                {
                    FileStream fs = new FileStream(Server.MapPath("/pics/" + id), FileMode.Open, FileAccess.Read);
                    byte[] pReadByte = new byte[0];
                    BinaryReader r = new BinaryReader(fs);
                    r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                    pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                    return base.File(pReadByte, "image/jpeg");
                }
                catch
                {
                    return RedirectToAction("Error", "Error", new { errorno = "404" });
                }
            }
        }
        [HttpPost]
        public ActionResult BatchUpdatePhotoInfo(String photoIDs, String photoTitles, String photoContents)
        {
            CBB.ExceptionHelper.OperationResult result = BiZ.Photo.PhotoManager.BatchUpdatePhotoInfo(photoIDs, photoTitles, photoContents);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        #endregion
    }
}
