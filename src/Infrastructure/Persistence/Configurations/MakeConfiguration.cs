using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> builder)
        {
            builder.Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
