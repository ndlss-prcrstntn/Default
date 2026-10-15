using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Default.Hosting.StartupTasks.Filter;

namespace Default.Hosting.StartupTasks;

public static class StartupTaskServiceCollectionExtensions
{
    public static IServiceCollection AddStartupTasks(this IServiceCollection services)
    {
        services
            .AddTransient<IStartupFilter, StartupFilter>()
            .Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo<IStartupTask>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());


        return services;
    }
}