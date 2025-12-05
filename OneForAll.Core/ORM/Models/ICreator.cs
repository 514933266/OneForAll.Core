using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 约束：创建人
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface ICreator<TType>
    {
        /// <summary>
        /// 创建人id
        /// </summary>
        TType CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        string CreatorName { get; set; }
    }
}
