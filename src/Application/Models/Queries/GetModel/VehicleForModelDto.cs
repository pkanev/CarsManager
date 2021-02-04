using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Models.Queries.GetModel
{
    public class VehicleForModelDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public string LicencePlate { get; set; }
    }
}
