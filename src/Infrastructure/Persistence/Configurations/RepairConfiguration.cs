using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class RepairConfiguration : IEntityTypeConfiguration<Repair>
    {
        private readonly IDateTime dateTime;

        public RepairConfiguration(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public void Configure(EntityTypeBuilder<Repair> builder)
        {
            builder.Property(r => r.Date)
                .HasDefaultValue(dateTime.Today);
        }
    }
}
