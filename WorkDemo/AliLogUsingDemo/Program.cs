using System;
using System.Collections.Generic;
using Aliyun.Api.SLS;
using Aliyun.Api.SLS.Data;
using Aliyun.Api.SLS.Request;
using Aliyun.Api.SLS.Response;

namespace AliLogUsingDemo
{
    /// <summary>
    /// 使用教程查看官网
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            String endpoint = "cn-hangzhou.log.aliyuncs.com";//选择与上面步骤创建 project 所属区域匹配的日志服务 Endpoint
            String accessKeyId = "yacNT1obW34STZCL"; //使用你的阿里云访问秘钥 AccessKeyId
            String accessKeySecret = "K4JY48aALclGK8iZ082B4MPLLfy2BN"; //使用你的阿里云访问秘钥 AccessKeySecret
            String project = "chekongbao";     //上面步骤创建的项目名称
            String logstore = "ckb_log";   //上面步骤创建的日志库名称

            //构建一个客户端实例
            SLSClient client = new SLSClient(endpoint, accessKeyId, accessKeySecret);
            //列出当前 project 下的所有日志库名称
            ListLogstoresResponse res1 = client.ListLogstores(new ListLogstoresRequest(project));
            Console.WriteLine("Totoal logstore number is " + res1.Count);
            foreach (String name in res1.Logstores)
            {
                Console.WriteLine(name);
            }


            DateTime unixTimestampZeroPoint = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            //写入日志
            List<LogItem> logs = new List<LogItem>();
            for (int i = 0; i < 5; i++)
            {
                LogItem item = new LogItem();
                item.Time = (uint)((DateTime.UtcNow - unixTimestampZeroPoint).TotalSeconds);
                item.PushBack("index", i.ToString());
                logs.Add(item);
            }

            String topic = String.Empty;    //选择合适的日志主题
            String source = "localhost";    //选择合适的日志来源（如 IP 地址）
            PutLogsResponse res4 = client.PutLogs(new PutLogsRequest(project, logstore, topic, source, logs));
            Console.WriteLine("Request ID for PutLogs: " + res4.GetRequestId());
            //等待 1 分钟让日志可查询
            System.Threading.Thread.Sleep(60 * 1000);
            //查询日志分布情况
            DateTime fromStamp = DateTime.UtcNow - new TimeSpan(0, 10, 0);
            DateTime toStamp = DateTime.UtcNow;
            uint from = (uint)((fromStamp - unixTimestampZeroPoint).TotalSeconds);
            uint to = (uint)((toStamp - unixTimestampZeroPoint).TotalSeconds);
            GetHistogramsResponse res2 = null;
            do
            {
                res2 = client.GetHistograms(new GetHistogramsRequest(project, logstore, from, to));
            } while ((res2 != null) && (!res2.IsCompleted()));
            Console.WriteLine("Total count of logs is " + res2.TotalCount);
            foreach (Histogram ht in res2.Histograms)
            {
                Console.WriteLine(String.Format("from {0}, to {1}, count {2}.", ht.From, ht.To, ht.Count));
            }
            //查询日志数据
            GetLogsResponse res3 = null;
            do
            {
                res3 = client.GetLogs(new GetLogsRequest(project, logstore, from, to, String.Empty, String.Empty, 5, 0, true));
            } while ((res3 != null) && (!res3.IsCompleted()));
            Console.WriteLine("Have gotten logs...");
            foreach (QueriedLog log in res3.Logs)
            {
                Console.WriteLine("----{0}, {1}---", log.Time, log.Source);
                for (int i = 0; i < log.Contents.Count; i++)
                {
                    Console.WriteLine("{0} --> {1}", log.Contents[i].Key, log.Contents[i].Value);
                }
            }
            Console.ReadKey();
        }
    }
}
