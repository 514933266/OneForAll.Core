using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.Extension
{
    /// <summary>
    /// 扩展类：时间
    /// </summary>
    public static class TimeExtension
    {
        /// <summary>
        /// 转换日期为起始时间（当月第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ToFirstDate(this DateTime? date)
        {
            return TimeHelper.ConvertToFirstDate(date);
        }

        /// <summary>
        /// 转换日期为起始时间（当月第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ToFirstDate(this DateTime date)
        {
            return TimeHelper.ConvertToFirstDate(date);
        }

        /// <summary>
        /// 转换日期为结束时间（当月最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ToLastDate(this DateTime? date)
        {
            return TimeHelper.ConvertToLastDate(date);
        }

        /// <summary>
        /// 转换日期为结束时间（当月最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ToLastDate(this DateTime date)
        {
            return TimeHelper.ConvertToLastDate(date);
        }
    }
}
