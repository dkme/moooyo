using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.Security
{
    public class RandomPassword
    {
        private string randomChars = "ABCDEFGHIJKLMPOQRTVWXY21346789";

        public string GetRandomPassword(int passwordLen)
        {
            string password = string.Empty;
            int randomNum;
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < passwordLen; i++)
            {
                randomNum = random.Next(randomChars.Length);
                password += randomChars[randomNum];
            }
            return password;
        }
    }
}
