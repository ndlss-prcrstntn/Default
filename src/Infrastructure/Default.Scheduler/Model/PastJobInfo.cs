namespace Default.Scheduler.Model;

public class PastJobInfo
{
    public DateTime? End { get; set; }

    public DateTime? Argument { get; set; }

    public JobState JobState { get; set; }

    public long? Duration { get; set; }

    public string? Exception { get; set; }
}