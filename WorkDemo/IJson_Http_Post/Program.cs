using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NRails.Util;

namespace IJson_Http_Post
{
    /// <summary>
    /// Http Post +Json 此接口调用无IP限制
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("key", "1064837284388");
            requestParams.Add("status", "1");
           Post( requestParams); //status 2 卡状态 

            //var key = "1064837284388";//卡号
            //var status = "1";
            //var partnercode = "0129";
            //var servicecode = "h5fugs";
            //var password = "0129h5fugs";
            //var requesttime = $"{DateTime.Now:yyyyMMddhhmmssms}" + new Random().Next(10000000, 99999999);
            //var signStr = password + requesttime + key + status;
            //var sign = NRails.Util.EncryptHelper.MD5Encrypt();
            //#region 调用接口 post  json

            ////var postCs = string.Format("\"partnercode\":\"{0}\"", partnercode);
            //var body = "{" +
            //    $"\"partnercode\":\"{partnercode}\",\"servicecode\":\"{servicecode}\",\"requesttime\":\"{requesttime}\",\"sign\":\"{sign}\",\"key\":\"{key}\",\"status\":\"{status}\"" + "}";

            //string urlrequest = @"http://api.m2m10086.com:89/M2MSearchTermInfo.ashx";//请求地址
            //HttpClient httpClient = new HttpClient();
            //HttpContent httpContent = new StringContent(body);
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            ////HttpResponseMessage response = httpClient.PostAsync(tmpUrl, httpContent).Result;
            //var task = httpClient.PostAsync(urlrequest, httpContent).Result;//可通过详细信息查看出来是否地址可以正常连接
            //if (task.StatusCode == HttpStatusCode.OK)
            //{
            //    var res = task.Content.ReadAsStringAsync().Result;
            //}

            //#endregion
        }


        public static string Post( Dictionary<string, string> requestParams)
        {
            string res = null;
            try
            {
                var rnd = new Random(Environment.TickCount);
            var requesttime = $"{DateTime.Now:yyyyMMddhhmmssms}{rnd.Next(10000000, 99999999)}";

            #region sign

            Dictionary<string, string> signParams = new Dictionary<string, string>();
                signParams.Add("password", "0129h5fugs");
                signParams.Add("requesttime", requesttime);

                foreach (var item in requestParams)
                {
                    signParams.Add(item.Key, item.Value);
                }
           var sign =EncryptHelper.MD5Encrypt(string.Join("", signParams.Values).ToLower(), true);
            signParams.Clear();
                signParams = null;

                #endregion

                #region Body

                requestParams.Add("password", "0129h5fugs");
            requestParams.Add("requesttime", requesttime);
            requestParams.Add("partnercode", "0129");
                requestParams.Add("servicecode", "h5fugs");
            requestParams.Add("sign", sign);

            var body = JsonConvert.SerializeObject(requestParams);
                requestParams.Clear();
                requestParams = null;

                #endregion


                string tmpUrl = @"http://api.m2m10086.com:89/M2MSearchTermInfo.ashx";//请求地址
                HttpClient httpClient = new HttpClient();
                HttpContent httpContent = new StringContent(body);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var task = httpClient.PostAsync(tmpUrl, httpContent).Result;

            if (task.StatusCode == HttpStatusCode.OK)
            {
                res = task.Content.ReadAsStringAsync().Result;
            }
            var result = JsonConvert.DeserializeObject<CardRealTimeInfo>(res);
            if (result.status != "0000")
            {
                //log.Error($"公田接口请求出错,请求路径:{tmpUrl},post参数:{body},返回结果{res}", "能力接口服务", "HttpHelper Post");
            }
            return res;
        }
            catch (Exception ex)
            {
                //log.Error($"调用公田接口发生异常{ex}");
                return res;
            }



}

    }
}
