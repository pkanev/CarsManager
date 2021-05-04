using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Employees.Commands.AssignVehicle;
using CarsManager.Application.Employees.Commands.CreateEmployee;
using CarsManager.Application.Employees.Commands.DeleteEmplyee;
using CarsManager.Application.Employees.Commands.RemoveVehicle;
using CarsManager.Application.Employees.Commands.UpdateEmployedStatus;
using CarsManager.Application.Employees.Commands.UpdateEmployee;
using CarsManager.Application.Employees.Commands.UploadPhoto;
using CarsManager.Application.Employees.Queries.GetEmployee;
using CarsManager.Application.Employees.Queries.GetEmployees;
using CarsManager.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server;

namespace CarsManager.Server.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        private readonly IMapper mapper;

        private string path => Path.Combine(Startup.wwwRootFolder, Constants.IMAGES_FOLDER);

        public EmployeesController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeesVm>> Get()
            => await Mediator.Send(new GetEmployeesQuery { PhotoPath = HttpContext.Request.GetAbsoluteUri(Constants.IMAGES_FOLDER) });

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeVm>> Get(int id)
            => await Mediator.Send(new GetEmployeeQuery { Id = id, PhotoPath = HttpContext.Request.GetAbsoluteUri(Constants.IMAGES_FOLDER) });

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
            => await Mediator.Send(command);

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
            command.PhotoPath = path;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateIsEmployedStatus(int id, UpdateEmployedStatusCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id, PhotoPath = path });

            return NoContent();
        }

        [HttpPost("{id}/vehicle/{vehicleId}")]
        public async Task<ActionResult<int>> AssignVehicle(int id, int vehicleId, AssignVehicleCommand command)
        {
            if (id != command.EmployeeId || vehicleId != command.VehicleId)
                return BadRequest();

            return await Mediator.Send(command);
        }

        [HttpDelete("{id}/vehicle/{vehicleId}")]
        public async Task<ActionResult<int>> RemoveVehicle(int id, int vehicleId, RemoveVehicleCommand command)
        {
            if (id != command.EmployeeId || vehicleId != command.VehicleId)
                return BadRequest();

            return await Mediator.Send(command);
        }
    }
}
