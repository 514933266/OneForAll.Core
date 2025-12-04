using OneForAll.Core.ORM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneForAll.Core.DDD
{
    /// <summary>
    /// 约束：聚合根
    /// </summary>
    /// <typeparam name="TType">主键类型</typeparam>
    public class Entity<TType> : IEntity<TType>
    {
        [Key]
        [Required]
        public virtual TType Id { get; set; }
    }
}
