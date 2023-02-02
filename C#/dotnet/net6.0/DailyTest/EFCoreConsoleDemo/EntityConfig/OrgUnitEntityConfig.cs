using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleDemo
{
    public class OrgUnitEntityConfig : IEntityTypeConfiguration<OrgUnit>
    {
        public void Configure(EntityTypeBuilder<OrgUnit> builder)
        {
            builder.ToTable("T_OrgUnits");

            builder.Property(o => o.Name).IsUnicode().IsRequired().HasMaxLength(255);

            builder.HasOne<OrgUnit>(o => o.Parent).WithMany(o => o.Children);//���ڵ�û�� Parent����������ϵ��������Ϊ IsRequired() ��
        }
    }
}