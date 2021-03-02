using AutoMapper;
using CarsManager.Application.Liabilities;

namespace CarsManager.Server.Controllers
{
    public class CarInsurancesController : LiabilitiesController
    {
        public CarInsurancesController(IMapper mapper)
            : base(mapper, LiabilityType.CarInsurance)
        {
        }
    }
}
