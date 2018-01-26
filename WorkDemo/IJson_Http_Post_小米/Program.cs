using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace IJson_Http_Post_小米
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 翻译测试

            var aesTest = new AESTest();
            var rsaTest = new RSATest();
            RSAExtensionsTest();
            #endregion


            #region 这个部分放在数据适配层

            var merchantId = "100016";
            var logId = getLogId(merchantId);
            var data = new Dictionary<string, object>();
            data.Add("log_id", logId);
            data.Add("phone_number_type", 12);
            data.Add("phone_number", "1064776352047");
            var body = JsonConvert.SerializeObject(data);

            #endregion



            #region 生成requestData

            string clientPrivateKey = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKtSBfAfb35umUeTgaE0+eh8OunKBVR4k0/RrT6lukCFeB0F/8s7/Ku5n9z5Nv4Ebe41dRvAl+2WmKklAz3jePoBQ41qXKAXZI78a/ZIIKI0iFrIBwqpN5D488n2/wESaRhEsYe6Zw3u4/KFY0AVk/HlYDvmS2DtzZm6JcQwvfZnAgMBAAECgYEAgPbPzYB8d5pd/EmHzYiJj37lAlS3Sm0xx2Y5me07lZJjZsW7Vowjmkmzk65uvS0sa6MGMwv50joJVVqtZAs2Zw/x04fk8M5duW8Kr7uL90OEO5HEvg2bjuNAN9DSVGy1JJBDk2/+M3ubSx7N82TW37Rf8lgqnZxObS4cN8mkmyECQQDjJP1zDhsHcUqU77tibHEp21B+Nk6GN+1oC8qq6qrxrNzE4I9KM2SFfGkxYdh91xpr1jKcSRo7fMqM4uejR8VpAkEAwRWRMS8HUBMb0kQu/gLisvYw1UbJzZPLzFuAYv5r6AqM5h0l9aBVw55RwzaPm2upCTpL6rNXJtJSbXbOHf9TTwJBAJNBbMpFT8KQcNCDZpDVSrvfAZ0BKgEbit6UHmyVvAL3lRxRlLN/A+ECGdQ44bgbVnaoo6DsR4RfT5TsmU0if1kCQETYs9SduDXNGnZ26WqZDMxTDEZ/3yT2Ngy/859YqJEsceD7M7XJXctKgEzi/4GjebpYlwkwuTqWc92kJwp7J/ECQAlG5iNQUB8I8fxqsxq/9Zgr2vtN4NN0+pOQe7CZwlWhqjT9Q2dwcWXjV7lXAsTvs3WCWfSklRV39qUmAL28z/M=";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            var priKey = rsa.ConvertToXmlPrivateKey(clientPrivateKey);
            Debug.WriteLine("私钥:" + priKey);

            string serverPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCNMzyuOyRTkgtQpsNC64QT2I3GlNM+fzqiVzUUCqjYJdJJGv1Bzb4CK/SnlQVowY6LFzLkKW0G2rfChqbP3EDb6MUEbYJTQG+mSjuZwZtU44NwN2biT8SYMDkgjOuPThuSMzzLhzF+M8g5sTjgc7y96kygUFFFWDbHkaNr30582wIDAQAB";

            var pubKey = rsa.ConvertToXmlPublicJavaKey(serverPublicKey);
            Debug.WriteLine("公钥:" + pubKey);


            var responseData = new Dictionary<string, object>();
            var aesKey = "h2hK6QaQ7hl92zNnusRhdQ==";
            var dataEncryptResult = AESHelper. Encrypt(body, aesKey);
            responseData.Add("data", dataEncryptResult);

            responseData.Add("merchant_id", merchantId);

            var passEncryptResult = RSAHelper.Encrypt(aesKey, pubKey);
            responseData.Add("pass", passEncryptResult);

            StringBuilder sbSign = new StringBuilder();
            foreach (var item in responseData)
            {
                sbSign.Append($"{item.Key}={item.Value}&");
            }
            sbSign.Remove(sbSign.Length - 1, 1);
            var sign = sbSign.ToString();
            var signEncryptResult = RSAHelper.Sign(sign, priKey);
            responseData.Add("sign", signEncryptResult);

            var requestData = JsonConvert.SerializeObject(responseData);

            #endregion

            var postBody = new Dictionary<string, object>();
            postBody.Add("merchant_id", merchantId);
            postBody.Add("requestData", requestData);
            var urlRequest = "http://preview.exapi.10046.mi.com/v1/query";

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(postBody));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = httpClient.PostAsync(urlRequest, httpContent).Result;
            string res = null;
            if (task.StatusCode == HttpStatusCode.OK)
            {

                res = task.Content.ReadAsStringAsync().Result;
            }

            Console.Read();




        }

        /// <summary>
        /// 把java的私钥转换成.net的xml格式
        /// </summary>
        static void RSAExtensionsTest()
        {
            string privateKey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAK2g2PawCaVp4B4ZYkyMjrJFLMGX1FVOIdwCs8P1KRNyTmRMgyZBWgS5j1htGIZH2kgM008q8NWBLCG/IVXcjqfzQnsZkIq/9aOTxB/p41ywsVOrPeHCkUY14bFMnAVC/hSU3xXe/hv1TVLyJIgc534yMXJe4AwhNiRXHBTXjgXlAgMBAAECgYAkWJJyeYFbbiFrS5cHPTk12kM3N2hMfzGZU7t73ts9ZeVBG78sh+unJ4z5TsUefvYTtROa8/s8RnENk2wQBD6cU0iEHtZDrDXi+xhWzuvJrBQAlrisg8UqlGqh3M/qIgmN76m9Y5TNqZiuwU0m4YU34r6ybxLZjNTHdlSdjymYQQJBAOPPJGayiGADXpcAX4Z/7aatJcoCS7tVCnNRXvXw21aXjUv0E7NPQSSifocrQaC1nAHZQYCIMJK87iEPmqLnRvUCQQDDHUno6zYS0CesJXogpfJMP2T4VEmuTnU9ysdsX+hyDRJbCwwSv9S+OXsXbvsVzeAsmgCUf9D2pcRYHvP8kw0xAkAtwxWv35tPlj2xHa0Syq4FtnCJ5O4rooin46esxgZ5nZYPtOckNP6ECnRaWNElJHZDVM44sKL5RySa1ZSdttHlAkBi4Wv8g39i52TDpDaRZyg1JgeIpka09IQMLcEFFlHZwvAjfaS/t8IcKUtNY+wRb8WFNQLIP0JqTTYFJqKlxOxxAkAc8hYSYLvQseu+M9QW5SoHcp332PVX/6/bGwg3HcfxGrT7gMErpol65U24RGYn1aLd8OUwacT3tnuFvfO0M+55";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            var priKey = rsa.ConvertToXmlPrivateKey(privateKey);
            Debug.WriteLine("私钥:" + priKey);

            string publickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCtoNj2sAmlaeAeGWJMjI6yRSzBl9RVTiHcArPD9SkTck5kTIMmQVoEuY9YbRiGR9pIDNNPKvDVgSwhvyFV3I6n80J7GZCKv/Wjk8Qf6eNcsLFTqz3hwpFGNeGxTJwFQv4UlN8V3v4b9U1S8iSIHOd+MjFyXuAMITYkVxwU144F5QIDAQAB";

            var pubKey = rsa.ConvertToXmlPublicJavaKey(publickey);
            Debug.WriteLine("公钥:" + pubKey);
            Console.Read();
        }

     

     

        /// <summary>
        /// 日志ID log_id
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public static String getLogId(string merchantId)
        {
            // String code = UUID.randomUUID().toString();
            string code = System.Guid.NewGuid().ToString();
            // 一句话即可，但此时id中有“-”符号存在，使用下面语句可变为纯字母 + 数字。
            //string code = System.Guid.NewGuid().ToString("N");

            if (string.IsNullOrEmpty(merchantId))
            {
                return code;
            }

            //SimpleDateFormat formatDate = new SimpleDateFormat("yyyyMMddHHmmss");
            //Date d = new Date(System.currentTimeMillis());
            //String date = formatDate.format(d);
            string date = $"{DateTime.Now:yyyyMMddHHmmss}";

            return merchantId + "-" + date + code.GetHashCode();
        }
    }
}
