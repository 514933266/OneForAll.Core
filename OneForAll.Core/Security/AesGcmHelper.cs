using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// AES-256-GCM
    /// </summary>
    public class AesGcmHelper
    {
        /// <summary>
        /// AES-256-GCM解密
        /// </summary>
        /// <param name="associatedData">附加数据包（可能为空）</param>
        /// <param name="nonce">加密使用的随机串初始化向量</param>
        /// <param name="ciphertext">Base64编码后的密文</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string associatedData, string nonce, string ciphertext, string key)
        {
            GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            AeadParameters aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(key)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            byte[] data = Convert.FromBase64String(ciphertext);
            byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }
    }
}
