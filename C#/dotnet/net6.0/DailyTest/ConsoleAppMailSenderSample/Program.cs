using ConfigService;
using LogService;
using MailService;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppMailSenderSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<ILogProvider, ConsoleLogProvider>();
            services.AddScoped<IConfigProvider, EnvVarConfigProvider>();
            //services.AddScoped(typeof(IConfigProvider), s => new IniFileConfigProvider { FilePath = "mail.ini"});
            services.AddIniFileConfig("mail.ini");// 通过扩展方法实现 AddXXX：用 ServiceCollection 对象可以自动提示出来，并且这种方法可以不用让服务实现类用 public 关键字修饰
            services.AddLayeredConfig();
            services.AddScoped<IMailProvider, MailProvider>();

            using (var sp = services.BuildServiceProvider())
            {
                var mailService = sp.GetRequiredService<IMailProvider>();
                mailService.Send("Hello","trump@usa.gov","Hello Trump");
            }
        }
    }
}