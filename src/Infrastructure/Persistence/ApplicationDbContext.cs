using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<RepairShop> RepairShops { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<MOT> MOTs { get; set; }
        public DbSet<CivilLiability> CivilLiabilities { get; set; }
        public DbSet<CarInsurance> CarInsurances { get; set; }
        public DbSet<Vignette> Vignettes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<RoadBookEntry> RoadBookEntries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
