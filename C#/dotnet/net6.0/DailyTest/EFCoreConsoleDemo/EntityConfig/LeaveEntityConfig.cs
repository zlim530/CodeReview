using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreConsoleDemo
{

    public class LeaveEntityConfig : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            //单向导航属性配置：User 与 Leave 是一对多，且 Leave 中有多个字段引用 User 的 Id
            builder.ToTable("T_Leaves")
                .HasOne<User>(l => l.Requester).WithMany().IsRequired();

            builder.HasOne<User>(l => l.Approver).WithMany();

            builder.Property(l => l.Remarks).HasMaxLength(500).IsRequired();
        }
    }
}