using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Default.Hosting;
using Default.Hosting.StartupTasks;
using Default.Scheduler.Abstractions;
using Default.Syntax.Reflection;

namespace Default.Scheduler.DependencyInjection;

public class RecurringJobsStartupTask(
    IRecurringJobManager jobManager,
    IServiceProvider serviceProvider,
    IJobService jobExecutionService,
    IHostEnvironment environment,
    ILogger<RecurringJobsStartupTask> logger)
    : IStartupTask
{
    public int Order() => 5;

    public async Task ExecuteAsync(CancellationToken cancellationToke = default)
    {
        var jobDescriptors = jobExecutionService.DiscoverJobs();

        var name = AssemblyRoot.GetRootAssembly()?.GetName().Name?.Replace(".", "").ToLower() ?? "default";

        foreach (var jobDescriptor in jobDescriptors)

            try
            {
                using var scope = serviceProvider.CreateScope();
                var job = (IRecurringJob)scope.ServiceProvider.GetRequiredService(jobDescriptor.Type);
                jobManager.AddOrUpdate(
                    jobDescriptor.Id,
                    () => job.ExecuteBase(null),
                    !environment.IsLocal() ? jobDescriptor.Cron : "*/15 * * * 1",
                    queue: name
                );

                if (jobDescriptor.RunOnStart && !environment.IsLocal())
                    jobManager.Trigger(jobDescriptor.Id);

                logger.LogInformation(
                    "Registered recurring job: {Id} ({Type}) with cron: {Cron}, order: {Order}",
                    jobDescriptor.Id,
                    jobDescriptor.Type.Name,
                    jobDescriptor.Cron);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to register job {Id} of type {Type}",
                    jobDescriptor.Id, jobDescriptor.Type.Name);
            }

        logger.LogInformation("Registered {Count} recurring jobs", jobDescriptors.Count);
    }
}