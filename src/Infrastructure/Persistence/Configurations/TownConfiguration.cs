using CarsManager.Application.Common.Constants;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(TownConstants.NAME_MAX_LENGTH)
                .IsRequired();
        }
    }
}
