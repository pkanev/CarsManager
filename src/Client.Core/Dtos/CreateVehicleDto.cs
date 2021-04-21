using Client.Core.Models.Liabilities;

namespace Client.Core.Dtos
{
    public class CreateVehicleDto : VehicleDto
    {
        public LiabilityModel MOT { get; set; } = new LiabilityModel();
        public LiabilityModel CivilLiability { get; set; } = new LiabilityModel();
        public LiabilityModel CarInsurance { get; set; } = new LiabilityModel();
        public LiabilityModel Vignette { get; set; }

    }
}
