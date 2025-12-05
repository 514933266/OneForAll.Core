using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 约束：聚合根
    /// </summary>
    /// <typeparam name="TType">主键类型</typeparam>
    public interface IAggregateRoot<TType> : IEntity<TType>
    {
    }
}
