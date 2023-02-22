using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreModel;

public class MyMovieDBContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }


    public MyMovieDBContext(DbContextOptions<MyMovieDBContext> options) : base(options)
    {

    }
}