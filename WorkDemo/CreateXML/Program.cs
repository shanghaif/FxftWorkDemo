using System.Xml;

namespace CreateXML
{
    public class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc1 = new XmlDocument();
            //doc1.Load(@"C:\Users\zyy\Desktop\2017-9-27Demo\bookcreate.xml");
            doc1.LoadXml("<bookstore></bookstore>");


            //加载文件并选出要结点:
            XmlDocument doc = new XmlDocument();
           doc.Load(@"C:\Users\zyy\Desktop\2017-9-27Demo\bookcreate.xml");
           XmlNode root = doc.SelectSingleNode("bookstore");

            //创建一个结点,并设置结点的属性:
            XmlElement xelKey = doc.CreateElement("book");
            XmlAttribute xelType = doc.CreateAttribute("Type");
            xelType.InnerText = "adfdsf";
            xelKey.SetAttributeNode(xelType);

            //创建子结点:
             XmlElement xelAuthor = doc.CreateElement("author");
             xelAuthor.InnerText = "dfdsa";
             xelKey.AppendChild(xelAuthor);
        }
    }
}
