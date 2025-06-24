using Microsoft.EntityFrameworkCore; // ✅ This is required
using AssetManagement.Models;

namespace AssetManagement.Data
{
    public class AppDbContext : DbContext // ✅ Make sure you inherit from DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<EmployeeAsset> EmployeeAssets { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<AuditRequest> AuditRequests { get; set; }
    }
}
