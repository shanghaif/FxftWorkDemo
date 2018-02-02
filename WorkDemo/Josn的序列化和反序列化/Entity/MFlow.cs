namespace Josn的序列化和反序列化.Entity
{
    /// <summary>
    /// 单个号码月流量查询 
    /// </summary>
    public class MFlow
    {
        public ApnList[] apnList { get; set; }
        public string msisdn { get; set; }
    }

    public class ApnList
    {
        public string apnName { get; set; }
        public string lastFlowTime { get; set; }
        public Pkginfolist pkgInfoList { get; set; }
        public string restFlow { get; set; }
        public string totalFlow { get; set; }
        /// <summary>
        /// 号码已用总流量
        /// </summary>
        public string usedFlow { get; set; }
    }

    public class Pkginfolist
    {
        public ListPkg[] list { get; set; }
    }

    public class ListPkg
    {
        public string pkgCode { get; set; }
        public string pkgName { get; set; }
        public float restFlow { get; set; }
        public float totalFlow { get; set; }
        public float usedFlow { get; set; }
    }

}
