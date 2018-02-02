using System.Collections.Generic;

namespace Dapper_MySql
{
    /// <summary>
	/// 分页信息
	/// </summary>
	public class PageInfo<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public PageInfo()
        {

        }
        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Data
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public PageInfo(long total, IEnumerable<T> data)
        {
            this.TotalCount = total;
            this.Data = data;
        }
    }
}
