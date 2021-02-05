using System.Threading.Tasks;
using CarsManager.Application.Towns.Commands.CreateTown;
using CarsManager.Application.Towns.Commands.DeleteTown;
using CarsManager.Application.Towns.Commands.UpdateTown;
using CarsManager.Application.Towns.Queries.Dtos;
using CarsManager.Application.Towns.Queries.GetTown;
using CarsManager.Application.Towns.Queries.GetTowns;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class TownsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<TownsVm> Get()
            => await Mediator.Send(new GetTownsQuery());

        [HttpGet("{id}")]
        public async Task<TownDto> Get(int id)
            => await Mediator.Send(new GetTownQuery { Id = id});

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTownCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateTownCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTownCommand { Id = id });

            return NoContent();
        }
    }
}
