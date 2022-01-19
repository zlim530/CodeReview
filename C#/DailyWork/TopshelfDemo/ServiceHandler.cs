using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Timers;

namespace TopshelfDemo
{
    public class ServiceHandler
    {
        private readonly Timer _timer;
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceHandler));
        public ServiceHandler()
        {
            //_timer = new Timer(1000) { AutoReset = true };
            ////ElapsedEventHandler Timer.Elapsed 达到时间间隔时发生
            //_timer.Elapsed += (sender, EventArgs) => _log.Info("It is " + DateTime.Now + " and all is well");
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("AppSettings.json").Build();
            // 从配置文件中服务时间间隔
            var timeSpan = configuration.GetSection("timeSpan").Value;
            var time = Int32.Parse(timeSpan);
            _timer = new Timer(time) { AutoReset = true };
            _timer.Elapsed += (sender, EventArgs) => ReadEmailInfo.ReadPop3();
        }
        public void Start()
        {
            _log.Info("ReadEmialInfo Service is Started.");
            Console.WriteLine("Service and log is Started.");
            _timer.Start();
        }

        public void Stop()
        {
            _log.Info("Service is Stopped.");
            Console.WriteLine("Service and log is Stopped.");
            _timer.Stop();
        }
    }
}
