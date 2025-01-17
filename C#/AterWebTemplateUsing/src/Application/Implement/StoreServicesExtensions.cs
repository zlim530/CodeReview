namespace Application.Implement;

public static class StoreServicesExtensions
{
    public static void AddDataStore(this IServiceCollection services)
    {
        services.AddTransient<IUserContext, UserContext>();
        services.AddScoped(typeof(DataStoreContext));
        services.AddScoped(typeof(BlogQueryStore));
        services.AddScoped(typeof(SystemRoleQueryStore));
        services.AddScoped(typeof(SystemUserQueryStore));
        services.AddScoped(typeof(BlogCommandStore));
        services.AddScoped(typeof(SystemRoleCommandStore));
        services.AddScoped(typeof(SystemUserCommandStore));

    }

    public static void AddManager(this IServiceCollection services)
    {
        services.AddScoped<IBlogManager, BlogManager>();
        services.AddScoped<ISystemRoleManager, SystemRoleManager>();
        services.AddScoped<ISystemUserManager, SystemUserManager>();

    }
}
