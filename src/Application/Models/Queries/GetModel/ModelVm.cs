using System.Collections.Generic;
using CarsManager.Application.Models.Queries.Dtos;

namespace CarsManager.Application.Models.Queries.GetModel
{
    public class ModelVm
    {
        public ModelDto Model { get; set; }
        public IList<VehicleForModelDto> Vehicles { get; set; }
    }
}
