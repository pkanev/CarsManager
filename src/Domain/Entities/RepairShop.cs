using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class RepairShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Repair> Repairs { get; set; } = new HashSet<Repair>();
    }
}
