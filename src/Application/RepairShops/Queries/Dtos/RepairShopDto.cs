using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.RepairShops.Queries.Dtos
{
    public class RepairShopDto : IMapFrom<RepairShop>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
