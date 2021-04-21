using System.Collections.Generic;

namespace Client.Core.Models.RoadBook
{
    public class RoadBookEntryModel : RoadBookBasicEntryModel
    {
        public string MakeAndModel { get; set; }
        public string Color { get; set; }
        public List<string> Employees { get; set; } = new List<string>();
        public bool IsFullyCheckedIn { get; set; }
        public string Drivers => string.Join(", ", Employees);
    }
}
