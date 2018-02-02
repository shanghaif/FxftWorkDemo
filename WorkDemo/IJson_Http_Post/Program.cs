using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            #region 基础版本   Main方法里面就是完整的接口调用了

            var key = "1064837284388";//卡号
            var status = "1";
            var partnercode = "0129";
            var servicecode = "h5fugs";
            var password = "0129h5fugs";
            var requesttime = $"{DateTime.Now:yyyyMMddhhmmssms}" + new Random().Next(10000000, 99999999);
            var signStr = password + requesttime + key + status;
            var sign = EncryptHelper.MD5Encrypt(signStr.ToLower());

            #region 调用接口 post  json

            var body = "{" +
                $"\"partnercode\":\"{partnercode}\",\"servicecode\":\"{servicecode}\",\"requesttime\":\"{requesttime}\",\"sign\":\"{sign}\",\"key\":\"{key}\",\"status\":\"{status}\"" + "}";

            string urlrequest = @"http://api.m2m10086.com:89/M2MSearchTermInfo.ashx";//请求地址
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //HttpResponseMessage response = httpClient.PostAsync(tmpUrl, httpContent).Result;
            var task = httpClient.PostAsync(urlrequest, httpContent).Result;//可通过详细信息查看出来是否地址可以正常连接
            if (task.StatusCode == HttpStatusCode.OK)
            {
                var res = task.Content.ReadAsStringAsync().Result;
            }

            #endregion

            #endregion

            #region 常规版本

            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("key", "1064837284388");
            requestParams.Add("status", "1");
            Post(requestParams);

            #endregion
        }


        public static string Post(Dictionary<string, string> requestParams)
        {
            string res = null;
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
            var sign = EncryptHelper.MD5Encrypt(string.Join("", signParams.Values).ToLower(), true);
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

            string urlRequest = @"http://api.m2m10086.com:89/M2MSearchTermInfo.ashx";//请求地址
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = httpClient.PostAsync(urlRequest, httpContent).Result;

            if (task.StatusCode == HttpStatusCode.OK)
            {
                res = task.Content.ReadAsStringAsync().Result;
            }
            return res;

        }

    }
}
