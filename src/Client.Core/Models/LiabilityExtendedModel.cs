namespace Client.Core.Models
{
    public class LiabilityExtendedModel : LiabilityModel
    {
        public int VehicleId { get; set; }
        public string LicencePlate { get; set; }
        public LiabilityType LiabilityType { get; set; }
    }
}
