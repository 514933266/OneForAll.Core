using OneForAll.Core.Extension;
using OneForAll.Core.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 帮助类：集合
    /// </summary>
    public static class CollectionHelper
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="action">遍历方法</param>
        public static void ForEach<T>(IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
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
            IEnumerable<T> list,
            T parent,
            bool deep = true) where T : IEntity<TKey>, IParent<TKey>, new ()
        {
            var result = new List<T>();
            var children = list.Where(w => w.ParentId.Equals(parent.Id)).ToList();
            if (children.Count > 0)
            {
                result.AddRange(children);
                if (deep)
                {
                    children.ForEach(e =>
                    {
                        var deepChildren = FindChildren<T, TKey>(list, e, deep);
                        if (deepChildren.Count() > 0) result.AddRange(deepChildren);
                    });
                }
            }
            return result;
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
            IEnumerable<T> list,
            TKey parentId,
            bool deep = true) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            var obj = new T() { Id = parentId };
            return FindChildren<T, TKey>(list, obj, deep);
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
            IEnumerable<T> list,
            T entity) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            var key = default(TKey);
            if (!entity.ParentId.Equals(key))
            {
                var children = FindChildren(list, entity.Id);
                var parent = children.FirstOrDefault(w => w.Id.Equals(entity.ParentId));
                if (parent != null) return default;

                parent = list.FirstOrDefault(w => w.Id.Equals(entity.ParentId));
                if (parent != null) return parent;
            }
            return default;
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
            IEnumerable<T> list,
            TKey id,
            TKey parentId) where T : IEntity<TKey>, IParent<TKey>, new()
        {
            var parent = new T() { Id = id, ParentId = parentId };
            return FindParent<T, TKey>(list, parent);
        }

        /// <summary>
        /// 转换树形式
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>树</returns>
        public static IEnumerable<T> ConverToTree<T, TKey>(
            IEnumerable<T> list) where T : IEntity<TKey>, IParent<TKey>, IChildren<T>, new()
        {
            var key = default(TKey);
            var tops = new List<T>();

            list.ForEach(e =>
            {
                if (e.ParentId.Equals(key) || !list.Any(w => w.Id.Equals(e.ParentId)))
                {
                    tops.Add(e);
                }
            });
            tops.ForEach(e =>
            {
                ConverToTree<T, TKey>(list, e);
            });
            return tops;
        }

        private static void ConverToTree<T, TKey>(
            IEnumerable<T> list,
            T parent) where T : IEntity<TKey>, IParent<TKey>, IChildren<T>, new()
        {
            parent.Children = list.Where(w => w.ParentId.Equals(parent.Id)).ToList();
            foreach(var item in parent.Children)
            {
                ConverToTree<T, TKey>(list, item);
            }
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
            IEnumerable<T> list,
            TKey id) where T : IEntity<TKey>, IChildren<T>, new()
        {
            var node = list.Where(w => w.Id.Equals(id)).FirstOrDefault();
            if (node == null)
            {
                for (var i = 0; i < list.Count(); i++)
                {
                    var deepNode = FindNode(list.ElementAt(i).Children, id);
                    if (deepNode != null)
                    {
                        return deepNode;
                    }
                }
            }
            return node;
        }
    }
}
