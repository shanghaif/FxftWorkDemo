using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_MySql
{

    public class FlowCell
    {
        public int Id { get; set; }
        public int type_id { get; set; }
        public string comId { get; set; }
        public string bloc_name { get; set; }
        public string cell_number { get; set; }
        public string name { get; set; }

        public int flows { get; set; }

        public int status { get; set; }

        public int del { get; set; }
    }
}
