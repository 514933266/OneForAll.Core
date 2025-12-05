using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.UploadFile
{
    /// <summary>
    /// 文件上传器
    /// </summary>
    public interface IUploader
    {

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="maxSize">最大限制</param>
        Task<IUploadResult> WriteAsync(Stream fileStream, string path, string fileName, int maxSize = 0);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="autoName">自动生成的文件名（会覆盖原名）</param>
        /// <param name="maxSize">最大限制</param>
        Task<IUploadResult> WriteAsync(Stream fileStream, string path, string fileName, bool autoName = false, int maxSize = 0);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="filePath">文件路径</param>
        void DeleteAsync(string filePath);
    }
}
