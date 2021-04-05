using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IImageManager
    {
        Task<string> SaveFileAsync(string directory, IFormFile file, CancellationToken cancellationToken);
        void DeleteFile(string directory, string filename);
    }
}