using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Vehicles.Commands.Dtos;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    [Authorise]
    public class CreateVehicleCommand : IRequest<int>, IMapFrom<CreateVehicleDto>
    {
        public int ModelId { get; set; }
        public int? Year { get; set; }
        public FuelType Fuel { get; set; }
        public int EngineDisplacement { get; set; }
        public int Mileage { get; set; }
        public string LicencePlate { get; set; }
        public string Color { get; set; }
        public DateTime FirstRegistration { get; set; }
        public int BeltMileage { get; set; }
        public int BrakeLiningsMileage { get; set; }
        public int BrakeDisksMileage { get; set; }
        public int CoolantMileage { get; set; }
        public int FuelConsumption { get; set; }
        public int OilMileage { get; set; }
        public string ImageName { get; set; }
        public LiabilityDto MOT { get; set; }
        public LiabilityDto CivilLiability { get; set; }
        public LiabilityDto CarInsurance { get; set; }
        public LiabilityDto Vignette { get; set; }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var model = await context.Models.FindAsync(request.ModelId);
            if (model == null)
                throw new NotFoundException(nameof(Model), request.ModelId);  

            var entity = new Vehicle
            {
                Model = model,
                Year = request.Year,
                Fuel = request.Fuel,
                EngineDisplacement = request.EngineDisplacement,
                Mileage = request.Mileage,
                LicencePlate = request.LicencePlate.ToUpper(),
                Color = request.Color,
                FirstRegistration = request.FirstRegistration,
                BeltMileage = request.BeltMileage,
                BrakeLiningsMileage = request.BrakeLiningsMileage,
                BrakeDisksMileage = request.BrakeDisksMileage,
                CoolantMileage = request.CoolantMileage,
                FuelConsumption = request.FuelConsumption,
                OilMileage = request.OilMileage,
                Image = request.ImageName
            };

            if (request.MOT != null)
                entity.MOTs.Add(new MOT { StartDate = request.MOT.StartDate, EndDate = request.MOT.EndDate });

            if (request.CivilLiability != null)
                entity.CivilLiabilities.Add(new CivilLiability { StartDate = request.CivilLiability.StartDate, EndDate = request.CivilLiability.EndDate });

            if (request.CarInsurance != null)
                entity.CarInsurances.Add(new CarInsurance { StartDate = request.CarInsurance.StartDate, EndDate = request.CarInsurance.EndDate });

            if (request.Vignette != null)
                entity.Vignettes.Add(new Vignette { StartDate = request.Vignette.StartDate, EndDate = request.Vignette.EndDate });

            context.Vehicles.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
