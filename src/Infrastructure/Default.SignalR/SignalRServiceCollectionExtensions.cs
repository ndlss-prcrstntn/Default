using Default.Abstractions.Realtime;
using Default.SignalR.HubFilters;
using Default.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Default.SignalR;

public static class SignalRServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultSignalR(this IServiceCollection services)
    {
        services.AddSingleton<IEntityRealtimePublisher, EntityRealtimePublisher>();
        services.AddSignalR(options => { options.AddFilter<ExceptionFilter>(); });
        return services;
    }

    public static IEndpointRouteBuilder MapDefaultSignalR(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<EntityHub>(EntityHub.HubPath);
        return endpoints;
    }
}
