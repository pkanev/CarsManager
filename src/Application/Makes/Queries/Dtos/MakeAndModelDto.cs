using System.Collections.Generic;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Models.Queries.Dtos;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Makes.Queries.Dtos
{
    public class MakeAndModelDto : IMapFrom<Make>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ModelDto> Models { get; private set; } = new HashSet<ModelDto>();
    }
}
