using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// Markdown语法
    /// </summary>
    public static class MarkdownHelper
    {
        /// <summary>
        /// 将表格转换为Markdown语法
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string SerializeTable(DataTable dataTable)
        {
            StringBuilder markdownBuilder = new StringBuilder();

            // 添加表头
            markdownBuilder.AppendLine("| " + string.Join(" | ", dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName)) + " |");
            markdownBuilder.AppendLine("|" + string.Concat(Enumerable.Repeat("-", dataTable.Columns.Count * 3 - 1)) + "|");

            // 添加数据行
            foreach (DataRow row in dataTable.Rows)
            {
                markdownBuilder.AppendLine("| " + string.Join(" | ", row.ItemArray.Select(field => field?.ToString()?.Replace("|", "\\|") ?? "")) + " |");
            }

            return markdownBuilder.ToString();
        }
    }
}
