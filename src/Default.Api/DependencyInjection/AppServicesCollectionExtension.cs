using Default.FileStorage;
using Default.Mail;
using Default.SignalR;

namespace Default.Api.DependencyInjection;

/// <summary>
/// Composition-root wiring for infrastructure. Register domain modules here as they appear.
/// </summary>
public static class AppServicesCollectionExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFileStorage(configuration)
            .AddEmail(configuration)
            .AddDefaultSignalR();

        // Example when adding a domain module:
        // services.AddXxxModule(configuration);

        return services;
    }
}
