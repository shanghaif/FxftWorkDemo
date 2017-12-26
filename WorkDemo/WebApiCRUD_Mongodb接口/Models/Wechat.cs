using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatSync
{
    public class Wechat
    {
        public string OpenId { get; set; }
        public string WxPublicNo { get; set; }
        public long UserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public override string ToString() => $"UserId{UserId},UpdateTime:{UpdateTime}";


    }
}
