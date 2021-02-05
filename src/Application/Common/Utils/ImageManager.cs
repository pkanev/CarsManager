using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Common.Utils
{
    public class ImageManager : IImageManager
    {
        private readonly string[] imageExtensions = new string[] { ".png", ".jpg", ".jpeg" };

        public async Task<string> SaveFileAsync(string path, IFormFile file, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("Image store path cannot be null or empty");

            if (file.Length == 0)
                throw new ArgumentNullException("Image file length cannot be null");

            Directory.CreateDirectory(path);

            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!IsValidImage(extension))
                throw new InvalidImageTypeException("Unsupported image type.");

            string fileName = $"{Guid.NewGuid()}{extension}";

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            return fileName;
        }

        private bool IsValidImage(string extension)
            => imageExtensions.Contains(extension);
    }
}
