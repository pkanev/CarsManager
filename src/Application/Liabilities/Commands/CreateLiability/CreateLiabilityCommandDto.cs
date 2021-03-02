using System;

namespace CarsManager.Application.Liabilities.Commands.CreateLiability
{
    public class CreateLiabilityCommandDto
    {
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int DurationDays { get; set; }
    }
}
