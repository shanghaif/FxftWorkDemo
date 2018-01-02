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
        public IEnumerable<VehicleItem> GetPage(int pageIndex, int pageSize)
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
        public void PostOne([FromBody]VehicleItem vehicleItem)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表

            #region 创建或者更新  批量 似乎没有什么参考意义，但不建议删除

            //List<WriteModel<BsonDocument>> requests = new List<WriteModel<BsonDocument>>();

            //var key = new Random(Environment.TickCount).Next(100000, 999999);
            //BsonDocument query = new BsonDocument("key", key);
            //var update = new BsonDocument() { { "$set", BsonDocumentWrapper.Create(new { key = key }) } };
            //requests.Add(new UpdateOneModel<BsonDocument>(query, update) { IsUpsert = true });//存在则更新，不存在则新增

            //var update0 = new BsonDocument() { { "$addToSet",new BsonDocument() { {"111", BsonDocumentWrapper.Create(BsonDocument.Parse(JsonConvert.SerializeObject(vehicleItem))) } } } };
            //requests.Add(new UpdateOneModel<BsonDocument>(query, update0) { IsUpsert = true });

            ////var update0 = new BsonDocument() { { "$addToSet", BsonDocumentWrapper.Create(BsonDocument.Parse(JsonConvert.SerializeObject(vehicleItem))) } };
            ////requests.Add(new UpdateOneModel<BsonDocument>(new BsonDocument(), update0) { IsUpsert = true });


            //if (requests.Count > 0)
            //{

            //    db.GetCollection<BsonDocument>("tblCrud").BulkWrite(requests);

            //}

            #endregion

            vehicle.InsertOne(vehicleItem);

        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="vehicleItem"></param>
        public void PostMany([FromBody]IEnumerable<VehicleItem> vehicleItem)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表
            vehicle.InsertMany(vehicleItem);

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicleItem"></param>
        public void Put(int id, [FromBody]VehicleItem vehicleItem)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            List<WriteModel<BsonDocument>> requests = new List<WriteModel<BsonDocument>>();
            BsonDocument query = new BsonDocument("citycode", id);
            //var update = new BsonDocument() { { "$set", BsonDocumentWrapper.Create(new { citycode = vehicleItem.citycode , speed = vehicleItem.speed}) } };
            var update = new BsonDocument() { { "$set", BsonDocumentWrapper.Create(vehicleItem) } };
            //requests.Add(new UpdateOneModel<BsonDocument>(query, update) { IsUpsert = true });//存在则更新，不存在则新增,更新单个符合查询条件的数据
            requests.Add(new UpdateManyModel<BsonDocument>(query, update) { IsUpsert = true });//存在则更新，不存在则新增，更新多个符合查询条件的数据
            if (requests.Count > 0)
            {
                db.GetCollection<BsonDocument>("tblCrud").BulkWrite(requests);
            }
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOne(int id)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表
            BsonDocument query = new BsonDocument("citycode", id);
            vehicle.DeleteOne(query);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMany(int id)
        {
            var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            mdSetting.ReadPreference = readPreference;
            var connectionString = ConfigurationManager.AppSettings["dbCon"];
            var database = ConfigurationManager.AppSettings["dbName"];
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            IMongoDatabase db = client.GetDatabase(database, mdSetting);//数据库
            var vehicle = db.GetCollection<VehicleItem>("tblCrud");//表
            BsonDocument query = new BsonDocument("citycode", id);
            vehicle.DeleteMany(query);
        }
    }
}
