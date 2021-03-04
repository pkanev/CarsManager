using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Models.Exceptions;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using MediatR;

namespace CarsManager.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public VehicleType VehicleType { get; set; }
    }

    public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public UpdateModelCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Models.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Model), request.Id);

            if (entity.MakeId != request.MakeId)
                throw new NonMatchingMakeException($"Model with Id: {request.Id} has incorrect MakeId: {request.MakeId}");

            entity.Name = request.Name.Trim();
            entity.VehicleType = request.VehicleType;

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
