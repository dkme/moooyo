using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CBB.Security
{
    /// <summary>
    /// DES加密类--张磊
    /// </summary>
    public class Des加密
    {
        #region 私有属性

        /// <summary>
        /// MD5加密的字符串
        /// </summary>
        private string md5Str = null;
        /// <summary>
        /// DES加密的字符串
        /// </summary>
        private string encryptStr = null;

        /// <summary>
        /// DES解密的字符串
        /// </summary>
        private string decryptStr = null;

        /// <summary>
        /// DES密匙
        /// </summary>
        private string mydesKey = "N*E%S#";

        /// <summary>
        /// 返回的字符串
        /// </summary>
        private string mydesStr = null;

        /// <summary>
        /// 错误信息
        /// </summary>
        private string messAge = null;

        #endregion

        #region 公共属性

        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public string MD5Str
        {
            get { return md5Str; }
            set { md5Str = value; }
        }
        /// <summary>
        /// DES加密的字符串
        /// </summary>
        public string EncryptStr
        {
            get { return encryptStr; }
            set { encryptStr = value; }
        }

        /// <summary>
        ///DES 解密的字符串
        /// </summary>
        public string DecryptStr
        {
            get { return decryptStr; }
            set { decryptStr = value; }
        }

        /// <summary>
        /// DES密匙
        /// </summary>
        public string MyDesKey
        {
            get { return mydesKey; }
            set { mydesKey = value; }

        }

        /// <summary>
        /// 返回的字符串
        /// </summary>
        public string MyDesStr
        {
            get { return mydesStr; }
            set { mydesStr = value; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message
        {
            get { return messAge; }
            set { messAge = value; }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 2009-05-31 黄河
        /// 执行DES加密
        /// </summary>
        public void DesEncrypt()
        {
            try
            {
                while (mydesKey.Length > 0)
                {
                    byte[] MyStr_E = Encoding.UTF8.GetBytes(this.encryptStr);
                    byte[] MyKey_E = Encoding.UTF8.GetBytes(this.mydesKey.Length >= 8 ? this.mydesKey.Substring(0, 8) : this.mydesKey.PadRight(8, '0'));


                    DESCryptoServiceProvider MyDes_E = new DESCryptoServiceProvider();
                    MyDes_E.Key = MyKey_E;
                    MyDes_E.IV = MyKey_E;

                    MemoryStream MyMem_E = new MemoryStream();

                    CryptoStream MyCry_E = new CryptoStream(MyMem_E, MyDes_E.CreateEncryptor(), CryptoStreamMode.Write);
                    MyCry_E.Write(MyStr_E, 0, MyStr_E.Length);
                    MyCry_E.FlushFinalBlock();
                    MyCry_E.Close();
                    this.encryptStr = Convert.ToBase64String(MyMem_E.ToArray());
                    mydesKey = mydesKey.Remove(0, mydesKey.Length >= 8 ? 8 : mydesKey.Length);
                }
                this.mydesStr = this.encryptStr;
            }
            catch (Exception Error)
            {
                this.messAge = "DES加密出错：" + Error.Message;
            }
        }

        /// <summary>
        /// 2009-05-31 黄河
        /// 执行DES解密
        /// </summary>
        public void DesDecrypt()
        {
            try
            {
                while (mydesKey.Length > 0)
                {
                    int delint = mydesKey.Length % 8 == 0 ? mydesKey.Length - 8 : mydesKey.Length - (mydesKey.Length % 8);
                    byte[] MyStr_D = Convert.FromBase64String(this.decryptStr);
                    byte[] MyKey_D = Encoding.UTF8.GetBytes(this.mydesKey.Substring(delint));

                    DESCryptoServiceProvider MyDes_D = new DESCryptoServiceProvider();
                    MyDes_D.Key = MyKey_D;
                    MyDes_D.IV = MyKey_D;

                    MemoryStream MyMem_D = new MemoryStream();

                    CryptoStream MyCry_D = new CryptoStream(MyMem_D, MyDes_D.CreateDecryptor(), CryptoStreamMode.Write);
                    MyCry_D.Write(MyStr_D, 0, MyStr_D.Length);
                    MyCry_D.FlushFinalBlock();
                    MyCry_D.Close();
                    this.decryptStr = Encoding.UTF8.GetString(MyMem_D.ToArray());
                    mydesKey = mydesKey.Remove(delint);
                }
                this.mydesStr = this.decryptStr;
            }
            catch (Exception Error)
            {
                this.messAge = "DES解密出错：" + Error.Message;
            }
        }

        /// <summary>
        /// 2009-05-31 黄河
        /// 执行MD5加密
        /// </summary>
        public void MD5JiaMi()
        {
            MD5CryptoServiceProvider MyMD5 = new MD5CryptoServiceProvider();
            try
            {
                Byte[] MyMD5_Str = MyMD5.ComputeHash(Encoding.UTF8.GetBytes(this.md5Str));
                this.MyDesStr = Encoding.UTF8.GetString(MyMD5_Str);
            }
            catch (Exception Error)
            {
                this.messAge = "MD5加密出错：" + Error.Message;
            }

        }

        #endregion

        /// <summary>
        /// DES加密编码
        /// </summary>
        /// <param name="OutSideKey">字符串</param>
        /// <returns>String</returns>
        public String KeyEncrypt(String OutSideKey)
        {
            String[] Keys = new String[] { this.mydesKey, DateTime.Today.AddDays(DateTime.Today.Year).ToString("yyMMdd"), OutSideKey.Trim() };
            return _Encrypt(Keys);
        }

        /// <summary>
        /// DES加密编码
        /// </summary>
        /// <returns>String</returns>
        public String KeyEncrypt()
        {
            String[] Keys = new String[] { this.mydesKey, DateTime.Today.AddDays(DateTime.Today.Year).ToString("yyMMdd"), ")(*&^%$#@!" };
            return _Encrypt(Keys);
        }

        /// <summary>
        /// DES加密编码--对日期
        /// </summary>
        /// <param name="Date">日期</param>
        /// <returns></returns>
        public String KeyEncrypt(DateTime Date)
        {
            String[] Keys = new String[] { this.mydesKey, Date.AddDays(Date.Year).ToString("yyMMdd"), ")(*&^%$#@!" };
            return _Encrypt(Keys);
        }

        /// <summary>
        /// 私有方法--编码
        /// </summary>
        /// <param name="arrstr">字符串数组</param>
        /// <returns>String</returns>
        private String _Encrypt(String[] arrstr)
        {
            StringBuilder sb = new StringBuilder();
            int Strleng = 0;
            int charindex = 0;
            foreach (String str in arrstr)
            {
                charindex = 0;
                foreach (char c in str)
                {
                    charindex++;
                    sb.Insert(Strleng < charindex ? 0 : Strleng - charindex, c);
                }
                Strleng = sb.Length;
            }
            return sb.ToString();
        }
    }
    public class Encrypt
    {
        /// <summary>
        /// 检查NES编码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <param name="Date">日期</param>
        /// <returns>字符串</returns>
        public static String CheckNESEncrypt(String Value, DateTime Date)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt(Date);
            myDes.EncryptStr = Value.Trim();
            myDes.DesEncrypt();
            return myDes.MyDesStr;
        }

        /// <summary>
        /// 检查NES解码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <param name="Date">日期</param>
        /// <returns>字符串</returns>
        public static String CheckNESDecrypt(String Value, DateTime Date)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt(Date);
            myDes.DecryptStr = Value.Trim();
            myDes.DesDecrypt();
            return myDes.MyDesStr;
        }

        /// <summary>
        /// 检查NES编码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <returns>字符串</returns>
        public static String CheckNESEncrypt(String Value)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt();
            myDes.EncryptStr = Value.Trim();
            myDes.DesEncrypt();
            return myDes.MyDesStr;
        }

        /// <summary>
        /// 检查NES解码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <returns>字符串</returns>
        public static String CheckNESDecrypt(String Value)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt();
            myDes.DecryptStr = Value.Trim();
            myDes.DesDecrypt();
            return myDes.MyDesStr;
        }

        /// <summary>
        /// 检查NES编码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <param name="Key">键</param>
        /// <returns>字符串</returns>
        public static String CheckNESEncrypt(String Value, String Key)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt(Key);
            myDes.EncryptStr = Value.Trim();
            myDes.DesEncrypt();
            return myDes.MyDesStr;
        }

        /// <summary>
        /// 检查NES解码
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <param name="Key">键</param>
        /// <returns>字符串</returns>
        public static String CheckNESDecrypt(String Value, String Key)
        {
            Des加密 myDes = new Des加密();
            myDes.MyDesKey = myDes.KeyEncrypt(Key);
            myDes.DecryptStr = Value.Trim();
            myDes.DesDecrypt();
            return myDes.MyDesStr;
        }
    }

}
