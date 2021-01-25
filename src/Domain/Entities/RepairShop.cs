using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public class RepairShop : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
