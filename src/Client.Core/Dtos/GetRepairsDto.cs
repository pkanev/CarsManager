using System.Collections.Generic;
using Client.Core.Models.Repairs;

namespace Client.Core.Dtos
{
    public class GetRepairsDto
    {
        public IList<RepairModel> Repairs { get; set; }
    }
}
