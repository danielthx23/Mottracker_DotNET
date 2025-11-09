using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.HealthCheck;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ApplicationContext _context;

    public DatabaseHealthCheck(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Tenta executar uma query simples para verificar a conexão
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
            
            if (canConnect)
            {
                // Tenta executar uma query simples
                await _context.Database.ExecuteSqlRawAsync("SELECT 1 FROM DUAL", cancellationToken);
                
                return HealthCheckResult.Healthy(
                    "Database is available and responding",
                    new Dictionary<string, object>
                    {
                        { "database", _context.Database.GetDbConnection().Database ?? "Unknown" },
                        { "server", _context.Database.GetDbConnection().DataSource ?? "Unknown" }
                    });
            }
            else
            {
                return HealthCheckResult.Unhealthy("Database is not available");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Database health check failed",
                ex,
                new Dictionary<string, object>
                {
                    { "error", ex.Message }
                });
        }
    }
}

