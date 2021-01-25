using System.Collections.Generic;
using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public Town Town { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public byte[] Photo { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
    }
}
