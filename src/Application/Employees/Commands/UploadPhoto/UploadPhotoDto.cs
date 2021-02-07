using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Employees.Commands.UploadPhoto
{
    public class UploadPhotoDto
    {
        public int EmployeeId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
