using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest
    {
        public int Id { get; set; }
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

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public UpdateVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            entity.Model = request.Model;
            entity.Year = request.Year;
            entity.Fuel = request.Fuel;
            entity.EngineDisplacement = request.EngineDisplacement;
            entity.Mileage = request.Mileage;
            entity.LicencePlate = request.LicencePlate;
            entity.Color = request.Color;
            entity.FirstRegistration = request.FirstRegistration;
            entity.BeltMileage = request.BeltMileage;
            entity.BrakeLiningsMileage = request.BrakeLiningsMileage;
            entity.BrakeDisksMileage = request.BrakeDisksMileage;
            entity.CoolantMileage = request.CoolantMileage;
            entity.FuelConsumption = request.FuelConsumption;
            entity.OilMileage = request.OilMileage;

            if (request.MOT != null && entity.MOTs.Last() != request.MOT)
                entity.MOTs.Add(request.MOT);

            if (request.CivilLiability != null && entity.CivilLiabilities.Last() != request.CivilLiability)
                entity.CivilLiabilities.Add(request.CivilLiability);

            if (request.CarInsurance != null && entity.CarInsurances.Last() != request.CarInsurance)
                entity.CarInsurances.Add(request.CarInsurance);

            if (request.Vignette != null && entity.Vignettes.Last() != request.Vignette)
                entity.Vignettes.Add(request.Vignette);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
