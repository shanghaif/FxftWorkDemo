using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_MySql
{
    public class SyncLog
    {
        public int id { get; set; }
        public string data_type { get; set; }
        public System.DateTime start_time { get; set; }
        public System.DateTime end_time { get; set; }
        public int syndata_volu { get; set; }
        public int suc_number { get; set; }
        public int fail_number { get; set; }
        public string fail_reason { get; set; }
        public int retry_count { get; set; }
        public byte oType { get; set; }
        public string comId { get; set; }
        public string syncType { get; set; }
        public string fileurl { get; set; }
    }
}
