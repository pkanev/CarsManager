using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IImageManager
    {
        Task<string> SaveFileAsync(string path, IFormFile file, CancellationToken cancellationToken);
    }
}