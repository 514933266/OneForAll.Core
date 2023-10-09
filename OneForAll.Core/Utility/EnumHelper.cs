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
        /// 获取Description
        /// </summary>
        /// <param name="obj">枚举值</param>
        /// <param name="name">名称</param>
        /// <returns>Description</returns>
        public static string GetAttribute<T>(Enum obj, string name) where T : Attribute
        {
            var field = obj.GetType().GetField(name);
            var attrs = System.Attribute.GetCustomAttributes(field, typeof(T));
            var props = typeof(T).GetProperties();
            foreach (var attr in attrs)
            {
                var prop = props.FirstOrDefault(w => w.Name == name);
                if (prop != null)
                {
                    return prop.GetValue(attr).ToString();
                }
            }
            return "";
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
                if (field.Name == description)
                {
                    return (T)field.GetValue(null);
                }

                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.FirstOrDefault() != null)
                {
                    if (attributes.First().Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 根据Description获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetEnumItemByName<T>(string name) where T : Enum
        {
            Type type = typeof(T);
            return (T)Enum.Parse(typeof(T), name);

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", name), "name");
        }
    }
}