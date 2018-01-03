using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信记录信息查询接口传参
    /// </summary>
    public class SmsQuery
    {
        public string cNo { get; set; }
        /// <summary>
        /// 短信记录分页索引，从0起始
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 短信记录分页大小,最大是1000
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 创建时间/接收时间/发送时间  0=升序  1=降序
        /// </summary>
        public byte sort { get; set; }
    }
}