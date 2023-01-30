using System;
using Microsoft.Extensions.DependencyInjection;

namespace DailyTest
{
	public class DI
	{
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<Controller>();
            services.AddScoped<ILog, LogImpl>();
            services.AddScoped<IConfig, ConfigImpl>();
            // 当我们想从数据库中读取配置时，不需要更改业务代码，只需要让数据库实现类实现 IConfig 接口并注册到服务容器中即可
            services.AddScoped<IConfig, DBConfigImpl>();
            services.AddScoped<IStorage, StorageImpl>();

            using (var sp = services.BuildServiceProvider())
            {
                // 第一个根对象还是需要用 ServiceLocator 通过 GetService 来获取，但是在 controller 类中的服务则通过构造函数依赖注入全部被容器创建好了
                var c = sp.GetRequiredService<Controller>();
                c.Test();
            }
        }
	}

    class Controller
    {
        private readonly ILog log;
        private readonly IStorage storage;

        // 依赖注入是具有“传染性”的：只要 controller 对象是通过容器获取的，那么在 controller 类构造函数中需要的其他的服务也会一并通过容易自动获取其实现类
        public Controller(ILog log, IStorage storage)
        {
            this.log = log;
            this.storage = storage;
        }

        public void Test()
        {
            this.log.Log("开始上传");
            this.storage.Save("12345678910", "1.txt");
            this.log.Log("上传完毕");
        }

    }

	interface ILog
	{
		public void Log(string msg);
	}

    class LogImpl : ILog
    {
        public void Log(string msg)
        {
			Console.WriteLine($"Log: {msg}");
        }
    }

	interface IConfig
	{
		public string GetValue(string msg);
	}

    class ConfigImpl : IConfig
    {
        public string GetValue(string msg)
        {
            return "Hello";
        }
    }

    class DBConfigImpl : IConfig
    {
        public string GetValue(string msg)
        {
            Console.WriteLine("Read config from DB");
            return "hello DB";
        }
    }

    interface IStorage
	{
		public void Save(string content, string name);
	}

    class StorageImpl : IStorage
    {
        // DI:降低模块之间的耦合程度
        private readonly IConfig config;

        public StorageImpl(IConfig config)
        {
            this.config = config;   
        }

        // 实现存储服务的类不需要关系 config 服务是谁实现的，只需要知道在进行存储服务的获取时，config 对象也会成功创建获取
        public void Save(string content, string name)
        {
            string serverName = config.GetValue("server");
            Console.WriteLine($"Upload to {serverName} for {name} file with content: {content}");
        }
    }

}
