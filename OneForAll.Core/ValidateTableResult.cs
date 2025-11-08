using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 表格验证结果
    /// </summary>
    public class ValidateTableResult
    {
        public ValidateTableResult()
        {
            Columns = new HashSet<ValidateTableColumnResult>();
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 元数据
        /// </summary>
        public object[] Source { get; set; }

        /// <summary>
        /// 列错误
        /// </summary>
        public ICollection<ValidateTableColumnResult> Columns { get; set; }
    }

    /// <summary>
    /// 表格列验证结果
    /// </summary>
    public class ValidateTableColumnResult
    {
        /// <summary>
        /// 列号
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public string Error { get; set; }
    }
}
