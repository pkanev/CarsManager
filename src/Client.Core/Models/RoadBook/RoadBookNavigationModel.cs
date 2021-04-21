using System;
using System.Threading.Tasks;

namespace Client.Core.Models.RoadBook
{
    public class RoadBookNavigationModel
    {
        public int Id { get; set; }
        public Func<RoadBookBasicEntryModel, Task> Callback { get; set; }
    }
}
