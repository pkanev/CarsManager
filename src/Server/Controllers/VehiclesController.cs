using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Vehicles.Commands.CreateVehicle;
using CarsManager.Application.Vehicles.Commands.DeleteVehicle;
using CarsManager.Application.Vehicles.Commands.UpdateMileage;
using CarsManager.Application.Vehicles.Commands.UpdateVehicle;
using CarsManager.Application.Vehicles.Queries.GetAllBasicVehicles;
using CarsManager.Application.Vehicles.Queries.GetVehicle;
using CarsManager.Application.Vehicles.Queries.GetVehicleExtended;
using CarsManager.Application.Vehicles.Queries.GetVehiclesQuery;
using CarsManager.Application.Vehicles.Queries.GetVehiclesSummary;
using CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination;
using CarsManager.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server;

namespace CarsManager.Server.Controllers
{
    public class VehiclesController : ApiControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        private string imagesFolder => configuration.GetValue<string>("Images:Path");
        private string imagesPath => Path.Combine(Startup.wwwRootFolder, imagesFolder);


        public VehiclesController(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<VehiclesVm>> GetVehicles()
            => await Mediator.Send(new GetVehiclesQuery { Path = HttpContext.Request.GetAbsoluteUri(imagesFolder) });

        [HttpGet("pages")]
        public async Task<ActionResult<PaginatedList<ListedVehicleDto>>> GetVehiclesWithPagination([FromQuery] GetVehiclesWithPaginationQuery query)
            => await Mediator.Send(query);

        [HttpGet("basic")]
        public async Task<IList<ListedVehicleDto>> GetAllBasicVehicles()
            => await Mediator.Send(new GetAllBasicVehiclesQuery());

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleVm>> Get(int id)
            => await Mediator.Send(new GetVehicleQuery { Id = id, Path = HttpContext.Request.GetAbsoluteUri(imagesFolder) });

        [HttpGet("{id}/extended")]
        public async Task<ActionResult<VehicleExtendedVm>> GetExtended(int id)
            => await Mediator.Send(new GetVehicleExtendedQuery { Id = id, PhotoPath = HttpContext.Request.GetAbsoluteUri(imagesFolder) });

        [HttpGet("summary")]
        public async Task<ActionResult<VehiclesSummaryDto>> GetSummary()
            => await Mediator.Send(new GetVehiclesSummaryQuery());

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateVehicleDto dto)
        {
            var command = mapper.Map<CreateVehicleCommand>(dto);
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateVehicleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult>UpdateMileage(int id, UpdateMileageCommand command)
        {
            if (id != command.VehicleId)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteVehicleCommand { Id = id, ImagePath = imagesPath });

            return NoContent();
        }
    }
}
