using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 接口：操作人
    /// </summary>
    public interface IOperator<TType> : IEntity<TType>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
