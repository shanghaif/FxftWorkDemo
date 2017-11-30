using System;
using System.Security.Cryptography;
using System.Text;
using NRails.Util;//加密类
using static System.Console;

namespace 加密
{
    class Program
    {
        static void Main(string[] args)
        {
            NRails.Util.EncryptHelper.MD5Encrypt("`24");
          
            #region MD5加密  MD5的加解密有问题

            string strUrl =
     "serverNameLao.base.cardState.querycustId3071859120357006randomId12320170228170923457iccid8986061501000889173";

            var strMd5Hash16 = GetMd5Hash16(strUrl);
            var strMd5Hash32 = GetMd5Hash32(strUrl);
            var strMd5Hash64 = GetMd5Hash64(strUrl);

            #endregion

            #region Shal加密

            var str =
              "96d55353ccdac6526cffc3e36cfe682dapnNameappKeyeayfresqxyformatjsonmethodtriopi.business.gprs.switchmsisdn1064888683150optType2transID2001829965201710101244287951228v3.096d55353ccdac6526cffc3e36cfe682d";

            var strShal16 = Sha1(str);

            #endregion

            #region 3des的加密与解密

            var strResult = @"HvOIOlbtsPiq1XdHEiHYP5VyV4CU8o4xEwDaBINhsil6Hmlnb/ZAFzGzT3lBBb1LMIAROyFTl+mxLA8m + BdaLIzl7G9ZpR9EUKiUw2mRjrnyQgXYxUWWBgZZcwI1hOqs514rstKPDKNwON / R2uWZMkK0sFU94DzOaQQ5bCLXBDEWB / 1U1sO /q9fFR + 9n /XjRQvY2WI3rc0XomIqCezTEcowpLU4G1Y4Rb0U4oagBhsd1OPMtDFNKg5 + XOgzmVhxt";//注意字符串是否换行了,换行符会影响加密结果
            var secret = "96d55353ccdac6526cffc3e3";
            var str3des解密 = Decrypt3Des(strResult, secret,CipherMode.ECB, null);
            var str3des加密 = Encrypt3Des(str3des解密, secret, CipherMode.ECB, null);
           
            #endregion


            ReadKey();
        }


        #region 对字符串进行MD5加密,并返回字符串

        /// <summary>
        /// 对字符串进行MD5加密 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>16进制格式的字符串</returns>
        public static string GetMd5Hash16(String input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create(); //实例化一个md5对像

            // 将输入字符串转换为字节数组并计算哈希数据
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // 返回十六进制字符串  
            return sBuilder.ToString();
        }

        /// <summary>
        /// 对字符串进行MD5加密 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>32位字符串</returns>
        public static string GetMd5Hash32(String input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create(); //实例化一个md5对像

            // 将输入字符串转换为字节数组并计算哈希数据
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为32进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X"));
            }

            // 返回32进制字符串  
            return sBuilder.ToString();
        }

        /// <summary>
        /// 对字符串进行MD5加密 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>64位字符串</returns>
        public static string GetMd5Hash64(String input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create(); //实例化一个md5对像

            // 将输入字符串转换为字节数组并计算哈希数据
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(data);
        }

        #endregion

        #region Shal加密

        /// <summary>
        /// 对字符串进行Sha1加密 16进制
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lower"></param>
        /// <returns></returns>
        public static string Sha1(string value, bool lower = false)
        {
            try
            {
                var sha1 = SHA1.Create();
                sha1.Initialize();
                byte[] buffer = Encoding.UTF8.GetBytes(value);
                byte[] ret = sha1.ComputeHash(buffer);
                sha1.Clear();
                sha1.Dispose();
                return lower ? BC.ToBCDString(ret).ToLower() : BC.ToBCDString(ret).ToUpper();
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region 3des的加密与解密

        #region 3des解密

        /// <summary>
        /// des 解密
        /// </summary>
        /// <param name="aStrString">加密的字符串</param>
        /// <param name="aStrKey">密钥</param>
        /// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>解密的字符串</returns>
        public static string Decrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(aStrKey),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                }
                var desDecrypt = des.CreateDecryptor();
                var result = "";
                byte[] buffer = Convert.FromBase64String(aStrString);
                result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return result;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        #endregion

        #region 3des加密

        /// <summary>
        /// 3des ecb模式加密
        /// </summary>
        /// <param name="aStrString">待加密的字符串</param>
        /// <param name="aStrKey">密钥</param>
        /// <param name="iv">加密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(aStrKey),
                    Mode = mode
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                }
                var desEncrypt = des.CreateEncryptor();
                byte[] buffer = Encoding.UTF8.GetBytes(aStrString);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        #endregion

        #endregion
    }
}
