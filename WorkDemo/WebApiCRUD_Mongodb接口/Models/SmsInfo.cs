using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiCRUD_Mongodb接口.Models
{
    /// <summary>
    /// 短信对象
    /// </summary>
    [BsonIgnoreExtraElements]
    public class SmsInfo
    {
        /// <summary>
        /// 短信ID
        /// </summary>
        [BsonId]
        public string id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>        
        public string cNo { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>        
        public string bacthNo { get; set; }
        /// <summary>
        /// 手机号码/目标号码
        /// </summary>
        public string mNo { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 集团编号
        /// </summary>
        public string comId { get; set; }
        /// <summary>
        /// 创建时间/接收时间/发送时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime oDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime uDate { get; set; }
        /// <summary>
        /// 状态（待发送、发送中、成功、失败）
        /// </summary>
        public SMState state { get; set; }
        /// <summary>
        /// 短信操作类型：1=发送、2=接收
        /// </summary>
        public SMType sType { get; set; }
        /// <summary>
        /// 短信目标类型
        /// </summary>
        public SMDestType mType { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary>
        public string oResult { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public SMSourceType cType { get; set; }
        /// <summary>
        /// 销售者编号: 销售给终端的客户（例：代理商下的销售者）
        /// </summary>
        public string sNo { get; set; }
        /// <summary>
        /// 自营套餐编码
        /// </summary>
        public string sPCode { get; set; }
    }
}