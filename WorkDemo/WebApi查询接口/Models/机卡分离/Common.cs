using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WebApi查询接口.Models.机卡分离
{
    public class Common
    {
        /// <summary>
        /// 密匙
        /// </summary>
        private const string secret = "default";

        /// <summary>
        /// SHA1验签
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="chartset">字符集 可选 默认为“UTF-8”</param>
        /// <returns></returns>
        public static bool CheckSign_HMACSHA1(string timestamp, string signature, string chartset = null)
        {
            //try
            {
                Encoding encode = Encoding.UTF8;
                if (chartset != null)
                    encode = Encoding.GetEncoding(chartset);

                byte[] byteData = encode.GetBytes(timestamp);
                HMACSHA1 hmac = new HMACSHA1(encode.GetBytes(secret));
                using (CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
                {
                    cs.Write(byteData, 0, byteData.Length);
                }
                var result = System.Web.HttpUtility.UrlDecode(Convert.ToBase64String(hmac.Hash));
                if (result.Equals(signature))
                    return true;
                return false;
            }
            //catch (Exception ex)
            //{
            //   throw new ServiceContainer.MessageException("HMACSHA1出错了--" + ex.ToString());
               
            //}
        }

        /// <summary>
        /// 解析xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default(T);
            //try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                    {
                        t = (T)xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    throw new ServiceContainer.MessageException("xml反序列化出错了--" + ex.ToString());
            //}
            return t;
        }
    }
}