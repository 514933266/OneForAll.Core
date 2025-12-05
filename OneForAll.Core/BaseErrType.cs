using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 基础错误类型
    /// </summary>
    public enum BaseErrType
    {
        #region 系统级别错误

        /// <summary>
        /// 第三方服务不可用
        /// </summary>
        ThirdPartyServiceUnavailable = -30002,

        /// <summary>
        /// 网络超时
        /// </summary>
        NetworkTimeout = -30001,

        /// <summary>
        /// 服务器错误
        /// </summary>
        ServerError = -30000,

        #endregion

        #region 客户端/业务逻辑错误

        /// <summary>
        /// 业务规则不满足
        /// </summary>
        BusinessRuleViolation = -20016,

        /// <summary>
        /// 不支持的文件类型
        /// </summary>
        UnsupportedFileType = -20015,

        /// <summary>
        /// 文件过大
        /// </summary>
        FileTooLarge = -20014,

        /// <summary>
        /// 请求过于频繁（触发限流）
        /// </summary>
        TooManyRequests = -20013,

        /// <summary>
        /// 状态冲突（如：操作与当前状态不符）
        /// </summary>
        StateConflict = -20012,

        /// <summary>
        /// 请求参数无效
        /// </summary>
        InvalidParameter = -20011,

        /// <summary>
        /// 客户端无效
        /// </summary>
        ClientInvalid = -20010,

        /// <summary>
        /// 等级过低
        /// </summary>
        LowLevel = - 20009,

        /// <summary>
        /// 超出限制
        /// </summary>
        Overflow = -20008,

        /// <summary>
        /// 不允许
        /// </summary>
        NotAllow = -20007,

        /// <summary>
        /// 已被冻结
        /// </summary>
        Frozen = -20006,

        /// <summary>
        /// 权限不足
        /// </summary>
        PermissionNotEnough = -20005,

        /// <summary>
        /// 验证码无效
        /// </summary>
        AuthCodeInvalid = -20004,

        /// <summary>
        /// 用户名不存在
        /// </summary>
        AccountNotFound = -20003,

        /// <summary>
        /// 密码无效
        /// </summary>
        PasswordInvalid = -20002,

        /// <summary>
        /// 令牌失效
        /// </summary>
        TokenInvalid = -20001,

        /// <summary>
        /// 时间戳无效
        /// </summary>
        TimestampInvalid = -20000,

        #endregion

        #region 数据层错误

        /// <summary>
        /// 资源不存在
        /// </summary>
        ResourceNotFound = -10005,

        /// <summary>
        /// 数据为空
        /// </summary>
        DataEmpty = -10004,

        /// <summary>
        /// 数据不匹配
        /// </summary>
        DataNotMatch = -10003,

        /// <summary>
        /// 找不到数据
        /// </summary>
        DataNotFound = -10002,

        /// <summary>
        /// 数据已存在
        /// </summary>
        DataExist = -10001,

        /// <summary>
        /// 数据异常
        /// </summary>
        DataError = -10000,

        #endregion

        /// <summary>
        /// 未知失败
        /// </summary>
        UnknownFailure = -1,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1
    }
}
