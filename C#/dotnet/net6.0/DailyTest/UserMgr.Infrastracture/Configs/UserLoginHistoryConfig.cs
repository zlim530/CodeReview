using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMgr.Domain.Entities;

namespace UserMgr.Infrastracture.Configs
{
    public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("T_UserLoginHistories");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(pn => pn.RegionCode).HasMaxLength(5).IsUnicode(false);
                nb.Property(pn => pn.Number).HasMaxLength(20).IsUnicode(false);
            });
            /*
            UserLoginHistory 中有 UserId 属性，但是我们并没有在 EFCore 中实际创建这两个表之间的外键关系：因为 UserLoginHistory 属于另一个聚合根，以后可能会迁移到另一个微服务中，而在 EFCore 中对于实体之间的外键关系是强制生成的，如果想在 EFCore 中不使用外键：
            1）使用SQL脚本删除某一张表的所有外键
            2）使用DDD，不在实体类的配置类中配置外键关系，但是建议聚合内的外键配置保留（比如 UserConfig 中的 AccessFail 的配置），因此不同聚合间是自然没有外键的（指没有在 EFCore 中配置外键关系，但实际上我们引用别的聚合根的唯一标识符在另外的一个聚合根中，这就是我们所说的外键）
            */
            builder.Property("Message").HasMaxLength(200);
        }
    }
}
