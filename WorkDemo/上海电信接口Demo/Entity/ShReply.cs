using System.Collections.Generic;

namespace TelSDK
{
    /// <summary>
    /// 上海电信http应答响应
    /// </summary>
    public class ShReply
    {
        public string result_msg;
        /// <summary>
        /// 应答代码
        /// </summary>
        public result_code result_code;

        /// <summary>
        /// 结果集
        /// </summary>
        public List<Recordset> res_recordset;



    }
    /// <summary>
    /// 结果集
    /// </summary>
    public class Recordset
    {
        public string data;
    }
    /// <summary>
    /// 应答代码
    /// </summary>
    public enum result_code
    {
        成功 = 0,
        系统异常 = 1,
        服务不存在 = 1001,
        请求参数缺失 = 1002,
        用户名错误 = 1003,
        密码错误 = 1004,
        服务版本错误 = 1005,
        请求流水重复 = 1006
    }
}
