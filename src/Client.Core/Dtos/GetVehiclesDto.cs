using System.Collections.Generic;

namespace Client.Core.Dtos
{
    public class GetVehiclesDto
    {
        public IList<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
    }
}
