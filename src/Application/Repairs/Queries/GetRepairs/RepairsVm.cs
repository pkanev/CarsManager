using System.Collections.Generic;
using CarsManager.Application.Repairs.Queries.Dtos;

namespace CarsManager.Application.Repairs.Queries.GetRepairs
{
    public class RepairsVm
    {
        public IList<RepairDto> Repairs { get; set; }
    }
}
