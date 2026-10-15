using Hangfire.Storage;

namespace Default.Scheduler.Model.Ext;

public static class RecurringJobDtoExtensions
{
    public static JobInfo ToJobInfo(this RecurringJobDto recurringJobDto, string name)
    {
        if (recurringJobDto == null)
            throw new ArgumentNullException(nameof(recurringJobDto));

        return new JobInfo
        {
            Id = recurringJobDto.Id,
            Name = name,
            Cron = recurringJobDto.Cron,
            NextExecution = recurringJobDto.NextExecution,
            Start = recurringJobDto.LastExecution,
            CreatedAt = recurringJobDto.CreatedAt,
            JobState = MapJobState(recurringJobDto.LastJobState),
            Exception = recurringJobDto.Error,
            PastJobs = [] // Пустой список, требует отдельной логики для заполнения
        };
    }

    private static JobState? MapJobState(string? hangfireJobState)
    {
        return hangfireJobState switch
        {
            "Succeeded" => JobState.Succeed,
            "Failed" => JobState.Failed,
            "Processing" => JobState.Processing,
            _ => null
        };
    }
}