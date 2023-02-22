using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkCoreModel;

public class MyMovieDesignTimeFactory : IDesignTimeDbContextFactory<MyMovieDBContext>
{
    public MyMovieDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyMovieDBContext>();
        var connStr = "Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
        builder.UseSqlServer(connStr);
        var ctx = new MyMovieDBContext(builder.Options);
        return ctx;
    }
}