using log4net;
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
            _timer = new Timer(1000) { AutoReset = true };
            //ElapsedEventHandler Timer.Elapsed 达到时间间隔时发生
            _timer.Elapsed += (sender, EventArgs) => _log.Info("It is " + DateTime.Now + " and all is well");
        }
        public void Start()
        {
            _log.Info("Service is Started.");
            _timer.Start();
        }

        public void Stop()
        {
            _log.Info("Service is Stopped.");
            _timer.Stop();
        }
    }
}
