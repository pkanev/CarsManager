using System.Threading.Tasks;
using CarsManager.Application.Models.Commands.CreateModel;
using CarsManager.Application.Models.Commands.DeleteModel;
using CarsManager.Application.Models.Commands.UpdateModel;
using CarsManager.Application.Models.Queries.GetModel;
using CarsManager.Application.Models.Queries.GetModels;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class ModelsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelVm>> Get(int id)
            => await Mediator.Send(new GetModelQuery { Id = id });

        [HttpGet("make/{id}")]
        public async Task<ActionResult<ModelsVm>> GetModelsForMake(int id)
            => await Mediator.Send(new GetModelsQuery { Id = id });

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateModelCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateModelCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteModelCommand { Id = id });

            return NoContent();
        }
    }
}
