using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelSDK;

namespace IForm_Http_Post
{
    /// <summary>
    ///post多段传参  此demo刚好只有一段  无IP限制
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var cmdType = "9028";
            dynamic body = new
            {
                phonenum = "1064927583245"
            };
            object[] objBody = { body };
        var res=    Post(cmdType, objBody);
            var resJson = JsonConvert.DeserializeObject<FjTelComRescs>(res);
          
        }

        public static string Post(string cmdtype, object[] body)
        {
            string res = null;
            var rnd = new Random(Environment.TickCount);
            Dictionary<string, object> bodys = new Dictionary<string, object>();
            dynamic head = new
            {
                reqSerial = $"{DateTime.Now:yyyyMMddhhmmss}" + rnd.Next(1000, 9999),//4位还是6位随机数？
                reqTime = $"{DateTime.Now:yyyyMMddhhmmss}",
                cmdType = cmdtype,
                PID = "tsp3000",
                PToken = "test1234",
                org_code = "FXFTWLKJYXGS"
            };

            bodys.Add("head", head);
            bodys.Add("body", body);
            var reqData = JsonConvert.SerializeObject(bodys);
            var requestParams = new Dictionary<string, string>();
            requestParams.Add("reqData", reqData);
            string requestUrl = "http://59.56.74.52:9009/boss/bossController.do";

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new FormUrlEncodedContent(requestParams);
            var task = httpClient.PostAsync(requestUrl, httpContent).Result;

            if (task.StatusCode == HttpStatusCode.OK)
            {
                res = task.Content.ReadAsStringAsync().Result;
            }

            return res;
        }
    }
}
