using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DailyLocalCode.Algorithms
{
    public class AESCommon
    {
        static void Main(string[] args)
        {
            var AESKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
            var str = AESEncrypt("Aa60996349", AESKey);
            var str2 = AESDecrypt(str, AESKey);
            Console.WriteLine(str);
            Console.WriteLine(str2);
        }


        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文</returns>
        public static string AESEncrypt(string plainText, string strKey)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;
            //得到需要加密的字节数组
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
            SymmetricAlgorithm des = Rijndael.Create();
            //设置密钥
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //得到加密后的字节数组
            var cipherBytes = ms.ToArray();
            cs.Close();
            ms.Close();
            return Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string AESDecrypt(string cipherText, string strKey)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;

            var toEncryptArray = Convert.FromBase64String(cipherText);
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            byte[] decryptBytes = new byte[cipherText.Length];
            MemoryStream ms = new MemoryStream(toEncryptArray);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
