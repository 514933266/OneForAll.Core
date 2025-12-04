using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class BaseEntity<TType> : ITenant<TType>, ICreateTime, IUpdateTime, ICreator<TType>, IOperator<TType>, IDeleted
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key]
        [Required]
        [PrimaryKey]
        public TType Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public TType TenantId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public TType CreatorId { get; set; }

        /// <summary>
        /// 操作人id
        /// </summary>
        [Required]
        public TType OperatorId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string OperatorName { get; set; } = "";

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 是否已删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }
    }
}
