using OneForAll.Core.ORM.Models;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneForAll.Core.Extension
{
    /// <summary>
    /// 扩展类：集合
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="action">遍历方法</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            CollectionHelper.ForEach(list, action);
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="parent">父级</param>
        /// <param name="deep">是否递归查找</param>
        /// <returns>子级列表</returns>
        public static IEnumerable<T> FindChildren<T, TKey>(
            this IEnumerable<T> list,
            T parent,
            bool deep = true) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            return CollectionHelper.FindChildren<T, TKey>(list, parent, deep);
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="parentId">父级id</param>
        /// <param name="deep">是否递归查找</param>
        /// <returns>子级列表</returns>
        public static IEnumerable<T> FindChildren<T, TKey>(
            this IEnumerable<T> list,
            TKey parentId,
            bool deep = true) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            return CollectionHelper.FindChildren(list, parentId, deep);
        }

        /// <summary>
        /// 查找父级
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="entity">父级</param>
        /// <returns>父级</returns>
        public static T FindParent<T, TKey>(
            this IEnumerable<T> list,
            T entity) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            return CollectionHelper.FindParent<T, TKey>(list, entity);
        }

        /// <summary>
        /// 查找父级
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="id">id</param>
        /// <param name="parentId">父级id</param>
        /// <returns>父级</returns>
        public static T FindParent<T, TKey>(
            this IEnumerable<T> list,
            TKey id,
            TKey parentId) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            return CollectionHelper.FindParent(list, id, parentId);
        }

        /// <summary>
        /// 转换树形式
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>树</returns>
        public static IEnumerable<T> ToTree<T, TKey>(
            this IEnumerable<T> list) where T : IEntity<TKey>, IParent<TKey>, IChildren<T>, new()
        {
            return CollectionHelper.ConverToTree<T, TKey>(list);
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="id">id</param>
        /// <returns>节点</returns>
        public static T FindNode<T, TKey>(
            this IEnumerable<T> list,
            TKey id) where T : IEntity<TKey>, IChildren<T>, new()
        {
            return CollectionHelper.FindNode(list, id);
        }
    }
}
