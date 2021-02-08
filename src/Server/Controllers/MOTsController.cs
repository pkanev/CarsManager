using System.Threading.Tasks;
using CarsManager.Application.Common.Models;
using CarsManager.Application.MOTs.Commands.CreateMOT;
using CarsManager.Application.MOTs.Commands.DeleteMOT;
using CarsManager.Application.MOTs.Commands.UpdateMOT;
using CarsManager.Application.MOTs.Queries.GetMOT;
using CarsManager.Application.MOTs.Queries.GetMOTsForVehicleWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class MOTsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MOTVm>> Get(int id)
            => await Mediator.Send(new GetMOTQuery { Id = id });

        [HttpGet("vehicle/{id}")]
        public async Task<ActionResult<PaginatedList<ListedMOTDto>>> GetForVehicle(int id, [FromQuery] GetMOTsForVehicleWithPaginationQuery query)
        {
            if (id != query.VehicleId)
                return BadRequest();

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMOTCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateMOTCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteMOTCommand { Id = id });

            return NoContent();
        }
    }
}
