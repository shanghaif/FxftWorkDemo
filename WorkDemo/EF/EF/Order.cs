using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
