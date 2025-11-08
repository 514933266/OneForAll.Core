using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// 帮助类：MD5加密解密
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>MD5加密后的字符串（32位）</returns>
        public static string Encrypt(string input, Encoding encoding = null)
        {
            var sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                var buff = md5.ComputeHash(encoding.GetBytes(input));
                foreach (var t in buff)
                {
                    sb.AppendFormat("{0:x2}", t);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>MD5加密后的字符串（32位）</returns>
        public static string ToMd5(this string input)
        {
            return Encrypt(input);
        }
    }
}
