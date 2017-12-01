
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_MySql
{
    public interface IDbClient
    {
        IDbConnection GetOpenConnection(bool mars = false);

        void Add<T>(IEnumerable<T> entities) where T : class, new();

        T Add<T>(T entity) where T : class, new();

        bool Update<T>(T entity) where T : class, new();

        bool Delete<T>(T entity) where T : class, new();

        bool Delete<T>(object predicate) where T : class, new();

        T Get<T>(object id) where T : class, new();

        IEnumerable<T> Get<T>(object predicate = null, IList<ISort> sort = null) where T : class, new();

        PageInfo<T> Get<T>(object predicate, IList<ISort> sort, int pageIndex, int pageSize) where T : class, new();

        //PageInfo<T> Get<T, Tkey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, bool isDesc = false) where T : class, new();
    }
}
