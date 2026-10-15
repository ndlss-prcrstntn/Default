namespace Default.Scheduler.Abstractions;

public interface IRecurringJob
{
    public Task ExecuteBase(DateTime? start);
}