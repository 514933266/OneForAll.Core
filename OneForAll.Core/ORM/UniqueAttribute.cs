using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    /// <summary>
    /// 数据库约束特性：唯一
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UniqueAttribute : Attribute
    {
    }
}
