using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 枚举：日期部件
    /// </summary>
    public enum DatePart
    {
        /// <summary>
        /// 年份部分（例如：2025）
        /// </summary>
        Year,

        /// <summary>
        /// 月份部分（1 到 12）
        /// </summary>
        Month,

        /// <summary>
        /// 周部分（一年中的第几周，通常基于日历周计算）
        /// </summary>
        Week,

        /// <summary>
        /// 日期中的天部分（1 到 31，具体取决于月份）
        /// </summary>
        Day,

        /// <summary>
        /// 小时部分（0 到 23，24 小时制）
        /// </summary>
        Hour,

        /// <summary>
        /// 分钟部分（0 到 59）
        /// </summary>
        Minute,

        /// <summary>
        /// 秒部分（0 到 59）
        /// </summary>
        Second
    }
}
