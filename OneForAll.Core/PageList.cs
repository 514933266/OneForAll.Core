using System.Collections.Generic;
using System.Linq;

namespace OneForAll.Core
{
    /// <summary>
    /// 分页对象
    /// </summary>
    public class PageList<T>
    {
        /// <summary>
        /// 数据总量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        public PageList()
        {

        }

        public PageList(int total, int pageIndex, int pageSize, IEnumerable<T> items)
        {
            Items = items;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPage = Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1;
        }

        public PageList(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            if (data != null)
            {
                Total = data.Count();
                PageSize = pageSize;
                PageIndex = pageIndex;
                Items = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                TotalPage = Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1;
            }
        }
    }

}
