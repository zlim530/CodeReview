using log4net.Config;
using System;
using System.IO;
using Topshelf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Topshelf.Runtime.Windows;

namespace TopshelfDemo
{
    class Program
    {
        // Topshelf 创建服务示例
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            HostFactory.Run(x =>
            {
                x.Service<ServiceHandler>(s =>
                {
                    s.ConstructUsing(name => new ServiceHandler());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                //以local system模式运行
                x.RunAsLocalSystem();

                /*
                //启动类型设置
                x.StartAutomatically();//自动
                x.StartAutomaticallyDelayed();// 自动（延迟启动）
                x.StartManually();//手动
                x.Disabled();//禁用
                */
                //常规信息
                //MyService服务的描述信息
                x.SetDescription("Sample Topshelf Core Host.");
                //MyService服务的显示名称
                x.SetDisplayName("TopshelfDotNetCoreDemoService");
                //MyService服务名称
                x.SetServiceName("TopshelfDotNetCoreDemoService");
                x.UseLog4Net("log4net.config");
            });
        }

        static void Main1(string[] args)
        {
            // string json = JsonConvert.SerializeObject(new { status = "y", info = "sucess"});
            // Console.WriteLine(json);
            // {"status":"y","info":"sucess"}

            var p = new Person
            {
                Age = 23,
                Name = "ZLim",
                IsMarry = false,
                Sex = "Female",
                Birthday = new DateTime(1998,5,30)
            };
            // JsonSerializerSettings jSettings = new JsonSerializerSettings();
            // jSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            // Console.WriteLine(JsonConvert.SerializeObject(p,Formatting.Indented,jSettings));

            // 使用自定义的解析类
            var jSettings = new JsonSerializerSettings
            {
                ContractResolver = new LimitPropsContractResolver(new string[] {"Age", "IsMarry"})
            };
            Console.WriteLine(JsonConvert.SerializeObject(p,Formatting.Indented,jSettings));

            Console.WriteLine(JsonConvert.SerializeObject(new TestEnum()));
        }

    }
}
