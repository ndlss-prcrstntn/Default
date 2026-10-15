using Default.Abstractions.Realtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Default.SignalR.Hubs;

/// <summary>Sample authorized hub for entity-change subscriptions by type.</summary>
[Authorize]
public class EntityHub : Hub
{
    public const string HubPath = "/hub/entities";
    public const string EntityChangedEvent = "entityChanged";

    public static string TypeGroup(int entityType) => $"entity:{entityType}";

    public Task JoinType(int entityType)
        => Groups.AddToGroupAsync(Context.ConnectionId, TypeGroup(entityType));

    public Task LeaveType(int entityType)
        => Groups.RemoveFromGroupAsync(Context.ConnectionId, TypeGroup(entityType));
}

public sealed class EntityRealtimePublisher(IHubContext<EntityHub> hub) : IEntityRealtimePublisher
{
    public Task PublishAsync(EntityChangedMessage message, CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            entityType = message.EntityType,
            entityId = message.EntityId,
            change = message.Change,
            likesCount = message.LikesCount,
            version = message.Version ?? Guid.NewGuid().ToString("N"),
        };

        return hub.Clients
            .Group(EntityHub.TypeGroup(message.EntityType))
            .SendAsync(EntityHub.EntityChangedEvent, payload, cancellationToken);
    }
}
