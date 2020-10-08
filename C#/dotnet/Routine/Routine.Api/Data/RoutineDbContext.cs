using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Data {
    public class RoutineDbContext:DbContext {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options):base(options) {

        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);// 即当要删除此表时如果另外一张表有数据则不允许删除


            // 添加种子数据：这里只添加了Company数据
            modelBuilder.Entity<Company>().HasData(
                new Company {
                    Id = Guid.Parse("e2e02b30-f5d0-466b-b657-5e23c8d5632a"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
               new Company {
                   Id = Guid.Parse("ba3a0e24-f5bb-4311-9414-d8f411280fa0"),
                   Name = "Goole",
                   Introduction = "No Evil Company ... "
               },
               new Company {
                   Id = Guid.Parse("6fbcec37-6774-4b94-93d4-ddd29ba87779"),
                   Name = "Alipapa",
                   Introduction = "Fubao Company"
               }
            );
        }
    }
}
