using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneForAll.Core
{
    /// <summary>
    /// 基础领域类，提供通用的结果处理逻辑，用于将数据库操作或其他返回影响行数的操作
    /// </summary>
    public class BaseManager
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseManager(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 从用户声明中解析当前登录用户信息。
        /// </summary>
        protected LoginUser LoginUser
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                if (claims != null && claims.Any())
                {
                    return new LoginUser
                    {
                        DisplayName = claims.FirstOrDefault(e => e.Type == UserClaimType.DisplayName)?.Value ?? "",
                        UserName = claims.FirstOrDefault(e => e.Type == UserClaimType.UserName)?.Value ?? "",
                        WxAppId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxAppId)?.Value ?? "",
                        WxOpenId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxOpenId)?.Value ?? "",
                        WxUnionId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxUnionId)?.Value ?? "",
                        Id = claims.FirstOrDefault(e => e.Type == UserClaimType.UserId)?.Value ?? "",
                        TenantId = claims.FirstOrDefault(e => e.Type == UserClaimType.TenantId)?.Value ?? "",
                        IsDefault = claims.FirstOrDefault(e => e.Type == UserClaimType.IsDefault)?.Value.TryBoolean() ?? false
                    };
                }
                return new LoginUser();
            }
        }

        /// <summary>
        /// 执行一个同步操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">一个返回受影响行数的同步函数</param>>
        protected BaseErrType Result(Func<int> action)
        {
            var effected = action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }

        /// <summary>
        /// 异步执行一个操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">一个返回 的异步函数，表示异步操作后受影响的行数</param>
        protected async Task<BaseErrType> ResultAsync(Func<Task<int>> action)
        {
            var effected = await action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }
    }
}
