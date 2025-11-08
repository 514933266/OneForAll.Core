using Microsoft.VisualBasic;
using System;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 帮助类：日期时间
    /// </summary>
    public static class TimeHelper
    {

        #region 时间戳

        /// <summary>
        /// 转换当前时间的时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        public static long ToTimeStamp()
        {
            return ToTimeStamp(DateTime.Now);
        }
        /// <summary>
        /// 转换指定时间为时间戳
        /// </summary>
        /// <param name="datetime">终止时间</param>
        /// <returns>时间戳</returns>
        public static long ToTimeStamp(DateTime datetime)
        {
            return new DateTimeOffset(datetime).ToUnixTimeSeconds();
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="unix">时间戳</param>
        /// <returns>时间</returns>
        public static DateTime ToDateTime(long unix)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().DateTime;
        }
        #endregion

        #region 中文格式时间
        /// <summary>
        /// 获取中文格式时间：yyyy年MM月dd日 星期x
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>星期x</returns>
        public static string ToChineseTimeWeek(DateTime datetime)
        {
            string time = string.Empty;
            DayOfWeek weekDay = datetime.DayOfWeek;
            switch (weekDay)
            {
                case DayOfWeek.Sunday:
                    time = string.Format("{0} 星期{1}", datetime, "日");
                    break;
                case DayOfWeek.Monday:
                    time = string.Format("{0} 星期{1}", datetime, "一");
                    break;
                case DayOfWeek.Tuesday:
                    time = string.Format("{0} 星期{1}", datetime, "二");
                    break;
                case DayOfWeek.Wednesday:
                    time = string.Format("{0} 星期{1}", datetime, "三");
                    break;
                case DayOfWeek.Thursday:
                    time = string.Format("{0} 星期{1}", datetime, "四");
                    break;
                case DayOfWeek.Friday:
                    time = string.Format("{0} 星期{1}", datetime, "五");
                    break;
                case DayOfWeek.Saturday:
                    time = string.Format("{0} 星期{1}", datetime, "六");
                    break;
            }
            return time;
        }

        #endregion

        #region 获取日期

        /// <summary>
        /// 转换日期为起始时间（当月第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToFirstDate(DateTime? date)
        {
            return date.HasValue ? ConvertToFirstDate(date.Value) : ConvertToFirstDate(DateTime.Now);
        }

        /// <summary>
        /// 转换日期为起始时间（当月第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToFirstDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 转换日期为结束时间（当月最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToLastDate(DateTime? date)
        {
            return date.HasValue ? ConvertToLastDate(date.Value) : ConvertToLastDate(DateTime.Now);
        }

        /// <summary>
        /// 转换日期为结束时间（当月最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToLastDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// 转换日期为当天开始时间（23:59）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToDayEnd(DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 转换日期为当天开始时间（23:59）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>起始时间</returns>
        public static DateTime ConvertToDayEnd(DateTime? date)
        {
            return date.HasValue ? date.Value.Date.AddDays(1).AddSeconds(-1) : ConvertToDayEnd(DateTime.Now);
        }
        #endregion
    }
}
