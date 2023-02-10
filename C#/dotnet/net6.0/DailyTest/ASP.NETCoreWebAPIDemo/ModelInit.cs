using Zack.Commons;

namespace ASP.NETCoreWebAPIDemo;

public class ModelInit : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<MyServices>();
        services.AddScoped<LongTimeServices>();
    }
}