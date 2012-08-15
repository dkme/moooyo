using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Moooyo.WebUI.Common
{
    public class FileManager
    {
        public FileManager() { }
        /// <summary>
        /// 判断是否是隐藏文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public bool IsHidden(string path)
        {
            FileAttributes MyAttributes = File.GetAttributes(path);
            string MyFileType = MyAttributes.ToString();
            if (MyFileType.LastIndexOf("Hidden") != -1) //是否隐藏文件
            {
                return true;
            }
            else
                return false;
        }
        ///<summary>
        ///创建指定目录
        ///</summary>
        ///<param name="targetDir"></param>
        public void CreateDirectory(string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
                dir.Create();
        }
        ///<summary>
        ///删除指定目录的所有文件和子目录
        ///</summary>
        ///<param name="targetDir">操作目录</param>
        ///<param name="delSubDir">如果为true,包含对子目录的操作</param>
        public static void DeleteFiles(string targetDir, bool delSubDir)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                File.Delete(fileName);
            }
            if (delSubDir)
            {
                DirectoryInfo dir = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo subDi in dir.GetDirectories())
                {
                    DeleteFiles(subDi.FullName, true);
                    subDi.Delete();
                }
            }
        }
        ///<summary>
        ///删除指定目录的所有子目录,不包括对当前目录文件的删除
        ///</summary>
        ///<param name="targetDir">目录路径</param>
        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(subDir);
            }
        }
        ///<summary>
        ///删除指定目录,包括当前目录和所有子目录和文件
        ///</summary>
        ///<param name="targetDir">目录路径</param>
        public static void DeleteDirectory(string targetDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(targetDir);
            if (dirInfo.Exists)
            {
                DeleteFiles(targetDir, true);
                dirInfo.Delete(true);
            }
        }
        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileAddress">文件绝对路径</param>
        public static void DeleteFile(string fileAddress)
        {
            File.SetAttributes(fileAddress, FileAttributes.Normal);
            File.Delete(fileAddress);
        }
        ///<summary>
        ///剪切指定目录的所有文件
        ///</summary>
        ///<param name="sourceDir">原始目录</param>
        ///<param name="targetDir">目标目录</param>
        ///<param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        ///<param name="moveSubDir">如果为true,包含目录,否则不包含</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            //移动当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf("\\") + 1));
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Delete(targetFileName);
                        File.Move(sourceFileName, targetFileName);
                    }
                }
                else
                {
                    File.Move(sourceFileName, targetFileName);
                }
            }
            if (moveSubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf("\\") + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    MoveFiles(sourceSubDir, targetSubDir, overWrite, true);
                    Directory.Delete(sourceSubDir);
                }
            }
        }
        ///<summary>
        ///复制指定目录的所有文件
        ///</summary>
        ///<param name="sourceDir">原始目录</param>
        ///<param name="targetDir">目标目录</param>
        ///<param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        ///<param name="copySubDir">如果为true,包含目录,否则不包含</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            //复制当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf("\\") + 1));
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
            //复制子目录
            if (copySubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf("\\") + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
                }
            }
        }
        /// <summary>
        /// 获取子目录文件信息
        /// </summary>
        /// <param name="targetDir">文件夹名称</param>
        public static List<string> getChildFileInfo(string targetDir)
        {
            List<string> childrenFileName = new List<string>();
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                FileInfo fileinfo = new FileInfo(fileName);
                string name = fileName.Substring(fileName.LastIndexOf("\\") + 1);//文件名
                string address = fileName; //地址
                string size = fileinfo.Length.ToString();//文件大小,字节
                string createtime = fileinfo.CreationTime.ToString();//文件创建时间
                childrenFileName.Add(address);
            }
            return childrenFileName;
        }
        public static string readFile(string fileName)
        {
            string content = "";//返回的字符串

            // 以只读模式打开一个文本文件
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8))
                {
                    string text = string.Empty;
                    while (!reader.EndOfStream)
                    {
                        text = reader.ReadLine();
                        content = text;
                    }
                }
            }
            return content;
        }
        public static void writeFile(string path, string str)
        {
            //如果文件path存在就打开，不存在就新建 .append 是追加写, CreateNew 是覆盖
            FileStream fst = new FileStream(path, FileMode.Append);
            StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
            //写入 
            swt.WriteLine(str);
            swt.Close();
            fst.Close();
        }
    }
}