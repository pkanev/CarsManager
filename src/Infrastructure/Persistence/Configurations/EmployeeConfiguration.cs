using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsManager.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.GivenName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.MiddleName)
                .HasMaxLength(100);

            builder.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
