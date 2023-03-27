using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMgr.Domain.Entities;

namespace UserMgr.Infrastracture.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");

            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(pn => pn.RegionCode).HasMaxLength(5).IsUnicode(false);
                nb.Property(pn => pn.Number).HasMaxLength(20).IsUnicode(false);
            });

            // 把私有成员变量映射到数据表中的列。
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);

            builder.HasOne(x => x.AccessFail).WithOne(x => x.User).HasForeignKey<UserAccessFail>(x => x.UserId);
        }
    }
}
