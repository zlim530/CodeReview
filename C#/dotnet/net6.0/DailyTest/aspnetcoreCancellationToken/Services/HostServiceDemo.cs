
namespace aspnetcoreCancellationToken.Services;

public class HostServiceDemo : BackgroundService
{
    private IServiceScope serviceScope;

    /// <summary>
    /// 因为 HostService 是单例的，因此无法通过 DI 注入瞬时或者 Scope 的服务，如果需要注入 Scope 的服务可以通过 IServiceScopeFactory.CreateScope() 方法创建，注意一定要同时实现 Dispose 方法
    /// </summary>
    /// <param name="serviceProviderFactory"></param>
    public HostServiceDemo(IServiceScopeFactory serviceProviderFactory)
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
        var service = serviceScope.ServiceProvider.GetService<TestScopeService>();
        var sum = service.Add(1,1);
        Console.WriteLine("HostServiceDemo 启动");
        Console.WriteLine(sum);
        await Task.Delay(3000);// 等待3秒：注意在异步中等待不要使用 Sleep
        string txt = await File.ReadAllTextAsync("1.txt");
        Console.WriteLine("文件读取完成");
        await Task.Delay(3000);// 等待3秒：注意在异步中等待不要使用 Sleep
        Console.WriteLine(txt);
    }
}