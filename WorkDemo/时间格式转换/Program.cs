using System;
using System.Globalization;
using static System.Console;
namespace 时间格式转换
{
    class Program
    {


        static void Main(string[] args)
        {
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
