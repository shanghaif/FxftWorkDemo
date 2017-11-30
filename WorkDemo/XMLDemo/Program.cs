using System;
using System.Xml;

namespace XMLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(@"C:\Users\zyy\Desktop\2017-9-27Demo\Book.xml", settings);
            xmlDoc.Load(reader);

            // 得到根节点bookstore
            XmlNode xn = xmlDoc.SelectSingleNode("bookstore");


            // 得到根节点的所有子节点
            XmlNodeList xnl = xn.ChildNodes;

            foreach (XmlNode xn1 in xnl)
            {
                BookModel bookModel = new BookModel();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                bookModel.BookISBN = xe.GetAttribute("ISBN").ToString();
                bookModel.BookType = xe.GetAttribute("Type").ToString();

                #region 重点

                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                bookModel.BookName = xnl0.Item(0).InnerText;
                bookModel.BookAuthor = xnl0.Item(1).InnerText;
                bookModel.BookPrice = Convert.ToDouble(xnl0.Item(2).InnerText);

                #endregion

                //bookModeList.Add(bookModel);
            }
            //dgvBookInfo.DataSource = bookModeList;

        }
    }

}
