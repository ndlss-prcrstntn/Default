using Default.Abstractions.Realtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Default.SignalR.Hubs;

[Authorize]
public class CatalogHub : Hub
{
    public const string HubPath = "/hub/catalog";
    public const string EntityChangedEvent = "entityChanged";

    public static string TypeGroup(int entityType) => $"catalog:{entityType}";

    public Task JoinType(int entityType)
        => Groups.AddToGroupAsync(Context.ConnectionId, TypeGroup(entityType));

    public Task LeaveType(int entityType)
        => Groups.RemoveFromGroupAsync(Context.ConnectionId, TypeGroup(entityType));
}

public sealed class CatalogRealtimePublisher(IHubContext<CatalogHub> hub) : ICatalogRealtimePublisher
{
    public Task PublishAsync(CatalogEntityChangedMessage message, CancellationToken cancellationToken = default)
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
            .Group(CatalogHub.TypeGroup(message.EntityType))
            .SendAsync(CatalogHub.EntityChangedEvent, payload, cancellationToken);
    }
}
