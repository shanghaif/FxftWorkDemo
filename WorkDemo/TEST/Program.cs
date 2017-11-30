using System;
using System.Xml;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {


            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"C:\Users\zyy\Desktop\2017-9-27Demo\test.xml");

            ////创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            //var aa = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点
            XmlNode rootNode = xmlDoc.CreateElement("students");

            //创建student子节点
            XmlNode studentNode = xmlDoc.CreateElement("student");
            //创建一个属性
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("name");
            nameAttribute.Value = "张同学";
            //xml节点附件属性
            studentNode.Attributes.Append(nameAttribute);


            //创建courses子节点
            XmlNode coursesNode = xmlDoc.CreateElement("courses");
            XmlNode courseNode1 = xmlDoc.CreateElement("course");
            XmlAttribute courseNameAttr = xmlDoc.CreateAttribute("name");
            courseNameAttr.Value = "语文";
            courseNode1.Attributes.Append(courseNameAttr);
            XmlNode teacherCommentNode = xmlDoc.CreateElement("teacherComment");
            //创建Cdata块
            XmlCDataSection cdata = xmlDoc.CreateCDataSection("<font color=\"red\">这是语文老师的批注</font>");
            teacherCommentNode.AppendChild(cdata);
            courseNode1.AppendChild(teacherCommentNode);
            coursesNode.AppendChild(courseNode1);
            //附加子节点
            studentNode.AppendChild(coursesNode);

            rootNode.AppendChild(studentNode);
            //附加根节点
            xmlDoc.AppendChild(rootNode);

            //保存Xml文档
            xmlDoc.Save(@"C:\Users\zyy\Desktop\2017-9-27Demo\test.xml");

            //xmlDoc.LoadXml(@"C:\Users\zyy\Desktop\2017-9-27Demo\test.xml");

            Console.WriteLine("已保存Xml文档");
        }
    }
}
