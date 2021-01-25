using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class RepairShopConfiguration : IEntityTypeConfiguration<RepairShop>
    {
        public void Configure(EntityTypeBuilder<RepairShop> builder)
        {
            builder.Property(rs => rs.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
