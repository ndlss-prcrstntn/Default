using Hangfire;
using Microsoft.Extensions.Logging;

namespace Default.Scheduler.Abstractions;

public abstract class BaseRecurringJob<T>(ILogger<T> logger) : IRecurringJob
    where T : class
{
    public async Task ExecuteBase(DateTime? start)
    {
        try
        {
            var jobTypeName = typeof(T).Name;
            logger.LogInformation("Запуск задачи {jobTypeName}", jobTypeName);
            var effectiveStart = start ?? GetLastSuccessfulExecutionStart();
            await Execute(effectiveStart);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении задачи {Type}", typeof(T).Name);
            throw;
        }
    }

    protected abstract Task Execute(DateTime start);

    private DateTime GetLastSuccessfulExecutionStart()
    {
        try
        {
            var jobTypeName = typeof(T).Name;

            var monitoring = JobStorage.Current.GetMonitoringApi();
            var succeeded = monitoring.SucceededJobs(0, 100).Select(j => j.Value)
                .Where(j => j.Job != null && j.Job.Type.Name == jobTypeName)
                .MaxBy(f => f.SucceededAt);

            var result = succeeded is { SucceededAt: not null, TotalDuration: not null }
                ? succeeded.SucceededAt.Value.AddMilliseconds(-succeeded.TotalDuration.Value)
                : DateTime.MinValue;

            logger.LogInformation("Для задачи {Type} определена дата начала: {StartDate}",
                jobTypeName, result);

            return result;
        }
        catch (Exception e)
        {
            return DateTime.MinValue;
        }
    }
}