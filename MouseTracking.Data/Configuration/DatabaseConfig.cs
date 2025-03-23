using Microsoft.Extensions.Configuration;

namespace MouseTracking.Data.Configuration;

public static class DatabaseConfig
{
    private static readonly IConfiguration Configuration;

    static DatabaseConfig()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(@"Configuration\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string ConnectionString => Configuration["ConnectionStrings:DefaultConnection"]
                                             ?? throw new InvalidOperationException(
                                                 "ConnectionStrings not found in configuration.");

    
}