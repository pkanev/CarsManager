using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using CarsManager.Domain.Events;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<int>
    {
        public Model Model { get; set; }
        public DateTime Year { get; set; }
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
        public MOT MOT { get; set; }
        public CivilLiability CivilLiability { get; set; }
        public CarInsurance CarInsurance { get; set; }
        public Vignette Vignette { get; set; }
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
            var entity = new Vehicle
            {
                Model = request.Model,
                Year = request.Year,
                Fuel = request.Fuel,
                EngineDisplacement = request.EngineDisplacement,
                Mileage = request.Mileage,
                LicencePlate = request.LicencePlate,
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
                entity.MOTs.Add(request.MOT);

            if (request.CivilLiability != null)
                entity.CivilLiabilities.Add(request.CivilLiability);

            if (request.CarInsurance != null)
                entity.CarInsurances.Add(request.CarInsurance);

            if (request.Vignette != null)
                entity.Vignettes.Add(request.Vignette);

            entity.DomainEvents.Add(new VehicleCreatedEvent(entity));

            context.Vehicles.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
