namespace Default.Abstractions.Realtime;

/// <summary>Publishes entity-change notifications to realtime clients (e.g. SignalR).</summary>
public interface IEntityRealtimePublisher
{
    Task PublishAsync(EntityChangedMessage message, CancellationToken cancellationToken = default);
}

public sealed class EntityChangedMessage
{
    /// <summary>Product-defined entity type discriminator (int or enum cast).</summary>
    public required int EntityType { get; init; }

    public required Guid EntityId { get; init; }

    /// <summary>created | updated | statusChanged | deleted | liked (or your own vocabulary).</summary>
    public required string Change { get; init; }

    public int? LikesCount { get; init; }

    public string? Version { get; init; }
}

/// <summary>No-op so modules can inject the interface without SignalR wired.</summary>
public sealed class NullEntityRealtimePublisher : IEntityRealtimePublisher
{
    public Task PublishAsync(EntityChangedMessage message, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
