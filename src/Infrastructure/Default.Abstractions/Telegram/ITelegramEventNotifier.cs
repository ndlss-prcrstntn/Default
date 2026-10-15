namespace Default.Abstractions.Telegram;

/// <summary>Outbound Telegram notifications for catalog events (approved / lineup / reminders).</summary>
public interface ITelegramEventNotifier
{
    Task NotifyEventApprovedAsync(Guid eventId, CancellationToken cancellationToken = default);

    Task NotifyLineupChangedAsync(Guid eventId, CancellationToken cancellationToken = default);

    Task NotifyOneHourRemindersAsync(CancellationToken cancellationToken = default);
}

/// <summary>Default no-op so Events can inject without TelegramBots.</summary>
public sealed class NullTelegramEventNotifier : ITelegramEventNotifier
{
    public static NullTelegramEventNotifier Instance { get; } = new();

    public Task NotifyEventApprovedAsync(Guid eventId, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task NotifyLineupChangedAsync(Guid eventId, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task NotifyOneHourRemindersAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
