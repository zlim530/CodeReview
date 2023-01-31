using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Xml;

namespace DailyTest
{
    public class Program
    {
        static void Main0(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<TestWebConfig>();

            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            //configBuilder.Add(new FxConfigSource() { Path = "web.config"});
            configBuilder.AddFxConfig();
            IConfigurationRoot configRoot = configBuilder.Build();

            services.AddOptions()
                    .Configure<WebConfig>(e => configRoot.Bind(e));

            using (var sp = services.BuildServiceProvider())
            {
                var t = sp.GetRequiredService<TestWebConfig>();
                t.Test();
            }
        }
    }

    static class FxConfigExtensions 
    {
        public static IConfigurationBuilder AddFxConfig(this IConfigurationBuilder builder, string path = null)
        {
            if (path == null)
            {
                path = "web.config";
            }
            builder.Add(new FxConfigSource() { Path = path});
            return builder;
        }
    }

    public class FxConfigProvider : FileConfigurationProvider
    {
        public FxConfigProvider(FxConfigSource source) : base(source)
        {

        }

        public override void Load(Stream stream)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            var csNodes = xmlDoc.SelectNodes("/configuration/connectionStrings/add");
            foreach (XmlNode xmlNode in csNodes.Cast<XmlNode>())
            {
                var name = xmlNode.Attributes["name"].Value;
                var conn = xmlNode.Attributes["connectionString"].Value;
                //[conn1:{connectionString:"", providerName:""}, conn2: {...}]

                data[$"{name}:connectionString"] = conn;
                var attProviderName = xmlNode.Attributes["providerName"];
                if (attProviderName != null)
                {
                    data[$"{name}:providerName"] = attProviderName.Value;
                }
            }

            var asNodes = xmlDoc.SelectNodes("/configuration/appSettings/add");
            foreach (XmlNode xmlNode in asNodes.Cast<XmlNode>())
            {
                var key = xmlNode.Attributes["key"].Value;
                key = key.Replace(".",":");
                var value = xmlNode.Attributes["value"].Value;
                data[key] = value;
            }

            this.Data = data;
        }
    }


    /// <summary>
    /// 主要是提供参数使用：如 Path 等 
    /// </summary>
    public class FxConfigSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new FxConfigProvider(this);
        }
    }


    class WebConfig
    {
        public ConnectStr Conn1 { get; set; }
        public ConnectStr ConnTest { get; set; }
        public Server Server { get; set; }
    }

    public class ConnectStr
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }

    class TestWebConfig
    {
        private IOptionsSnapshot<WebConfig> optWC;

        public TestWebConfig(IOptionsSnapshot<WebConfig> optWC)
        {
            this.optWC = optWC;
        }

        public void Test()
        {
            WebConfig wc = optWC.Value;
            wc = optWC.Value;
            Console.WriteLine(wc.Conn1.ConnectionString);
            Console.WriteLine(wc.Server.Age);
            Console.WriteLine(wc.Server.Proxy.Address);
        }

        public void TestProxy()
        {
            WebConfig wc = optWC.Value;
            Console.WriteLine(string.Join(',', wc.Server.Proxy.Ids));
        }
    }

}