using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new FigureContext("DemoEF"))
            {

                var o = new Figure();
                o.Name = "夏目";
                ctx.Figures.Add(o);
                ctx.SaveChanges();

                var query = from Attribute in ctx.Figures
                    select Attribute;
                foreach (var q in query)
                {
                    Console.WriteLine("OrderId:{0},OrderDate:{1}", q.Id, q.Name);
                }

                Console.Read();
            }
        }
    }
}
