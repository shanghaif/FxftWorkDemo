using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using WebApiCRUD_Mongodb接口.Models;
using JsonConvert = Newtonsoft.Json.JsonConvert;

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
        public IEnumerable<VehicleItem> GetPage(int pageIndex,int pageSize)
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

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="cNo"></param>
        /// <returns></returns>
        public VehicleItem GetOne(int citycode)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表
            BsonArray querys = new BsonArray();
            querys.Add(new BsonDocument("citycode", citycode));
            querys.Add(new BsonDocument("lng", citycode));
            BsonDocument query = new BsonDocument("$or", querys);
            var pageData = vehicle.Find(query).FirstOrDefault();
            return pageData;
        }
     

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vehicleItem"></param>
        public void Post([FromBody]VehicleItem vehicleItem)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表

            #region 创建或者更新

            List<WriteModel<BsonDocument>> requests = new List<WriteModel<BsonDocument>>();
            
            var update0 = new BsonDocument() { { "$set", BsonDocumentWrapper.Create(BsonDocument.Parse(JsonConvert.SerializeObject(vehicleItem))) } };
            requests.Add(new UpdateOneModel<BsonDocument>(new BsonDocument(), update0) { IsUpsert = true });

            //var updateSet = new BsonDocument() { { "$addToSet", new BsonDocument() { { "wxInfo", BsonDocumentWrapper.Create(vehicleItem) } } } };
            //BsonDocument query = new BsonDocument();
            //requests.Add(new UpdateOneModel<BsonDocument>(query, updateSet) { IsUpsert = true });

            if (requests.Count > 0)
            {
          db.GetCollection<BsonDocument>("tblCrud").BulkWrite(requests);
                
            }
            #endregion
          
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
