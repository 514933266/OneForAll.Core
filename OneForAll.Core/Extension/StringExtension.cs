using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OneForAll.Core.Extension
{
    public static class StringExtension
    {
        #region 字符串处理

        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="format">占位字符串</param>
        /// <param name="args">占位值</param>
        /// <returns>新字符串</returns>
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="value">当前值</param>
        /// <param name="appendValue">追加值</param>
        /// <returns></returns>
        public static string Append(this string value, string appendValue)
        {
            return value += appendValue;
        }

        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">转换对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson<T>(this T value)
        {
            return SerializationHelper.SerializeToJson(value);
        }

        /// <summary>
        /// 序列化Json字符串为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">json字符串</param>
        /// <returns>对象</returns>
        public static T FromJson<T>(this string value)
        {
            return SerializationHelper.DeserializeFromJson<T>(value);
        }

        /// <summary>
        /// 半角转成全角  
        /// </summary>
        public static string DBCToSBC(this string value)
        {
            return CodeHelper.DBCToSBC(value);
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        public static string SBCToDBC(this string value)
        {
            return CodeHelper.SBCToDBC(value);
        }

        /// <summary>
        /// 用制定字符串作为分隔符切割字符串
        /// </summary>
        public static List<string> ToList(this string value, string[] split)
        {
            var splitArray = value.Split(split, StringSplitOptions.RemoveEmptyEntries);
            return splitArray.ToList();
        }

        #endregion

        #region 字符串校验

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否含有中文
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool HasChinese(this string value)
        {
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否全中文字符
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        /// <summary>
        /// 是否手机号
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsMobile(this string value)
        {
            return new Regex("^1[0-9]{10}$").IsMatch(value);
        }

        /// <summary>
        /// 是否电话
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsPhone(this string value)
        {
            return new Regex(@"^(\d{3,4}-?)?\d{7,8}$").IsMatch(value);
        }

        /// <summary>
        /// 是否IP地址
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIP(this string value)
        {
            if (value.IsIPV4() || value.IsIPV6()) return true;
            return false;
        }

        /// <summary>
        /// 是否IP4地址
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIPV4(this string value)
        {
            string[] IPs = value.Split('.');

            for (int i = 0; i < IPs.Length; i++)
            {
                if (!Regex.IsMatch(IPs[i], @"^\d+$"))
                {
                    return false;
                }
                if (Convert.ToUInt16(IPs[i]) > 255)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否IP6地址
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIPV6(this string value)
        {
            string pattern = "";
            string temp = value;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = StringHelper.Count(value, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";
                return Regex.IsMatch(pattern, value);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                return Regex.IsMatch(pattern, value);
            }
        }

        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="id">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIDCard(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            if (id.Length == 18)
                return IsIDCard18(id);
            else if (id.Length == 15)
                return IsIDCard15(id);
            else
                return false;
        }

        /// <summary>
        /// 是否15位身份证
        /// </summary>
        /// <param name="Id">字符串值</param>
        /// <returns>结果</returns>
        static bool IsIDCard15(this string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证

            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证

            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 是否18位身份证
        /// </summary>
        /// <param name="Id">字符串值</param>
        /// <returns>结果</returns>
        static bool IsIDCard18(this string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            int y = -1;
            System.Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                return false;//校验码验证

            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsDate(this string value)
        {
            return new Regex(@"(\d{4})-(\d{1,2})-(\d{1,2})").IsMatch(value);
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="numericStr">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNumeric(this string numericStr)
        {
            return new Regex(@"^[-]?[0-9]+(\.[0-9]+)?$").IsMatch(numericStr);
        }

        /// <summary>
        /// 是否小写
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsLetter(this string value)
        {
            return new Regex(@"^[A-Za-z]+$").IsMatch(value);
        }

        /// <summary>
        /// 是否压缩
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsZipCode(this string value)
        {
            return new Regex(@"^\d{6}$").IsMatch(value);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="list">内容集合</param>
        /// <returns>结果</returns>
        public static bool Contains(this string value, IEnumerable<string> list)
        {
            return list.Any(value.Contains);
        }

        #endregion

    }
}
