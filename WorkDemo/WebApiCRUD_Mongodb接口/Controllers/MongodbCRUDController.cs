using System;
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
    public class MongodbCRUDController : ApiController
    {
       /// <summary>
       /// 分页查询
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        public IEnumerable<VehicleItem> Get(int pageIndex,int pageSize)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表
            var query = new BsonDocument();
            var sort = new BsonDocument("DTime", -1);
            //分页需要进一步优化
            var pageData = vehicle.Find(query).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            return pageData;
        }

        // GET: api/MongodbCRUD/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MongodbCRUD
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/MongodbCRUD/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MongodbCRUD/5
        public void Delete(int id)
        {
        }
    }
}
