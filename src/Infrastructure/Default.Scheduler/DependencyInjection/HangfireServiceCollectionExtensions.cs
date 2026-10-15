using Hangfire;
using Hangfire.PostgreSql;
using Default.DataAccess.Config;
using Default.DependencyInjection;
using Default.Scheduler.Abstractions;
using Default.Syntax.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Default.Scheduler.DependencyInjection;

public static class HangfireServiceCollectionExtensions
{
    public static IServiceCollection
        AddScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConfigSection<DataAccessConfigSection>().Connection;

        var podName = Environment.GetEnvironmentVariable("HOSTNAME") ?? Environment.MachineName;
        var name = AssemblyRoot.GetRootAssembly()?.GetName().Name?.Replace(".", "").ToLower() ?? "default";
        var instanceId = $"{podName}_{Environment.ProcessId}";

        services
            .AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseDefaultTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(x => { x.UseNpgsqlConnection(connection); }, new PostgreSqlStorageOptions
                    {
                        SchemaName = $"{name}.hangfire",
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        InvisibilityTimeout = TimeSpan.FromMinutes(30),
                        DistributedLockTimeout = TimeSpan.FromMinutes(10),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DeleteExpiredBatchSize = 1000,
                        UseSlidingInvisibilityTimeout = false
                    });
            })
            .AddScoped<IJobService, JobService>()
            .Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo<IRecurringJob>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());

        services.AddHangfireServer(options =>
        {
            options.ServerName = $"{name}_{instanceId}";
            options.Queues = [name];
            options.WorkerCount = Environment.ProcessorCount;
        });

        return services;
    }
}