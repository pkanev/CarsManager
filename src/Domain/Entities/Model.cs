using CarsManager.Domain.Common;
using CarsManager.Domain.Enums;

namespace CarsManager.Domain.Entities
{
    public class Model : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public Make Make { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
