using System;
using System.Collections.Generic;
using DapperExtensions;

namespace Dapper_MySql
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "server=localhost;User Id=sa;password=123456;Database=datasync";
            MySqlAdapter mySqlClient = new MySqlAdapter(connStr);
            SyncLog synclog = new SyncLog();
            synclog.comId = "4555564";
            synclog.oType = 1;
            synclog.syncType = "平台方式";


            #region 新增

            mySqlClient.Add<SyncLog>(synclog);

            #region 批量新增

            #endregion
            SyncLog synclog1 = new SyncLog();
            synclog1.comId = "6666";
            SyncLog synclog2 = new SyncLog();
            synclog2.comId = "9999";
            List<SyncLog> syncLogsList = new List<SyncLog>();
            syncLogsList.Add(synclog1);
            syncLogsList.Add(synclog2);
            IEnumerable<SyncLog> syncLogsIEnumerable = syncLogsList;

            mySqlClient.Add(syncLogsIEnumerable);

            #endregion

            #region 修改

            //更新理论上来讲都是要传id值的
            SyncLog synclog3 = new SyncLog();
            synclog3.id = 55;
            synclog3.comId = "111111";
            synclog3.oType = 1;
            synclog3.syncType = "神奇";

            mySqlClient.Update<SyncLog>(synclog3);


            #region 批量修改

            SyncLog synclog4 = new SyncLog();
            synclog4.id = 56;
            synclog4.comId = "0123";
            SyncLog synclog5 = new SyncLog();
            synclog5.id = 57;
            synclog5.comId = "1023456789";
            List<SyncLog> syncLogsList1 = new List<SyncLog>();
            syncLogsList1.Add(synclog4);
            syncLogsList1.Add(synclog5);
            IEnumerable<SyncLog> syncLogsIEnumerable1 = syncLogsList1;

            mySqlClient.Update(syncLogsIEnumerable1);//批量修改功能似乎有点问题，识别不到映射的表名
            #endregion

            #endregion

            #region 查询

            var signleQuery = mySqlClient.Get<SyncLog>(4);


            var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<SyncLog>(f => f.comId, Operator.Eq, 90));
            IList<ISort> sorts = new List<ISort>();
            ISort sort = new Sort();
            sort.Ascending = false;
            sort.PropertyName = "id"; //如果有Map，则此次要填写Map对象的字段名称，而不是数据库表字段名称
            sorts.Add(sort);
            var syncLog2 = mySqlClient.Get<SyncLog>(pg, sorts);
            var syncLog3 = mySqlClient.Get<SyncLog>(pg, sorts, 0, 2);

            var pg4 = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            var syncLog4 = mySqlClient.Get<SyncLog>(pg4, sorts, 1, 10);
            //var syncLog5 = mySqlClient.Get<SyncLog>(pg4, sorts, 2, 10);
            //var syncLog6 = mySqlClient.Get<SyncLog>(pg4, sorts, 3, 10);

            #endregion

            #region 删除

            //理论上来讲删除应该要传id值
            SyncLog synclog6 = new SyncLog();
            synclog6.id = 55;

            mySqlClient.Delete(synclog6);

            #region 条件删除

            SyncLog synclog7=new SyncLog();
            synclog7.id = 52;

            synclog7.syncType = "平台方式";
            object object1 = new {synclog7};
            //mySqlClient.Delete(object1);//条件删除未知

            #endregion
            #endregion


            #region 查询的例子

            //MySqlAdapter dbClient = new MySqlAdapter(connStr);
            //var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            //pg.Predicates.Add(Predicates.Field<FlowCell>(f => f.status, Operator.Eq, 1));
            //pg.Predicates.Add(Predicates.Field<FlowCell>(f => f.del, Operator.Eq, 0));

            //var flowCell = dbClient.Get<FlowCell>(4);

            //IList<ISort> sorts = new List<ISort>();
            //ISort sort = new Sort();
            //sort.Ascending = false;
            //sort.PropertyName = "name"; //如果有Map，则此次要填写Map对象的字段名称，而不是数据库表字段名称
            //sorts.Add(sort);
            //var flowCell2 = dbClient.Get<FlowCell>(pg, sorts);

            //var flowCell3 = dbClient.Get<FlowCell>(pg, sorts, 0, 2);

            #endregion

            Console.ReadKey();
        }
    }
}
