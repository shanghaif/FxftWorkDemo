using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF
{
    class FigureContext:DbContext     
    {
        public FigureContext(string connectionName)
           : base(connectionName)
        {
        }

        public DbSet<Figure> Figures
        {
            get;
            set;
        }
    }
}
