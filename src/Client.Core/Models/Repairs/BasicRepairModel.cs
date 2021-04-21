using System;
namespace Client.Core.Models.Repairs
{
    public class BasicRepairModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RepairShopId { get; set; }
        public string RepairShopName { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Description { get; set; }
    }
}
