using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Josn的序列化和反序列化
{
    /// <summary>
    /// 附加数据
    /// </summary>
    public class AdditionalField : reultInfo
    {
        public new DifferentField data { get; set; }
        //public new DifferentField data
    }
}
