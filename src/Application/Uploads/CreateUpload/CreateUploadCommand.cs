using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Uploadds.CreateUpload
{
    [Authorise]
    public class CreateUploadCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
    }

    public class CreateUploadCommandHandler : IRequestHandler<CreateUploadCommand, string>
    {
        private readonly IImageManager imageManager;

        public CreateUploadCommandHandler(IImageManager imageManager)
        {
            this.imageManager = imageManager;
        }

        public async Task<string> Handle(CreateUploadCommand request, CancellationToken cancellationToken)
            => await imageManager.SaveFileAsync(request.Path, request.File, cancellationToken);
    }
}
