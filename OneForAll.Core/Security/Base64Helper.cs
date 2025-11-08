using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// 帮助类：Base64
    /// </summary>
    public static class Base64Helper
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">要加密的数据</param>
        /// <param name="encode">编码格式</param>
        /// <returns>返回Base64编码后的加密字符串</returns>
        public static string Encrypt(string input, Encoding encode = null)
        {
            if (encode == null)
            {
                encode = Encoding.UTF8;
            }
            var bytes = encode.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">要解密的数据</param>
        /// <param name="encode">编码格式</param>
        /// <returns>返回Base64解密字符串</returns>
        public static string Decrypt(string input, Encoding encode = null)
        {
            if (encode == null)
            {
                encode = Encoding.UTF8;
            }
            var bytes = Convert.FromBase64String(input);
            return encode.GetString(bytes);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">要加密的数据</param>
        /// <returns>返回Base64编码后的加密字符串</returns>
        public static string ToBase64(this string input)
        {
            return Encrypt(input);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">要解密的数据</param>
        /// <returns>返回Base64解密字符串</returns>
        public static string FromBase64(this string input)
        {
            return Decrypt(input);
        }
    }
}
