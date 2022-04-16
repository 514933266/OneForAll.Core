﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.OAuth
{
    /// <summary>
    /// 授权用户身份信息
    /// </summary>
    public class UserClaimType
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public const string TENANT_ID = "TenantId";

        /// <summary>
        /// 用户名
        /// </summary>
        public const string USERNAME = "UserName";

        /// <summary>
        /// 名称
        /// </summary>
        public const string USER_NICKNAME = "UserNickname";

        /// <summary>
        /// id
        /// </summary>
        public const string USER_ID = "UserId";

        /// <summary>
        /// 角色
        /// </summary>
        public const string ROLE = "Role";
    }
}
