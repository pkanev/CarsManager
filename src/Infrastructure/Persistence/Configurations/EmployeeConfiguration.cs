﻿using CarsManager.Application.Common.Constants;
using CarsManager.Application.Employees;
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
                .HasMaxLength(EmployeeConstants.NAME_MAX_LENGTH)
                .IsRequired();

            builder.Property(e => e.MiddleName)
                .HasMaxLength(EmployeeConstants.NAME_MAX_LENGTH);

            builder.Property(e => e.Surname)
                .HasMaxLength(EmployeeConstants.NAME_MAX_LENGTH)
                .IsRequired();
        }
    }
}
