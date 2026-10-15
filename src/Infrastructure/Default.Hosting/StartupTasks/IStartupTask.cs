namespace Default.Hosting.StartupTasks;

public interface IStartupTask
{
    int Order();
    Task ExecuteAsync(CancellationToken cancellationToke = default);
}