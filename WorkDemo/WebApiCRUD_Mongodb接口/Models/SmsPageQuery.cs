using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 1.1.15	短信记录信息查询结果集
    /// </summary>
    public class SmsPageQuery
    {
        /// <summary>
        /// 短信记录总数
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// 短信记录分页索引，从0起始
        /// </summary>
        public int pageIndex { get; set; }

        public SmsInfo[] SmsInfos { get; set; }
    }
}