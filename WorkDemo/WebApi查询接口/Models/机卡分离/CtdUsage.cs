using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi查询接口.Models.机卡分离
{
    public class CtdUsage
    {
        /// <summary>
        /// iccid
        /// </summary>
        public string iccid { get; set; }
        /// <summary>
        /// 用量（B）
        /// </summary>
        public string dataUsage { get; set; }
    }
}