using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.UploadFile
{
    /// <summary>
    /// 上传结果
    /// </summary>
    public enum UploadEnum
    {
        /// <summary>
        /// 失败
        /// </summary>
        Fail,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 超出限制
        /// </summary>
        Overflow,
        /// <summary>
        /// 类型错误
        /// </summary>
        TypeError,
        /// <summary>
        /// 错误
        /// </summary>
        Error
    }
}
