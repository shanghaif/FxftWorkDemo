using NRails.Configuration;
using NRails.Util;
using ServiceTimeSync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace 服务时间同步服务
{
    static partial class Program
    {
        //static AliLog.Logger log;
        static string serviceName = "服务时间同步服务";
        static List<DataSyncService> services;

        [STAThread]
        static void Main(string[] args)
        {
            ServiceRunner.Run(MainMethod, args, ShutDown);
        }
        
        static void MainMethod()
        {

            var entryName = AssemblyHelper.GetEntryFileName();
            if (entryName != null)
                serviceName = Path.GetFileNameWithoutExtension(entryName);

            //log = new AliLog.Logger();

            //log.Debug(string.Format("[{0}]已启动!", serviceName));

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

            string[] files = Directory.GetFiles(configPath);
            services = LoadService(files);
        }

        private static List<DataSyncService> LoadService(string[] files)
        {
            List<DataSyncService> serviceList = new List<DataSyncService>();
            foreach (string cfg in files)
            {
                AppConfigProvider provider = new AppConfigProvider(cfg);
                if (!provider.CanAccess)
                {
                    provider.Dispose();
                    //log.Debug(string.Format("加载服务:[{0}]失败,配置文件不可读!", Path.GetFileNameWithoutExtension(cfg)));
                    continue;
                }

                Setting setting = new Setting();
                setting.Initialize(provider);
                setting.Name = Path.GetFileNameWithoutExtension(cfg);
                DataSyncService service;

                #region 加载终端数据解释器
                if (setting.ServiceAssembly == null || setting.ServiceAssembly == string.Empty)
                {
                    throw new Exception("解析服务[{0},必须加载服务配置ServiceAssembly!".format(setting.Name));
                }
                else
                {
                    try
                    {
                        service = (DataSyncService)AssemblyHelper.CreateInstance(setting.ServiceAssembly.Split(','));
                        if (service == null)
                            throw new Exception("解析服务[{0},无法加载!找不到指定的程序集:{1}".format(setting.Name, setting.ServiceAssembly));
                        service.Initialize(provider);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("解析服务[{0},在加载时发生错误!".format(setting.Name), ex);
                    }
                }
                #endregion

                serviceList.Add(service);
            }
            return serviceList;
        }

        public static void ShutDown()
        {
            if (services != null && services.Count != 0)
            {
                foreach (var service in services)
                {
                    service.Dispose();
                }
                services.Clear();
            }
            //log.Debug("{0}已退出!".f(serviceName));
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //log.Debug("出现未处理的应用程序异常!", e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //log.Debug("出现未处理的域异常!", (Exception)e.ExceptionObject);
        }
   
    }
}
