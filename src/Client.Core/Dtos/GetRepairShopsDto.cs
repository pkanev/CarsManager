using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class GetRepairShopsDto
    {
        public IList<RepairShopModel> RepairShops { get; set; } = new List<RepairShopModel>();
    }
}
