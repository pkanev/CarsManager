using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Repairs.Commands.CreateRepair
{
    public class CreateRepairCommand : IRequest<int>
    {
        public int VehicleId { get; set; }
        public int RepairShopId { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
        public bool IsOilChanged { get; set; }
        public bool IsBeltChanged { get; set; }
        public bool IsBatteryChanged { get; set; }
        public bool IsSparkPlugChanged { get; set; }
        public bool IsFuelFilterChanged { get; set; }
        public bool IsBrakeLiningsChanged { get; set; }
        public bool IsBrakeDisksChanged { get; set; }
        public bool IsCoolantChanged { get; set; }
        public bool IsOtherWorkDone { get; set; }
        public string Description { get; set; }
        public decimal FinalPrice { get; set; }
    }

    public class CreateRepairCommandHandler : IRequestHandler<CreateRepairCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateRepairCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateRepairCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);
            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            var shop = await context.RepairShops.FindAsync(request.RepairShopId);
            if (shop == null)
                throw new NotFoundException(nameof(RepairShop), request.RepairShopId);

            var entity = new Repair
            {
                Vehicle = vehicle,
                RepairShop = shop,
                Mileage = request.Mileage,
                Date = request.Date,
                IsOilChanged = request.IsOilChanged,
                IsBeltChanged = request.IsBeltChanged,
                IsBatteryChanged = request.IsBatteryChanged,
                IsSparkPlugChanged = request.IsSparkPlugChanged,
                IsFuelFilterChanged = request.IsFuelFilterChanged,
                IsBrakeLiningsChanged = request.IsBrakeLiningsChanged,
                IsBrakeDisksChanged = request.IsBrakeDisksChanged,
                IsCoolantChanged = request.IsCoolantChanged,
                IsOtherWorkDone = request.IsOtherWorkDone,
                Description = request.Description,
                FinalPrice = request.FinalPrice,
            };

            vehicle.Mileage = entity.Mileage;
            if (entity.IsBeltChanged)
                vehicle.BeltMileage = vehicle.Mileage;

            if (entity.IsBrakeDisksChanged)
                vehicle.BrakeDisksMileage = vehicle.Mileage;

            if (entity.IsBrakeLiningsChanged)
                vehicle.BrakeLiningsMileage = vehicle.Mileage;

            if (entity.IsCoolantChanged)
                vehicle.CoolantMileage = vehicle.Mileage;

            if (entity.IsOilChanged)
                vehicle.OilMileage = vehicle.Mileage;

            await context.Repairs.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
