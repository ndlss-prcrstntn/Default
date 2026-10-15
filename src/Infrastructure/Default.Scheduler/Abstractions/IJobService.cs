using Default.Scheduler.Model;

namespace Default.Scheduler.Abstractions;

public interface IJobService
{
    /// <summary>
    ///     Запускает задачу по её идентификатору
    /// </summary>
    /// <param name="jobId">Идентификатор задачи</param>
    /// <param name="startTime">Время начала выполнения (опционально)</param>
    /// <returns>Идентификатор запущенной задачи в Hangfire</returns>
    void ExecuteJob(string jobId, DateTime? startTime = null);

    /// <summary>
    ///     Получает список всех доступных задач
    /// </summary>
    /// <returns>Список задач с выполненными ранее</returns>
    List<JobInfo> GetAvailableJobs();

    /// <summary>
    ///     Возвращает список с дескрипторами задач
    /// </summary>
    /// <returns>Список дескрипторов с данными по задачам</returns>
    List<JobDescriptor> DiscoverJobs();
}