using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 接口：操作人
    /// </summary>
    public interface IOperator<TType>
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        TType OperatorId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        string OperatorName { get; set; }
    }
}
