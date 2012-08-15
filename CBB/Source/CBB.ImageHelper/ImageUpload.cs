using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CBB.ImageHelper
{
    public class PixelFormatIndexed
    {
        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare, PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed };
        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        public Boolean IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }
            return false;
        }
    }
    public class ImageUpload
    {
        public void AddImageToGridFS(String filename,Stream file,ImageSizeType[] imagesizes)
        {
            Image im = Image.FromStream(file);
            string filenamepart1 = filename.Substring(0,filename.LastIndexOf('.'));
            foreach (ImageSizeType ist in imagesizes)
            {
                Image imsmall = CBB.ImageHelper.ImageSizeHelper.MakeThumbnail(ist.width, ist.height, ist.type, im,ist.watermark);
                saveFile(filenamepart1, ist, imsmall);
            }
        }
        public void DelImageFromGridFS(String filename)
        {
            CBB.MongoDB.GridFSHelper.DelFile(filename);
        }
        private static void saveFile(string filenamepart1, ImageSizeType ist, Image imsmall)
        {
            MemoryStream imagestream = new MemoryStream();
            imsmall.Save(imagestream, System.Drawing.Imaging.ImageFormat.Jpeg);
            CBB.MongoDB.GridFSHelper.UploadFile(imagestream, filenamepart1 + ist.extname + ".jpg");
            imsmall.Dispose();
            imagestream.Dispose();
        }
    }
    public class ImageSizeType
    {
        public int width;
        public int height;
        public bool markSuare = false;
        public bool cut = false;
        public string extname;
        public ImageMakeMode type;
        public ImageWaterMark watermark;

        public ImageSizeType(int width, int height, bool markSuare, bool cut, string extname, ImageMakeMode type,ImageWaterMark watermark)
        {
            this.width = width;
            this.height = height;
            this.markSuare = markSuare;
            this.cut = cut;
            this.extname = extname;
            this.type = type;
            this.watermark = watermark;
        }
    
    }
}
