using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CarsManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender mediator;

        protected ISender Mediator => mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
