namespace WebApiCRUD_Mongodb接口.Formatter
{
    /// <summary>
    /// 应答响应代码
    /// </summary>
    public enum OutCode : ushort
    {
        /// <summary>
        /// 成功
        /// </summary>
        成功 = 0,
        /// <summary>
        /// 失败：用户名/密码错误
        /// </summary>
        失败,
        /// <summary>
        /// 签名无效
        /// </summary>
        签名无效,
        /// <summary>
        /// 时间戳超时
        /// </summary>
        时间戳超时,
        /// <summary>
        /// Nonce已存在
        /// </summary>
        Nonce已存在,
        /// <summary>
        /// ContentMD5错误
        /// </summary>
        ContentMD5错误,
        /// <summary>
        /// AppKey无效
        /// </summary>
        AppKey无效,
        /// <summary>
        /// 请求参数错误
        /// </summary>
        请求参数错误,
        /// <summary>
        /// 请求功能无效
        /// </summary>
        请求功能无效,
        /// <summary>
        /// 无效卡
        /// </summary>
        无效卡 = 11000,
        /// <summary>
        /// 无效客户
        /// </summary>
        无效客户,
        /// <summary>
        /// 系统错误
        /// </summary>
        系统错误 = 60000,
        请求超时 = 60001,
    }
}
