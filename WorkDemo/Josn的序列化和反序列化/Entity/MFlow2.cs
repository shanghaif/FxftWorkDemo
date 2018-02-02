namespace Josn的序列化和反序列化.Entity
{

    public class MFlow2
    {
        public Apninfo[] apnInfo { get; set; }
    }

    public class Apninfo
    {
        public string apnName { get; set; }
        public Monthlylist monthlyList { get; set; }
    }

    public class Monthlylist
    {
        public List1[] list { get; set; }
    }

    public class List1
    {
        public string month { get; set; }
        public string usedFlow { get; set; }
    }

}
