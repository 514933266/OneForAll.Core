using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    /// <summary>
    /// 约束：修改时间
    /// </summary>
    public interface IUpdateTime
    {
        DateTime UpdateTime { get; set; }
    }
}