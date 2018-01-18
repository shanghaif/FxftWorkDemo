using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 广东揭阳API3._0
{
    class Program
    {
       

        static void Main(string[] args)
        {

            #region  广东揭阳移动API

            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>();
            requestParams.Add("msisdn", "1064887860607");
            requestParams.Add("optType", "1" );//操作类型:1-停机 2-开机
            string url = UrlWrapper(config, requestParams);
            requestParams.Clear();
            requestParams = null;
            var secret = config.comConfig.apiKey.Substring(0, 24);//加密秘钥
            var res = Get(url, secret);
            try
            {
                if (res != null)
                {
                    var item = JsonConvert.DeserializeObject<Response>(res);
                    if (item != null && item.code.Equals("0"))
                    {
                        result.code = OutCode.成功;
                    }
                    else
                    {
                        result.code = OutCode.失败;
                        result.msg = item.error;
                    }

                }
                else
                {
                    result.code = OutCode.失败;
                    result.msg = "服务请求失败,请稍后重试";
                }
            }
            catch (Exception ex)
            {

                log.Debug("广东揭阳移动API:" + ex.Message);
            }

            #endregion
        }

        /// <summary>
        /// Url封装
        /// </summary>
        /// <param name="config">集团配置信息</param>
        /// <param name="reqParams">其他参数</param>
        /// <returns>Url</returns>
        public static string UrlWrapper(ConfigCache config, SortedDictionary<string, object> reqParams)
        {
            string secret = config.comConfig.apiKey;

            var time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //报文流水号，一共31位，构成：10位集团编码 + 17位到毫秒的时间戳 + 4位随机数字，例：2009999999201705161835221230001
            var transID = config.comConfig.comId.Substring(5, 10) + time + new Random().Next(1000, 9999);

            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>();

            #region 系统级参数

            requestParams.Add("appKey", config.comConfig.apiId);
            requestParams.Add("method", config.ebUrl);
            requestParams.Add("v", "3.0");
            requestParams.Add("format", "json");
            requestParams.Add("transID", transID);


            #endregion

            if (reqParams != null)
            {
                foreach (var item in reqParams)
                {
                    requestParams.Add(item.Key, item.Value);
                }
            }

            StringBuilder sbSign = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            sb.Append($"{config.comConfig.apiUrl}?");
            foreach (var item in requestParams)
            {
                sb.Append($"{item.Key}={item.Value}&");
                sbSign.Append($"{item.Key}{item.Value}");
            }

            requestParams.Clear();
            requestParams = null;
            var sign = UtilHelper.Sha1($"{secret}{sbSign.ToString()}{secret}");

            return $"{sb.ToString()}sign={sign}";
        }

        /// <summary>
        /// 广东揭阳3.0接口调用终极版本
        /// </summary>
        /// <param name="cNo"></param>
        /// <param name="requestUri"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string Get( string requestUri, string secret)
        {
            string result = null;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.GetStringAsync(requestUri).ContinueWith((requestTask) =>
                {
                    result = requestTask.Result;

                }).Wait(30000);

                result = Decrypt3Des(result, secret, CipherMode.ECB, null);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        #region 3des解密

        /// <summary>
        ///对字符串进行 des 解密
        /// </summary>
        /// <param name="aStrString">加密的字符串</param>
        /// <param name="aStrKey">密钥</param>
        /// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>解密的字符串</returns>
        public static string Decrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(aStrKey),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                }
                var desDecrypt = des.CreateDecryptor();
                var result = "";
                byte[] buffer = Convert.FromBase64String(aStrString);
                result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return result;
            }
            catch //(Exception e)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
