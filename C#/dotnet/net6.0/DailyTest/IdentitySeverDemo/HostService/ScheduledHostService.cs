using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentitySeverDemo.HostService;

/// <summary>
/// 常驻托管服务并不需要特殊的技术，只要让 ExecuteAsync 中的代码一直执行即可
/// </summary>
public class ScheduledHostService : BackgroundService
{
    private IServiceScope serviceScope;

    public ScheduledHostService(IServiceScopeFactory serviceProviderFactory)
    {
        serviceScope = serviceProviderFactory.CreateScope();
    }

    public override void Dispose()
    {
        this.serviceScope.Dispose();
        base.Dispose();
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<MyUser>>();
            while (!stoppingToken.IsCancellationRequested) // 当程序要关闭时，IsCancellationRequested 才为 true，相当于 while(true) 在程序运行过程中的死循环
            {
                long count = await userManager.Users.LongCountAsync();
                await File.WriteAllTextAsync("d:/userc.txt", count.ToString());
                await Task.Delay(5000);// 每隔5秒查询一下数据库中用户的数量并写入到文件中
                Console.WriteLine("读取写入成功");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}