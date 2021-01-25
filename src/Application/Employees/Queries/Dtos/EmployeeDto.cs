﻿using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Employees.Queries.Dtos
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public byte[] Photo { get; set; }
        public int? VehicleId { get; set; }
    }
}