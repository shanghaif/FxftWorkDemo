using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信操作类型：1=发送、2=接收
    /// </summary>
    public enum SMType
    {
        /// <summary>
        /// 发送
        /// </summary>
        发送 = 1,
        /// <summary>
        /// 接收
        /// </summary>
        接收 = 2
    }
}