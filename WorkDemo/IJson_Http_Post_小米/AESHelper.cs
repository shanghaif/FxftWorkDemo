using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IJson_Http_Post_小米
{
    public class AESTest
    {
        /// <summary>
        /// 翻译java源码AES
        /// </summary>
        public AESTest()
        {
            string key = "ZcAnM9q1FwCnLvuEuyfGYQ==";
            var encryptResult = AESHelper.Encrypt("test", key);
            var decryptResult = AESHelper.Decrypt(encryptResult, key);
        }
    }

    /// <summary>
    /// 翻译java源码AES
    /// </summary>
    public class AESHelper
    {

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt">需要解密的字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = Convert.FromBase64String(key); //将TestGenAESByteKey类输出的字符串转为byte数组  
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;        //必须设置为ECB  
            rDel.Padding = PaddingMode.PKCS7;  //必须设置为PKCS7  
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt">需要加密的字符串</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            byte[] keyArray = Convert.FromBase64String(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
