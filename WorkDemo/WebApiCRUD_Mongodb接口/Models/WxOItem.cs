using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace WechatSync.Entity
{
    //[BsonIgnoreExtraElements]
    public class WxOItem
    {
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wxName { get; set; }
        /// <summary>
        /// 微信openId集合:应华强使用openid，与文档定义[openId]不一致
        /// </summary>
        [BsonElement("openid")]
        public string openId { get; set; }
    }
}
