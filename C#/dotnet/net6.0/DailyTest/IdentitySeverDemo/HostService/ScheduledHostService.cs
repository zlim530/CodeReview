using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentitySeverDemo.HostService;

/// <summary>
/// ��פ�йܷ��񲢲���Ҫ����ļ�����ֻҪ�� ExecuteAsync �еĴ���һֱִ�м���
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
            while (!stoppingToken.IsCancellationRequested) // ������Ҫ�ر�ʱ��IsCancellationRequested ��Ϊ true���൱�� while(true) �ڳ������й����е���ѭ��
            {
                long count = await userManager.Users.LongCountAsync();
                await File.WriteAllTextAsync("d:/userc.txt", count.ToString());
                await Task.Delay(5000);// ÿ��5���ѯһ�����ݿ����û���������д�뵽�ļ���
                Console.WriteLine("��ȡд��ɹ�");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}