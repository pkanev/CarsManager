using AutoMapper;
using CarsManager.Application.Liabilities;

namespace CarsManager.Server.Controllers
{
    public class VignettesController : LiabilitiesController
    {
        public VignettesController(IMapper mapper)
            : base(mapper, LiabilityType.Vignette)
        {
        }
    }
}
