using Microsoft.AspNetCore.Http;
using OneForAll.Core.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.DDD
{
    /// <summary>
    /// 基础应用服务
    /// </summary>
    public class BaseService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 用户id
        /// </summary>
        protected Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(e => e.Type == UserClaimType.USER_ID);

                if (userId != null)
                {
                    return new Guid(userId.Value);
                }
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        protected string UserName
        {
            get
            {
                var username = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(e => e.Type == UserClaimType.USERNAME);

                if (username != null)
                {
                    return username.Value;
                }
                return null;
            }
        }

        /// <summary>
        /// 租户id
        /// </summary>
        protected Guid TenantId
        {
            get
            {
                var tenantId = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(e => e.Type == UserClaimType.TENANT_ID);

                if (tenantId != null)
                {
                    return new Guid(tenantId.Value);
                }
                return Guid.Empty;
            }
        }

        protected LoginUser LoginUser
        {
            get
            {
                var name = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(e => e.Type == UserClaimType.USER_NICKNAME);

                var role = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(e => e.Type == UserClaimType.ROLE);

                return new LoginUser()
                {
                    Id = UserId,
                    Name = name?.Value,
                    TenantId = TenantId
                };
            }
        }
    }
}