using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;


namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 帮助类：对象类型处理
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        ///  将一个枚举类型转换成字典
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="keyDefault">默认key值</param>
        /// <param name="valueDefault">默认value值</param>
        /// <returns>枚举成员字典</returns>
        public static Dictionary<string, object> ToDic(Type enumType, string keyDefault = "", string valueDefault = "")
        {
            var dicEnum = new Dictionary<string, object>();
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) 
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            var fieldstrs = Enum.GetNames(enumType);
            foreach (var item in fieldstrs)
            {
                dicEnum.Add(item, (int)Enum.Parse(enumType, item));
            }
            return dicEnum;
        }

        #region Type

        /// <summary>
        /// 查找IEnumerable对象
        /// </summary>
        public static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (var arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            var ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }

        public static Type GetSequenceType(Type elementType)
        {
            return typeof(IEnumerable<>).MakeGenericType(elementType);
        }

        /// <summary>
        /// 获取IEnumerable成员类型
        /// </summary>
        public static Type GetElementType(Type seqType)
        {
            var ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }
        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        public static bool IsNullableType(Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        public static bool IsNullAssignable(Type type)
        {
            return !type.IsValueType || IsNullableType(type);
        }

        /// <summary>
        /// 获取不可空类型
        /// </summary>
        public static Type GetNonNullableType(Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        /// <summary>
        /// 获取可空类型
        /// </summary>
        public static Type GetNullAssignableType(Type type)
        {
            if (!IsNullAssignable(type))
            {
                return typeof(Nullable<>).MakeGenericType(type);
            }
            return type;
        }

        /// <summary>
        /// 获取空值常量表达式
        /// </summary>
        public static ConstantExpression GetNullConstant(Type type)
        {
            return Expression.Constant(null, GetNullAssignableType(type));
        }

        /// <summary>
        /// 获取成员对象的类型
        /// </summary>
        public static Type GetMemberType(MemberInfo mi)
        {
            var fi = mi as FieldInfo;
            if (fi != null) return fi.FieldType;
            var pi = mi as PropertyInfo;
            if (pi != null) return pi.PropertyType;
            var ei = mi as EventInfo;
            if (ei != null) return ei.EventHandlerType;
            var meth = mi as MethodInfo;  // property getters really
            if (meth != null) return meth.ReturnType;
            return null;
        }

        /// <summary>
        /// 获取类型默认值
        /// </summary>
        public static object GetDefault(Type type)
        {
            bool isNullable = !type.IsValueType || IsNullableType(type);
            if (!isNullable)
                return Activator.CreateInstance(type);
            return null;
        }

        /// <summary>
        /// 判断成员是否只读
        /// </summary>
        public static bool IsReadOnly(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return (((FieldInfo)member).Attributes & FieldAttributes.InitOnly) != 0;
                case MemberTypes.Property:
                    PropertyInfo pi = (PropertyInfo)member;
                    return !pi.CanWrite || pi.GetSetMethod() == null;
                default:
                    return true;
            }
        }

        /// <summary>
        ///  是否数字类型
        /// </summary>
        public static bool IsInteger(this Type type)
        {
            Type nnType = GetNonNullableType(type);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
