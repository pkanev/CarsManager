using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
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
                if (vehicle != null)
                    entity.Vehicles.Add(vehicle);
            }

            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
