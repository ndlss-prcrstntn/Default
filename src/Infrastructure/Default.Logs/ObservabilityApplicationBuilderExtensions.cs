using System.Reflection;
using Default.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;

namespace Default.Logs;

public static class ObservabilityApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder MapObservability(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks(ObservabilityEndpoints.Live, new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(ObservabilityHealthTags.Live),
        }).AllowAnonymous().ExcludeFromDescription();

        endpoints.MapHealthChecks(ObservabilityEndpoints.Ready, new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(ObservabilityHealthTags.Ready),
        }).AllowAnonymous().ExcludeFromDescription();

        endpoints.MapPrometheusScrapingEndpoint(ObservabilityEndpoints.Metrics)
            .AllowAnonymous()
            .ExcludeFromDescription();

        endpoints.MapGet(ObservabilityEndpoints.Version, (
                IVersionExtractor versions,
                IHostEnvironment environment,
                IConfiguration configuration) =>
            {
                var gitSha = configuration["GIT_SHA"];
                if (string.IsNullOrWhiteSpace(gitSha))
                    gitSha = Environment.GetEnvironmentVariable("GIT_SHA");

                return Results.Json(new ApplicationVersionInfo
                {
                    Version = string.IsNullOrWhiteSpace(versions.GetVersion())
                        ? Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "0.0.0.0"
                        : versions.GetVersion(),
                    GitSha = string.IsNullOrWhiteSpace(gitSha) ? null : gitSha,
                    Environment = environment.EnvironmentName,
                });
            })
            .AllowAnonymous()
            .ExcludeFromDescription();

        return endpoints;
    }
}

public sealed class ApplicationVersionInfo
{
    public required string Version { get; init; }

    public string? GitSha { get; init; }

    public required string Environment { get; init; }
}
