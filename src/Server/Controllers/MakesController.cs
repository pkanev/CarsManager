using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Makes.Commands.CreateMake;
using CarsManager.Application.Makes.Commands.DeleteMake;
using CarsManager.Application.Makes.Commands.UpdateMake;
using CarsManager.Application.Makes.Queries.GetMakes;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class MakesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<MakesVm>> Get()
        {
            return await Mediator.Send(new GetMakesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMakeCommand command)
        {
            return await Mediator.Send(command);
        }

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
