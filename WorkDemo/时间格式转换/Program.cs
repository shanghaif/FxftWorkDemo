using System;
using System.Globalization;
using static System.Console;
namespace 时间格式转换
{
    class Program
    {
       

        static void Main(string[] args)
        {

           var ts =    ConvertDateTimeToInt(DateTime.Now);
           
            Program program=new Program();

            #region DateTime 格式转换成任意其它格式

            #region 时间格式：20170913

            var timeStr = $"{DateTime.Now:yyyyMMddHHmmss}";
            timeStr = program.timeFormat();

            #endregion

            timeStr = $"{DateTime.Now:yyyy-MM}";
            timeStr = $"{DateTime.Today:MMdd}";

            #endregion


            #region 任意格式时间转换成DateTime格式

            var oriTime = "1309201715:41:25";
            DateTime time;
            if (DateTime.TryParseExact(oriTime, "ddMMyyyyHH:mm:ss", null, DateTimeStyles.None, out time))
            {

            }
            var timeFormat = time;
            var oriTime2 = "20170913154336";
            if (DateTime.TryParseExact(oriTime2, "yyyyMMddHHmmss", null, DateTimeStyles.None, out time))
            {

            }
            var timeFormat2 = time;

            #endregion

            ReadKey();
        }

        /// <summary>  
        ///参考文档: https://www.cnblogs.com/testsec/p/6095945.html
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks);//17位
            //long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        ///时间格式：20170913
        /// </summary>
        /// <returns></returns>
        public string timeFormat()
        {
            var date = DateTime.Now;
            var year = date.ToString("yyyy");
            var month = date.ToString("MM");
            var day = date.ToString("dd");
            var hour = date.ToString("HH");
            var munite = date.ToString("mm");
            var seconds = date.ToString("ss");
            return $"{year}{month}{day}{hour}{munite}{seconds}";
        }
    }
}
