using System;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Commands.Dtos
{
    public class LiabilityDto :IMapFrom<Liability>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;
    }
}
