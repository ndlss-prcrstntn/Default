namespace Default.Abstractions.Realtime;

public interface ICatalogRealtimePublisher
{
    Task PublishAsync(CatalogEntityChangedMessage message, CancellationToken cancellationToken = default);
}

public sealed class CatalogEntityChangedMessage
{
    /// <summary>Same numbering as LikeTargetType: Artist=1, Event=2, Promo=3, Venue=4.</summary>
    public required int EntityType { get; init; }

    public required Guid EntityId { get; init; }

    /// <summary>created | updated | statusChanged | deleted | liked</summary>
    public required string Change { get; init; }

    public int? LikesCount { get; init; }

    public string? Version { get; init; }
}

/// <summary>Default no-op so domain modules can inject the interface without SignalR.</summary>
public sealed class NullCatalogRealtimePublisher : ICatalogRealtimePublisher
{
    public Task PublishAsync(CatalogEntityChangedMessage message, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
