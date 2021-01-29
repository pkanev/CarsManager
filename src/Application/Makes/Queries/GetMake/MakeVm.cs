using System.Collections.Generic;
using CarsManager.Application.Makes.Queries.Dtos;
using CarsManager.Application.Models.Queries.Dtos;

namespace CarsManager.Application.Makes.Queries.GetMake
{
    public class MakeVm
    {
        public MakeDto Make { get; set; }
        public IList<ModelDto> Models { get; set; }
    }
}
