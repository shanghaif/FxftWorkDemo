using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace WechatSync.Entity
{
    /// <summary>
    /// 微信
    /// </summary>
    public class WxItem
    {
        /// <summary>
        /// 微信appId
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 微信WxOItem
        /// </summary>
        public List<WxOItem> oList { get; set; }

        public override string ToString()
        {
            return appId;
        }
    }
}
