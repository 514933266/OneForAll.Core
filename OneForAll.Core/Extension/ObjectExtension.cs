using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OneForAll.Core.Extension
{
    /// <summary>
    /// 扩展类：Object
    /// </summary>
   public static class ObjectExtension
    {
        #region 转换

        /// <summary>
        /// 尝试转换Long类型,Null值返回-1
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static long TryLong(this object o)
        {
            long outNum;
            if (null == o)
            {
                return -1;
            }
            else
            {
                long.TryParse(o.ToString(), out outNum);
                return outNum;
            }
        }


        /// <summary>
        /// 尝试转换Int类型,Null值返回-1
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static int TryInt(this object o)
        {
            int outNum;
            if (null == o)
            {
                return -1;
            }
            else
            {
                var str = o.ToString();
                if (str == "true")
                {
                    outNum = 1;
                }
                else if (str == "false")
                {
                    outNum = 0;
                }
                else
                {
                    int.TryParse(str, out outNum);
                }
                return outNum;
            }
        }


        /// <summary>
        /// 尝试转换byte类型,Null值返回-1
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static int TryByte(this object o)
        {
            byte outNum;
            if (null == o)
            {
                return -1;
            }
            else
            {
                var str = o.ToString();
                if (str.ToLower() == "true")
                {
                    outNum = 1;
                }
                else if (str.ToLower() == "false")
                {
                    outNum = 0;
                }
                else
                {
                    byte.TryParse(str, out outNum);
                }
                return outNum;
            }
        }

        /// <summary>
        /// 尝试转换Decimal类型
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static decimal TryDecimal(this object o)
        {
            decimal outNum;
            if (null == o)
            {
                return -1;
            }
            else
            {
                decimal.TryParse(o.ToString(), out outNum);
                return outNum;
            }
        }

        /// <summary>
        /// 尝试转换Bool类型
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static bool TryBoolean(this object o)
        {
            bool outNum;
            if (null == o)
            {
                return false;
            }
            else
            {
                var str = o.ToString();
                if (str == "1")
                {
                    outNum = true;
                }
                else if (str == "0")
                {
                    outNum = false;
                }
                else
                {
                    bool.TryParse(str, out outNum);
                }
                return outNum;
            }
        }

        /// <summary>
        /// 尝试转换Guid类型
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static Guid TryGuid(this object o)
        {
            Guid outNum;
            if (null == o)
            {
                return new Guid();
            }
            else
            {
                Guid.TryParse(o.ToString(), out outNum);
                return outNum;
            }
        }

        /// <summary>
        /// 尝试转换DateTime类型
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static DateTime TryDateTime(this object o)
        {
            DateTime outNum;
            if (null == o)
            {
                return DateTime.MinValue;
            }
            else
            {
                DateTime.TryParse(o.ToString(), out outNum);
                return outNum;
            }
        }

        /// <summary>
        /// 尝试转换String类型
        /// </summary>
        /// <param name="o">转换对象</param>
        /// <returns>转换值</returns>
        public static string TryString(this object o)
        {
            if (null == o)
            {
                return string.Empty;
            }
            else
            {
                return o.ToString();
            }
        }

        /// <summary>
        ///  检查对象是否为null
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象值</param>
        /// <returns>是否为NULL</returns>
        public static bool IsNull<T>(this T obj)
        {
            return obj == null ? true : false;
        }
        #endregion

        #region 特殊

        /// <summary>
        /// 判断对象是否存在于集合中
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象值</param>
        /// <param name="list">集合对象</param>
        /// <returns>是否为NULL</returns>
        public static bool In<T>(this T obj, IEnumerable<T> list)
        {
            return list.Any(o => o.Equals(obj));
        }
        /// <summary>
        /// 判断对象是否不存在集合中
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象值</param>
        /// <param name="list">集合对象</param>
        /// <returns>是否为NULL</returns>
        public static bool NotIn<T>(this T obj, IEnumerable<T> list)
        {
            return !list.Any(o => o.Equals(obj));
        }
        #endregion

        #region 异常处理

        /// <summary>
        /// 如果NULL值则抛出异常
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="argument">对象值</param>
        /// <param name="paramName">异常名称</param>
        public static void ThrowIfNull<T>(this T argument, string paramName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        /// <summary>
        /// 符合条件抛出异常
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="argument">对象值</param>
        /// <param name="predicate">条件</param>
        /// <param name="msg">异常消息</param>
        public static void ThrowIf<T>(this T argument, Func<T, bool> predicate, string msg)
        {
            if (predicate(argument))
            {
                throw new ArgumentException(msg);
            }
        }

        #endregion

    }
}
