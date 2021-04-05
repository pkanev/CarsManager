using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class LoginController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
