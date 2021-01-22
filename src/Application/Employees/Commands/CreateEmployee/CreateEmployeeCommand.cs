using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Events;
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
        public byte[] Photo { get; set; }
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
            var entity = new Employee
            {
                GivenName = request.GivenName,
                MiddleName = request.MiddleName,
                Surname = request.Surname,
                TownId = request.TownId,
                Address = request.Address,
                PostCode = request.PostCode,
                Telephone = request.Telephone,
                Photo = request.Photo
            };

            entity.DomainEvents.Add(new EmployeeCreatedEvent(entity));

            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
