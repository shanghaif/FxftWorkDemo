using System;
using System.Security.Cryptography;
using System.Text;

namespace IJson_Http_Post_小米
{
    public class RSATest
    {
        public RSATest()
        {
            string publicKey = "<RSAKeyValue><Modulus>raDY9rAJpWngHhliTIyOskUswZfUVU4h3AKzw/UpE3JOZEyDJkFaBLmPWG0YhkfaSAzTTyrw1YEsIb8hVdyOp/NCexmQir/1o5PEH+njXLCxU6s94cKRRjXhsUycBUL+FJTfFd7+G/VNUvIkiBznfjIxcl7gDCE2JFccFNeOBeU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            string privateKey = "<RSAKeyValue><Modulus>raDY9rAJpWngHhliTIyOskUswZfUVU4h3AKzw/UpE3JOZEyDJkFaBLmPWG0YhkfaSAzTTyrw1YEsIb8hVdyOp/NCexmQir/1o5PEH+njXLCxU6s94cKRRjXhsUycBUL+FJTfFd7+G/VNUvIkiBznfjIxcl7gDCE2JFccFNeOBeU=</Modulus><Exponent>AQAB</Exponent><P>488kZrKIYANelwBfhn/tpq0lygJLu1UKc1Fe9fDbVpeNS/QTs09BJKJ+hytBoLWcAdlBgIgwkrzuIQ+aoudG9Q==</P><Q>wx1J6Os2EtAnrCV6IKXyTD9k+FRJrk51PcrHbF/ocg0SWwsMEr/Uvjl7F277Fc3gLJoAlH/Q9qXEWB7z/JMNMQ==</Q><DP>LcMVr9+bT5Y9sR2tEsquBbZwieTuK6KIp+OnrMYGeZ2WD7TnJDT+hAp0WljRJSR2Q1TOOLCi+UckmtWUnbbR5Q==</DP><DQ>YuFr/IN/Yudkw6Q2kWcoNSYHiKZGtPSEDC3BBRZR2cLwI32kv7fCHClLTWPsEW/FhTUCyD9Cak02BSaipcTscQ==</DQ><InverseQ>HPIWEmC70LHrvjPUFuUqB3Kd99j1V/+v2xsINx3H8Rq0+4DBK6aJeuVNuERmJ9Wi3fDlMGnE97Z7hb3ztDPueQ==</InverseQ><D>JFiScnmBW24ha0uXBz05NdpDNzdoTH8xmVO7e97bPWXlQRu/LIfrpyeM+U7FHn72E7UTmvP7PEZxDZNsEAQ+nFNIhB7WQ6w14vsYVs7ryawUAJa4rIPFKpRqodzP6iIJje+pvWOUzamYrsFNJuGFN+K+sm8S2YzUx3ZUnY8pmEE=</D></RSAKeyValue>";

            string content = "hello xinxianlong";

            var encryptContent = RSAHelper.Encrypt(content, publicKey);
            var decryptContent = RSAHelper.Decrypt(encryptContent, privateKey);

            bool result = content.Equals(decryptContent);
        }
    }

    /// <summary>
    /// </summary>
    public class RSAHelper
    {
        public static string Sign(string content, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                using (var sh = SHA1.Create())
                {
                    byte[] signData = rsa.SignData(Encoding.UTF8.GetBytes(content), sh);
                    return Convert.ToBase64String(signData);
                }
            }
        }

        /// <summary>
        /// 公钥加密
        /// </summary>
        public static string Encrypt(string content, string publicKey)
        {
            using (RSACryptoServiceProvider publicRsa = new RSACryptoServiceProvider())
            {
                publicRsa.FromXmlString(publicKey);
                var decipher = publicRsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
                return Convert.ToBase64String(decipher);
            }
        }
        /// <summary>
        /// 私钥解密
        /// 
        /// </summary>
        public static string Decrypt(string encryptContent, string privateKey)
        {
            using (RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider())
            {
                var data = Convert.FromBase64String(encryptContent);
                privateRsa.FromXmlString(privateKey);
                var encData = privateRsa.Decrypt(data, false);
                return Encoding.UTF8.GetString(encData);
            }

        }
    }
}
