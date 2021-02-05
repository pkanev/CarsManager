using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Employees.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }

    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, string>
    {
        public async Task<string> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            return "file";
        }
    }
}
