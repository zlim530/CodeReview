using DDDEFCoreOfRicherModel.Events;
using DDDEFCoreOfRicherModel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.DbContexts;

public class MyDbContext: DbContext
{
    public DbSet<User> Users { get; private set; }

    public DbSet<Region> Regions { get; private set; }

    private readonly IMediator mediator;

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        
    }

    public MyDbContext(DbContextOptions<MyDbContext> options,
        IMediator mediator) : base(options)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// 重写 SaveChangesAsync 方法：实现在实体中进行注册，真正发布在领域服务或者应用服务调用 DbContext 的 SaveChangesAsync 方法中
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 获取所有含有未发布事件的实体对象
        var domainEntities = this.ChangeTracker.Entries<IDomainEvents>()
                                .Where(e => e.Entity.GetDomainEvents().Any());
        // 获取所有待发布的消息
        var domainEvents = domainEntities.SelectMany(e => e.Entity.GetDomainEvents()).ToList();
        domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvents());

        foreach (var e in domainEvents)
        {
            await mediator.Publish(e);
        }
        // 把消息的发布放到 SaveChangesAsync 之前，可以保证领域事件响应代码中的事务操作和 base.SaveChangesAsync 中的代码操作在同一个事务中，实现强一致性事务（如果需要放在 base.SaveChangesAsync 之后发送事件可以手动添加一个 TransactionScope，这样发送事件失败 SaveChangesAsync 也会回滚）
        return await base.SaveChangesAsync(cancellationToken);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 从当前程序集加载所有的 IEntityTypeConfiguration 
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}