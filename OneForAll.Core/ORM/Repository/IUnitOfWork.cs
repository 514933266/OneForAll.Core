using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        HashSet<Exception> Exceptions { get; set; }

        /// <summary>
        /// 注册事务
        /// </summary>
        /// <param name="transactionType">事务类型</param>
        /// <returns></returns>
        IUnitTransaction BeginTransaction(TransactionType transactionType);

        /// <summary>
        /// 提交所有事务并返回影响行数
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// 提交所有事务并返回影响行数
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        void RollBack();

    }
}
