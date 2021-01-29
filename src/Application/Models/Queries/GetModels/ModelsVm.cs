using System.Collections.Generic;
using CarsManager.Application.Models.Queries.Dtos;

namespace CarsManager.Application.Models.Queries.GetModels
{
    public class ModelsVm
    {
        public IList<ModelDto> Models { get; set; }
    }
}
