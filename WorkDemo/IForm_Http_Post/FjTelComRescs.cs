using System.Collections.Generic;

namespace TelSDK
{
    /// <summary>
    /// 福建电信接口返回值
    /// </summary>
    public  class FjTelComRescs
    {
      public Head head { get; set; }
      public List<Body> body { get; set; }
    }

    public class Body
    {
        public string status { get; set; }
        /// <summary>
        /// 产品资料查询接口
        /// </summary>
        public SvcCont SvcCont
        { get; set; }


       
    }

    /// <summary>
    /// 产品资料查询
    /// </summary>
    public class SvcCont
    {

        public string resultMsg
        { get; set; }
        /// <summary>
        /// 产品查询结果码
        /// </summary>
        public ResultCode resultCode { get; set; }

        public SvcContResult[]  result { get; set; }
    }
    /// <summary>
    /// 产品资料查询结果
    /// </summary>
    public class SvcContResult
    {
        public ProdInfos[] prodInfos { get; set; }
    }

    public class ProdInfos
    {
        public List<FunProdInfos>  funProdInfos { get; set; }
    }
    /// <summary>
    /// 找到产品资料正确的节点
    /// </summary>
    public class FunProdInfos
    {
        public AttrInfos[] attrInfos { get; set; }
        public string productName { get; set; }
    }
    /// <summary>
    /// 产品资料最好要获取的字段的值
    /// </summary>
    public class AttrInfos
    {
        public string attrName { get; set; }
        public string attrValue { get; set; }
    }
    /// <summary>
    /// 产品查询结果码
    /// </summary>
    public enum ResultCode
    {
        成功,
        失败,
        异常
    }
    public class Head
    {
        /// <summary>
        /// 命令码类型
        /// </summary>
        public long cmdType { get; set; }
        public string resultDesc { get; set; }
        /// <summary>
        /// result结果码
        /// </summary>
        public fjTelCode result { get; set; }
    }

    /// <summary>
    /// result结果码
    /// </summary>
    public enum fjTelCode:byte
    {
        交易成功
    }
}
