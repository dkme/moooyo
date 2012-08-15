using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CBB.Security
{
    /// <summary>
    /// MD5类
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// Hash an input string and return the hash as a 32 character hexadecimal string.
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>string</returns>
        public static string getMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder(); 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 字符串比较
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="hash">字符串</param>
        /// <returns>true/false</returns>
        public static bool verifyMd5Hash(string input, string hash)
        {
            string hashOfInput = getMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; 
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
