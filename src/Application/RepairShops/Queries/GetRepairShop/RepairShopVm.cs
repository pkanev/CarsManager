using System.Collections.Generic;
using CarsManager.Application.Repairs.Queries.Dtos;
using CarsManager.Application.RepairShops.Queries.Dtos;

namespace CarsManager.Application.RepairShops.Queries.GetRepairShop
{
    public class RepairShopVm
    {
        public RepairShopDto RepairShop { get; set; }

        public IList<RepairDto> Repairs { get; set; }
    }
}
