using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Employees.Commands.DeleteEmplyee
{
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
            var entity = await context.Employees.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            if (!string.IsNullOrEmpty(entity.Image))
                imageManager.DeleteFile(request.PhotoPath, entity.Image);

            entity.Vehicles.Clear();
            context.Employees.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
