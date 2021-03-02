using AutoMapper;
using CarsManager.Application.Liabilities;

namespace CarsManager.Server.Controllers
{
    public class CivilLiabilitiesController : LiabilitiesController
    {
        public CivilLiabilitiesController(IMapper mapper)
            : base(mapper, LiabilityType.CivilLiability)
        {
        }
    }
}
