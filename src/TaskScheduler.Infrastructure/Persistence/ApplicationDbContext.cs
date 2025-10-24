using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TaskScheduler.Infrastructure.Persistence;

/// <summary>
/// Application database context for TaskScheduler.
/// Manages entity configurations and database operations using SQLite.
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ILogger<ApplicationDbContext> logger) : base(options)
    {
        _logger = logger;
        _logger.LogDebug("ApplicationDbContext initialized");
    }

    /// <summary>
    /// Configures entity mappings and relationships.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _logger.LogDebug("Configuring entity model");

        // Future: Apply entity configurations from assembly
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    /// <summary>
    /// Saves changes to the database with audit logging.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of state entries written to the database.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Saving changes to database");

        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully saved {EntityCount} entities to database", result);
            return result;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while saving changes");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving changes to database");
            throw;
        }
    }

    /// <summary>
    /// Saves changes to the database with audit logging (synchronous).
    /// </summary>
    /// <returns>Number of state entries written to the database.</returns>
    public override int SaveChanges()
    {
        _logger.LogInformation("Saving changes to database (synchronous)");

        try
        {
            var result = base.SaveChanges();
            _logger.LogInformation("Successfully saved {EntityCount} entities to database", result);
            return result;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while saving changes");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving changes to database");
            throw;
        }
    }
}
