using AutoMapper;
using CarsManager.Application.Liabilities;

namespace CarsManager.Server.Controllers
{
    public class MOTsController : LiabilitiesController
    {
        public MOTsController(IMapper mapper)
            : base(mapper, LiabilityType.MOT)
        {
        }
    }
}
