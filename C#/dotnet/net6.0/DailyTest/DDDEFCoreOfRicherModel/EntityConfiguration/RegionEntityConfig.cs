using DDDEFCoreOfRicherModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDEFCoreOfRicherModel.EntityConfiguration;

public class RegionEntityConfig : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("T_Regions");

        // 在EF Core中，实体的属性可以定义为枚举类型，枚举类型的属性在数据库中默认是以整数类型来保存的。
        // EF Core中使用 HasConversion() 把枚举类型的值配置成保存为字符串。一般除非有特殊需要否则都不这么配置
        // 因为字符串在数据库中存储成本大
        //builder.Property(r => r.Level).HasConversion<string>();

        //“从属实体类型（owned entities）”：使用 Fluent API 中的 OwnsOne 等方法来配置
        builder.OwnsOne(r => r.Name, n =>
        {
            n.Property(m => m.Chinese).HasMaxLength(255).IsUnicode(true).IsRequired();
            // n 表示 Unicode 字符，var 表示可扩展
            n.Property(m => m.English).HasColumnType("varchar(255)").IsUnicode(false);
        });

        builder.OwnsOne(r => r.Area, n =>
        {
            n.Property(a => a.Value).IsRequired().HasComment("区域大小").HasColumnName("AreaSize");
            n.Property(a => a.Unit).HasConversion<string>().HasMaxLength(20).IsUnicode(false);
        });

        builder.OwnsOne(r => r.Location);
    }
}