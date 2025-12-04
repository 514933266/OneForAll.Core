using System;
using System.Threading.Tasks;

namespace OneForAll.Core.DDD
{
    /// <summary>
    /// 基础领域类，提供通用的结果处理逻辑，用于将数据库操作或其他返回影响行数的操作
    /// </summary>
    public class BaseManager
    {
        /// <summary>
        /// 执行一个同步操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">一个返回受影响行数的同步函数</param>>
        protected BaseErrType Result(Func<int> action)
        {
            var effected = action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }

        /// <summary>
        /// 异步执行一个操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">一个返回 的异步函数，表示异步操作后受影响的行数</param>
        protected async Task<BaseErrType> ResultAsync(Func<Task<int>> action)
        {
            var effected = await action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }
    }
}
