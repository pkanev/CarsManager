using CarsManager.Application.Common.Constants;
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
                .HasMaxLength(MakeConstants.NAME_MAX_LENGTH)
                .IsRequired();
            builder.HasIndex(m => m.Name)
                .IsUnique();
        }
    }
}
