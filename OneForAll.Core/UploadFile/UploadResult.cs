using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.UploadFile
{
    public class UploadResult : IUploadResult
    {
        public UploadEnum State { get; set; } = UploadEnum.Fail;

        public string Url { get; set; }

        public string Title { get; set; }

        public string Original { get; set; }
    }
}
