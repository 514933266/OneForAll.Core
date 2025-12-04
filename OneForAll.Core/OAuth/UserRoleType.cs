using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.OAuth
{
    /// <summary>
    /// 授权角色类型
    /// </summary>
    public static class UserRoleType
    {
        /// <summary>
        /// 系统开发者（最高权限，可修改系统核心配置）
        /// </summary>
        public const string Ruler = "ruler";

        /// <summary>
        /// 系统管理员（管理用户、内容等日常运维操作）
        /// </summary>
        public const string Admin = "admin";

        /// <summary>
        /// 普通用户（一般业务操作权限）
        /// </summary>
        public const string User = "user";

        /// <summary>
        /// 审核员 / 审计员
        /// </summary>
        public const string Auditor = "auditor";

        /// <summary>
        /// 运维人员
        /// </summary>
        public const string Operator = "operator";

        /// <summary>
        /// 访客（只读或受限访问）
        /// </summary>
        public const string Guest = "guest";
    }
}
