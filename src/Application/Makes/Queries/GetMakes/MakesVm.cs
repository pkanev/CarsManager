using System.Collections.Generic;
using CarsManager.Application.Makes.Queries.Dtos;

namespace CarsManager.Application.Makes.Queries.GetMakes
{
    public class MakesVm
    {
        public IList<MakeDto> Makes { get; set; }
    }
}
