using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信类型：1=物联网、2=普通
    /// </summary>
    public enum SMSourceType : byte
    {
        /// <summary>
        /// 物联网
        /// </summary>
        物联网 = 1,
        /// <summary>
        /// 普通
        /// </summary>
        普通
    }
}