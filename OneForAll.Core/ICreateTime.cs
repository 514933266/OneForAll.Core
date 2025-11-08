using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 约束：创建时间
    /// </summary>
    public interface ICreateTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
