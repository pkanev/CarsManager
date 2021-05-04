using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Employees.Commands.UpdateEmployedStatus
{
    [Authorise]
    public class UpdateEmployedStatusCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsEmployed { get; set; }
    }

    public class UpdateEmployedStatusCommandHandler : IRequestHandler<UpdateEmployedStatusCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateEmployedStatusCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateEmployedStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Employees.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            entity.IsEmployed = request.IsEmployed;
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
