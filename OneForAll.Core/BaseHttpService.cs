using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core
{
    /// <summary>
    /// HTTP 服务基类，提供带认证的 HttpClient 和当前用户信息。
    /// </summary>
    public class BaseHttpService
    {
        private readonly string AUTH_KEY = "Authorization";
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpClientFactory _httpClientFactory;

        public BaseHttpService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 是否强制要求 HttpClient 必须配置 BaseAddress。
        /// 默认为 false（允许动态传完整 URL）。
        /// 子类可重写此属性以开启校验。
        /// </summary>
        protected virtual bool RequireBaseAddress => false;

        /// <summary>
        /// 从当前请求头中获取 Authorization 令牌。
        /// </summary>
        protected string Token
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    return context.Request.Headers
                        .FirstOrDefault(h => h.Key.Equals(AUTH_KEY, StringComparison.OrdinalIgnoreCase))
                        .Value.TryString();
                }
                return "";
            }
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
        /// 创建指定名称的 HttpClient，并自动附加认证令牌（如有）。
        /// </summary>
        /// <param name="name">HttpClient 的注册名称。</param>
        /// <param name="token">可选的认证令牌（应为完整值，如 "Bearer xxx"）。</param>
        protected HttpClient GetHttpClient(string name, string token = "")
        {
            var client = _httpClientFactory.CreateClient(name);

            // 确定最终使用的令牌
            string finalToken = !string.IsNullOrEmpty(token) ? token : Token;

            if (!string.IsNullOrEmpty(finalToken))
            {
                client.DefaultRequestHeaders.Remove(AUTH_KEY);
                client.DefaultRequestHeaders.Add(AUTH_KEY, finalToken);
            }

            return client;
        }
    }
}
