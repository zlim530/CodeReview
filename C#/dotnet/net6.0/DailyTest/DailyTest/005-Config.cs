using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DailyTest
{
	public class Config
	{
		/// <summary>
		/// ����ϵͳ1-����
		/// </summary>
		/// <param name="args"></param>
		static void Main0(string[] args)
		{
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            // optional:false ��ѡ��Ϊ false���������ļ��������ǻ����쳣����
            configBuilder.AddJsonFile("config.json",optional:false,reloadOnChange:true);
			IConfigurationRoot configRoot = configBuilder.Build();
			// �򵥵ķ�ʽ��ȡ���ã������ã�����Ϊ�����˽�
			string name = configRoot["name"];
			Console.WriteLine($"name = {name}");
			//string addr = configRoot.GetSection("proxy:address").Value;
			//Console.WriteLine($"address = {addr}");

			// ��һ���࣬�Զ�������õĶ�ȡ
			Proxy proxy = configRoot.GetSection("proxy").Get<Proxy>();
			Console.WriteLine($"address = {proxy.Address}, port = {proxy.Port}");

			// �����԰����������ļ�
			Server server = configRoot.Get<Server>();
			Console.WriteLine(server.Name);
			Console.WriteLine(server.Age);
			Console.WriteLine(server.Proxy.Address);
        }

		/// <summary>
		/// ͨ�� DI ������ȡ����
		/// </summary>
		/// <param name="args"></param>
		static void Main1(string[] args)
		{
			var services = new ServiceCollection();
			services.AddScoped<TestController>();
            
			ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configRoot = configBuilder.Build();

			// ������ config �ļ��󶨵� Server ����; �������ļ��е� Proxy �ڵ�󶨵� Proxy ����
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
					Console.WriteLine("������������");
					// IOptionsSnapshot ������һ���µķ�Χ�����ɸ��ģ��ڱ��η�Χ�в��䣺����֤��ͬһ�η�Χ�в���
                    Console.ReadKey();
                    
                }

                //var t = sp.GetRequiredService<TestController>();
                //t.Test();

                //var p = sp.GetRequiredService<TestController>();
                //p.TestProxy();

            }
        }

		/// <summary>
		/// ���������ṩ�ߣ�����̨�������������ļ���
		/// </summary>
		/// <param name="args"></param>
		static void Main2(string[] args)
		{
            var services = new ServiceCollection();
            services.AddScoped<TestController>();
			services.AddScoped<TestWebConfig>();

            ConfigurationBuilder configBuilder = new ConfigurationBuilder();

			// ��ȡ�����в���������Ϣ
			configBuilder.AddCommandLine(args);
			// ��ȡ��������������Ϣ
            configBuilder.AddFxConfig();
			configBuilder.AddEnvironmentVariables("C1_");// ��������Զ����ǰ׺���Ա����ͻ
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

		// ������ǲ���Ҫ���������ļ���ֻ��Ҫ����һ���֣���ô���԰�������Ҫ���Ǹ���
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