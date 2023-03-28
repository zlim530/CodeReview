using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserMgr.Infrastracture.DbContexts
{
    public class UserDbContextDesginTimeFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UserDbContext>();
            var connStr = "Server=127.0.0.1;Database=DemoDDD;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
            builder.UseSqlServer(connStr);
            var ctx = new UserDbContext(builder.Options);
            return ctx;
        }
    }
}
