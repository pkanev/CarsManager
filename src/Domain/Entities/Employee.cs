using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class Employee
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
        public string Image { get; set; }

        public ICollection<Vehicle> Vehicles { get; private set; } = new HashSet<Vehicle>();
        public ICollection<RoadBookEntry> RoadBookEntries { get; private set; } = new HashSet<RoadBookEntry>();
        public ICollection<RoadBookEntry> ActiveRecords { get; private set; } = new HashSet<RoadBookEntry>();
    }
}
