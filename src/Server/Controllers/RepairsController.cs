﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Repairs.Commands.CreateRepair;
using CarsManager.Application.Repairs.Commands.DeleteRepair;
using CarsManager.Application.Repairs.Commands.UpdateRepair;
using CarsManager.Application.Repairs.Queries.Dtos;
using CarsManager.Application.Repairs.Queries.GetBasicRepairs;
using CarsManager.Application.Repairs.Queries.GetRepair;
using CarsManager.Application.Repairs.Queries.GetRepairs;
using CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class RepairsController : ApiControllerBase
    {
        private readonly IMapper mapper;

        public RepairsController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RepairVm>> GetModelsForMake(int id)
            => await Mediator.Send(new GetRepairQuery { Id = id });

        [HttpGet("vehicle/{id}")]
        public async Task<ActionResult<RepairsVm>> GetRepairsForVehicle(int id)
            => await Mediator.Send(new GetRepairsQuery { VehicleId = id });

        [HttpGet("vehicle/{id}/basic")]
        public async Task<IList<BasicRepairForVehicleDto>> GetBasicRepairsForVehicle(int id)
            => await Mediator.Send(new GetBasicRepairsQuery { VehicleId = id });

        [HttpGet("vehicle/{id}/pages")]
        public async Task<ActionResult<PaginatedList<BasicRepairForVehicleDto>>> GetPaginatedRepairsForVehicle(
            int id,
            [FromQuery] GetRepairsForVehicleWithPaginationQueryDto dto)
        {
            var query = mapper.Map<GetRepairsForVehicleWithPaginationQuery>(dto);
            query.VehicleId = id;

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateRepairCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateRepairCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRepairCommand { Id = id });

            return NoContent();
        }
    }
}
