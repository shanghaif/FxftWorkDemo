namespace Josn的序列化和反序列化.Entity
{

  

    /// <summary>
    /// VS生成的JSon类(菜单编辑-选择性黏贴)
    /// </summary>
    public class Rootobject
    {
        public Apnlist apnList { get; set; }
        public string msisdn { get; set; }
        public string openTime { get; set; }
        public Packages packages { get; set; }
        public string status { get; set; }
        public string statusTime { get; set; }
    }

    public class Apnlist
    {
        public string[] list { get; set; }
    }

    public class Packages
    {
        public List[] list { get; set; }
    }

    public class List
    {
        public string pkgCode { get; set; }
        public string pkgEfftTime { get; set; }
        public string pkgExpireTime { get; set; }
        public string pkgName { get; set; }
        public string pkgStatus { get; set; }
    }
}
