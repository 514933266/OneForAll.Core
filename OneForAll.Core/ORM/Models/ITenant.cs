using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 约束：租户
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface ITenant<TType>
    {
        TType TenantId { get; set; }
    }
}
