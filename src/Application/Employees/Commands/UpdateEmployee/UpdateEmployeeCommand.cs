using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
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
        public byte[] Photo { get; set; }
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
                throw new NotFoundException(nameof(Employees), request.Id);

            entity.GivenName = request.GivenName;
            entity.MiddleName = request.MiddleName;
            entity.Surname = request.Surname;
            entity.TownId = request.TownId;
            entity.Address = request.Address;
            entity.PostCode = request.PostCode;
            entity.Telephone = request.Telephone;
            entity.Photo = request.Photo;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
