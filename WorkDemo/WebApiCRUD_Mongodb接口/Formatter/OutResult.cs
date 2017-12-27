using Newtonsoft.Json;

namespace WebApiCRUD_Mongodb接口.Formatter
{
    /// <summary>
    /// 统一结果格式
    /// </summary>
    public class OutResult
    {
        /// <summary>
        /// 应答响应代码
        /// </summary>
        public OutCode code { get; set; }
        /// <summary>
        /// 应答结果
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 应答消息
        /// </summary>
        public string msg { get; set; }

        public override string ToString()
        {
            if (this != null)
            {
                return JsonConvert.SerializeObject(this);
            }
            return string.Empty;
        }
    }
}
