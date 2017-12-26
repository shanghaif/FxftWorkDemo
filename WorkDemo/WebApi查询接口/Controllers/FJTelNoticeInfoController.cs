using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi查询接口.Models;

namespace WebApi查询接口.Controllers
{
    public class FJTelNoticeInfoController : ApiController
    {


        public string Get()
        {
            return "成功测试get";
        }



        /// <summary>
        /// 福建电信消息通知接口
        /// </summary>
        /// <param name="fjNInfo">请求体</param>
        [HttpPost]
        public string Post([FromBody] FJTelNoticeInfoItem fjNInfo)
        {
            if (fjNInfo.body[0].result_code == "0")
            {
                var returnMessage = new Dictionary<string, object>();
                var head = new
                {
                    respSerial = fjNInfo.head.reqSerial,
                    respTime = $"{DateTime.Now:yyyyMMddHHmmss}",
                    result = "00",
                    resultDesc = "推送成功"
                };
                object[] body = { };
                returnMessage.Add("head", head);
                returnMessage.Add("body", body);
                var bodyContent = JsonConvert.SerializeObject(returnMessage);
                return bodyContent;

            }
            else
            {
                var returnMessage = new Dictionary<string, object>();
                var head = new
                {
                    respSerial = fjNInfo.head.reqSerial,
                    respTime = $"{DateTime.Now:yyyyMMddHHmmss}",
                    result = "02",//失败
                    resultDesc = "推送失败"
                };
                object[] body = { };
                returnMessage.Add("head", head);
                returnMessage.Add("body", body);
                var bodyContent = JsonConvert.SerializeObject(returnMessage);
                return bodyContent;
            }

           
        }
    }
}
