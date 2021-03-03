using System.Collections.Generic;
using CarsManager.Application.RepairShops.Queries.Dtos;

namespace CarsManager.Application.RepairShops.Queries.GetRepairShops
{
    public class RepairShopsVm
    {
        public IList<RepairShopDto> RepairShops { get; set; }
    }
}
