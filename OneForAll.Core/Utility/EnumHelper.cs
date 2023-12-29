using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取指定特性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Description</returns>
        public static string GetDescription(Enum obj)
        {
            Type type = obj.GetType();
            string name = Enum.GetName(type, obj);
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var attr = type.GetField(name).GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description;
        }

        /// <summary>
        /// 根据Description获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueByDescription<T>(string description) where T : Enum
        {
            Type type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    if (attributes.First().Description == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }
    }
}