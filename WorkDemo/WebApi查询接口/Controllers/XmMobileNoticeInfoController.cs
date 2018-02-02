using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi查询接口.Models;

namespace WebApi查询接口.Controllers
{
    /// <summary>
    /// 小米移动消息通知接口
    /// </summary>
    public class XmMobileNoticeInfoController : ApiController
    {
        [HttpPost]
        public string notify([FromBody] XmMobileInfoItem xmMobileInfo)
        {
            var xmResponse = new XmResponse();
            if (xmMobileInfo.merchant_id == 100016 && xmMobileInfo.notification_type == 0x0004 && !String.IsNullOrWhiteSpace(xmMobileInfo.phone_number) && !String.IsNullOrWhiteSpace(xmMobileInfo.log_id))
            {
                xmResponse.rtnCode = 0;
                xmResponse.log_id = xmMobileInfo.log_id;
                xmResponse.rtnMsg = "成功接收通知";
                //日志
                var log = xmMobileInfo.ToString();
                return JsonConvert.SerializeObject(xmResponse);
            }

            xmResponse.rtnCode = -1;
            xmResponse.log_id = xmMobileInfo.log_id;
            xmResponse.rtnMsg = "失败";
            //日志
            return JsonConvert.SerializeObject(xmResponse);
        }

    }
}
