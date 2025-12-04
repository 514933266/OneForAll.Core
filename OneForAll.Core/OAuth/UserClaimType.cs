using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.OAuth
{
    /// <summary>
    /// 用户声明（Claim）类型常量。
    /// </summary>
    public static class UserClaimType
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public const string TenantId = "tenant_id";

        /// <summary>
        /// 用户名
        /// </summary>
        public const string UserName = "user_name";

        /// <summary>
        /// 用户昵称
        /// </summary>
        public const string DisplayName = "display_name";

        /// <summary>
        /// 用户ID
        /// </summary>
        public const string UserId = "user_id";

        /// <summary>
        /// 人员ID
        /// </summary>
        public const string PersonId = "person_id";

        /// <summary>
        /// 是否为默认
        /// </summary>
        public const string IsDefault = "is_default";

        /// <summary>
        /// 微信小程序AppId
        /// </summary>
        public const string WxAppId = "wx_appid";

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public const string WxOpenId = "wx_openid";

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public const string WxUnionId = "wx_unionid";

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// 用户手机号
        /// </summary>
        public const string PhoneNumber = "phone_number";

        /// <summary>
        /// 用户角色
        /// </summary>
        public const string Role = "role";
    }
}
