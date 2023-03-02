
namespace aspnetcoreCancellationToken.Services;

public class HostServiceDemo : BackgroundService
{
    private IServiceScope serviceScope;

    /// <summary>
    /// ��Ϊ HostService �ǵ����ģ�����޷�ͨ�� DI ע��˲ʱ���� Scope �ķ��������Ҫע�� Scope �ķ������ͨ�� IServiceScopeFactory.CreateScope() ����������ע��һ��Ҫͬʱʵ�� Dispose ����
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
        Console.WriteLine("HostServiceDemo ����");
        Console.WriteLine(sum);
        await Task.Delay(3000);// �ȴ�3�룺ע�����첽�еȴ���Ҫʹ�� Sleep
        string txt = await File.ReadAllTextAsync("1.txt");
        Console.WriteLine("�ļ���ȡ���");
        await Task.Delay(3000);// �ȴ�3�룺ע�����첽�еȴ���Ҫʹ�� Sleep
        Console.WriteLine(txt);
    }
}