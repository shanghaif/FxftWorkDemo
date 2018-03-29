using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Dapper_MySql;
using WebApiCRUD_Mongodb接口.Formatter;
using WebApiCRUD_Mongodb接口.Models;

namespace WebApiCRUD_Mongodb接口.Controllers
{
    public class JxcFlowCellController : ApiController
    {


   
        [HttpGet]
        public JxcFlowCellItem Get(ushort eId, string cellNumber)
        {
            var flowCell = new JxcFlowCellItem();

            var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<JxcFlowCellItem>(f => f.cell_number, Operator.Eq, cellNumber));
            IList<ISort> sorts = new List<ISort>();
            ISort sort = new Sort();
            sort.Ascending = false;
            sort.PropertyName = "cell_number"; //如果有Map，则此次要填写Map对象的字段名称，而不是数据库表字段名称
            sorts.Add(sort); 
            string connStr = "Server=rm-bp1gut3b938v3tms1.mysql.rds.aliyuncs.com;Port=3306;initial catalog=erp;uid=jijiuser;pwd=Fxft2016;Allow User Variables=True";
            MySqlAdapter mySqlClient = new MySqlAdapter(connStr);
             flowCell = mySqlClient.Get<JxcFlowCellItem>(pg, sorts).FirstOrDefault();

            return flowCell;
        }
      

        // POST: api/JxcFlowCell
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/JxcFlowCell/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/JxcFlowCell/5
        public void Delete(int id)
        {
        }
    }
}
