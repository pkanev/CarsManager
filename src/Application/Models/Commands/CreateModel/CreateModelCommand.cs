using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using MediatR;

namespace CarsManager.Application.Models.Commands.CreateModel
{
    public class CreateModelCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int MakeId { get; set; }
        public VehicleType VehicleType { get; set; }
    }

    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateModelCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var make = await context.Makes.FindAsync(request.MakeId);
            if (make == null)
                throw new NotFoundException(nameof(Make), request.MakeId);

            var entity = new Model
            {
                Name = request.Name.Trim(),
                MakeId = make.Id,
                VehicleType = request.VehicleType,
            };

            await context.Models.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
