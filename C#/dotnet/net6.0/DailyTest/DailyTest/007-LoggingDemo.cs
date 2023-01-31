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
		/// ʲô�� Logging
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
							//.AddConsole()// ����־���������̨
							//.AddEventLog()// EventLog��������־�ṩ�ߣ� ���� windows ��֧��
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
			logger.LogDebug("��ʼִ�����ݿ�ͬ��");
			logger.LogDebug("�������ݿ�ɹ�");
			logger.LogWarning("��������ʧ�ܣ����Ե�һ��");
			logger.LogWarning("��������ʧ�ܣ����Եڶ���");
			logger.LogError("������������ʧ��");
			try
			{
				File.ReadAllText("A:/1.txt");
				logger.LogDebug("��ȡ�ļ��ɹ�");
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message,"��ȡ�ļ�ʧ��");
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
            logger.LogDebug("��ʼִ��FTPͬ��");
            logger.LogDebug("����FTP�ɹ�");
			logger.LogDebug("New Employee {@person}", employee);
            logger.LogWarning("��������ʧ�ܣ����Ե�һ��");
            logger.LogWarning("��������ʧ�ܣ����Եڶ���");
            logger.LogError("������������ʧ��");
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
            logger.LogDebug("��ʼִ��FTPͬ��");
            logger.LogDebug("����FTP�ɹ�");
            logger.LogWarning("��������ʧ�ܣ����Ե�һ��");
            logger.LogWarning("��������ʧ�ܣ����Եڶ���");
            logger.LogError("������������ʧ��");
        }
    }
}