using Microsoft.Extensions.DependencyInjection;

namespace TaskScheduler.Application;

/// <summary>
/// Dependency injection configuration for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Application layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Future: Register application services here
        // services.AddScoped<ITaskService, TaskService>();
        // services.AddScoped<ICategoryService, CategoryService>();
        // services.AddScoped<IPomodoroService, PomodoroService>();
        // services.AddScoped<IRoutineService, RoutineService>();
        // services.AddScoped<IPackingListService, PackingListService>();

        // Future: Register AutoMapper
        // services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
