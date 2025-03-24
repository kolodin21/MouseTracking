using Microsoft.EntityFrameworkCore;
using MouseTracking.Data.Configuration;
using MouseTracking.Domain;

namespace MouseTracking.Data
{
    public class MouseTrackingDbContext : DbContext
    {
        public MouseTrackingDbContext() { }
        public MouseTrackingDbContext(DbContextOptions<MouseTrackingDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlite(DatabaseConfig.ConnectionString);
           base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<MouseMoveEvent> MouseMoveEvents { get; set; }
    }
}
