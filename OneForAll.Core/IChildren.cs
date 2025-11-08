using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 约束：子级
    /// </summary>
    /// <typeparam name="TType">主键类型</typeparam>
    public interface IChildren<TType>
    {
        /// <summary>
        /// 子级
        /// </summary>
        IEnumerable<TType> Children { get; set; }
    }
}
