using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.DDD
{
    /// <summary>
    /// 基础领域类
    /// </summary>
    public class BaseManager
    {
        protected BaseErrType Result(Func<int> action)
        {
            var effected = action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }

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
