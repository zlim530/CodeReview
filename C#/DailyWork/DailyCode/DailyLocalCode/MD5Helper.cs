using System;
using System.Security.Cryptography;
using System.Text;

namespace DailyLocalCode
{
    public static class MD5Helper
    {
        static void Main0(string[] args)
        {
            var pass = "LIMforever";
            Console.WriteLine(pass);
            var pass1 = MD5Helper.MD5Encrypt16(pass);
            Console.WriteLine(pass1);
            var pass2 = MD5Helper.MD5Encrypt32(pass);
            Console.WriteLine(pass2);
            var pass3 = MD5Helper.MD5Encrypt64(pass);
            Console.WriteLine(pass3);
            Console.WriteLine(MD5Helper.MD5Encrypt32("LIMforever") == pass2);

        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="pssword"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string pssword)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(pssword)),4,8);
            t2 = t2.Replace("-",string.Empty);
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password = "")
        {
            string pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(password) &&
                    !string.IsNullOrWhiteSpace(password))
                {
                    MD5 md5 = MD5.Create();
                    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    foreach (var item in s)
                    {
                        pwd = string.Concat(pwd, item.ToString("X2"));
                    }
                }
            }
            catch 
            {
                throw new Exception($"错误的 password 字符串：【{password}】") ;
            }
            return pwd;
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(s);
        }
    }
}
