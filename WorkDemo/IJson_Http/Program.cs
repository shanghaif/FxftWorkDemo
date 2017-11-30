using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IJson_Http
{
    /// <summary>
    /// Http+Json   Get请求   (Query)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region API接口传参  业务级参数  由具体服务API定义

            #region 停开机传参

            SortedDictionary<string, string> requestParams = new SortedDictionary<string, string>();//SortedDictionary默认升序排序
            requestParams.Add("iccid", "89860617030000452329");
            requestParams.Add("stateCode", "1");//0-停 1-开

            #endregion

            #endregion

            #region 调用接口

            HttpClient httpClient = new HttpClient();
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//数据交互方式JSon
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            var requestUri = UrlRequest(requestParams);
            httpClient.GetStringAsync(requestUri).ContinueWith((requestTask) =>
            {
                var result = requestTask.Result;

            }).Wait(30000);//调用接口，返回字符串结果。挂起操作(注意IP限制)

            #endregion

        }

        #region Url封装

        /// <summary>
        /// 适配Url封装  应用键和秘钥均由企业申请
        /// </summary>
        /// <param name="reqParams">业务级参数</param>
        /// <returns></returns>
        public static string UrlRequest(SortedDictionary<string, string> reqParams)
        {
            SortedDictionary<string, string> requestParams = new SortedDictionary<string, string>();//实现所有请求参数按参数名升序排序

            #region 系统级参数  是所有服务API都拥有的参数

            requestParams.Add("serverName", "Lao.base.cardHandle.stopOn");//服务方法名
            requestParams.Add("custId", "3071536592715439");//应用键(用户名) appId
            requestParams.Add("randomId", RandomId());//随机数

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
            sb.Append(@"http://gla.lenovomm.com//httpOpenServer/serviceProvide?");//接口地址
            foreach (var item in requestParams)
            {
                sbSign.Append($"{item.Key}{item.Value}");
                sb.Append($"{item.Key}={item.Value}&");
            }
            requestParams.Clear();
            requestParams = null;

            string secret = "A8vcEh39839DGok6Jf1ux30oKlw866";//密钥 appKey  
            var sign = GetMd5Hash16($"{secret}{sbSign}{secret}");//签名

            #region 签名算法

            //签名(sign)算法描述如下： 
            //1,所有请求参数按参数名升序排序； 
            //2, 按 请 求 参 数 名 及 参 数 值 相 互 连 接 组 成 一 个 字 符 串 ：
            //< paramName1 >< paramValue1 >< paramName2 >< paramValue2 >…之后并进行排序操作。
            //3,将应用密钥分别添加到以上请求参数串的头部和尾部1I4tz54D2968lvQTdh113F05o9y999custId3071136120059197dayDate2017 - 02iccid898602B5191651038187randomId12320170228170923457serverNameLao.base.monthFlow.query1I4tz54D2968lvQTdh113F05o9y999
            //4,对该字符串进行MD5 运算；
            // http://<serverUrl>/<ServletUri>?&serverName=Lao.base.surplusFlow.query &…&sign=8625FD7EEAE1E68203B48C64DE495792BF59E833

            #endregion

            return $"{sb.ToString()}sign={sign}";


        }
        #endregion


        #region MD5加密

        /// <summary>
        /// 对字符串进行MD5加密 16位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash16(String input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create();

            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // 返回十六进制字符串  
            return sBuilder.ToString();
        }

        #endregion


        #region 随机数

        /// <summary>
        /// 随机数【3位随机数+YYYYMMDDhhmmss+3位随机数】(不能重复)
        /// </summary>
        /// <returns>randomId</returns>
        public static string RandomId()
        {
            var timeStr = $"{DateTime.Now:yyyyMMddHHmmss}";
            Random ran = new Random();
            int firRandNum = ran.Next(100, 999);
            int secRandNum = ran.Next(100, 999);
            return $"{firRandNum}{timeStr}{secRandNum}";
        }

        #endregion



    }
}
