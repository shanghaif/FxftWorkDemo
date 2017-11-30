using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelSDK;

namespace 上海电信接口Demo
{
    /// <summary>
    ///Http Post+Json  有IP限制  仅供参考
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, object> requestParams = new Dictionary<string, object>();
            requestParams.Add("service_name", "M2MPakage");
            dynamic parameter = new
            {
                callNbr = "14916205446",
            };
            requestParams.Add("service_parameter", parameter);
            var res = Post(requestParams);


            #region 写入文件

            var file = @"D:\Test\test.txt";
            FileInfo fileInfo = new FileInfo(file);
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
            File.WriteAllText(file, res);
            Console.ReadKey();

            #endregion


        }

        static string Post(Dictionary<string, object> requestParams)
        {
            Dictionary<string, object> body = new Dictionary<string, object>();
            var msgId = Guid.NewGuid().ToString().Replace("-", "");
            body.Add("msgId", msgId);
            body.Add("version_id", "1.5");//
            body.Add("consumer", "saige");//用户名
            body.Add("password", "123456");//
            body.Add("test_flag", "1");//0 生产数据
            body.Add("reqTime", DateTime.Now.ToString());
            foreach (var item in requestParams)
            {
                body.Add(item.Key, item.Value);
            }

            string requestUrl = $"http://101.95.48.192:8080/tsp-api/TspService";
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(body));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = httpClient.PostAsync(requestUrl, httpContent).Result;
            string res = null;
            if (task.StatusCode == HttpStatusCode.OK)
            {
                res = task.Content.ReadAsStringAsync().Result;
            }
            Console.WriteLine(res);
          
            return res;
        }

    }
}
