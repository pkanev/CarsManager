using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Commands.RemoveVehicle
{
    [Authorise(Roles = RoleConstants.ADMIN)]
    public class RemoveVehicleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
        public DateTime CheckedIn { get; set; }
        public int NewMileage { get; set; }
        public string Destination { get; set; }
    }

    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand, int>
    {
        private readonly IApplicationDbContext context;

        public RemoveVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
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

            var roadBookEntry = await context.RoadBookEntries.Include(e => e.ActiveUsers).FirstOrDefaultAsync(e => e.Id == request.Id);
            if (roadBookEntry == null)
                throw new NotFoundException(nameof(RoadBookEntry), request.Id);

            employee.Vehicles.Remove(vehicle);
            vehicle.Mileage = request.NewMileage;

            roadBookEntry.CheckedIn = request.CheckedIn;
            roadBookEntry.NewMileage = request.NewMileage;
            roadBookEntry.Destination = request.Destination?.Trim();

            if (!IsValidRoadBookData(roadBookEntry))
                throw new InvalidRoadEntryDataException($"Invalid Road Entry: {roadBookEntry}");

            if (roadBookEntry.ActiveUsers.Contains(employee))
                roadBookEntry.ActiveUsers.Remove(employee);

            await context.SaveChangesAsync(cancellationToken);
            return roadBookEntry.ActiveUsers.Count > 0
                ? roadBookEntry.Id
                : 0;
        }

        private static bool IsValidRoadBookData(RoadBookEntry roadBookEntry)
            => roadBookEntry.CheckedIn >= roadBookEntry.CheckedOut
            && roadBookEntry.NewMileage >= roadBookEntry.OldMileage
            && !string.IsNullOrWhiteSpace(roadBookEntry.Destination);
    }
}
