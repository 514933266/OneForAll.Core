using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 约束：父级
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface IParent<TType>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        TType ParentId { get; set; }
    }
}
