using Microsoft.EntityFrameworkCore;
using MouseTracking.Data.Configuration;
using MouseTracking.Domain;

namespace MouseTracking.Data
{
    public class MouseTrackingDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlite(DatabaseConfig.ConnectionString);
           base.OnConfiguring(optionsBuilder);
        }
        public DbSet<MouseMoveEvent> MouseMoveEvents { get; set; }
    }
}
