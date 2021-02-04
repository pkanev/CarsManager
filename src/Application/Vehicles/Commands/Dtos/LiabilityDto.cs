using System;

namespace CarsManager.Application.Vehicles.Commands.Dtos
{
    public class LiabilityDto
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int DurationDays { get; set; } = 365;
    }
}
