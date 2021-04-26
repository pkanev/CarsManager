using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class UserRole
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
