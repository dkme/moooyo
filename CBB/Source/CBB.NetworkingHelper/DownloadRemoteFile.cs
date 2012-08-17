using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CBB.NetworkingHelper
{
    public class DownloadRemoteFile
    {
        /*
        public string StrUrl;//文件下载网址
        public string StrFileName;//下载文件保存地址 
        public string strError;//返回结果
        public long lStartPos = 0; //返回上次下载字节
        public long lCurrentPos = 0;//返回当前下载字节
        public long lDownloadFile;//返回当前下载文件长度
        public long kbm = 0;//返回下载速度
        */

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="StrUrl">URL地址</param>
        /// <param name="StrFileName">文件名</param>
        /// <returns>string</returns>
        public string DownloadFile(String StrUrl, String StrFileName)
        {
            long lStartPos = 0; //返回上次下载字节
            long lCurrentPos = 0;//返回当前下载字节
            long lDownloadFile=0;//返回当前下载文件长度
            long kbm = 0;//返回下载速度

            return DownloadFile(StrUrl, StrFileName, ref lStartPos, ref lCurrentPos, ref lDownloadFile, ref kbm);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="StrUrl">URL地址</param>
        /// <param name="StrFileName">文件名</param>
        /// <param name="lStartPos">字符长度</param>
        /// <param name="lCurrentPos">字符长度</param>
        /// <param name="lDownloadFile">数据内容长度</param>
        /// <param name="kbm">文件大小</param>
        /// <returns>string</returns>
        public string DownloadFile(String StrUrl,String StrFileName,ref long lStartPos,ref long lCurrentPos,ref long lDownloadFile,ref long kbm)
        {
            string strError;//返回结果
            
            System.IO.FileStream fs;
            if (System.IO.File.Exists(StrFileName))
            {
                fs = System.IO.File.OpenWrite(StrFileName);
                lStartPos = fs.Length;
                fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
                //移动文件流中的当前指针 
            }
            else
            {
                fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
                lStartPos = 0;
            }

            //打开网络连接 
            try
            {
                DateTime olddt = DateTime.Now;
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(StrUrl);

                request.AllowAutoRedirect = true;
                long length = request.GetResponse().ContentLength;
                lDownloadFile = length;
                if (lStartPos > 0)
                    request.AddRange((int)lStartPos); //设置Range值 

                //向服务器请求，获得服务器回应数据流 
                System.IO.Stream ns = request.GetResponse().GetResponseStream();
                byte[] nbytes = new byte[512];
                int nReadSize = 0;
                nReadSize = ns.Read(nbytes, 0, 512);
                while (nReadSize > 0)
                {
                    fs.Write(nbytes, 0, nReadSize);
                    nReadSize = ns.Read(nbytes, 0, 512);
                    lCurrentPos = fs.Length;
                }
                TimeSpan ts = DateTime.Now - olddt;
                fs.Close();
                ns.Close();
                kbm = lDownloadFile * 1000 / (1024 * (long)ts.TotalMilliseconds);
                strError = "下载完成";
                return strError;
            }
            catch (Exception ex)
            {
                fs.Close();
                strError = "下载过程中出现错误:" + ex.ToString();
                return strError;
            }

        }
    }
}
