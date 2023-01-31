using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DailyTest
{
	public class Config
	{
		/// <summary>
		/// 配置系统1-入门
		/// </summary>
		/// <param name="args"></param>
		static void Main0(string[] args)
		{
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            // optional:false 可选性为 false，则配置文件不存在是会抛异常报错
            configBuilder.AddJsonFile("config.json",optional:false,reloadOnChange:true);
			IConfigurationRoot configRoot = configBuilder.Build();
			// 简单的方式读取配置：不常用，仅作为入门了解
			string name = configRoot["name"];
			Console.WriteLine($"name = {name}");
			//string addr = configRoot.GetSection("proxy:address").Value;
			//Console.WriteLine($"address = {addr}");

			// 绑定一个类，自动完成配置的读取
			Proxy proxy = configRoot.GetSection("proxy").Get<Proxy>();
			Console.WriteLine($"address = {proxy.Address}, port = {proxy.Port}");

			// 还可以绑定整个配置文件
			Server server = configRoot.Get<Server>();
			Console.WriteLine(server.Name);
			Console.WriteLine(server.Age);
			Console.WriteLine(server.Proxy.Address);
        }

		/// <summary>
		/// 通过 DI 方法读取配置
		/// </summary>
		/// <param name="args"></param>
		static void Main1(string[] args)
		{
			var services = new ServiceCollection();
			services.AddScoped<TestController>();
            
			ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configRoot = configBuilder.Build();

			// 把整个 config 文件绑定到 Server 类上; 把配置文件中的 Proxy 节点绑定到 Proxy 类上
			services.AddOptions()
					.Configure<Server>(e => configRoot.Bind(e))
					.Configure<Proxy>(e => configRoot.GetSection("proxy").Bind(e));

			using (var sp = services.BuildServiceProvider())
			{
				while (true)
				{
					using (var scope = sp.CreateScope())
					{
                        var t = scope.ServiceProvider.GetRequiredService<TestController>();
                        t.Test();
						Console.WriteLine("change age");
						Console.ReadKey();
                        //var p = scope.ServiceProvider.GetRequiredService<TestController>();
                        //p.TestProxy();
                    }
					Console.WriteLine("点击任意键继续");
					// IOptionsSnapshot 是在下一次新的范围中生成更改，在本次范围中不变：即保证在同一次范围中不变
                    Console.ReadKey();
                    
                }

                //var t = sp.GetRequiredService<TestController>();
                //t.Test();

                //var p = sp.GetRequiredService<TestController>();
                //p.TestProxy();

            }
        }

		/// <summary>
		/// 其他配置提供者：控制台、环境变量、文件等
		/// </summary>
		/// <param name="args"></param>
		static void Main2(string[] args)
		{
            var services = new ServiceCollection();
            services.AddScoped<TestController>();
			services.AddScoped<TestWebConfig>();

            ConfigurationBuilder configBuilder = new ConfigurationBuilder();

			// 读取命令行参数配置信息
			configBuilder.AddCommandLine(args);
			// 读取环境变量配置信息
            configBuilder.AddFxConfig();
			configBuilder.AddEnvironmentVariables("C1_");// 建议加上自定义的前缀，以避免冲突
            //var conn = "Server=127.0.0.1;Database=MiniKitchen;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
            //configBuilder.AddDbConfiguration(() => new SqlConnection(conn), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));

            IConfigurationRoot configRoot = configBuilder.Build();

            services.AddOptions()
                    .Configure<Server>(e => configRoot.Bind(e))
                    .Configure<Proxy>(e => configRoot.GetSection("proxy").Bind(e));

            using (var sp = services.BuildServiceProvider())
            {
				var t = sp.GetRequiredService<TestController>();
				t.Test();
                t.TestProxy();
			}
        }

	}

	class TestController
	{
		private readonly IOptionsSnapshot<Server> optConfig;

		// 如果我们不需要整个配置文件，只需要其中一部分，那么可以绑定我们需要的那个类
		private readonly IOptionsSnapshot<Proxy> optProxy;

		public TestController(IOptionsSnapshot<Server> options, IOptionsSnapshot<Proxy> optProxy)
		{
			optConfig = options;
			this.optProxy = optProxy;

        }

		public void TestProxy()
		{
			Proxy proxy = optProxy.Value;
			Console.WriteLine(proxy.Address);
            Console.WriteLine("**********");
            Console.WriteLine(proxy.Port);
			//Console.WriteLine(string.Join(',',proxy.Ids));
		}

        public void Test()
		{
			Server server = optConfig.Value;
			Console.WriteLine(server.Name);
			Console.WriteLine("**********");
			Console.WriteLine(server.Age);
		}
	}

	public class Server
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public Proxy Proxy { get; set; }
	}

	public class Proxy
	{
		public string Address { get; set; }

		public int Port { get; set; }

		public int[] Ids { get; set; }
	}

}