using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.RoadBookEntries.Queries;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string ImageName { get; set; }
        public IList<int> VehicleIds { get; set; } = new List<int>();
        public IList<RoadBookBasicEntryDto> RoadBookEntries { get; set; } = new List<RoadBookBasicEntryDto>();
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var town = await context.Towns.FindAsync(request.TownId);
            if (town == null)
                throw new NotFoundException(nameof(Town), request.TownId);

            var entity = new Employee
            {
                GivenName = request.GivenName,
                MiddleName = request.MiddleName,
                Surname = request.Surname,
                Town = town,
                Address = request.Address,
                PostCode = request.PostCode,
                Telephone = request.Telephone,
                Image = request.ImageName,
            };

            foreach(var vehicleId in request.VehicleIds)
            {
                var vehicle = context.Vehicles.Find(vehicleId);
                if (vehicle == null)
                    throw new NotFoundException(nameof(Vehicle), vehicleId);

                entity.Vehicles.Add(vehicle);
                var roadBookEntry = request.RoadBookEntries.FirstOrDefault(e => e.VehicleId == vehicle.Id);
                if (roadBookEntry == null)
                    throw new NotFoundException($"There is no roadbook entry with a vehicle id {vehicleId}");

                var entryEntity = roadBookEntry.Id == 0
                ? new RoadBookEntry()
                : await context.RoadBookEntries.FindAsync(roadBookEntry.Id);

                if (roadBookEntry == null)
                    throw new NotFoundException(nameof(RoadBookEntry), roadBookEntry.Id);

                if (roadBookEntry.Id == 0)
                {
                    entryEntity.VehicleId = roadBookEntry.VehicleId;
                    entryEntity.OldMileage = vehicle.Mileage;
                }

                entryEntity.CheckedOut = roadBookEntry.CheckedOut;
                entryEntity.Destination = roadBookEntry.Destination?.Trim();
                entryEntity.Employees.Add(entity);
                entryEntity.ActiveUsers.Add(entity);

                if (roadBookEntry.Id == 0)
                    context.RoadBookEntries.Add(entryEntity);
            }

            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
