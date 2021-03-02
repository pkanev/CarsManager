using System;

namespace CarsManager.Application.Liabilities.Commands.UpdateLiability
{
    public class UpdateLiabilityCommandDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int DurationDays { get; set; }
    }
}
