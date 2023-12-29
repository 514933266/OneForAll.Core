using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Extension
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取Description
        /// </summary>
        /// <param name="obj">枚举值</param>
        /// <returns>Description</returns>
        public static string GetDescription(this Enum obj)
        {
            return EnumHelper.GetDescription(obj);
        }
    }
}
