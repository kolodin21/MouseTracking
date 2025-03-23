using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MouseTracking.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMouseTrackingDbContext(this IServiceCollection services)
        {
            var dbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "MouseTracking.Data");
            var dbPath = Path.Combine(dbFolder, "MouseTrackingDb.db");

            services.AddDbContext<MouseTrackingDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            return services;
        }
    }
}
