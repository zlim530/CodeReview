using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleDemo
{
    public class DeliveryEntityConfig : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("T_Deliverys");

            builder.Property(d => d.CompanyName).IsUnicode().HasMaxLength(100).IsRequired();
            builder.Property(d => d.Number).IsUnicode().HasMaxLength(100).IsRequired();

            builder.HasOne<Order>(d => d.Order).WithOne(d => d.Delivery).HasForeignKey<Delivery>(o => o.OrderId);
        }
    }
}