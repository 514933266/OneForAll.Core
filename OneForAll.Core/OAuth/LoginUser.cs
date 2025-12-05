using OneForAll.Core.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.OAuth
{
    /// <summary>
    /// 登录用户信息载体，用于在系统中传递经过认证的用户上下文
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 用户的唯一标识符 (ID)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户所属租户的唯一标识符
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 用户显示名称（如昵称或真实姓名），用于界面展示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否为租户默认用户（如超级管理员）
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 微信小程序 AppID
        /// </summary>
        public string WxAppId { get; set; }

        /// <summary>
        /// 微信 OpenID
        /// </summary>
        public string WxOpenId { get; set; }

        /// <summary>
        /// 微信 UnionID
        /// </summary>
        public string WxUnionId { get; set; }

        /// <summary>
        /// 用户角色列表
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 用户权限列表
        /// </summary>
        public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();
    }
}
