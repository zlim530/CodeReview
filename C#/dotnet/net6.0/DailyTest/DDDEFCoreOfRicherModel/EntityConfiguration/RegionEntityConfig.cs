using DDDEFCoreOfRicherModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDEFCoreOfRicherModel.EntityConfiguration;

public class RegionEntityConfig : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("T_Regions");

        // ��EF Core�У�ʵ������Կ��Զ���Ϊö�����ͣ�ö�����͵����������ݿ���Ĭ��������������������ġ�
        // EF Core��ʹ�� HasConversion() ��ö�����͵�ֵ���óɱ���Ϊ�ַ�����һ�������������Ҫ���򶼲���ô����
        // ��Ϊ�ַ��������ݿ��д洢�ɱ���
        //builder.Property(r => r.Level).HasConversion<string>();

        //������ʵ�����ͣ�owned entities������ʹ�� Fluent API �е� OwnsOne �ȷ���������
        builder.OwnsOne(r => r.Name, n =>
        {
            n.Property(m => m.Chinese).HasMaxLength(255).IsUnicode(true).IsRequired();
            // n ��ʾ Unicode �ַ���var ��ʾ����չ
            n.Property(m => m.English).HasColumnType("varchar(255)").IsUnicode(false);
        });

        builder.OwnsOne(r => r.Area, n =>
        {
            n.Property(a => a.Value).IsRequired().HasComment("�����С").HasColumnName("AreaSize");
            n.Property(a => a.Unit).HasConversion<string>().HasMaxLength(20).IsUnicode(false);
        });

        builder.OwnsOne(r => r.Location);
    }
}