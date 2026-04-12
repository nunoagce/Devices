using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }

    public static async Task UseMigrationsAsync(this IServiceProvider serviceProvider, bool isDevelopment)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            logger.LogInformation("Checking for pending migrations...");
            await context.Database.MigrateAsync();
            logger.LogInformation("Database is up to date.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to migrate database.");
            throw;
        }

        if (isDevelopment && !await context.Devices.AnyAsync())
        {
            context.Devices.AddRange(
                Device.Create("Device 1", "Brand 1").Value,
                Device.Create("Device 2", "Brand 1").Value,
                Device.Create("Device 3", "Brand 2").Value,
                Device.Create("Device 4", "Brand 2").Value
            );
            await context.SaveChangesAsync();
        }
    }
}

