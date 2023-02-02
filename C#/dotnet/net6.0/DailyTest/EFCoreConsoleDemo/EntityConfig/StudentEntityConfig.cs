using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleDemo
{
    public class StduentEntityConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("T_Students");

            builder.Property(d => d.Name).IsUnicode().HasMaxLength(100).IsRequired();

            builder.HasMany<Teacher>(s => s.Teachers).WithMany(t => t.Students).UsingEntity(j => j.ToTable("T_Students_Teachers"));
        }
    }
}