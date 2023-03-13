using DDDEFCoreOfRicherModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDEFCoreOfRicherModel.EntityConfiguration;

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 把私有成员变量映射到数据表中的列。
        builder.Property("passwordHash");
        // 属性是只读的，也就是它的值是从数据库中读取出来的，但是我们不能修改属性值。
        builder.Property(b => b.Remark).HasField("remark");
        // builder.Ignore有的属性不需要映射到数据列，仅在运行时被使用。
        builder.Ignore(u => u.Tag);

        builder.ToTable("T_Users");
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
    }
}