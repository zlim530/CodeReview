namespace Application.Implement;
public class DataStoreContext
{
    public QueryDbContext QueryContext { get; init; }
    public CommandDbContext CommandContext { get; init; }

    public QuerySet<Blog> BlogQuery { get; init; }
    public QuerySet<SystemRole> SystemRoleQuery { get; init; }
    public QuerySet<SystemUser> SystemUserQuery { get; init; }
    public CommandSet<Blog> BlogCommand { get; init; }
    public CommandSet<SystemRole> SystemRoleCommand { get; init; }
    public CommandSet<SystemUser> SystemUserCommand { get; init; }


    /// <summary>
    /// 绑在对象
    /// </summary>
    private readonly Dictionary<string, object> SetCache = new();

    public DataStoreContext(
        BlogQueryStore blogQuery,
        SystemRoleQueryStore systemRoleQuery,
        SystemUserQueryStore systemUserQuery,
        BlogCommandStore blogCommand,
        SystemRoleCommandStore systemRoleCommand,
        SystemUserCommandStore systemUserCommand,

        QueryDbContext queryDbContext,
        CommandDbContext commandDbContext
    )
    {
        QueryContext = queryDbContext;
        CommandContext = commandDbContext;
        BlogQuery = blogQuery;
        AddCache(BlogQuery);
        SystemRoleQuery = systemRoleQuery;
        AddCache(SystemRoleQuery);
        SystemUserQuery = systemUserQuery;
        AddCache(SystemUserQuery);
        BlogCommand = blogCommand;
        AddCache(BlogCommand);
        SystemRoleCommand = systemRoleCommand;
        AddCache(SystemRoleCommand);
        SystemUserCommand = systemUserCommand;
        AddCache(SystemUserCommand);

    }

    public async Task<int> SaveChangesAsync()
    {
        return await CommandContext.SaveChangesAsync();
    }

    public QuerySet<TEntity> QuerySet<TEntity>() where TEntity : EntityBase
    {
        var typename = typeof(TEntity).Name + "QueryStore";
        var set = GetSet(typename);
        if (set == null) throw new ArgumentNullException($"{typename} class object not found");
        return (QuerySet<TEntity>)set;
    }
    public CommandSet<TEntity> CommandSet<TEntity>() where TEntity : EntityBase
    {
        var typename = typeof(TEntity).Name + "CommandStore";
        var set = GetSet(typename);
        if (set == null) throw new ArgumentNullException($"{typename} class object not found");
        return (CommandSet<TEntity>)set;
    }

    private void AddCache(object set)
    {
        var typeName = set.GetType().Name;
        if (!SetCache.ContainsKey(typeName))
        {
            SetCache.Add(typeName, set);
        }
    }

    private object GetSet(string type)
    {
        return SetCache[type];
    }
}
