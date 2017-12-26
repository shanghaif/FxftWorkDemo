using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace WechatSync.Entity
{
    [BsonIgnoreExtraElements]
    public class WxInfo
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string cNo { get; set; }
        /// <summary>
        /// 微信信息
        /// </summary>
        public List<WxItem> wxInfo { get; set; }

        public override string ToString()
        {
            return cNo;
        }
    }
}
