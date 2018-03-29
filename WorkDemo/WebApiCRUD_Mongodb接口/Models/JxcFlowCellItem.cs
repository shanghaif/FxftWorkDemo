using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 流量池信息
    /// </summary>
    public class JxcFlowCellItem
    {
        /// <summary>
        /// 集团名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 流量池名称
        /// </summary>
        public string bloc_name { get; set; }
        /// <summary>
        /// 流量池编号
        /// </summary>
        public string cell_number { get; set; }
    }
}