using CarsManager.Domain.Enums;

namespace CarsManager.Application.Issues.Queries
{
    public class IssueReportDto
    {
        public string LicencePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public int MileageAtRepair { get; set; }
        public int IssueType { get; set; }
    }
}
