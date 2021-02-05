using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Employees.Commands.CreateEmployee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CarsManager.Server.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public EmployeesController(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateEmployeeDto dto)
        {
            var command = mapper.Map<CreateEmployeeCommand>(dto);
            command.Path = configuration.GetValue<string>("Photos:Path");
            return await Mediator.Send(command);
        }
    }
}
