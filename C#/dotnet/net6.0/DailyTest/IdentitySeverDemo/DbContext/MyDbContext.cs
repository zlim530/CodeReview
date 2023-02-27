using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySeverDemo.DbContext;

/// <summary>
/// Òª¼Ì³Ð IdentityDbContext<IdentityUser, IdentityRole, string> Àà
/// </summary>
public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
{
	public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
	{

	}

	public MyDbContext()
	{

	}
}