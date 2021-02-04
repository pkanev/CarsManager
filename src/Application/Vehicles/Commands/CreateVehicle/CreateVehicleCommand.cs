using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Vehicles.Commands.Dtos;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<int>
    {
        public int ModelId { get; set; }
        public int Year { get; set; }
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
            };

            if (request.MOT != null)
                entity.MOTs.Add(new MOT { Date = request.MOT.Date, Duration = TimeSpan.FromDays(request.MOT.DurationDays) });

            if (request.CivilLiability != null)
                entity.CivilLiabilities.Add(new CivilLiability { Date = request.CivilLiability.Date, Duration = TimeSpan.FromDays(request.CivilLiability.DurationDays) });

            if (request.CarInsurance != null)
                entity.CarInsurances.Add(new CarInsurance { Date = request.CarInsurance.Date, Duration = TimeSpan.FromDays(request.CarInsurance.DurationDays) });

            if (request.Vignette != null)
                entity.Vignettes.Add(new Vignette { Date = request.Vignette.Date, Duration = TimeSpan.FromDays(request.Vignette.DurationDays) });

            context.Vehicles.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
