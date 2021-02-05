using System.Collections.Generic;
using CarsManager.Application.Towns.Queries.Dtos;

namespace CarsManager.Application.Towns.Queries.GetTowns
{
    public class TownsVm
    {
        public IList<TownDto> Towns { get; set; }
    }
}
