using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Commands.AssignVehicle
{
    public class AssignVehicleCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
    }

    public class AssignVehicleCommandHandler : IRequestHandler<AssignVehicleCommand>
    {
        private readonly IApplicationDbContext context;

        public AssignVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(AssignVehicleCommand request, CancellationToken cancellationToken)
        {
            var employee = await context
                .Employees
                .Include(e => e.Vehicles)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId);
            if (employee == null)
                throw new NotFoundException(nameof(Employee), request.EmployeeId);

            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);
            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            if (employee.Vehicles.Any(v => v.Id == vehicle.Id))
                throw new VehicleAlreadyRegisteredException($"Vehicle {vehicle.LicencePlate} has already been assigned to {employee.GivenName} {employee.Surname}");

            employee.Vehicles.Add(vehicle);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
