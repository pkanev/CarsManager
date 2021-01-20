using System.Threading;
using System.Threading.Tasks;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<Repair> Repairs { get; set; }
        DbSet<RepairShop> RepairShops { get; set; }
        DbSet<Make> Makes { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<MOT> MOTs { get; set; }
        DbSet<CivilLiability> CivilLiabilities { get; set; }
        DbSet<CarInsurance> CarInsurances { get; set; }
        DbSet<Vignette> Vignettes { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Town> Towns { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
