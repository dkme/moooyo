using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing;
using Moooyo.BiZ.PhotoCheck;

namespace Moooyo.WebUI.Controllers
{
    public class UpController : Controller
    {
        HttpPostedFileBase httpFile;

        #region 视图方法
        #endregion

        #region 数据与业务方法
        //[Authorize]
        [HttpPost]
        [Authorize]
        public ActionResult addphoto(String photoType)
        {
            if (HttpContext.User.Identity.Name == null)
                return Json(new int[] { -1 });

            string memberID = HttpContext.User.Identity.Name;
            int holderNo = 0;
            if (!Int32.TryParse(photoType, out holderNo))
                return Json(new int[] { -1 });

            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }
            httpFile = Request.Files[0];

            #region 微博分享时所需要的图片
            if (Session["shareWeiBoImage"] == null)
            {
                //删除临时图片文件夹中的图片
                //Common.FileManager.DeleteFiles(HttpContext.Server.MapPath("/upload/WeiBoUpImage/"), true);
                //设置临时图片的名称
                string imagename = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
                //保存图片到临时文件夹中
                httpFile.SaveAs(Server.MapPath("/upload/WeiBoUpImage/" + imagename));
                //记录当前发布的内容中的第一个图片的文件名
                Session["shareWeiBoImage"] = Server.MapPath("/upload/WeiBoUpImage/" + imagename);
            }
            #endregion

            Moooyo.BiZ.Photo.Photo myp = SavePhoto(memberID, holderNo, httpFile.InputStream, httpFile.FileName);
            return Json(myp);
        }
        /// <summary>
        /// 视频认证，认证时Flash调用的方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult addIdentificationPhoto()
        {
            try
            {
                string memberID = HttpContext.User.Identity.Name;

                byte[] bytes = readPostedFile();
                MemoryStream ms = new MemoryStream();
                ms = new System.IO.MemoryStream(bytes);
                //保存至本地文件夹 （测试用）
                //Image img = Image.FromStream(ms);
                //img.Save(Server.MapPath("~/temp_up_file/aaaup.jpg"));

                string filename = memberID + DateTime.Now.ToString("yyyymmddhhMMss") + DateTime.Now.Millisecond + ".jpg";
                Moooyo.BiZ.Photo.Photo myp = SavePhoto(memberID, 98, ms, filename);
                ms.Dispose();
                BiZ.Member.Member member = Moooyo.BiZ.MemberManager.MemberManager.GetMember(memberID);
                AddCheckPhoto(memberID, myp.FileName, member.MemberInfomation.IconPath);

                //增加用户动态到后台
                string operateUrl = "";
                if (member.UniqueNumber != null)
                    operateUrl = "/u/" + member.UniqueNumber.ConvertedID;
                else
                    operateUrl = "/Content/TaContent/" + member.ID;
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    memberID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.VideoCertificate,
                    operateUrl
                    );

                return Json(myp);
            }
            catch (Exception es)
            {
                string s = es.Message;
                return Json(s);
            }
        }
        //添加视频认证待审记录
        [ValidateInput(true)]
        private void AddCheckPhoto(string userid, string imgname, string userheadname)
        {
            PhotoCheckModel pcm = new PhotoCheckModel();
            pcm.CheckImgPath = imgname;
            pcm.CheckStatus = Convert.ToInt32(CheckPhotoStatus.waitaudit);
            pcm.JionTime = DateTime.Now;
            pcm.UserHeadName = userheadname;
            pcm.UserId = userid;
            new PhotoCheckOperation().AddCheckPhoto(pcm);
        }
        private byte[] readPostedFile()
        {
            if (Request.ContentLength > 0)
            {
                byte[] buffer = new byte[Request.ContentLength];
                using (BinaryReader br = new BinaryReader(Request.InputStream))
                    br.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            else
            {
                return null;
            }
        }
        [ValidateInput(true)]
        public Moooyo.BiZ.Photo.Photo SavePhoto(string memberID, int holderNo, Stream file, String filename)
        {
            Moooyo.BiZ.Photo.Photo myp = new BiZ.Photo.Photo();
            if (file != null && file.Length > 0 && !string.IsNullOrEmpty(filename))
            {
                string fname = filename;
                string ext = fname.Substring(fname.LastIndexOf('.') + 1);
                string month = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                string nFilename = month + "_" + CBB.Security.MD5Helper.getMd5Hash(fname + memberID + DateTime.Now.ToString()) + "." + ext;
                myp = Moooyo.BiZ.Photo.PhotoManager.AddPhoto(memberID, (BiZ.Photo.PhotoType)holderNo, ext, fname, ext, nFilename);
                myp.FileName = nFilename;
                CBB.ImageHelper.ImageWaterMark wm = new CBB.ImageHelper.ImageWaterMark();
                //打文字水印
                if (CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrOn") == "true")
                {
                    wm.WaterStrOn = true;
                    wm.WaterStrMarginRight = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginRight") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginRight"));
                    wm.WaterStrMarginBottom = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginBottom") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrMarginBottom"));
                    wm.WaterStrFontSize = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrFontSize");
                    wm.WaterStrFont = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStrFont");
                    wm.WaterStr = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterStr");
                }
                //打图片水印
                if (CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicOn") == "true")
                {
                    wm.WaterPicOn = true;
                    wm.WaterPicMarginRight = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginRight") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginRight"));
                    wm.WaterPicMarginBottom = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginBottom") == "" ? 5 : Int32.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicMarginBottom"));
                    wm.WaterPicPath = System.Web.Hosting.HostingEnvironment.MapPath(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("WaterPicPath"));
                }
                CBB.ImageHelper.ImageSizeType[] imagetypes;
                BiZ.Photo.PhotoType photoType = (BiZ.Photo.PhotoType)holderNo;

                if (photoType == BiZ.Photo.PhotoType.ImageContentPhoto || photoType == BiZ.Photo.PhotoType.SuoSuoContentPhoto || photoType == BiZ.Photo.PhotoType.CallForContentPhoto || photoType == BiZ.Photo.PhotoType.MemberSkinPersonalityPicture || photoType == BiZ.Photo.PhotoType.InterestSelfhoodPicture)
                {
                    imagetypes = SetImageSize(800, 800, wm);
                }
                else if (photoType == BiZ.Photo.PhotoType.MemberSkinPersonalityBackgroundPicture || photoType == BiZ.Photo.PhotoType.WebsiteHome)
                {
                    imagetypes = SetImageSize(1600, 1600, wm);
                }
                else
                {
                    imagetypes = SetImageSize(600, 600, wm);
                }

                CBB.ImageHelper.ImageUpload upload = new CBB.ImageHelper.ImageUpload();
                upload.AddImageToGridFS(nFilename, file, imagetypes);
            }

            return myp;
        }
        private CBB.ImageHelper.ImageSizeType[] SetImageSize(int artworkWidth, int artworkHeight, CBB.ImageHelper.ImageWaterMark wm)
        {
            CBB.ImageHelper.ImageSizeType[] imagetypes = null;

            imagetypes = new CBB.ImageHelper.ImageSizeType[]{
                    new CBB.ImageHelper.ImageSizeType(artworkWidth, artworkHeight, false,false, "", CBB.ImageHelper.ImageMakeMode.WButOnlyShrink, wm),
                    new CBB.ImageHelper.ImageSizeType(200,200,false,false,"_s", CBB.ImageHelper.ImageMakeMode.W,null),
                    new CBB.ImageHelper.ImageSizeType(150,150,false,false,"_p", CBB.ImageHelper.ImageMakeMode.Cut,null),
                    new CBB.ImageHelper.ImageSizeType(50,50,false,false,"_i", CBB.ImageHelper.ImageMakeMode.Cut,null)
                };
            
            return imagetypes;
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddPhotoAndSetIcon(String photoType)
        {
            if (HttpContext.User.Identity.Name == null)
                return Json(new int[] { -1 });

            string memberID = HttpContext.User.Identity.Name;

            int holderNo = 0;
            if (!Int32.TryParse(photoType, out holderNo))
                return Json(new int[] { -1 });

            if (Request.Files == null || Request.Files.Count == 0)
            {
                //上传文件为空，返回
                return Json(new int[] { -1 });
            }
            httpFile = Request.Files[0];
            Moooyo.BiZ.Photo.Photo myp = SavePhoto(memberID, holderNo, httpFile.InputStream, httpFile.FileName);
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetMemberIconPhoto(memberID, myp.ID);
            return Json(myp);
        }
        [HttpPost]
        [Authorize]
        public ActionResult delphoto(String photoID)
        {
            if (HttpContext.User.Identity.Name == null)
                return Json(new int[] { -1 });

            BiZ.Photo.Photo p = Moooyo.BiZ.Photo.PhotoManager.GetPhoto(photoID);

            CBB.ExceptionHelper.OperationResult result = Moooyo.BiZ.Photo.PhotoManager.DeletePhoto(
                HttpContext.User.Identity.Name,
                photoID);

            if (p != null)
            {
                if (result.ok)
                {
                    string filenamepart1 = p.FileName.Substring(0, p.FileName.LastIndexOf('.'));
                    new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(p.FileName);
                    new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(filenamepart1 + "_s.jpg");
                    new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(filenamepart1 + "_p.jpg");
                    new CBB.ImageHelper.ImageUpload().DelImageFromGridFS(filenamepart1 + "_i.jpg");
                }
            }
            return Json(result);
        }
        [HttpPost]
        [Authorize]
        public ActionResult buildcoverimg(String mid)
        {
            bool flag = BuildMemberCoverImage(mid,
                Server.MapPath(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("CoverBackground")),
                Server.MapPath(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("CoverFront")));

            return Json(new CBB.ExceptionHelper.OperationResult(flag));
        }
        public bool BuildMemberCoverImage(String mid, String coverimgbackground, String coverfront)
        {
            Image coverback;
            Graphics g;

            try
            {
                coverback = Image.FromFile(coverimgbackground);
            }
            catch
            { return false; };

            if (coverback == null) return false;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);
            if (mym.MemberPhoto.IconID == "") return false;

            //建立背景
            g = System.Drawing.Graphics.FromImage(coverback);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //加入用户头像
            byte[] stream = new CBB.ImageHelper.ImageLoader().loadimage(mym.MemberInfomation.IconPath.Split('.')[0] + "_p.jpg");
            Stream st = new MemoryStream(stream);

            Image membericon = Image.FromStream(st);

            g.DrawImage(membericon,
                        new Rectangle(108, 44, membericon.Width, membericon.Height),
                        0, 0,
                        membericon.Width, membericon.Height,
                        GraphicsUnit.Pixel);

            //加入头像前遮罩
            if (coverfront != "")
            {
                try
                {
                    Image coverfrontimg = Image.FromFile(coverfront);
                    g.DrawImage(coverfrontimg,
                        new Rectangle(108, 44, coverfrontimg.Width, coverfrontimg.Height),
                        0, 0,
                        coverfrontimg.Width, coverfrontimg.Height,
                        GraphicsUnit.Pixel);
                }
                catch { }
            }

            //String Str1 = "我在米柚（Moooyo.com）获得";
            //String Str2 = mym.Status.ScoreAvg.ToString("#0.0") + "分";

            //g.DrawString(Str1,
            //     new Font("黑体", 20, GraphicsUnit.Pixel),
            //     new SolidBrush(Color.Black),
            //     new PointF(230, 58));

            //g.DrawString(Str2,
            //     new Font("黑体", 50, GraphicsUnit.Pixel),
            //     new SolidBrush(Color.Red),
            //     new PointF(310, 98));

            //String[] colors = new string[] { "#23C619", "#03642A", "#FF3399", "#933605", "#0066CC" };
            //int[] lefts = new int[] { 110, 140, 40, 120, 50 };

            //for (int i = 0; i < mym.MemberRelations.Marks.Count & i < 5; i++)
            //{
            //    g.DrawString(mym.MemberRelations.Marks[i].ContentCove,
            //         new Font("黑体", 18, GraphicsUnit.Pixel),
            //         new SolidBrush(ColorTranslator.FromHtml(colors[i])),
            //         new PointF(lefts[i], 200 + i * 35));
            //}

            g.Dispose();

            MemoryStream imagestream = new MemoryStream();
            coverback.Save(imagestream, System.Drawing.Imaging.ImageFormat.Jpeg);
            CBB.MongoDB.GridFSHelper.UploadFile(imagestream, "cover_" + mym.ID + ".jpg");
            coverback.Dispose();
            imagestream.Dispose();

            return true;
        }
        #endregion   
    }
}
