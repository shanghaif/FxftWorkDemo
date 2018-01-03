using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信状态
    /// </summary>
    public enum SMState : byte
    {
        /// <summary>
        /// 待发送
        /// </summary>
        待发送 = 0,
        /// <summary>
        /// 已经提交到运营商网关
        /// </summary>
        发送中,
        /// <summary>
        /// 成功：运营商网关已发送到对方终端
        /// </summary>
        成功,
        /// <summary>
        /// 失败
        /// </summary>
        失败,
        /// <summary>
        /// 欠费
        /// </summary>
        欠费
    }
}