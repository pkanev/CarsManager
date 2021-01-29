using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(m => new
                {
                    m.MakeId,
                    m.Name
                })
                .IsUnique();
        }
    }
}
