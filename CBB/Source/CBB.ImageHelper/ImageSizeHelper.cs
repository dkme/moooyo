using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace CBB.ImageHelper
{
    /// <summary>
    /// 图片缩小方法
    /// </summary>
    public class ImageSizeHelper
    {
        #region SaveSmallPhoto
        /// <summary>
        /// 高清晰缩略图
        /// </summary>
        /// <param name="oPath">原文件路径</param>
        /// <param name="oFileName">原文件名</param>
        /// <param name="iconSavePath">目标文件路径</param>
        /// <param name="fileNameAddStr">目标文件名附加字符串</param>
        /// <param name="Width">目标高度</param>
        /// <param name="Height">目标宽度</param>
        public static void MakeSmallImg(String oPath, String oFileName, String iconSavePath,String fileNameAddStr, System.Double Width, System.Double Height)
        {
            if (!Directory.Exists(iconSavePath))
                Directory.CreateDirectory(iconSavePath);

            //SourcePhotoName
            string m_OriginalFilename = oPath + "\\" + oFileName;
            string m_strGoodFile = iconSavePath + "\\" + oFileName.Split('.')[0] + fileNameAddStr + ".jpg";
            //GetPhotoObject From SourceFile
            System.Drawing.Image m_Image = System.Drawing.Image.FromFile(m_OriginalFilename, true);
            System.Drawing.Image bitmap = MarkSmallImgStream(Width, Height, m_Image);
            //SavePhoto Of HightFocus
            bitmap.Save(m_strGoodFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            //DisposeRes
            
            m_Image.Dispose();
            bitmap.Dispose();
        }
        public static System.Drawing.Image MarkSmallImgStream(System.Double Width, System.Double Height, System.Drawing.Image m_Image)
        {
            System.Drawing.Image bitmap;
            System.Drawing.Graphics g;
            System.Double NewWidth, NewHeight;
            if (m_Image.Width > m_Image.Height)
            {
                NewWidth = Width;
                NewHeight = m_Image.Height * (NewWidth / m_Image.Width);
            }
            else
            {
                NewHeight = Height;
                NewWidth = (NewHeight / m_Image.Height) * m_Image.Width;
            }
            if (NewWidth > Width)
            {
                NewWidth = Width;
            }
            if (NewHeight > Height)
            {
                NewHeight = Height;
            }
            //GetPhotoSize
            System.Drawing.Size size = new System.Drawing.Size((int)NewWidth, (int)NewHeight);
            //The New of Bimp Photo
            bitmap = new System.Drawing.Bitmap(size.Width, size.Height);
            // The New of Palette
            g = System.Drawing.Graphics.FromImage(bitmap);
            // Set HightQuality Arithmetic For Graphics
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //ClearCanvas
            g.Clear(System.Drawing.Color.White);
            //在指定位置画图  
            g.DrawImage(m_Image, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, m_Image.Width, m_Image.Height),
            System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            return bitmap;
        }
        /// <summary>
        /// 高清晰方形图标
        /// </summary>
        /// <param name="oPath">原文件路径</param>
        /// <param name="oFileName">原文件名</param>
        /// <param name="iconSavePath">目标文件路径</param>
        /// <param name="fileNameAddStr">目标文件名附加字符串</param>
        /// <param name="SideLength">边长</param>
        public static void MakeICON(String oPath, String oFileName, String iconSavePath, String fileNameAddStr, System.Double SideLength)
        {
            if (!Directory.Exists(iconSavePath))
                Directory.CreateDirectory(iconSavePath);

            //SourcePhotoName
            string m_OriginalFilename = oPath + "\\" + oFileName;
            string m_strGoodFile = iconSavePath + "\\" + oFileName.Split('.')[0] + fileNameAddStr + ".jpg";
            //GetPhotoObject From SourceFile
            System.Drawing.Image m_Image = System.Drawing.Image.FromFile(m_OriginalFilename, true);
            System.Drawing.Image bitmap = MarkICONStream(SideLength, m_Image);
            //SavePhoto Of HightFocus
            bitmap.Save(m_strGoodFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            //DisposeRes
            //g.Dispose();
            m_Image.Dispose();
            bitmap.Dispose();
        }
        public static System.Drawing.Image MarkICONStream(System.Double SideLength, System.Drawing.Image m_Image)
        {
            System.Drawing.Image bitmap; 
            System.Drawing.Graphics g;

            System.Double NewWidth, NewHeight;
            if (m_Image.Width < m_Image.Height)
            {
                NewWidth = SideLength;
                NewHeight = m_Image.Height * (NewWidth / m_Image.Width);
            }
            else
            {
                NewHeight = SideLength;
                NewWidth = (NewHeight / m_Image.Height) * m_Image.Width;
            }
            if (NewWidth > SideLength)
            {
                NewWidth = SideLength;
            }
            if (NewHeight > SideLength)
            {
                NewHeight = SideLength;
            }
            //GetPhotoSize
            System.Drawing.Size size = new System.Drawing.Size((int)NewWidth, (int)NewHeight);
            //The New of Bimp Photo
            bitmap = new System.Drawing.Bitmap(size.Width, size.Height);
            // The New of Palette
            g = System.Drawing.Graphics.FromImage(bitmap);
            // Set HightQuality Arithmetic For Graphics
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //ClearCanvas
            g.Clear(System.Drawing.Color.White);
            //在指定位置画图  
            g.DrawImage(m_Image, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, (int)SideLength, (int)SideLength),
            System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            return bitmap;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ImageMakeMode mode,ImageWaterMark watermark)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            Image bitmap =
            MakeThumbnail(width, height, mode, originalImage, watermark);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
            }
        }
        public static Image MakeThumbnail(int width, int height, ImageMakeMode mode, Image originalImage,ImageWaterMark watermark)
        {
            Image bitmap;
            Graphics g;

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ImageMakeMode.HW://指定高宽缩放（可能变形）                
                    break;
                case ImageMakeMode.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ImageMakeMode.WButOnlyShrink://指定宽，高按比例，但仅缩小，小于高度的图片不会放大
                    if (originalImage.Width <= towidth)
                    {
                        towidth = originalImage.Width;
                        toheight = originalImage.Height;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case ImageMakeMode.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ImageMakeMode.HButOnlyShrink://指定高，宽按比例，但仅缩小，小于高度的图片不会被放大
                    if (originalImage.Height <= toheight)
                    {
                        towidth = originalImage.Width;
                        toheight = originalImage.Height;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case ImageMakeMode.Cut://指定高宽裁减                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case ImageMakeMode.CutA://指定高宽裁减（不变形）自定义 
                    if (ow <= towidth && oh <= toheight)
                    {
                        x = -(towidth - ow) / 2;
                        y = -(toheight - oh) / 2;
                        ow = towidth;
                        oh = toheight;
                    }
                    else
                    {
                        if (ow > oh)//宽大于高
                        {
                            x = 0;
                            y = -(ow - oh) / 2;
                            oh = ow;
                        }
                        else//高大于宽
                        {
                            y = 0;
                            x = -(oh - ow) / 2;
                            ow = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);

            //放置水印
            if (watermark != null)
            {
                if (watermark.WaterPicOn)
                {
                    Image waterimg = null;
                    try
                    {
                        waterimg = Image.FromFile(watermark.WaterPicPath);
                    }
                    catch { }
                    if (waterimg != null)
                    {
                        g.DrawImage(waterimg,
                                    new Rectangle(
                                                bitmap.Width-waterimg.Width-watermark.WaterPicMarginRight,
                                                bitmap.Height-waterimg.Height-watermark.WaterPicMarginBottom,
                                                waterimg.Width,
                                                waterimg.Height),
                                    0,
                                    0,
                                    waterimg.Width,
                                    waterimg.Height,
                                    GraphicsUnit.Pixel
                                    );
                    }
                }
            }

            g.Dispose();

            return bitmap;
        }
        #endregion
    }
    public enum ImageMakeMode
    {
        HW,//指定高宽缩放（可能变形）
        W,//指定宽，高按比例 
        WButOnlyShrink,//指定宽，高按比例，但仅缩小，小于宽度的图片不会被放大
        H,//指定高，宽按比例
        HButOnlyShrink,//指定高，宽按比例，但仅缩小，小于高度的图片不会被放大
        Cut,//指定高宽裁减
        CutA//指定高宽裁减（不变形）自定义

    }

}
