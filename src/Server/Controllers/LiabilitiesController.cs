﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Dtos;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Liabilities;
using CarsManager.Application.Liabilities.Commands.CreateLiability;
using CarsManager.Application.Liabilities.Commands.DeleteLiability;
using CarsManager.Application.Liabilities.Commands.UpdateLiability;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReportWithPagination;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicle;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicleWithPagination;
using CarsManager.Application.Liabilities.Queries.GetLiability;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public abstract class LiabilitiesController : ApiControllerBase
    {
        private readonly IMapper mapper;
        private readonly LiabilityType liabilityType;

        protected LiabilitiesController(IMapper mapper, LiabilityType liabilityType)
        {
            this.mapper = mapper;
            this.liabilityType = liabilityType;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiabilityVm>> Get(int id)
            => await Mediator.Send(new GetLiabilityQuery { Id = id, Liability = liabilityType });

        [HttpGet("vehicle/{id}")]
        public async Task<ActionResult<LiabilitiesVm>> GetForVehicle(int id)
            => await Mediator.Send(new GetLiabilitiesForVehicleQuery { VehicleId = id, Liability = liabilityType });

        [HttpGet("vehicle/{id}/pages")]
        public async Task<ActionResult<PaginatedList<ListedLiabilityDto>>> GetPagesForVehicle(
            int id,
            [FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetLiabilitiesForVehicleWithPaginationQuery>(dto);
            query.VehicleId = id;
            query.Liability = liabilityType;

            return await Mediator.Send(query);
        }

        [HttpGet("reports/all")]
        public async Task<IList<LiabilityForReportDto>> GetLiabilitiesForReport()
            => await Mediator.Send(new GetLiabilitiesForReportQuery { Liability = liabilityType });

        [HttpGet("reports/pages")]
        public async Task<ActionResult<PaginatedList<LiabilityForReportDto>>> GetLiabilitiesForReportPaginated([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetLiabilitiesForReportWithPaginationQuery>(dto);
            query.Liability = liabilityType;

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateLiabilityCommandDto dto)
        {
            var command = mapper.Map<CreateLiabilityCommand>(dto);
            command.Liability = liabilityType;
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateLiabilityCommandDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var command = mapper.Map<UpdateLiabilityCommand>(dto);
            command.Liability = liabilityType;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteLiabilityCommand { Id = id, Liability = liabilityType });

            return NoContent();
        }
    }
}
