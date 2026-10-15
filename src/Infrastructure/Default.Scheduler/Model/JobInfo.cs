namespace Default.Scheduler.Model;

public class JobInfo
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Cron { get; set; }

    public DateTime? NextExecution { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public DateTime? CreatedAt { get; set; }

    public List<PastJobInfo> PastJobs { get; set; } = [];

    public JobState? JobState { get; set; }

    public string? Exception { get; set; }

    public DateTime? Argument { get; set; }
}