using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApi查询接口.Models
{
    /// <summary>
    /// 福建电信消息通知
    /// </summary>
    public class FJTelNoticeInfoItem
    {
        public Head head { get; set; }
        public Body[] body { get; set; }

        public override string ToString()
        {
            FJTelNoticeInfoItem item = new FJTelNoticeInfoItem();
            item.body = body;
            item.head = head;
            //var headInfo = JsonConvert.SerializeObject(head);
            //var bodyInfo = JsonConvert.SerializeObject(body);
            var res = JsonConvert.SerializeObject(item);
            return res;
        }


    }


    public class Body
    {
        /// <summary>
        /// 唯一流水号
        /// </summary>
        public string group_transactionid { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mdn { get; set; }
        /// <summary>
        /// 结果码,成功为0
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string result_msg { get; set; }
    }
    public class Head
    {
        /// <summary>
        /// 推送流水号
        /// </summary>
        public string reqSerial { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public string reqTime { get; set; }
        /// <summary>
        /// 提醒类型码
        /// </summary>
        public string notifyType { get; set; }
    }
}