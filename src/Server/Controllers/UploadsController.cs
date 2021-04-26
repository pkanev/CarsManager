using System.IO;
using System.Threading.Tasks;
using CarsManager.Application.Uploadds.CreateUpload;
using CarsManager.Application.Uploadds.DeleteUpload;
using CarsManager.Application.Uploads.DeleteUpload;
using CarsManager.Server.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server;

namespace CarsManager.Server.Controllers
{
    public class UploadsController : ApiControllerBase
    {
        private string path => Path.Combine(Startup.wwwRootFolder, Constants.IMAGES_FOLDER);

        [HttpPost]
        public async Task<ActionResult<string>> Upload(IFormFile file)
        {
            var command = new CreateUploadCommand
            {
                File = file,
                Path = path,
            };

            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteUploadCommandDto dto)
        {
            await Mediator.Send(new DeleteUploadCommand { FileName = dto.FileName, Path = path, });
            return NoContent();
        }
    }
}
