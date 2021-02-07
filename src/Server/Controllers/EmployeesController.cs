using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Employees.Commands.CreateEmployee;
using CarsManager.Application.Employees.Commands.DeleteEmplyee;
using CarsManager.Application.Employees.Commands.UpdateEmployee;
using CarsManager.Application.Employees.Commands.UploadPhoto;
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
            command.PhotoPath = configuration.GetValue<string>("Photos:Path");
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/photo")]
        public async Task<ActionResult<int>> UpdatePhoto(int id, [FromForm] UploadPhotoDto dto)
        {
            if (id != dto.EmployeeId)
                return BadRequest();

            var command = mapper.Map<UploadPhotoCommand>(dto);
            command.PhotoPath = configuration.GetValue<string>("Photos:Path");
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id, PhotoPath = configuration.GetValue<string>("Photos:Path") });

            return NoContent();
        }
    }
}
