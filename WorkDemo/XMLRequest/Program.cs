using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;

namespace XMLRequest
{
    /// <summary>
    /// Http Post +XML  (Body) 此接口调用IP限制
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlNode xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "GBK", "");
            xmlDoc.AppendChild(xmlDeclaration);

            //创建根节点
            XmlNode rootNode = xmlDoc.CreateElement("operation_in");
            //附加根节点
            xmlDoc.AppendChild(rootNode);

            //创建根节点并添加它的值
            XmlNode process_code = xmlDoc.CreateElement("process_code");
            process_code.InnerText = "cc_wlw_controlsr";
            rootNode.AppendChild(process_code);


            CreateNode(xmlDoc, rootNode, "app_id", "109000000124");
            CreateNode(xmlDoc, rootNode, "access_token", "moK5Zvop7YZete0PW7Cp");
            CreateNode(xmlDoc, rootNode, "req_type", "02");
            CreateNode(xmlDoc, rootNode, "req_time", $"{DateTime.Now:yyyyMMddHHmmss}");
            CreateNode(xmlDoc, rootNode, "req_seq", $"{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(10000, 99999)}");
            CreateNode(xmlDoc, rootNode, "sign", null);
            CreateNode(xmlDoc, rootNode, "verify_code", null);
            CreateNode(xmlDoc, rootNode, "terminal_id", null);
            CreateNode(xmlDoc, rootNode, "accept_seq", null);


            var contentNode = CreateNode(xmlDoc, rootNode, "content", null);//content节点
            CreateNode(xmlDoc, contentNode, "groupid", "FXFT_52733219701");
            CreateNode(xmlDoc, contentNode, "telnum", "1064844470020");
            CreateNode(xmlDoc, contentNode, "oprtype", "2");//1、 停机；2、 复机
            CreateNode(xmlDoc, contentNode, "reason", "7");



            var body = xmlDoc.InnerXml;//请求报文
            string urlRequest = @"http://221.178.251.182:80/internet_surfing";//正式地址
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            var task = httpClient.PostAsync(urlRequest, httpContent).Result;//可通过详细信息查看出来是否地址可以正常连接

            if (task.StatusCode == HttpStatusCode.OK)
            {
                var res = task.Content.ReadAsStringAsync().Result;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(res);

                //获取某个节点的值
                var xnodes = doc.GetElementsByTagName("resp_code");
                var value = xnodes[0].InnerText.Trim();

            }

            Console.ReadKey();

            ////保存Xml文档
            //xmlDoc.Save(@"C:\Users\zyy\Desktop\2017-9-27Demo\createXML.xml");

            //Console.WriteLine("已保存Xml文档");
        }
        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc">xml文档</param>    
        /// <param name="parentnode">父节点</param>    
        /// <param name="name">节点名</param>    
        /// <param name="value">节点值</param>    
        ///   
        public static XmlNode CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            if (value != null)
            {
                node.InnerText = value;
            }
            parentNode.AppendChild(node);
            return node;
        }
    }

}
