using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Employees.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IRequest, IMapFrom<UploadPhotoDto>
    {
        public int EmployeeId { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
    }

    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IImageManager imageManager;

        public UploadPhotoCommandHandler(IApplicationDbContext context, IImageManager imageManager)
        {
            this.context = context;
            this.imageManager = imageManager;
        }

        public async Task<Unit> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Employees.FindAsync(request.EmployeeId);

            if (entity == null)
                throw new NotFoundException(nameof(Employees), request.EmployeeId);

            if (!string.IsNullOrEmpty(entity.ImageName))
                imageManager.DeleteFileAsync(request.PhotoPath, entity.ImageName, cancellationToken);

            entity.ImageName = request.Photo == null
                ? string.Empty
                : await imageManager.SaveFileAsync(request.PhotoPath, request.Photo, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
