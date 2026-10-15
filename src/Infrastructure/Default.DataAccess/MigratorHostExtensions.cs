using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Default.DataAccess.EntityFramework;

public static class MigratorHostExtensions
{
    public static IHost Migrate<TDbContext>(this IHost app, string[] args) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        dbContext.Database.Migrate();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TDbContext>>();
        logger.LogInformation("Database migration completed.");

        if (args.Contains("--migrate"))
        {
            logger.LogInformation("Exiting application after --migrate.");
            Environment.Exit(0);
        }

        return app;
    }
}