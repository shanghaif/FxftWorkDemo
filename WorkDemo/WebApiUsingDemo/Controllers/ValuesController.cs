using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiUsingDemo.Models;

namespace WebApiUsingDemo.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        public static List<TestUseMode> allModeList = new List<TestUseMode>();


        public IEnumerable<TestUseMode> GetAll()
        {
            TestUseMode testMode=new TestUseMode();
            testMode.ModeKey = "1";
            return allModeList;
        }

        //public IEnumerable<TestUseMode> GetOne(string key)
        //{
        //    return allModeList.FindAll((mode) => { if (mode.ModeKey.Equals(key)) return true; return false; });
        //}

        public bool PostNew(TestUseMode mode)
        {
            allModeList.Add(mode);
            return true;
        }

        public int Delete(string key)
        {
            return allModeList.RemoveAll((mode) => { if (mode.ModeKey == key) return true; return false; });
        }

        public int DeleteAll()
        {
            return allModeList.RemoveAll((mode) => { return true; });
        }


        public int PutOne(string key, string value)
        {
            List<TestUseMode> upDataList = allModeList.FindAll((mode) => { if (mode.ModeKey == key) return true; return false; });
            foreach (var mode in upDataList)
            {
                mode.ModeValue = value;
            }
            return upDataList.Count;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
