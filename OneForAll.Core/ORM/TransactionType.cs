using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.ORM
{
    public enum TransactionType
    {
        /// <summary>
        ///  本地事务（相同数据库连接）
        /// </summary>
        Local = 1,

        /// <summary>
        /// 分布式事务（不同数据库连接）
        /// </summary>
        Distribute = 2,

        /// <summary>
        /// 补偿事务
        /// </summary>
        Compensate = 3
    }
}
