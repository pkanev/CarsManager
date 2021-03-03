using System.Threading.Tasks;
using CarsManager.Application.RepairShops.Commands.CreateRepairShop;
using CarsManager.Application.RepairShops.Commands.DeleteRepairShop;
using CarsManager.Application.RepairShops.Commands.UpdateRepairShop;
using CarsManager.Application.RepairShops.Queries.GetRepairShop;
using CarsManager.Application.RepairShops.Queries.GetRepairShops;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class RepairShopsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<RepairShopsVm>> Get()
            => await Mediator.Send(new GetRepairShopsQuery());

        [HttpGet("{id}")]
        public async Task<ActionResult<RepairShopVm>> Get(int id)
            => await Mediator.Send(new GetRepairShopQuery { Id = id });

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateRepairShopCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateRepairShopCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRepairShopCommand { Id = id });

            return NoContent();
        }
    }
}
