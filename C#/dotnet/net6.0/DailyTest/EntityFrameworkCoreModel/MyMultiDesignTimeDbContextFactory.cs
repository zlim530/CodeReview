using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkCoreModel;

public class MyMultiDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyMultiDBContext>
{
    // 开发时（执行 Add-Migration、Update-Database 等命令）使用，项目真正运行时不会需要这个类
    public MyMultiDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyMultiDBContext>();
        //var connStr = Environment.GetEnvironmentVariable("ConnStr"); // 或者使用环境变量的方式读取数据库连接字符串
        var connStr = "Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
        builder.UseSqlServer(connStr);
        var ctx = new MyMultiDBContext(builder.Options);
        return ctx;
    }
}