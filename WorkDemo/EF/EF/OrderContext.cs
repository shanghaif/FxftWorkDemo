using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class OrderContext:DbContext
    {
        public OrderContext(string connectionName)
           : base(connectionName)
        {
        }
        public DbSet<Order> Orders
        {
            get;
            set;
        }

        public DbSet<OrderDetail> OrderDetails
        {
            get;
            set;
        }
    }
}
