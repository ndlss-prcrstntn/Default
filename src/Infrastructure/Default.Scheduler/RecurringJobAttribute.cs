using Default.Abstractions.Exceptions;

namespace Default.Scheduler;

[AttributeUsage(AttributeTargets.Class)]
public class RecurringJobAttribute(string jobId, string jobName, string cronExpression, bool runOnStart = false)
    : Attribute
{
    public string JobId { get; } = jobId ?? throw new ClientException(nameof(jobId));

    public string Cron { get; } = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));

    public string JobName { get; } = jobName ?? throw new ArgumentNullException(nameof(jobName));

    public bool RunOnStart { get; } = runOnStart;
}