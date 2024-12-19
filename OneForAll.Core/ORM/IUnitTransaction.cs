using OneForAll.Core;
using System;
using System.Data;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    /// <summary>
    /// 单元事务
    /// </summary>
    public interface IUnitTransaction : IDisposable
    {
        /// <summary>
        /// 事务是否已提交
        /// </summary>
        bool Commited { get; set; }

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="conn">连接上下文</param>
        void Register<T>(Func<int> action, T conn);

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="conn">连接上下文</param>
        Task RegisterAsync<T>(Func<Task<int>> action, T conn);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>受影响行数</returns>
        int Commit();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>受影响行数</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 回滚
        /// </summary>
        void RollBack();
    }
}
