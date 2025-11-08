using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    /// <summary>
    /// 数据库约束特性：检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CheckAttribute : Attribute
    {
        #region 字段、属性

        private string _rule;
        /// <summary>
        /// 规则内容 例：Id的>0
        /// </summary>
        public string Rule { get { return _rule; } }

        #endregion

        #region 构造

        public CheckAttribute(string rule)
        {
            _rule = rule;
        }

        #endregion
    }
}
