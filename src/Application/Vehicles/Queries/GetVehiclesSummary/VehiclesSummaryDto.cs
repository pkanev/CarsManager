namespace CarsManager.Application.Vehicles.Queries.GetVehiclesSummary
{
    public class VehiclesSummaryDto
    {
        public int InUse { get; set; }
        public int Blocked { get; set; }
        public int Total { get; set; }
    }
}
