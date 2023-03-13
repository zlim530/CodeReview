using DDDEFCoreOfRicherModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDEFCoreOfRicherModel.EntityConfiguration;

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // ��˽�г�Ա����ӳ�䵽���ݱ��е��С�
        builder.Property("passwordHash");
        // ������ֻ���ģ�Ҳ��������ֵ�Ǵ����ݿ��ж�ȡ�����ģ��������ǲ����޸�����ֵ��
        builder.Property(b => b.Remark).HasField("remark");
        // builder.Ignore�е����Բ���Ҫӳ�䵽�����У���������ʱ��ʹ�á�
        builder.Ignore(u => u.Tag);

        builder.ToTable("T_Users");
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
    }
}