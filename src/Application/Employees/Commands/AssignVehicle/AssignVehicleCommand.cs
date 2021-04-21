using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Vehicles.Exceptions;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Commands.AssignVehicle
{
    public class AssignVehicleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
        public DateTime CheckedOut { get; set; } = DateTime.Now;
        public string Destination { get; set; }
    }

    public class AssignVehicleCommandHandler : IRequestHandler<AssignVehicleCommand, int>
    {
        private readonly IApplicationDbContext context;

        public AssignVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(AssignVehicleCommand request, CancellationToken cancellationToken)
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

            var roadBookEntry = request.Id == 0
                ? new RoadBookEntry()
                : await context.RoadBookEntries.FindAsync(request.Id);

            if (roadBookEntry == null)
                throw new NotFoundException(nameof(RoadBookEntry), request.Id);

            if (employee.Vehicles.Any(v => v.Id == vehicle.Id))
                throw new VehicleAlreadyRegisteredException($"Vehicle {vehicle.LicencePlate} has already been assigned to {employee.GivenName} {employee.Surname}");

            employee.Vehicles.Add(vehicle);

            if (roadBookEntry.Id == 0)
            {
                roadBookEntry.Vehicle = vehicle;
                roadBookEntry.OldMileage = vehicle.Mileage;
            }

            if (!roadBookEntry.Employees.Contains(employee))
                roadBookEntry.Employees.Add(employee);

            if (!roadBookEntry.ActiveUsers.Contains(employee))
                roadBookEntry.ActiveUsers.Add(employee);

            roadBookEntry.CheckedOut = request.CheckedOut;
            roadBookEntry.Destination = request.Destination?.Trim();

            if (roadBookEntry.Id == 0)
                context.RoadBookEntries.Add(roadBookEntry);

            await context.SaveChangesAsync(cancellationToken);
            return roadBookEntry.Id;
        }
    }
}
