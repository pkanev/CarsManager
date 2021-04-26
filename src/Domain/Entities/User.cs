using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<UserRole> Roles { get; private set; } = new HashSet<UserRole>();
    }
}
