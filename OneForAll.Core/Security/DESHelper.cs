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
        private static readonly byte[] _keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="encryptKey">8位加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string input, string encryptKey)
        {
            var rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            var rgbIV = _keys;
            var inputByteArray = Encoding.UTF8.GetBytes(input);
            var dCSP = new DESCryptoServiceProvider();
            using (var ms = new MemoryStream())
            {
                var cStream = new CryptoStream(ms, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="input">待解密的字符串</param>
        /// <param name="decryptKey">8位解密密钥（同加密密钥）</param>
        /// <returns>原字符串</returns>
        public static string Decrypt(string input, string decryptKey)
        {
            var rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            var rgbIV = _keys;
            var inputByteArray = Convert.FromBase64String(input);
            var DCSP = new DESCryptoServiceProvider();
            var ms = new MemoryStream();
            var cStream = new CryptoStream(ms, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
