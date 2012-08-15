using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace Moooyo.WebUI.Controllers
{
    public class SharedController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        /// <summary>
        /// 带页码的分页控件
        /// </summary>
        /// <param name="nowpage"></param>
        /// <param name="pagecount"></param>
        /// <param name="additionID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ActionResult pagger(string nowpage, string pagecount, string additionID, string url)
        {
            int now = 0;
            Int32.TryParse(nowpage, out now);
            int count = 0;
            Int32.TryParse(pagecount, out count);
            if (now < 1 || count < 1) return View();

            string pagger = "";
            if (now == 1)
                pagger += "<span class='prev'>〈前页</span>";
            else
                pagger += "<a href='" + url + "/" + getadditionID(additionID) + (now - 1).ToString() + "' data-i='" + getadditionID(additionID) + (now - 1).ToString() + "'>〈前页</a>";

            if (count < 11)
            {
                for (int i = 1; i <= count & i < 11; i++)
                {
                    if (now != i)
                        pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                    else
                        pagger += "<span class='thispage'>" + i + "</span>";
                }
            }
            else
            {
                int begin = now - 4;
                int end = now + 4;
                for (int i = 1; i <= count; i++)
                {

                    if (now < 6 & i < 10)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        if (i == 9 & count > 12)
                            pagger += "<span class='break'> ... </span>";

                        continue;
                    }
                    if (now > count - 7 & i > count - 10)
                    {
                        if (i == count - 9 & count > 12)
                            pagger += "<span class='break'> ... </span>";

                        if (now != i)
                            pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        continue;
                    }

                    if ((i <= begin || i > end) & i == count - 10) continue;

                    if (i < 3)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        continue;
                    }

                    if (i >= begin & i <= end)
                    {
                        if (i == begin)
                            pagger += "<span class='break'> ... </span>";

                        if (now != i)
                            pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                        {
                            pagger += "<span class='thispage'>" + i + "</span>";
                        }
                        if (i == end)
                            pagger += "<span class='break'> ... </span>";

                        continue;
                    }

                    if (i > count - 2)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + "/" + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + getadditionID(additionID) + i + "</span>";

                        continue;
                    }
                }
            }

            if (now == count)
                pagger += "<span class='next'>后页〉</span>";
            else
                pagger += "<a href='" + url + "/" + getadditionID(additionID) + (now + 1).ToString() + "' data-i='" + getadditionID(additionID) + (now + 1).ToString() + "'>后页〉</a>";

            ViewData["pagger"] = pagger;
            return View();
        }
        /// <summary>
        /// 无页码的分页控件
        /// </summary>
        /// <param name="nowpage"></param>
        /// <param name="pagecount"></param>
        /// <param name="additionID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult SmallPagger(string nowpage, string pagecount, string additionID, string url)
        {
            int now = 0;
            Int32.TryParse(nowpage, out now);
            int count = 0;
            Int32.TryParse(pagecount, out count);
            if (now < 1 || count < 1) return View();

            string pagger = "";
            pagger += "<a href='" + url + "/1' data-i='" + getadditionID(additionID) + (now - 1).ToString() + "'>〈〈首页</a>";
            if (now == 1)
            {
                pagger += "<span class='prev'>〈前页</span>";
            }
            else
            {
                pagger += "<a href='" + url + "/" + getadditionID(additionID) + (now - 1).ToString() + "' data-i='" + getadditionID(additionID) + (now - 1).ToString() + "'>〈前页</a>";
            }
            if (now == count)
            {
                pagger += "<span class='next'>后页〉</span>";
            }
            else
            {
                pagger += "<a href='" + url + "/" + getadditionID(additionID) + (now + 1).ToString() + "' data-i='" + getadditionID(additionID) + (now + 1).ToString() + "'>后页〉</a>";
            }
            pagger += "<a href='" + url + "/" + count + "' data-i='" + getadditionID(additionID) + (now - 1).ToString() + "'>尾页〉〉</a>";
            ViewData["spagger"] = pagger;
            return View();
        }
        /// <summary>
        /// 带页码的分页控件
        /// </summary>
        /// <param name="nowpage"></param>
        /// <param name="pagecount"></param>
        /// <param name="additionID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult QueryStrPaging(string nowpage, string pagecount, string additionID, string url)
        {
            int now = 0;
            Int32.TryParse(nowpage, out now);
            int count = 0;
            Int32.TryParse(pagecount, out count);
            if (now < 1 || count < 1) return View();

            string pagger = "";
            if (now == 1)
                pagger += "<span class='prev'>〈前页</span>";
            else
                pagger += "<a href='" + url + getadditionID(additionID) + (now - 1).ToString() + "' data-i='" + getadditionID(additionID) + (now - 1).ToString() + "'>〈前页</a>";

            if (count < 11)
            {
                for (int i = 1; i <= count & i < 11; i++)
                {
                    if (now != i)
                        pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                    else
                        pagger += "<span class='thispage'>" + i + "</span>";
                }
            }
            else
            {
                int begin = now - 4;
                int end = now + 4;
                for (int i = 1; i <= count; i++)
                {

                    if (now < 6 & i < 10)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        if (i == 9 & count > 12)
                            pagger += "<span class='break'> ... </span>";

                        continue;
                    }
                    if (now > count - 7 & i > count - 10)
                    {
                        if (i == count - 9 & count > 12)
                            pagger += "<span class='break'> ... </span>";

                        if (now != i)
                            pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        continue;
                    }

                    if ((i <= begin || i > end) & i == count - 10) continue;

                    if (i < 3)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + i + "</span>";

                        continue;
                    }

                    if (i >= begin & i <= end)
                    {
                        if (i == begin)
                            pagger += "<span class='break'> ... </span>";

                        if (now != i)
                            pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                        {
                            pagger += "<span class='thispage'>" + i + "</span>";
                        }
                        if (i == end)
                            pagger += "<span class='break'> ... </span>";

                        continue;
                    }

                    if (i > count - 2)
                    {
                        if (now != i)
                            pagger += "<a href='" + url + getadditionID(additionID) + i + "' data-i='" + getadditionID(additionID) + i + "'>" + i + "</a>";
                        else
                            pagger += "<span class='thispage'>" + getadditionID(additionID) + i + "</span>";

                        continue;
                    }
                }
            }

            if (now == count)
                pagger += "<span class='next'>后页〉</span>";
            else
                pagger += "<a href='" + url + getadditionID(additionID) + (now + 1).ToString() + "' data-i='" + getadditionID(additionID) + (now + 1).ToString() + "'>后页〉</a>";

            ViewData["paging"] = pagger;
            return View();
        }
        /// <summary>
        /// 剪裁较小图片
        /// </summary>
        /// <param name="x">剪裁区域X坐标</param>
        /// <param name="y">剪裁区域Y坐标</param>
        /// <param name="w">剪裁区域宽</param>
        /// <param name="h">剪裁区域高</param>
        /// <param name="uploadPhoto">图片名称</param>
        /// <param name="getjump">上传后转向地址</param>
        /// <param name="phototype">图片类型</param>
        /// <param name="skintype">用户皮肤类型</param>
        /// <param name="pictureproportion">图片比例</param>
        /// <returns>视图或转向</returns>
        [Authorize]
        public ActionResult CustomSmallPicture(
            String x, String y, String w, String h, String uploadPhoto, String getjump, String phototype, String skintype, String pictureproportion)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //String image = "";
            string memberID = HttpContext.User.Identity.Name;
            String photoRelativePath = Request.QueryString["photoRelativePath"];
            //将上传后转向地址和图片比例传到前台
            ViewData["getjump"] = getjump != null ? getjump : "";
            ViewData["pictureProportion"] = pictureproportion != null ? pictureproportion : "";

            //剪裁图片
            string picturePathObj = CustomPicture(memberID, x, y, w, h, uploadPhoto, phototype, skintype, photoRelativePath);
            string pictureId = picturePathObj.Split('|')[0];
            string picturePath = picturePathObj.Split('|')[1];

            //ViewData["imagePath"] = image;

            if (x != null && y != null && w != null && h != null && uploadPhoto != null && phototype != null && getjump != null)
            {
                //上传后转向
                if (getjump != "" && getjump != null)
                    return Content("<script type=\"text/javascript\">window.parent.location=\"" + getjump + "\"; </script>");
                else
                    return Content("<script type=\"text/javascript\">window.parent.trimmedPictureAfter('" + pictureId + "', '" + picturePath + "');window.parent.jBox.close(true);</script>");
            }

            if (phototype == "1")
            {
                ViewData["isMemberUploadAvatar"] = "true";
                string memberBindedPlatforms = Models.DisplayObjProvider.MemberBindingPlatform(memberID);
                ViewData["memberBindedPlatforms"] = memberBindedPlatforms;
                ViewData["memberId"] = memberID;
                BiZ.Member.MemberInfomation mInfo = BiZ.MemberManager.MemberManager.GetMemberInfomation(memberID);
                ViewData["memberName"] = mInfo.NickName;
            }
            else
                ViewData["isMemberUploadAvatar"] = "false";

            return View();
        }
        /// <summary>
        /// 剪裁较大图片
        /// </summary>
        /// <param name="x">剪裁区域X坐标</param>
        /// <param name="y">剪裁区域Y坐标</param>
        /// <param name="w">剪裁区域宽</param>
        /// <param name="h">剪裁区域高</param>
        /// <param name="uploadPhoto">图片名称</param>
        /// <param name="getjump">上传后转向地址</param>
        /// <param name="phototype">图片类型</param>
        /// <param name="skintype">用户皮肤类型</param>
        /// <param name="pictureproportion">图片比例</param>
        /// <param name="defaultwidth">默认剪裁区域宽</param>
        /// <param name="defaultheight">默认剪裁区域高</param>
        /// <returns>视图或转向</returns>
        [Authorize]
        public ActionResult CustomBigPicture(
            String x, String y, String w, String h, String uploadPhoto, String getjump, String phototype, String skintype, String pictureproportion, String defaultwidth, String defaultheight, String contentid)
        {
            #region metas version
            SetMetasVersion();
            #endregion

            //String image = "";
            string memberID = HttpContext.User.Identity.Name;
            String picturePathObj = "";
            String photoRelativePath = Request.QueryString["photoRelativePath"];
            ViewData["skinType"] = skintype != null ? skintype : "";
            ViewData["pictureProportion"] = pictureproportion != null ? pictureproportion : "";
            ViewData["defaultwidth"] = defaultwidth != null ? defaultwidth : "150";
            ViewData["defaultheight"] = defaultheight != null ? defaultheight : "150";
            ViewData["contentid"] = contentid != null ? contentid : "";

            //剪裁图片
            picturePathObj = CustomPicture(memberID, x, y, w, h, uploadPhoto, phototype, skintype, photoRelativePath
                //, contentid
                );
            string pictureId = picturePathObj.Split('|')[0];
            string picturePath = picturePathObj.Split('|')[1];
            ViewData["photoNamePath"] = picturePathObj.Split('|')[1];

            //ViewData["imagePath"] = image;
            int photType = 0;
            Int32.TryParse(phototype, out photType);
            if (photType >= 201 && photType <= 203 && w != null && h != null)
            {
                getjump += "?photo=" + picturePath + "&ctd=" + contentid;
            }
            ViewData["getjump"] = getjump != null ? getjump : "";

            if (x != null && y != null && w != null && h != null && uploadPhoto != null && phototype != null && getjump != null)
            {
                if (getjump != "" && getjump != null)
                    return Content("<script type=\"text/javascript\">window.parent.location=\"" + getjump + "\"; </script>");
                else
                    return Content("<script type=\"text/javascript\">window.parent.trimmedPictureAfter('" + pictureId + "', '" + picturePath + "');window.parent.jBox.close(true);</script>");
            }

            return View();
        }
        #endregion

        #region 数据与业务方法
        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <param name="x">剪裁区域X坐标</param>
        /// <param name="y">剪裁区域Y坐标</param>
        /// <param name="w">剪裁区域宽</param>
        /// <param name="h">剪裁区域高</param>
        /// <param name="uploadPhoto">图片名称</param>
        /// <param name="phototype">图片类型</param>
        /// <param name="skintype">用户皮肤类型</param>
        /// <param name="photoRelativePath">图片虚拟路径</param>
        /// <returns>操作状态</returns>
        private String CustomPicture(
            String memberID, String x, String y, String w, String h, String uploadPhoto, 
            String phototype, String skintype, String photoRelativePath
            //, String contentId
            )
        {
            CBB.ExceptionHelper.OperationResult result = null;
            Moooyo.BiZ.Photo.Photo myp = new BiZ.Photo.Photo();
            ViewData["photoRelativePath"] = photoRelativePath != null ? photoRelativePath : "";
            ViewData["phototype"] = phototype;
            ViewData["skinType"] = skintype;

            if (x != null && y != null && w != null && h != null && uploadPhoto != null && phototype != null)
            {
                int photType = Convert.ToInt32(phototype);

                int xCoord = int.Parse(x);
                int yCoord = int.Parse(y);
                int width = int.Parse(w);
                int height = int.Parse(h);
                //裁图并保存图片
                String photoPath = CropImage(uploadPhoto, width, height, xCoord, yCoord, memberID);
                if (photoPath == "error")
                {
                    //return new CBB.ExceptionHelper.OperationResult(false, "图片有误");
                    return "";
                }

                //uploadPhoto = uploadPhoto.Replace("original", "modifiedsize");
                String strPhotoPath = System.Web.Hosting.HostingEnvironment.MapPath(photoPath);
                //用文件流的方式打开该张图片
                System.IO.FileStream tfs = System.IO.File.Open(strPhotoPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                String strRelativePath = "/temp_up_file/";
                int strRelativePathLength = strRelativePath.Length;
                UpController upControl = new UpController();

                //保存图片数据到数据库
                myp = upControl.SavePhoto(memberID, Convert.ToInt32(phototype), tfs, photoPath.Substring(strRelativePathLength, photoPath.Length - strRelativePathLength));
                //图片类型是用户头像类型
                if (photType == 1)
                {
                    BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler modiPicPathHand = 
                        new BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler();
                    modiPicPathHand.MemberId = memberID;
                    modiPicPathHand.PictureId = myp.ID;
                    modiPicPathHand.PicturePath = myp.FileName;
                    modiPicPathHand.ModifyPicPathEvent +=
                        new BiZ.Member.ModifyPicturePath.ModifyPicturePathHandler.ModifyPicPathEventHandler(
                            new BiZ.Member.ModifyPicturePath.ModifyPicturePathProvider().ModifyMemberAvatarPicPath
                            ); //注册方法
                    modiPicPathHand.ModifyPicPath(); //自动调用注册过对象的方法

                    //同步发布说说
                    IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberID, 0, 0);
                    String interestIds = "";
                    if (interestList != null)
                    {
                        foreach (BiZ.InterestCenter.Interest interest in interestList)
                        {
                            interestIds += interest.ID + ",";
                        }
                    }
                    String userID = memberID, permissions = "0", interestids = interestIds, lat = "0", lng = "0", type = "修改个人头像";
                    BiZ.Content.MemberContent imageContent = null;
                    if (userID != "" && permissions != "" && interestids != "" && lat != "" && lng != "" && type != "")
                    {
                        //创建兴趣操作内容对象
                        imageContent = new BiZ.Content.MemberContent(userID, ContentController.getContentPerMissions(permissions), interestids.Split(',').ToList(), Double.Parse(lat), Double.Parse(lng), type);
                        //保存兴趣操作内容对象
                        imageContent = imageContent.Save(imageContent);
                        //判断是否是新用户
                        Boolean ifnewmember = BiZ.MemberManager.MemberManager.getMemberToNew(userID) != null ? false : true;
                        if (ifnewmember)
                        {
                            BiZ.Member.MemberToNew newmember = new BiZ.Member.MemberToNew(userID, imageContent.ID, BiZ.Member.MemberToNewType.ImageContent);
                            newmember.Save(newmember);
                        }
                    }

                    BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(memberID);

                    //增加动态
                    BiZ.Member.Activity.ActivityController.AddActivity(
                        memberID,
                        BiZ.Member.Activity.ActivityType.SetICON,
                        BiZ.Member.Activity.ActivityController.GetActivityContent_SetICON_Title(),
                        BiZ.Member.Activity.ActivityController.GetActivityContent_SetICON(member),
                        true);
                    
                    //增加用户动态到后台
                    string operateUrl = "";
                    if (member.UniqueNumber != null)
                        operateUrl = "/u/" + member.UniqueNumber.ConvertedID;
                    else
                        operateUrl = "/Content/TaContent/" + member.ID;
                    BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                        member.ID,
                        "",
                        BiZ.Sys.MemberActivity.MemberActivityType.UploadAvatar,
                        operateUrl);

                    string filename = UploadPictureToTemporary(memberID, member.MemberInfomation.IconPath);
                    Session["shareWeiBoImage"] = filename;

                    ////上传头像分享到外部平台
                    //string bindedPlatforms = Models.DisplayObjProvider.MemberBindingPlatform(member.ID);
                    //MicroConnController mcc = new MicroConnController();
                    //mcc.PlatformShareInfo(
                    //    member.ID, 
                    //    bindedPlatforms, 
                    //    member.MemberInfomation.NickName, 
                    //    Server.MapPath(strPhotoPath.Replace("modifiedsize", "original")), 
                    //    "/Content/ContentDetail/" + imageContent.ID
                    //    );
                    
                }
                ////图片类型内容图片
                //if (photType >= 201 && photType <= 203 && contentId != null)
                //{
                //    List<BiZ.Content.Image> listImage = new List<BiZ.Content.Image>();
                //    BiZ.Content.Image image = new BiZ.Content.Image(myp.FileName);
                //    listImage.Add(image);
                //    BiZ.TopImagePush.ImagePush imagePush = new BiZ.TopImagePush.ImagePush(contentId, memberID, listImage, 
                //}

                //构造源图片成FileInfo对象
                System.IO.FileInfo fileOriginalPhoto = new System.IO.FileInfo(strPhotoPath.Replace("modifiedsize", "original"));
                tfs.Dispose();
                tfs.Close();
                System.GC.Collect(); //强制对所有代码进行即时垃圾回收*（不回收就会被iis进程占用文件）
                //如果文件已存在，则删除
                if (fileOriginalPhoto.Exists) fileOriginalPhoto.Delete(); //删除单个文件(original)

                //{
                //    System.IO.File.Delete(strPhotoPath.Replace("modifiedsize", "original"));
                //}
                System.IO.File.Delete(strPhotoPath); //删除临时文件夹的剪裁后的图片(modifiedsize)

                //设置个性图片
                if (photType == 11 || photType == 12)
                {
                    if (skintype != null && skintype != "")
                    {
                        //设置个性图片
                        AddMemberSkinPicture(memberID, skintype, myp);
                    }
                }
            }
            
            return myp.ID + "|" + myp.FileName;
        }
        private string UploadPictureToTemporary(string memberId, string picturePath)
        {
            byte[] stream = new CBB.ImageHelper.ImageLoader().loadimage(picturePath.Split('.')[0] + "_p.jpg");
            Stream st = new MemoryStream(stream);
            Image membericon = Image.FromStream(st);
            //图片的到临时文件夹
            string filename = HttpContext.Server.MapPath("/temp_up_file/" + (DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString()) + ".jpg");
            membericon.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            return filename;
        }
        /// <summary>
        /// 设置个性图片
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <param name="skintype">用户皮肤类型</param>
        /// <param name="myp">图片对象</param>
        /// <returns>操作状态</returns>
        private CBB.ExceptionHelper.OperationResult SetMemberSkinPicture(String memberID, String skintype, Moooyo.BiZ.Photo.Photo myp)
        {
            CBB.ExceptionHelper.OperationResult result = null;

            if (skintype != "" && skintype != null)
            {
                switch (skintype)
                {
                    case "PersonalityPicture":
                        //设置个性图片
                        result = SettingController.SetMemberSkinPicture(
                            memberID,
                            myp.FileName,
                            BiZ.Member.MemberSkin.MemberSkinType.PersonalityPicture);
                        break;
                    case "PersonalityBackgroundPicture":
                        //设置个性背景图片
                        result = SettingController.SetMemberSkinPicture(
                            memberID,
                            myp.FileName,
                            BiZ.Member.MemberSkin.MemberSkinType.PersonalityBackgroundPicture);
                        break;
                    default: break;
                }
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        /// 添加个性图片
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <param name="skintype">用户皮肤类型</param>
        /// <param name="myp">图片对象</param>
        /// <returns>操作状态</returns>
        private CBB.ExceptionHelper.OperationResult AddMemberSkinPicture(String memberID, String skintype, Moooyo.BiZ.Photo.Photo myp)
        {
            CBB.ExceptionHelper.OperationResult result = null;
            string personalityPicture = "",
                personalityBackgroundPicture = "";

            if (skintype != "" && skintype != null)
            {
                switch (skintype)
                {
                    case "PersonalityPicture":
                        ////设置个性图片
                        //result = SettingController.SetMemberSkinPicture(
                        //    memberID,
                        //    myp.FileName,
                        //    BiZ.Member.MemberSkin.MemberSkinType.PersonalityPicture);

                        personalityPicture = myp.FileName;
                        break;
                    case "PersonalityBackgroundPicture":
                        ////设置个性背景图片
                        //result = SettingController.SetMemberSkinPicture(
                        //    memberID,
                        //    myp.FileName,
                        //    BiZ.Member.MemberSkin.MemberSkinType.PersonalityBackgroundPicture);

                        personalityBackgroundPicture = myp.FileName;
                        break;
                    default: break;
                }

                //添加用户皮肤
                BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
                    memberID,
                    BiZ.Comm.UserType.Member,
                    personalityPicture,
                    personalityBackgroundPicture
                    );
                if (ms != null)
                    result = new CBB.ExceptionHelper.OperationResult(true);
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public String getadditionID(String id)
        {
            if (id != null & id != "")
            {
                return id + "/";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 按虚拟路径获取上传路径并保存图片
        /// </summary>
        /// <param name="photoNamePath">图片虚拟路径和名称</param>
        /// <param name="memberID">用户编号</param>
        /// <returns>存储后的图片路径</returns>
        [Authorize]
        public ActionResult SavePathPhotoGetUploadedPath(String photoNamePath, String memberID)
        {
            if (memberID == null || memberID == "") memberID = HttpContext.User.Identity.Name;
            string picturePathAndState = SavePathPhotoGetUploadedPath2(photoNamePath, memberID);
            string sFilePath = picturePathAndState.Split('|')[0];
            string state = picturePathAndState.Split('|')[1];
            return Content("{\"filePath\":\"" + sFilePath + "\",\"status\":\"" + state + "\"}");
        }
        public string SavePathPhotoGetUploadedPath2(String photoNamePath, String memberID)
        {
            //String uploadPath = "/temp_up_file/";   //保存路径
            String fileType = ".jpg,.gif,.png";   //文件允许格式
            Int32 fileSize = 5000;    //文件大小限制，单位5KB
            string state = "SUCCESS";   //文件上传状态,当成功时返回SUCCESS，其余值将直接返回对应字符窜并显示在图片预览框，同时可以在前端页面通过回调函数获取对应字符窜
            string sFilePath = "";
            String infoma;
            photoNamePath = photoNamePath.ToLower();
            if (photoNamePath.IndexOf("/photo/get/") >= 0)
            {
                string urlStr = HttpContext.Request.Url.AbsoluteUri.ToString();
                urlStr = urlStr.Substring(0, urlStr.IndexOf('/', 7));

                String photoNameFullPath = urlStr + photoNamePath;
                System.Net.WebRequest webReqt = System.Net.WebRequest.Create(photoNameFullPath);
                webReqt.Timeout = 1000;
                System.Net.HttpWebResponse wresp = (System.Net.HttpWebResponse)webReqt.GetResponse();
                System.IO.Stream stream = wresp.GetResponseStream();
                System.Drawing.Image image;
                image = System.Drawing.Image.FromStream(stream);

                if (image != null)
                {
                    string fileExtension = System.IO.Path.GetExtension(photoNamePath).ToLower();

                    if (fileType.ToLower().IndexOf(fileExtension) > -1)//检测是否为允许的上传文件类型
                    {
                        //if (fileSize * 1024 >= stream.Length)
                        //{
                        try
                        {
                            String sFileName = memberID + "_original";
                            string FullPath = "~/temp_up_file/" + sFileName + fileExtension;//最终文件路径
                            image.Save(Server.MapPath(FullPath));
                            sFilePath = "/temp_up_file/" + sFileName + fileExtension;
                            wresp.Close();
                            stream.Dispose();
                            stream.Close();
                            image.Dispose();
                        }
                        catch (Exception)
                        {
                            state = "failure";
                            infoma = "文件上传失败";
                        }
                        //}
                        //else
                        //{
                        //    state = "failure";
                        //    infoma = "图片大小超出限制";
                        //}
                    }
                    else
                    {
                        state = "failure";
                        infoma = "图片类型错误";
                    }
                }
            }
            return sFilePath + "|" + state;
        }
        /// <summary>
        /// 剪裁图像
        /// </summary>
        /// <param name="Img">图片名称</param>
        /// <param name="Width">图片宽</param>
        /// <param name="Height">图片高</param>
        /// <param name="X">图片X坐标</param>
        /// <param name="Y">图片Y坐标</param>
        /// <returns>图片相对路径</returns>
        private String CropImage(string Img, int Width, int Height, int X, int Y, String memberID)
        {
            try
            {
                String strPhotoPath = "";
                //if (Img.IndexOf("/photo/get/") >= 0) strPhotoPath = "http://www.moooyo.com" + Img;
                //else strPhotoPath = System.Web.Hosting.HostingEnvironment.MapPath(Img);
                strPhotoPath = System.Web.Hosting.HostingEnvironment.MapPath(Img);
                using (System.Drawing.Bitmap OriginalImage = new System.Drawing.Bitmap(strPhotoPath))
                {
                    using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Width, Height, OriginalImage.PixelFormat))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        if (!new CBB.ImageHelper.PixelFormatIndexed().IsPixelFormatIndexed(bmp.PixelFormat))
                        {
                            using (System.Drawing.Graphics Graphic = System.Drawing.Graphics.FromImage(bmp))
                            {
                                Graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                Graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                Graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                Graphic.DrawImage(OriginalImage, new System.Drawing.Rectangle(0, 0, Width, Height), X, Y, Width, Height,
                                                  System.Drawing.GraphicsUnit.Pixel);
                                string fileExtension = System.IO.Path.GetExtension(Img).ToLower();
                                string savePath = "/temp_up_file/" + memberID + "_modifiedsize" + fileExtension;
                                //保存图片
                                bmp.Save(Server.MapPath(savePath), OriginalImage.RawFormat);

                                OriginalImage.Dispose();
                                bmp.Dispose();
                                Graphic.Dispose();
                                System.GC.Collect(); //强制对所有代码进行即时垃圾回收*（不回收就会被iis进程占用文件）
                                return savePath;
                            }
                        }
                        else
                        {
                            return "error";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 上传临时需要剪裁的图片
        /// </summary>
        /// <param name="post">上传文件集合</param>
        /// <param name="photoNamePath">图片名称和路径</param>
        /// <param name="pictureType">图片类型</param>
        /// <returns>图片文件名称和路径和状态信息</returns>
        [Authorize]
        public ActionResult UploadTempCustomPhoto(FormCollection post, String photoNamePath, String pictureType)
        {
            if (HttpContext.User.Identity.Name == null)
                return Json(new int[] { -1 });

            string memberID = HttpContext.User.Identity.Name;
            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }

            //String uploadPath = "/temp_up_file/";   //保存路径
            String fileType = ".jpg,.gif,.png";   //文件允许格式
            Int32 fileSize = 5000;    //文件大小限制，单位5KB
            string state = "SUCCESS";   //文件上传状态,当成功时返回SUCCESS，其余值将直接返回对应字符串并显示在图片预览框，同时可以在前端页面通过回调函数获取对应字符串
            string sFilePath = "";

            HttpPostedFileBase oFile = Request.Files[0];
            //if(oFile == null) System.Drawing.Image image = System.Drawing.Image.FromStream(file);
            if (oFile != null)
            {
                string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower();

                if (fileType.ToLower().IndexOf(fileExtension) > -1)//检测是否为允许的上传文件类型
                {
                    //检测文件大小是否超过限制
                    if (fileSize * 1024 >= oFile.ContentLength)
                    {
                        try
                        {
                            String sFileName = memberID + "_original";
                            string FullPath = "~/temp_up_file/" + sFileName + fileExtension;//最终文件路径
                            //保存文件
                            oFile.SaveAs(Server.MapPath(FullPath));
                            
                            String strFullPath = Server.MapPath(FullPath);

                            sFilePath = "/temp_up_file/" + sFileName + fileExtension;

                            if (pictureType != null)
                            {
                                //按图片路径创建图片对象
                                System.Drawing.Image image = System.Drawing.Image.FromFile(strFullPath);

                                //如果是兴趣个性图片
                                if (pictureType == "31")
                                {
                                    if (image.Width < 800 || image.Height < 250)
                                    {
                                        image.Dispose();
                                        state = "图片宽应大于等于800像素、高大于等于250像素，请重新上传！";
                                        //删除该刚上传的文件
                                        System.IO.File.Delete(strFullPath);
                                        System.GC.Collect(); //强制对所有代码进行即时垃圾回收*（不回收就会被iis进程占用文件）
                                        return Content(sFilePath + "|" + state);
                                    }
                                }
                                else
                                {
                                    //如果是用户个性图片
                                    //图片宽高限制
                                    switch (pictureType)
                                    {
                                        case "PersonalityPicture":
                                            if (image.Width < 766 || image.Height < 200)
                                            {
                                                image.Dispose();
                                                state = "图片宽应大于766像素、高大于200像素，请重新上传！";
                                                //删除该刚上传的文件
                                                System.IO.File.Delete(strFullPath);
                                                System.GC.Collect(); //强制对所有代码进行即时垃圾回收*（不回收就会被iis进程占用文件）
                                                return Content(sFilePath + "|" + state);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            state = "文件上传失败！";

                            throw new CBB.ExceptionHelper.OperationException(
                                CBB.ExceptionHelper.ErrType.SystemErr,
                                CBB.ExceptionHelper.ErrNo.FileUpload,
                                ex);
                        }
                    }
                    else
                    {
                        state = "图片大小超出限制！";
                    }
                }
                else
                {
                    state = "图片类型错误！";
                }
            }

            //返回数据
            //return Json(new JavaScriptSerializer().Serialize(new { filePath = sFilePath, state = state }));
            System.GC.Collect(); //强制对所有代码进行即时垃圾回收*（不回收就会被iis进程占用文件）
            return Content(sFilePath + "|" + state);
        }
        #endregion
    }
}
