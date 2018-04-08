using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;
using NLog;
using WebApi查询接口.Models.机卡分离;

namespace WebApi查询接口.Controllers
{
    /// <summary>
    /// 机卡分离
    /// </summary>
    public class JkflController : ApiController
    {
        ///// <param name="eventId">事件id</param>
        ///// <param name="eventType">事件类型</param>
        ///// <param name="timestamp">时间</param>
        ///// <param name="signature">签名</param>
        ///// <param name="data">数据</param>
        ///// <returns></returns>
        //public string CtdUsagePushAPI(string eventId, string eventType, string timestamp, string signature, string data)
        //{
        //    data = data.Replace("http://api.jasperwireless.com/ws/schema", "");
        //    data = data.Replace("Past24HDataUsage", "CtdUsage");
        //    CtdUsage obj = Common.XmlDeserialize<CtdUsage>(data);
        //    return "test";
        //}

        // POST: api/Jkfl
        public void Post([FromBody]PushItem value)
        {
            var qq = ToString();
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Debug($"{qq}{value}");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(value.data);

        }

   
    }

    public class PushItem
    {
        public  string eventId { get; set; }
        public string eventType { get; set; }
        public string timestamp { get; set; }
        public string signature { get; set; }
        public string data { get; set; }
        //public override string ToString()
        //{
        //    //return JsonConvert.SerializeObject(this);
        //    return JsonConvert.SerializeObject(this);
        //}
    }
}
