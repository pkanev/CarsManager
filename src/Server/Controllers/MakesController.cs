using System.Threading.Tasks;
using CarsManager.Application.Makes.Commands.CreateMake;
using CarsManager.Application.Makes.Commands.DeleteMake;
using CarsManager.Application.Makes.Commands.UpdateMake;
using CarsManager.Application.Makes.Queries.GetMake;
using CarsManager.Application.Makes.Queries.GetMakes;
using CarsManager.Application.Makes.Queries.GetMakesForVehicleType;
using CarsManager.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class MakesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<MakesVm>> Get()
            => await Mediator.Send(new GetMakesQuery());

        [HttpGet("{id}")]
        public async Task<ActionResult<MakeVm>> Get(int id)
            => await Mediator.Send(new GetMakeQuery { Id = id });

        [HttpGet("type/{vehicleType}")]
        public async Task<ActionResult<MakesVm>> GetForVehicleType(VehicleType vehicleType)
            => await Mediator.Send(new GetMakesForVehicleTypeQuery { VehicleType = vehicleType});

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMakeCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateMakeCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteMakeCommand { Id = id });

            return NoContent();
        }
    }
}
