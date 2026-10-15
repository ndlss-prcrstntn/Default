using Default.Abstractions;
using Default.Hosting;
using Default.Logs.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Default.Logs;

public static class ObservabilityHealthChecksExtensions
{
    public static IServiceCollection AddObservabilityHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IVersionExtractor, AssemblyVersionExtractor>();
        services.AddHttpClient(MinioEndpointHealthCheck.HttpClientName)
            .ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(2));

        var builder = services.AddHealthChecks();
        builder.AddCheck("self", () => HealthCheckResult.Healthy(), tags: [ObservabilityHealthTags.Live]);

        var postgres = configuration["dataAccess:connection"];
        builder.AddCheck(
            "postgres",
            new PostgresHealthCheck(postgres ?? string.Empty),
            tags: [ObservabilityHealthTags.Ready],
            timeout: TimeSpan.FromSeconds(3));

        var keyDb = configuration["keyDB:connection"];
        if (!string.IsNullOrWhiteSpace(keyDb))
        {
            builder.AddCheck(
                "keydb",
                new KeyDbHealthCheck(keyDb),
                tags: [ObservabilityHealthTags.Ready],
                timeout: TimeSpan.FromSeconds(3));
        }

        var minioEndpoint = configuration["s3:endpoint"];
        if (Uri.TryCreate(minioEndpoint, UriKind.Absolute, out var minioBase))
        {
            var healthUri = new Uri(minioBase, "/minio/health/live");
            builder.Add(new HealthCheckRegistration(
                "minio",
                sp =>
                {
                    var http = sp.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(MinioEndpointHealthCheck.HttpClientName);
                    return new MinioEndpointHealthCheck(http, healthUri);
                },
                HealthStatus.Unhealthy,
                [ObservabilityHealthTags.Ready],
                TimeSpan.FromSeconds(3)));
        }

        return services;
    }
}
