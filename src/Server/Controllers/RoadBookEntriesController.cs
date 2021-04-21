using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarsManager.Application.Common.Models;
using CarsManager.Application.RoadBookEntries.Queries;
using CarsManager.Application.RoadBookEntries.Queries.GetPaginatedRoadBook;
using CarsManager.Application.RoadBookEntries.Queries.GetRoadBook;
using CarsManager.Application.RoadBookEntries.Queries.GetRoadBookEntry;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class RoadBookEntriesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadBookBasicEntryDto>> Get(int id)
            => await Mediator.Send(new GetRoadBookEntryQuery { Id = id });

        [HttpGet]
        public async Task<IList<RoadBookFullEntryDto>> GetRoadBook([FromQuery] GetRoadBookQuery query)
            => await Mediator.Send(query);

        [HttpGet("pages")]
        public async Task<ActionResult<PaginatedList<RoadBookFullEntryDto>>> GetRoadBookWithPagination([FromQuery]GetPaginatedRoadBookQuery query)
            => await Mediator.Send(query);
    }
}
