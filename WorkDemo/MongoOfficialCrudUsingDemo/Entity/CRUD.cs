using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoOfficialCrudUsingDemo.Entity
{
    [BsonIgnoreExtraElements]
    public class CRUD
    {
        public string name { get; set; }
        public string type { get; set; }
        public string count { get; set; }
        //public object info { get; set; }
    }
}
