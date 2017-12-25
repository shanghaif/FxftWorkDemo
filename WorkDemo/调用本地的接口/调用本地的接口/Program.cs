using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace 调用本地的接口
{
    class Program
    {
        static void Main(string[] args)
        {
            string res = null;
            var rnd = new Random(Environment.TickCount);
            Dictionary<string, object> bodys = new Dictionary<string, object>();
            dynamic head = new
            {
                reqSerial = $"{DateTime.Now:yyyyMMddhhmmss}" + rnd.Next(1000, 9999),//4位还是6位随机数？
                reqTime = $"{DateTime.Now:yyyyMMddhhmmss}",
                notifyType = "9027"
              
            };
            bodys.Add("head", head);
            dynamic body1 = new
            {
                group_transactionid = "20150817103800100000",
                mdn = "888",
                result_code = "00",
                result_msg = "调用测试"
            };
            var body=new object[] { body1 };
            bodys.Add("body", body);
            var requestParams = JsonConvert.SerializeObject(bodys);
            string requestUrl = "http://localhost:8033/api/FJTelNoticeInfo/Post";

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(requestParams);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = httpClient.PostAsync(requestUrl, httpContent).Result;
           

            if (task.StatusCode == HttpStatusCode.OK)
            {
                res = task.Content.ReadAsStringAsync().Result;
            }

           
        }
    }
}
