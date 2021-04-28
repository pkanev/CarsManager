using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Commands.DeleteEmplyee
{
    [Authorise(Roles = RoleConstants.ADMIN)]
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IImageManager imageManager;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context, IImageManager imageManager)
        {
            this.context = context;
            this.imageManager = imageManager;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Employees
                .Include(e => e.ActiveRecords)
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            if (entity.ActiveRecords.Count > 0)
                throw new ForbiddenEmployeeDeletionException($"Employee {entity.GivenName} {entity.Surname} has an assigned vehicle.");

            if (!string.IsNullOrEmpty(entity.Image))
                imageManager.DeleteFile(request.PhotoPath, entity.Image);

            entity.Vehicles.Clear();
            entity.RoadBookEntries.Clear();
            entity.ActiveRecords.Clear();
            context.Employees.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
