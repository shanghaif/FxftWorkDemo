using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi查询接口.Models
{
    /// <summary>
    /// 小米移动消息通知接口返回值
    /// </summary>
    public class XmResponse
    {
       
        public string rtnMsg { get; set; }
        /// <summary>
        ///0  成功接收通知，-1  失败 
        /// </summary>
        public int rtnCode { get; set; }

        public string log_id { get; set; }
    }
}