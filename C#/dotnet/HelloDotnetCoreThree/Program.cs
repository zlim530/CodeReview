using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HelloDotnetCoreThree {
    public class Program {

        // 在 ASP .NET Core 中，将 Program.cs 文件的 Main() 方法作为应用的入口（这点和 Console 项目、Winform 项目是一致的），当你启动你的 web 应用时这个方法必须存在。在 ASP .NET Core 3.0应用中，它被创建和运行一个 IHostBuilder 实例，如下面的代码所示：
        // 【注意】：
        //  .NET Core 2.x的Program.cs是创建一个IWebHost，而3.0创建的是一个IHostBuilder
        //  .NET Core 2.x的IWebHost的命名空间是Microsoft.AspNetCore.Hosting，而3.0的IHostBuilder命名空间是Microsoft.Extensions.Hosting

        public static void Main(string[] args) {
            // CreateHostBuilder(args).Build() 相当于把一个控制台应用变为了 Asp .Net Core 应用
            CreateHostBuilder(args).Build().Run();
        }

        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
        // CreateHostBuilder() 介绍：
        // 从环境变量中加载“DOTNET_”前缀的配置
        // 从命令行参数中加载配置
        // 从“appsetting.json”和“appsettings.[环境名].json”加载配置
        // 如果[环境名] 时“Development”时从用户机密（User Secrets）加载配置
        // 从环境变量加载配置
        // 配置ILoggerFactory记录日志
        // 当[环境名] 为“Development”时在依赖注入容器中启用范围验证（scope validation）

        // ConfigureWebHostDefaults() 介绍
        /*
         将Kestrel作为Web服务器，并使用应用的配置提供程序对其进行配置
         添加主机筛选中间件
         如果 ASPNETCORE_FORWARDEDHEADERS_ENABLED 环境变量设置为true，则添加转发标头中间件
         启动IIS集成
         */

        // 关于Web服务器 Kestrel 的介绍： 
        // ASP.NET Core 的 Web 服务器默认采用Kestrel，这是一个跨平台、轻量级的Web服务器。
        // 在开始之前，先回顾一下.NET Core 3.0默认的main()方法模板中，我们会调用Host.CreateDefaultBuilder方法，该方法的主要功能是配置应用主  机及设置主机的属性，设置Kestrel 服务器配置为 Web 服务器，另外还包括日志功能、应用配置加载等等。
        // 作为一个轻量级的Web Server，它并没有IIS、Apache那些大而全的功能，但它依然可以单独运行，也可以搭配IIS、Apache等反向代理服务器结合使用。
    }
}
