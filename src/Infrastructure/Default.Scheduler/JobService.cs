using System.Reflection;
using Hangfire;
using Hangfire.Storage;
using Default.Abstractions.Exceptions;
using Default.Scheduler.Abstractions;
using Default.Scheduler.Model;
using Default.Scheduler.Model.Ext;
using Default.Syntax.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Default.Scheduler;

public class JobService : IJobService
{
    private readonly List<JobDescriptor> _availableJobs;
    private readonly ILogger<JobService> _logger;
    private readonly string _queueName;
    private readonly IServiceProvider _serviceProvider;

    public JobService(
        IServiceProvider serviceProvider,
        ILogger<JobService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _availableJobs = DiscoverJobs();

        _queueName = AssemblyRoot.GetRootAssembly()?.GetName().Name?.Replace(".", "").ToLower() ?? "default";
    }

    public void ExecuteJob(string jobId, DateTime? startTime = null)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job ID не может быть пустым", nameof(jobId));

        var jobDescriptor = _availableJobs.FirstOrDefault(j => j.Id == jobId);

        if (jobDescriptor == null)
        {
            _logger.LogWarning("Попытка запуска несуществующей задачи: {Id}", jobId);
            throw new ClientException($"Задача с идентификатором '{jobId}' не найдена");
        }

        try
        {
            var job = (IRecurringJob)_serviceProvider.GetRequiredService(jobDescriptor.Type);

            var hangfireJobId = BackgroundJob.Enqueue(_queueName, () => job.ExecuteBase(startTime));

            _logger.LogInformation("Задача {Id} успешно запущена с Hangfire ID: {HangfireJobId}",
                jobId, hangfireJobId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при запуске задачи {Id}", jobId);
            throw new ClientException($"Не удалось запустить задачу '{jobId}': {ex.Message}");
        }
    }

    public List<JobInfo> GetAvailableJobs()
    {
        var result = new List<JobInfo>();
        var monitoring = JobStorage.Current.GetMonitoringApi();
        var succeeded = monitoring.SucceededJobs(0, 50);
        var failed = monitoring.FailedJobs(0, 30);
        var processing = monitoring.ProcessingJobs(0, 10);

        using var connection = JobStorage.Current.GetConnection();
        var jobs = connection.GetRecurringJobs();

        foreach (var job in jobs.Where(j => j.Queue == _queueName))
        {
            var jobDescriptor = _availableJobs.FirstOrDefault(j => j.Id == job.Id);

            if (jobDescriptor == null)
                continue;

            var jobInfo = job.ToJobInfo(jobDescriptor?.Name ?? job.Id);

            var s = succeeded.Select(j => j.Value).Where(j => j.Job?.Type != null)
                .Where(j => j.Job.Type.Name == jobDescriptor.Type.Name).Select(j =>
                    new PastJobInfo
                    {
                        End = j.SucceededAt,
                        Duration = j.TotalDuration,
                        JobState = JobState.Succeed,
                        Argument = (DateTime?)j.Job.Args.FirstOrDefault()
                    });
            jobInfo.PastJobs.AddRange(s);


            var f = failed.Select(j => j.Value).Where(j => j.Job?.Type != null)
                .Where(j => j.Job.Type.Name == jobDescriptor.Type.Name).Select(j =>
                    new PastJobInfo
                    {
                        End = j.FailedAt,
                        Exception = j.ExceptionMessage,
                        JobState = JobState.Failed,
                        Argument = (DateTime?)j.Job.Args.FirstOrDefault()
                    });
            jobInfo.PastJobs.AddRange(f);

            var p = processing.Select(j => j.Value).Where(j => j.Job?.Type != null)
                .Where(j => j.Job.Type.Name == jobDescriptor.Type.Name).Select(j =>
                    new PastJobInfo
                    {
                        End = j.StartedAt,
                        JobState = JobState.Processing,
                        Argument = (DateTime?)j.Job.Args.FirstOrDefault()
                    });
            jobInfo.PastJobs.AddRange(p);

            jobInfo.PastJobs = jobInfo.PastJobs.OrderByDescending(j => j.End)
                .ToList();

            var lastJob = jobInfo.PastJobs.MaxBy(j => j.End);

            if (lastJob != null)
            {
                jobInfo.End = lastJob.End;
                jobInfo.Exception = lastJob.Exception;
                jobInfo.Argument = lastJob.Argument;
                jobInfo.JobState = lastJob.JobState;
            }

            result.Add(jobInfo);
        }

        return result;
    }

    public List<JobDescriptor> DiscoverJobs()
    {
        var jobDescriptors = new List<JobDescriptor>();
        var assembly = AssemblyRoot.GetRootAssembly();
        try
        {
            var types = assembly.GetTypes()
                .Where(t => typeof(IRecurringJob).IsAssignableFrom(t) &&
                            t is { IsInterface: false, IsAbstract: false } &&
                            t.GetCustomAttribute<RecurringJobAttribute>() != null)
                .ToList();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<RecurringJobAttribute>();
                if (attribute != null)
                    jobDescriptors.Add(new JobDescriptor
                    {
                        Type = type,
                        Id = attribute.JobId,
                        Cron = attribute.Cron,
                        Name = attribute.JobName,
                        RunOnStart = attribute.RunOnStart
                    });
            }

            _logger.LogDebug("Found {Count} job types in assembly {AssemblyName}",
                types.Count, assembly.GetName().Name);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to scan assembly {AssemblyName} for job types",
                assembly.GetName().Name);
        }

        return jobDescriptors;
    }
}