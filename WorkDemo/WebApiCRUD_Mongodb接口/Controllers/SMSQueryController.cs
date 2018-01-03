using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiCRUD_Mongodb接口.Models;

namespace WebApiCRUD_Mongodb接口.Controllers
{
    /// <summary>
    /// 短信记录信息查询接口
    /// </summary>
    public class SMSQueryController : ApiController
    {



        /// <summary>
        /// 短信记录分页索引，从0起始
        /// 短信记录分页大小,最大是1000
        /// </summary>
        /// <param name="sms"></param>
        public SmsPageQuery Post([FromBody]SmsQuery sms)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var smsCollection = db.GetCollection<SmsInfo>("tblSMS");//表
            var query = new BsonDocument("cNo", sms.cNo);
            var sort = sms.sort == 0 ? new BsonDocument("oDate", 1) : new BsonDocument("oDate", -1);
            if (sms.pageSize > 1000)
            {
                sms.pageSize = 1000;
            }
            if (sms.pageIndex < 0)
            {
                sms.pageIndex = 0;
            }
            //分页需要进一步优化
            var pageData = smsCollection.Find(query).Sort(sort).Skip(sms.pageSize * sms.pageIndex).Limit(sms.pageSize).ToList().ToArray();
            SmsPageQuery smsPageQuery = new SmsPageQuery();
            smsPageQuery.pageIndex = sms.pageIndex;
            smsPageQuery.total = smsCollection.CountAsync(new BsonDocument()).Result;
            smsPageQuery.SmsInfos = pageData;
            return smsPageQuery;
        }

      
    }
}
