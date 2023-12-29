using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 对象帮助类
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 获取指定特性
        /// </summary>
        /// <returns>Description</returns>
        public static bool HasAttribute<T>(object obj, string propName) where T : Attribute
        {
            return GetAttribute<T>(obj, propName) == null ? false : true;
        }

        /// <summary>
        /// 获取指定特性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Description</returns>
        public static T GetAttribute<T>(object obj, string propName) where T : Attribute
        {
            var prop = obj.GetType().GetProperty(propName);
            return prop?.GetCustomAttributes<T>()?.FirstOrDefault();
        }

        /// <summary>
        /// 获取指定特性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propName">属性字段名</param>
        /// <param name="attrPropName">特性属性字段名</param>
        /// <returns>Description</returns>
        public static string GetAttributeValue<T>(object obj, string propName, string attrPropName) where T : Attribute
        {
            var attr = GetAttribute<T>(obj, propName);
            if (attr != null)
            {
                var prop = attr.GetType().GetProperties().FirstOrDefault(w => w.Name == attrPropName);
                if (prop != null)
                    return prop.GetValue(attr).ToString();
            }
            return string.Empty;
        }
    }
}
