﻿using CarsManager.Application.Vehicles.Commands.CreateVehicle;
using CarsManager.Application.Vehicles.Commands.DeleteVehicle;
using CarsManager.Application.Vehicles.Commands.UpdateVehicle;
using CarsManager.Application.Vehicles.Queries.GetVehicle;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarsManager.Server.Controllers
{
    public class VehiclesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleVm>> Get(int id)
            => await Mediator.Send(new GetVehicleQuery { Id = id });

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateVehicleCommand command)
            => await Mediator.Send(command);

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateVehicleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteVehicleCommand { Id = id });

            return NoContent();
        }


    }
}
