using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.OAuth
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否默认用户
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
