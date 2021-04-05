using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string ImageName { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Employees.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            var town = await context.Towns.FindAsync(request.TownId);
            if (town == null)
                throw new NotFoundException(nameof(Town), request.TownId);

            entity.GivenName = request.GivenName;
            entity.MiddleName = request.MiddleName;
            entity.Surname = request.Surname;
            entity.Town = town;
            entity.Address = request.Address;
            entity.PostCode = request.PostCode;
            entity.Telephone = request.Telephone;
            entity.Image = request.ImageName;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
