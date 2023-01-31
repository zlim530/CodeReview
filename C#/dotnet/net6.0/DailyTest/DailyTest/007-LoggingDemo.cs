using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using SystemServices;

namespace DailyTest
{
	public class LoggingDemo
	{
		/// <summary>
		/// 什么是 Logging
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var services = new ServiceCollection();

			services.AddLogging(logBuilder =>
			{
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(new JsonFormatter())
                .CreateLogger();

                logBuilder
							//.AddConsole()// 将日志输出到控制台
							//.AddEventLog()// EventLog：其他日志提供者： 仅在 windows 上支持
							//.AddNLog()
							.AddSerilog()
							//.SetMinimumLevel(LogLevel.Trace)
							;
			});

            services.AddScoped<LogTest>();
			services.AddScoped<NLogTest>();
			services.AddScoped<SerilogTest>();

            using (var sp = services.BuildServiceProvider())
			{
				var lt = sp.GetService<LogTest>();
				lt.Test();

                var serilog = sp.GetService<SerilogTest>();
                serilog.Test();

                //var nlog = sp.GetRequiredService<NLogTest>();
                //for (int i = 0; i < 10000; i++)
                //{
                //	nlog.Test();
                //}
            }
		}
	}

	class LogTest
	{
		private readonly ILogger<LogTest> logger;

		public LogTest(ILogger<LogTest> logger)
		{
			this.logger = logger;
		}

		public void Test()
		{
			logger.LogDebug("开始执行数据库同步");
			logger.LogDebug("连接数据库成功");
			logger.LogWarning("查找数据失败，重试第一次");
			logger.LogWarning("查找数据失败，重试第二次");
			logger.LogError("查找数据最终失败");
			try
			{
				File.ReadAllText("A:/1.txt");
				logger.LogDebug("读取文件成功");
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message,"读取文件失败");
			}
        }
	}

	class SerilogTest
	{
        private readonly ILogger<SerilogTest> logger;

        public SerilogTest(ILogger<SerilogTest> logger)
        {
            this.logger = logger;
        }

        public void Test()
        {
			var employee = new Employee() { Id = 1, Name = "Jack"};
            logger.LogDebug("开始执行FTP同步");
            logger.LogDebug("连接FTP成功");
			logger.LogDebug("New Employee {@person}", employee);
            logger.LogWarning("查找数据失败，重试第一次");
            logger.LogWarning("查找数据失败，重试第二次");
            logger.LogError("查找数据最终失败");
        }
    }

}

namespace SystemServices
{
    class NLogTest
    {
        private readonly ILogger<NLogTest> logger;

        public NLogTest(ILogger<NLogTest> logger)
        {
            this.logger = logger;
        }

        public void Test()
        {
            logger.LogDebug("开始执行FTP同步");
            logger.LogDebug("连接FTP成功");
            logger.LogWarning("查找数据失败，重试第一次");
            logger.LogWarning("查找数据失败，重试第二次");
            logger.LogError("查找数据最终失败");
        }
    }
}