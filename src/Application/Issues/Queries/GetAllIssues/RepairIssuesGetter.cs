using System.Linq;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public class RepairIssuesGetter : IRepairIssuesGetter
    {
        public async Task<int> GetBeltMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => alertLimit >= (v.BeltMileage + allowedMileage - v.Mileage))
                .ToListAsync();
            
            return vehicles.Count();
        }

        public async Task<int> GetBeltMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit = 0)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => warningLimit >= (v.BeltMileage + allowedMileage - v.Mileage)
                    && alertLimit < (v.BeltMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetBrakeDisksMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => alertLimit >= (v.BrakeDisksMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetBrakeDisksMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => warningLimit >= (v.BrakeDisksMileage + allowedMileage - v.Mileage)
                    && alertLimit < (v.BrakeDisksMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetBrakeLiningsMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => alertLimit >= (v.BrakeLiningsMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetBrakeLiningsMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => warningLimit >= (v.BrakeLiningsMileage + allowedMileage - v.Mileage)
                    && alertLimit < (v.BrakeLiningsMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetCoolantMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => alertLimit >= (v.CoolantMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetCoolantMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => warningLimit >= (v.CoolantMileage + allowedMileage - v.Mileage)
                    && alertLimit < (v.CoolantMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetOilMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => alertLimit >= (v.OilMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }

        public async Task<int> GetOilMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit)
        {
            if (alertLimit == 0)
                return 0;

            var vehicles = await context.Vehicles
                .Where(v => warningLimit >= (v.OilMileage + allowedMileage - v.Mileage)
                    && alertLimit < (v.OilMileage + allowedMileage - v.Mileage))
                .ToListAsync();

            return vehicles.Count();
        }
    }
}
