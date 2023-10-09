using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// RSA加密解密
    /// </summary>
    public static class RSAHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="xmlPublicKey">xml格式私钥</param>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string Encrypt(string xmlPublicKey, string content)
        {
            string encryptedContent = string.Empty;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(xmlPublicKey);
                byte[] encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(content), false);
                encryptedContent = Convert.ToBase64String(encryptedData);
            }
            return encryptedContent;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="xmlPrivateKey">xml格式私钥</param>
        /// <param name="content">密文</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string content)
        {
            string decryptedContent = string.Empty;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(xmlPrivateKey);
                byte[] decryptedData = rsa.Decrypt(Convert.FromBase64String(content), false);
                decryptedContent = Encoding.GetEncoding("gb2312").GetString(decryptedData);
            }
            return decryptedContent;
        }
    }
}
