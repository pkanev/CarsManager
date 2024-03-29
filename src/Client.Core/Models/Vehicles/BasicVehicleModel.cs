﻿namespace Client.Core.Models.Vehicles
{
    public class BasicVehicleModel
    {
        public int Id { get; set; }
        public string LicencePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public int ActiveRecordEntryId { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsBlocked { get; set; }
    }
}
