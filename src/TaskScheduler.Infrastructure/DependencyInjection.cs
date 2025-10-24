using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskScheduler.Infrastructure.Persistence;

namespace TaskScheduler.Infrastructure;

/// <summary>
/// Dependency injection configuration for the Infrastructure layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Infrastructure layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="isDevelopment">Whether the application is running in development mode.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment = false)
    {
        // Register Database Context
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);

            // Enable sensitive data logging in development
            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Future: Register repositories
        // services.AddScoped<ITaskRepository, TaskRepository>();
        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IPomodoroSessionRepository, PomodoroSessionRepository>();
        // services.AddScoped<IRoutineRepository, RoutineRepository>();
        // services.AddScoped<IPackItemRepository, PackItemRepository>();

        // Future: Register Unit of Work
        // services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Future: Register external services
        // services.AddScoped<ITeamsIntegrationService, TeamsIntegrationService>();
        // services.AddScoped<IAuthenticationService, AuthenticationService>();
        // services.AddScoped<ISecureStorageService, SecureStorageService>();

        return services;
    }
}
