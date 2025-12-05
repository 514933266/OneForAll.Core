using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.UploadFile
{
    public interface IUploadResult
    {
        /// <summary>
        /// 上传状态
        /// </summary>
        UploadEnum State { get; set; }

        /// <summary>
        /// 上传后的地址
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// 文件标题
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 原文件名
        /// </summary>
        string Original { get; set; }
    }
}
