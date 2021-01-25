using System;

namespace CarsManager.Domain.Entities
{
    public class Repair
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int RepairShopId { get; set; }
        public RepairShop RepairShop { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
        public bool IsOilChanged { get; set; }
        public bool IsBeltChanged { get; set; }
        public bool IsBatteryChanged { get; set; }
        public bool IsSparkPlugChanged { get; set; }
        public bool IsFuelFilterChanged { get; set; }
        public bool IsBrakeLiningsChanged { get; set; }
        public bool IsBrakeDisksChanged { get; set; }
        public bool IsCoolantChanged { get; set; }
        public bool IsOtherWorkDone { get; set; }
        public string Description { get; set; }
        public decimal InitialPrice { get; set; }
    }
}
