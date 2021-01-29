using System.Collections.Generic;
using CarsManager.Application.Models.Queries.Dtos;
using CarsManager.Application.Vehicles;

namespace CarsManager.Application.Models.Queries.GetModel
{
    public class ModelVm
    {
        public ModelDto Model { get; set; }
        public IList<VehicleDto> Vehicles { get; set; }
    }
}
