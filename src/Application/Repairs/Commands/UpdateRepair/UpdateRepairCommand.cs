using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Repairs.Commands.UpdateRepair
{
    [Authorise]
    public class UpdateRepairCommand : IRequest
    {
        public int Id { get; set; }
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

    public class UpdateRepairCommandHandler : IRequestHandler<UpdateRepairCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateRepairCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateRepairCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Repairs.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Repair), request.Id);

            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);
            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            var shop = await context.RepairShops.FindAsync(request.RepairShopId);
            if (shop == null)
                throw new NotFoundException(nameof(RepairShop), request.RepairShopId);

            entity.Vehicle = vehicle;
            entity.RepairShop = shop;
            entity.Mileage = request.Mileage;
            entity.Date = request.Date;
            entity.IsOilChanged = request.IsOilChanged;
            entity.IsBeltChanged = request.IsBeltChanged;
            entity.IsBatteryChanged = request.IsBatteryChanged;
            entity.IsSparkPlugChanged = request.IsSparkPlugChanged;
            entity.IsFuelFilterChanged = request.IsFuelFilterChanged;
            entity.IsBrakeLiningsChanged = request.IsBrakeLiningsChanged;
            entity.IsBrakeDisksChanged = request.IsBrakeDisksChanged;
            entity.IsCoolantChanged = request.IsCoolantChanged;
            entity.IsOtherWorkDone = request.IsOtherWorkDone;
            entity.Description = request.Description;
            entity.FinalPrice = request.FinalPrice;

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
