using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi查询接口.Models
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public class NoticeInfoItem
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string transactionid { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string message { get; set; }

        public string type { get; set; }
    }
}