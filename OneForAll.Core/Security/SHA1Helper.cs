using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// SHA1加密
    /// </summary>
    public static class SHA1Helper
    {
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="isReplace">是否替换掉加密后的字符串中的"-"字符</param>
        /// <param name="isToLower">是否把加密后的字符串转小写</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string input, bool isReplace = true, bool isToLower = false)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            string shaStr = BitConverter.ToString(hash);
            if (isReplace)
            {
                shaStr = shaStr.Replace("-", "");
            }
            if (isToLower)
            {
                shaStr = shaStr.ToLower();
            }
            return shaStr;
        }
    }
}
