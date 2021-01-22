using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Makes.Queries.Dtos
{
    public class MakeDto : IMapFrom<Make>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
