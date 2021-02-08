using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Commands.RemoveVehicle
{
    public class RemoveVehicleCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
    }

    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand>
    {
        private readonly IApplicationDbContext context;

        public RemoveVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
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

            employee.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
