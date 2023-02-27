using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySeverDemo.DbContext;

/// <summary>
/// Ҫ�̳� IdentityDbContext<IdentityUser, IdentityRole, string> ��
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