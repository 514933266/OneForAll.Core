using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// 帮助类：AES加密解密
    /// </summary>
    public static class AESHelper
    {

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">密钥（16或者32位）</param>
        /// <param name="iv">向量</param>
        /// <returns>Base64转码后的密文</returns>
        public static string Encrypt(string input, string key, string iv)
        {
            var sourceBytes = Encoding.UTF8.GetBytes(input);
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            var transform = aes.CreateEncryptor();
            return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">要解密的内容</param>
        /// <param name="key">密钥（16或者32位）</param>
        /// <param name="iv">向量</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string input, string key, string iv)
        {
            var encryptBytes = Convert.FromBase64String(input);
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            var transform = aes.CreateDecryptor();
            return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">密钥（16或者32位）</param>
        /// <param name="iv">向量</param>
        /// <returns>Base64转码后的密文</returns>
        public static string ToAES(this string input, string key, string iv)
        {
            return Encrypt(input, key, iv);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">要解密的内容</param>
        /// <param name="key">密钥（16或者32位）</param>
        /// <param name="iv">向量</param>
        /// <returns>解密后的明文</returns>
        public static string FromAES(this string input, string key, string iv)
        {
            return Decrypt(input, key, iv);
        }
    }
}
