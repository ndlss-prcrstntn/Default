namespace Default.Scheduler;

public class JobDescriptor
{
    public Type Type { get; set; }

    public string Id { get; set; }

    public string Cron { get; set; }

    public string Name { get; set; }

    public bool RunOnStart { get; set; }
}