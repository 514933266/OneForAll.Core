using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 约束：假删除
    /// </summary>
    public interface IDeleted
    {
        bool IsDeleted { get; set; }
    }
}
