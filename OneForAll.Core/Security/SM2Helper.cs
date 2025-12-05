using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace OneForAll.Core.Security
{
    /// <summary>
    /// SM2 国密加密帮助类，兼容 Vue3 sm-crypto 库生成的密文格式 (C1||C3||C2)。
    /// </summary>
    public class SM2Helper
    {
        // SM2 国密标准参数 (基于BouncyCastle的SM2曲线)
        private static readonly string CURVE_NAME = "sm2p256v1";
        private static readonly X9ECParameters Sm2CurveParams = ECNamedCurveTable.GetByName(CURVE_NAME);
        private static readonly ECDomainParameters SM2Params = new ECDomainParameters(
            Sm2CurveParams.Curve,
            Sm2CurveParams.G,
            Sm2CurveParams.N,
            Sm2CurveParams.H,
            Sm2CurveParams.GetSeed()
        );

        /// <summary>
        /// 使用公钥加密数据，生成与sm-crypto兼容的Hex格式密文 (C1||C3||C2)
        /// </summary>
        /// <param name="publicKeyHex">公钥 (Hex字符串, 通常65字节, 0x04开头)</param>
        /// <param name="data">要加密的原始数据 (字节数组)</param>
        /// <returns>加密后的密文 (Hex字符串, 格式: C1||C3||C2)</returns>
        public static string Encrypt(string publicKeyHex, byte[] data)
        {
            if (string.IsNullOrEmpty(publicKeyHex))
                throw new ArgumentException("公钥不能为空", nameof(publicKeyHex));
            if (data == null || data.Length == 0)
                throw new ArgumentException("数据不能为空", nameof(data));

            try
            {
                // 1. 解析Hex公钥
                byte[] publicKeyBytes = Hex.Decode(publicKeyHex);
                var publicKey = ParsePublicKey(publicKeyBytes);

                // 2. 创建SM2公钥参数
                var keyParam = new ECPublicKeyParameters(publicKey, SM2Params);

                // 3. 创建SM2加密引擎
                var engine = new SM2Engine();
                engine.Init(true, new ParametersWithRandom(keyParam, new SecureRandom()));

                // 4. 执行加密 (BouncyCastle默认输出 C1||C2||C3)
                byte[] cipherBytes = engine.ProcessBlock(data, 0, data.Length);

                // 5. 解析BouncyCastle的输出 (C1||C2||C3) -> 提取 C1, C2, C3
                //    C1 长度固定为 65 字节 (0x04开头的未压缩点)
                const int C1_LENGTH = 65;
                //    C3 长度固定为 32 字节 (SM3哈希)
                const int C3_LENGTH = 32;

                if (cipherBytes.Length < C1_LENGTH + C3_LENGTH)
                    throw new CryptographicException("加密结果长度不足");

                byte[] c1 = cipherBytes.Take(C1_LENGTH).ToArray();
                byte[] c2c3 = cipherBytes.Skip(C1_LENGTH).ToArray(); // C2||C3

                // 在 C2||C3 中，最后32字节是C3，前面的是C2
                if (c2c3.Length < C3_LENGTH)
                    throw new CryptographicException("C2C3部分长度不足");
                byte[] c3 = c2c3.Skip(c2c3.Length - C3_LENGTH).ToArray();
                byte[] c2 = c2c3.Take(c2c3.Length - C3_LENGTH).ToArray();

                // 6. 重新组装为 sm-crypto 兼容格式: C1 || C3 || C2
                byte[] finalCipher = new byte[C1_LENGTH + C3_LENGTH + c2.Length];
                Buffer.BlockCopy(c1, 0, finalCipher, 0, C1_LENGTH);
                Buffer.BlockCopy(c3, 0, finalCipher, C1_LENGTH, C3_LENGTH);
                Buffer.BlockCopy(c2, 0, finalCipher, C1_LENGTH + C3_LENGTH, c2.Length);

                // 7. 转换为Hex字符串返回
                return Hex.ToHexString(finalCipher).ToLower();
            }
            catch (Exception ex)
            {
                // 根据需要记录日志
                throw new CryptographicException($"SM2加密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 使用私钥解密数据，兼容sm-crypto生成的Hex格式密文 (C1||C3||C2)
        /// </summary>
        /// <param name="privateKeyHex">私钥 (Hex字符串, 通常32字节)</param>
        /// <param name="cipherHex">密文 (Hex字符串, 格式: C1||C3||C2)</param>
        /// <returns>解密后的原始数据 (字节数组)</returns>
        public static byte[] Decrypt(string privateKeyHex, string cipherHex)
        {
            if (string.IsNullOrEmpty(privateKeyHex))
                throw new ArgumentException("私钥不能为空", nameof(privateKeyHex));
            if (string.IsNullOrEmpty(cipherHex))
                throw new ArgumentException("密文不能为空", nameof(cipherHex));

            try
            {
                // 1. 解析Hex私钥和密文
                byte[] privateKeyBytes = Hex.Decode(privateKeyHex);
                byte[] cipherBytes = Hex.Decode(cipherHex);

                // 2. 解析 sm-crypto 格式密文 (C1||C3||C2)
                const int C1_LENGTH = 65; // C1: 65字节
                const int C3_LENGTH = 32; // C3: 32字节 (SM3)

                if (cipherBytes.Length < C1_LENGTH + C3_LENGTH)
                    throw new CryptographicException("密文长度不足");

                byte[] c1 = cipherBytes.Take(C1_LENGTH).ToArray();
                byte[] c3 = cipherBytes.Skip(C1_LENGTH).Take(C3_LENGTH).ToArray();
                byte[] c2 = cipherBytes.Skip(C1_LENGTH + C3_LENGTH).ToArray();

                // 3. 重新组装为 BouncyCastle 可接受的格式 (C1||C2||C3)
                byte[] bcCipher = new byte[C1_LENGTH + c2.Length + C3_LENGTH];
                Buffer.BlockCopy(c1, 0, bcCipher, 0, C1_LENGTH);
                Buffer.BlockCopy(c2, 0, bcCipher, C1_LENGTH, c2.Length);
                Buffer.BlockCopy(c3, 0, bcCipher, C1_LENGTH + c2.Length, C3_LENGTH);

                // 4. 创建SM2私钥参数
                BigInteger privateKeyInt = new BigInteger(1, privateKeyBytes);
                var keyParam = new ECPrivateKeyParameters(privateKeyInt, SM2Params);

                // 5. 创建SM2解密引擎
                var engine = new SM2Engine();
                engine.Init(false, keyParam);

                // 6. 执行解密
                return engine.ProcessBlock(bcCipher, 0, bcCipher.Length);
            }
            catch (Exception ex)
            {
                // 根据需要记录日志
                throw new CryptographicException($"SM2解密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从字节数组解析SM2公钥 (ECPublicKey)
        /// </summary>
        /// <param name="publicKeyBytes">公钥字节数组 (0x04开头)</param>
        /// <returns>ECPublicKey</returns>
        private static Org.BouncyCastle.Math.EC.ECPoint ParsePublicKey(byte[] publicKeyBytes)
        {
            if (publicKeyBytes == null || publicKeyBytes.Length != 65 || publicKeyBytes[0] != 0x04)
                throw new ArgumentException("无效的SM2公钥格式");

            // 公钥前缀0x04 + X(32字节) + Y(32字节)
            byte[] xBytes = new byte[32];
            byte[] yBytes = new byte[32];
            Array.Copy(publicKeyBytes, 1, xBytes, 0, 32);
            Array.Copy(publicKeyBytes, 33, yBytes, 0, 32);

            BigInteger x = new BigInteger(1, xBytes);
            BigInteger y = new BigInteger(1, yBytes);

            return SM2Params.Curve.CreatePoint(x, y);
        }

        // --- 可选：生成密钥对 ---
        /// <summary>
        /// 生成SM2密钥对 (私钥32字节Hex, 公钥65字节Hex 0x04开头)
        /// </summary>
        /// <returns>包含私钥和公钥的元组 (PrivateKeyHex, PublicKeyHex)</returns>
        public static (string PrivateKeyHex, string PublicKeyHex) GenerateKeyPair()
        {
            var keyGen = new ECKeyPairGenerator();
            var keyGenParam = new KeyGenerationParameters(new SecureRandom(), 256);
            keyGen.Init(keyGenParam);

            var keyPair = keyGen.GenerateKeyPair();
            var privateKey = (ECPrivateKeyParameters)keyPair.Private;
            var publicKey = (ECPublicKeyParameters)keyPair.Public;

            // 私钥 (32字节)
            byte[] privBytes = privateKey.D.ToByteArrayUnsigned();
            // 确保是32字节，前面补0
            if (privBytes.Length < 32)
            {
                byte[] padded = new byte[32];
                Array.Copy(privBytes, 0, padded, 32 - privBytes.Length, privBytes.Length);
                privBytes = padded;
            }

            // 公钥 (65字节, 0x04开头)
            byte[] pubBytes = publicKey.Q.GetEncoded(false); // false = uncompressed (0x04开头)

            return (Hex.ToHexString(privBytes).ToLower(), Hex.ToHexString(pubBytes).ToLower());
        }
    }
}
