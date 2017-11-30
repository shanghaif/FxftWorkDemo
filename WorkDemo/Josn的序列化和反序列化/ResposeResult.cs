using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Josn的序列化和反序列化
{
  public  class ResposeResult
    {
      public reultInfo resultInfo { get; set; }
    }

    public class reultInfo
    {
        public string respCode { get; set; }
        public string respDesc { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public object data { get; set; }

    }

   
}
