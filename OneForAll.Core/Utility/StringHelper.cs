using OneForAll.Core.Extension;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 帮助类：字符串
    /// </summary>
    public static class StringHelper
    {
        #region 随机字符串

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>字符串（数字）</returns>
        public static string GetRandomNumber(int length)
        {
            string value = string.Empty;
            int seed = Math.Abs((int)BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0));
            Random random = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                value += random.Next(10);
            }
            return value;
        }

        /// <summary>
        /// 随机生成编号
        /// </summary>
        /// <param name="PreStr">前缀</param>
        /// <param name="lenght">长度</param>
        /// <returns>字符串（前缀+时间（毫秒级，举例：20160111081245023）+ 指定长度随机数字。）</returns>
        public static string GetRandomNumber(string PreStr, int lenght = 4)
        {
            Random rd = new Random();
            string value = "0123456789";
            string Last_CommonNo = "";
            for (int i = 0; i < lenght; i++)
            {
                Last_CommonNo += value[rd.Next(value.Length)];
            }
            string CommonNo = string.Format("{0}{1}{2}", PreStr, DateTime.Now.ToString("yyyyMMddHHmmssfff"), Last_CommonNo);
            return CommonNo;
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>字符串（数字 + 大小写英文字母混合）</returns>
        public static string GetRandomString(int length)
        {
            char[] arrChar = new char[]
            {
                'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u', 'v',
                'w', 'z', 'y', 'x',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U',
                'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            };
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                sb.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>字符串（大小写英文字母混合）</returns>
        public static string GetRandomLetter(int length)
        {
            char[] arrChar = new char[]
            {
                'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u', 'v',
                'w', 'z', 'y', 'x',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U',
                'W', 'X', 'Y', 'Z'
            };
            StringBuilder num = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return num.ToString();
        }

        #endregion

        #region 字符串处理

        /// <summary>
        /// 获取中文内容
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>匹配内容集合</returns>
        public static MatchCollection MatchChinese(string value)
        {
            return Regex.Matches(value, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 匹配第一个中文字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>匹配内容</returns>
        public static string MatchFirstChinese(string value)
        {
            return Regex.Match(value, @"^[\u4e00-\u9fa5]+$").Value;
        }

        /// <summary>
        /// 匹配所有邮箱地址
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>内容集合</returns>
        public static IEnumerable<string> MatchEmail(string value)
        {
            var emails = new List<string>();
            MatchCollection mc = Regex.Matches(value, @"^[\u4e00-\u9fa5]+$");
            if (mc != null)
            {
                foreach (Match c in mc)
                {
                    emails.Add(c.Value);
                }
            }
            return emails;
        }

        /// <summary>
        /// 正则匹配项替换为指定项
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <param name="content">内容</param>
        /// <param name="replace">替换内容</param>
        /// <returns>字符串</returns>
        public static string ReplaceRegex(string pattern, string content, string replace)
        {
            Regex regx = new Regex(pattern, RegexOptions.IgnoreCase);
            return regx.Replace(content, replace);
        }

        /// <summary>
        /// 替换特殊符号为空(仅剩数字和英文字母)
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>替换的值</returns>
        public static string RemoveSymbol(string value)
        {
            return Regex.Replace(value, "[^0-9A-Za-z]", "");
        }

        /// <summary>
        ///  将指定的字符串集合替换指定的字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="symbols">要移除的字符串集合</param>
        /// <param name="replacStr">此字符会用来代替被替换的字符串</param>
        /// <returns>更简洁的字符串</returns>
        public static string RemoveSymbol(string value, string[] symbols, string replacStr = "")
        {
            foreach (var item in symbols)
            {
                value = value.Replace(item, replacStr);
            }
            return value;
        }

        /// <summary>
        /// 截取某段字符串,获取中间值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="begin">前缀</param>
        /// <param name="end">后缀</param>
        /// <returns>截取的内容值</returns>
        public static string MatchMiddleValue(string value, string begin, string end)
        {
            Regex regex = new Regex(string.Concat(new string[]
           {
                "(?<=(",
                begin,
                "))[.\\s\\S]*?(?=(",
                end,
                "))"
           }), RegexOptions.Multiline | RegexOptions.Singleline);
            return regex.Match(value).Value;
        }

        /// <summary>
        /// 删除字符串内的Javasctipt元素
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>更简洁的字符串</returns>
        public static string FilterScript(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            value = Regex.Replace(value, @"<script[^>]*>([\s\S](?!<script))*?</script>", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"-->", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"<!--.*", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            value.Replace("<", "");
            value.Replace(">", "");
            value.Replace("\r\n", "");
            return value;
        }

        /// <summary>
        ///  电话掩码
        /// </summary>
        /// <param name="mobile">电话号码字符串</param>
        /// <returns>掩码后的电话</returns>
        public static string MaskMobile(string mobile)
        {
            if (!string.IsNullOrEmpty(mobile) && mobile.Length > 7)
            {
                return ReplaceRegex(@"(?<=\d{3}).+(?=\d{4})", mobile, "****");
            }
            return mobile;
        }
        /// <summary>
        /// 中国身份证掩码
        /// </summary>
        /// <param name="cert">身份证字符串</param>
        /// <returns>掩码后的身份证字符串</returns>
        public static string MaskIdCard(string cert)
        {
            if (!string.IsNullOrEmpty(cert) && cert.Length > 16)
            {
                return ReplaceRegex(@"(?<=\w{6}).+(?=\w{4})", cert, "********");
            }
            return cert;
        }

        /// <summary>
        /// 银行卡掩码
        /// </summary>
        /// <param name="bankCark">银行卡号字符串</param>
        /// <returns>掩码后的银行卡号</returns>
        public static string MaskBankCard(string bankCark)
        {
            if (!string.IsNullOrEmpty(bankCark) && bankCark.Length > 10)
            {
                return ReplaceRegex(@"(?<=\d{4})\d+(?=\d{4})", bankCark, " **** **** ");
            }
            return bankCark;
        }

        public static int Count(string value, string key)
        {
            Regex rege = new Regex(value, RegexOptions.Compiled);
            return rege.Matches(key).Count;
        }

        /// <summary>
        /// 获取身份证出生日期
        /// </summary>
        /// <param name="idCard">15或18位身份证</param>
        /// <returns></returns>
        public static DateTime GetBirthdayByIdCard(string idCard)
        {
            var birthday = DateTime.Now;
            if (!string.IsNullOrEmpty(idCard) && idCard.Length >= 15)
            {
                var strYear = DateTime.Now.Year;
                var strMonth = DateTime.Now.Month;
                var strDay = DateTime.Now.Day;
                if (idCard.Length == 15)
                {
                    strYear = idCard.Substring(6, 4).TryInt();
                    strMonth = idCard.Substring(8, 2).TryInt();
                    strDay = idCard.Substring(10, 2).TryInt();
                }
                if (idCard.Length == 18)
                {
                    strYear = idCard.Substring(6, 4).TryInt();
                    strMonth = idCard.Substring(10, 2).TryInt();
                    strDay = idCard.Substring(12, 2).TryInt();
                }

                if (strYear > 0 && strMonth > 0 && strDay > 0)
                    birthday = new DateTime(strYear, strMonth, strDay);
            }
            return birthday;
        }

        /// <summary>
        /// 获取身份证性别
        /// </summary>
        /// <param name="idCard">15或18位身份证</param>
        /// <returns></returns>
        public static bool GetSexByIdCard(string idCard)
        {
            if (!string.IsNullOrEmpty(idCard) && idCard.Length >= 15)
            {
                var idSex = 0;
                if (idCard.Length == 15)
                {
                    idSex = idCard.Substring(12, 3).TryInt();
                }
                else if (idCard.Length == 18)
                {
                    idSex = idCard.Substring(14, 3).TryInt();
                }
                return (idSex % 2) == 0 ? false : true;
            }
            return false;
        }

        /// <summary>
        /// 获取身份证年龄
        /// </summary>
        /// <param name="idCard">15或18位身份证</param>
        /// <returns></returns>
        public static int GetAgeByIdCard(string idCard)
        {
            var birthday = GetBirthdayByIdCard(idCard);
            return DateTime.Now.Year - birthday.Year;
        }
        #endregion
    }
}
