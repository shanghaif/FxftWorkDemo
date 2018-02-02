using MongoDB.Bson;
using MongoDB.Driver;
using MongoOfficialCrudUsingDemo.Entity;

namespace MongoOfficialCrudUsingDemo
{
    /// <summary>
    /// 此demo失败，未完成
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {

            //var readPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            //MongoDatabaseSettings mdSetting = new MongoDatabaseSettings();
            //mdSetting.ReadPreference = readPreference;
            var connectionString = "mongodb://127.0.0.1:27017";
            MongoClient client = new MongoClient(connectionString);//连接mogodb数据库
            var database = client.GetDatabase("Test");//数据库
            var collection = database.GetCollection<CRUD>("tblCrud");//表

            var document = new BsonDocument
            {
                {"name", "MongoDB"},
                {"type", "Database"},
                {"count", 1}
            };
   

            //collection.InsertOne(document);
        }

    }
}
