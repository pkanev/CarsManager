using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Towns.Queries.Dtos
{
    public class TownDto : IMapFrom<Town>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
