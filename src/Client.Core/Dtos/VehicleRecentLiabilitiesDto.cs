using System;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class VehicleRecentLiabilitiesDto
    {
        public int Id { get; set; }
        public LiabilityModel LastMOT { get; set; }
        public LiabilityModel LastCivilLiability { get; set; }
        public LiabilityModel LastCarInsurance { get; set; }
        public LiabilityModel LastVignette { get; set; }
    }
}
