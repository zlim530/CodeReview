using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkCoreModel;

public class MyMultiDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyMultiDBContext>
{
    // ����ʱ��ִ�� Add-Migration��Update-Database �����ʹ�ã���Ŀ��������ʱ������Ҫ�����
    public MyMultiDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyMultiDBContext>();
        //var connStr = Environment.GetEnvironmentVariable("ConnStr"); // ����ʹ�û��������ķ�ʽ��ȡ���ݿ������ַ���
        var connStr = "Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
        builder.UseSqlServer(connStr);
        var ctx = new MyMultiDBContext(builder.Options);
        return ctx;
    }
}