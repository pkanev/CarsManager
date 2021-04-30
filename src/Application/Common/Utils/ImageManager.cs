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

        public async Task<string> SaveFileAsync(string directory, IFormFile file, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentNullException("Image store path cannot be null or empty");

            if (file.Length == 0)
                throw new ArgumentNullException("Image file length cannot be null");

            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!IsValidImage(extension))
                throw new InvalidImageTypeException("Unsupported image type.");

            string fileName = $"{Guid.NewGuid()}{extension}";

            using (var fileStream = new FileStream(Path.Combine(directory, fileName), FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            return fileName;
        }

        public void DeleteFile(string directory, string filename)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentNullException("Image store path cannot be null or empty");

            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException("File name cannot be null or empty");

            File.Delete(Path.Combine(directory, filename));
        }

        private bool IsValidImage(string extension)
            => imageExtensions.Contains(extension);
    }
}
