using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// 帮助类：DES加密解密
    /// </summary>
    public static class DESHelper
    {
        //默认密钥向量 
        private static byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="encryptKey">8位加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string input, string encryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
            var DCSP = Aes.Create();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="input">待解密的字符串</param>
        /// <param name="decryptKey">8位解密密钥（同加密密钥）</param>
        /// <returns>原字符串</returns>
        public static string Decrypt(string input, string decryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(input);
            var DCSP = Aes.Create();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            Byte[] inputByteArrays = new byte[inputByteArray.Length];
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
