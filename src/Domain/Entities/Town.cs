using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public class Town : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
