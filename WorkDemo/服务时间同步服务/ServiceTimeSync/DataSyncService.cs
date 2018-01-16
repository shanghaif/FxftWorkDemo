using FluentScheduler;
using NRails.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using NLog;

namespace ServiceTimeSync
{
    public class DataSyncService : IDisposable
    {
        #region 参数

        /// <summary>
        /// 配置信息
        /// </summary>
        Setting setting;

        /// <summary>
        /// 日志客户端
        /// </summary>
        private Logger log;


        #endregion


        public void Initialize(ParameterProvider provider = null)
        {
            setting = new Setting();
            setting.Initialize(provider);

            log = LogManager.GetCurrentClassLogger();
           
            Sync();

            JobManager.AddJob(() => Sync(), s =>
            {
                s.ToRunEvery(1).Days().At(setting.Hours, setting.Minutes);
            });
        }

        void Sync()
        {

            #region mogodb

            //链接字符串
            string conn = setting.mongodb;
            //指定的数据库
            string dbName = setting.database;
            // Mongo客户端
            MongoClient client;
            //当前操作数据库
            IMongoDatabase database;
            //当前操作的数据库表
            IMongoCollection<CardInfo> collection;

            #endregion


            try
            {
                #region mogodb


                client = new MongoClient(conn);

                database = client.GetDatabase(dbName);
                collection = database.GetCollection<CardInfo>("tblCard");//表

                BsonArray querys = new BsonArray();
                querys.Add(new BsonDocument("bNo", "No00000000507"));
                querys.Add(new BsonDocument("bNo", "No00000000028"));
                querys.Add(new BsonDocument("bNo", "No00000000002"));
                querys.Add(new BsonDocument("bNo", "No00000000097"));
                querys.Add(new BsonDocument("bNo", "No00000000286"));
                querys.Add(new BsonDocument("bNo", "No00000000297"));
                querys.Add(new BsonDocument("bNo", "No00000000094"));
                BsonDocument query = new BsonDocument("$or", querys);

                List<CardInfo> tblCards = new List<CardInfo>();
                collection.Find(query).ForEachAsync((doc) =>
                {
                    tblCards.Add(doc);
                }).Wait();

                #region 用于检查数据是否同步
                BsonArray querysCno = new BsonArray();
                querysCno.Add(new BsonDocument("cNo", tblCards[(new Random().Next(6666))].cNo));
                querysCno.Add(new BsonDocument("cNo", tblCards[(new Random().Next(888))].cNo));
                BsonDocument queryCno = new BsonDocument("$or", querysCno);

                List<CardInfo> tblCardInfo = new List<CardInfo>();
                collection.Find(queryCno).ForEachAsync((doc) =>
                {
                    var result = doc;
                    tblCardInfo.Add(result);
                }).Wait();

                log.Debug("用于检查数据是否同步的新平台mogodb数据库记录：" + "记录1，cNo:" + tblCardInfo[0].cNo + " 服务开始时间: " + tblCardInfo[0].pEffDate + " 服务结束时间:" + tblCardInfo[0].pInvDate + "|记录2,cNo:" + tblCardInfo[1].cNo + " 服务开始时间: " + tblCardInfo[1].pEffDate + " 服务结束时间:" + tblCardInfo[1].pInvDate);

                #endregion

                #endregion


               
                string constr = setting.MySqlCon;

                #region 分页入库、Mysql事务的使用 

                var pageSize = 10000;
                var total = tblCards.Count;
                var pages = total / pageSize + (total % pageSize > 0 ? 1 : 0);
                for (int i = 0; i < pages; i++)
                {
                    var sucCount = 0;
                    var synCount = 0;
                    var subDatas = tblCards.Skip(i * pageSize).Take(pageSize);
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        con.Open();
                        MySqlTransaction transaction = con.BeginTransaction();

                        MySqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = transaction;

                        #region 执行mysql的两种方法

                        //StringBuilder sb = new StringBuilder();
                        foreach (var data in subDatas)
                        {

                            //sb.Append(
                            //    $"UPDATE card set ServiceStartDate='{data.pEffDate:yyyy-MM-dd}',ServiceEndDate='{data.pInvDate:yyyy-MM-dd} 'where sim='{data.cNo}';");
                            cmd.CommandText = $"UPDATE card set ServiceStartDate='{data.pEffDate:yyyy-MM-dd HH:mm:ss}',ServiceEndDate='{data.pInvDate:yyyy-MM-dd HH:mm:ss} 'where sim='{data.cNo}';";
                            int code = cmd.ExecuteNonQuery();
                            if (code == 1)
                            {
                                sucCount++;
                            }
                            else
                            {
                                log.Error($"服务时间同步第{i + 1}页,同步{data.cNo}失败,mysql更新语句:{cmd.CommandText}");
                            }
                            synCount++;
                        }

                        log.Debug($"服务时间同步第{i + 1}页,同步数据量:{synCount},成功数:{sucCount},失败数:{synCount - sucCount}");

                        transaction.Commit();

                        #endregion

                    }

                }

                #endregion

            }
            catch (Exception ex)
            {
                log.Error("服务时间同步服务请求出错:" + ex);
            }


        }

        /// <summary>
        /// 对应数据库中的数据表
        /// </summary>
        [BsonIgnoreExtraElements]
        public class CardInfo
        {
            /// <summary>
            /// 服务开始时间
            /// </summary>
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime pEffDate { get; set; }
            /// <summary>
            /// 服务结束时间
            /// </summary>
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime pInvDate { get; set; }
            /// <summary>
            /// 卡号
            /// </summary>
            public string cNo { get; set; }

        }

        #region 构造和析构

        #region IDisposable
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        int disposedFlag;

        ~DataSyncService()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放所占用的资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 获取该对象是否已经被释放
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsDisposed
        {
            get
            {
                return disposedFlag != 0;
            }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (System.Threading.Interlocked.Increment(ref disposedFlag) != 1) return;
            if (disposing)
            {
                //在这里编写托管资源释放代码
            }
            //在这里编写非托管资源释放代码
        }

        #endregion
    }
}
