using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NLogUsingDemo
{
    /// <summary>
    /// 注意：NLog.config文件要能够复制才能读到配置
    /// </summary>
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            logger.Trace("race Messag");
            logger.Debug("Debug Message");
            logger.Info("Info Message");
            logger.Error("Error Message");
        
        }
    }
}
