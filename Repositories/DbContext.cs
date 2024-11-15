using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class DbContext : IdentityDbContext <AppUser>
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<MaintenanceData> MaintenanceData { get; set; }
    
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
}