using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    /// <summary>
    /// 数据库锁对象
    /// </summary>
    public static class DbLock
    {
        /// <summary>
        /// 默认设置
        /// </summary>
        public const string Default = "";

        /// <summary>
        /// 不添加共享锁和排它锁，可能读到未提交读的数据或“脏数据”
        /// </summary>
        public const string NoLock = "(NOLOCK)";
    }
}
