using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreConsoleDemo
{

    public class LeaveEntityConfig : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            //���򵼺��������ã�User �� Leave ��һ�Զ࣬�� Leave ���ж���ֶ����� User �� Id
            builder.ToTable("T_Leaves")
                .HasOne<User>(l => l.Requester).WithMany().IsRequired();

            builder.HasOne<User>(l => l.Approver).WithMany();

            builder.Property(l => l.Remarks).HasMaxLength(500).IsRequired();
        }
    }
}