using System;
using Microsoft.Extensions.DependencyInjection;

namespace DailyTest
{
	public class DI
	{
        /// <summary>
        /// ����ע��
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<Controller>();
            services.AddScoped<ILog, LogImpl>();
            services.AddScoped<IConfig, ConfigImpl>();
            // ������������ݿ��ж�ȡ����ʱ������Ҫ����ҵ����룬ֻ��Ҫ�����ݿ�ʵ����ʵ�� IConfig �ӿڲ�ע�ᵽ���������м���
            services.AddScoped<IConfig, DBConfigImpl>();
            services.AddScoped<IStorage, StorageImpl>();

            using (var sp = services.BuildServiceProvider())
            {
                // ��һ������������Ҫ�� ServiceLocator ͨ�� GetService ����ȡ�������� controller ���еķ�����ͨ�����캯������ע��ȫ����������������
                var c = sp.GetRequiredService<Controller>();
                c.Test();
            }
        }
	}

    class Controller
    {
        private readonly ILog log;
        private readonly IStorage storage;

        // ����ע���Ǿ��С���Ⱦ�ԡ��ģ�ֻҪ controller ������ͨ��������ȡ�ģ���ô�� controller �๹�캯������Ҫ�������ķ���Ҳ��һ��ͨ�������Զ���ȡ��ʵ����
        public Controller(ILog log, IStorage storage)
        {
            this.log = log;
            this.storage = storage;
        }

        public void Test()
        {
            this.log.Log("��ʼ�ϴ�");
            this.storage.Save("12345678910", "1.txt");
            this.log.Log("�ϴ����");
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
        // DI:����ģ��֮�����ϳ̶�
        private readonly IConfig config;

        public StorageImpl(IConfig config)
        {
            this.config = config;   
        }

        // ʵ�ִ洢������಻��Ҫ��ϵ config ������˭ʵ�ֵģ�ֻ��Ҫ֪���ڽ��д洢����Ļ�ȡʱ��config ����Ҳ��ɹ�������ȡ
        public void Save(string content, string name)
        {
            string serverName = config.GetValue("server");
            Console.WriteLine($"Upload to {serverName} for {name} file with content: {content}");
        }
    }

}
