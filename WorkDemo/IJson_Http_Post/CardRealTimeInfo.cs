using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJson_Http_Post
{
    public class CardRealTimeInfo
    {
        public string status { get; set; }
        public string message { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardcode { get; set; }
        /// <summary>
        /// 卡序列号
        /// </summary>
        public string iccid { get; set; }
        /// <summary>
        /// IMSI
        /// </summary>
        public string imsi { get; set; }
        /// <summary>
        /// 当月使用流量 单位：KB
        /// </summary>
        public string flow { get; set; }
        /// <summary>
        /// 卡状态
        /// </summary>
        public string cardstatus { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string macstatus { get; set; }
        /// <summary>
        /// GPRS状态
        /// </summary>
        public string gprsstatus { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 接入点名称
        /// </summary>
        public string apn { get; set; }
        /// <summary>
        /// 网络类型
        /// </summary>
        public string rat { get; set; }
    }
}
