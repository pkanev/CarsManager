using System;

namespace CarsManager.Application.Liabilities.Commands.CreateLiability
{
    public class CreateLiabilityCommandDto
    {
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
