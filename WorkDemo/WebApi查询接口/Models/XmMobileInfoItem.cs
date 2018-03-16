using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApi查询接口.Models
{
    /// <summary>
    /// 小米移动消息通知接口传参
    /// </summary>
    public class XmMobileInfoItem
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int merchant_id { get; set; }
        /// <summary>
        /// 日志ID
        /// </summary>
        public string log_id { get; set; }
        /// <summary>
        /// 请求的数据类型
        /// </summary>
        public int notification_type { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string phone_number { get; set; }

        #region 可选参数
       
        public int int_value { get; set; }
        public string string_value { get; set; }
        public ExtraData extra_data { get; set; }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ExtraData
    {
        
    }
}