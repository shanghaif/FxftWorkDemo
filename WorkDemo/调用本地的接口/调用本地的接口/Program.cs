using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace 调用本地的接口
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpClient client = new HttpClient();

            var forms = new Dictionary<string, string>();
            forms.Add("eventId", "SIM_CUSTOM_FIELD_CHANGE-151888917");
            forms.Add("eventType", "SIM_CUSTOM_FIELD_CHANGE");
            forms.Add("timestamp", "2015-07-01T11:00:03.812Z");
            forms.Add("data", "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SimFieldChange xmlns=\"http://api.jasperwireless.com/ws/schema \"><iccid>8986061569000000161</iccid><oldValue>testForPush1</oldValue><newValue>testForPush2</newValue><fieldName>OPERATORCUSTOM1</fieldName></SimFieldChange>");

            var content = new FormUrlEncodedContent(forms);

      var qq=      client.PostAsync("http://localhost:8033/api/Jkfl/Post", content).Result;
            if (qq.StatusCode == HttpStatusCode.OK)
            {
                var res = qq.Content.ReadAsStringAsync().Result;
            }

            #region Post_Json 调用WebAPI原生版本

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
            var body = new object[] { body1 };
            bodys.Add("body", body);
            var requestParams = JsonConvert.SerializeObject(bodys);
            string requestUrl = "http://localhost:8033/api/FJTelNoticeInfo/Post";
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(requestParams);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = httpClient.PostAsync(requestUrl, httpContent).Result;

            if (task.StatusCode == HttpStatusCode.OK)
            {
                var res = task.Content.ReadAsStringAsync().Result;
            }
            #endregion


            #region Get

            var getTask = httpClient.GetStringAsync(requestUrl).Result;

            #endregion


        }
    }
}
