using DapperExtensions;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Z.Dapper.Plus;


namespace Dapper_MySql
{
    public class MySqlAdapter : IDisposable, IDbClient
    {
        
        string connStr = "server=localhost;User Id=sa;password=123456;Database=datasync";
        //private string connStr =
        //    @"Data Source=.\sqlexpress;Initial Catalog=erp;Integrated Security=True;uid=jijiuser;pwd=Fxft2016";
        DbType dbType = DbType.MYSql;
        int commandTimeout = 30;
        /// <summary>
        /// 数据客户端
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="commandTimeout">操作超时,单位：秒</param>
        /// <param name="autoEditEntityTime">是否自动更实体对象的创建时间、更新时间</param>
        public MySqlAdapter(string connStr, DbType dbType = DbType.MYSql, int commandTimeout = 30, bool autoEditEntityTime = true)
        {
            if (string.IsNullOrWhiteSpace(connStr)) throw new NoNullAllowedException("数据库连接字符串不允许为空");
            this.connStr = connStr;
            this.dbType = dbType;
            this.commandTimeout = commandTimeout;
            if (dbType == DbType.MYSql)
            {
                DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            }
            Mappings.Initialize();
        }
        /// <summary>
        /// 获取打开的连接
        /// </summary>
        /// <param name="mars">MSSql数据库下有效：如果为 true，则应用程序可以保留多活动结果集 (MARS)。 如果为 false，则应用程序必须处理或取消一个批处理中的所有结果集，然后才能对该连接执行任何其他批处理。</param>
        /// <returns></returns>
        public IDbConnection GetOpenConnection(bool mars = false)
        {
            IDbConnection connection = null;
            string cs = connStr;
            switch (dbType)
            {
                case DbType.MSSql:
                {
                    if (mars)
                    {
                        var scsb = new SqlConnectionStringBuilder(cs)
                        {
                            MultipleActiveResultSets = true
                        };
                        cs = scsb.ConnectionString;
                    }
                    connection = new SqlConnection(cs);
                    break;
                }
                case DbType.MYSql:
                {
                    DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();

                    var scsb = new MySqlConnectionStringBuilder(cs);
                    cs = scsb.ConnectionString;
                    connection = new MySqlConnection(cs);
                    break;
                }
            }
            connection.Open();
            return connection;
        }

        #region Add

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体对象集</param>
        public void Add<T>(IEnumerable<T> entities) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        cnn.Insert(entities, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
            }

            //using (IDbConnection cnn = GetOpenConnection())
            //{
            //    var trans = cnn.BeginTransaction();
            //    cnn.Execute(@"insert Member(Username, IsActive) values(@Username, @IsActive)", entities, transaction: trans);
            //    trans.Commit();
            //}
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象</returns>
        public T Add<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                T res = null;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        int id = cnn.Insert(entity, trans, commandTimeout);
                        if (id > 0)
                        {
                            res = entity;
                        }
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Update<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Update(entity, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        public bool Update<T>(IEnumerable<T> entities) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        trans.BulkUpdate(entities);
                        res = true;
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Delete<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Delete(entity, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Delete<T>(object predicate) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Delete(predicate, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        #endregion

        #region Query/Get

        /// <summary>
        /// 查询单个结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">实体的Id属性值</param>
        /// <returns>查询结果</returns>
        public T Get<T>(object id) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                T res = null;
                try
                {
                    res = cnn.Get<T>(id, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return res;
            }
        }

        /// <summary>
        /// 查询结果集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">分页查询条件</param>
        /// <param name="sort">是否排序</param>
        /// <returns>查询结果</returns>
        public IEnumerable<T> Get<T>(object predicate, IList<ISort> sort) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                IEnumerable<T> res = null;
                try
                {
                    res = cnn.GetList<T>(predicate, sort, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return res;
            }
        }

        /// <summary>
        /// 查询结果分页
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">分页查询条件</param>
        /// <param name="sort">是否排序,不允许为空</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>查询结果</returns>
        public PageInfo<T> Get<T>(object predicate, IList<ISort> sort, int pageIndex, int pageSize) where T : class, new()
        {
            if (sort == null) throw new ArgumentNullException("sort 不允许为null");
            if (pageIndex < 0) pageIndex = 0;
            using (IDbConnection cnn = GetOpenConnection())
            {
                PageInfo<T> pInfo = null;
                try
                {
                    pInfo = new PageInfo<T>();
                    if (pageIndex == 0)
                    {
                        int count = cnn.Count<T>(predicate, null, commandTimeout);
                        pInfo.TotalCount = count;
                    }
                    pInfo.Data = cnn.GetPage<T>(predicate, sort, pageIndex, pageSize, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return pInfo;
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DbClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
