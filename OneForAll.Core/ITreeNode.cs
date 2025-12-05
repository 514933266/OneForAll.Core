using OneForAll.Core.ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 接口：树
    /// </summary>
    public interface ITreeNode<TKey, T> : IEntity<TKey>, IParent<TKey>, IChildren<T>
    {
    }
}
