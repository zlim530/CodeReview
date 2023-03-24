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
    /// ��д SaveChangesAsync ������ʵ����ʵ���н���ע�ᣬ��������������������Ӧ�÷������ DbContext �� SaveChangesAsync ������
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // ��ȡ���к���δ�����¼���ʵ�����
        var domainEntities = this.ChangeTracker.Entries<IDomainEvents>()
                                .Where(e => e.Entity.GetDomainEvents().Any());
        // ��ȡ���д���������Ϣ
        var domainEvents = domainEntities.SelectMany(e => e.Entity.GetDomainEvents()).ToList();
        domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvents());

        foreach (var e in domainEvents)
        {
            await mediator.Publish(e);
        }
        // ����Ϣ�ķ����ŵ� SaveChangesAsync ֮ǰ�����Ա�֤�����¼���Ӧ�����е���������� base.SaveChangesAsync �еĴ��������ͬһ�������У�ʵ��ǿһ�������������Ҫ���� base.SaveChangesAsync ֮�����¼������ֶ����һ�� TransactionScope�����������¼�ʧ�� SaveChangesAsync Ҳ��ع���
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
        // �ӵ�ǰ���򼯼������е� IEntityTypeConfiguration 
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}