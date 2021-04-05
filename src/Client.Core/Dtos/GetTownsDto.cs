using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class GetTownsDto
    {
        public IList<TownModel> Towns { get; set; } = new List<TownModel>();
    }
}
