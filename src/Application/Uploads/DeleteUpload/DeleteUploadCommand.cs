using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;

namespace CarsManager.Application.Uploadds.DeleteUpload
{
    [Authorise]
    public class DeleteUploadCommand : IRequest
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }

    public class DeleteUploadCommandHandler : IRequestHandler<DeleteUploadCommand>
    {
        private readonly IImageManager imageManager;

        public DeleteUploadCommandHandler(IImageManager imageManager)
        {
            this.imageManager = imageManager;
        }

        public Task<Unit> Handle(DeleteUploadCommand request, CancellationToken cancellationToken)
        {
            imageManager.DeleteFile(request.Path, request.FileName);

            return Unit.Task;
        }
    }
}
