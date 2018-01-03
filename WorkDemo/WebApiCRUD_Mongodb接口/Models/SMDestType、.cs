using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信目标类型
    /// </summary>
    public enum SMDestType : byte
    {
        /// <summary>
        /// 平台->终端
        /// </summary>
        平台To终端 = 1,
        /// <summary>
        /// 终端->平台
        /// </summary>
        终端To平台,
        /// <summary>
        /// 手机->平台
        /// </summary>
        手机To平台,
        /// <summary>
        /// 平台->手机
        /// </summary>
        平台To手机
    }
}